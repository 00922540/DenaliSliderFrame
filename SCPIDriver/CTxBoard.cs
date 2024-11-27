using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleDriver
{
    public class CTxBoard : CBoardBase
    {
        public void AMPPower(bool on)
        {
            ulong power = Driver.ReadLatch(string.Format("Source ExternalRf:{0}-{1}", "TxBoard", "PowerControl"));
            if (on)
            {
                Driver.WriteLatch(string.Format("Source ExternalRf:{0}-{1}", "TxBoard", "PowerControl"), power | 0x0000001ff800);
            }
            else
            {
                Driver.WriteLatch(string.Format("Source ExternalRf:{0}-{1}", "TxBoard", "PowerControl"), power | 0xffe007ff);
            }

        }
        public bool GetAMPPower()
        {
            ulong power = Driver.ReadLatch(string.Format("Source ExternalRf:{0}-{1}", "TxBoard", "PowerControl"));
            if ((power & 0x0000001ff800) == 0x0000001ff800)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
