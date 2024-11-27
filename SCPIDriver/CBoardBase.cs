using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
namespace ModuleDriver
{
    public class CBoardBase
    {
        public int ID;
        public SCPIDriver Driver;
        public string BoardName = "";
        public bool IsBoardDriverMark = true;
    }
}
