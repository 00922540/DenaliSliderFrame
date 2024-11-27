using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDriver
{
    public class CCarrierBoard : CBoardBase
    {
        public void setWyvernFreq(int wyvernNum, double frequency)
        {
            if (1 == wyvernNum)
            {
                Driver.SetValue("CarrierBoard;SourceWyvernFrequency", frequency.ToString());

            }
            else
            {
                Driver.SetValue("CarrierBoard;ReceiverWyvernFrequency", frequency.ToString());

            }

        }
        public void setWyvernScale(int wyvernNum, double scale)
        {
            if (1 == wyvernNum)
            {
                Driver.SetValue("CarrierBoard;SourceWyvernScale", scale.ToString());
            }
            else
            {
                Driver.SetValue("CarrierBoard;ReceiverWyvernScale", scale.ToString());
            }
        }
        public void SetWyvernVterm(int iWyven, double value)
        {
            if (1 == iWyven)
            {
                Driver.SetValue("CarrierBoard;Ltc2602SourceDac", value.ToString());
            }
            else
            {
                Driver.SetValue("CarrierBoard;Ltc2602ReceiverDac", value.ToString());
            }

        }
        public double GetWyvernFreq(int wyvernNum)
        {
            double rs;
            if (1 == wyvernNum)
            {
                rs = Convert.ToDouble(Driver.GetValue("CarrierBoard;SourceWyvernFrequency"));
            }
            else
            {
                rs = Convert.ToDouble(Driver.GetValue("CarrierBoard;ReceiverWyvernFrequency"));
            }
            return rs;
        }
        public double GetWyvernScale(int wyvernNum)
        {
            double rs;
            if (1 == wyvernNum)
            {
                rs = Convert.ToDouble(Driver.GetValue("CarrierBoard;SourceWyvernScale"));
            }
            else
            {
                rs = Convert.ToDouble(Driver.GetValue("CarrierBoard;ReceiverWyvernScale"));
            }
            return rs;
        }
        public double GetWyvernVterm(int wyvernNum)
        {
            double rs;
            if (1 == wyvernNum)
            {
                rs = Convert.ToDouble(Driver.GetValue("CarrierBoard;Ltc2602SourceDac"));
            }
            else
            {
                rs = Convert.ToDouble(Driver.GetValue("CarrierBoard;Ltc2602ReceiverDac"));
            }
            return rs;
        }
    }
}
