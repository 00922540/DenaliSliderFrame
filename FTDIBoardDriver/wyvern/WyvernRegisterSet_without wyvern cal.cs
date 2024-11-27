using System;
using System.Collections.Generic;
using Keysight.ModularInstruments.Core.Register;

namespace Keysight.ModularInstruments.KtM9347.Registers
{
    #region Register Names

    // NOTE: normally we prefer this enumeration being inside the class and accessed
    // NOTE: similar to:   CommonPluginRegisterSet.RegisterNames, but due to
    // NOTE: the evolutionary history of TLO code we need this as a public enum

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum WyvernBF
    {
        Address,   //31...19
        Parity,    //18
        Increment, //17
        RW,        //16
        Value      //15...0
    }

    /// <summary>
    /// Enumerate the slug (non-carrier) registers unique to this module.
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true)]
    public enum WyvernReg
    {
        #region Digital

        #region General

        /// <summary>
        /// Can insert up to 4 DataMarks with shared time offsets in the datapath
        /// </summary>
        Mark,               // 0x0004 

        /// <summary>
        /// Bits in this register indicate various exception conditions
        /// </summary>
        Status0,            // 0x0007

        /// <summary>
        /// Bits in this register indicate that data has clipped at some place in the datapath
        /// </summary>
        Status1,            // 0x0008

        /// <summary>
        /// Turns off the clock for the selected modules
        /// </summary>
        CClkGate,           // 0x0010

        #endregion

        #region MarkPropagation

        /// <summary>
        /// Configures DataMark generator
        /// </summary>
        InMarkCfg,          // 0x0028

        /// <summary>
        /// Configures OutMark generator
        /// </summary>
        OutMarkCfg0,        // 0x0033

        /// <summary>
        /// Configures OutMark generator
        /// </summary>
        OutMarkCfg1,        // 0x0034

        /// <summary>
        /// Configures OutMark generator
        /// </summary>
        OutMarkCfg2,        // 0x0035

        /// <summary>
        /// Configures OutMark generator
        /// </summary>
        OutMarkCfg3,        // 0x0036

        #endregion

        #region GNco

        /// <summary>
        /// Configures GNCO
        /// </summary>
        GNcoCfg,            // 0x0058

        /// <summary>
        /// Contains LSBs of GNCO phase bump value.  Must also write GNcoDFU to take effect.
        /// </summary>
        GNcoDFL,            // 0x005e - Group 4

        /// <summary>
        /// Contains MSBs of GNCO phase bump value.
        /// </summary>
        GNcoDFU,            // 0x005f - Group 4 - Must write this register to take effect

        /// <summary>
        /// Contains LSBs of GNCO global phase offset value.  Must also write GNcoPhaseOffsetU to take effect.
        /// </summary>
        GNcoPhaseOffsetL,   // 0x0060 - Group 5

        /// <summary>
        /// Contains MSBs of GNCO global phase offset value
        /// </summary>
        GNcoPhaseOffsetU,   // 0x0061 - Group 5 - Must write this register to take effect

        /// <summary>
        /// Contains GNCO scale value
        /// </summary>
        GNcoScale,          // 0x0062

        /// <summary>
        /// Contains bits[15:0] of 80-bit GNCO frequency.  Must also write GNcoFreq_4 to take effect.
        /// </summary>
        GNcoFreq_0,         // 0x0096 - Group 7

        /// <summary>
        /// Contains bits[31:16] of 80-bit GNCO frequency.  Must also write GNcoFreq_4 to take effect.
        /// </summary>
        GNcoFreq_1,         // 0x0097 - Group 7

        /// <summary>
        /// Contains bits[47:32] of 80-bit GNCO frequency.  Must also write GNcoFreq_4 to take effect.
        /// </summary>
        GNcoFreq_2,         // 0x0098 - Group 7

        /// <summary>
        /// Contains bits[63:48] of 80-bit GNCO frequency.  Must also write GNcoFreq_4 to take effect.
        /// </summary>
        GNcoFreq_3,         // 0x0099 - Group 7

        /// <summary>
        /// Contains bits[79:64] of 80-bit GNCO frequency.
        /// </summary>
        GNcoFreq_4,         // 0x009a - Group 7 - Must write this register to take effect

        /// <summary>
        /// Controls which DataMark is used to control a GNCO related action
        /// </summary>
        GNcoMarkSel,        // 0x009c

        /// <summary>
        /// Controls how the selected DataMark controls a GNCO related action
        /// </summary>
        GNcoMarkCfg,        // 0x00a2

        #endregion

        #region SignalGen

        /// <summary>
        /// Configures scaling.Same group as Scale. Both need to be set together
        /// </summary>
        ScaleCfg,           // 0x03a4 - Group 136

        /// <summary>
        /// Contains scale value.  Same group as ScaleCfg. Both need to be set together
        /// </summary>
        Scale,              // 0x03a5 - Group 136 - Must write this register to take effect

        #endregion

        #region DdrInterp

        /// <summary>
        /// Configures DDR interpolator
        /// </summary>
        DdrInterpCfg,       // 0x0406

        #endregion

        #region Dem (Dynamic element matching)

        /// <summary>
        /// Configures dynamic element matching
        /// </summary>
        DemCfg,             // 0x041a

        #endregion

        #region FinalMuxIn

        /// <summary>
        /// Configures the input to the final mux
        /// </summary>
        FinalMuxInCfg,      // 0x042e

        /// <summary>
        /// Configures random DataMark generator which is the source for the DllClk generator
        /// </summary>
        DllClkCreateMark,   // 0x04ef 

        /// <summary>
        /// Configures DllClk generator
        /// </summary>
        DllClkCfg,          // 0x04f0 

        /// <summary>
        /// Controls length of pulse in pulse mode
        /// </summary>
        DllClkWidth,        // 0x04f3 

        #endregion

        #region FinalMux

        /// <summary>
        /// Final mux config
        /// </summary>
        FinalMuxCfg,        // 0x0800 

        /// <summary>
        /// Digital clock system config
        /// </summary>
        ClkdCfg,            // 0x0801

        /// <summary>
        /// mux clock timing DAC config
        /// </summary>
        MuxClkDelayCfg,     // 0x0802

        /// <summary>
        /// mux clock delay control, coarse lower bits
        /// </summary>
        MuxClkDelayCoarseL,

        /// <summary>
        /// mux clock delay control, coarse upper bits
        /// </summary>
        MuxClkDelayCoarseU,

        /// <summary>
        /// mux clock delay control, fineA bits
        /// </summary>
        MuxClkDelayFineA,

        /// <summary>
        /// mux clock delay control, fineB bits
        /// </summary>
        MuxClkDelayFineB,

        /// <summary>
        /// mux clock delay control, tweak bits
        /// </summary>
        MuxClkDelayTweak,

        /// <summary>
        /// Enable for custom LVDS IO
        /// </summary>
        CustomLvdsEnable,   // 0x080b

        /// <summary>
        /// Force custom LVDS outputs
        /// </summary>
        CustomLvdsForce,    // 0x080c

        /// <summary>
        /// Isolation-crossing DLL loop filter config
        /// </summary>
        IsoLoopFilterCfg1,  // 0x080e 

        /// <summary>
        /// Isolation-crossing DLL loop filter config
        /// </summary>
        IsoLoopFilterCfg2,  // 0x080f 

        /// <summary>
        /// Isolation-crossing DLL loop filter status outputs
        /// </summary>
        IsoLoopFilterStat1, // 0x0810

        /// <summary>
        /// Isolation-crossing DLL loop filter status outputs
        /// </summary>
        IsoLoopFilterStat2, // 0x0811

        /// <summary>
        /// Request loop filter status
        /// </summary>
        IsoLoopFilterStatReq, // 0x0812

        /// <summary>
        /// Acknowledge for loop filter status
        /// </summary>
        IsoLoopFilterStatAck, // 0x0813

        #endregion

        #endregion

        #region Analog

        #region DacCfg

        /// <summary>
        /// Dac configuration: miscellaneous
        /// </summary>
        DacCfg1,            // 0x1000

        /// <summary>
        /// Dac configuration: CML NMOS bias current
        /// </summary>
        DacCfg2,            // 0x1001

        /// <summary>
        /// Dac configuration: CML bias modes
        /// </summary>
        DacCfg3,            // 0x1002 

        /// <summary>
        /// Dac configuration: miscellaneous
        /// </summary>
        DacCfg4,            // 0x1003 

        /// <summary>
        /// Dac configuration: CML PMOS bias current
        /// </summary>
        DacCfg5,            // 0x1004 

        /// <summary>
        /// Dac configuration: CML swing adjust
        /// </summary>
        DacCfg6,            // 0x1005 

        #endregion

        #region DacAnaClkSys

        ClkaCfg,            // 0x10e9
        LClkDelayCfg,       // 0x10ea
        LClkDelayCoarseL,   // 0x10eb
        LClkDelayCoarseU,   // 0x10ec
        LClkDelayFineA,     // 0x10ed
        LClkDelayFineB,     // 0x10ee
        LClkDelayTweak,     // 0x10ef
        GlobalDelayCfg,     // 0x10f0
        GlobalDelayCoarseL, // 0x10f1
        GlobalDelayCoarseU, // 0x10f2
        GlobalDelayFineA,   // 0x10f3
        GlobalDelayFineB,   // 0x10f4
        GlobalDelayTweak,   // 0x10f5
        RsLoopFilterCfg1,   // 0x10f8 
        RsLoopFilterCfg2,   // 0x10f9 
        RsLoopFilterStat1,  // 0x10fa
        RsLoopFilterStat2,  // 0x10fb
        RsLoopFilterStatReq, // 0x10fc
        RsLoopFilterStatAck, // 0x10fd
        DllPhaseDetCfg10,   // 0x10fe 
        DllPhaseDetCfg11,   // 0x10ff 
        DllPhaseDetCfg20,   // 0x1100 
        DllPhaseDetCfg21,   // 0x1101 
        ClkaSpare,          // 0x1102

        #endregion

        #endregion
    }

    #endregion

    #region BitField enums used for M9347PluginRegisterSet

    #region General

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum MarkBF
    {
        /// <summary>
        /// Mark event offset in MClk cycles
        /// </summary>
        MarkTimeOffsetA,     // Bits  0- 5

        /// <summary>
        /// Inserts a DataMark[0] at offset MarkTimeOffsetA
        /// </summary>
        Mark0,               // Bit   6

        /// <summary>
        /// Inserts a DataMark[1] at offset MarkTimeOffsetA
        /// </summary>
        Mark1,               // Bit   7

        /// <summary>
        /// Mark event offset in MClk cycles
        /// </summary>
        MarkTimeOffsetB,     // Bits  8-13

        /// <summary>
        /// Inserts a DataMark[2] at offset MarkTimeOffsetB
        /// </summary>
        Mark2,               // Bit  14

        /// <summary>
        /// Inserts a DataMark[3] at offset MarkTimeOffsetB
        /// </summary>
        Mark3,               // Bit  15 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum Status0BF
    {
        /// <summary>
        /// Midscale is asserted
        /// </summary>
        Midscale,

        /// <summary>
        /// High overvoltage on positive side of the differential output
        /// </summary>
        OverVoltAoutpHigh,

        /// <summary>
        /// Low overvoltage on positive side of the differential output
        /// </summary>
        OverVoltAoutpLow,

        /// <summary>
        /// High overvoltage on negative side of the differential output
        /// </summary>
        OverVoltAoutnHigh,

        /// <summary>
        /// Low overvoltage on negative side of the differential output
        /// </summary>
        OverVoltAoutnLow,

        /// <summary>
        /// One of the control ports tried to access non-existing address
        /// </summary>
        NonExistingAddr,

        /// <summary>
        /// Multiple control ports tried to access the same address
        /// </summary>
        CtrlCollision,

        /// <summary>
        /// SPI ports access collision
        /// </summary>
        SpiCollision,

        /// <summary>
        /// Multiple InMark channels tried to send a mark with a different time offset
        /// </summary>
        InMarkCollision,

        /// <summary>
        /// Multiple DataMark channels tried to send a mark with a different time offset
        /// </summary>
        DataMarkCollision,

        /// <summary>
        /// Control port tried to access waveform memory out of bounds
        /// </summary>
        WaveMemOutOfBounds,

        /// <summary>
        /// Attempt was made to write 0 to SwpSpan register in GNCO. This is not allowed.
        /// </summary>
        SwpSpanZero,

        /// <summary>
        /// SPI parity bit not as expected
        /// </summary>
        SpiParityError
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum Status1BF
    {
        /// <summary>
        /// GNco frequency has clipped
        /// </summary>
        GNcoFreqClip,

        /// <summary>
        /// GNco sweep accumulator has clipped
        /// </summary>
        GNcoSwpClip,

        /// <summary>
        /// SNco frequency has clipped
        /// </summary>
        SNcoFreqClip,

        /// <summary>
        /// SNco scale value has clipped in Mag/Phase mode
        /// </summary>
        SNcoSpareClip,

        /// <summary>
        /// Interpolator node has clipped
        /// </summary>
        InterpolatorClip,

        /// <summary>
        /// Modulator output has clipped
        /// </summary>
        ModulatorClip,

        /// <summary>
        /// Scale output has clipped
        /// </summary>
        ScaleClip,

        /// <summary>
        /// Flattening filter output has clipped
        /// </summary>
        FlatFilterClip,

        /// <summary>
        /// Nonlinear predistortion output has clipped
        /// </summary>
        NLinPredistClip,

        /// <summary>
        /// SHF output has clipped
        /// </summary>
        ShfClip,

        /// <summary>
        /// Offset output has clipped
        /// </summary>
        OffsetClip,

        /// <summary>
        /// DDR inpolator path B has clipped
        /// </summary>
        DDRInterpClipB,

        /// <summary>
        /// Csec has clipped
        /// </summary>
        CsecClip,

        /// <summary>
        /// AWGN has clipped
        /// </summary>
        AwgnClip
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum CClkGateBF
    {
        /// <summary>
        /// Clock gate for waveform memory
        /// </summary>
        WaveMem,        // Bit 0

        /// <summary>
        /// Clock gate for GNCO
        /// </summary>
        GNco,           // Bit 1

        /// <summary>
        /// Clock gate for SNCO
        /// </summary>
        SNco,           // Bit 2

        /// <summary>
        /// Clock gate for Interpolation stages. Can only be used if Interpolator is not used at all
        /// </summary>
        AGx2,           // Bit 3

        /// <summary>
        /// Clock gate for Interpolation stage. Can be used if not in 3.2x or 6.4x interpolation mode
        /// </summary>
        Bx08,           // Bit 4

        /// <summary>
        /// Clock gate for Interpolation stages. Can be used if not in 6.4x, 8x, 16x, 32x or 64x interpolation mode
        /// </summary>
        CDEFx2,         // Bit 5

        /// <summary>
        /// Clock gate for modulator
        /// </summary>
        Modulation,     // Bit 6

        /// <summary>
        /// Clock gate for flattening filter
        /// </summary>
        FlatFilter,     // Bit 7

        /// <summary>
        /// Clock gate for non linear predistortion module
        /// </summary>
        NLPD,           // Bit 8

        /// <summary>
        /// Clock gate for subharmonic filter
        /// </summary>
        Shf,            // Bit 9

        /// <summary>
        /// Clock gate for DDR interpolator
        /// </summary>
        DdrInterp,      // Bit 10

        /// <summary>
        /// Clock gate for DAC-B part of Offset and DEM
        /// </summary>
        EncodingB,      // Bit 11

        /// <summary>
        /// Clock gate for DAC-AB part of digital current source error corrector
        /// </summary>
        Csec,           // Bit 12

        /// <summary>
        /// Clock gate for AWGN
        /// </summary>
        Awgn,           // Bit 13

        /// <summary>
        /// Clock gate for DdrDuc
        /// </summary>
        DdrDuc,         // Bit 14

        /// <summary>
        /// Clock gate for Power Detector
        /// </summary>
        PowerDetector,  // Bit 15
    }

    #endregion

    #region MarkPropagation

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum InMarkCfgBF
    {
        /// <summary>
        /// Determines when DataMark[0] propagates<para />
        /// 00: no DataMark[0] propagates<para />
        /// 01: DataMark[0] propagates whenever there is a new Mark.Mark0 or Mark01.Mark0 register write<para />
        /// 10: DataMark[0] propagates whenever there is a rising edge on LvdsInMarkA<para />
        /// 11: DataMark[0] propagates whenever there is a rising edge on LvdsInMarkA or whenever there is new Mark.Mark0 or Mark01.Mark0 write
        /// </summary>
        Cfg0,    //0..1

        /// <summary>
        /// Determines when DataMark[1] propagates<para />
        /// 00: no DataMark[1] propagates<para />
        /// 01: DataMark[1] propagates whenever there is a new Mark.Mark1 or Mark01.Mark1 register write<para />
        /// 10: DataMark[1] propagates whenever there is a rising edge on LvdsInMarkA<para />
        /// 11: DataMark[1] propagates whenever there is a rising edge on LvdsInMarkA or whenever there is new Mark.Mark1 or Mark01.Mark1 write
        /// </summary>
        Cfg1,    //2..3

        /// <summary>
        /// Determines when DataMark[2] propagates<para />
        /// 00: no DataMark[2] propagates<para />
        /// 01: DataMark[2] propagates whenever there is a new Mark.Mark2 or Mark23.Mark2 register write<para />
        /// 10: DataMark[2] propagates whenever there is a rising edge on LvdsInMarkA<para />
        /// 11: DataMark[2] propagates whenever there is a rising edge on LvdsInMarkA or whenever there is new Mark.Mark2 or Mark23.Mark2 write
        /// </summary>
        Cfg2,    //4..5

        /// <summary>
        /// Determines when DataMark[3] propagates<para />
        /// 00: no DataMark[3] propagates<para />
        /// 01: DataMark[3] propagates whenever there is a new Mark.Mark3 or Mark23.Mark3 register write<para />
        /// 10: DataMark[3] propagates whenever there is a rising edge on LvdsInMarkA<para />
        /// 11: DataMark[3] propagates whenever there is a rising edge on LvdsInMarkA or whenever there is new Mark.Mark3 or Mark23.Mark3 write
        /// </summary>
        Cfg3,    //6..7

        /// <summary>
        /// Determines when DataMark[0] propagates<para />
        /// 0: no JESD DataMark[0] propagates<para />
        /// 1: JESD DataMark[0] propagates
        /// </summary>
        JesdMarkEn0, //8

        /// <summary>
        /// Determines when DataMark[1] propagates<para />
        /// 0: no JESD DataMark[1] propagates<para />
        /// 1: JESD DataMark[1] propagates
        /// </summary>
        JesdMarkEn1, //9

        /// <summary>
        /// Determines when DataMark[2] propagates<para />
        /// 0: no JESD DataMark[2] propagates<para />
        /// 1: JESD DataMark[2] propagates
        /// </summary>
        JesdMarkEn2, //10
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum OutMarkCfgBF
    {
        /// <summary>
        /// Selects source of the OutMark generator<para />
        /// 0 : DataMark[0]<para />
        /// 1 : DataMark[1]<para />
        /// 2 : DataMark[2]<para />
        /// 3 : DataMark[3]<para />
        /// 4 : DataMark[4]<para />
        /// 5 : DataMark[5]<para />
        /// 6 : constant '0'<para />
        /// 7 : constant '1'
        /// </summary>
        DataMarkSelect,     // Bits 0-2

        /// <summary>
        /// Selects the mode of the OutMark generator<para />
        /// 0: pulse mode<para />
        /// 1: toggle mode
        /// </summary>
        Mode,               // Bit 3

        /// <summary>
        /// Defines initial (and default) state of OutMark output
        /// </summary>
        State,              // Bit 4

        /// <summary>
        /// Sets the polarity of the generated OutMark<para />
        /// 0: 'OutMarkPattern' register or 'State' field not inverted<para />
        /// 1: 'OutMarkPattern' register or 'State' field inverted
        /// </summary>
        InvertOutput,       // Bit 5

        /// <summary>
        /// 0 - 511: Contains the delay value in MClk periods for the programmable delayline in the OutMark generator
        /// </summary>
        Delay               // Bits 6-15
    }

    #endregion

    #region Gnco

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum GNcoCfgBF
    {
        /// <summary>
        /// Enable for angle modulation<para />
        /// 0: disable angle modulation<para />
        /// 1: enable angle modulation
        /// </summary>
        AngModEn,       // Bit 0

        /// <summary>
        /// Selects modulation type<para />
        // 0: FM<para />
        // 1: PM
        /// </summary>
        AngModTyp,      // Bit 1

        /// <summary>
        /// Contains scale value for angle modulation data
        /// </summary>
        AngModScl,      // Bits 2-7

        /// <summary>
        /// Selects phase rounding method<para />
        // 0 random random rounding<para />
        // 1 fixed  fixed rounding
        /// </summary>
        PhaseRound,     // Bit 8

        /// <summary>
        /// Selects amplitude rounding method<para />
        /// 0 random random rounding<para />
        /// 1 fixed  fixed rounding
        /// </summary>
        AmpRound,       // Bit 9

        /// <summary>
        /// Enable for parabolic phase interpolation<para />
        /// 0: linear phase interpolation<para />
        /// 1: parabolic phase interpolation
        /// </summary>
        PhaseInterp,    // Bit 10

        /// <summary>
        /// Bypass for the GNCO output scaling<para />
        /// 0: scaling active<para />
        /// 1: scaling bypassed
        /// </summary>
        ScaleBypass,    // Bit 11

        /// <summary>
        /// Enable for phase modulation by AWGN module output<para />
        /// 0: disable phase modulation<para />
        /// 1: enable phase modulation
        /// </summary>
        AwgnModEn       // Bit 12
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum GNcoDFLBF
    {
        /// <summary>
        /// Contains LSBs of GNCO phase bump value
        /// </summary>
        DFL // Bits 0-15
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum GNcoDFUBF
    {
        /// <summary>
        /// Contains MSBs  of GNCO phase bump value
        /// </summary>
        DFU // Bits 0-15
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum GNcoPhaseOffsetBF
    {
        /// <summary>
        /// Contains lower or upper bits of GNCO global phase offset value
        /// </summary>
        PhaseOffset         // Bits 0-15
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum GNcoScaleBF
    {
        /// <summary>
        /// Contains GNCO scale value
        /// </summary>
        Scale,
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum GNcoFreqBF
    {
        /// <summary>
        /// Contains 16 bits of 80-bit GNCO frequency.
        /// </summary>
        FreqBits,
    }

    /// <summary>
    /// Each bitfield is 2 bits wide.<para />
    /// 0 mark_0 DataMark[0]<para />
    /// 1 mark_1 DataMark[1]<para />
    /// 2 mark_2 DataMark[2]<para />
    /// 3 mark_3 DataMark[3]<para />
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum GNcoMarkSelBF
    {
        /// <summary>
        /// Controls which DataMark is used to control phase accumulator reset
        /// </summary>
        RstPA,

        /// <summary>
        /// Controls which DataMark is used to control frequency updates
        /// </summary>
        Freq,

        /// <summary>
        /// Controls which DataMark is used to control phase bump updates
        /// </summary>
        DF,

        /// <summary>
        /// Controls which DataMark is used to control phase offset updates
        /// </summary>
        PhaseOffset,

        /// <summary>
        /// Controls which DataMark is used to control angle modulation enable
        /// </summary>
        AngModEn,

        /// <summary>
        /// Controls which DataMark is used to control angle modulation data updates
        /// </summary>
        AngModDat,

        /// <summary>
        /// Controls which DataMark is used to control scale updates
        /// </summary>
        Scale,

        /// <summary>
        /// Controls which DataMark is used to control AWGN modulation enable
        /// </summary>
        AwgnModEn
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum GNcoMarkCfgBF
    {
        /// <summary>
        /// Controls if phase accumulator resets occur immediately or when selected DataMark arrives<para />
        /// 0: phase accumulator resets occur when selected DataMark arrives<para />
        /// 1: phase accumulator resets occur immediately
        /// </summary>
        ActOnNewRstPA,

        /// <summary>
        /// Controls if phase accumulator resets occur on all selected DataMarks or only on armed selected DataMarks<para />
        /// 0: phase accumulator resets occur only on armed selected DataMarks<para />
        /// 1: phase accumulator resets occur on all selected DataMarks
        /// </summary>
        ActOnAllSelectedMarksRstPA,

        /// <summary>
        /// Controls if frequency updates occur on GNcoFreq_4 write or when selected DataMark arrives<para />
        /// 0: updates occur when selected DataMark arrives<para />
        /// 1: updates occur on GNcoFreq_4 write
        /// </summary>
        ActOnNewFreq,

        /// <summary>
        /// Controls if frequency updates occur on all selected DataMarks or only on armed selected DataMarks<para />
        /// 0: updates occur only on armed selected DataMarks<para />
        /// 1: updates occur on all selected DataMarks
        /// </summary>
        ActOnAllSelectedMarksFreq,

        /// <summary>
        /// Controls if phase bump occurs on GNcoDFU write or when selected DataMark arrives<para />
        /// 0: phase bump occurs when selected DataMark arrives<para />
        /// 1: phase bump occurs on GNcoDFU write
        /// </summary>
        ActOnNewDF,

        /// <summary>
        /// Controls if phase bump occurs on all selected DataMarks or only on armed selected DataMarks<para />
        /// 0: phase bump occurs only on armed selected DataMarks<para />
        /// 1: phase bump occurs on all selected DataMarks
        /// </summary>
        ActOnAllSelectedMarksDF,

        /// <summary>
        /// Controls if phase offset updates occur on GNcoPhaseOffsetU write or when selected DataMark arrives<para />
        /// 0: updates occur when selected DataMark arrives<para />
        /// 1: updates occur on GNcoPhaseOffsetU write
        /// </summary>
        ActOnNewPhaseOffset,

        /// <summary>
        /// Controls if phase offset updates occur on all selected DataMarks or only on armed selected DataMarks<para />
        /// 0: updates occur only on armed selected DataMarks<para />
        /// 1: updates occur on all selected DataMarks
        /// </summary>
        ActOnAllSelectedMarksPhaseOffset,

        /// <summary>
        /// Controls if angle modulation enable updates occur immediately or when selected DataMark arrives<para />
        /// 0: updates occur when selected DataMark arrives<para />
        /// 1: updates occur immediately
        /// </summary>
        ActOnNewAngModEn,

        /// <summary>
        /// Controls if angle modulation enable updates occur on all selected DataMarks or only on armed selected DataMarks<para />
        /// 0: updates occur only on armed selected DataMarks<para />
        /// 1: updates occur on all selected DataMarks
        /// </summary>
        ActOnAllSelectedMarksAngModEn,

        /// <summary>
        /// Controls if angle modulation data updates occur immediately or when selected DataMark arrives<para />
        /// 0: updates occur when selected DataMark arrives<para />
        /// 1: updates occur immediately
        /// </summary>
        ActOnNewAngModDat,

        /// <summary>
        /// Controls if angle modulation data updates occur on all selected DataMarks or only on armed selected DataMarks<para />
        /// 0: updates occur only on armed selected DataMarks<para />
        /// 1: updates occur on all selected DataMarks
        /// </summary>
        ActOnAllSelectedMarksAngModDat,

        /// <summary>
        /// Controls if scale updates occur immediately or when selected DataMark arrives<para />
        /// 0: updates occur when selected DataMark arrives<para />
        /// 1: updates occur immediately
        /// </summary>
        ActOnNewScale,

        /// <summary>
        /// Controls if scale updates occur on all selected DataMarks or only on armed selected DataMarks<para />
        /// 0: updates occur only on armed selected DataMarks<para />
        /// 1: updates occur on all selected DataMarks
        /// </summary>
        ActOnAllSelectedMarksScale,

        /// <summary>
        /// Controls if scale updates occur immediately or when selected DataMark arrives<para />
        /// 0: updates occur when selected DataMark arrives<para />
        /// 1: updates occur immediately
        /// </summary>
        ActOnNewAwgnModEn,

        /// <summary>
        /// Controls if scale updates occur on all selected DataMarks or only on armed selected DataMarks<para />
        /// 0: updates occur only on armed selected DataMarks<para />
        /// 1: updates occur on all selected DataMarks
        /// </summary>
        ActOnAllSelectedMarksAwgnModEn
    }

    #endregion

    #region SignalGen

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ScaleCfgBF
    {
        /// <summary>
        /// Causes a scale by 2^(Exponent-10), 0x0b is a scale of 2
        /// </summary>
        Exponent,

        /// <summary>
        /// Selects rounding method<para />
        /// 0 random random rounding<para />
        /// 1 fixed  fixed rounding
        /// </summary>
        Round,
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ScaleBF
    {
        /// <summary>
        /// Contains scale value; 2's complement number;0x4000 is a scale of 0.5
        /// </summary>
        Scale,
    }

    #endregion

    #region DdrInterp

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DdrInterpCfgBF
    {
        /// <summary>
        /// Enables DDR interpolator<para />
        /// 0: DDR interpolator disabled<para />
        /// 1: DDR interpolator enabled
        /// </summary>
        Enable,         // Bit 0

        /// <summary>
        /// Enables fixed rounding in the DDR interpolator<para />
        /// 0 random random rounding<para />
        /// 1 fixed  fixed rounding
        /// </summary>
        Round,          // Bit 1

        /// <summary>
        /// Enables Doublet mode in the DDR interpolator<para />
        /// 0 : Normal mode<para />
        /// 1 : -DACB doublet mode<para />
        /// 2, 3: DACB = 0 RZ mode
        /// </summary>
        DoubletMode,    // Bit 2
    }

    #endregion

    #region Dem

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DemCfgBF
    {
        /// <summary>
        /// Enable for permuting<para />
        /// 0: Permuting disabled<para />
        /// 1: Permuting enabled
        /// </summary>
        Enable,         // Bit 0

        /// <summary>
        /// Determines if DEM is in DDR or non-DDR mode<para />
        /// 0: DEM operates on 32 phases (non-DDR)<para />
        /// 1: DEM operates on 64 phases (DDR)
        /// </summary>
        DoDdr,          // Bit 1

        /// <summary>
        /// Contains the number of restricted lower bits out of the PRNG
        /// </summary>
        DemLsbClip      // Bits 2-5
    }

    #endregion

    #region FinalMuxIn

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum FinalMuxInCfgBF
    {
        /// <summary>
        /// Selects input data for the final mux<para />
        /// 0: Constant mid-scale values<para />
        /// 1: FinalMuxIn* registers<para />
        /// 2: Normal mode of operation<para />
        /// 3: Normal mode of operation
        /// </summary>
        Source,           //0..1 0: constant mid-scale values 1: FinalMuxIn* registers 2-3: Normal mode

        /// <summary>
        /// Enables midscale forcing by over-voltage detector<para />
        /// 0: force disabled<para />
        /// 1: force enabled
        /// </summary>
        OverForceEnable,
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DllClkCreateMarkBF
    {
        /// <summary>
        /// Enables/disables the generation of random DataMarks<para />
        /// 0: Random DataMark generation disabled<para />
        /// 1: Random DataMark generation enabled
        /// </summary>
        Enable,              // Bit   0

        /// <summary>
        /// Sets the minimum number of CClk cycles between DataMarks
        /// </summary>
        cfgMin,              // Bits  1- 6

        /// <summary>
        /// Sets the maximum number of CClk cycles between DataMarks
        /// </summary>
        cfgMask,             // Bits  7-10 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DllClkCfgBF
    {
        /// <summary>
        /// Selects source of the DllClk generator<para />
        /// 0 : DataMark[0]<para />
        /// 1 : DataMark[1]<para />
        /// 2 : DataMark[2]<para />
        /// 3 : DataMark[3]<para />
        /// 4 : DataMark[4]<para />
        /// 5 : Random DataMark generator<para />
        /// 6 : constant '0'<para />
        /// 7 : constant '1'
        /// </summary>
        DataMarkSelect,       // Bits  0- 2

        /// <summary>
        /// Selects the mode of the DllClk generator<para />
        /// 0: pulse mode<para />
        /// 1: toggle mode
        /// </summary>
        Mode,                // Bit   3

        /// <summary>
        /// Defines initial state of DllClk output (= way to reset the state in toggle mode)
        /// </summary>
        State,               // Bit   4

        /// <summary>
        /// Sets the polarity of the generated DllClk<para />
        /// 0: 'DllClkPattern' register or 'State' field not inverted<para />
        /// 1: 'DllClkPattern' register or 'State' field inverted
        /// </summary>
        InvertOutput,        // Bit   5

        /// <summary>
        /// 0 - 511: Contains the delay value for the programmable delayline in the DllClk generator
        /// </summary>
        Delay,               // Bits  6-15 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DllClkWidthBF
    {
        /// <summary>
        /// Contains length of pulse in pulse mode (in MClk cycles). Exception: Value 0 corresponds to a pulse length of 2^16
        /// </summary>
        Width               // Bits 0-15
    }

    #endregion

    #region FinalMux

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum FinalMuxCfgBF
    {
        /// <summary>
        /// 0 = DAC-B mux lanes off<para />
        /// 1 = DAC-B mux lanes on
        /// </summary>
        DdrEn,               // Bit   0

        /// <summary>
        /// OutMarkEn[N] controls mux lane for OutMark[N]<para />
        /// 0 = OutMark[N] mux lane off<para />
        /// 1 = OutMark[N] mux lane on
        /// </summary>
        OutMarkEn,           // Bits  1- 4

        /// <summary>
        /// 0 = DllClk mux lane off<para />
        /// 1 = DllClk mux lane on
        /// </summary>
        DllClkEn,            // Bit   5

        /// <summary>
        /// 0 = Midscale pin latched<para />
        /// 1 = Midscale pin not latched
        /// </summary>
        MidScaleLatch,       // Bit   6

        /// <summary>
        /// 0 = Midscale pin enabled<para />
        /// 1 = Midscale pin disabled
        /// </summary>
        MidScaleOff,         // Bit   7

        /// <summary>
        /// 0 = normal operation<para />
        /// 1 = turn off all mux lanes (sleep)
        /// </summary>
        Powerdown,           // Bit   8

        /// <summary>
        /// 0 = TestClk mux lane off<para />
        /// 1 = TestClk mux lane on
        /// </summary>
        TestClkEn,           // Bit   9

        /// <summary>
        /// Select DTestBus sub-bus<para />
        /// 00 = 1.0V test bus<para />
        /// 01 = temperature diode test bus<para />
        /// 1X = 1.8V test bus
        /// </summary>
        DTestBusSel,         // Bits 10-11

        /// <summary>
        /// 1 = unselected sub-buses tied to GNDD<para />
        /// 0 = unselected sub-buses left floating
        /// </summary>
        DTestBusTiedDownOn,  // Bit  12
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkdCfgBF
    {
        /// <summary>
        /// FpgaClk divider counter control. Reset enables MClk/128 output.<para />
        /// 0 = counter off<para />
        /// N = divide by N+1
        /// </summary>
        FpgaClkCount,       // Bits 0-2

        /// <summary>
        /// FpgaClk divider mux control<para />
        /// 0 = bypass final divide-by-2 stage<para />
        /// 1 = enable final divide-by-2 stage
        /// </summary>
        FpgaClkMux,         // Bit 3

        /// <summary>
        /// mux clock gating for sleep/nap mode<para />
        /// 0 = normal opperation<para />
        /// 1 = mux clocks gated off
        /// </summary>
        MuxClkOff,          // Bit 4

        /// <summary>
        /// FpgaClk source<para />
        /// 0 = FpgaClk from FpgaClk divider<para />
        /// 1 = copy CClk to FpgaClk
        /// </summary>
        FpgaClkFromCClk,    // Bit 5

        /// <summary>
        /// enable feedback peaking in IClk receiver
        /// </summary>
        FbEnIClkRx,         // Bit 6

        /// <summary>
        /// enable feedback peaking in master divide-by-2
        /// </summary>
        FbEnDiv,            // Bit 7

        /// <summary>
        /// enable feedback peaking in muxClkDelay timing DAC
        /// </summary>
        FbEnMuxClkDelay,    // Bit 8

        /// <summary>
        /// CClk gating. Note: turning off CClk will lose XSync alignment.<para />
        /// 0 = CClk on<para />
        /// 1 = CClk off
        /// </summary>
        CClkOff,            // Bit 9

        /// <summary>
        /// Duty cycle correction for divide-by-2 clock used in final mux output stage.<para />
        /// 0 = duty cycle correction off<para />
        /// 1 = duty cycle correction on
        /// </summary>
        DutyEn,             // Bit 10

        /// <summary>
        /// Enable watchdog timer.<para />
        /// 0 = watchdog timer off<para />
        /// 1 = watchdog timer on
        /// </summary>
        WatchdogEn,         // Bit 11

        /// <summary>
        /// Force watchdog to timeout for testing.<para />
        /// 0 = normal operation<para />
        /// 1 = watchdog input clock gated off
        /// </summary>
        WatchdogTest,       // Bit 12

        /// <summary>
        /// enable feedback peaking protection by watchdog, 1=enabled
        /// </summary>
        WatchdogForceEn     // Bit 13
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkDelayCfgBF
    {
        /// <summary>
        /// detector gain for production test mode
        /// </summary>
        TestGain,

        /// <summary>
        /// enable for production test mode, 1=enabled
        /// </summary>
        TestMode
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkDelayCoarseLBF
    {
        /// <summary>
        /// lower bits of coarse thermometer code
        /// </summary>
        coarseL,
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkDelayCoarseUBF
    {
        coarseU,
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkDelayFineABF
    {
        /// <summary>
        /// fineA thermometer code
        /// </summary>
        fineA,
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkDelayFineBBF
    {
        /// <summary>
        /// fineB thermometer code
        /// </summary>
        fineB,
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkDelayTweakBF
    {
        /// <summary>
        /// tweak0 binary code
        /// </summary>
        tweak0,

        /// <summary>
        /// tweak1 binary code
        /// </summary>
        tweak1,
    }

    /// <summary>
    /// Enables custom LVDS IOs, disabling unused IOs reduces power consumption. Turning off LVDS outputs causes both sides to go to 1.2V. LVDS inputs have 100 Ohm diff termination that is independent of enable.
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum CustomLvdsEnableBF
    {
        OutMarkEn,
        FpgaClkEn,
        InSyncClkEn,
        InRefClkEn,
        OutRefClkEn,
        SyncDEn
    }

    /// <summary>
    /// Force custom LVDS outputs to a static value. Must be turned on in CustomLvdsEnable for forcing to work.
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum CustomLvdsForceBF
    {
        OutMarkForceMode,       // Bits 0-3
        OutMarkForceVal,        // Bits 4-7
        FpgaClkForceMode,       // Bit 8
        FpgaClkForceVal,        // Bit 9
        SyncDForceMode,         // Bit 10
        SyncDForceVal,          // Bit 11
        OutRefClkForceMode,     // Bit 12
        OutRefClkVal            // Bit 13
    }

    /// <summary>
    /// DLL loop filter configuration
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum LoopFilterCfg1BF
    {
        /// <summary>
        /// low pass filter pole at 1/(1-2^(-filtShift)). Valid range 4-12.
        /// </summary>
        filtShift,           // Bits  0- 5

        /// <summary>
        /// 1 = invert DllDetect to change the sign of feedback (CYA mode).
        /// </summary>
        invert,              // Bit   6

        /// <summary>
        /// FSM run control<para />
        /// 0 = FSM idle<para />
        /// 1 = FSM running
        /// </summary>
        run,                 // Bit   7

        /// <summary>
        /// 1 = load delay register from LoopFilterCfg2.loadVal
        /// </summary>
        load,                // Bit   8

        /// <summary>
        /// source of timing DAC control<para />
        /// 0 = from DelayCoarseU/DelayCoarseU/DelayFineA/DelayFineB config registers<para />
        /// 1 = from loop filter delay register
        /// </summary>
        sel,                 // Bit   9

        /// <summary>
        /// delay decrement/increment is triggered when LPF output reaches or exceeds the positive/negative threshold:<para />
        /// 0 = 0.031 / -0.062<para />
        /// 1 = 0.062 / -0.094<para />
        /// 2 = 0.125 / -0.156<para />
        /// 3 = 0.250 / -0.281<para />
        /// 4 = 0.500 / -0.531<para />
        /// 5 = 0.750 / -0.781<para />
        /// 6 = 0.875 / -0.906<para />
        /// 7 = 0.938 / -0.969<para />
        /// 8 = 0.969 / -1.000<para />
        /// 9 = never triggered<para />
        /// 10-15 = undefined
        /// </summary>
        thresh,              // Bits 10-13 		 
    }

    /// <summary>
    /// DLL loop filter configuration
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum LoopFilterCfg2BF
    {
        /// <summary>
        /// Value loaded into delay when LoopFilterCfg1.load = 1.
        /// </summary>
        loadVal,             // Bits  0-10 		 
    }

    /// <summary>
    /// DLL loop filter status outputs
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum LoopFilterStat1BF
    {
        /// <summary>
        /// Output from low pass filter. Signed fraction.
        /// </summary>
        filtOut,            // Bits 0-5

        /// <summary>
        /// 1 = filtOut is valid
        /// </summary>
        filtValid,          // Bit 6
    }

    /// <summary>
    /// DLL loop filter status outputs
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum LoopFilterStat2BF
    {
        /// <summary>
        /// Loop filter delay register
        /// </summary>
        delay,            // Bits 0-10
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum LoopFilterStatReqBF
    {
        /// <summary>
        /// Software requests status
        /// </summary>
        request             // Bit 0
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum LoopFilterStatAckBF
    {
        /// <summary>
        /// Loop filter aknowledges status is ready
        /// </summary>
        acknowledge             // Bit 0
    }

    #endregion

    #region DacCfg

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DacCfg1BF
    {
        /// <summary>
        /// enable DDR-mode CML receivers, 1=enabled
        /// </summary>
        DdrRxEn,            //0

        /// <summary>
        /// enable DllClk CML receiver, 1=enabled
        /// </summary>
        DllClkRxEn,         //1

        /// <summary>
        /// enable OutMark[1:0] CML receivers, 1=enabled
        /// </summary>
        OutMarkRxEn,        //2...3

        /// <summary>
        /// enable DDR-mode mux, 1=enabled
        /// </summary>
        DdrMuxEn,           //4

        /// <summary>
        /// enable lane deskew test outputs, 1=enabled
        /// </summary>
        TestEn,             //5

        /// <summary>
        /// force Aout to midscale, 1=forced
        /// </summary>
        Midscale,           //6

        /// <summary>
        /// enable OutMark[1:0] A-to-D transmitters, 1=enabled
        /// </summary>
        OutMarkTxEn,        //7..8

        /// <summary>
        /// Aoutp high over-volt detector enable, 1=enabled
        /// </summary>
        AoutpOverHighEn,    //9

        /// <summary>
        /// Aoutp low over-volt detector enable, 1=enabled
        /// </summary>
        AoutpOverLowEn,     //10

        /// <summary>
        /// Aoutn high over-volt detector enable, 1=enabled
        /// </summary>
        AoutnOverHighEn,    //11

        /// <summary>
        /// Aoutn low over-volt detector enable, 1=enabled
        /// </summary>
        AoutnOverLowEn,     //12

        /// <summary>
        /// enable over-volt detector midscale force, 1=enabled
        /// </summary>
        OverForceEnable,    //13

        /// <summary>
        /// enable OutMark[1:0] resamplers, 1=enabled
        /// </summary>
        MarkRsEn,           //14..15
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DacCfg2BF
    {
        /// <summary>
        /// final FF CML bias NMOS current, 0=12uA, 255=4mA
        /// </summary>
        CmlIref1,            // Bits  0- 7

        /// <summary>
        /// switch driver CML bias NMOS current, 0=12uA, 255=4mA
        /// </summary>
        CmlIref2,            // Bits  8-15 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DacCfg3BF
    {
        /// <summary>
        /// enable final FF CML bias ring oscillator, 1=enabled
        /// </summary>
        CmlOscEn1,           // Bit   0

        /// <summary>
        /// enable switch driver CML bias ring oscillator, 1=enabled
        /// </summary>
        CmlOscEn2,           // Bit   1

        /// <summary>
        /// select CML bias ring oscillator sent to Sync Toolkit<para />
        /// 0 = final FF<para />
        /// 1 = switch driver
        /// </summary>
        CmlOscSel,           // Bit   2

        /// <summary>
        /// final FF CML bias lowpass filter control<para />
        /// 0 = 200 kHz BW<para />
        /// 1 = 2 kHz BW<para />
        /// 2 = 1 kHz BW<para />
        /// 3 = 400 Hz BW
        /// </summary>
        CmlLpfEn1,           // Bits  3- 4

        /// <summary>
        /// switch driver CML bias lowpass filter control<para />
        /// 0 = 200 kHz BW<para />
        /// 1 = 2 kHz BW<para />
        /// 2 = 1 kHz BW<para />
        /// 3 = 400 Hz BW
        /// </summary>
        CmlLpfEn2,           // Bits  5- 6 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DacCfg4BF
    {
        /// <summary>
        /// Adjust the gate voltage of the DAC output cascode
        /// </summary>
        DacOutCasGateAdj,    // Bits  0- 5

        /// <summary>
        /// Adjust the second folding current in DAC core
        /// </summary>
        DacSecFoldCurAdj,    // Bits  6-11

        /// <summary>
        /// Enable feedback peaking in DAC reference slice clock driver
        /// </summary>
        DacRefSliceClkFbEn,  // Bit  12

        /// <summary>
        /// Select ATestLoBus sub-bus<para />
        /// 0 = 1.0V test bus<para />
        /// 1 = temperature diode test bus
        /// </summary>
        ATestLoBusSel,       // Bit  13

        /// <summary>
        /// 1 = unselected sub-buses tied to GNDA<para />
        /// 0 = unselected sub-buses left floating
        /// </summary>
        ATestLoBusTiedownOn, // Bit  14 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DacCfg5BF
    {
        /// <summary>
        /// final FF CML bias PMOS current, 0=12uA, 255=4mA
        /// </summary>
        CmlIrefp1,           // Bits  0- 7

        /// <summary>
        /// switch driver CML bias PMOS current, 0=12uA, 255=4mA
        /// </summary>
        CmlIrefp2,           // Bits  8-15 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DacCfg6BF
    {
        /// <summary>
        /// final FF CML swing, 0=434mV, 32=495mV, 63=554mV
        /// </summary>
        CmlSwing1,           // Bits  0- 5

        /// <summary>
        /// switch driver CML swing, 0=523mV, 38=593mV, 63=639mV
        /// </summary>
        CmlSwing2,           // Bits  6-11 		 
    }

    #endregion

    #region DacAnaClkSys

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkaCfgBF
    {
        /// <summary>
        /// static AClk value used when AClkMux1=1, AClkMux2=0
        /// </summary>
        AClkStatic,         // Bit 0

        /// <summary>
        /// AClkMux1 select<para />
        /// 0 = low noise AClk path<para />
        /// 1 = AClk from AClkMux2
        /// </summary>
        AClkMux1,           // Bit 1

        /// <summary>
        /// AClkMux2 select<para />
        /// 0 = static AClk(i.e.no resampling) from AClkStatic<para />
        /// 1 = AClk from GlobalDelay
        /// </summary>
        AClkMux2,           // Bit 2

        /// <summary>
        /// LClkMux select<para />
        /// 0 = normal LClk path<para />
        /// 1 = LClk from TestClk
        /// </summary>
        LClkMux,            // Bit 3

        /// <summary>
        /// TestClk receiver enable
        /// </summary>
        TestClkRxEn,        // Bit 4

        /// <summary>
        /// enable feedback peaking in AClk buffers, 1=enabled
        /// </summary>
        FbEnAClkDrive,      // Bit 5

        /// <summary>
        /// enable feedback peaking in AClkMux2, 1=enabled
        /// </summary>
        FbEnAClkMux2,       // Bit 6

        /// <summary>
        /// enable feedback peaking in GlobalDelay timing DAC, 1=enabled
        /// </summary>
        FbEnGlobalDelay,    // Bit 7

        /// <summary>
        /// enable feedback peaking in IClk buffers, 1=enabled
        /// </summary>
        FbEnIClkDrive,      // Bit 8

        /// <summary>
        /// enable feedback peaking in LClkDelay timing DAC, 1=enabled
        /// </summary>
        FbEnLClkDelay,      // Bit 9

        /// <summary>
        /// enable feedback peaking in LClk buffers, 1=enabled
        /// </summary>
        FbEnLClkDrive,      // Bit 10

        /// <summary>
        /// enable feedback peaking in CML-to-CMOS level shift, 1=enabled
        /// </summary>
        FbEnLs,             // Bit 11

        /// <summary>
        /// enable feedback peaking in TestClkDelay1 timing DAC, 1=enabled
        /// </summary>
        FbEnTestClkDelay1,  // Bit 12

        /// <summary>
        /// enable feedback peaking in TestClkDelay2 timing DAC, 1=enabled
        /// </summary>
        FbEnTestClkDelay2,  // Bit 13

        /// <summary>
        /// enable feedback peaking in IClk2 buffers, 1=enabled
        /// </summary>
        FbEnIClk2Drive,     // Bit 14

        /// <summary>
        /// enable IClk2 output to IntSerRefClk divider, 0=enabled
        /// </summary>
        IClk2Off,           // Bit 15
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DllPhaseDetCfg1BF
    {
        /// <summary>
        /// delay adjust for AClk/LClk input, unsigned
        /// </summary>
        DelayClk,            // Bits  0- 2

        /// <summary>
        /// delay adjust for DllClk input, unsigned, valid range 0-12
        /// </summary>
        DelayDllClk,         // Bits  3- 6

        /// <summary>
        /// Nreset for dither counter
        /// </summary>
        DithNreset,          // Bit   7

        /// <summary>
        /// dither scale control, unsigned
        /// </summary>
        DithScale,           // Bits  8-10

        /// <summary>
        /// enable feedback peaking in AClk/LClk delay line
        /// </summary>
        fbEn,                // Bit  11 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DllPhaseDetCfg2BF
    {
        BiasAdj,             // Bits  0- 7 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkaSpareBF
    {
        /// <summary>
        /// enable feedback peaking in final FF slices, 1=enabled
        /// </summary>
        FbEnFffSlice,       // Bit 0

        /// <summary>
        /// enable feedback peaking protection by watchdog, 1=enabled
        /// </summary>
        WatchdogForceEn,       // Bit 1

        /// <summary>
        /// enable feedback peaking in AClk buffer for OutMark resampler and resampling DLL phase detector, 1=enabled
        /// </summary>
        FbEnAClkBufMark,       // Bit 2
    }

    #endregion

    #endregion BitField enums used for M9347PluginRegisterSet

    /// <summary>
    /// 
    /// </summary>
    public class WyvernRegisterSet 
    {
        //public BoardDriver.VXT3BoardDriver driver;
        public IRegister[] Registers
        {
            get;
            protected set;
        }
        #region constants/enums/lookup tables

        private readonly RegDef[] mRegisterDefinitions =
            {
               // General
               new RegDef( (int)WyvernReg.Mark,                    0x0004, typeof( MarkBF ) ),
               new RegDef( (int)WyvernReg.Status0,                 0x0007, typeof( Status0BF )),
               new RegDef( (int)WyvernReg.Status1,                 0x0008, typeof( Status1BF )),
               new RegDef( (int)WyvernReg.CClkGate,                0x0010, typeof( CClkGateBF )),

               // MarkPropagation
               new RegDef( (int)WyvernReg.InMarkCfg,               0x0028, typeof( InMarkCfgBF )),
               new RegDef( (int)WyvernReg.OutMarkCfg0,             0x0033, typeof( OutMarkCfgBF )),
               new RegDef( (int)WyvernReg.OutMarkCfg1,             0x0033, typeof( OutMarkCfgBF )),
               new RegDef( (int)WyvernReg.OutMarkCfg2,             0x0033, typeof( OutMarkCfgBF )),
               new RegDef( (int)WyvernReg.OutMarkCfg3,             0x0033, typeof( OutMarkCfgBF )),

               // Gnco
               new RegDef( (int)WyvernReg.GNcoCfg,                 0x005B, typeof( GNcoCfgBF )),
               new RegDef( (int)WyvernReg.GNcoDFL,                 0x005e, typeof( GNcoDFLBF )),
               new RegDef( (int)WyvernReg.GNcoDFU,                 0x005f, typeof( GNcoDFUBF )),
               new RegDef( (int)WyvernReg.GNcoPhaseOffsetL,        0x0060, typeof( GNcoPhaseOffsetBF )),
               new RegDef( (int)WyvernReg.GNcoPhaseOffsetU,        0x0061, typeof( GNcoPhaseOffsetBF )),
               new RegDef( (int)WyvernReg.GNcoScale,               0x0062, typeof( GNcoScaleBF )),
               new RegDef( (int)WyvernReg.GNcoFreq_0,              0x0096, typeof( GNcoFreqBF )),
               new RegDef( (int)WyvernReg.GNcoFreq_1,              0x0097, typeof( GNcoFreqBF )),
               new RegDef( (int)WyvernReg.GNcoFreq_2,              0x0098, typeof( GNcoFreqBF )),
               new RegDef( (int)WyvernReg.GNcoFreq_3,              0x0099, typeof( GNcoFreqBF )),
               new RegDef( (int)WyvernReg.GNcoFreq_4,              0x009A, typeof( GNcoFreqBF )),
               new RegDef( (int)WyvernReg.GNcoMarkSel,             0x009c, typeof( GNcoMarkSelBF )),
               new RegDef( (int)WyvernReg.GNcoMarkCfg,             0x00A2, typeof( GNcoMarkCfgBF )),

               // SignalGen
               new RegDef( (int)WyvernReg.ScaleCfg,                0x03A4, typeof( ScaleCfgBF )),
               new RegDef( (int)WyvernReg.Scale,                   0x03A5, typeof( ScaleBF )),

               // DdrInterp
               new RegDef( (int)WyvernReg.DdrInterpCfg,            0x0406, typeof( DdrInterpCfgBF )),

               // Dem
               new RegDef( (int)WyvernReg.DemCfg,                  0x041a, typeof( DemCfgBF )),

               // FinalMuxIn
               new RegDef( (int)WyvernReg.FinalMuxInCfg,           0x042E, typeof( FinalMuxInCfgBF )),
               new RegDef( (int)WyvernReg.DllClkCreateMark,        0x04ef, typeof( DllClkCreateMarkBF )),
			   new RegDef( (int)WyvernReg.DllClkCfg,               0x04f0, typeof( DllClkCfgBF )),
               new RegDef( (int)WyvernReg.DllClkWidth,             0x04f3, typeof( DllClkWidthBF )),

               // FinalMux
			   new RegDef( (int)WyvernReg.FinalMuxCfg,             0x0800, typeof( FinalMuxCfgBF )),
               new RegDef( (int)WyvernReg.ClkdCfg,                 0x0801, typeof( ClkdCfgBF )),
               new RegDef( (int)WyvernReg.MuxClkDelayCfg,          0x0802, typeof( ClkDelayCfgBF )),
               new RegDef( (int)WyvernReg.MuxClkDelayCoarseL,      0x0803, typeof( ClkDelayCoarseLBF )),
               new RegDef( (int)WyvernReg.MuxClkDelayCoarseU,      0x0804, typeof( ClkDelayCoarseUBF )),
               new RegDef( (int)WyvernReg.MuxClkDelayFineA,        0x0805, typeof( ClkDelayFineABF )),
               new RegDef( (int)WyvernReg.MuxClkDelayFineB,        0x0806, typeof( ClkDelayFineBBF )),
               new RegDef( (int)WyvernReg.MuxClkDelayTweak,        0x0807, typeof( ClkDelayTweakBF )),
               new RegDef( (int)WyvernReg.CustomLvdsEnable,        0x080b, typeof( CustomLvdsEnableBF )),
               new RegDef( (int)WyvernReg.CustomLvdsForce,         0x080c, typeof( CustomLvdsForceBF )),
               new RegDef( (int)WyvernReg.IsoLoopFilterCfg1,       0x080e, typeof( LoopFilterCfg1BF )),
               new RegDef( (int)WyvernReg.IsoLoopFilterCfg2,       0x080f, typeof( LoopFilterCfg2BF )),
               new RegDef( (int)WyvernReg.IsoLoopFilterStat1,      0x0810, typeof( LoopFilterStat1BF )),
               new RegDef( (int)WyvernReg.IsoLoopFilterStat2,      0x0811, typeof( LoopFilterStat2BF )),
               new RegDef( (int)WyvernReg.IsoLoopFilterStatReq,    0x0812, typeof( LoopFilterStatReqBF )),
               new RegDef( (int)WyvernReg.IsoLoopFilterStatAck,    0x0813, typeof( LoopFilterStatAckBF )),

               // DacCfg
               new RegDef( (int)WyvernReg.DacCfg1,                 0x1000, typeof( DacCfg1BF )),
               new RegDef( (int)WyvernReg.DacCfg2,                 0x1001, typeof( DacCfg2BF )),
               new RegDef( (int)WyvernReg.DacCfg3,                 0x1002, typeof( DacCfg3BF )),
               new RegDef( (int)WyvernReg.DacCfg4,                 0x1003, typeof( DacCfg4BF )),
               new RegDef( (int)WyvernReg.DacCfg5,                 0x1004, typeof( DacCfg5BF )),
               new RegDef( (int)WyvernReg.DacCfg6,                 0x1005, typeof( DacCfg6BF )),

               // DacAnaClkSys
               new RegDef( (int)WyvernReg.ClkaCfg,                 0x10e9, typeof( ClkaCfgBF )),
               new RegDef( (int)WyvernReg.LClkDelayCfg,            0x10ea, typeof( ClkDelayCfgBF )),
               new RegDef( (int)WyvernReg.LClkDelayCoarseL,        0x10eb, typeof( ClkDelayCoarseLBF )),
               new RegDef( (int)WyvernReg.LClkDelayCoarseU,        0x10ec, typeof( ClkDelayCoarseUBF )),
               new RegDef( (int)WyvernReg.LClkDelayFineA,          0x10ed, typeof( ClkDelayFineABF )),
               new RegDef( (int)WyvernReg.LClkDelayFineB,          0x10ee, typeof( ClkDelayFineBBF )),
               new RegDef( (int)WyvernReg.LClkDelayTweak,          0x10ef, typeof( ClkDelayTweakBF )),
               new RegDef( (int)WyvernReg.GlobalDelayCfg,          0x10f0, typeof( ClkDelayCfgBF )),
               new RegDef( (int)WyvernReg.GlobalDelayCoarseL,      0x10f1, typeof( ClkDelayCoarseLBF )),
               new RegDef( (int)WyvernReg.GlobalDelayCoarseU,      0x10f2, typeof( ClkDelayCoarseUBF )),
               new RegDef( (int)WyvernReg.GlobalDelayFineA,        0x10f3, typeof( ClkDelayFineABF )),
               new RegDef( (int)WyvernReg.GlobalDelayFineB,        0x10f4, typeof( ClkDelayFineBBF ) ),
               new RegDef( (int)WyvernReg.GlobalDelayTweak,        0x10f5, typeof( ClkDelayTweakBF )),
               new RegDef( (int)WyvernReg.RsLoopFilterCfg1,        0x10f8, typeof( LoopFilterCfg1BF )),
               new RegDef( (int)WyvernReg.RsLoopFilterCfg2,        0x10f9, typeof( LoopFilterCfg2BF )),
               new RegDef( (int)WyvernReg.RsLoopFilterStat1,       0x10fa, typeof( LoopFilterStat1BF )),
               new RegDef( (int)WyvernReg.RsLoopFilterStat2,       0x10fb, typeof( LoopFilterStat2BF )),
               new RegDef( (int)WyvernReg.RsLoopFilterStatReq,     0x10fc, typeof( LoopFilterStatReqBF )),
               new RegDef( (int)WyvernReg.RsLoopFilterStatAck,     0x10fd, typeof( LoopFilterStatAckBF )),
               new RegDef( (int)WyvernReg.DllPhaseDetCfg10,        0x10fe, typeof( DllPhaseDetCfg1BF )),
			   new RegDef( (int)WyvernReg.DllPhaseDetCfg11,        0x10ff, typeof( DllPhaseDetCfg1BF )),
			   new RegDef( (int)WyvernReg.DllPhaseDetCfg20,        0x1100, typeof( DllPhaseDetCfg2BF ) ),
			   new RegDef( (int)WyvernReg.DllPhaseDetCfg21,        0x1101, typeof( DllPhaseDetCfg2BF )),
               new RegDef( (int)WyvernReg.ClkaSpare,               0x1102, typeof( ClkaSpareBF )),
            };

        private readonly BitFieldDef[] mBitFieldDefinitions =
            {

                #region General
                //
                new BitFieldDef( (int)WyvernReg.Mark, (int)MarkBF.MarkTimeOffsetA, 0,  5 ),
                new BitFieldDef( (int)WyvernReg.Mark, (int)MarkBF.Mark0,               6 ),
                new BitFieldDef( (int)WyvernReg.Mark, (int)MarkBF.Mark1,               7 ),
                new BitFieldDef( (int)WyvernReg.Mark, (int)MarkBF.MarkTimeOffsetB, 8, 13 ),
                new BitFieldDef( (int)WyvernReg.Mark, (int)MarkBF.Mark2,              14 ),
                new BitFieldDef( (int)WyvernReg.Mark, (int)MarkBF.Mark3,              15 ),
                //
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.Midscale,             0),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.OverVoltAoutpHigh,    1),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.OverVoltAoutpLow,     2),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.OverVoltAoutnHigh,    3),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.OverVoltAoutnLow,     4),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.NonExistingAddr,      5),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.CtrlCollision,        6),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.SpiCollision,         7),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.InMarkCollision,      8),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.DataMarkCollision,    9),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.WaveMemOutOfBounds,   10),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.SwpSpanZero,          11),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.SpiParityError,       12),
                //                              
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.GNcoFreqClip,       0),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.GNcoSwpClip,        1),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.SNcoFreqClip,       2),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.SNcoSpareClip,      3),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.InterpolatorClip,   4),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.ModulatorClip,      5),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.ScaleClip,          6),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.FlatFilterClip,     7),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.NLinPredistClip,    8),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.ShfClip,            9),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.OffsetClip,         10),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.DDRInterpClipB,     11),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.CsecClip,           12),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.AwgnClip,           13),
                //
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.WaveMem,        0),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.GNco,           1),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.SNco,           2),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.AGx2,           3),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.Bx08,           4),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.CDEFx2,         5),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.Modulation,     6),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.FlatFilter,     7),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.NLPD,           8),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.Shf,            9),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.DdrInterp,      10),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.EncodingB,      11),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.Csec,           12),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.Awgn,           13),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.DdrDuc,         14),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.PowerDetector,  15),
                //
                #endregion

                #region MarkPropagation
                //
                new BitFieldDef( (int)WyvernReg.InMarkCfg, (int)InMarkCfgBF.Cfg0, 0, 1),
                new BitFieldDef( (int)WyvernReg.InMarkCfg, (int)InMarkCfgBF.Cfg1, 2, 3),
                new BitFieldDef( (int)WyvernReg.InMarkCfg, (int)InMarkCfgBF.Cfg2, 4, 5),
                new BitFieldDef( (int)WyvernReg.InMarkCfg, (int)InMarkCfgBF.Cfg3, 6, 7),
                new BitFieldDef( (int)WyvernReg.InMarkCfg, (int)InMarkCfgBF.JesdMarkEn0, 8),
                new BitFieldDef( (int)WyvernReg.InMarkCfg, (int)InMarkCfgBF.JesdMarkEn1, 9),
                new BitFieldDef( (int)WyvernReg.InMarkCfg, (int)InMarkCfgBF.JesdMarkEn2, 10),
                //
                new BitFieldDef( typeof(OutMarkCfgBF), (int)OutMarkCfgBF.DataMarkSelect, 0, 2),
                new BitFieldDef( typeof(OutMarkCfgBF), (int)OutMarkCfgBF.Mode, 3),
                new BitFieldDef( typeof(OutMarkCfgBF), (int)OutMarkCfgBF.State, 4),
                new BitFieldDef( typeof(OutMarkCfgBF), (int)OutMarkCfgBF.InvertOutput, 5),
                new BitFieldDef( typeof(OutMarkCfgBF), (int)OutMarkCfgBF.Delay, 6, 15),
                //
                #endregion

                #region Gnco
                //
                new BitFieldDef( (int)WyvernReg.GNcoCfg, (int)GNcoCfgBF.AngModEn, 0 ),
                new BitFieldDef( (int)WyvernReg.GNcoCfg, (int)GNcoCfgBF.AngModTyp, 1 ),
                new BitFieldDef( (int)WyvernReg.GNcoCfg, (int)GNcoCfgBF.AngModScl, 2, 7 ),
                new BitFieldDef( (int)WyvernReg.GNcoCfg, (int)GNcoCfgBF.PhaseRound, 8 ),
                new BitFieldDef( (int)WyvernReg.GNcoCfg, (int)GNcoCfgBF.AmpRound, 9 ),
                new BitFieldDef( (int)WyvernReg.GNcoCfg, (int)GNcoCfgBF.PhaseInterp, 10 ),
                new BitFieldDef( (int)WyvernReg.GNcoCfg, (int)GNcoCfgBF.ScaleBypass, 11 ),
                new BitFieldDef( (int)WyvernReg.GNcoCfg, (int)GNcoCfgBF.AwgnModEn, 12 ),
                //
                new BitFieldDef( (int)WyvernReg.GNcoDFL, (int)GNcoDFLBF.DFL, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.GNcoDFU, (int)GNcoDFUBF.DFU, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.GNcoPhaseOffsetL, (int)GNcoPhaseOffsetBF.PhaseOffset, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.GNcoPhaseOffsetU, (int)GNcoPhaseOffsetBF.PhaseOffset, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.GNcoScale, (int)GNcoScaleBF.Scale, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.GNcoFreq_0, (int)GNcoFreqBF.FreqBits, 0, 15 ),
                new BitFieldDef( (int)WyvernReg.GNcoFreq_1, (int)GNcoFreqBF.FreqBits, 0, 15 ),
                new BitFieldDef( (int)WyvernReg.GNcoFreq_2, (int)GNcoFreqBF.FreqBits, 0, 15 ),
                new BitFieldDef( (int)WyvernReg.GNcoFreq_3, (int)GNcoFreqBF.FreqBits, 0, 15 ),
                new BitFieldDef( (int)WyvernReg.GNcoFreq_4, (int)GNcoFreqBF.FreqBits, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.GNcoMarkSel, (int)GNcoMarkSelBF.RstPA, 0, 1 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkSel, (int)GNcoMarkSelBF.Freq, 2, 3 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkSel, (int)GNcoMarkSelBF.DF, 4, 5 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkSel, (int)GNcoMarkSelBF.PhaseOffset, 6, 7 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkSel, (int)GNcoMarkSelBF.AngModEn, 8, 9 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkSel, (int)GNcoMarkSelBF.AngModDat, 10, 11 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkSel, (int)GNcoMarkSelBF.Scale, 12, 13 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkSel, (int)GNcoMarkSelBF.AwgnModEn, 14, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnNewRstPA,                    0 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnAllSelectedMarksRstPA,       1 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnNewFreq,                     2 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnAllSelectedMarksFreq,        3 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnNewDF,                       4 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnAllSelectedMarksDF,          5 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnNewPhaseOffset,              6 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnAllSelectedMarksPhaseOffset, 7 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnNewAngModEn,                 8 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnAllSelectedMarksAngModEn,    9 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnNewAngModDat,                10 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnAllSelectedMarksAngModDat,   11 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnNewScale,                    12 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnAllSelectedMarksScale,       13 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnNewAwgnModEn,                14 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnAllSelectedMarksAwgnModEn,   15 ),
                //
                #endregion

                #region SignalGen
                //
                new BitFieldDef( (int)WyvernReg.ScaleCfg, (int)ScaleCfgBF.Exponent, 0, 4 ),
                new BitFieldDef( (int)WyvernReg.ScaleCfg, (int)ScaleCfgBF.Round, 5 ),
                //
                new BitFieldDef( (int)WyvernReg.Scale, (int)ScaleBF.Scale, 0, 15 ),
                //
                #endregion

                #region DdrInterp

                new BitFieldDef( (int)WyvernReg.DdrInterpCfg, (int)DdrInterpCfgBF.Enable, 0 ),
                new BitFieldDef( (int)WyvernReg.DdrInterpCfg, (int)DdrInterpCfgBF.Round, 1 ),
                new BitFieldDef( (int)WyvernReg.DdrInterpCfg, (int)DdrInterpCfgBF.DoubletMode, 2, 3 ),

                #endregion

                #region Dem

                new BitFieldDef( (int)WyvernReg.DemCfg, (int)DemCfgBF.Enable, 0 ),
                new BitFieldDef( (int)WyvernReg.DemCfg, (int)DemCfgBF.DoDdr, 1 ),
                new BitFieldDef( (int)WyvernReg.DemCfg, (int)DemCfgBF.DemLsbClip, 2, 5 ),

                #endregion

                #region FinalMuxIn
                
                new BitFieldDef( (int)WyvernReg.FinalMuxInCfg, (int)FinalMuxInCfgBF.Source, 0, 1),
                new BitFieldDef( (int)WyvernReg.FinalMuxInCfg, (int)FinalMuxInCfgBF.OverForceEnable, 2),
                //
                new BitFieldDef( (int)WyvernReg.DllClkCreateMark, (int)DllClkCreateMarkBF.Enable, 0 ),
                new BitFieldDef( (int)WyvernReg.DllClkCreateMark, (int)DllClkCreateMarkBF.cfgMin, 1, 6 ),
                new BitFieldDef( (int)WyvernReg.DllClkCreateMark, (int)DllClkCreateMarkBF.cfgMask, 7, 10 ),
                //
				new BitFieldDef( (int)WyvernReg.DllClkCfg, (int)DllClkCfgBF.DataMarkSelect, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.DllClkCfg, (int)DllClkCfgBF.Mode, 3 ),
                new BitFieldDef( (int)WyvernReg.DllClkCfg, (int)DllClkCfgBF.State, 4 ),
                new BitFieldDef( (int)WyvernReg.DllClkCfg, (int)DllClkCfgBF.InvertOutput, 5 ),
                new BitFieldDef( (int)WyvernReg.DllClkCfg, (int)DllClkCfgBF.Delay, 6, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.DllClkWidth, (int)DllClkWidthBF.Width, 0, 15 ),
                
                #endregion

                #region FinalMux

                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.DdrEn, 0 ),
                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.OutMarkEn, 1, 4 ),
                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.DllClkEn, 5 ),
                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.MidScaleLatch, 6 ),
                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.MidScaleOff, 7 ),
                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.Powerdown, 8 ),
                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.TestClkEn, 9 ),
                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.DTestBusSel, 10, 11 ),
                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.DTestBusTiedDownOn, 12 ),
                //
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.FpgaClkCount, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.FpgaClkMux, 3 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.MuxClkOff, 4 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.FpgaClkFromCClk, 5 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.FbEnIClkRx, 6 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.FbEnDiv, 7 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.FbEnMuxClkDelay, 8 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.CClkOff, 9 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.DutyEn, 10 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.WatchdogEn, 11 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.WatchdogTest, 12 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.WatchdogForceEn, 13 ),
                //
                new BitFieldDef( (int)WyvernReg.MuxClkDelayCfg, (int)ClkDelayCfgBF.TestGain, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.MuxClkDelayCfg, (int)ClkDelayCfgBF.TestMode, 3 ),
                //
                new BitFieldDef( (int)WyvernReg.MuxClkDelayCoarseL, (int)ClkDelayCoarseLBF.coarseL, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.MuxClkDelayCoarseU, (int)ClkDelayCoarseUBF.coarseU, 0, 14 ),
                //
                new BitFieldDef( (int)WyvernReg.MuxClkDelayFineA, (int)ClkDelayFineABF.fineA, 0, 2 ),
                //
                new BitFieldDef( (int)WyvernReg.MuxClkDelayFineB, (int)ClkDelayFineBBF.fineB, 0, 14 ),
                //
                new BitFieldDef( (int)WyvernReg.MuxClkDelayTweak, (int)ClkDelayTweakBF.tweak0, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.MuxClkDelayTweak, (int)ClkDelayTweakBF.tweak1, 3, 6 ),
                //
                new BitFieldDef( (int)WyvernReg.CustomLvdsEnable, (int)CustomLvdsEnableBF.OutMarkEn, 0, 3 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsEnable, (int)CustomLvdsEnableBF.FpgaClkEn, 4 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsEnable, (int)CustomLvdsEnableBF.InSyncClkEn, 5 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsEnable, (int)CustomLvdsEnableBF.InRefClkEn, 6, 7 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsEnable, (int)CustomLvdsEnableBF.OutRefClkEn, 8 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsEnable, (int)CustomLvdsEnableBF.SyncDEn, 9 ),
                //
                new BitFieldDef( (int)WyvernReg.CustomLvdsForce, (int)CustomLvdsForceBF.OutMarkForceMode, 0, 3 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsForce, (int)CustomLvdsForceBF.OutMarkForceVal, 4, 7 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsForce, (int)CustomLvdsForceBF.FpgaClkForceMode, 8 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsForce, (int)CustomLvdsForceBF.FpgaClkForceVal, 9 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsForce, (int)CustomLvdsForceBF.SyncDForceMode, 10 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsForce, (int)CustomLvdsForceBF.SyncDForceVal, 11 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsForce, (int)CustomLvdsForceBF.OutRefClkForceMode, 12 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsForce, (int)CustomLvdsForceBF.OutRefClkVal, 13 ),
                //
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterCfg1, (int)LoopFilterCfg1BF.filtShift, 0, 5 ),
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterCfg1, (int)LoopFilterCfg1BF.invert, 6 ),
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterCfg1, (int)LoopFilterCfg1BF.run, 7 ),
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterCfg1, (int)LoopFilterCfg1BF.load, 8 ),
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterCfg1, (int)LoopFilterCfg1BF.sel, 9 ),
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterCfg1, (int)LoopFilterCfg1BF.thresh, 10, 13 ),
                //
				new BitFieldDef( (int)WyvernReg.IsoLoopFilterCfg2, (int)LoopFilterCfg2BF.loadVal, 0, 10 ),
                //
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterStat1, (int)LoopFilterStat1BF.filtOut, 0, 5 ),
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterStat1, (int)LoopFilterStat1BF.filtValid, 6 ),
                //
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterStat2, (int)LoopFilterStat2BF.delay, 0, 10 ),
                //
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterStatReq, (int)LoopFilterStatReqBF.request, 0 ),
                //
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterStatAck, (int)LoopFilterStatAckBF.acknowledge, 0 ),

                #endregion

                #region DacCfg  

                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.DdrRxEn,         0),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.DllClkRxEn,      1),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.OutMarkRxEn,     2, 3),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.DdrMuxEn,        4),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.TestEn,          5),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.Midscale,        6),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.OutMarkTxEn,     7, 8),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.AoutpOverHighEn, 9),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.AoutpOverLowEn,  10),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.AoutnOverHighEn,  11),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.AoutnOverLowEn,   12),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.OverForceEnable,  13),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.MarkRsEn,         14, 15),
                //
                new BitFieldDef( (int)WyvernReg.DacCfg2, (int)DacCfg2BF.CmlIref1, 0, 7 ),
                new BitFieldDef( (int)WyvernReg.DacCfg2, (int)DacCfg2BF.CmlIref2, 8, 15 ),
                //
				new BitFieldDef( (int)WyvernReg.DacCfg3, (int)DacCfg3BF.CmlOscEn1, 0 ),
                new BitFieldDef( (int)WyvernReg.DacCfg3, (int)DacCfg3BF.CmlOscEn2, 1 ),
                new BitFieldDef( (int)WyvernReg.DacCfg3, (int)DacCfg3BF.CmlOscSel, 2 ),
                new BitFieldDef( (int)WyvernReg.DacCfg3, (int)DacCfg3BF.CmlLpfEn1, 3, 4 ),
                new BitFieldDef( (int)WyvernReg.DacCfg3, (int)DacCfg3BF.CmlLpfEn2, 5, 6 ),
                //
				new BitFieldDef( (int)WyvernReg.DacCfg4, (int)DacCfg4BF.DacOutCasGateAdj, 0, 5 ),
                new BitFieldDef( (int)WyvernReg.DacCfg4, (int)DacCfg4BF.DacSecFoldCurAdj, 6, 11 ),
                new BitFieldDef( (int)WyvernReg.DacCfg4, (int)DacCfg4BF.DacRefSliceClkFbEn, 12 ),
                new BitFieldDef( (int)WyvernReg.DacCfg4, (int)DacCfg4BF.ATestLoBusSel, 13 ),
                new BitFieldDef( (int)WyvernReg.DacCfg4, (int)DacCfg4BF.ATestLoBusTiedownOn, 14 ),
                //
				new BitFieldDef( (int)WyvernReg.DacCfg5, (int)DacCfg5BF.CmlIrefp1, 0, 7 ),
                new BitFieldDef( (int)WyvernReg.DacCfg5, (int)DacCfg5BF.CmlIrefp2, 8, 15 ),
                //
				new BitFieldDef( (int)WyvernReg.DacCfg6, (int)DacCfg6BF.CmlSwing1, 0, 5 ),
                new BitFieldDef( (int)WyvernReg.DacCfg6, (int)DacCfg6BF.CmlSwing2, 6, 11 ),

                #endregion

                #region DacAnaClkSys

                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.AClkStatic, 0 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.AClkMux1, 1 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.AClkMux2, 2 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.LClkMux, 3 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.TestClkRxEn, 4 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnAClkDrive, 5 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnAClkMux2, 6 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnGlobalDelay, 7 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnIClkDrive, 8 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnLClkDelay, 9 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnLClkDrive, 10 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnLs, 11 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnTestClkDelay1, 12 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnTestClkDelay2, 13 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnIClk2Drive, 14 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.IClk2Off, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.LClkDelayCfg, (int)ClkDelayCfgBF.TestGain, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.LClkDelayCfg, (int)ClkDelayCfgBF.TestMode, 3 ),
                //
                new BitFieldDef( (int)WyvernReg.LClkDelayCoarseL, (int)ClkDelayCoarseLBF.coarseL, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.LClkDelayCoarseU, (int)ClkDelayCoarseUBF.coarseU, 0, 14 ),
                //
                new BitFieldDef( (int)WyvernReg.LClkDelayFineA, (int)ClkDelayFineABF.fineA, 0, 2 ),
                //
                new BitFieldDef( (int)WyvernReg.LClkDelayFineB, (int)ClkDelayFineBBF.fineB, 0, 14 ),
                //
                new BitFieldDef( (int)WyvernReg.LClkDelayTweak, (int)ClkDelayTweakBF.tweak0, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.LClkDelayTweak, (int)ClkDelayTweakBF.tweak1, 3, 6 ),
                //
                new BitFieldDef( (int)WyvernReg.GlobalDelayCfg, (int)ClkDelayCfgBF.TestGain, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.GlobalDelayCfg, (int)ClkDelayCfgBF.TestMode, 3 ),
                //
                new BitFieldDef( (int)WyvernReg.GlobalDelayCoarseL, (int)ClkDelayCoarseLBF.coarseL, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.GlobalDelayCoarseU, (int)ClkDelayCoarseUBF.coarseU, 0, 14 ),
                //
                new BitFieldDef( (int)WyvernReg.GlobalDelayFineA, (int)ClkDelayFineABF.fineA, 0, 2 ),
                //
                new BitFieldDef( (int)WyvernReg.GlobalDelayFineB, (int)ClkDelayFineBBF.fineB, 0, 14 ),
                //
                new BitFieldDef( (int)WyvernReg.GlobalDelayTweak, (int)ClkDelayTweakBF.tweak0, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.GlobalDelayTweak, (int)ClkDelayTweakBF.tweak1, 3, 6 ),
                //
                new BitFieldDef( (int)WyvernReg.RsLoopFilterCfg1, (int)LoopFilterCfg1BF.filtShift, 0, 5 ),
                new BitFieldDef( (int)WyvernReg.RsLoopFilterCfg1, (int)LoopFilterCfg1BF.invert, 6 ),
                new BitFieldDef( (int)WyvernReg.RsLoopFilterCfg1, (int)LoopFilterCfg1BF.run, 7 ),
                new BitFieldDef( (int)WyvernReg.RsLoopFilterCfg1, (int)LoopFilterCfg1BF.load, 8 ),
                new BitFieldDef( (int)WyvernReg.RsLoopFilterCfg1, (int)LoopFilterCfg1BF.sel, 9 ),
                new BitFieldDef( (int)WyvernReg.RsLoopFilterCfg1, (int)LoopFilterCfg1BF.thresh, 10, 13 ),
                //
				new BitFieldDef( (int)WyvernReg.RsLoopFilterCfg2, (int)LoopFilterCfg2BF.loadVal, 0, 10 ),
                //
                new BitFieldDef( (int)WyvernReg.RsLoopFilterStat1, (int)LoopFilterStat1BF.filtOut, 0, 5 ),
                new BitFieldDef( (int)WyvernReg.RsLoopFilterStat1, (int)LoopFilterStat1BF.filtValid, 6 ),
                //
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterStat2, (int)LoopFilterStat2BF.delay, 0, 10 ),
                //
                new BitFieldDef( (int)WyvernReg.RsLoopFilterStatReq, (int)LoopFilterStatReqBF.request, 0 ),
                //
                new BitFieldDef( (int)WyvernReg.RsLoopFilterStatAck, (int)LoopFilterStatAckBF.acknowledge, 0 ),
                //
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg10, (int)DllPhaseDetCfg1BF.DelayClk, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg10, (int)DllPhaseDetCfg1BF.DelayDllClk, 3, 6 ),
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg10, (int)DllPhaseDetCfg1BF.DithNreset, 7 ),
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg10, (int)DllPhaseDetCfg1BF.DithScale, 8, 10 ),
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg10, (int)DllPhaseDetCfg1BF.fbEn, 11 ),
                //
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg11, (int)DllPhaseDetCfg1BF.DelayClk, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg11, (int)DllPhaseDetCfg1BF.DelayDllClk, 3, 6 ),
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg11, (int)DllPhaseDetCfg1BF.DithNreset, 7 ),
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg11, (int)DllPhaseDetCfg1BF.DithScale, 8, 10 ),
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg11, (int)DllPhaseDetCfg1BF.fbEn, 11 ),
                //
				new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg20, (int)DllPhaseDetCfg2BF.BiasAdj, 0, 7 ),
                //
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg21, (int)DllPhaseDetCfg2BF.BiasAdj, 0, 7 ),
                //
                new BitFieldDef( (int)WyvernReg.ClkaSpare, (int)ClkaSpareBF.FbEnFffSlice, 0 ),
                new BitFieldDef( (int)WyvernReg.ClkaSpare, (int)ClkaSpareBF.WatchdogForceEn, 1 ),
                new BitFieldDef( (int)WyvernReg.ClkaSpare, (int)ClkaSpareBF.FbEnAClkBufMark, 2 ),

                #endregion
            };

        #endregion constants/enums/lookup tables

        #region Properties

        internal IRegister DacConfig1
        {
            get;
            private set;
        }

        internal IRegister FinalMuxInCfg
        {
            get;
            private set;
        }

        internal IRegister Scale
        {
            get;
            private set;
        }

        #endregion Properties

        #region constructors

        /// <summary>
        /// Creates the module specific carrier register set "from scratch" using RegFactory and the definitions located
        /// in CommonCarrierRegisterSet (namely, mRegisterDefinitions and mBitFieldDefinitions).
        /// </summary>
        /// <param name="manager">IRegManager instance ... the created registers will be added as a group to this</param>
        /// <param name="module">The module these register are for (used for name, RegDriver, ...)</param>
        /// <param name="groupName">Name of the group the created registers will be registered as in IRegManager</param>
        /// <param name="baseAddr">Base address for register definitions (normally 0)</param>
        public WyvernRegisterSet(String moduleName, string groupName, IRegister wyvernRegister)
        {
            // Create the registers and bitfields...
            //RegFactory wyvernRegFactory = new RegFactory(module, AddrDataReg32.ConstructReg);
            RegFactory wyvernRegFactory = new RegFactory();

            // Custom common plugin registers...
            List<RegDef> registerDefs = new List<RegDef>(mRegisterDefinitions);
            List<BitFieldDef> bitFieldDefs = new List<BitFieldDef>(mBitFieldDefinitions);

            //object[] addrDataRegFields = 
            //{
            //    wyvernRegister.GetField( (int)WyvernBF.Address ),
            //    wyvernRegister.GetField( (int)WyvernBF.Value ),
            //    wyvernRegister.GetField( (int)WyvernBF.RW ),
            //    0,
            //    1
            //};
            object[] addrDataRegFields = 
            {
                0
            };

            Registers = wyvernRegFactory.CreateRegArray(mRegisterDefinitions,
                typeof(WyvernReg),
                moduleName,
                String.Empty,
                addrDataRegFields);

            wyvernRegFactory.CreateBitFields(mBitFieldDefinitions,
                Registers,
                moduleName
                );

            // Add the Wyvern registers to the register manager under the specified group name.
            DacConfig1 = Registers[(int)WyvernReg.DacCfg1];
            FinalMuxInCfg = Registers[(int)WyvernReg.FinalMuxInCfg];
            Scale = Registers[(int)WyvernReg.Scale];
        }

        #endregion constructors

        public void SetInitialValues()
        {
            //base.SetInitialValues();
            Registers[(int)WyvernReg.DacCfg1].Value32 = 0x3e40;
            Registers[(int)WyvernReg.DacCfg2].Fields[(int)DacCfg2BF.CmlIref1].Value = 63;
            Registers[(int)WyvernReg.DacCfg2].Fields[(int)DacCfg2BF.CmlIref2].Value = 63;
            Registers[(int)WyvernReg.DacCfg4].Fields[(int)DacCfg4BF.DacOutCasGateAdj].Value = 32;
            Registers[(int)WyvernReg.DacCfg4].Fields[(int)DacCfg4BF.DacSecFoldCurAdj].Value = 32;
            Registers[(int)WyvernReg.DacCfg5].Fields[(int)DacCfg5BF.CmlIrefp1].Value = 63;
            Registers[(int)WyvernReg.DacCfg5].Fields[(int)DacCfg5BF.CmlIrefp2].Value = 63;
            Registers[(int)WyvernReg.DacCfg6].Fields[(int)DacCfg6BF.CmlSwing1].Value = 32;
            Registers[(int)WyvernReg.DacCfg6].Fields[(int)DacCfg6BF.CmlSwing2].Value = 38;
            Registers[(int)WyvernReg.FinalMuxInCfg].Value32 = 0x0040;
            Registers[(int)WyvernReg.ScaleCfg].Value32 = 0x000b;
            Registers[(int)WyvernReg.Scale].Value32 = 0x4000;
            Registers[(int)WyvernReg.DllClkCfg].Fields[(int)DllClkCfgBF.DataMarkSelect].Value = 6;
            Registers[(int)WyvernReg.DllClkCfg].Fields[(int)DllClkCfgBF.Delay].Value = 256;
            Registers[(int)WyvernReg.DllPhaseDetCfg10].Fields[(int)DllPhaseDetCfg1BF.DelayDllClk].Value = 6;
            Registers[(int)WyvernReg.DllPhaseDetCfg20].Fields[(int)DllPhaseDetCfg2BF.BiasAdj].Value = 63;
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.filtShift].Value = 6;
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.thresh].Value = 4;
            Registers[(int)WyvernReg.IsoLoopFilterCfg2].Fields[(int)LoopFilterCfg2BF.loadVal].Value = 1024;
            Registers[(int)WyvernReg.ClkdCfg].Fields[(int)ClkdCfgBF.FpgaClkCount].Value = 7;
            Registers[(int)WyvernReg.ClkdCfg].Fields[(int)ClkdCfgBF.FpgaClkMux].Value = 1;
            Registers[(int)WyvernReg.ClkaCfg].Fields[(int)ClkaCfgBF.IClk2Off].Value = 1;            
        }

        /// <summary>
        /// Complete brute force approach to reset the registers to a known state.  Will set all the registers
        /// to the default values as per the ERS.
        /// </summary>
        public void ResetRegisters()
        {
            ResetGeneralRegisters();
            ResetMarkPropagationRegisters();
            ResetGncoRegisters();
            ResetSignalGenRegisters();
            ResetDdrInterpRegisters();
            ResetDemRegisters();
            ResetFinalMuxInRegisters();
            ResetFinalMuxRegisters();
            ResetDacCfgRegisters();
            ResetDacAnaClkSysRegisters();
        }

        public void ResetGeneralRegisters()
        {
            Registers[(int)WyvernReg.Mark].Value32 = 0x0;
            Registers[(int)WyvernReg.CClkGate].Value32 = 0x3ffd;
        }

        public void ResetMarkPropagationRegisters()
        {
            Registers[(int)WyvernReg.InMarkCfg].Value32 = 0x0;
            Registers[(int)WyvernReg.OutMarkCfg0].Value32 = 0x4006;
            Registers[(int)WyvernReg.OutMarkCfg1].Value32 = 0x4006;
            Registers[(int)WyvernReg.OutMarkCfg2].Value32 = 0x4006;
            Registers[(int)WyvernReg.OutMarkCfg3].Value32 = 0x4006;
        }

        public void ResetGncoRegisters()
        {
            Registers[(int)WyvernReg.GNcoCfg].Value32 = 0x0c00;
            Registers[(int)WyvernReg.GNcoDFL].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoDFU].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoPhaseOffsetL].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoPhaseOffsetU].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoScale].Value32 = 0x7fff;
            Registers[(int)WyvernReg.GNcoFreq_0].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoFreq_1].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoFreq_2].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoFreq_3].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoFreq_4].Value32 = 0x1800;
            Registers[(int)WyvernReg.GNcoMarkSel].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoMarkCfg].Value32 = 0x1555;
        }

        public void ResetSignalGenRegisters()
        {
            Registers[(int)WyvernReg.ScaleCfg].Value32 = 0x000b;
            Registers[(int)WyvernReg.Scale].Value32 = 0x4000;
        }

        public void ResetDdrInterpRegisters()
        {
            Registers[(int)WyvernReg.DdrInterpCfg].Value32 = 0x0;
        }

        public void ResetDemRegisters()
        {
            Registers[(int)WyvernReg.DemCfg].Value32 = 0x0001;
        }

        public void ResetFinalMuxInRegisters()
        {
            Registers[(int)WyvernReg.FinalMuxInCfg].Value32 = 0x0004;
            Registers[(int)WyvernReg.DllClkCreateMark].Value32 = 0x0;
            Registers[(int)WyvernReg.DllClkCfg].Value32 = 0x4006;
            Registers[(int)WyvernReg.DllClkWidth].Value32 = 0x0;
        }

        public void ResetFinalMuxRegisters()
        {
            Registers[(int)WyvernReg.FinalMuxCfg].Value32 = 0x0;
            Registers[(int)WyvernReg.ClkdCfg].Value32 = 0x000f;
            Registers[(int)WyvernReg.MuxClkDelayCfg].Value32 = 0x0;
            Registers[(int)WyvernReg.MuxClkDelayCoarseL].Value32 = 0x0;
            Registers[(int)WyvernReg.MuxClkDelayCoarseU].Value32 = 0x0;
            Registers[(int)WyvernReg.MuxClkDelayFineA].Value32 = 0x0;
            Registers[(int)WyvernReg.MuxClkDelayFineB].Value32 = 0x0;
            Registers[(int)WyvernReg.MuxClkDelayTweak].Value32 = 0x0;
            Registers[(int)WyvernReg.CustomLvdsEnable].Value32 = 0x001f;
            Registers[(int)WyvernReg.CustomLvdsForce].Value32 = 0x000f;
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Value32 = 0x1006;
            Registers[(int)WyvernReg.IsoLoopFilterCfg2].Value32 = 0x0400;
            Registers[(int)WyvernReg.IsoLoopFilterStatReq].Value32 = 0x0;
        }

        public void ResetDacCfgRegisters()
        {
            Registers[(int)WyvernReg.DacCfg1].Value32 = 0x3e40;
            Registers[(int)WyvernReg.DacCfg2].Value32 = 0x3f3f;
            Registers[(int)WyvernReg.DacCfg3].Value32 = 0x0;
            Registers[(int)WyvernReg.DacCfg4].Value32 = 0x0820;
            Registers[(int)WyvernReg.DacCfg5].Value32 = 0x3f3f;
            Registers[(int)WyvernReg.DacCfg6].Value32 = 0x09a0;
        }

        public void ResetDacAnaClkSysRegisters()
        {
            Registers[(int)WyvernReg.ClkaCfg].Value32 = 0x8000;
            Registers[(int)WyvernReg.LClkDelayCfg].Value32 = 0x0;
            Registers[(int)WyvernReg.LClkDelayCoarseL].Value32 = 0x0;
            Registers[(int)WyvernReg.LClkDelayCoarseU].Value32 = 0x0;
            Registers[(int)WyvernReg.LClkDelayFineA].Value32 = 0x0;
            Registers[(int)WyvernReg.LClkDelayFineB].Value32 = 0x0;
            Registers[(int)WyvernReg.LClkDelayTweak].Value32 = 0x0;
            Registers[(int)WyvernReg.GlobalDelayCfg].Value32 = 0x0;
            Registers[(int)WyvernReg.GlobalDelayCoarseL].Value32 = 0x0;
            Registers[(int)WyvernReg.GlobalDelayCoarseU].Value32 = 0x0;
            Registers[(int)WyvernReg.GlobalDelayFineA].Value32 = 0x0;
            Registers[(int)WyvernReg.GlobalDelayFineB].Value32 = 0x0;
            Registers[(int)WyvernReg.GlobalDelayTweak].Value32 = 0x0;
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Value32 = 0x1006;
            Registers[(int)WyvernReg.RsLoopFilterCfg2].Value32 = 0x0400;
            Registers[(int)WyvernReg.RsLoopFilterStatReq].Value32 = 0x0;
            Registers[(int)WyvernReg.DllPhaseDetCfg10].Value32 = 0x0030;
            Registers[(int)WyvernReg.DllPhaseDetCfg11].Value32 = 0x0030;
            Registers[(int)WyvernReg.DllPhaseDetCfg20].Value32 = 0x003f;
            Registers[(int)WyvernReg.DllPhaseDetCfg21].Value32 = 0x003f;
            Registers[(int)WyvernReg.ClkaSpare].Value32 = 0x0;
        }
    }
}
