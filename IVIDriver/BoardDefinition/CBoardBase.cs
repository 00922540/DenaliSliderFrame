using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace BoardDriver
{
    public class CBoardBase
    {
        public int ID;
        public IVIDriver Driver;
        public string BoardName = "";
        public bool IsBoardDriverMark = true;

    }
}
