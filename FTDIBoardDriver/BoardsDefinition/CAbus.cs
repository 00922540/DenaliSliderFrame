using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDriver
{
    public class CAbus
    {
        public string Name{ get; set; }
        public int Addr{ get; set; }
        public double Coefficient{ get; set; }
        public int Size { get; set; }
        public CAbus(string name, string addr, double coeff)
        {
            this.Name = name;
            this.Addr = Convert.ToInt32(addr,2);
            this.Coefficient = coeff;
            this.Size = addr.Length;
        }
        public CAbus( string addr, double coeff)
        {
            this.Addr = Convert.ToInt32(addr, 2);
            this.Coefficient = coeff;
            this.Size = addr.Length;
        }
    }
}
