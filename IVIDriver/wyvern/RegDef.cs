/******************************************************************************
 *                                                                         
 *               Copyright 2011 Keysight Technologies, Inc.
 *               All rights reserved.
 *
 *****************************************************************************/

using System;

namespace Keysight.ModularInstruments.Core.Register
{
    /// <summary>
    /// Defines a register.
    /// </summary>
    public class RegDef
    {
        private const int DEFAULT_REG_SIZE = 32; // in bits

        /// <summary>
        /// Multi purpose variable. Serves as index into the created Reg array, so it must
        /// start at 0 and sequence with no gaps. Also is the Register name
        /// </summary>
        public int nameEnum;

        /// <summary>
        /// BARoffset is an offset from "something". Typically this is an offset into a memory
        /// block for PCIe BAR (Base Address Register) -- i.e. a register.  Different types of
        /// registers may use this as an offset to an arbitrary concept (e.g. "device registers"
        /// use this as the index of a device's internal registers).
        /// </summary>
        public int BARoffset;

        /// <summary>
        /// Enum type that defines the BitFields within the register. This enum type must:
        /// start at 0, be sequential in value with no gaps, and have an enum value for
        /// each and every BitField within the register.
        /// </summary>
        public Type BFenum;       
        

        /// <summary>
        /// NEW: Most register types ignore this!
        /// 
        /// The size of the register in bits (8,16,24,32...).  Typically used to control
        /// the number of byte operations that make up a single read/write for
        /// device registers.
        /// </summary>
        public int Size
        {
            get;
            set;
        }

        public RegDef(int nameEnumValue, int barOffset, Type bfEnumType)
            : this(nameEnumValue, barOffset, bfEnumType, DEFAULT_REG_SIZE, null, string.Empty)
        {
        }
        public RegDef(int nameEnumValue, int barOffset, Type bfEnumType, int size, Type regImplementationType, string condition)
        {
            nameEnum = nameEnumValue;
            BARoffset = barOffset;
            BFenum = bfEnumType;            
            Size = size; // Most register types ignore this ...
        }
    }
}