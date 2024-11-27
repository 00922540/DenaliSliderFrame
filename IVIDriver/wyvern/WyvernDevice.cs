using System;
using System.Threading;
using Keysight.ModularInstruments.Core.Register;
using Keysight.ModularInstruments.KtM9347.Registers;
using System.Collections.Generic;

namespace Keysight.ModularInstruments.KtM9347.Peripherals
{
    public class WyvernDevice 
    {
        #region Member Variables
        public readonly string ClockDetNode = "CLK1_IN_DET";
        private bool mIsFrequencyWritePending;
        private bool mIsScaleWritePending;

        private WyvernRegisterSet RegisterSet;
        public string Name
        {
            get;
            protected set;
        }
        #endregion

        public WyvernDevice(string name, WyvernRegisterSet registerSet, string clockDetAbusNode)            
        {
            ClockDetNode = clockDetAbusNode;
            RegisterSet = registerSet;
        }

        #region Properties       

        private IRegister ClkaCfg
        {
            get
            {
                return Registers[(int)WyvernReg.ClkaCfg];
            }
        }

        private IRegister ClkdCfg
        {
            get
            {
                return Registers[(int)WyvernReg.ClkdCfg];
            }
        }

        private IRegister[] Registers
        {
            get
            {
                return RegisterSet.Registers;
            }
        }

        #endregion

        /// <summary>
        /// Initialize the peripheral.
        /// </summary>
        /// <param name="IsPowerOn">if true, this is the first time Initialize() has been called since power on.</param>
        /// <remarks>
        /// "Public use" is intended for short term use early during turn-on to allow explicit control over device initialization.
        /// </remarks>
        public void Initialize(bool IsPowerOn)
        {
            Initialize(IsPowerOn, 6e9);
        }

        /// <summary>
        /// Initialize the peripheral.
        /// Follows initialization procedure laid out in the ERS
        /// </summary>
        /// <param name="IsPowerOn">if true, this is the first time Initialize() has been called since power on.</param>
        /// <param name="clockRate"></param>
        /// <remarks>
        /// "Public use" is intended for short term use early during turn-on to allow explicit control over device initialization.
        /// </remarks>
        public void Initialize(bool IsPowerOn, double clockRate)
        {           
            LogMessage(string.Format("Initializing {0}.", Name));

            // 1) Apply MCLK and drive NReset pin low
            //      Handled at the module level at initialization

            // 2) Apply DC power
            //      Handled at the module level at initialization

            // 3) Apply SCLK
            //      Handled at the module level at initialization

            // 4) Hold the NReset pin low for at least NNN SCLK cycles, than drive high
            //      Handled at the module level since it effects all Wyvern chips

            // 5) If using GlobalDelay, switch it into the AClk path
            //      Using GlobalDelay greatly degrades phase noise performance.  At this time we will ignore it

            // 6) If MClk is faster than 12 GHz, enable clock feedback and enable the Missing MClk Watchdog circuit

            if (clockRate >= 12e9)
            {
                LogMessage("Clock rate faster than 12 GHz.");
                EnableMissingClockWatchdog();
                ConfigureDutyCycleCorrection(true);
            }

            // 7) If MClk is faster than 10 GHz, configure the CML bias current.

            if (clockRate >= 10e9)
            {
                DoCMLSpeedAdjustment(clockRate);
            }

            // 8) Align the DLL's
            AlignDll(clockRate);

            // DDR mode on
            ConfigureDDR(InterpolationModeEmum.DDR);

            ConfigureOutMarks();

            LogMessage("Set the digital scaling");
            // Set the digital scaling to 0.5 (Scale uses a combination of an exponent and a 2's complement value...)
            // We will leave the exponent at the default value of 11 which corresponds to a multiply by two.
            // Therefore we have 2 x 0.25 = 0.5
            Registers[(int)WyvernReg.Scale].Fields[(int)ScaleBF.Scale].Write(0x2000);

            LogMessage("11) Wait for lock");
            Thread.Sleep(1000);
            //WaitForLock( "ISO" );
            //WaitForLock( "RS" );

            //Registers[ (int)WyvernReg.IsoLoopFilterStat1 ].Read32();
            //var locked = Registers[ (int)WyvernReg.IsoLoopFilterStat1 ].Fields[ (int)LoopFilterStat1BF.filtValid ].Value;

            //Registers[ (int)WyvernReg.RsLoopFilterStat1 ].Read32();
            //locked = Registers[ (int)WyvernReg.RsLoopFilterStat1 ].Fields[ (int)LoopFilterStat1BF.filtValid ].Value;

            LogMessage("Disable the loops");
            // Disable the DllClk
            Registers[(int)WyvernReg.DllClkCfg].Fields[(int)DllClkCfgBF.DataMarkSelect].Value = 6;
            Registers[(int)WyvernReg.DllClkCfg].Apply(true);
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.run].Value = 0;
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Apply(true);
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.run].Value = 0;
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Apply(true);

            // 12) Enable the DAC output
            EnableDACOutput();


        }

        /// <summary>
        /// The high speed clock circuits use a form of negative feedback that increases maximum operating
        /// frequency by reducing sensitivity to offsets.  For MCLK rates higher than 12 GHz, feedback is 
        /// required.  Below 12 GHz, it should be turned off to save power.  If feedback is left on while the 
        /// clock is stuck at a static 1 or 0, excessive heating will occur in the clock amplifiers, shortening
        /// their lifetime.
        /// </summary>
        /// <param name="enabled"></param>
        public void ConfigureClockFeedbackCircuits(bool enabled)
        {
            LogMessage("Enabling Clock Feedback Circuits.");

            Registers[(int)WyvernReg.DllPhaseDetCfg10].Fields[(int)DllPhaseDetCfg1BF.DelayDllClk].Value = 6;
            Registers[(int)WyvernReg.DllPhaseDetCfg10].Fields[(int)DllPhaseDetCfg1BF.fbEn].Value = enabled ? 1 : 0;
            Registers[(int)WyvernReg.DllPhaseDetCfg10].Apply(true);

            Registers[(int)WyvernReg.DllPhaseDetCfg11].Fields[(int)DllPhaseDetCfg1BF.DelayDllClk].Value = 6;
            Registers[(int)WyvernReg.DllPhaseDetCfg11].Fields[(int)DllPhaseDetCfg1BF.fbEn].Value = enabled ? 1 : 0;
            Registers[(int)WyvernReg.DllPhaseDetCfg11].Apply(true);
        }


        /// <summary>
        /// If the input MCLK is missing (e.g. disconnected or turned off) the input clock chain might self-oscillate at around
        /// 16-20 GHz, or it might sit at a static state.  The static condition creates destructive self-heating that would 
        /// reduce the life of the chip
        /// 
        /// To avoid this, there is a watchdog circuit on the chip that will be triggered when the MCLK frequency drops below
        /// around 200 MHz.
        /// </summary>
        public void EnableMissingClockWatchdog()
        {
            LogMessage("Enabling Missing Clock Watchdog Circuit.");

            #region Configure Digital System Clock

            LogMessage("Configuring Digital System Clock.");

            ClkdCfg.Fields[(int)ClkdCfgBF.FpgaClkCount].Value = 7;
            ClkdCfg.Fields[(int)ClkdCfgBF.FpgaClkMux].Value = 1;

            // Enable feedback peaking in IClk receiver
            ClkdCfg.Fields[(int)ClkdCfgBF.FbEnIClkRx].Value = 1;

            // Enable feedback peaking in master divide-by-2
            ClkdCfg.Fields[(int)ClkdCfgBF.FbEnDiv].Value = 1;

            // Disable feedback peaking in muxClkDelay timing DAC
            ClkdCfg.Fields[(int)ClkdCfgBF.FbEnMuxClkDelay].Value = 0;

            // Enable feedback peaking protection by watchdog
            ClkdCfg.Fields[(int)ClkdCfgBF.WatchdogForceEn].Value = 1;

            ClkdCfg.Apply(true);

            #endregion

            #region Configure Analog System Clock

            LogMessage("Configuring Analog System Clock.");

            ClkaCfg.Fields[(int)ClkaCfgBF.IClk2Off].Value = 1;

            // Enable feedback peaking in GlobalDelay timing DAC
            ClkaCfg.Fields[(int)ClkaCfgBF.FbEnGlobalDelay].Value = 1;

            // Enable feedback peaking in IClk buffers
            ClkaCfg.Fields[(int)ClkaCfgBF.FbEnIClkDrive].Value = 1;

            // Enable feedback peaking in LClkDelay timing DAC
            ClkaCfg.Fields[(int)ClkaCfgBF.FbEnLClkDelay].Value = 1;

            // Enable feedback peaking in LClk buffers
            ClkaCfg.Fields[(int)ClkaCfgBF.FbEnLClkDrive].Value = 1;

            // Enable feedback peaking in CML-to-CMOS level shift
            ClkaCfg.Fields[(int)ClkaCfgBF.FbEnLs].Value = 1;

            // Enable feedback peaking in AClk buffers (only if AClk active.  i.e. not in NRZ mode)
            ClkaCfg.Fields[(int)ClkaCfgBF.FbEnAClkDrive].Value = 1;

            //if ( ClkaCfg.Fields[ (int)ClkaCfgBF.AClkMux1 ].Value == 0 ||
            //    ClkaCfg.Fields[ (int)ClkaCfgBF.AClkMux2 ].Value == 1 )
            //{
            //    ClkaCfg.Fields[ (int)ClkaCfgBF.FbEnAClkDrive ].Value = 1;
            //}
            //else
            //{
            //    ClkaCfg.Fields[ (int)ClkaCfgBF.FbEnAClkDrive ].Value = 0;
            //}
            // Disable feedback peaking in AClkMux2
            ClkaCfg.Fields[(int)ClkaCfgBF.FbEnAClkMux2].Value = 0;

            //if ( ClkaCfg.Fields[ (int)ClkaCfgBF.AClkMux1 ].Value == 1 &&
            //    ClkaCfg.Fields[ (int)ClkaCfgBF.AClkMux2 ].Value == 1 )
            //{
            //    // Enable only if GlobalDelay is un use
            //    ClkaCfg.Fields[ (int)ClkaCfgBF.FbEnAClkMux2 ].Value = 1;
            //}
            //else
            //{
            //    ClkaCfg.Fields[ (int)ClkaCfgBF.FbEnAClkMux2 ].Value = 0;
            //}

            // Disable feedback peaking in TestClkDelay1 timing DAC
            ClkaCfg.Fields[(int)ClkaCfgBF.FbEnTestClkDelay1].Value = 0;

            // Disable feedback peaking in TestClkDelay2 timing DAC
            ClkaCfg.Fields[(int)ClkaCfgBF.FbEnTestClkDelay2].Value = 0;

            // Disable feedback peaking in IClk2 buffers
            ClkaCfg.Fields[(int)ClkaCfgBF.FbEnIClk2Drive].Value = 0;

            ClkaCfg.Apply(true);

            // Enable feedback peaking in AClk buffer for OutMark resampler and resampling DLL phase detector
            Registers[(int)WyvernReg.ClkaSpare].Fields[(int)ClkaSpareBF.FbEnAClkBufMark].Value = 1;

            // Enable feedback peaking in final FF slices
            Registers[(int)WyvernReg.ClkaSpare].Fields[(int)ClkaSpareBF.FbEnFffSlice].Value = 1;

            // Enable feedback peaking protection by watchdog
            Registers[(int)WyvernReg.ClkaSpare].Fields[(int)ClkaSpareBF.WatchdogForceEn].Value = 1;

            Registers[(int)WyvernReg.ClkaSpare].Apply(true);

            #endregion

            ConfigureClockFeedbackCircuits(true);

            LogMessage("Enabling Watchdog Circuit.");

            // Enable the watchdog circuit
            Registers[(int)WyvernReg.ClkdCfg].Fields[(int)ClkdCfgBF.WatchdogEn].Value = 1;
            Registers[(int)WyvernReg.ClkdCfg].Apply(true);
        }


        /// <summary>
        /// The master system clock is divided by two to drive the last stage of the final mux serializer.  A duty
        /// cycle error in ckDiv2 translates to an odd/even timing error in the serialized data, which degrades the
        /// eye opening.  When the MCLK is above 12 GHz, the internal duty cycle correction circuit should be enabled.
        /// </summary>
        /// <param name="enabled"></param>
        public void ConfigureDutyCycleCorrection(bool enabled)
        {
            LogMessage(string.Format("{0} Duty Cycle Corrections.", enabled ? "Enabling" : "Disabling"));

            ClkdCfg.Fields[(int)ClkdCfgBF.DutyEn].Value = enabled ? 1 : 0;
            ClkdCfg.Apply(true);
        }

        /// <summary>
        /// The digital circuits in the DAC are primarily current-mode logic (CML).  The differential inputs and 
        /// outputs use nearly constant supply current for lower switching noise than full-swing CMOS.  The bias 
        /// required is a function of clock rate, and should be configured any time the MCLK rate is above 10 GHz.
        /// </summary>
        /// <param name="clockRate">Master system clock rate, in Hz.</param>
        public void DoCMLSpeedAdjustment(double clockRate)
        {
            LogMessage("Adjusting CML Bias.");

            // Calculate the BIAS code
            double clockRateGHz = clockRate / 1e9;
            int biasCode = (int)Math.Round(128 * clockRateGHz / 20);
            Console.WriteLine("CML Bias Code: {0}", biasCode);

            // Set the CML bias currents.
            Registers[(int)WyvernReg.DacCfg2].Fields[(int)DacCfg2BF.CmlIref1].Value = biasCode;
            Registers[(int)WyvernReg.DacCfg2].Fields[(int)DacCfg2BF.CmlIref2].Value = biasCode;
            Registers[(int)WyvernReg.DacCfg2].Apply(false);

            Registers[(int)WyvernReg.DacCfg5].Fields[(int)DacCfg5BF.CmlIrefp1].Value = biasCode;
            Registers[(int)WyvernReg.DacCfg5].Fields[(int)DacCfg5BF.CmlIrefp2].Value = biasCode;
            Registers[(int)WyvernReg.DacCfg5].Apply(false);

            // Set the CML bias lowpass filters to the 400 Hz BW path.
            Registers[(int)WyvernReg.DacCfg3].Fields[(int)DacCfg3BF.CmlLpfEn1].Value = 0x3;
            Registers[(int)WyvernReg.DacCfg3].Fields[(int)DacCfg3BF.CmlLpfEn2].Value = 0x3;
            Registers[(int)WyvernReg.DacCfg3].Apply(false);
        }

        public void EnableDACOutput()
        {
            LogMessage("Enabling DAC Output.");

            // Disable mid-scale forcing of Aout
            Registers[(int)WyvernReg.DacCfg1].Fields[(int)DacCfg1BF.Midscale].Write(0);

            // Set the Final MUX source to normal mode, and enable midscale forcing by over-voltage detector
            Registers[(int)WyvernReg.FinalMuxInCfg].Fields[(int)FinalMuxInCfgBF.Source].Value = 2;
            Registers[(int)WyvernReg.FinalMuxInCfg].Fields[(int)FinalMuxInCfgBF.OverForceEnable].Value =
                1;
            Registers[(int)WyvernReg.FinalMuxInCfg].Apply(true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clockRate">The MClk rate, in Hz.</param>
        public void AlignDll(double clockRate)
        {
            LogMessage("---------  Aligning DLL's.  ---------");

            LogMessage("Configure DataMark0 to propagate whenever this is a new Mark0 or Mark0 register write");
            Registers[(int)WyvernReg.InMarkCfg].Fields[(int)InMarkCfgBF.Cfg0].Value = 1;
            Registers[(int)WyvernReg.InMarkCfg].Apply(true);

            LogMessage("1) Configure the random DataMark generator for an average CClk/DataMark divide ration of 18.5");
            Registers[(int)WyvernReg.DllClkCreateMark].Fields[(int)DllClkCreateMarkBF.Enable].Value = 1;
            Registers[(int)WyvernReg.DllClkCreateMark].Fields[(int)DllClkCreateMarkBF.cfgMin].Value = 2;
            Registers[(int)WyvernReg.DllClkCreateMark].Fields[(int)DllClkCreateMarkBF.cfgMask].Value = 0;
            Registers[(int)WyvernReg.DllClkCreateMark].Apply(true);

            LogMessage(
                "2) Configure the DllClk generator in pulse mode, pulse width 32 MClk cycles, using DataMark0 for loop filter initialization");
            Registers[(int)WyvernReg.DllClkCfg].Fields[(int)DllClkCfgBF.Delay].Value = 0x100;
            Registers[(int)WyvernReg.DllClkCfg].Fields[(int)DllClkCfgBF.DataMarkSelect].Value = 0;
            Registers[(int)WyvernReg.DllClkCfg].Fields[(int)DllClkCfgBF.Mode].Value = 0;
            Registers[(int)WyvernReg.DllClkCfg].Apply(true);
            Registers[(int)WyvernReg.DllClkWidth].Fields[(int)DllClkWidthBF.Width].Value = 32;
            Registers[(int)WyvernReg.DllClkWidth].Apply(true);

            LogMessage("3) Enable DllClk in the custom region");
            // TODO - This is writing the wrong bitfield...
            Registers[(int)WyvernReg.FinalMuxCfg].Value32 = 0x0;
            Registers[(int)WyvernReg.FinalMuxCfg].Fields[(int)FinalMuxCfgBF.DllClkEn].Value = 1;
            Registers[(int)WyvernReg.FinalMuxCfg].Apply(true);
            Registers[(int)WyvernReg.DacCfg1].Value32 = 0x3e40;
            Registers[(int)WyvernReg.DacCfg1].Fields[(int)DacCfg1BF.DllClkRxEn].Value = 1;
            Registers[(int)WyvernReg.DacCfg1].Apply(true);

            // Configure the DLL loops
            ConfigureIsolationCrossingLoop(clockRate);
            ConfigureResamplingLoop(clockRate);

            LogMessage("Restore mark configuration");
            Registers[(int)WyvernReg.InMarkCfg].Value32 = 0;
            Registers[(int)WyvernReg.InMarkCfg].Apply(true);

            LogMessage("Enable loops");
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.run].Value = 1;
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Apply(true);
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.run].Value = 1;
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Apply(true);

            LogMessage("10) Enable the pseudo-random DllClk to start the loops running");
            Registers[(int)WyvernReg.DllClkCfg].Fields[(int)DllClkCfgBF.DataMarkSelect].Value = 5;
            Registers[(int)WyvernReg.DllClkCfg].Apply(true);
        }

        public void ConfigureIsolationCrossingLoop(double clockRate)
        {
            LogMessage("------ Configure the isolation crossing loop ------");
            LogMessage("Configure the isolation crossing phase detector");
            Registers[(int)WyvernReg.DllPhaseDetCfg10].Fields[(int)DllPhaseDetCfg1BF.DelayClk].Value = 0;
            Registers[(int)WyvernReg.DllPhaseDetCfg10].Fields[(int)DllPhaseDetCfg1BF.DelayDllClk].Value = 11;
            Registers[(int)WyvernReg.DllPhaseDetCfg10].Fields[(int)DllPhaseDetCfg1BF.DithNreset].Value = 0;
            // TODO - This is different between the ERS and the Script!
            Registers[(int)WyvernReg.DllPhaseDetCfg10].Fields[(int)DllPhaseDetCfg1BF.DithScale].Value = 0;
            Registers[(int)WyvernReg.DllPhaseDetCfg10].Apply(true);

            Registers[(int)WyvernReg.DllPhaseDetCfg20].Fields[(int)DllPhaseDetCfg2BF.BiasAdj].Value = 120;
            Registers[(int)WyvernReg.DllPhaseDetCfg20].Apply(true);

            // Compute the starting delay code
            // Average timing DAC step (nominally 150 fs.)
            const double Tstep = 150e-15;
            int DStart = (int)Math.Round(Math.Min(1024, 0.75 / (clockRate * Tstep)));
            Console.WriteLine("Lock DLLs from starting code {0}", DStart);

            LogMessage("Initialize the loop filter delay registers");
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Value32 = 0x1006;
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.run].Value = 0;
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.load].Value = 1;
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Apply(true);

            Registers[(int)WyvernReg.IsoLoopFilterCfg2].Fields[(int)LoopFilterCfg2BF.loadVal].Value = DStart;
            Registers[(int)WyvernReg.IsoLoopFilterCfg2].Apply(true);

            LogMessage("Flow a DataMark0 to generate a single DllClk pulse and latch DStart into the delay registers");
            PropagateMark(WyvernDataMarkEnum.Mark0);

            //int ack = Registers[(int)WyvernReg.IsoLoopFilterStatAck].Read32();
            //// Initialize status request
            //Registers[(int)WyvernReg.IsoLoopFilterStatReq].Fields[(int)LoopFilterStatReqBF.request].Value = 1;
            //Registers[(int)WyvernReg.IsoLoopFilterStatReq].Apply(true);

            //// Send a couple of marks 
            //PropagateMark(WyvernDataMarkEnum.Mark0);
            //PropagateMark(WyvernDataMarkEnum.Mark0);
            //PropagateMark(WyvernDataMarkEnum.Mark0);
            //PropagateMark(WyvernDataMarkEnum.Mark0);

            //// Read back the delay value to see if it was loaded correctly
            //Registers[(int)WyvernReg.IsoLoopFilterStat2].Read32();
            //var delay = Registers[(int)WyvernReg.IsoLoopFilterStat2].Fields[(int)LoopFilterStat2BF.delay].Value;

            //ack = Registers[(int)WyvernReg.IsoLoopFilterStatAck].Read32();

            //Registers[(int)WyvernReg.IsoLoopFilterStatReq].Fields[(int)LoopFilterStatReqBF.request].Value = 0;
            //Registers[(int)WyvernReg.IsoLoopFilterStatReq].Apply(true);
            //ack = Registers[(int)WyvernReg.IsoLoopFilterStatAck].Read32();


            LogMessage("Disable delay register load");
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.load].Value = 0;
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Apply(true);

            LogMessage("Configure the isolation crossing loop filter");
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.filtShift].Value = 8;
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.invert].Value = 1;
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.sel].Value = 1;
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.thresh].Value = 4;
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.run].Value = 0;
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Apply(true);
        }

        public void ConfigureResamplingLoop(double clockRate)
        {
            LogMessage("------ Configure the resampling loop ------");

            // Average timing DAC step (nominally 150 fs.)
            const double Tstep = 150e-15;

            int delayDllClk;
            int biasAdj;
            double Tclk = 1 / clockRate;
            if (clockRate >= 12e9)
            {
                delayDllClk = (int)Math.Max(0, Math.Min(12, Math.Round(11 - 11 * ((Tclk - 50e-12) / 150e-12))));
                biasAdj = 120;
            }
            else
            {
                delayDllClk = 12;
                //biasAdj = (int)Math.Max( 70, Math.Min( 120, Math.Round( 120 - 50 * ( ( clockRate / 1e9 ) - 5 ) / 7 ) ) );
                biasAdj = (int)Math.Max(0, Math.Min(255, Math.Round(120 - 50 * (clockRate - 5e9) / 7e9)));
            }

            Console.WriteLine("Resampling loop DelayDllClk is {0}", delayDllClk);
            Console.WriteLine("Resampling loop BiasAdj is {0}", biasAdj);

            LogMessage("Configure the resampling phase detector");
            Registers[(int)WyvernReg.DllPhaseDetCfg11].Fields[(int)DllPhaseDetCfg1BF.DelayClk].Value = 0;
            Registers[(int)WyvernReg.DllPhaseDetCfg11].Fields[(int)DllPhaseDetCfg1BF.DelayDllClk].Value =
                delayDllClk;
            Registers[(int)WyvernReg.DllPhaseDetCfg11].Fields[(int)DllPhaseDetCfg1BF.DithNreset].Value = 0;
            // TODO - This is different between the ERS and the Script!
            Registers[(int)WyvernReg.DllPhaseDetCfg11].Fields[(int)DllPhaseDetCfg1BF.DithScale].Value = 0;
            Registers[(int)WyvernReg.DllPhaseDetCfg11].Apply(true);
            Registers[(int)WyvernReg.DllPhaseDetCfg21].Fields[(int)DllPhaseDetCfg2BF.BiasAdj].Value = biasAdj;
            Registers[(int)WyvernReg.DllPhaseDetCfg21].Apply(true);

            // Compute the starting delay code
            int DStart = (int)Math.Round(Math.Min(1024, 0.75 / (clockRate * Tstep)));

            LogMessage("Initialize the loop filter delay registers");
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Value32 = 0x1006;
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.run].Value = 0;
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.load].Value = 1;
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Apply(true);
            Registers[(int)WyvernReg.RsLoopFilterCfg2].Fields[(int)LoopFilterCfg2BF.loadVal].Value = DStart;
            Registers[(int)WyvernReg.RsLoopFilterCfg2].Apply(true);

            LogMessage("Flow a DataMark0 to generate a single DllClk pulse and latch DStart into the delay registers");
            PropagateMark(WyvernDataMarkEnum.Mark0);

            LogMessage("Disable delay register load");
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.load].Value = 0;
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Apply(true);

            LogMessage("Configure the resampling loop filter");
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.filtShift].Value = 8;
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.invert].Value = 0;
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.sel].Value = 1;
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.thresh].Value = 4;
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.run].Value = 0;
            // TODO - This is different between the ERS and the Script!
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Apply(true);
        }

        /// <summary>
        /// Wait for DLL to lock.  Valid only after DllClk is running!
        /// </summary>
        /// <param name="loop"></param>
        private void WaitForLock(string loop)
        {
            // TODO - This method is not working right now...status registers never change state
            IRegister reqRegister = null;
            IRegister ackRegister = null;

            if (loop == "ISO" || loop.ToLower().Contains("isolation"))
            {
                reqRegister = Registers[(int)WyvernReg.IsoLoopFilterStatReq];
                ackRegister = Registers[(int)WyvernReg.IsoLoopFilterStatAck];
            }

            if (loop == "RS" || loop.ToLower().Contains("resampling"))
            {
                reqRegister = Registers[(int)WyvernReg.RsLoopFilterStatReq];
                ackRegister = Registers[(int)WyvernReg.RsLoopFilterStatAck];
            }

            if (reqRegister == null || ackRegister == null)
            {
                throw new ArgumentException(
                    string.Format("Invalid loop: {0}.  Valid delay loops include 'isolation' or 'resampling'.", loop));
            }

            // Maximum time to wait for the loop to lock
            const int TIMEOUT_MS = 30000;
            const int INTERVAL_MS = 100;
            int timeWaited_ms = 0;

            // Request a status read
            reqRegister.Fields[(int)LoopFilterStatReqBF.request].Value = 1;
            reqRegister.Apply(true);

            // Read back the acknowledge until a 0 is seen
            while (true)
            {
                Thread.Sleep(INTERVAL_MS);
                timeWaited_ms += INTERVAL_MS;
                ackRegister.Read();
                var ack = ackRegister.Fields[(int)LoopFilterStatAckBF.acknowledge].Value;
                if (ack == 0)
                {
                    break;
                }

                if (timeWaited_ms > TIMEOUT_MS)
                {
                    throw new Exception("Loop failed to lock within 30 seconds!");
                }
            }

            // End the request
            reqRegister.Fields[(int)LoopFilterStatReqBF.request].Value = 0;
            reqRegister.Apply(true);

            // Read back the acknowledge until a 1 is seen
            timeWaited_ms = 0;
            while (true)
            {
                Thread.Sleep(INTERVAL_MS);
                timeWaited_ms += INTERVAL_MS;
                ackRegister.Read();
                var ack = ackRegister.Fields[(int)LoopFilterStatAckBF.acknowledge].Value;
                if (ack == 1)
                {
                    break;
                }

                if (timeWaited_ms > TIMEOUT_MS)
                {
                    throw new Exception("Loop failed to lock within 30 seconds!");
                }
            }
        }

        public void ConfigureDDR(InterpolationModeEmum mode)
        {
            LogMessage("Configure DDR Interpolation mode");
            Registers[(int)WyvernReg.CClkGate].Value32 = 0x3ffd;

            switch (mode)
            {
                case InterpolationModeEmum.SDR:
                    Registers[(int)WyvernReg.CClkGate].Fields[(int)CClkGateBF.DdrInterp].Value = 0;
                    Registers[(int)WyvernReg.CClkGate].Fields[(int)CClkGateBF.EncodingB].Value = 0;
                    Registers[(int)WyvernReg.CClkGate].Apply(true);

                    Registers[(int)WyvernReg.DdrInterpCfg].Fields[(int)DdrInterpCfgBF.Enable].Value = 0;
                    Registers[(int)WyvernReg.DdrInterpCfg].Fields[(int)DdrInterpCfgBF.DoubletMode].Value = 0;
                    Registers[(int)WyvernReg.DdrInterpCfg].Apply(true);

                    Registers[(int)WyvernReg.DacCfg1].Fields[(int)DacCfg1BF.DdrMuxEn].Value = 0;
                    Registers[(int)WyvernReg.DacCfg1].Fields[(int)DacCfg1BF.DdrRxEn].Value = 0;
                    Registers[(int)WyvernReg.DacCfg1].Apply(true);

                    Registers[(int)WyvernReg.FinalMuxCfg].Fields[(int)FinalMuxCfgBF.DdrEn].Value = 0;
                    Registers[(int)WyvernReg.FinalMuxCfg].Apply(true);

                    Registers[(int)WyvernReg.DemCfg].Fields[(int)DemCfgBF.Enable].Value = 1;
                    Registers[(int)WyvernReg.DemCfg].Fields[(int)DemCfgBF.DoDdr].Value = 0;
                    Registers[(int)WyvernReg.DemCfg].Fields[(int)DemCfgBF.DemLsbClip].Value = 0;
                    Registers[(int)WyvernReg.DemCfg].Apply(true);
                    break;

                case InterpolationModeEmum.SDRDoublet:
                    Registers[(int)WyvernReg.CClkGate].Fields[(int)CClkGateBF.DdrInterp].Value = 0;
                    Registers[(int)WyvernReg.CClkGate].Fields[(int)CClkGateBF.EncodingB].Value = 0;
                    Registers[(int)WyvernReg.CClkGate].Apply(true);

                    Registers[(int)WyvernReg.DdrInterpCfg].Fields[(int)DdrInterpCfgBF.Enable].Value = 0;
                    Registers[(int)WyvernReg.DdrInterpCfg].Fields[(int)DdrInterpCfgBF.DoubletMode].Value = 1;
                    Registers[(int)WyvernReg.DdrInterpCfg].Apply(true);

                    Registers[(int)WyvernReg.DacCfg1].Fields[(int)DacCfg1BF.DdrMuxEn].Value = 1;
                    Registers[(int)WyvernReg.DacCfg1].Fields[(int)DacCfg1BF.DdrRxEn].Value = 1;
                    Registers[(int)WyvernReg.DacCfg1].Apply(true);

                    Registers[(int)WyvernReg.FinalMuxCfg].Fields[(int)FinalMuxCfgBF.DdrEn].Value = 1;
                    Registers[(int)WyvernReg.FinalMuxCfg].Apply(true);

                    Registers[(int)WyvernReg.DemCfg].Fields[(int)DemCfgBF.Enable].Value = 1;
                    Registers[(int)WyvernReg.DemCfg].Fields[(int)DemCfgBF.DoDdr].Value = 0;
                    Registers[(int)WyvernReg.DemCfg].Fields[(int)DemCfgBF.DemLsbClip].Value = 0;
                    Registers[(int)WyvernReg.DemCfg].Apply(true);
                    break;

                case InterpolationModeEmum.DDR:
                    Registers[(int)WyvernReg.CClkGate].Fields[(int)CClkGateBF.DdrInterp].Value = 0;
                    Registers[(int)WyvernReg.CClkGate].Fields[(int)CClkGateBF.EncodingB].Value = 0;
                    Registers[(int)WyvernReg.CClkGate].Apply(true);

                    Registers[(int)WyvernReg.DdrInterpCfg].Fields[(int)DdrInterpCfgBF.Enable].Value = 1;
                    Registers[(int)WyvernReg.DdrInterpCfg].Fields[(int)DdrInterpCfgBF.DoubletMode].Value = 0;
                    Registers[(int)WyvernReg.DdrInterpCfg].Apply(true);

                    Registers[(int)WyvernReg.FinalMuxCfg].Fields[(int)FinalMuxCfgBF.DdrEn].Value = 1;
                    Registers[(int)WyvernReg.FinalMuxCfg].Apply(true);

                    Registers[(int)WyvernReg.DacCfg1].Fields[(int)DacCfg1BF.DdrMuxEn].Value = 1;
                    Registers[(int)WyvernReg.DacCfg1].Fields[(int)DacCfg1BF.DdrRxEn].Value = 1;
                    Registers[(int)WyvernReg.DacCfg1].Apply(true);

                    Registers[(int)WyvernReg.DemCfg].Fields[(int)DemCfgBF.Enable].Value = 1;
                    Registers[(int)WyvernReg.DemCfg].Fields[(int)DemCfgBF.DoDdr].Value = 1;
                    Registers[(int)WyvernReg.DemCfg].Fields[(int)DemCfgBF.DemLsbClip].Value = 0;
                    Registers[(int)WyvernReg.DemCfg].Apply(true);
                    break;

                case InterpolationModeEmum.DDRDoublet:
                    Registers[(int)WyvernReg.CClkGate].Fields[(int)CClkGateBF.DdrInterp].Value = 0;
                    Registers[(int)WyvernReg.CClkGate].Fields[(int)CClkGateBF.EncodingB].Value = 0;
                    Registers[(int)WyvernReg.CClkGate].Apply(true);

                    Registers[(int)WyvernReg.DdrInterpCfg].Fields[(int)DdrInterpCfgBF.Enable].Value = 1;
                    Registers[(int)WyvernReg.DdrInterpCfg].Fields[(int)DdrInterpCfgBF.DoubletMode].Value = 1;
                    Registers[(int)WyvernReg.DdrInterpCfg].Apply(true);

                    Registers[(int)WyvernReg.DacCfg1].Fields[(int)DacCfg1BF.DdrMuxEn].Value = 1;
                    Registers[(int)WyvernReg.DacCfg1].Fields[(int)DacCfg1BF.DdrRxEn].Value = 1;
                    Registers[(int)WyvernReg.DacCfg1].Apply(true);

                    Registers[(int)WyvernReg.FinalMuxCfg].Fields[(int)FinalMuxCfgBF.DdrEn].Value = 1;
                    Registers[(int)WyvernReg.FinalMuxCfg].Apply(true);

                    Registers[(int)WyvernReg.DemCfg].Fields[(int)DemCfgBF.Enable].Value = 1;
                    Registers[(int)WyvernReg.DemCfg].Fields[(int)DemCfgBF.DoDdr].Value = 1;
                    Registers[(int)WyvernReg.DemCfg].Fields[(int)DemCfgBF.DemLsbClip].Value = 0;
                    Registers[(int)WyvernReg.DemCfg].Apply(true);
                    break;
            }
        }

        public void ConfigureOutMarks()
        {
            LogMessage("Configure Out Marks");
            Registers[(int)WyvernReg.FinalMuxCfg].Fields[(int)FinalMuxCfgBF.OutMarkEn].Value = 15;
            Registers[(int)WyvernReg.FinalMuxCfg].Apply(true);

            Registers[(int)WyvernReg.DacCfg1].Fields[(int)DacCfg1BF.OutMarkRxEn].Value = 3;
            Registers[(int)WyvernReg.DacCfg1].Fields[(int)DacCfg1BF.OutMarkTxEn].Value = 3;
            Registers[(int)WyvernReg.DacCfg1].Fields[(int)DacCfg1BF.MarkRsEn].Value = 3;
            Registers[(int)WyvernReg.DacCfg1].Apply(true);

            Registers[(int)WyvernReg.CustomLvdsForce].Fields[(int)CustomLvdsForceBF.OutMarkForceMode].Value = 0;
            Registers[(int)WyvernReg.CustomLvdsForce].Apply(true);
        }

        public void PropagateMark(WyvernDataMarkEnum mark)
        {
            switch (mark)
            {
                case WyvernDataMarkEnum.Mark0:
                    Registers[(int)WyvernReg.Mark].Fields[(int)MarkBF.Mark0].Value = 1;
                    Registers[(int)WyvernReg.Mark].Fields[(int)MarkBF.Mark1].Value = 0;
                    Registers[(int)WyvernReg.Mark].Fields[(int)MarkBF.Mark2].Value = 0;
                    Registers[(int)WyvernReg.Mark].Fields[(int)MarkBF.Mark3].Value = 0;
                    break;
                case WyvernDataMarkEnum.Mark1:
                    Registers[(int)WyvernReg.Mark].Fields[(int)MarkBF.Mark0].Value = 0;
                    Registers[(int)WyvernReg.Mark].Fields[(int)MarkBF.Mark1].Value = 1;
                    Registers[(int)WyvernReg.Mark].Fields[(int)MarkBF.Mark2].Value = 0;
                    Registers[(int)WyvernReg.Mark].Fields[(int)MarkBF.Mark3].Value = 0;
                    break;
                case WyvernDataMarkEnum.Mark2:
                    Registers[(int)WyvernReg.Mark].Fields[(int)MarkBF.Mark0].Value = 0;
                    Registers[(int)WyvernReg.Mark].Fields[(int)MarkBF.Mark1].Value = 0;
                    Registers[(int)WyvernReg.Mark].Fields[(int)MarkBF.Mark2].Value = 1;
                    Registers[(int)WyvernReg.Mark].Fields[(int)MarkBF.Mark3].Value = 0;
                    break;
                case WyvernDataMarkEnum.Mark3:
                    Registers[(int)WyvernReg.Mark].Fields[(int)MarkBF.Mark0].Value = 0;
                    Registers[(int)WyvernReg.Mark].Fields[(int)MarkBF.Mark1].Value = 0;
                    Registers[(int)WyvernReg.Mark].Fields[(int)MarkBF.Mark2].Value = 0;
                    Registers[(int)WyvernReg.Mark].Fields[(int)MarkBF.Mark3].Value = 1;
                    break;
            }

            Registers[(int)WyvernReg.Mark].Apply(true);
        }

        /// <summary>OutMarkForceMode
        /// Calculates and sets the value of registers 
        /// _0-4 required to achieve the desired
        /// output frequency with the given clock rate.
        /// </summary>
        /// <param name="frequency">The desired GNco frequency in Hz.</param>
        /// <param name="clock">MClk frequency in Hz.</param>
        public void SetFrequencyRegValues(double frequency, double clock)
        {
            //BigInteger freqRep = new BigInteger( ( Frequency / mClock ) * (Math.Pow( 2, 80 ) - 1) );
            //byte[] freq = freqRep.ToByteArray();
            UInt64 freq = (UInt64)((frequency / clock) * UInt64.MaxValue);
            Registers[(int)WyvernReg.GNcoFreq_0].Fields[(int)GNcoFreqBF.FreqBits].Value = 0;
            Registers[(int)WyvernReg.GNcoFreq_1].Fields[(int)GNcoFreqBF.FreqBits].Value = (ushort)freq;
            Registers[(int)WyvernReg.GNcoFreq_2].Fields[(int)GNcoFreqBF.FreqBits].Value = (ushort)(freq >> 16);
            Registers[(int)WyvernReg.GNcoFreq_3].Fields[(int)GNcoFreqBF.FreqBits].Value = (ushort)(freq >> 32);
            Registers[(int)WyvernReg.GNcoFreq_4].Fields[(int)GNcoFreqBF.FreqBits].Value = (ushort)(freq >> 48);
            mIsFrequencyWritePending = true;
        }

        /// <summary>
        /// Calculates and sets the value of registers ScaleCfg and Scale to achieve the desired amplitude out of GNco.
        /// </summary>
        /// <param name="amplitude">The desired output amplitude in dBm.</param>
        public void SetScaleRegValues(double amplitude)
        {
            // Empirically derived based on a crude series of measurements on a single unit...
            double[] coeffs = { 22017.6240773199, 2061.67260807303, 66.7086074011270, 0.711357179306968 };

            const int exponent = 11;
            int scale = (int)PolyEval(coeffs, amplitude);

            // Limit the value to a valid range
            scale = Math.Min(0xffff, scale);
            scale = Math.Max(0x0, scale);

            //temperory omit by jiang tao
            Registers[(int)WyvernReg.ScaleCfg].Fields[(int)ScaleCfgBF.Exponent].Value = exponent;
            Registers[(int)WyvernReg.ScaleCfg].Fields[(int)ScaleCfgBF.Round].Value = 0;
            Registers[(int)WyvernReg.Scale].Fields[(int)ScaleBF.Scale].Value = scale;
            mIsScaleWritePending = true;
        }
        public void setScaleByHex(int scaleHex)
        {
            Registers[(int)WyvernReg.Scale].Value32 = scaleHex;
            Registers[(int)WyvernReg.Scale].Apply(true);
        }

        /// <summary>
        /// Applies the register values for frequency control to HW, ensuring that GNcoFreq_4 is written which
        /// is necessary for any changes to take effect.
        /// </summary>
        /// <param name="ForceApply"></param>
        public void ApplyFrequencyToHw(bool ForceApply)
        {
            Registers[(int)WyvernReg.GNcoFreq_0].Apply(ForceApply);
            Registers[(int)WyvernReg.GNcoFreq_1].Apply(ForceApply);
            Registers[(int)WyvernReg.GNcoFreq_2].Apply(ForceApply);
            Registers[(int)WyvernReg.GNcoFreq_3].Apply(ForceApply);
            Registers[(int)WyvernReg.GNcoFreq_4].Apply(true);
            //GncoFreq_4 needs to be written to apply new frequency
            mIsFrequencyWritePending = false;
        }

        /// <summary>
        /// Applies the register values for Scale control to HW, ensuring that Scale is written which
        /// is necessary for any changes to take effect.
        /// </summary>
        /// <param name="ForceApply"></param>
        public void ApplyScaleToHw(bool ForceApply)
        {
            Registers[(int)WyvernReg.ScaleCfg].Apply(ForceApply);
            Registers[(int)WyvernReg.ScaleCfg].Apply(ForceApply);
            Registers[(int)WyvernReg.Scale].Apply(true);
            //Needs to be written for the Apply to take effect
            mIsScaleWritePending = false;
        }

        public void ApplyRegValuesToHw(bool ForceApply)
        {
            if (mIsFrequencyWritePending)
            {
                ApplyFrequencyToHw(ForceApply);
            }
            if (mIsScaleWritePending)
            {
                ApplyScaleToHw(ForceApply);
            }
        }

        private void LogMessage(string message)
        {
        }

        //public void RefreshRegisters()
        //{
        //    Registers[0].Driver.RegRefresh(Registers);

        //    // Verify...
        //    for (int i = 0; i < Registers.Length; i++)
        //    {
        //        Thread.Sleep(10);
        //        var value = Registers[i].Value32;
        //        Console.WriteLine(string.Format("{0} - {1}", Registers[i].Name, value));
        //    }
        //}

        /// <summary>
        /// Reset the device to a known state.
        /// </summary>
        public void Reset()
        {
            RegisterSet.ResetRegisters();
        }

        /// <summary>
        /// Evaluate the polynomial described by 'coef' at 'x'
        /// </summary>
        /// <param name="coef">The array of coefficients from the fit</param>
        /// <param name="x">The X value to evaluate the coefficients at</param>
        /// <returns></returns>
        private static double PolyEval(double[] coef, double x)
        {
            double y = coef[0];
            double xn = 1;
            for (int j = 1; j < coef.Length; j++)
            {
                xn *= x;
                y += coef[j] * xn;
            }
            return y;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="phaseOffset">Phase offset, in radians</param>
        public void ApplyPhaseOffset(double phaseOffset)
        {
            // We have a full cycle of phase adjustment available
            int targetValue = (int)((0xffff / (2.0 * Math.PI)) * phaseOffset);

            // However, we do not want to apply large phase change differences at once.
            // Read back the current value, and slowly increment the register to get to the target value
            int currentValue = Registers[(int)WyvernReg.GNcoPhaseOffsetU].Read();
            while (Math.Abs(targetValue - currentValue) > 0x2000)
            {
                if (targetValue > currentValue)
                {
                    currentValue = currentValue + 0x2000;
                }
                else
                {
                    currentValue = currentValue - 0x2000;
                }
                Registers[(int)WyvernReg.GNcoPhaseOffsetU].Value32 = currentValue;
                Registers[(int)WyvernReg.GNcoPhaseOffsetU].Apply(true);
            }

            Registers[(int)WyvernReg.GNcoPhaseOffsetU].Value32 = targetValue;
            Registers[(int)WyvernReg.GNcoPhaseOffsetU].Apply(true);
        }
    }

    public enum InterpolationModeEmum
    {
        SDR,
        SDRDoublet,
        DDR,
        DDRDoublet,
    }

    public enum WyvernDataMarkEnum
    {
        Mark0,
        Mark1,
        Mark2,
        Mark3,
    }
}