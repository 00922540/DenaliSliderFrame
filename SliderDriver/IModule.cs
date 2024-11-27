using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SliderDriver
{
    public interface IModule
    {
        bool Connect(string Addr, bool Locked, int TimeOut);
        UInt64 ReadLatch(string mlatch);
        void WriteLatch(string mlatch, UInt64 mvalue);
        void UnlockAllLatch();
    }
}
