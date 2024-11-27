using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keysight.ModularInstruments.Core.Register
{
    public class RegFactory
    {
        private readonly Int32 mBaseAddr = 0; // only used by feldspar registers.

        public IRegister[] CreateRegArray(ICollection<RegDef> regDefArray,
                                   Type regEnumType,
                                   string moduleName,
                                   string optionalMidName,
                                   object[] args)
        {
            int numRegsToCreate = Enum.GetNames(regEnumType).Length;

            // Scan the enums (register name enum's) to look for the largest enumerated value
            // then compare this against the length of the enum array.  This should prevent
            // index out of bounds exceptions if someone doesn't override one of the "base" 
            // enumerated values and then extends the list of enum values.
            int largestEnum = 0;
            foreach (RegDef regDef in regDefArray)
            {
                largestEnum = Math.Max(largestEnum, regDef.nameEnum);
            }

            numRegsToCreate = Math.Max(numRegsToCreate, largestEnum + 1);

            IRegister[] regArray = new IRegister[numRegsToCreate];

            // Now scan the register definition array and create each register and add them to the return array.
            foreach (RegDef regDef in regDefArray)
            {
                // Allow conditional inclusion/exclusion base on module flags
                //if (Evaluate(regDef.Condition))
                //{
                    string regName = NameCreator(regEnumType, regDef.nameEnum, moduleName, optionalMidName, false);

                    IRegister newReg = new Register(regName, regDef.BARoffset + mBaseAddr, regDef.BFenum/*, (IBitField)args[ 0 ],  (IBitField)args[ 1 ],(IBitField)args[ 2 ],(int)args[ 3 ],(int)args[ 4 ]*/);

                    regArray[regDef.nameEnum] = newReg;
                //}
            }

            return regArray;
        }
        public void CreateBitFields(ICollection<BitFieldDef> bitFieldDefArray,
                                     IRegister[] containingRegArray,
                                     string moduleName)
        {
            foreach (BitFieldDef bfd in bitFieldDefArray)
            {
                // Allow conditional inclusion/exclusion base on module flags
                //if (Evaluate(bfd.Condition))
                {
                    // Two cases:
                    //   bfd.BFType == null: create a BitField for the register identified by regEnum
                    //   bfd.BFType != null: create a BitField for ALL registers with IRegister.BFType == bfd.BFType
                    if (bfd.BFType == null)
                    {
                        IRegister containingReg = containingRegArray[bfd.RegEnum];
                        if (containingReg != null)
                        {
                            IBitField newBitField = CreateBitField(bfd, containingReg);
                            containingReg.AddField(newBitField); // add newly created BitField to it's reg.
                        }
                    }
                    else
                    {
                        foreach (IRegister containingReg in containingRegArray)
                        {
                            if (containingReg != null && ReferenceEquals(containingReg.BFType, bfd.BFType))
                            {
                                IBitField newBitField = CreateBitField(bfd, containingReg);
                                containingReg.AddField(newBitField); // add newly created BitField to it's reg.
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Creates a BitField and inserts it into the containing register.
        /// </summary>
        /// <param name="bfd">structure defining the BitField.</param>
        /// <param name="containingReg">Register this BitField belongs to.</param>
        /// <returns>The created BitField</returns>
        private IBitField CreateBitField(BitFieldDef bfd,
                                          IRegister containingReg)
        {
            IBitField newBitField = null;

            // create the bit field name. 
            string bitFieldName = NameCreator(containingReg.BFType,
                                               bfd.nameEnum,
                                               string.Empty,
                                               containingReg.Name,
                                               true);

            //if (ConstructField != null)
            //{
            //    newBitField = ConstructField(bitFieldName,
            //                                  bfd.nameEnum,
            //                                  bfd.startBit,
            //                                  bfd.width,
            //                                  containingReg,
            //                                  bfd.Args);
            //}
            //else if (containingReg.SizeInBytes == sizeof(long))
            if (containingReg.SizeInBytes == sizeof(long))
            {
                //if (containingReg.IsCommand || containingReg.IsEvent)
                //{
                //    // Command/event registers get command/event bits fields (changes how Value/Apply and Write work)
                //    newBitField = new CommandField64(bitFieldName,
                //                                      bfd.startBit,
                //                                      bfd.width,
                //                                      containingReg);
                //}
                //else
                //{
                //    // Non-command/event registers get "normal" bit fields
                //    newBitField = new RegField64(bitFieldName,
                //                                  bfd.startBit,
                //                                  bfd.width,
                //                                  containingReg);
                //}
            }
            else if (containingReg.SizeInBytes == sizeof(int))
            {
                //if (containingReg.IsCommand || containingReg.IsEvent)
                //{
                //    // Command/event registers get command/event bits fields (changes how Value/Apply and Write work)
                //    newBitField = new CommandField32(bitFieldName,
                //                                      bfd.startBit,
                //                                      bfd.width,
                //                                      containingReg);
                //}
                //else
                {
                    // Non-command/event registers get "normal" bit fields
                    newBitField = new RegField(bitFieldName,
                                                  bfd.startBit,
                                                  bfd.width,
                                                  containingReg);
                }
            }
            else
            {
                throw new ApplicationException("Invalid Reg Array Type");
            }

            return newBitField;
        }
        public static string NameCreator(Type itemEnumType,
                                          int itemEnumVal,
                                          string moduleName,
                                          string middleName,
                                          Boolean bIsBitField)
        {
            string itemName = Enum.GetName(itemEnumType, itemEnumVal);

            return NameCreator(itemName, moduleName, middleName, bIsBitField);
        }
        public static string NameCreator(string itemName,
                                         string moduleName,
                                         string middleName,
                                         Boolean bIsBitField)
        {
            string fullName = string.Empty;

            if (moduleName != string.Empty)
            {
                fullName = moduleName + "~";
            }

            if (middleName != string.Empty)
            {
                if (bIsBitField)
                {
                    fullName += middleName + ":";
                }
                else
                {
                    fullName += middleName + "~";
                }
            }

            fullName += itemName;

            return fullName;
        }
    }
}
