/******************************************************************************
 *                                                                         
 *               Copyright 2011-2015 Keysight Technologies, Inc.
 *               All rights reserved.
 *
 *****************************************************************************/

using System;

namespace Keysight.ModularInstruments.Core.Register
{
    /// <summary>
    /// BitFieldDef is a struct that uniquely defines a BitField from every other
    /// BitField.  Its used to create arrays of BitFieldDefs that are parsed
    /// by the BitFieldCreator to create BitFields that are used to 
    /// modify bits in hardware registers, thereby controlling hardware behavior. 
    /// Arrays of BitFieldDef's provide a quick and simple way to create BitFields
    /// with very minimal coding.  Therefore they provide an easy way to create and maintain
    /// BitFields.
    /// </summary>
    public class BitFieldDef
    {
        public int nameEnum;

        public int width;
        public int startBit;
        /// <summary>
        /// Int value of the Enum of the register that contains this bitfield.
        /// This is used as an index into an array of registers so at to identify
        /// which register this BitField is part of. 
        /// </summary>
        public int RegEnum
        {
            get;
            protected set;
        }
        public Type BFType
        {
            get;
            set;
        }
        public BitFieldDef(int regEnum, int bitFieldEnum, int startBit, int endBit)
        {
            RegEnum = regEnum;
            nameEnum = bitFieldEnum;
            width = endBit - startBit + 1;
            this.startBit = startBit;
        }       
        public BitFieldDef(Type bitFieldType, int bitFieldEnum, int startBit, int endBit)
        {
            BFType = bitFieldType;

            nameEnum = bitFieldEnum;
            width = endBit - startBit + 1;
            this.startBit = startBit;
        }
        public BitFieldDef(Type bitFieldType, int bitFieldEnum, int bit):
            this(bitFieldType,bitFieldEnum,bit,bit)
        {

        }
        public BitFieldDef(int regEnum, int bitFieldEnum, int bit) :
            this(regEnum, bitFieldEnum, bit, bit)
        {
        }         
    }
}