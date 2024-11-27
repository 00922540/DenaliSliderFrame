using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BoardDriver
{
    public class CEquipment
    {
        public IVIDriver Driver;
        public string BoardName = "";
        public string EquipmentName = "";
        public int BoardID = 0;
        public int Addr;
        public int Size;
        public CEquipment(int iAddr, int iSize)
        {
            Addr = iAddr;
            Size = iSize;
        }
        public void ReSetBits(long Value)
        {
            long Data = this.Driver.Read(EquipmentName, BoardName);
            Data = Data | Value;
            this.Driver.Write(EquipmentName, BoardName, Value);
        }
        public void Write(long Value)
        {
            this.Driver.Write(EquipmentName, BoardName, Value);
        }
        public long Read()
        {
            return this.Driver.Read(EquipmentName, BoardName);
        }
    }
}
