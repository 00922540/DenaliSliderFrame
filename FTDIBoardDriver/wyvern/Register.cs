using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FTDIBoardDriver;

namespace Keysight.ModularInstruments.Core.Register
{
    public class Register:IRegister
    {
        protected readonly IBitField mAddrField;
        protected readonly IBitField mDataField;
        protected readonly IBitField mRwField;
        protected readonly int mReadValue;
        protected readonly int mWriteValue;
        protected int mValue;

        protected string regName;
        protected readonly string mName;
        protected Int32 mOffset; // location of the Reg in BAR space.   
        protected Type mBFType; // the enumerated type for the bitfields this reg contains
        protected int mNumBFs; // number of Bitfields contained in this register
        protected IBitField[] mFields;
        protected int mFirstBFvalue = -1; // sometimes the enumerated bit field list does not start with 0

        public BoardDriver.FTDIBoardDriver driver; //driver = new BoardDriver.VXT3BoardDriver();
        public int LastBF
        {
            get
            {
                return mFirstBFvalue + mNumBFs - 1;
            }
        }
        public int SizeInBytes
        {
            get
            {
                return sizeof(Int32);
            }
        }       
        public int Value32
        {
            get
            {
                return mValue;
            }
            set
            {
                mValue = value;
            }
        }
        public int NumBitFields
        {
            get
            {
                return mNumBFs;
            }
        }
        public Int32 Offset
        {
            get
            {
                return mOffset;
            }
        }
        public string Name
        {
            get
            {
                return mName;
            }
        }
        public string NameBase
        {
            get
            {
                int i = mName.IndexOf('_');
                return (i > 0) ? mName.Substring(i + 1) : mName;
            }
        }
        public bool IsCommand
        {
            get;
            private set;
        }
        public IBitField GetField(uint i)
        {
            if (i > LastBF)
            {
                throw new ApplicationException("Field index exceeds numBitFieldsFields");
            }

            return mFields[i];
        }
        public virtual IBitField[] Fields
        {
            get
            {
                return mFields;
            }
            set
            {
                mFields = value;
                // Determine the BF characteristics
                mFirstBFvalue = 0;
                for (int j = 0; j < mFields.Length; j++)
                {
                    if (mFields[j] != null)
                    {
                        mFirstBFvalue = j;
                        break;
                    }
                }
                mNumBFs = mFields.Length - mFirstBFvalue;
            }
        }
        public int FirstBF
        {
            get
            {
                return mFirstBFvalue;
            }
        }
        public Type BFType
        {
            get
            {
                return mBFType;
            }
        }
        public Register( string name,
                              Int32 offset,
                              Type bfType/*,                              
                              IBitField addrField,
                              IBitField dataField,
                              IBitField rwField,
                              int readValue,
                              int writeValue*/ )            
        {
            //mAddrField = addrField;
            //mDataField = dataField;
            //mRwField = rwField;
            //mReadValue = readValue;
            //mWriteValue = writeValue;
            mOffset = offset;
            mName = name;
            if (name.Contains("Wyvern1"))
                regName = "Wyver1";
            else
                regName = "Wyver2";
            if (bfType != null)
            {             
                // PREPARING FOR UNUSED COMMON BITS
                Array values = Enum.GetValues(bfType);
                mFirstBFvalue = (byte)((int)values.GetValue(0));
                mNumBFs = (byte)values.Length;                
                // we create an array for enum values 0-LastBFvalue
                mFields = new IBitField[LastBF + 1];
                // mNumBFs = (byte) (maxValue+1);

                mBFType = bfType;
            }
            else
            {
                mFields = null;
            }
            //????Reset(); // reset IEnumerable index.
        }
        public void Apply(bool ForceApply)
        {
            int readWrite = 1;
            int parity = 0;
            long data = mValue;
            long contrlData =mOffset;// (( << 2 + parity) << 1 + readWrite);
            contrlData = contrlData << 2;
            contrlData += parity;
            contrlData = contrlData << 1;
            contrlData += readWrite;
            string contrlStr = Convert.ToString(contrlData, 2).PadLeft(13 + 2 + 1, '0');
            //string dataStr = Convert.ToString(data, 2).PadLeft(32, '0');
            driver.Write(regName + "@" + contrlStr, data);           
        }
        public void Write(int value)
        {  
            mValue = value;
            int readWrite = 1;
            int parity = 0;
            long data = mValue;
            long contrlData = mOffset;
            contrlData = contrlData << 2;
            contrlData += parity;
            contrlData = contrlData << 1;
            contrlData += readWrite;
            string contrlStr = Convert.ToString(contrlData, 2).PadLeft(13 + 2 + 1, '0');
            //string dataStr = Convert.ToString(data, 2).PadLeft(32, '0');
            driver.Write(regName + "@" + contrlStr, data);                         
        }
        public int Read()
        {
            int readWrite = 0;
            int parity = 0;
            //long contrlData = ((mOffset << 2 + parity) << 1 + readWrite);
            long contrlData = mOffset;
            contrlData = contrlData << 2;
            contrlData += parity;
            contrlData = contrlData << 1;
            contrlData += readWrite;
            string contrlStr = Convert.ToString(contrlData, 2).PadLeft(13 + 2 + 1, '0');
            long dataBack = driver.Read(regName+"@"+contrlStr);
            int mask = 0x1FFFE;
            dataBack = (dataBack & mask) >> 1;            
            mValue = (int)dataBack;
            return mValue;
        }
        public void AddField(IBitField bf)
        {
            if (mBFType == null)
            {
                throw new Exception("Can't add bit fields to a register that does not contain bitfields.");
            }

            // remove all characters up to and including the "_" (the RegName) in order to
            //  get just the BitField portion of the name.
            string bfName = bf.Name;
            //int underScoreLocation = bfName.LastIndexOf("_");
            int underScoreLocation = bfName.LastIndexOf(":", StringComparison.InvariantCultureIgnoreCase);

            int firstCharOfFieldName = underScoreLocation + 1;

            if (firstCharOfFieldName == 0)
            {
                // no "_" was found, so we must strip out any " "s instead
                firstCharOfFieldName = bfName.LastIndexOf(" ", StringComparison.InvariantCultureIgnoreCase) +
                                       " ".Length;
            }

            string bfNameOnly = bfName.Substring(firstCharOfFieldName, bfName.Length - firstCharOfFieldName);
            int index;

            // see if the BFname exists in the enum list or not.
            try
            {
                // If it exists we will have it's index position into the containing array.
                index = (int)Enum.Parse(mBFType, bfNameOnly);
            }
            catch
            {
                // the BitField passed in was not implementing one of the valid BitFields
                // for this register.
                throw new ArgumentException("No enum match found for BitField name '" +
                                             bfName + "'");
            }

            SetField(index, bf);
        }
        protected void SetField(int i, IBitField bf)
        {
            mFields[i] = bf;
        }
    }
}
