/******************************************************************************
 *                                                                         
 *               Copyright 2011-2012 Keysight Technologies, Inc.
 *               All rights reserved.
 *
 *****************************************************************************/
using System;

namespace Keysight.ModularInstruments.Core.Register
{
    /// <summary>
    /// A RegField is a BitField within a specific Register.  a RegField will modify a 
    /// a BitField within a register where the bitfield may be in size from 1 bit up to
    /// the whole size of the register.
    /// </summary>
    /// <remarks>
    /// Because registers are involved in literally millions of operations (register read/write)
    /// it is imperative that the implementation is as efficient as possible -- so we exclude
    /// this class from control flow obfuscation
    /// </remarks>
    [System.Reflection.ObfuscationAttribute(Feature = "controlflow=false")]
    public class RegField:IBitField
    {
        protected readonly IRegister mRegister;
        protected readonly int mMask;
        private const int REG_SIZE = 32;

        private readonly string mName;
        protected readonly byte mStartBit;
        protected readonly byte mSizeInBits;

        public int EndBit
        {
            get
            {
                return mStartBit + mSizeInBits - 1;
            }
        }
        public string Name
        {
            get
            {
                return mName;
            }
        }
        public int Size
        {
            get
            {
                return mSizeInBits;
            }
        }
        public int StartBit
        {
            get
            {
                return mStartBit;
            }
        }
        public string ShortName
        {
            get
            {
                // remove all characters up to and including the ":" (the RegName) in order to
                //  get just the BitField portion of the name.
                int firstCharOfShortName = mName.IndexOf(":") + 1;

                return mName.Substring(firstCharOfShortName).Trim();
            }
        }
        // Create the RegField
        public RegField(string bitFieldName,
                           int startBit,
                           int sizeInBits,
                           IRegister reg)           
        {
            mRegister = reg;
            mMask = (int)CreateMask(REG_SIZE, sizeInBits, startBit);

            mName = bitFieldName;
            mStartBit = (byte)startBit;
            mSizeInBits = (byte)sizeInBits;

            if (startBit + sizeInBits > REG_SIZE)
            {
                throw new Exception(
                    string.Format("Register '{0}' startBit ({1}) + bitFieldSizeInBits ({2}) exceeds RegSize ({3})",
                                   bitFieldName,
                                   startBit,
                                   sizeInBits,
                                   REG_SIZE));
            }
        }
        protected static long CreateMask(Int32 regSizeInBits, Int32 fieldSizeInBits, Int32 startBit)
        {
            long mask;


            if (fieldSizeInBits == regSizeInBits)
            {
                unchecked
                {
                    mask = (long)0xffffffffffffffff;
                }
            }
            else
            {
                unchecked
                {
                    mask = ~((long)0xffffffffffffffff << fieldSizeInBits) << startBit;
                }
            }

            return mask;
        }       

        /// <summary>
        /// Compute the value to set the register to (either value caching or writing
        /// directly to hardware).
        /// 
        /// This version preserves all bits outside of this field (appropriate for commands and events)
        /// </summary>
        /// <param name="bitsToSet"></param>
        /// <returns></returns>
        protected virtual int UpdateRegField(int bitsToSet)
        {           
            return (mRegister.Value32 & ~mMask) | ((bitsToSet << mStartBit) & mMask);
        }

        protected long ReadBitField()
        {
            uint temp = (uint)mRegister.Value32 & (uint)mMask;
            temp >>= mStartBit;
            long bitFieldVal = temp;           
            return bitFieldVal;
        }

        /// <summary>
        /// Gets or Sets the cached latch value, does not access HW.  A set leaves the register
        /// marked dirty. Use Apply() to write dirty values to HW or use Write() to "directly"
        /// write value to HW.
        /// 
        /// Most implementations preserve all bits outside the current bitfield definition,
        /// but some implementations (CommandField32, CommandField64) clear all other bits.
        /// </summary>
        public int Value
        {
            get
            {
                return (int)ReadBitField();
            }
            set
            {
                // See doc for UpdateRegField -- some versions clear all other bits!
                mRegister.Value32 = UpdateRegField(value);
            }
        }

        /// <summary>
        /// Gets or Sets the cached latch value, does not access HW.  A set leaves the register
        /// marked dirty. Use Apply() to write dirty values to HW or use Write() to "directly"
        /// write value to HW.
        /// 
        /// Most implementations preserve all bits outside the current bitfield definition,
        /// but some implementations (CommandField32, CommandField64) clear all other bits.
        /// </summary>
        public long Value64
        {
            get
            {
                return ReadBitField();
            }
            set
            {
                // See doc for UpdateRegField -- some versions clear all other bits!
                mRegister.Value32 = UpdateRegField((int)value);
            }
        }

        /// <summary>
        /// Gets the mask for the bit field within the register.
        /// </summary>
        public long Mask
        {
            get
            {
                // Casting to (uint) prevents 0x80000000 from turning into 0xffffffff80000000
                return (uint)mMask;
            }
        }

        /// <summary>
        /// Return a reference to the register this field belongs to.  THIS MAY BE NULL!!!
        /// </summary>
        public IRegister Register
        {
            get
            {
                return mRegister;
            }
        }

        public void Write(int value)
        {
            mRegister.Write(UpdateRegField(value));
        }
    }
}