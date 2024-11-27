using System;

namespace Keysight.ModularInstruments.Core.Register
{
    /// <summary>
    /// Public interface for register operations.  Not all methods/properties must be implemented
    /// for a given implementation (e.g. Buffer32 does not implement Value32) 
    /// </summary>
    public interface IRegister
    {
        /// <summary>
        /// Complete name of the register.   Some implementations prepend the module name and identifier
        /// so the register can be identified in a multi-module system.
        /// </summary>
        string Name
        {
            get;
        }

        /// <summary>
        /// The base name of the register (i.e. the register name not qualified by the module name and
        /// identifier)
        /// </summary>
        string NameBase
        {
            get;
        }     
        /// <summary>
        /// The interpretation of Offset depends on the type of register...
        /// 
        /// For memory mapped registers, Offset is the offset from the BAR (Base Address Register) and
        /// is one of the VISA I/O arguments  (e.g.  viOut( BAR, Offset, Data ))
        /// 
        /// For simple device registers, Offset is usually the address of the register within the device.
        /// </summary>
        Int32 Offset
        {
            get;
        }

        /// <summary>
        /// The size, in bytes, of the register data.
        /// </summary>
        int SizeInBytes
        {
            get;
        }

        /// <summary>
        /// Set or get the software copy of the registers value.
        /// No hardware access will take place.
        /// If this is called on a 64 bit register, the implementation may
        /// either throw an exception or
        ///   get: truncate the value to 32 bits.
        ///   set: zero pad the value to 64 bits.
        /// </summary>
        int Value32
        {
            get;
            set;
        }

        void Apply(bool ForceApply);

        void Write(int value);

        int Read();
        /// <summary>
        /// Set or get the software copy of the registers value.
        /// No hardware access will take place.
        /// If this is called on a 32 bit register, the implementation may
        /// either throw an exception or
        ///   get: zero pad the value to 64 bits.
        ///   set: truncate the value to 32 bits.
        /// </summary>        
        #region BitField support

        /// <summary>
        /// Returns the type (normally an Enum) of the bit field identifiers.  If this register
        /// does not support fields, returns null
        /// </summary>
        Type BFType
        {
            get;
        }

        /// <summary>
        /// Return the index of the first BitField.  -1 if there are no BitFields defined.
        /// 
        /// BitFields are internally stored in an array.  "empty" slots contain a null and "non-empty" slots
        /// contain a BitField.  FirstBF is the index of the first non-empty slot.
        /// </summary>
        int FirstBF
        {
            get;
        }

        /// <summary>
        /// Return the index of the last BitField. -1 if there are no BitFields defined.
        /// 
        /// BitFields are internally stored in an array.  "empty" slots contain a null and "non-empty" slots
        /// contain a BitField.  LastBF is the index of the last non-empty slot.
        /// </summary>
        int LastBF
        {
            get;
        }

        /// <summary>
        /// Return the number of BitFields
        /// 
        /// BitFields are internally stored in an array.  "empty" slots contain a null and "non-empty" slots
        /// contain a BitField.  NumBitFields return (LastBF-FirstBF-1) but it is possible for some of the
        /// entries between these values to be null.
        /// </summary>
        int NumBitFields
        {
            get;
        }

        /// <summary>
        /// Returns a reference to the BitField specified by index i.
        /// 
        /// Some implementations also provide one of the following methods (not part of this interface)
        ///     RegField32 Field( uint );
        ///     RegField64 Field( uint );
        /// </summary>
        IBitField GetField(uint i);

        /// <summary>
        /// Adds a Bit Field to a Register
        /// </summary>
        /// <param name="bf">The BitField being added to the register</param>
        /// <remarks>The BitField passed in must be an implementation of the Type.BFType
        /// passed in on construction of the register.  If not, an exception is thrown.</remarks>
        void AddField(IBitField bf);

        /// <summary>
        /// For diagnostic purposes -- normally only called in debug/development
        /// 
        /// Iterate over the BitField array and if there are any gaps (i.e. undefined fields)
        /// display a debug message.
        /// 
        /// This may happen if either
        /// 1) The BitField enumeration (BFType) does not assign contiguous values
        /// 2) Not all bit field enumerations are used
        /// </summary>
        /// <remarks> Will print a debug console message if a bitfield is not defined.</remarks>
        

        /// <summary>
        /// Fields exposes the internal collection of BitFields ... This is intended ONLY
        /// for use by RegFactory to dynamically create registers.
        /// </summary>
        IBitField[] Fields
        {
            get;
            set;
        }

        #endregion BitField support

        #region performance enhancements

        /// <summary>
        /// Helper property to increase performance by caching (mRegType & RegType.Cmd) != 0
        /// </summary>
        bool IsCommand
        {
            get;
        }

        /// <summary>
        /// Helper property to increase performance by caching (mRegType & RegType.Event) != 0
        /// </summary>       

        /// <summary>
        /// Helper property to increase performance by caching (mRegType & RegType.WO) != 0
        /// </summary>
        

        /// <summary>
        /// Helper property to increase performance by caching (mRegType & RegType.RO) != 0
        /// </summary>
       

        /// <summary>
        /// Helper property to increase performance by caching (mRegType & RegType.VolatileRw) != 0
        /// </summary>
        

        /// <summary>
        /// Helper property to increase performance by caching (mRegType & RegType.NoForce) != 0
        /// </summary>
       

        /// <summary>
        /// Helper property to increase performance by caching (mRegType & RegType.NoForce) == 0
        /// </summary>
        

        /// <summary>
        /// Helper property to increase performance by caching (mRegType & RegType.NoValue) != 0
        /// </summary>
        

        /// <summary>
        /// Helper property to increase performance by caching (mRegType & RegType.NoValueFilter) != 0
        /// </summary>
        

        /// <summary>
        /// Helper property to increase performance by caching (mRegType & RegType.Feldspar) != 0
        /// </summary>
        

        #endregion performance enhancements
    }   
}