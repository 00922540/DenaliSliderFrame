using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SliderDriver
{
    public interface IBoard
    {
        bool Connect(string Resource, bool idquery, bool reset,string options);
        void Write(string RegName, string Group, long value);
        long Read(string RegName, string Group);
        void Close();
    }
}
