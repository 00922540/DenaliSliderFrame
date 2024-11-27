using System;
using System.Collections.Generic;
using Keysight.ModularInstruments.KtM9347.Registers;
using System.Diagnostics;
using Keysight.ModularInstruments.Core.Register;

namespace Keysight.ModularInstruments
{

    public abstract class WyvernCalBase
    {
        public delegate double MeasureMarkerValueDelegation();
        public delegate void PrintTestResultDelegation(string value);

        private bool opt_sa = true;

        protected string mSlugSerialNumber = "";
        protected WyvernRegisterSet mRegiserset;
        protected string whichWyvern = "";
        protected string mSavePath = "";
        public MeasureMarkerValueDelegation mMeasureMarkerValueMethod = null;
        public PrintTestResultDelegation mPrintTestResultMethod = null;

        public WyvernCalBase(//string slugSerialNumber,
                                      string wyvernName,
                                      string calDataSavePath,
                                      WyvernRegisterSet regiserset,
                                      MeasureMarkerValueDelegation measureDelegation, PrintTestResultDelegation printTestResultDelegation)
        {
            //mSlugSerialNumber = slugSerialNumber;
            whichWyvern = wyvernName;
            mRegiserset = regiserset;
            mSavePath = calDataSavePath;

            mMeasureMarkerValueMethod = measureDelegation;
            mPrintTestResultMethod = printTestResultDelegation;
        }


        abstract public void Execute();

        protected void WriteWyvernRegisterField(string regName, string BFName, int value)
        {
            getBitField(getRegister(regName), BFName).Write(value);
        }

        protected int ReadWyvernRegisterField(string regName, string BFName)
        {
            getRegister(regName).Read();
            return getBitField(getRegister(regName), BFName).Value;
        }

        protected void WriteWyvernRegister(string regName, int value)
        {
            getRegister(regName).Write(value);
        }

        protected int ReadWyvernRegister(string regName)
        {
            return getRegister(regName).Read();
        }

        protected IRegister getRegister(string regName)
        {
            foreach (var item in mRegiserset.Registers)
            {
                if (item.NameBase == regName)
                {
                    return item;
                }
            }
            throw new Exception(String.Format("Register name : {0} is not defined", regName));
        }

        protected IBitField getBitField(IRegister reg, string BFName)
        {
            foreach (var item in reg.Fields)
            {
                if (item.ShortName == BFName)
                {
                    return item;
                }
            }

            //the BF name is not found return the Bit Field 0
            if(reg.Fields.Length==1)
            {
                return reg.Fields[0];
            }
            else
            {
                throw new Exception(String.Format("Register {0}, Bit Field {1} is not defined", reg.Name, BFName));
            }
        }

        protected double ReadMarkerAmplitude()
        {
            if (mMeasureMarkerValueMethod != null)
            {
                return mMeasureMarkerValueMethod();
            }
            else
            {
                return 0.0;
            }
        }


        protected void SetDDRMode()
        {
            //CClkGate
            //Refresh();
            WriteWyvernRegisterField("CClkGate", "DdrInterp", 0x0);
            WriteWyvernRegisterField("CClkGate", "EncodingB", 0x0);

            //DdrInterpCfg
            //Refresh();
            WriteWyvernRegisterField("DdrInterpCfg", "Enable", 0x1);
            WriteWyvernRegisterField("DdrInterpCfg", "DoubletMode", 0x0);

            //FinalMuxCfg
            //Refresh();
            WriteWyvernRegisterField("FinalMuxCfg", "DdrEn", 0x1);

            //DacCfg1
            //Refresh();
            WriteWyvernRegisterField("DacCfg1", "DdrMuxEn", 0x1);
            WriteWyvernRegisterField("DacCfg1", "DdrRxEn", 0x1);

            //DemCfg
            //Refresh();
            WriteWyvernRegisterField("DemCfg", "Enable", 0x1);
            WriteWyvernRegisterField("DemCfg", "DoDdr", 0x1);
            WriteWyvernRegisterField("DemCfg", "DemLsbClip", 0x0);

        }

        protected double measVolt(int numAvgs)
        {
            double amplitude = 0;
            double volts = 0;

            if (opt_sa)
            {

                int i = 0;
                while (i < numAvgs)
                {
                    amplitude = ReadMarkerAmplitude();

                    volts = ConvertdBmtoVrms(amplitude);

                    //volts += volts;
                    i++;
                }
            }
            else
            { }

            return volts / numAvgs;
        }

        protected double ConvertdBmtoVrms(double amplitude)
        {
            double volts;
            //PXA reports back the FFT of the waveform so make sure to 2 * power to capture real 
            //and imaginary component when converting back to volts?
            //formula grabbed from: http://rfmw.em.keysight.com/wireless/helpfiles/89600b/webhelp/subsystems/gettingstarted/content/concepts_decibels.htm
            volts = (.2236 * Math.Pow(10, (amplitude / 20)));
            return volts;
        }

    }

}