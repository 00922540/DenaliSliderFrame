using System;
using System.Collections.Generic;
using Keysight.ModularInstruments.Core.Register;

namespace Keysight.ModularInstruments.KtM9347.Registers
{
    #region Register Names

    // NOTE: normally we prefer this enumeration being inside the class and accessed
    // NOTE: similar to:   CommonPluginRegisterSet.RegisterNames, but due to
    // NOTE: the evolutionary history of TLO code we need this as a public enum

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum WyvernBF
    {
        Address,   //31...19
        Parity,    //18
        Increment, //17
        RW,        //16
        Value      //15...0
    }

    /// <summary>
    /// Enumerate the slug (non-carrier) registers unique to this module.
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true)]
    public enum WyvernReg
    {
        #region Digital

        #region General

        /// <summary>
        /// Can insert up to 4 DataMarks with shared time offsets in the datapath
        /// </summary>
        Mark,               // 0x0004 

        /// <summary>
        /// Bits in this register indicate various exception conditions
        /// </summary>
        Status0,            // 0x0007

        /// <summary>
        /// Bits in this register indicate that data has clipped at some place in the datapath
        /// </summary>
        Status1,            // 0x0008

        /// <summary>
        /// Turns off the clock for the selected modules
        /// </summary>
        CClkGate,           // 0x0010

        #endregion

        #region MarkPropagation

        /// <summary>
        /// Configures DataMark generator
        /// </summary>
        InMarkCfg,          // 0x0028

        /// <summary>
        /// Configures OutMark generator
        /// </summary>
        OutMarkCfg0,        // 0x0033

        /// <summary>
        /// Configures OutMark generator
        /// </summary>
        OutMarkCfg1,        // 0x0034

        /// <summary>
        /// Configures OutMark generator
        /// </summary>
        OutMarkCfg2,        // 0x0035

        /// <summary>
        /// Configures OutMark generator
        /// </summary>
        OutMarkCfg3,        // 0x0036

        #endregion

        #region GNco

        /// <summary>
        /// Configures GNCO
        /// </summary>
        GNcoCfg,            // 0x0058

        /// <summary>
        /// Contains LSBs of GNCO phase bump value.  Must also write GNcoDFU to take effect.
        /// </summary>
        GNcoDFL,            // 0x005e - Group 4

        /// <summary>
        /// Contains MSBs of GNCO phase bump value.
        /// </summary>
        GNcoDFU,            // 0x005f - Group 4 - Must write this register to take effect

        /// <summary>
        /// Contains LSBs of GNCO global phase offset value.  Must also write GNcoPhaseOffsetU to take effect.
        /// </summary>
        GNcoPhaseOffsetL,   // 0x0060 - Group 5

        /// <summary>
        /// Contains MSBs of GNCO global phase offset value
        /// </summary>
        GNcoPhaseOffsetU,   // 0x0061 - Group 5 - Must write this register to take effect

        /// <summary>
        /// Contains GNCO scale value
        /// </summary>
        GNcoScale,          // 0x0062

        /// <summary>
        /// Contains bits[15:0] of 80-bit GNCO frequency.  Must also write GNcoFreq_4 to take effect.
        /// </summary>
        GNcoFreq_0,         // 0x0096 - Group 7

        /// <summary>
        /// Contains bits[31:16] of 80-bit GNCO frequency.  Must also write GNcoFreq_4 to take effect.
        /// </summary>
        GNcoFreq_1,         // 0x0097 - Group 7

        /// <summary>
        /// Contains bits[47:32] of 80-bit GNCO frequency.  Must also write GNcoFreq_4 to take effect.
        /// </summary>
        GNcoFreq_2,         // 0x0098 - Group 7

        /// <summary>
        /// Contains bits[63:48] of 80-bit GNCO frequency.  Must also write GNcoFreq_4 to take effect.
        /// </summary>
        GNcoFreq_3,         // 0x0099 - Group 7

        /// <summary>
        /// Contains bits[79:64] of 80-bit GNCO frequency.
        /// </summary>
        GNcoFreq_4,         // 0x009a - Group 7 - Must write this register to take effect

        /// <summary>
        /// Controls which DataMark is used to control a GNCO related action
        /// </summary>
        GNcoMarkSel,        // 0x009c

        /// <summary>
        /// Controls how the selected DataMark controls a GNCO related action
        /// </summary>
        GNcoMarkCfg,        // 0x00a2

        #endregion

        #region SignalGen

        /// <summary>
        /// Configures scaling.Same group as Scale. Both need to be set together
        /// </summary>
        ScaleCfg,           // 0x03a4 - Group 136

        /// <summary>
        /// Contains scale value.  Same group as ScaleCfg. Both need to be set together
        /// </summary>
        Scale,              // 0x03a5 - Group 136 - Must write this register to take effect

        #endregion

        #region DdrInterp

        /// <summary>
        /// Configures DDR interpolator
        /// </summary>
        DdrInterpCfg,       // 0x0406

        #endregion

        #region Dem (Dynamic element matching)

        /// <summary>
        /// Configures dynamic element matching
        /// </summary>
        DemCfg,             // 0x041a

        #endregion

        #region FinalMuxIn

        /// <summary>
        /// Configures the input to the final mux
        /// </summary>
        FinalMuxInCfg,      // 0x042e

        /// <summary>
        /// Configures random DataMark generator which is the source for the DllClk generator
        /// </summary>
        DllClkCreateMark,   // 0x04ef 

        /// <summary>
        /// Configures DllClk generator
        /// </summary>
        DllClkCfg,          // 0x04f0 

        /// <summary>
        /// Controls length of pulse in pulse mode
        /// </summary>
        DllClkWidth,        // 0x04f3 

        #endregion

        #region FinalMux

        /// <summary>
        /// Final mux config
        /// </summary>
        FinalMuxCfg,        // 0x0800 

        /// <summary>
        /// Digital clock system config
        /// </summary>
        ClkdCfg,            // 0x0801

        /// <summary>
        /// mux clock timing DAC config
        /// </summary>
        MuxClkDelayCfg,     // 0x0802

        /// <summary>
        /// mux clock delay control, coarse lower bits
        /// </summary>
        MuxClkDelayCoarseL,

        /// <summary>
        /// mux clock delay control, coarse upper bits
        /// </summary>
        MuxClkDelayCoarseU,

        /// <summary>
        /// mux clock delay control, fineA bits
        /// </summary>
        MuxClkDelayFineA,

        /// <summary>
        /// mux clock delay control, fineB bits
        /// </summary>
        MuxClkDelayFineB,

        /// <summary>
        /// mux clock delay control, tweak bits
        /// </summary>
        MuxClkDelayTweak,

        /// <summary>
        /// Enable for custom LVDS IO
        /// </summary>
        CustomLvdsEnable,   // 0x080b

        /// <summary>
        /// Force custom LVDS outputs
        /// </summary>
        CustomLvdsForce,    // 0x080c

        /// <summary>
        /// Isolation-crossing DLL loop filter config
        /// </summary>
        IsoLoopFilterCfg1,  // 0x080e 

        /// <summary>
        /// Isolation-crossing DLL loop filter config
        /// </summary>
        IsoLoopFilterCfg2,  // 0x080f 

        /// <summary>
        /// Isolation-crossing DLL loop filter status outputs
        /// </summary>
        IsoLoopFilterStat1, // 0x0810

        /// <summary>
        /// Isolation-crossing DLL loop filter status outputs
        /// </summary>
        IsoLoopFilterStat2, // 0x0811

        /// <summary>
        /// Request loop filter status
        /// </summary>
        IsoLoopFilterStatReq, // 0x0812

        /// <summary>
        /// Acknowledge for loop filter status
        /// </summary>
        IsoLoopFilterStatAck, // 0x0813

        #endregion

        #endregion

        #region Analog

        #region DacCfg

        /// <summary>
        /// Dac configuration: miscellaneous
        /// </summary>
        DacCfg1,            // 0x1000

        /// <summary>
        /// Dac configuration: CML NMOS bias current
        /// </summary>
        DacCfg2,            // 0x1001

        /// <summary>
        /// Dac configuration: CML bias modes
        /// </summary>
        DacCfg3,            // 0x1002 

        /// <summary>
        /// Dac configuration: miscellaneous
        /// </summary>
        DacCfg4,            // 0x1003 

        /// <summary>
        /// Dac configuration: CML PMOS bias current
        /// </summary>
        DacCfg5,            // 0x1004 

        /// <summary>
        /// Dac configuration: CML swing adjust
        /// </summary>
        DacCfg6,            // 0x1005 

        #endregion

        #region DacAnaClkSys

        ClkaCfg,            // 0x10e9
        LClkDelayCfg,       // 0x10ea
        LClkDelayCoarseL,   // 0x10eb
        LClkDelayCoarseU,   // 0x10ec
        LClkDelayFineA,     // 0x10ed
        LClkDelayFineB,     // 0x10ee
        LClkDelayTweak,     // 0x10ef
        GlobalDelayCfg,     // 0x10f0
        GlobalDelayCoarseL, // 0x10f1
        GlobalDelayCoarseU, // 0x10f2
        GlobalDelayFineA,   // 0x10f3
        GlobalDelayFineB,   // 0x10f4
        GlobalDelayTweak,   // 0x10f5
        RsLoopFilterCfg1,   // 0x10f8 
        RsLoopFilterCfg2,   // 0x10f9 
        RsLoopFilterStat1,  // 0x10fa
        RsLoopFilterStat2,  // 0x10fb
        RsLoopFilterStatReq, // 0x10fc
        RsLoopFilterStatAck, // 0x10fd
        DllPhaseDetCfg10,   // 0x10fe 
        DllPhaseDetCfg11,   // 0x10ff 
        DllPhaseDetCfg20,   // 0x1100 
        DllPhaseDetCfg21,   // 0x1101 
        ClkaSpare,          // 0x1102
        #endregion

        #region CalRelated
        DacCurAdjMsbA0, // 0x1030 
        DacCurAdjMsbA1, // 0x1031 
        DacCurAdjMsbA2, // 0x1032 
        DacCurAdjMsbA3, // 0x1033 
        DacCurAdjMsbA4, // 0x1034 
        DacCurAdjMsbA5, // 0x1035 
        DacCurAdjMsbA6, // 0x1036 
        DacCurAdjMsbA7, // 0x1037 
        DacCurAdjMsbA8, // 0x1038 
        DacCurAdjMsbA9, // 0x1039 
        DacCurAdjMsbA10, // 0x103a 
        DacCurAdjMsbA11, // 0x103b 
        DacCurAdjMsbA12, // 0x103c 
        DacCurAdjMsbA13, // 0x103d 
        DacCurAdjMsbA14, // 0x103e 
        DacCurAdjMsbA15, // 0x103f 
        DacCurAdjPLsbA0, // 0x1026 
        DacCurAdjPLsbA1, // 0x1027 
        DacCurAdjPLsbA2, // 0x1028 
        DacCurAdjPLsbA3, // 0x1029 
        DacCurAdjPLsbA4, // 0x102a 
        DacCurAdjSLsbA0, // 0x102b 
        DacCurAdjSLsbA1, // 0x102c 
        DacCurAdjSLsbA2, // 0x102d 
        DacCurAdjSLsbA3, // 0x102e 
        DacCurAdjSLsbA4, // 0x102f 
        DacCurAdjMsbB0, // 0x108a 
        DacCurAdjMsbB1, // 0x108b 
        DacCurAdjMsbB2, // 0x108c 
        DacCurAdjMsbB3, // 0x108d 
        DacCurAdjMsbB4, // 0x108e 
        DacCurAdjMsbB5, // 0x108f 
        DacCurAdjMsbB6, // 0x1090 
        DacCurAdjMsbB7, // 0x1091 
        DacCurAdjMsbB8, // 0x1092 
        DacCurAdjMsbB9, // 0x1093 
        DacCurAdjMsbB10, // 0x1094 
        DacCurAdjMsbB11, // 0x1095 
        DacCurAdjMsbB12, // 0x1096 
        DacCurAdjMsbB13, // 0x1097 
        DacCurAdjMsbB14, // 0x1098 
        DacCurAdjMsbB15, // 0x1099 
        DacCurAdjPLsbB0, // 0x1080 
        DacCurAdjPLsbB1, // 0x1081 
        DacCurAdjPLsbB2, // 0x1082 
        DacCurAdjPLsbB3, // 0x1083 
        DacCurAdjPLsbB4, // 0x1084 
        DacCurAdjSLsbB0, // 0x1085 
        DacCurAdjSLsbB1, // 0x1086 
        DacCurAdjSLsbB2, // 0x1087 
        DacCurAdjSLsbB3, // 0x1088 
        DacCurAdjSLsbB4, // 0x1089
        SwDrvOsAdj0, 	//0x10c2
        SwDrvOsAdj1, 	//0x10c3
        SwDrvOsAdj2, 	//0x10c4
        SwDrvOsAdj3, 	//0x10c5
        SwDrvOsAdj4, 	//0x10c6
        SwDrvOsAdj5, 	//0x10c7
        SwDrvOsAdj6, 	//0x10c8
        SwDrvOsAdj7, 	//0x10c9
        SwDrvOsAdj8, 	//0x10ca
        SwDrvOsAdj9, 	//0x10cb
        SwDrvOsAdj10, 	//0x10cc
        SwDrvOsAdj11, 	//0x10cd
        SwDrvOsAdj12, 	//0x10ce
        SwDrvOsAdj13, 	//0x10cf
        SwDrvOsAdj14, 	//0x10d0
        SwDrvOsAdj15, 	//0x10d1
        SwDrvOsAdj16, 	//0x10d2
        SwDrvOsAdj17, 	//0x10d3
        SwDrvOsAdj18, 	//0x10d4
        SwDrvOsAdj19, 	//0x10d5
        SwDrvOsAdj20, 	//0x10d6
        SwDrvOsAdj21, 	//0x10d7
        SwDrvOsAdj22, 	//0x10d8
        SwDrvOsAdj23, 	//0x10d9
        SwDrvOsAdj24, 	//0x10da
        SwDrvOsAdj25, 	//0x10db
        SwDrvOsAdj26, 	//0x10dc
        SwDrvOsAdj27, 	//0x10dd
        SwDrvOsAdj28, 	//0x10de
        SwDrvOsAdj29, 	//0x10df
        SwDrvOsAdj30, 	//0x10e0
        SwDrvOsAdj31, 	//0x10e1
        SwDrvOsAdj32, 	//0x10e2
        SwDrvOsAdj33, 	//0x10e3
        SwDrvOsAdj34, 	//0x10e4
        SwDrvOsAdj35, 	//0x10e5
        SwDrvOsAdj36, 	//0x10e6
        SwDrvOsAdj37, 	//0x10e7
        SwDrvOsAdj38, 	//0x10e8		
        FinalMuxInP0, // 0x042f 
        FinalMuxInP1, // 0x0430 
        FinalMuxInP2, // 0x0431 
        FinalMuxInP3, // 0x0432 
        FinalMuxInP4, // 0x0433 
        FinalMuxInP5, // 0x0434 
        FinalMuxInP6, // 0x0435 
        FinalMuxInP7, // 0x0436 
        FinalMuxInP8, // 0x0437 
        FinalMuxInP9, // 0x0438 
        FinalMuxInP10, // 0x0439 
        FinalMuxInP11, // 0x043a 
        FinalMuxInP12, // 0x043b 
        FinalMuxInP13, // 0x043c 
        FinalMuxInP14, // 0x043d 
        FinalMuxInP15, // 0x043e 
        FinalMuxInP16, // 0x043f 
        FinalMuxInP17, // 0x0440 
        FinalMuxInP18, // 0x0441 
        FinalMuxInP19, // 0x0442 
        FinalMuxInP20, // 0x0443 
        FinalMuxInP21, // 0x0444 
        FinalMuxInP22, // 0x0445 
        FinalMuxInP23, // 0x0446 
        FinalMuxInP24, // 0x0447 
        FinalMuxInP25, // 0x0448 
        FinalMuxInP26, // 0x0449 
        FinalMuxInP27, // 0x044a 
        FinalMuxInP28, // 0x044b 
        FinalMuxInP29, // 0x044c 
        FinalMuxInP30, // 0x044d 
        FinalMuxInP31, // 0x044e 
        FinalMuxInP32, // 0x044f 
        FinalMuxInP33, // 0x0450 
        FinalMuxInP34, // 0x0451 
        FinalMuxInP35, // 0x0452 
        FinalMuxInP36, // 0x0453 
        FinalMuxInP37, // 0x0454 
        FinalMuxInP38, // 0x0455 
        FinalMuxInP39, // 0x0456 
        FinalMuxInP40, // 0x0457 
        FinalMuxInP41, // 0x0458 
        FinalMuxInP42, // 0x0459 
        FinalMuxInP43, // 0x045a 
        FinalMuxInP44, // 0x045b 
        FinalMuxInP45, // 0x045c 
        FinalMuxInP46, // 0x045d 
        FinalMuxInP47, // 0x045e 
        FinalMuxInP48, // 0x045f 
        FinalMuxInP49, // 0x0460 
        FinalMuxInP50, // 0x0461 
        FinalMuxInP51, // 0x0462 
        FinalMuxInP52, // 0x0463 
        FinalMuxInP53, // 0x0464 
        FinalMuxInP54, // 0x0465 
        FinalMuxInP55, // 0x0466 
        FinalMuxInP56, // 0x0467 
        FinalMuxInP57, // 0x0468 
        FinalMuxInP58, // 0x0469 
        FinalMuxInP59, // 0x046a 
        FinalMuxInP60, // 0x046b 
        FinalMuxInP61, // 0x046c 
        FinalMuxInP62, // 0x046d 
        FinalMuxInP63, // 0x046e 
        FinalMuxInS0, // 0x046f 
        FinalMuxInS1, // 0x0470 
        FinalMuxInS2, // 0x0471 
        FinalMuxInS3, // 0x0472 
        FinalMuxInS4, // 0x0473 
        FinalMuxInS5, // 0x0474 
        FinalMuxInS6, // 0x0475 
        FinalMuxInS7, // 0x0476 
        FinalMuxInS8, // 0x0477 
        FinalMuxInS9, // 0x0478 
        FinalMuxInS10, // 0x0479 
        FinalMuxInS11, // 0x047a 
        FinalMuxInS12, // 0x047b 
        FinalMuxInS13, // 0x047c 
        FinalMuxInS14, // 0x047d 
        FinalMuxInS15, // 0x047e 
        FinalMuxInS16, // 0x047f 
        FinalMuxInS17, // 0x0480 
        FinalMuxInS18, // 0x0481 
        FinalMuxInS19, // 0x0482 
        FinalMuxInS20, // 0x0483 
        FinalMuxInS21, // 0x0484 
        FinalMuxInS22, // 0x0485 
        FinalMuxInS23, // 0x0486 
        FinalMuxInS24, // 0x0487 
        FinalMuxInS25, // 0x0488 
        FinalMuxInS26, // 0x0489 
        FinalMuxInS27, // 0x048a 
        FinalMuxInS28, // 0x048b 
        FinalMuxInS29, // 0x048c 
        FinalMuxInS30, // 0x048d 
        FinalMuxInS31, // 0x048e 
        FinalMuxInS32, // 0x048f 
        FinalMuxInS33, // 0x0490 
        FinalMuxInS34, // 0x0491 
        FinalMuxInS35, // 0x0492 
        FinalMuxInS36, // 0x0493 
        FinalMuxInS37, // 0x0494 
        FinalMuxInS38, // 0x0495 
        FinalMuxInS39, // 0x0496 
        FinalMuxInS40, // 0x0497 
        FinalMuxInS41, // 0x0498 
        FinalMuxInS42, // 0x0499 
        FinalMuxInS43, // 0x049a 
        FinalMuxInS44, // 0x049b 
        FinalMuxInS45, // 0x049c 
        FinalMuxInS46, // 0x049d 
        FinalMuxInS47, // 0x049e 
        FinalMuxInS48, // 0x049f 
        FinalMuxInS49, // 0x04a0 
        FinalMuxInS50, // 0x04a1 
        FinalMuxInS51, // 0x04a2 
        FinalMuxInS52, // 0x04a3 
        FinalMuxInS53, // 0x04a4 
        FinalMuxInS54, // 0x04a5 
        FinalMuxInS55, // 0x04a6 
        FinalMuxInS56, // 0x04a7 
        FinalMuxInS57, // 0x04a8 
        FinalMuxInS58, // 0x04a9 
        FinalMuxInS59, // 0x04aa 
        FinalMuxInS60, // 0x04ab 
        FinalMuxInS61, // 0x04ac 
        FinalMuxInS62, // 0x04ad 
        FinalMuxInS63, // 0x04ae 
        FinalMuxInM0, // 0x04af 
        FinalMuxInM1, // 0x04b0 
        FinalMuxInM2, // 0x04b1 
        FinalMuxInM3, // 0x04b2 
        FinalMuxInM4, // 0x04b3 
        FinalMuxInM5, // 0x04b4 
        FinalMuxInM6, // 0x04b5 
        FinalMuxInM7, // 0x04b6 
        FinalMuxInM8, // 0x04b7 
        FinalMuxInM9, // 0x04b8 
        FinalMuxInM10, // 0x04b9 
        FinalMuxInM11, // 0x04ba 
        FinalMuxInM12, // 0x04bb 
        FinalMuxInM13, // 0x04bc 
        FinalMuxInM14, // 0x04bd 
        FinalMuxInM15, // 0x04be 
        FinalMuxInM16, // 0x04bf 
        FinalMuxInM17, // 0x04c0 
        FinalMuxInM18, // 0x04c1 
        FinalMuxInM19, // 0x04c2 
        FinalMuxInM20, // 0x04c3 
        FinalMuxInM21, // 0x04c4 
        FinalMuxInM22, // 0x04c5 
        FinalMuxInM23, // 0x04c6 
        FinalMuxInM24, // 0x04c7 
        FinalMuxInM25, // 0x04c8 
        FinalMuxInM26, // 0x04c9 
        FinalMuxInM27, // 0x04ca 
        FinalMuxInM28, // 0x04cb 
        FinalMuxInM29, // 0x04cc 
        FinalMuxInM30, // 0x04cd 
        FinalMuxInM31, // 0x04ce 
        FinalMuxInM32, // 0x04cf 
        FinalMuxInM33, // 0x04d0 
        FinalMuxInM34, // 0x04d1 
        FinalMuxInM35, // 0x04d2 
        FinalMuxInM36, // 0x04d3 
        FinalMuxInM37, // 0x04d4 
        FinalMuxInM38, // 0x04d5 
        FinalMuxInM39, // 0x04d6 
        FinalMuxInM40, // 0x04d7 
        FinalMuxInM41, // 0x04d8 
        FinalMuxInM42, // 0x04d9 
        FinalMuxInM43, // 0x04da 
        FinalMuxInM44, // 0x04db 
        FinalMuxInM45, // 0x04dc 
        FinalMuxInM46, // 0x04dd 
        FinalMuxInM47, // 0x04de 
        FinalMuxInM48, // 0x04df 
        FinalMuxInM49, // 0x04e0 
        FinalMuxInM50, // 0x04e1 
        FinalMuxInM51, // 0x04e2 
        FinalMuxInM52, // 0x04e3 
        FinalMuxInM53, // 0x04e4 
        FinalMuxInM54, // 0x04e5 
        FinalMuxInM55, // 0x04e6 
        FinalMuxInM56, // 0x04e7 
        FinalMuxInM57, // 0x04e8 
        FinalMuxInM58, // 0x04e9 
        FinalMuxInM59, // 0x04ea 
        FinalMuxInM60, // 0x04eb 
        FinalMuxInM61, // 0x04ec 
        FinalMuxInM62, // 0x04ed 
        FinalMuxInM63, // 0x04ee 

        #endregion

        #endregion
    }

    #endregion

    #region BitField enums used for M9347PluginRegisterSet

    #region General

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum MarkBF
    {
        /// <summary>
        /// Mark event offset in MClk cycles
        /// </summary>
        MarkTimeOffsetA,     // Bits  0- 5

        /// <summary>
        /// Inserts a DataMark[0] at offset MarkTimeOffsetA
        /// </summary>
        Mark0,               // Bit   6

        /// <summary>
        /// Inserts a DataMark[1] at offset MarkTimeOffsetA
        /// </summary>
        Mark1,               // Bit   7

        /// <summary>
        /// Mark event offset in MClk cycles
        /// </summary>
        MarkTimeOffsetB,     // Bits  8-13

        /// <summary>
        /// Inserts a DataMark[2] at offset MarkTimeOffsetB
        /// </summary>
        Mark2,               // Bit  14

        /// <summary>
        /// Inserts a DataMark[3] at offset MarkTimeOffsetB
        /// </summary>
        Mark3,               // Bit  15 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum Status0BF
    {
        /// <summary>
        /// Midscale is asserted
        /// </summary>
        Midscale,

        /// <summary>
        /// High overvoltage on positive side of the differential output
        /// </summary>
        OverVoltAoutpHigh,

        /// <summary>
        /// Low overvoltage on positive side of the differential output
        /// </summary>
        OverVoltAoutpLow,

        /// <summary>
        /// High overvoltage on negative side of the differential output
        /// </summary>
        OverVoltAoutnHigh,

        /// <summary>
        /// Low overvoltage on negative side of the differential output
        /// </summary>
        OverVoltAoutnLow,

        /// <summary>
        /// One of the control ports tried to access non-existing address
        /// </summary>
        NonExistingAddr,

        /// <summary>
        /// Multiple control ports tried to access the same address
        /// </summary>
        CtrlCollision,

        /// <summary>
        /// SPI ports access collision
        /// </summary>
        SpiCollision,

        /// <summary>
        /// Multiple InMark channels tried to send a mark with a different time offset
        /// </summary>
        InMarkCollision,

        /// <summary>
        /// Multiple DataMark channels tried to send a mark with a different time offset
        /// </summary>
        DataMarkCollision,

        /// <summary>
        /// Control port tried to access waveform memory out of bounds
        /// </summary>
        WaveMemOutOfBounds,

        /// <summary>
        /// Attempt was made to write 0 to SwpSpan register in GNCO. This is not allowed.
        /// </summary>
        SwpSpanZero,

        /// <summary>
        /// SPI parity bit not as expected
        /// </summary>
        SpiParityError
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum Status1BF
    {
        /// <summary>
        /// GNco frequency has clipped
        /// </summary>
        GNcoFreqClip,

        /// <summary>
        /// GNco sweep accumulator has clipped
        /// </summary>
        GNcoSwpClip,

        /// <summary>
        /// SNco frequency has clipped
        /// </summary>
        SNcoFreqClip,

        /// <summary>
        /// SNco scale value has clipped in Mag/Phase mode
        /// </summary>
        SNcoSpareClip,

        /// <summary>
        /// Interpolator node has clipped
        /// </summary>
        InterpolatorClip,

        /// <summary>
        /// Modulator output has clipped
        /// </summary>
        ModulatorClip,

        /// <summary>
        /// Scale output has clipped
        /// </summary>
        ScaleClip,

        /// <summary>
        /// Flattening filter output has clipped
        /// </summary>
        FlatFilterClip,

        /// <summary>
        /// Nonlinear predistortion output has clipped
        /// </summary>
        NLinPredistClip,

        /// <summary>
        /// SHF output has clipped
        /// </summary>
        ShfClip,

        /// <summary>
        /// Offset output has clipped
        /// </summary>
        OffsetClip,

        /// <summary>
        /// DDR inpolator path B has clipped
        /// </summary>
        DDRInterpClipB,

        /// <summary>
        /// Csec has clipped
        /// </summary>
        CsecClip,

        /// <summary>
        /// AWGN has clipped
        /// </summary>
        AwgnClip
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum CClkGateBF
    {
        /// <summary>
        /// Clock gate for waveform memory
        /// </summary>
        WaveMem,        // Bit 0

        /// <summary>
        /// Clock gate for GNCO
        /// </summary>
        GNco,           // Bit 1

        /// <summary>
        /// Clock gate for SNCO
        /// </summary>
        SNco,           // Bit 2

        /// <summary>
        /// Clock gate for Interpolation stages. Can only be used if Interpolator is not used at all
        /// </summary>
        AGx2,           // Bit 3

        /// <summary>
        /// Clock gate for Interpolation stage. Can be used if not in 3.2x or 6.4x interpolation mode
        /// </summary>
        Bx08,           // Bit 4

        /// <summary>
        /// Clock gate for Interpolation stages. Can be used if not in 6.4x, 8x, 16x, 32x or 64x interpolation mode
        /// </summary>
        CDEFx2,         // Bit 5

        /// <summary>
        /// Clock gate for modulator
        /// </summary>
        Modulation,     // Bit 6

        /// <summary>
        /// Clock gate for flattening filter
        /// </summary>
        FlatFilter,     // Bit 7

        /// <summary>
        /// Clock gate for non linear predistortion module
        /// </summary>
        NLPD,           // Bit 8

        /// <summary>
        /// Clock gate for subharmonic filter
        /// </summary>
        Shf,            // Bit 9

        /// <summary>
        /// Clock gate for DDR interpolator
        /// </summary>
        DdrInterp,      // Bit 10

        /// <summary>
        /// Clock gate for DAC-B part of Offset and DEM
        /// </summary>
        EncodingB,      // Bit 11

        /// <summary>
        /// Clock gate for DAC-AB part of digital current source error corrector
        /// </summary>
        Csec,           // Bit 12

        /// <summary>
        /// Clock gate for AWGN
        /// </summary>
        Awgn,           // Bit 13

        /// <summary>
        /// Clock gate for DdrDuc
        /// </summary>
        DdrDuc,         // Bit 14

        /// <summary>
        /// Clock gate for Power Detector
        /// </summary>
        PowerDetector,  // Bit 15
    }

    #endregion

    #region MarkPropagation

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum InMarkCfgBF
    {
        /// <summary>
        /// Determines when DataMark[0] propagates<para />
        /// 00: no DataMark[0] propagates<para />
        /// 01: DataMark[0] propagates whenever there is a new Mark.Mark0 or Mark01.Mark0 register write<para />
        /// 10: DataMark[0] propagates whenever there is a rising edge on LvdsInMarkA<para />
        /// 11: DataMark[0] propagates whenever there is a rising edge on LvdsInMarkA or whenever there is new Mark.Mark0 or Mark01.Mark0 write
        /// </summary>
        Cfg0,    //0..1

        /// <summary>
        /// Determines when DataMark[1] propagates<para />
        /// 00: no DataMark[1] propagates<para />
        /// 01: DataMark[1] propagates whenever there is a new Mark.Mark1 or Mark01.Mark1 register write<para />
        /// 10: DataMark[1] propagates whenever there is a rising edge on LvdsInMarkA<para />
        /// 11: DataMark[1] propagates whenever there is a rising edge on LvdsInMarkA or whenever there is new Mark.Mark1 or Mark01.Mark1 write
        /// </summary>
        Cfg1,    //2..3

        /// <summary>
        /// Determines when DataMark[2] propagates<para />
        /// 00: no DataMark[2] propagates<para />
        /// 01: DataMark[2] propagates whenever there is a new Mark.Mark2 or Mark23.Mark2 register write<para />
        /// 10: DataMark[2] propagates whenever there is a rising edge on LvdsInMarkA<para />
        /// 11: DataMark[2] propagates whenever there is a rising edge on LvdsInMarkA or whenever there is new Mark.Mark2 or Mark23.Mark2 write
        /// </summary>
        Cfg2,    //4..5

        /// <summary>
        /// Determines when DataMark[3] propagates<para />
        /// 00: no DataMark[3] propagates<para />
        /// 01: DataMark[3] propagates whenever there is a new Mark.Mark3 or Mark23.Mark3 register write<para />
        /// 10: DataMark[3] propagates whenever there is a rising edge on LvdsInMarkA<para />
        /// 11: DataMark[3] propagates whenever there is a rising edge on LvdsInMarkA or whenever there is new Mark.Mark3 or Mark23.Mark3 write
        /// </summary>
        Cfg3,    //6..7

        /// <summary>
        /// Determines when DataMark[0] propagates<para />
        /// 0: no JESD DataMark[0] propagates<para />
        /// 1: JESD DataMark[0] propagates
        /// </summary>
        JesdMarkEn0, //8

        /// <summary>
        /// Determines when DataMark[1] propagates<para />
        /// 0: no JESD DataMark[1] propagates<para />
        /// 1: JESD DataMark[1] propagates
        /// </summary>
        JesdMarkEn1, //9

        /// <summary>
        /// Determines when DataMark[2] propagates<para />
        /// 0: no JESD DataMark[2] propagates<para />
        /// 1: JESD DataMark[2] propagates
        /// </summary>
        JesdMarkEn2, //10
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum OutMarkCfgBF
    {
        /// <summary>
        /// Selects source of the OutMark generator<para />
        /// 0 : DataMark[0]<para />
        /// 1 : DataMark[1]<para />
        /// 2 : DataMark[2]<para />
        /// 3 : DataMark[3]<para />
        /// 4 : DataMark[4]<para />
        /// 5 : DataMark[5]<para />
        /// 6 : constant '0'<para />
        /// 7 : constant '1'
        /// </summary>
        DataMarkSelect,     // Bits 0-2

        /// <summary>
        /// Selects the mode of the OutMark generator<para />
        /// 0: pulse mode<para />
        /// 1: toggle mode
        /// </summary>
        Mode,               // Bit 3

        /// <summary>
        /// Defines initial (and default) state of OutMark output
        /// </summary>
        State,              // Bit 4

        /// <summary>
        /// Sets the polarity of the generated OutMark<para />
        /// 0: 'OutMarkPattern' register or 'State' field not inverted<para />
        /// 1: 'OutMarkPattern' register or 'State' field inverted
        /// </summary>
        InvertOutput,       // Bit 5

        /// <summary>
        /// 0 - 511: Contains the delay value in MClk periods for the programmable delayline in the OutMark generator
        /// </summary>
        Delay               // Bits 6-15
    }

    #endregion

    #region Gnco

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum GNcoCfgBF
    {
        /// <summary>
        /// Enable for angle modulation<para />
        /// 0: disable angle modulation<para />
        /// 1: enable angle modulation
        /// </summary>
        AngModEn,       // Bit 0

        /// <summary>
        /// Selects modulation type<para />
        // 0: FM<para />
        // 1: PM
        /// </summary>
        AngModTyp,      // Bit 1

        /// <summary>
        /// Contains scale value for angle modulation data
        /// </summary>
        AngModScl,      // Bits 2-7

        /// <summary>
        /// Selects phase rounding method<para />
        // 0 random random rounding<para />
        // 1 fixed  fixed rounding
        /// </summary>
        PhaseRound,     // Bit 8

        /// <summary>
        /// Selects amplitude rounding method<para />
        /// 0 random random rounding<para />
        /// 1 fixed  fixed rounding
        /// </summary>
        AmpRound,       // Bit 9

        /// <summary>
        /// Enable for parabolic phase interpolation<para />
        /// 0: linear phase interpolation<para />
        /// 1: parabolic phase interpolation
        /// </summary>
        PhaseInterp,    // Bit 10

        /// <summary>
        /// Bypass for the GNCO output scaling<para />
        /// 0: scaling active<para />
        /// 1: scaling bypassed
        /// </summary>
        ScaleBypass,    // Bit 11

        /// <summary>
        /// Enable for phase modulation by AWGN module output<para />
        /// 0: disable phase modulation<para />
        /// 1: enable phase modulation
        /// </summary>
        AwgnModEn       // Bit 12
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum GNcoDFLBF
    {
        /// <summary>
        /// Contains LSBs of GNCO phase bump value
        /// </summary>
        DFL // Bits 0-15
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum GNcoDFUBF
    {
        /// <summary>
        /// Contains MSBs  of GNCO phase bump value
        /// </summary>
        DFU // Bits 0-15
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum GNcoPhaseOffsetBF
    {
        /// <summary>
        /// Contains lower or upper bits of GNCO global phase offset value
        /// </summary>
        PhaseOffset         // Bits 0-15
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum GNcoScaleBF
    {
        /// <summary>
        /// Contains GNCO scale value
        /// </summary>
        Scale,
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum GNcoFreqBF
    {
        /// <summary>
        /// Contains 16 bits of 80-bit GNCO frequency.
        /// </summary>
        FreqBits,
    }

    /// <summary>
    /// Each bitfield is 2 bits wide.<para />
    /// 0 mark_0 DataMark[0]<para />
    /// 1 mark_1 DataMark[1]<para />
    /// 2 mark_2 DataMark[2]<para />
    /// 3 mark_3 DataMark[3]<para />
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum GNcoMarkSelBF
    {
        /// <summary>
        /// Controls which DataMark is used to control phase accumulator reset
        /// </summary>
        RstPA,

        /// <summary>
        /// Controls which DataMark is used to control frequency updates
        /// </summary>
        Freq,

        /// <summary>
        /// Controls which DataMark is used to control phase bump updates
        /// </summary>
        DF,

        /// <summary>
        /// Controls which DataMark is used to control phase offset updates
        /// </summary>
        PhaseOffset,

        /// <summary>
        /// Controls which DataMark is used to control angle modulation enable
        /// </summary>
        AngModEn,

        /// <summary>
        /// Controls which DataMark is used to control angle modulation data updates
        /// </summary>
        AngModDat,

        /// <summary>
        /// Controls which DataMark is used to control scale updates
        /// </summary>
        Scale,

        /// <summary>
        /// Controls which DataMark is used to control AWGN modulation enable
        /// </summary>
        AwgnModEn
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum GNcoMarkCfgBF
    {
        /// <summary>
        /// Controls if phase accumulator resets occur immediately or when selected DataMark arrives<para />
        /// 0: phase accumulator resets occur when selected DataMark arrives<para />
        /// 1: phase accumulator resets occur immediately
        /// </summary>
        ActOnNewRstPA,

        /// <summary>
        /// Controls if phase accumulator resets occur on all selected DataMarks or only on armed selected DataMarks<para />
        /// 0: phase accumulator resets occur only on armed selected DataMarks<para />
        /// 1: phase accumulator resets occur on all selected DataMarks
        /// </summary>
        ActOnAllSelectedMarksRstPA,

        /// <summary>
        /// Controls if frequency updates occur on GNcoFreq_4 write or when selected DataMark arrives<para />
        /// 0: updates occur when selected DataMark arrives<para />
        /// 1: updates occur on GNcoFreq_4 write
        /// </summary>
        ActOnNewFreq,

        /// <summary>
        /// Controls if frequency updates occur on all selected DataMarks or only on armed selected DataMarks<para />
        /// 0: updates occur only on armed selected DataMarks<para />
        /// 1: updates occur on all selected DataMarks
        /// </summary>
        ActOnAllSelectedMarksFreq,

        /// <summary>
        /// Controls if phase bump occurs on GNcoDFU write or when selected DataMark arrives<para />
        /// 0: phase bump occurs when selected DataMark arrives<para />
        /// 1: phase bump occurs on GNcoDFU write
        /// </summary>
        ActOnNewDF,

        /// <summary>
        /// Controls if phase bump occurs on all selected DataMarks or only on armed selected DataMarks<para />
        /// 0: phase bump occurs only on armed selected DataMarks<para />
        /// 1: phase bump occurs on all selected DataMarks
        /// </summary>
        ActOnAllSelectedMarksDF,

        /// <summary>
        /// Controls if phase offset updates occur on GNcoPhaseOffsetU write or when selected DataMark arrives<para />
        /// 0: updates occur when selected DataMark arrives<para />
        /// 1: updates occur on GNcoPhaseOffsetU write
        /// </summary>
        ActOnNewPhaseOffset,

        /// <summary>
        /// Controls if phase offset updates occur on all selected DataMarks or only on armed selected DataMarks<para />
        /// 0: updates occur only on armed selected DataMarks<para />
        /// 1: updates occur on all selected DataMarks
        /// </summary>
        ActOnAllSelectedMarksPhaseOffset,

        /// <summary>
        /// Controls if angle modulation enable updates occur immediately or when selected DataMark arrives<para />
        /// 0: updates occur when selected DataMark arrives<para />
        /// 1: updates occur immediately
        /// </summary>
        ActOnNewAngModEn,

        /// <summary>
        /// Controls if angle modulation enable updates occur on all selected DataMarks or only on armed selected DataMarks<para />
        /// 0: updates occur only on armed selected DataMarks<para />
        /// 1: updates occur on all selected DataMarks
        /// </summary>
        ActOnAllSelectedMarksAngModEn,

        /// <summary>
        /// Controls if angle modulation data updates occur immediately or when selected DataMark arrives<para />
        /// 0: updates occur when selected DataMark arrives<para />
        /// 1: updates occur immediately
        /// </summary>
        ActOnNewAngModDat,

        /// <summary>
        /// Controls if angle modulation data updates occur on all selected DataMarks or only on armed selected DataMarks<para />
        /// 0: updates occur only on armed selected DataMarks<para />
        /// 1: updates occur on all selected DataMarks
        /// </summary>
        ActOnAllSelectedMarksAngModDat,

        /// <summary>
        /// Controls if scale updates occur immediately or when selected DataMark arrives<para />
        /// 0: updates occur when selected DataMark arrives<para />
        /// 1: updates occur immediately
        /// </summary>
        ActOnNewScale,

        /// <summary>
        /// Controls if scale updates occur on all selected DataMarks or only on armed selected DataMarks<para />
        /// 0: updates occur only on armed selected DataMarks<para />
        /// 1: updates occur on all selected DataMarks
        /// </summary>
        ActOnAllSelectedMarksScale,

        /// <summary>
        /// Controls if scale updates occur immediately or when selected DataMark arrives<para />
        /// 0: updates occur when selected DataMark arrives<para />
        /// 1: updates occur immediately
        /// </summary>
        ActOnNewAwgnModEn,

        /// <summary>
        /// Controls if scale updates occur on all selected DataMarks or only on armed selected DataMarks<para />
        /// 0: updates occur only on armed selected DataMarks<para />
        /// 1: updates occur on all selected DataMarks
        /// </summary>
        ActOnAllSelectedMarksAwgnModEn
    }

    #endregion

    #region SignalGen

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ScaleCfgBF
    {
        /// <summary>
        /// Causes a scale by 2^(Exponent-10), 0x0b is a scale of 2
        /// </summary>
        Exponent,

        /// <summary>
        /// Selects rounding method<para />
        /// 0 random random rounding<para />
        /// 1 fixed  fixed rounding
        /// </summary>
        Round,
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ScaleBF
    {
        /// <summary>
        /// Contains scale value; 2's complement number;0x4000 is a scale of 0.5
        /// </summary>
        Scale,
    }

    #endregion

    #region DdrInterp

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DdrInterpCfgBF
    {
        /// <summary>
        /// Enables DDR interpolator<para />
        /// 0: DDR interpolator disabled<para />
        /// 1: DDR interpolator enabled
        /// </summary>
        Enable,         // Bit 0

        /// <summary>
        /// Enables fixed rounding in the DDR interpolator<para />
        /// 0 random random rounding<para />
        /// 1 fixed  fixed rounding
        /// </summary>
        Round,          // Bit 1

        /// <summary>
        /// Enables Doublet mode in the DDR interpolator<para />
        /// 0 : Normal mode<para />
        /// 1 : -DACB doublet mode<para />
        /// 2, 3: DACB = 0 RZ mode
        /// </summary>
        DoubletMode,    // Bit 2
    }

    #endregion

    #region Dem

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DemCfgBF
    {
        /// <summary>
        /// Enable for permuting<para />
        /// 0: Permuting disabled<para />
        /// 1: Permuting enabled
        /// </summary>
        Enable,         // Bit 0

        /// <summary>
        /// Determines if DEM is in DDR or non-DDR mode<para />
        /// 0: DEM operates on 32 phases (non-DDR)<para />
        /// 1: DEM operates on 64 phases (DDR)
        /// </summary>
        DoDdr,          // Bit 1

        /// <summary>
        /// Contains the number of restricted lower bits out of the PRNG
        /// </summary>
        DemLsbClip      // Bits 2-5
    }

    #endregion

    #region FinalMuxIn

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum FinalMuxInCfgBF
    {
        /// <summary>
        /// Selects input data for the final mux<para />
        /// 0: Constant mid-scale values<para />
        /// 1: FinalMuxIn* registers<para />
        /// 2: Normal mode of operation<para />
        /// 3: Normal mode of operation
        /// </summary>
        Source,           //0..1 0: constant mid-scale values 1: FinalMuxIn* registers 2-3: Normal mode

        /// <summary>
        /// Enables midscale forcing by over-voltage detector<para />
        /// 0: force disabled<para />
        /// 1: force enabled
        /// </summary>
        OverForceEnable,
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DllClkCreateMarkBF
    {
        /// <summary>
        /// Enables/disables the generation of random DataMarks<para />
        /// 0: Random DataMark generation disabled<para />
        /// 1: Random DataMark generation enabled
        /// </summary>
        Enable,              // Bit   0

        /// <summary>
        /// Sets the minimum number of CClk cycles between DataMarks
        /// </summary>
        cfgMin,              // Bits  1- 6

        /// <summary>
        /// Sets the maximum number of CClk cycles between DataMarks
        /// </summary>
        cfgMask,             // Bits  7-10 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DllClkCfgBF
    {
        /// <summary>
        /// Selects source of the DllClk generator<para />
        /// 0 : DataMark[0]<para />
        /// 1 : DataMark[1]<para />
        /// 2 : DataMark[2]<para />
        /// 3 : DataMark[3]<para />
        /// 4 : DataMark[4]<para />
        /// 5 : Random DataMark generator<para />
        /// 6 : constant '0'<para />
        /// 7 : constant '1'
        /// </summary>
        DataMarkSelect,       // Bits  0- 2

        /// <summary>
        /// Selects the mode of the DllClk generator<para />
        /// 0: pulse mode<para />
        /// 1: toggle mode
        /// </summary>
        Mode,                // Bit   3

        /// <summary>
        /// Defines initial state of DllClk output (= way to reset the state in toggle mode)
        /// </summary>
        State,               // Bit   4

        /// <summary>
        /// Sets the polarity of the generated DllClk<para />
        /// 0: 'DllClkPattern' register or 'State' field not inverted<para />
        /// 1: 'DllClkPattern' register or 'State' field inverted
        /// </summary>
        InvertOutput,        // Bit   5

        /// <summary>
        /// 0 - 511: Contains the delay value for the programmable delayline in the DllClk generator
        /// </summary>
        Delay,               // Bits  6-15 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DllClkWidthBF
    {
        /// <summary>
        /// Contains length of pulse in pulse mode (in MClk cycles). Exception: Value 0 corresponds to a pulse length of 2^16
        /// </summary>
        Width               // Bits 0-15
    }

    #endregion

    #region FinalMux

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum FinalMuxCfgBF
    {
        /// <summary>
        /// 0 = DAC-B mux lanes off<para />
        /// 1 = DAC-B mux lanes on
        /// </summary>
        DdrEn,               // Bit   0

        /// <summary>
        /// OutMarkEn[N] controls mux lane for OutMark[N]<para />
        /// 0 = OutMark[N] mux lane off<para />
        /// 1 = OutMark[N] mux lane on
        /// </summary>
        OutMarkEn,           // Bits  1- 4

        /// <summary>
        /// 0 = DllClk mux lane off<para />
        /// 1 = DllClk mux lane on
        /// </summary>
        DllClkEn,            // Bit   5

        /// <summary>
        /// 0 = Midscale pin latched<para />
        /// 1 = Midscale pin not latched
        /// </summary>
        MidScaleLatch,       // Bit   6

        /// <summary>
        /// 0 = Midscale pin enabled<para />
        /// 1 = Midscale pin disabled
        /// </summary>
        MidScaleOff,         // Bit   7

        /// <summary>
        /// 0 = normal operation<para />
        /// 1 = turn off all mux lanes (sleep)
        /// </summary>
        Powerdown,           // Bit   8

        /// <summary>
        /// 0 = TestClk mux lane off<para />
        /// 1 = TestClk mux lane on
        /// </summary>
        TestClkEn,           // Bit   9

        /// <summary>
        /// Select DTestBus sub-bus<para />
        /// 00 = 1.0V test bus<para />
        /// 01 = temperature diode test bus<para />
        /// 1X = 1.8V test bus
        /// </summary>
        DTestBusSel,         // Bits 10-11

        /// <summary>
        /// 1 = unselected sub-buses tied to GNDD<para />
        /// 0 = unselected sub-buses left floating
        /// </summary>
        DTestBusTiedDownOn,  // Bit  12
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkdCfgBF
    {
        /// <summary>
        /// FpgaClk divider counter control. Reset enables MClk/128 output.<para />
        /// 0 = counter off<para />
        /// N = divide by N+1
        /// </summary>
        FpgaClkCount,       // Bits 0-2

        /// <summary>
        /// FpgaClk divider mux control<para />
        /// 0 = bypass final divide-by-2 stage<para />
        /// 1 = enable final divide-by-2 stage
        /// </summary>
        FpgaClkMux,         // Bit 3

        /// <summary>
        /// mux clock gating for sleep/nap mode<para />
        /// 0 = normal opperation<para />
        /// 1 = mux clocks gated off
        /// </summary>
        MuxClkOff,          // Bit 4

        /// <summary>
        /// FpgaClk source<para />
        /// 0 = FpgaClk from FpgaClk divider<para />
        /// 1 = copy CClk to FpgaClk
        /// </summary>
        FpgaClkFromCClk,    // Bit 5

        /// <summary>
        /// enable feedback peaking in IClk receiver
        /// </summary>
        FbEnIClkRx,         // Bit 6

        /// <summary>
        /// enable feedback peaking in master divide-by-2
        /// </summary>
        FbEnDiv,            // Bit 7

        /// <summary>
        /// enable feedback peaking in muxClkDelay timing DAC
        /// </summary>
        FbEnMuxClkDelay,    // Bit 8

        /// <summary>
        /// CClk gating. Note: turning off CClk will lose XSync alignment.<para />
        /// 0 = CClk on<para />
        /// 1 = CClk off
        /// </summary>
        CClkOff,            // Bit 9

        /// <summary>
        /// Duty cycle correction for divide-by-2 clock used in final mux output stage.<para />
        /// 0 = duty cycle correction off<para />
        /// 1 = duty cycle correction on
        /// </summary>
        DutyEn,             // Bit 10

        /// <summary>
        /// Enable watchdog timer.<para />
        /// 0 = watchdog timer off<para />
        /// 1 = watchdog timer on
        /// </summary>
        WatchdogEn,         // Bit 11

        /// <summary>
        /// Force watchdog to timeout for testing.<para />
        /// 0 = normal operation<para />
        /// 1 = watchdog input clock gated off
        /// </summary>
        WatchdogTest,       // Bit 12

        /// <summary>
        /// enable feedback peaking protection by watchdog, 1=enabled
        /// </summary>
        WatchdogForceEn     // Bit 13
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkDelayCfgBF
    {
        /// <summary>
        /// detector gain for production test mode
        /// </summary>
        TestGain,

        /// <summary>
        /// enable for production test mode, 1=enabled
        /// </summary>
        TestMode
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkDelayCoarseLBF
    {
        /// <summary>
        /// lower bits of coarse thermometer code
        /// </summary>
        coarseL,
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkDelayCoarseUBF
    {
        coarseU,
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkDelayFineABF
    {
        /// <summary>
        /// fineA thermometer code
        /// </summary>
        fineA,
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkDelayFineBBF
    {
        /// <summary>
        /// fineB thermometer code
        /// </summary>
        fineB,
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkDelayTweakBF
    {
        /// <summary>
        /// tweak0 binary code
        /// </summary>
        tweak0,

        /// <summary>
        /// tweak1 binary code
        /// </summary>
        tweak1,
    }

    /// <summary>
    /// Enables custom LVDS IOs, disabling unused IOs reduces power consumption. Turning off LVDS outputs causes both sides to go to 1.2V. LVDS inputs have 100 Ohm diff termination that is independent of enable.
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum CustomLvdsEnableBF
    {
        OutMarkEn,
        FpgaClkEn,
        InSyncClkEn,
        InRefClkEn,
        OutRefClkEn,
        SyncDEn
    }

    /// <summary>
    /// Force custom LVDS outputs to a static value. Must be turned on in CustomLvdsEnable for forcing to work.
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum CustomLvdsForceBF
    {
        OutMarkForceMode,       // Bits 0-3
        OutMarkForceVal,        // Bits 4-7
        FpgaClkForceMode,       // Bit 8
        FpgaClkForceVal,        // Bit 9
        SyncDForceMode,         // Bit 10
        SyncDForceVal,          // Bit 11
        OutRefClkForceMode,     // Bit 12
        OutRefClkVal            // Bit 13
    }

    /// <summary>
    /// DLL loop filter configuration
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum LoopFilterCfg1BF
    {
        /// <summary>
        /// low pass filter pole at 1/(1-2^(-filtShift)). Valid range 4-12.
        /// </summary>
        filtShift,           // Bits  0- 5

        /// <summary>
        /// 1 = invert DllDetect to change the sign of feedback (CYA mode).
        /// </summary>
        invert,              // Bit   6

        /// <summary>
        /// FSM run control<para />
        /// 0 = FSM idle<para />
        /// 1 = FSM running
        /// </summary>
        run,                 // Bit   7

        /// <summary>
        /// 1 = load delay register from LoopFilterCfg2.loadVal
        /// </summary>
        load,                // Bit   8

        /// <summary>
        /// source of timing DAC control<para />
        /// 0 = from DelayCoarseU/DelayCoarseU/DelayFineA/DelayFineB config registers<para />
        /// 1 = from loop filter delay register
        /// </summary>
        sel,                 // Bit   9

        /// <summary>
        /// delay decrement/increment is triggered when LPF output reaches or exceeds the positive/negative threshold:<para />
        /// 0 = 0.031 / -0.062<para />
        /// 1 = 0.062 / -0.094<para />
        /// 2 = 0.125 / -0.156<para />
        /// 3 = 0.250 / -0.281<para />
        /// 4 = 0.500 / -0.531<para />
        /// 5 = 0.750 / -0.781<para />
        /// 6 = 0.875 / -0.906<para />
        /// 7 = 0.938 / -0.969<para />
        /// 8 = 0.969 / -1.000<para />
        /// 9 = never triggered<para />
        /// 10-15 = undefined
        /// </summary>
        thresh,              // Bits 10-13 		 
    }

    /// <summary>
    /// DLL loop filter configuration
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum LoopFilterCfg2BF
    {
        /// <summary>
        /// Value loaded into delay when LoopFilterCfg1.load = 1.
        /// </summary>
        loadVal,             // Bits  0-10 		 
    }

    /// <summary>
    /// DLL loop filter status outputs
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum LoopFilterStat1BF
    {
        /// <summary>
        /// Output from low pass filter. Signed fraction.
        /// </summary>
        filtOut,            // Bits 0-5

        /// <summary>
        /// 1 = filtOut is valid
        /// </summary>
        filtValid,          // Bit 6
    }

    /// <summary>
    /// DLL loop filter status outputs
    /// </summary>
    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum LoopFilterStat2BF
    {
        /// <summary>
        /// Loop filter delay register
        /// </summary>
        delay,            // Bits 0-10
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum LoopFilterStatReqBF
    {
        /// <summary>
        /// Software requests status
        /// </summary>
        request             // Bit 0
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum LoopFilterStatAckBF
    {
        /// <summary>
        /// Loop filter aknowledges status is ready
        /// </summary>
        acknowledge             // Bit 0
    }

    #endregion

    #region DacCfg

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DacCfg1BF
    {
        /// <summary>
        /// enable DDR-mode CML receivers, 1=enabled
        /// </summary>
        DdrRxEn,            //0

        /// <summary>
        /// enable DllClk CML receiver, 1=enabled
        /// </summary>
        DllClkRxEn,         //1

        /// <summary>
        /// enable OutMark[1:0] CML receivers, 1=enabled
        /// </summary>
        OutMarkRxEn,        //2...3

        /// <summary>
        /// enable DDR-mode mux, 1=enabled
        /// </summary>
        DdrMuxEn,           //4

        /// <summary>
        /// enable lane deskew test outputs, 1=enabled
        /// </summary>
        TestEn,             //5

        /// <summary>
        /// force Aout to midscale, 1=forced
        /// </summary>
        Midscale,           //6

        /// <summary>
        /// enable OutMark[1:0] A-to-D transmitters, 1=enabled
        /// </summary>
        OutMarkTxEn,        //7..8

        /// <summary>
        /// Aoutp high over-volt detector enable, 1=enabled
        /// </summary>
        AoutpOverHighEn,    //9

        /// <summary>
        /// Aoutp low over-volt detector enable, 1=enabled
        /// </summary>
        AoutpOverLowEn,     //10

        /// <summary>
        /// Aoutn high over-volt detector enable, 1=enabled
        /// </summary>
        AoutnOverHighEn,    //11

        /// <summary>
        /// Aoutn low over-volt detector enable, 1=enabled
        /// </summary>
        AoutnOverLowEn,     //12

        /// <summary>
        /// enable over-volt detector midscale force, 1=enabled
        /// </summary>
        OverForceEnable,    //13

        /// <summary>
        /// enable OutMark[1:0] resamplers, 1=enabled
        /// </summary>
        MarkRsEn,           //14..15
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DacCfg2BF
    {
        /// <summary>
        /// final FF CML bias NMOS current, 0=12uA, 255=4mA
        /// </summary>
        CmlIref1,            // Bits  0- 7

        /// <summary>
        /// switch driver CML bias NMOS current, 0=12uA, 255=4mA
        /// </summary>
        CmlIref2,            // Bits  8-15 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DacCfg3BF
    {
        /// <summary>
        /// enable final FF CML bias ring oscillator, 1=enabled
        /// </summary>
        CmlOscEn1,           // Bit   0

        /// <summary>
        /// enable switch driver CML bias ring oscillator, 1=enabled
        /// </summary>
        CmlOscEn2,           // Bit   1

        /// <summary>
        /// select CML bias ring oscillator sent to Sync Toolkit<para />
        /// 0 = final FF<para />
        /// 1 = switch driver
        /// </summary>
        CmlOscSel,           // Bit   2

        /// <summary>
        /// final FF CML bias lowpass filter control<para />
        /// 0 = 200 kHz BW<para />
        /// 1 = 2 kHz BW<para />
        /// 2 = 1 kHz BW<para />
        /// 3 = 400 Hz BW
        /// </summary>
        CmlLpfEn1,           // Bits  3- 4

        /// <summary>
        /// switch driver CML bias lowpass filter control<para />
        /// 0 = 200 kHz BW<para />
        /// 1 = 2 kHz BW<para />
        /// 2 = 1 kHz BW<para />
        /// 3 = 400 Hz BW
        /// </summary>
        CmlLpfEn2,           // Bits  5- 6 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DacCfg4BF
    {
        /// <summary>
        /// Adjust the gate voltage of the DAC output cascode
        /// </summary>
        DacOutCasGateAdj,    // Bits  0- 5

        /// <summary>
        /// Adjust the second folding current in DAC core
        /// </summary>
        DacSecFoldCurAdj,    // Bits  6-11

        /// <summary>
        /// Enable feedback peaking in DAC reference slice clock driver
        /// </summary>
        DacRefSliceClkFbEn,  // Bit  12

        /// <summary>
        /// Select ATestLoBus sub-bus<para />
        /// 0 = 1.0V test bus<para />
        /// 1 = temperature diode test bus
        /// </summary>
        ATestLoBusSel,       // Bit  13

        /// <summary>
        /// 1 = unselected sub-buses tied to GNDA<para />
        /// 0 = unselected sub-buses left floating
        /// </summary>
        ATestLoBusTiedownOn, // Bit  14 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DacCfg5BF
    {
        /// <summary>
        /// final FF CML bias PMOS current, 0=12uA, 255=4mA
        /// </summary>
        CmlIrefp1,           // Bits  0- 7

        /// <summary>
        /// switch driver CML bias PMOS current, 0=12uA, 255=4mA
        /// </summary>
        CmlIrefp2,           // Bits  8-15 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DacCfg6BF
    {
        /// <summary>
        /// final FF CML swing, 0=434mV, 32=495mV, 63=554mV
        /// </summary>
        CmlSwing1,           // Bits  0- 5

        /// <summary>
        /// switch driver CML swing, 0=523mV, 38=593mV, 63=639mV
        /// </summary>
        CmlSwing2,           // Bits  6-11 		 
    }

    #endregion

    #region DacAnaClkSys

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkaCfgBF
    {
        /// <summary>
        /// static AClk value used when AClkMux1=1, AClkMux2=0
        /// </summary>
        AClkStatic,         // Bit 0

        /// <summary>
        /// AClkMux1 select<para />
        /// 0 = low noise AClk path<para />
        /// 1 = AClk from AClkMux2
        /// </summary>
        AClkMux1,           // Bit 1

        /// <summary>
        /// AClkMux2 select<para />
        /// 0 = static AClk(i.e.no resampling) from AClkStatic<para />
        /// 1 = AClk from GlobalDelay
        /// </summary>
        AClkMux2,           // Bit 2

        /// <summary>
        /// LClkMux select<para />
        /// 0 = normal LClk path<para />
        /// 1 = LClk from TestClk
        /// </summary>
        LClkMux,            // Bit 3

        /// <summary>
        /// TestClk receiver enable
        /// </summary>
        TestClkRxEn,        // Bit 4

        /// <summary>
        /// enable feedback peaking in AClk buffers, 1=enabled
        /// </summary>
        FbEnAClkDrive,      // Bit 5

        /// <summary>
        /// enable feedback peaking in AClkMux2, 1=enabled
        /// </summary>
        FbEnAClkMux2,       // Bit 6

        /// <summary>
        /// enable feedback peaking in GlobalDelay timing DAC, 1=enabled
        /// </summary>
        FbEnGlobalDelay,    // Bit 7

        /// <summary>
        /// enable feedback peaking in IClk buffers, 1=enabled
        /// </summary>
        FbEnIClkDrive,      // Bit 8

        /// <summary>
        /// enable feedback peaking in LClkDelay timing DAC, 1=enabled
        /// </summary>
        FbEnLClkDelay,      // Bit 9

        /// <summary>
        /// enable feedback peaking in LClk buffers, 1=enabled
        /// </summary>
        FbEnLClkDrive,      // Bit 10

        /// <summary>
        /// enable feedback peaking in CML-to-CMOS level shift, 1=enabled
        /// </summary>
        FbEnLs,             // Bit 11

        /// <summary>
        /// enable feedback peaking in TestClkDelay1 timing DAC, 1=enabled
        /// </summary>
        FbEnTestClkDelay1,  // Bit 12

        /// <summary>
        /// enable feedback peaking in TestClkDelay2 timing DAC, 1=enabled
        /// </summary>
        FbEnTestClkDelay2,  // Bit 13

        /// <summary>
        /// enable feedback peaking in IClk2 buffers, 1=enabled
        /// </summary>
        FbEnIClk2Drive,     // Bit 14

        /// <summary>
        /// enable IClk2 output to IntSerRefClk divider, 0=enabled
        /// </summary>
        IClk2Off,           // Bit 15
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DllPhaseDetCfg1BF
    {
        /// <summary>
        /// delay adjust for AClk/LClk input, unsigned
        /// </summary>
        DelayClk,            // Bits  0- 2

        /// <summary>
        /// delay adjust for DllClk input, unsigned, valid range 0-12
        /// </summary>
        DelayDllClk,         // Bits  3- 6

        /// <summary>
        /// Nreset for dither counter
        /// </summary>
        DithNreset,          // Bit   7

        /// <summary>
        /// dither scale control, unsigned
        /// </summary>
        DithScale,           // Bits  8-10

        /// <summary>
        /// enable feedback peaking in AClk/LClk delay line
        /// </summary>
        fbEn,                // Bit  11 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum DllPhaseDetCfg2BF
    {
        BiasAdj,             // Bits  0- 7 		 
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    internal enum ClkaSpareBF
    {
        /// <summary>
        /// enable feedback peaking in final FF slices, 1=enabled
        /// </summary>
        FbEnFffSlice,       // Bit 0

        /// <summary>
        /// enable feedback peaking protection by watchdog, 1=enabled
        /// </summary>
        WatchdogForceEn,       // Bit 1

        /// <summary>
        /// enable feedback peaking in AClk buffer for OutMark resampler and resampling DLL phase detector, 1=enabled
        /// </summary>
        FbEnAClkBufMark,       // Bit 2
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbA0BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbA1BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbA2BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbA3BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbA4BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbA5BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbA6BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbA7BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbA8BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbA9BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbA10BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbA11BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbA12BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbA13BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbA14BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbA15BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjPLsbA0BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjPLsbA1BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjPLsbA2BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjPLsbA3BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjPLsbA4BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjSLsbA0BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjSLsbA1BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjSLsbA2BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjSLsbA3BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjSLsbA4BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbB0BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbB1BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbB2BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbB3BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbB4BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbB5BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbB6BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbB7BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbB8BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbB9BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbB10BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbB11BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbB12BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbB13BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbB14BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjMsbB15BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjPLsbB0BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjPLsbB1BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjPLsbB2BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjPLsbB3BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjPLsbB4BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjSLsbB0BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjSLsbB1BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjSLsbB2BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjSLsbB3BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum DacCurAdjSLsbB4BF
    {
        Value
    }
		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj0BF
		{
			AdjA,                // Bits  0- 5 		 backGate adjustment for DAC-A switch driver. 0=1V, 63=0V
			AdjEnAp,             // Bit   6 		 backGate Connection for n-side DAC-A switch driver. 1=backgate from adjA, 0=backgate shorted to VDDA
			AdjEnAn,             // Bit   7 		 backGate Connection for n-side DAC-A switch driver. 1=backgate from adjA, 0=backgate shorted to VDDA
			AdjB,                // Bits  8-13 		 backGate adjustment for DAC-B switch driver. 0=1V, 63=0V
			AdjEnBp,             // Bit  14 		 backGate Connection for n-side DAC-B switch driver. 1=backgate from adjB, 0=backgate shorted to VDDA
			AdjEnBn,             // Bit  15 		 backGate Connection for n-side DAC-B switch driver. 1=backgate from adjB, 0=backgate shorted to VDDA
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj1BF
		{
			AdjA,                // Bits  0- 5 		 backGate adjustment for DAC-A switch driver. 0=1V, 63=0V
			AdjEnAp,             // Bit   6 		 backGate Connection for n-side DAC-A switch driver. 1=backgate from adjA, 0=backgate shorted to VDDA
			AdjEnAn,             // Bit   7 		 backGate Connection for n-side DAC-A switch driver. 1=backgate from adjA, 0=backgate shorted to VDDA
			AdjB,                // Bits  8-13 		 backGate adjustment for DAC-B switch driver. 0=1V, 63=0V
			AdjEnBp,             // Bit  14 		 backGate Connection for n-side DAC-B switch driver. 1=backgate from adjB, 0=backgate shorted to VDDA
			AdjEnBn,             // Bit  15 		 backGate Connection for n-side DAC-B switch driver. 1=backgate from adjB, 0=backgate shorted to VDDA
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj2BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj3BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj4BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj5BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj6BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj7BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj8BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj9BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj10BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj11BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj12BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj13BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj14BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj15BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj16BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj17BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj18BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj19BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj20BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj21BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj22BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj23BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj24BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj25BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj26BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj27BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj28BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj29BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj30BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj31BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj32BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj33BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj34BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj35BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj36BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj37BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}

		[System.Reflection.Obfuscation( Exclude = true )]
		public enum SwDrvOsAdj38BF
		{
			AdjA,                // Bits  0- 5 		 
			AdjEnAp,             // Bit   6 		 
			AdjEnAn,             // Bit   7 		 
			AdjB,                // Bits  8-13 		 
			AdjEnBp,             // Bit  14 		 
			AdjEnBn,             // Bit  15 		 
		}	
    #endregion

    #region Cal internal used registers
    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP0BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP1BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP2BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP3BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP4BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP5BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP6BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP7BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP8BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP9BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP10BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP11BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP12BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP13BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP14BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP15BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP16BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP17BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP18BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP19BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP20BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP21BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP22BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP23BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP24BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP25BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP26BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP27BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP28BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP29BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP30BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP31BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP32BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP33BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP34BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP35BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP36BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP37BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP38BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP39BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP40BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP41BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP42BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP43BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP44BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP45BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP46BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP47BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP48BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP49BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP50BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP51BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP52BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP53BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP54BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP55BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP56BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP57BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP58BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP59BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP60BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP61BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP62BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInP63BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS0BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS1BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS2BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS3BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS4BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS5BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS6BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS7BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS8BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS9BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS10BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS11BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS12BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS13BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS14BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS15BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS16BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS17BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS18BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS19BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS20BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS21BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS22BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS23BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS24BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS25BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS26BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS27BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS28BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS29BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS30BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS31BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS32BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS33BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS34BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS35BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS36BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS37BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS38BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS39BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS40BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS41BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS42BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS43BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS44BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS45BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS46BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS47BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS48BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS49BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS50BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS51BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS52BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS53BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS54BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS55BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS56BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS57BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS58BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS59BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS60BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS61BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS62BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInS63BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM0BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM1BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM2BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM3BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM4BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM5BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM6BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM7BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM8BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM9BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM10BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM11BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM12BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM13BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM14BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM15BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM16BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM17BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM18BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM19BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM20BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM21BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM22BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM23BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM24BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM25BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM26BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM27BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM28BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM29BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM30BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM31BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM32BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM33BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM34BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM35BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM36BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM37BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM38BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM39BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM40BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM41BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM42BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM43BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM44BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM45BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM46BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM47BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM48BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM49BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM50BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM51BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM52BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM53BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM54BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM55BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM56BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM57BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM58BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM59BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM60BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM61BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM62BF
    {
        Value
    }

    [System.Reflection.Obfuscation(Exclude = true)]
    public enum FinalMuxInM63BF
    {
        Value
    }
    #endregion

    #endregion BitField enums used for M9347PluginRegisterSet

    /// <summary>
    /// 
    /// </summary>
    public class WyvernRegisterSet
    {
        //public BoardDriver.VXT3BoardDriver driver;
        public IRegister[] Registers
        {
            get;
            protected set;
        }
        #region constants/enums/lookup tables

        private readonly RegDef[] mRegisterDefinitions =
            {
               // General
               new RegDef( (int)WyvernReg.Mark,                    0x0004, typeof( MarkBF ) ),
               new RegDef( (int)WyvernReg.Status0,                 0x0007, typeof( Status0BF )),
               new RegDef( (int)WyvernReg.Status1,                 0x0008, typeof( Status1BF )),
               new RegDef( (int)WyvernReg.CClkGate,                0x0010, typeof( CClkGateBF )),

               // MarkPropagation
               new RegDef( (int)WyvernReg.InMarkCfg,               0x0028, typeof( InMarkCfgBF )),
               new RegDef( (int)WyvernReg.OutMarkCfg0,             0x0033, typeof( OutMarkCfgBF )),
               new RegDef( (int)WyvernReg.OutMarkCfg1,             0x0033, typeof( OutMarkCfgBF )),
               new RegDef( (int)WyvernReg.OutMarkCfg2,             0x0033, typeof( OutMarkCfgBF )),
               new RegDef( (int)WyvernReg.OutMarkCfg3,             0x0033, typeof( OutMarkCfgBF )),

               // Gnco  Cancel by zhangfan fot Banjo
               new RegDef( (int)WyvernReg.GNcoCfg,                 0x005B, typeof( GNcoCfgBF )),
               new RegDef( (int)WyvernReg.GNcoDFL,                 0x005e, typeof( GNcoDFLBF )),
               new RegDef( (int)WyvernReg.GNcoDFU,                 0x005f, typeof( GNcoDFUBF )),
               new RegDef( (int)WyvernReg.GNcoPhaseOffsetL,        0x0060, typeof( GNcoPhaseOffsetBF )),
               new RegDef( (int)WyvernReg.GNcoPhaseOffsetU,        0x0061, typeof( GNcoPhaseOffsetBF )),
               new RegDef( (int)WyvernReg.GNcoScale,               0x0062, typeof( GNcoScaleBF )),
               new RegDef( (int)WyvernReg.GNcoFreq_0,              0x0096, typeof( GNcoFreqBF )),
               new RegDef( (int)WyvernReg.GNcoFreq_1,              0x0097, typeof( GNcoFreqBF )),
               new RegDef( (int)WyvernReg.GNcoFreq_2,              0x0098, typeof( GNcoFreqBF )),
               new RegDef( (int)WyvernReg.GNcoFreq_3,              0x0099, typeof( GNcoFreqBF )),
               new RegDef( (int)WyvernReg.GNcoFreq_4,              0x009A, typeof( GNcoFreqBF )),
               new RegDef( (int)WyvernReg.GNcoMarkSel,             0x009c, typeof( GNcoMarkSelBF )),
               new RegDef( (int)WyvernReg.GNcoMarkCfg,             0x00A2, typeof( GNcoMarkCfgBF )),

               // SignalGen
               new RegDef( (int)WyvernReg.ScaleCfg,                0x03A4, typeof( ScaleCfgBF )),
               new RegDef( (int)WyvernReg.Scale,                   0x03A5, typeof( ScaleBF )),

               // DdrInterp
               new RegDef( (int)WyvernReg.DdrInterpCfg,            0x0406, typeof( DdrInterpCfgBF )),

               // Dem
               new RegDef( (int)WyvernReg.DemCfg,                  0x041a, typeof( DemCfgBF )),

               // FinalMuxIn
               new RegDef( (int)WyvernReg.FinalMuxInCfg,           0x042E, typeof( FinalMuxInCfgBF )),
               new RegDef( (int)WyvernReg.DllClkCreateMark,        0x04ef, typeof( DllClkCreateMarkBF )),
			   new RegDef( (int)WyvernReg.DllClkCfg,               0x04f0, typeof( DllClkCfgBF )),
               new RegDef( (int)WyvernReg.DllClkWidth,             0x04f3, typeof( DllClkWidthBF )),

               // FinalMux
			   new RegDef( (int)WyvernReg.FinalMuxCfg,             0x0800, typeof( FinalMuxCfgBF )),
               new RegDef( (int)WyvernReg.ClkdCfg,                 0x0801, typeof( ClkdCfgBF )),
               new RegDef( (int)WyvernReg.MuxClkDelayCfg,          0x0802, typeof( ClkDelayCfgBF )),
               new RegDef( (int)WyvernReg.MuxClkDelayCoarseL,      0x0803, typeof( ClkDelayCoarseLBF )),
               new RegDef( (int)WyvernReg.MuxClkDelayCoarseU,      0x0804, typeof( ClkDelayCoarseUBF )),
               new RegDef( (int)WyvernReg.MuxClkDelayFineA,        0x0805, typeof( ClkDelayFineABF )),
               new RegDef( (int)WyvernReg.MuxClkDelayFineB,        0x0806, typeof( ClkDelayFineBBF )),
               new RegDef( (int)WyvernReg.MuxClkDelayTweak,        0x0807, typeof( ClkDelayTweakBF )),
               new RegDef( (int)WyvernReg.CustomLvdsEnable,        0x080b, typeof( CustomLvdsEnableBF )),
               new RegDef( (int)WyvernReg.CustomLvdsForce,         0x080c, typeof( CustomLvdsForceBF )),
               new RegDef( (int)WyvernReg.IsoLoopFilterCfg1,       0x080e, typeof( LoopFilterCfg1BF )),
               new RegDef( (int)WyvernReg.IsoLoopFilterCfg2,       0x080f, typeof( LoopFilterCfg2BF )),
               new RegDef( (int)WyvernReg.IsoLoopFilterStat1,      0x0810, typeof( LoopFilterStat1BF )),
               new RegDef( (int)WyvernReg.IsoLoopFilterStat2,      0x0811, typeof( LoopFilterStat2BF )),
               new RegDef( (int)WyvernReg.IsoLoopFilterStatReq,    0x0812, typeof( LoopFilterStatReqBF )),
               new RegDef( (int)WyvernReg.IsoLoopFilterStatAck,    0x0813, typeof( LoopFilterStatAckBF )),

               // DacCfg
               new RegDef( (int)WyvernReg.DacCfg1,                 0x1000, typeof( DacCfg1BF )),
               new RegDef( (int)WyvernReg.DacCfg2,                 0x1001, typeof( DacCfg2BF )),
               new RegDef( (int)WyvernReg.DacCfg3,                 0x1002, typeof( DacCfg3BF )),
               new RegDef( (int)WyvernReg.DacCfg4,                 0x1003, typeof( DacCfg4BF )),
               new RegDef( (int)WyvernReg.DacCfg5,                 0x1004, typeof( DacCfg5BF )),
               new RegDef( (int)WyvernReg.DacCfg6,                 0x1005, typeof( DacCfg6BF )),

               // DacAnaClkSys
               new RegDef( (int)WyvernReg.ClkaCfg,                 0x10e9, typeof( ClkaCfgBF )),
               new RegDef( (int)WyvernReg.LClkDelayCfg,            0x10ea, typeof( ClkDelayCfgBF )),
               new RegDef( (int)WyvernReg.LClkDelayCoarseL,        0x10eb, typeof( ClkDelayCoarseLBF )),
               new RegDef( (int)WyvernReg.LClkDelayCoarseU,        0x10ec, typeof( ClkDelayCoarseUBF )),
               new RegDef( (int)WyvernReg.LClkDelayFineA,          0x10ed, typeof( ClkDelayFineABF )),
               new RegDef( (int)WyvernReg.LClkDelayFineB,          0x10ee, typeof( ClkDelayFineBBF )),
               new RegDef( (int)WyvernReg.LClkDelayTweak,          0x10ef, typeof( ClkDelayTweakBF )),
               new RegDef( (int)WyvernReg.GlobalDelayCfg,          0x10f0, typeof( ClkDelayCfgBF )),
               new RegDef( (int)WyvernReg.GlobalDelayCoarseL,      0x10f1, typeof( ClkDelayCoarseLBF )),
               new RegDef( (int)WyvernReg.GlobalDelayCoarseU,      0x10f2, typeof( ClkDelayCoarseUBF )),
               new RegDef( (int)WyvernReg.GlobalDelayFineA,        0x10f3, typeof( ClkDelayFineABF )),
               new RegDef( (int)WyvernReg.GlobalDelayFineB,        0x10f4, typeof( ClkDelayFineBBF ) ),
               new RegDef( (int)WyvernReg.GlobalDelayTweak,        0x10f5, typeof( ClkDelayTweakBF )),
               new RegDef( (int)WyvernReg.RsLoopFilterCfg1,        0x10f8, typeof( LoopFilterCfg1BF )),
               new RegDef( (int)WyvernReg.RsLoopFilterCfg2,        0x10f9, typeof( LoopFilterCfg2BF )),
               new RegDef( (int)WyvernReg.RsLoopFilterStat1,       0x10fa, typeof( LoopFilterStat1BF )),
               new RegDef( (int)WyvernReg.RsLoopFilterStat2,       0x10fb, typeof( LoopFilterStat2BF )),
               new RegDef( (int)WyvernReg.RsLoopFilterStatReq,     0x10fc, typeof( LoopFilterStatReqBF )),
               new RegDef( (int)WyvernReg.RsLoopFilterStatAck,     0x10fd, typeof( LoopFilterStatAckBF )),
               new RegDef( (int)WyvernReg.DllPhaseDetCfg10,        0x10fe, typeof( DllPhaseDetCfg1BF )),
			   new RegDef( (int)WyvernReg.DllPhaseDetCfg11,        0x10ff, typeof( DllPhaseDetCfg1BF )),
			   new RegDef( (int)WyvernReg.DllPhaseDetCfg20,        0x1100, typeof( DllPhaseDetCfg2BF ) ),
			   new RegDef( (int)WyvernReg.DllPhaseDetCfg21,        0x1101, typeof( DllPhaseDetCfg2BF )),
               new RegDef( (int)WyvernReg.ClkaSpare,               0x1102, typeof( ClkaSpareBF )),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbA0, 		0x1030, typeof(DacCurAdjMsbA0BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbA1, 		0x1031, typeof(DacCurAdjMsbA1BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbA2, 		0x1032, typeof(DacCurAdjMsbA2BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbA3, 		0x1033, typeof(DacCurAdjMsbA3BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbA4,		0x1034, typeof(DacCurAdjMsbA4BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbA5, 		0x1035, typeof(DacCurAdjMsbA5BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbA6, 		0x1036, typeof(DacCurAdjMsbA6BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbA7, 		0x1037, typeof(DacCurAdjMsbA7BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbA8, 		0x1038, typeof(DacCurAdjMsbA8BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbA9,		0x1039, typeof(DacCurAdjMsbA9BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbA10, 		0x103a, typeof(DacCurAdjMsbA10BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbA11, 		0x103b, typeof(DacCurAdjMsbA11BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbA12, 		0x103c, typeof(DacCurAdjMsbA12BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbA13, 		0x103d, typeof(DacCurAdjMsbA13BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbA14, 		0x103e, typeof(DacCurAdjMsbA14BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbA15, 		0x103f, typeof(DacCurAdjMsbA15BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjPLsbA0, 		0x1026, typeof(DacCurAdjPLsbA0BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjPLsbA1, 		0x1027, typeof(DacCurAdjPLsbA1BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjPLsbA2,		0x1028, typeof(DacCurAdjPLsbA2BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjPLsbA3, 		0x1029, typeof(DacCurAdjPLsbA3BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjPLsbA4, 		0x102a, typeof(DacCurAdjPLsbA4BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjSLsbA0, 		0x102b, typeof(DacCurAdjSLsbA0BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjSLsbA1, 		0x102c, typeof(DacCurAdjSLsbA1BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjSLsbA2, 		0x102d, typeof(DacCurAdjSLsbA2BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjSLsbA3, 		0x102e, typeof(DacCurAdjSLsbA3BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjSLsbA4, 		0x102f, typeof(DacCurAdjSLsbA4BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbB0, 		0x108a, typeof(DacCurAdjMsbB0BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbB1, 		0x108b, typeof(DacCurAdjMsbB1BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbB2, 		0x108c, typeof(DacCurAdjMsbB2BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbB3, 		0x108d, typeof(DacCurAdjMsbB3BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbB4, 		0x108e, typeof(DacCurAdjMsbB4BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbB5, 		0x108f, typeof(DacCurAdjMsbB5BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbB6, 		0x1090, typeof(DacCurAdjMsbB6BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbB7, 		0x1091, typeof(DacCurAdjMsbB7BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbB8, 		0x1092, typeof(DacCurAdjMsbB8BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbB9, 		0x1093, typeof(DacCurAdjMsbB9BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbB10, 		0x1094, typeof(DacCurAdjMsbB10BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbB11, 		0x1095, typeof(DacCurAdjMsbB11BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbB12, 		0x1096, typeof(DacCurAdjMsbB12BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbB13, 		0x1097, typeof(DacCurAdjMsbB13BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbB14, 		0x1098, typeof(DacCurAdjMsbB14BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjMsbB15, 		0x1099, typeof(DacCurAdjMsbB15BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjPLsbB0, 		0x1080, typeof(DacCurAdjPLsbB0BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjPLsbB1, 		0x1081, typeof(DacCurAdjPLsbB1BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjPLsbB2, 		0x1082, typeof(DacCurAdjPLsbB2BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjPLsbB3, 		0x1083, typeof(DacCurAdjPLsbB3BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjPLsbB4, 		0x1084, typeof(DacCurAdjPLsbB4BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjSLsbB0, 		0x1085, typeof(DacCurAdjSLsbB0BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjSLsbB1, 		0x1086, typeof(DacCurAdjSLsbB1BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjSLsbB2, 		0x1087, typeof(DacCurAdjSLsbB2BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjSLsbB3, 		0x1088, typeof(DacCurAdjSLsbB3BF)),
			   new RegDef( (int)WyvernReg.DacCurAdjSLsbB4, 		0x1089, typeof(DacCurAdjSLsbB4BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj0, 0x10c2, typeof(SwDrvOsAdj0BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj1, 0x10c3, typeof(SwDrvOsAdj1BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj2, 0x10c4, typeof(SwDrvOsAdj2BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj3, 0x10c5, typeof(SwDrvOsAdj3BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj4, 0x10c6, typeof(SwDrvOsAdj4BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj5, 0x10c7, typeof(SwDrvOsAdj5BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj6, 0x10c8, typeof(SwDrvOsAdj6BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj7, 0x10c9, typeof(SwDrvOsAdj7BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj8, 0x10ca, typeof(SwDrvOsAdj8BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj9, 0x10cb, typeof(SwDrvOsAdj9BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj10, 0x10cc, typeof(SwDrvOsAdj10BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj11, 0x10cd, typeof(SwDrvOsAdj11BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj12, 0x10ce, typeof(SwDrvOsAdj12BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj13, 0x10cf, typeof(SwDrvOsAdj13BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj14, 0x10d0, typeof(SwDrvOsAdj14BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj15, 0x10d1, typeof(SwDrvOsAdj15BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj16, 0x10d2, typeof(SwDrvOsAdj16BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj17, 0x10d3, typeof(SwDrvOsAdj17BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj18, 0x10d4, typeof(SwDrvOsAdj18BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj19, 0x10d5, typeof(SwDrvOsAdj19BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj20, 0x10d6, typeof(SwDrvOsAdj20BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj21, 0x10d7, typeof(SwDrvOsAdj21BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj22, 0x10d8, typeof(SwDrvOsAdj22BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj23, 0x10d9, typeof(SwDrvOsAdj23BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj24, 0x10da, typeof(SwDrvOsAdj24BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj25, 0x10db, typeof(SwDrvOsAdj25BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj26, 0x10dc, typeof(SwDrvOsAdj26BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj27, 0x10dd, typeof(SwDrvOsAdj27BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj28, 0x10de, typeof(SwDrvOsAdj28BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj29, 0x10df, typeof(SwDrvOsAdj29BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj30, 0x10e0, typeof(SwDrvOsAdj30BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj31, 0x10e1, typeof(SwDrvOsAdj31BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj32, 0x10e2, typeof(SwDrvOsAdj32BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj33, 0x10e3, typeof(SwDrvOsAdj33BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj34, 0x10e4, typeof(SwDrvOsAdj34BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj35, 0x10e5, typeof(SwDrvOsAdj35BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj36, 0x10e6, typeof(SwDrvOsAdj36BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj37, 0x10e7, typeof(SwDrvOsAdj37BF)),
			   new RegDef( (int)WyvernReg.SwDrvOsAdj38, 0x10e8, typeof(SwDrvOsAdj38BF)),			   
			   new RegDef( (int)WyvernReg.FinalMuxInP0, 		0x042f, typeof(FinalMuxInP0BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP1, 		0x0430, typeof(FinalMuxInP1BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP2, 		0x0431, typeof(FinalMuxInP2BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP3, 		0x0432, typeof(FinalMuxInP3BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP4, 		0x0433, typeof(FinalMuxInP4BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP5, 		0x0434, typeof(FinalMuxInP5BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP6,		0x0435, typeof(FinalMuxInP6BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP7,		0x0436, typeof(FinalMuxInP7BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP8, 		0x0437, typeof(FinalMuxInP8BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP9, 		0x0438, typeof(FinalMuxInP9BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP10, 		0x0439, typeof(FinalMuxInP10BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP11, 		0x043a, typeof(FinalMuxInP11BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP12, 		0x043b, typeof(FinalMuxInP12BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP13, 		0x043c, typeof(FinalMuxInP13BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP14, 		0x043d, typeof(FinalMuxInP14BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP15, 		0x043e, typeof(FinalMuxInP15BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP16, 		0x043f, typeof(FinalMuxInP16BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP17, 		0x0440, typeof(FinalMuxInP17BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP18, 		0x0441, typeof(FinalMuxInP18BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP19, 		0x0442, typeof(FinalMuxInP19BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP20, 		0x0443, typeof(FinalMuxInP20BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP21, 		0x0444, typeof(FinalMuxInP21BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP22, 		0x0445, typeof(FinalMuxInP22BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP23, 		0x0446, typeof(FinalMuxInP23BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP24, 		0x0447, typeof(FinalMuxInP24BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP25, 		0x0448, typeof(FinalMuxInP25BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP26, 		0x0449, typeof(FinalMuxInP26BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP27, 		0x044a, typeof(FinalMuxInP27BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP28, 		0x044b, typeof(FinalMuxInP28BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP29, 		0x044c, typeof(FinalMuxInP29BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP30, 		0x044d, typeof(FinalMuxInP30BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP31, 		0x044e, typeof(FinalMuxInP31BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP32, 		0x044f, typeof(FinalMuxInP32BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP33, 		0x0450, typeof(FinalMuxInP33BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP34, 		0x0451, typeof(FinalMuxInP34BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP35, 		0x0452, typeof(FinalMuxInP35BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP36, 		0x0453, typeof(FinalMuxInP36BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP37, 		0x0454, typeof(FinalMuxInP37BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP38, 		0x0455, typeof(FinalMuxInP38BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP39,		0x0456, typeof(FinalMuxInP39BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP40,		0x0457, typeof(FinalMuxInP40BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP41, 		0x0458, typeof(FinalMuxInP41BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP42, 		0x0459, typeof(FinalMuxInP42BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP43, 		0x045a, typeof(FinalMuxInP43BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP44, 		0x045b, typeof(FinalMuxInP44BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP45, 		0x045c, typeof(FinalMuxInP45BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP46, 		0x045d, typeof(FinalMuxInP46BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP47, 		0x045e, typeof(FinalMuxInP47BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP48, 		0x045f, typeof(FinalMuxInP48BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP49, 		0x0460, typeof(FinalMuxInP49BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP50, 		0x0461, typeof(FinalMuxInP50BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP51, 		0x0462, typeof(FinalMuxInP51BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP52, 		0x0463, typeof(FinalMuxInP52BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP53, 		0x0464, typeof(FinalMuxInP53BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP54, 		0x0465, typeof(FinalMuxInP54BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP55, 		0x0466, typeof(FinalMuxInP55BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP56, 		0x0467, typeof(FinalMuxInP56BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP57, 		0x0468, typeof(FinalMuxInP57BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP58, 		0x0469, typeof(FinalMuxInP58BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP59, 		0x046a, typeof(FinalMuxInP59BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP60, 		0x046b, typeof(FinalMuxInP60BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP61, 		0x046c, typeof(FinalMuxInP61BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP62, 		0x046d, typeof(FinalMuxInP62BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInP63, 		0x046e, typeof(FinalMuxInP63BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS0, 		0x046f, typeof(FinalMuxInS0BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS1, 		0x0470, typeof(FinalMuxInS1BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS2, 		0x0471, typeof(FinalMuxInS2BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS3, 		0x0472, typeof(FinalMuxInS3BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS4, 		0x0473, typeof(FinalMuxInS4BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS5, 		0x0474, typeof(FinalMuxInS5BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS6, 		0x0475, typeof(FinalMuxInS6BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS7, 		0x0476, typeof(FinalMuxInS7BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS8, 		0x0477, typeof(FinalMuxInS8BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS9, 		0x0478, typeof(FinalMuxInS9BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS10, 		0x0479, typeof(FinalMuxInS10BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS11, 		0x047a, typeof(FinalMuxInS11BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS12, 		0x047b, typeof(FinalMuxInS12BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS13, 		0x047c, typeof(FinalMuxInS13BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS14, 		0x047d, typeof(FinalMuxInS14BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS15, 		0x047e, typeof(FinalMuxInS15BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS16, 		0x047f, typeof(FinalMuxInS16BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS17, 		0x0480, typeof(FinalMuxInS17BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS18, 		0x0481, typeof(FinalMuxInS18BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS19, 		0x0482, typeof(FinalMuxInS19BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS20, 		0x0483, typeof(FinalMuxInS20BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS21, 		0x0484, typeof(FinalMuxInS21BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS22, 		0x0485, typeof(FinalMuxInS22BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS23, 		0x0486, typeof(FinalMuxInS23BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS24, 		0x0487, typeof(FinalMuxInS24BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS25, 		0x0488, typeof(FinalMuxInS25BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS26, 		0x0489, typeof(FinalMuxInS26BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS27, 		0x048a, typeof(FinalMuxInS27BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS28, 		0x048b, typeof(FinalMuxInS28BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS29, 		0x048c, typeof(FinalMuxInS29BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS30, 		0x048d, typeof(FinalMuxInS30BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS31, 		0x048e, typeof(FinalMuxInS31BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS32, 		0x048f, typeof(FinalMuxInS32BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS33, 		0x0490, typeof(FinalMuxInS33BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS34, 		0x0491, typeof(FinalMuxInS34BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS35, 		0x0492, typeof(FinalMuxInS35BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS36, 		0x0493, typeof(FinalMuxInS36BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS37, 		0x0494, typeof(FinalMuxInS37BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS38, 		0x0495, typeof(FinalMuxInS38BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS39, 		0x0496, typeof(FinalMuxInS39BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS40, 		0x0497, typeof(FinalMuxInS40BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS41, 		0x0498, typeof(FinalMuxInS41BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS42, 		0x0499, typeof(FinalMuxInS42BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS43, 		0x049a, typeof(FinalMuxInS43BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS44, 		0x049b, typeof(FinalMuxInS44BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS45, 		0x049c, typeof(FinalMuxInS45BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS46, 		0x049d, typeof(FinalMuxInS46BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS47, 		0x049e, typeof(FinalMuxInS47BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS48, 		0x049f, typeof(FinalMuxInS48BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS49, 		0x04a0, typeof(FinalMuxInS49BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS50, 		0x04a1, typeof(FinalMuxInS50BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS51, 		0x04a2, typeof(FinalMuxInS51BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS52, 		0x04a3, typeof(FinalMuxInS52BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS53, 		0x04a4, typeof(FinalMuxInS53BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS54, 		0x04a5, typeof(FinalMuxInS54BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS55, 		0x04a6, typeof(FinalMuxInS55BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS56, 		0x04a7, typeof(FinalMuxInS56BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS57, 		0x04a8, typeof(FinalMuxInS57BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS58, 		0x04a9, typeof(FinalMuxInS58BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS59, 		0x04aa, typeof(FinalMuxInS59BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS60, 		0x04ab, typeof(FinalMuxInS60BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS61, 		0x04ac, typeof(FinalMuxInS61BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS62, 		0x04ad, typeof(FinalMuxInS62BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInS63, 		0x04ae, typeof(FinalMuxInS63BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM0, 		0x04af, typeof(FinalMuxInM0BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM1, 		0x04b0, typeof(FinalMuxInM1BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM2, 		0x04b1, typeof(FinalMuxInM2BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM3, 		0x04b2, typeof(FinalMuxInM3BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM4, 		0x04b3, typeof(FinalMuxInM4BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM5, 		0x04b4, typeof(FinalMuxInM5BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM6, 		0x04b5, typeof(FinalMuxInM6BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM7, 		0x04b6, typeof(FinalMuxInM7BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM8, 		0x04b7, typeof(FinalMuxInM8BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM9, 		0x04b8, typeof(FinalMuxInM9BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM10, 		0x04b9, typeof(FinalMuxInM10BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM11, 		0x04ba, typeof(FinalMuxInM11BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM12, 		0x04bb, typeof(FinalMuxInM12BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM13, 		0x04bc, typeof(FinalMuxInM13BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM14, 		0x04bd, typeof(FinalMuxInM14BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM15, 		0x04be, typeof(FinalMuxInM15BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM16, 		0x04bf, typeof(FinalMuxInM16BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM17, 		0x04c0, typeof(FinalMuxInM17BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM18, 		0x04c1, typeof(FinalMuxInM18BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM19, 		0x04c2, typeof(FinalMuxInM19BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM20, 		0x04c3, typeof(FinalMuxInM20BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM21, 		0x04c4, typeof(FinalMuxInM21BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM22, 		0x04c5, typeof(FinalMuxInM22BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM23, 		0x04c6, typeof(FinalMuxInM23BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM24, 		0x04c7, typeof(FinalMuxInM24BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM25, 		0x04c8, typeof(FinalMuxInM25BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM26, 		0x04c9, typeof(FinalMuxInM26BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM27, 		0x04ca, typeof(FinalMuxInM27BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM28, 		0x04cb, typeof(FinalMuxInM28BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM29, 		0x04cc, typeof(FinalMuxInM29BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM30, 		0x04cd, typeof(FinalMuxInM30BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM31, 		0x04ce, typeof(FinalMuxInM31BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM32, 		0x04cf, typeof(FinalMuxInM32BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM33, 		0x04d0, typeof(FinalMuxInM33BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM34, 		0x04d1, typeof(FinalMuxInM34BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM35, 		0x04d2, typeof(FinalMuxInM35BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM36, 		0x04d3, typeof(FinalMuxInM36BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM37, 		0x04d4, typeof(FinalMuxInM37BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM38, 		0x04d5, typeof(FinalMuxInM38BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM39, 		0x04d6, typeof(FinalMuxInM39BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM40, 		0x04d7, typeof(FinalMuxInM40BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM41, 		0x04d8, typeof(FinalMuxInM41BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM42, 		0x04d9, typeof(FinalMuxInM42BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM43, 		0x04da, typeof(FinalMuxInM43BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM44, 		0x04db, typeof(FinalMuxInM44BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM45, 		0x04dc, typeof(FinalMuxInM45BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM46, 		0x04dd, typeof(FinalMuxInM46BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM47, 		0x04de, typeof(FinalMuxInM47BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM48, 		0x04df, typeof(FinalMuxInM48BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM49, 		0x04e0, typeof(FinalMuxInM49BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM50, 		0x04e1, typeof(FinalMuxInM50BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM51, 		0x04e2, typeof(FinalMuxInM51BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM52, 		0x04e3, typeof(FinalMuxInM52BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM53, 		0x04e4, typeof(FinalMuxInM53BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM54, 		0x04e5, typeof(FinalMuxInM54BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM55, 		0x04e6, typeof(FinalMuxInM55BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM56, 		0x04e7, typeof(FinalMuxInM56BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM57, 		0x04e8, typeof(FinalMuxInM57BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM58, 		0x04e9, typeof(FinalMuxInM58BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM59, 		0x04ea, typeof(FinalMuxInM59BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM60, 		0x04eb, typeof(FinalMuxInM60BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM61, 		0x04ec, typeof(FinalMuxInM61BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM62, 		0x04ed, typeof(FinalMuxInM62BF) ),
				new RegDef( (int)WyvernReg.FinalMuxInM63, 		0x04ee, typeof(FinalMuxInM63BF) ),
            };

        private readonly BitFieldDef[] mBitFieldDefinitions =
            {

                #region General
                //
                new BitFieldDef( (int)WyvernReg.Mark, (int)MarkBF.MarkTimeOffsetA, 0,  5 ),
                new BitFieldDef( (int)WyvernReg.Mark, (int)MarkBF.Mark0,               6 ),
                new BitFieldDef( (int)WyvernReg.Mark, (int)MarkBF.Mark1,               7 ),
                new BitFieldDef( (int)WyvernReg.Mark, (int)MarkBF.MarkTimeOffsetB, 8, 13 ),
                new BitFieldDef( (int)WyvernReg.Mark, (int)MarkBF.Mark2,              14 ),
                new BitFieldDef( (int)WyvernReg.Mark, (int)MarkBF.Mark3,              15 ),
                //
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.Midscale,             0),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.OverVoltAoutpHigh,    1),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.OverVoltAoutpLow,     2),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.OverVoltAoutnHigh,    3),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.OverVoltAoutnLow,     4),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.NonExistingAddr,      5),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.CtrlCollision,        6),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.SpiCollision,         7),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.InMarkCollision,      8),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.DataMarkCollision,    9),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.WaveMemOutOfBounds,   10),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.SwpSpanZero,          11),
                new BitFieldDef( (int)WyvernReg.Status0, (int)Status0BF.SpiParityError,       12),
                //                              
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.GNcoFreqClip,       0),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.GNcoSwpClip,        1),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.SNcoFreqClip,       2),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.SNcoSpareClip,      3),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.InterpolatorClip,   4),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.ModulatorClip,      5),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.ScaleClip,          6),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.FlatFilterClip,     7),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.NLinPredistClip,    8),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.ShfClip,            9),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.OffsetClip,         10),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.DDRInterpClipB,     11),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.CsecClip,           12),
                new BitFieldDef( (int)WyvernReg.Status1, (int)Status1BF.AwgnClip,           13),
                //
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.WaveMem,        0),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.GNco,           1),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.SNco,           2),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.AGx2,           3),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.Bx08,           4),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.CDEFx2,         5),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.Modulation,     6),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.FlatFilter,     7),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.NLPD,           8),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.Shf,            9),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.DdrInterp,      10),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.EncodingB,      11),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.Csec,           12),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.Awgn,           13),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.DdrDuc,         14),
                new BitFieldDef( (int)WyvernReg.CClkGate, (int)CClkGateBF.PowerDetector,  15),
                //
                #endregion

                #region MarkPropagation
                //
                new BitFieldDef( (int)WyvernReg.InMarkCfg, (int)InMarkCfgBF.Cfg0, 0, 1),
                new BitFieldDef( (int)WyvernReg.InMarkCfg, (int)InMarkCfgBF.Cfg1, 2, 3),
                new BitFieldDef( (int)WyvernReg.InMarkCfg, (int)InMarkCfgBF.Cfg2, 4, 5),
                new BitFieldDef( (int)WyvernReg.InMarkCfg, (int)InMarkCfgBF.Cfg3, 6, 7),
                new BitFieldDef( (int)WyvernReg.InMarkCfg, (int)InMarkCfgBF.JesdMarkEn0, 8),
                new BitFieldDef( (int)WyvernReg.InMarkCfg, (int)InMarkCfgBF.JesdMarkEn1, 9),
                new BitFieldDef( (int)WyvernReg.InMarkCfg, (int)InMarkCfgBF.JesdMarkEn2, 10),
                //
                new BitFieldDef( typeof(OutMarkCfgBF), (int)OutMarkCfgBF.DataMarkSelect, 0, 2),
                new BitFieldDef( typeof(OutMarkCfgBF), (int)OutMarkCfgBF.Mode, 3),
                new BitFieldDef( typeof(OutMarkCfgBF), (int)OutMarkCfgBF.State, 4),
                new BitFieldDef( typeof(OutMarkCfgBF), (int)OutMarkCfgBF.InvertOutput, 5),
                new BitFieldDef( typeof(OutMarkCfgBF), (int)OutMarkCfgBF.Delay, 6, 15),
                //
                #endregion

                #region Gnco
                //
                new BitFieldDef( (int)WyvernReg.GNcoCfg, (int)GNcoCfgBF.AngModEn, 0 ),
                new BitFieldDef( (int)WyvernReg.GNcoCfg, (int)GNcoCfgBF.AngModTyp, 1 ),
                new BitFieldDef( (int)WyvernReg.GNcoCfg, (int)GNcoCfgBF.AngModScl, 2, 7 ),
                new BitFieldDef( (int)WyvernReg.GNcoCfg, (int)GNcoCfgBF.PhaseRound, 8 ),
                new BitFieldDef( (int)WyvernReg.GNcoCfg, (int)GNcoCfgBF.AmpRound, 9 ),
                new BitFieldDef( (int)WyvernReg.GNcoCfg, (int)GNcoCfgBF.PhaseInterp, 10 ),
                new BitFieldDef( (int)WyvernReg.GNcoCfg, (int)GNcoCfgBF.ScaleBypass, 11 ),
                new BitFieldDef( (int)WyvernReg.GNcoCfg, (int)GNcoCfgBF.AwgnModEn, 12 ),
                //
                new BitFieldDef( (int)WyvernReg.GNcoDFL, (int)GNcoDFLBF.DFL, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.GNcoDFU, (int)GNcoDFUBF.DFU, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.GNcoPhaseOffsetL, (int)GNcoPhaseOffsetBF.PhaseOffset, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.GNcoPhaseOffsetU, (int)GNcoPhaseOffsetBF.PhaseOffset, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.GNcoScale, (int)GNcoScaleBF.Scale, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.GNcoFreq_0, (int)GNcoFreqBF.FreqBits, 0, 15 ),
                new BitFieldDef( (int)WyvernReg.GNcoFreq_1, (int)GNcoFreqBF.FreqBits, 0, 15 ),
                new BitFieldDef( (int)WyvernReg.GNcoFreq_2, (int)GNcoFreqBF.FreqBits, 0, 15 ),
                new BitFieldDef( (int)WyvernReg.GNcoFreq_3, (int)GNcoFreqBF.FreqBits, 0, 15 ),
                new BitFieldDef( (int)WyvernReg.GNcoFreq_4, (int)GNcoFreqBF.FreqBits, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.GNcoMarkSel, (int)GNcoMarkSelBF.RstPA, 0, 1 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkSel, (int)GNcoMarkSelBF.Freq, 2, 3 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkSel, (int)GNcoMarkSelBF.DF, 4, 5 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkSel, (int)GNcoMarkSelBF.PhaseOffset, 6, 7 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkSel, (int)GNcoMarkSelBF.AngModEn, 8, 9 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkSel, (int)GNcoMarkSelBF.AngModDat, 10, 11 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkSel, (int)GNcoMarkSelBF.Scale, 12, 13 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkSel, (int)GNcoMarkSelBF.AwgnModEn, 14, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnNewRstPA,                    0 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnAllSelectedMarksRstPA,       1 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnNewFreq,                     2 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnAllSelectedMarksFreq,        3 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnNewDF,                       4 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnAllSelectedMarksDF,          5 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnNewPhaseOffset,              6 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnAllSelectedMarksPhaseOffset, 7 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnNewAngModEn,                 8 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnAllSelectedMarksAngModEn,    9 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnNewAngModDat,                10 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnAllSelectedMarksAngModDat,   11 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnNewScale,                    12 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnAllSelectedMarksScale,       13 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnNewAwgnModEn,                14 ),
                new BitFieldDef( (int)WyvernReg.GNcoMarkCfg, (int)GNcoMarkCfgBF.ActOnAllSelectedMarksAwgnModEn,   15 ),
                //
                #endregion

                #region SignalGen
                //
                new BitFieldDef( (int)WyvernReg.ScaleCfg, (int)ScaleCfgBF.Exponent, 0, 4 ),
                new BitFieldDef( (int)WyvernReg.ScaleCfg, (int)ScaleCfgBF.Round, 5 ),
                //
                new BitFieldDef( (int)WyvernReg.Scale, (int)ScaleBF.Scale, 0, 15 ),
                //
                #endregion

                #region DdrInterp

                new BitFieldDef( (int)WyvernReg.DdrInterpCfg, (int)DdrInterpCfgBF.Enable, 0 ),
                new BitFieldDef( (int)WyvernReg.DdrInterpCfg, (int)DdrInterpCfgBF.Round, 1 ),
                new BitFieldDef( (int)WyvernReg.DdrInterpCfg, (int)DdrInterpCfgBF.DoubletMode, 2, 3 ),

                #endregion

                #region Dem

                new BitFieldDef( (int)WyvernReg.DemCfg, (int)DemCfgBF.Enable, 0 ),
                new BitFieldDef( (int)WyvernReg.DemCfg, (int)DemCfgBF.DoDdr, 1 ),
                new BitFieldDef( (int)WyvernReg.DemCfg, (int)DemCfgBF.DemLsbClip, 2, 5 ),

                #endregion

                #region FinalMuxIn
                
                new BitFieldDef( (int)WyvernReg.FinalMuxInCfg, (int)FinalMuxInCfgBF.Source, 0, 1),
                new BitFieldDef( (int)WyvernReg.FinalMuxInCfg, (int)FinalMuxInCfgBF.OverForceEnable, 2),
                //
                new BitFieldDef( (int)WyvernReg.DllClkCreateMark, (int)DllClkCreateMarkBF.Enable, 0 ),
                new BitFieldDef( (int)WyvernReg.DllClkCreateMark, (int)DllClkCreateMarkBF.cfgMin, 1, 6 ),
                new BitFieldDef( (int)WyvernReg.DllClkCreateMark, (int)DllClkCreateMarkBF.cfgMask, 7, 10 ),
                //
				new BitFieldDef( (int)WyvernReg.DllClkCfg, (int)DllClkCfgBF.DataMarkSelect, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.DllClkCfg, (int)DllClkCfgBF.Mode, 3 ),
                new BitFieldDef( (int)WyvernReg.DllClkCfg, (int)DllClkCfgBF.State, 4 ),
                new BitFieldDef( (int)WyvernReg.DllClkCfg, (int)DllClkCfgBF.InvertOutput, 5 ),
                new BitFieldDef( (int)WyvernReg.DllClkCfg, (int)DllClkCfgBF.Delay, 6, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.DllClkWidth, (int)DllClkWidthBF.Width, 0, 15 ),
                
                #endregion

                #region FinalMux

                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.DdrEn, 0 ),
                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.OutMarkEn, 1, 4 ),
                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.DllClkEn, 5 ),
                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.MidScaleLatch, 6 ),
                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.MidScaleOff, 7 ),
                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.Powerdown, 8 ),
                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.TestClkEn, 9 ),
                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.DTestBusSel, 10, 11 ),
                new BitFieldDef( (int)WyvernReg.FinalMuxCfg, (int)FinalMuxCfgBF.DTestBusTiedDownOn, 12 ),
                //
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.FpgaClkCount, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.FpgaClkMux, 3 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.MuxClkOff, 4 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.FpgaClkFromCClk, 5 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.FbEnIClkRx, 6 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.FbEnDiv, 7 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.FbEnMuxClkDelay, 8 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.CClkOff, 9 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.DutyEn, 10 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.WatchdogEn, 11 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.WatchdogTest, 12 ),
                new BitFieldDef( (int)WyvernReg.ClkdCfg, (int)ClkdCfgBF.WatchdogForceEn, 13 ),
                //
                new BitFieldDef( (int)WyvernReg.MuxClkDelayCfg, (int)ClkDelayCfgBF.TestGain, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.MuxClkDelayCfg, (int)ClkDelayCfgBF.TestMode, 3 ),
                //
                new BitFieldDef( (int)WyvernReg.MuxClkDelayCoarseL, (int)ClkDelayCoarseLBF.coarseL, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.MuxClkDelayCoarseU, (int)ClkDelayCoarseUBF.coarseU, 0, 14 ),
                //
                new BitFieldDef( (int)WyvernReg.MuxClkDelayFineA, (int)ClkDelayFineABF.fineA, 0, 2 ),
                //
                new BitFieldDef( (int)WyvernReg.MuxClkDelayFineB, (int)ClkDelayFineBBF.fineB, 0, 14 ),
                //
                new BitFieldDef( (int)WyvernReg.MuxClkDelayTweak, (int)ClkDelayTweakBF.tweak0, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.MuxClkDelayTweak, (int)ClkDelayTweakBF.tweak1, 3, 6 ),
                //
                new BitFieldDef( (int)WyvernReg.CustomLvdsEnable, (int)CustomLvdsEnableBF.OutMarkEn, 0, 3 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsEnable, (int)CustomLvdsEnableBF.FpgaClkEn, 4 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsEnable, (int)CustomLvdsEnableBF.InSyncClkEn, 5 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsEnable, (int)CustomLvdsEnableBF.InRefClkEn, 6, 7 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsEnable, (int)CustomLvdsEnableBF.OutRefClkEn, 8 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsEnable, (int)CustomLvdsEnableBF.SyncDEn, 9 ),
                //
                new BitFieldDef( (int)WyvernReg.CustomLvdsForce, (int)CustomLvdsForceBF.OutMarkForceMode, 0, 3 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsForce, (int)CustomLvdsForceBF.OutMarkForceVal, 4, 7 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsForce, (int)CustomLvdsForceBF.FpgaClkForceMode, 8 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsForce, (int)CustomLvdsForceBF.FpgaClkForceVal, 9 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsForce, (int)CustomLvdsForceBF.SyncDForceMode, 10 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsForce, (int)CustomLvdsForceBF.SyncDForceVal, 11 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsForce, (int)CustomLvdsForceBF.OutRefClkForceMode, 12 ),
                new BitFieldDef( (int)WyvernReg.CustomLvdsForce, (int)CustomLvdsForceBF.OutRefClkVal, 13 ),
                //
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterCfg1, (int)LoopFilterCfg1BF.filtShift, 0, 5 ),
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterCfg1, (int)LoopFilterCfg1BF.invert, 6 ),
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterCfg1, (int)LoopFilterCfg1BF.run, 7 ),
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterCfg1, (int)LoopFilterCfg1BF.load, 8 ),
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterCfg1, (int)LoopFilterCfg1BF.sel, 9 ),
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterCfg1, (int)LoopFilterCfg1BF.thresh, 10, 13 ),
                //
				new BitFieldDef( (int)WyvernReg.IsoLoopFilterCfg2, (int)LoopFilterCfg2BF.loadVal, 0, 10 ),
                //
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterStat1, (int)LoopFilterStat1BF.filtOut, 0, 5 ),
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterStat1, (int)LoopFilterStat1BF.filtValid, 6 ),
                //
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterStat2, (int)LoopFilterStat2BF.delay, 0, 10 ),
                //
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterStatReq, (int)LoopFilterStatReqBF.request, 0 ),
                //
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterStatAck, (int)LoopFilterStatAckBF.acknowledge, 0 ),

                #endregion

                #region DacCfg  

                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.DdrRxEn,         0),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.DllClkRxEn,      1),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.OutMarkRxEn,     2, 3),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.DdrMuxEn,        4),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.TestEn,          5),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.Midscale,        6),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.OutMarkTxEn,     7, 8),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.AoutpOverHighEn, 9),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.AoutpOverLowEn,  10),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.AoutnOverHighEn,  11),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.AoutnOverLowEn,   12),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.OverForceEnable,  13),
                new BitFieldDef( (int)WyvernReg.DacCfg1, (int)DacCfg1BF.MarkRsEn,         14, 15),
                //
                new BitFieldDef( (int)WyvernReg.DacCfg2, (int)DacCfg2BF.CmlIref1, 0, 7 ),
                new BitFieldDef( (int)WyvernReg.DacCfg2, (int)DacCfg2BF.CmlIref2, 8, 15 ),
                //
				new BitFieldDef( (int)WyvernReg.DacCfg3, (int)DacCfg3BF.CmlOscEn1, 0 ),
                new BitFieldDef( (int)WyvernReg.DacCfg3, (int)DacCfg3BF.CmlOscEn2, 1 ),
                new BitFieldDef( (int)WyvernReg.DacCfg3, (int)DacCfg3BF.CmlOscSel, 2 ),
                new BitFieldDef( (int)WyvernReg.DacCfg3, (int)DacCfg3BF.CmlLpfEn1, 3, 4 ),
                new BitFieldDef( (int)WyvernReg.DacCfg3, (int)DacCfg3BF.CmlLpfEn2, 5, 6 ),
                //
				new BitFieldDef( (int)WyvernReg.DacCfg4, (int)DacCfg4BF.DacOutCasGateAdj, 0, 5 ),
                new BitFieldDef( (int)WyvernReg.DacCfg4, (int)DacCfg4BF.DacSecFoldCurAdj, 6, 11 ),
                new BitFieldDef( (int)WyvernReg.DacCfg4, (int)DacCfg4BF.DacRefSliceClkFbEn, 12 ),
                new BitFieldDef( (int)WyvernReg.DacCfg4, (int)DacCfg4BF.ATestLoBusSel, 13 ),
                new BitFieldDef( (int)WyvernReg.DacCfg4, (int)DacCfg4BF.ATestLoBusTiedownOn, 14 ),
                //
				new BitFieldDef( (int)WyvernReg.DacCfg5, (int)DacCfg5BF.CmlIrefp1, 0, 7 ),
                new BitFieldDef( (int)WyvernReg.DacCfg5, (int)DacCfg5BF.CmlIrefp2, 8, 15 ),
                //
				new BitFieldDef( (int)WyvernReg.DacCfg6, (int)DacCfg6BF.CmlSwing1, 0, 5 ),
                new BitFieldDef( (int)WyvernReg.DacCfg6, (int)DacCfg6BF.CmlSwing2, 6, 11 ),

                #endregion

                #region DacAnaClkSys

                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.AClkStatic, 0 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.AClkMux1, 1 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.AClkMux2, 2 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.LClkMux, 3 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.TestClkRxEn, 4 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnAClkDrive, 5 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnAClkMux2, 6 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnGlobalDelay, 7 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnIClkDrive, 8 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnLClkDelay, 9 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnLClkDrive, 10 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnLs, 11 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnTestClkDelay1, 12 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnTestClkDelay2, 13 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.FbEnIClk2Drive, 14 ),
                new BitFieldDef( (int)WyvernReg.ClkaCfg, (int)ClkaCfgBF.IClk2Off, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.LClkDelayCfg, (int)ClkDelayCfgBF.TestGain, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.LClkDelayCfg, (int)ClkDelayCfgBF.TestMode, 3 ),
                //
                new BitFieldDef( (int)WyvernReg.LClkDelayCoarseL, (int)ClkDelayCoarseLBF.coarseL, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.LClkDelayCoarseU, (int)ClkDelayCoarseUBF.coarseU, 0, 14 ),
                //
                new BitFieldDef( (int)WyvernReg.LClkDelayFineA, (int)ClkDelayFineABF.fineA, 0, 2 ),
                //
                new BitFieldDef( (int)WyvernReg.LClkDelayFineB, (int)ClkDelayFineBBF.fineB, 0, 14 ),
                //
                new BitFieldDef( (int)WyvernReg.LClkDelayTweak, (int)ClkDelayTweakBF.tweak0, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.LClkDelayTweak, (int)ClkDelayTweakBF.tweak1, 3, 6 ),
                //
                new BitFieldDef( (int)WyvernReg.GlobalDelayCfg, (int)ClkDelayCfgBF.TestGain, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.GlobalDelayCfg, (int)ClkDelayCfgBF.TestMode, 3 ),
                //
                new BitFieldDef( (int)WyvernReg.GlobalDelayCoarseL, (int)ClkDelayCoarseLBF.coarseL, 0, 15 ),
                //
                new BitFieldDef( (int)WyvernReg.GlobalDelayCoarseU, (int)ClkDelayCoarseUBF.coarseU, 0, 14 ),
                //
                new BitFieldDef( (int)WyvernReg.GlobalDelayFineA, (int)ClkDelayFineABF.fineA, 0, 2 ),
                //
                new BitFieldDef( (int)WyvernReg.GlobalDelayFineB, (int)ClkDelayFineBBF.fineB, 0, 14 ),
                //
                new BitFieldDef( (int)WyvernReg.GlobalDelayTweak, (int)ClkDelayTweakBF.tweak0, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.GlobalDelayTweak, (int)ClkDelayTweakBF.tweak1, 3, 6 ),
                //
                new BitFieldDef( (int)WyvernReg.RsLoopFilterCfg1, (int)LoopFilterCfg1BF.filtShift, 0, 5 ),
                new BitFieldDef( (int)WyvernReg.RsLoopFilterCfg1, (int)LoopFilterCfg1BF.invert, 6 ),
                new BitFieldDef( (int)WyvernReg.RsLoopFilterCfg1, (int)LoopFilterCfg1BF.run, 7 ),
                new BitFieldDef( (int)WyvernReg.RsLoopFilterCfg1, (int)LoopFilterCfg1BF.load, 8 ),
                new BitFieldDef( (int)WyvernReg.RsLoopFilterCfg1, (int)LoopFilterCfg1BF.sel, 9 ),
                new BitFieldDef( (int)WyvernReg.RsLoopFilterCfg1, (int)LoopFilterCfg1BF.thresh, 10, 13 ),
                //
				new BitFieldDef( (int)WyvernReg.RsLoopFilterCfg2, (int)LoopFilterCfg2BF.loadVal, 0, 10 ),
                //
                new BitFieldDef( (int)WyvernReg.RsLoopFilterStat1, (int)LoopFilterStat1BF.filtOut, 0, 5 ),
                new BitFieldDef( (int)WyvernReg.RsLoopFilterStat1, (int)LoopFilterStat1BF.filtValid, 6 ),
                //
                new BitFieldDef( (int)WyvernReg.IsoLoopFilterStat2, (int)LoopFilterStat2BF.delay, 0, 10 ),
                //
                new BitFieldDef( (int)WyvernReg.RsLoopFilterStatReq, (int)LoopFilterStatReqBF.request, 0 ),
                //
                new BitFieldDef( (int)WyvernReg.RsLoopFilterStatAck, (int)LoopFilterStatAckBF.acknowledge, 0 ),
                //
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg10, (int)DllPhaseDetCfg1BF.DelayClk, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg10, (int)DllPhaseDetCfg1BF.DelayDllClk, 3, 6 ),
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg10, (int)DllPhaseDetCfg1BF.DithNreset, 7 ),
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg10, (int)DllPhaseDetCfg1BF.DithScale, 8, 10 ),
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg10, (int)DllPhaseDetCfg1BF.fbEn, 11 ),
                //
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg11, (int)DllPhaseDetCfg1BF.DelayClk, 0, 2 ),
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg11, (int)DllPhaseDetCfg1BF.DelayDllClk, 3, 6 ),
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg11, (int)DllPhaseDetCfg1BF.DithNreset, 7 ),
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg11, (int)DllPhaseDetCfg1BF.DithScale, 8, 10 ),
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg11, (int)DllPhaseDetCfg1BF.fbEn, 11 ),
                //
				new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg20, (int)DllPhaseDetCfg2BF.BiasAdj, 0, 7 ),
                //
                new BitFieldDef( (int)WyvernReg.DllPhaseDetCfg21, (int)DllPhaseDetCfg2BF.BiasAdj, 0, 7 ),
                //
                new BitFieldDef( (int)WyvernReg.ClkaSpare, (int)ClkaSpareBF.FbEnFffSlice, 0 ),
                new BitFieldDef( (int)WyvernReg.ClkaSpare, (int)ClkaSpareBF.WatchdogForceEn, 1 ),
                new BitFieldDef( (int)WyvernReg.ClkaSpare, (int)ClkaSpareBF.FbEnAClkBufMark, 2 ),

				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbA0, (int)DacCurAdjMsbA0BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbA1, (int)DacCurAdjMsbA1BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbA2, (int)DacCurAdjMsbA2BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbA3, (int)DacCurAdjMsbA3BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbA4, (int)DacCurAdjMsbA4BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbA5, (int)DacCurAdjMsbA5BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbA6, (int)DacCurAdjMsbA6BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbA7, (int)DacCurAdjMsbA7BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbA8, (int)DacCurAdjMsbA8BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbA9, (int)DacCurAdjMsbA9BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbA10, (int)DacCurAdjMsbA10BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbA11, (int)DacCurAdjMsbA11BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbA12, (int)DacCurAdjMsbA12BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbA13, (int)DacCurAdjMsbA13BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbA14, (int)DacCurAdjMsbA14BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbA15, (int)DacCurAdjMsbA15BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjPLsbA0, (int)DacCurAdjPLsbA0BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjPLsbA1, (int)DacCurAdjPLsbA1BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjPLsbA2, (int)DacCurAdjPLsbA2BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjPLsbA3, (int)DacCurAdjPLsbA3BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjPLsbA4, (int)DacCurAdjPLsbA4BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjSLsbA0, (int)DacCurAdjSLsbA0BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjSLsbA1, (int)DacCurAdjSLsbA1BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjSLsbA2, (int)DacCurAdjSLsbA2BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjSLsbA3, (int)DacCurAdjSLsbA3BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjSLsbA4, (int)DacCurAdjSLsbA4BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbB0, (int)DacCurAdjMsbB0BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbB1, (int)DacCurAdjMsbB1BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbB2, (int)DacCurAdjMsbB2BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbB3, (int)DacCurAdjMsbB3BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbB4, (int)DacCurAdjMsbB4BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbB5, (int)DacCurAdjMsbB5BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbB6, (int)DacCurAdjMsbB6BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbB7, (int)DacCurAdjMsbB7BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbB8, (int)DacCurAdjMsbB8BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbB9, (int)DacCurAdjMsbB9BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbB10, (int)DacCurAdjMsbB10BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbB11, (int)DacCurAdjMsbB11BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbB12, (int)DacCurAdjMsbB12BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbB13, (int)DacCurAdjMsbB13BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbB14, (int)DacCurAdjMsbB14BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjMsbB15, (int)DacCurAdjMsbB15BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjPLsbB0, (int)DacCurAdjPLsbB0BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjPLsbB1, (int)DacCurAdjPLsbB1BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjPLsbB2, (int)DacCurAdjPLsbB2BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjPLsbB3, (int)DacCurAdjPLsbB3BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjPLsbB4, (int)DacCurAdjPLsbB4BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjSLsbB0, (int)DacCurAdjSLsbB0BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjSLsbB1, (int)DacCurAdjSLsbB1BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjSLsbB2, (int)DacCurAdjSLsbB2BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjSLsbB3, (int)DacCurAdjSLsbB3BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.DacCurAdjSLsbB4, (int)DacCurAdjSLsbB4BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj0, (int)SwDrvOsAdj0BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj0, (int)SwDrvOsAdj0BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj0, (int)SwDrvOsAdj0BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj0, (int)SwDrvOsAdj0BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj0, (int)SwDrvOsAdj0BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj0, (int)SwDrvOsAdj0BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj1, (int)SwDrvOsAdj1BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj1, (int)SwDrvOsAdj1BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj1, (int)SwDrvOsAdj1BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj1, (int)SwDrvOsAdj1BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj1, (int)SwDrvOsAdj1BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj1, (int)SwDrvOsAdj1BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj2, (int)SwDrvOsAdj2BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj2, (int)SwDrvOsAdj2BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj2, (int)SwDrvOsAdj2BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj2, (int)SwDrvOsAdj2BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj2, (int)SwDrvOsAdj2BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj2, (int)SwDrvOsAdj2BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj3, (int)SwDrvOsAdj3BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj3, (int)SwDrvOsAdj3BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj3, (int)SwDrvOsAdj3BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj3, (int)SwDrvOsAdj3BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj3, (int)SwDrvOsAdj3BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj3, (int)SwDrvOsAdj3BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj4, (int)SwDrvOsAdj4BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj4, (int)SwDrvOsAdj4BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj4, (int)SwDrvOsAdj4BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj4, (int)SwDrvOsAdj4BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj4, (int)SwDrvOsAdj4BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj4, (int)SwDrvOsAdj4BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj5, (int)SwDrvOsAdj5BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj5, (int)SwDrvOsAdj5BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj5, (int)SwDrvOsAdj5BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj5, (int)SwDrvOsAdj5BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj5, (int)SwDrvOsAdj5BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj5, (int)SwDrvOsAdj5BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj6, (int)SwDrvOsAdj6BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj6, (int)SwDrvOsAdj6BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj6, (int)SwDrvOsAdj6BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj6, (int)SwDrvOsAdj6BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj6, (int)SwDrvOsAdj6BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj6, (int)SwDrvOsAdj6BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj7, (int)SwDrvOsAdj7BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj7, (int)SwDrvOsAdj7BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj7, (int)SwDrvOsAdj7BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj7, (int)SwDrvOsAdj7BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj7, (int)SwDrvOsAdj7BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj7, (int)SwDrvOsAdj7BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj8, (int)SwDrvOsAdj8BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj8, (int)SwDrvOsAdj8BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj8, (int)SwDrvOsAdj8BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj8, (int)SwDrvOsAdj8BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj8, (int)SwDrvOsAdj8BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj8, (int)SwDrvOsAdj8BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj9, (int)SwDrvOsAdj9BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj9, (int)SwDrvOsAdj9BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj9, (int)SwDrvOsAdj9BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj9, (int)SwDrvOsAdj9BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj9, (int)SwDrvOsAdj9BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj9, (int)SwDrvOsAdj9BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj10, (int)SwDrvOsAdj10BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj10, (int)SwDrvOsAdj10BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj10, (int)SwDrvOsAdj10BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj10, (int)SwDrvOsAdj10BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj10, (int)SwDrvOsAdj10BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj10, (int)SwDrvOsAdj10BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj11, (int)SwDrvOsAdj11BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj11, (int)SwDrvOsAdj11BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj11, (int)SwDrvOsAdj11BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj11, (int)SwDrvOsAdj11BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj11, (int)SwDrvOsAdj11BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj11, (int)SwDrvOsAdj11BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj12, (int)SwDrvOsAdj12BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj12, (int)SwDrvOsAdj12BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj12, (int)SwDrvOsAdj12BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj12, (int)SwDrvOsAdj12BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj12, (int)SwDrvOsAdj12BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj12, (int)SwDrvOsAdj12BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj13, (int)SwDrvOsAdj13BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj13, (int)SwDrvOsAdj13BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj13, (int)SwDrvOsAdj13BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj13, (int)SwDrvOsAdj13BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj13, (int)SwDrvOsAdj13BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj13, (int)SwDrvOsAdj13BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj14, (int)SwDrvOsAdj14BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj14, (int)SwDrvOsAdj14BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj14, (int)SwDrvOsAdj14BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj14, (int)SwDrvOsAdj14BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj14, (int)SwDrvOsAdj14BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj14, (int)SwDrvOsAdj14BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj15, (int)SwDrvOsAdj15BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj15, (int)SwDrvOsAdj15BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj15, (int)SwDrvOsAdj15BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj15, (int)SwDrvOsAdj15BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj15, (int)SwDrvOsAdj15BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj15, (int)SwDrvOsAdj15BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj16, (int)SwDrvOsAdj16BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj16, (int)SwDrvOsAdj16BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj16, (int)SwDrvOsAdj16BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj16, (int)SwDrvOsAdj16BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj16, (int)SwDrvOsAdj16BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj16, (int)SwDrvOsAdj16BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj17, (int)SwDrvOsAdj17BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj17, (int)SwDrvOsAdj17BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj17, (int)SwDrvOsAdj17BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj17, (int)SwDrvOsAdj17BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj17, (int)SwDrvOsAdj17BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj17, (int)SwDrvOsAdj17BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj18, (int)SwDrvOsAdj18BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj18, (int)SwDrvOsAdj18BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj18, (int)SwDrvOsAdj18BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj18, (int)SwDrvOsAdj18BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj18, (int)SwDrvOsAdj18BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj18, (int)SwDrvOsAdj18BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj19, (int)SwDrvOsAdj19BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj19, (int)SwDrvOsAdj19BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj19, (int)SwDrvOsAdj19BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj19, (int)SwDrvOsAdj19BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj19, (int)SwDrvOsAdj19BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj19, (int)SwDrvOsAdj19BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj20, (int)SwDrvOsAdj20BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj20, (int)SwDrvOsAdj20BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj20, (int)SwDrvOsAdj20BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj20, (int)SwDrvOsAdj20BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj20, (int)SwDrvOsAdj20BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj20, (int)SwDrvOsAdj20BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj21, (int)SwDrvOsAdj21BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj21, (int)SwDrvOsAdj21BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj21, (int)SwDrvOsAdj21BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj21, (int)SwDrvOsAdj21BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj21, (int)SwDrvOsAdj21BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj21, (int)SwDrvOsAdj21BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj22, (int)SwDrvOsAdj22BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj22, (int)SwDrvOsAdj22BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj22, (int)SwDrvOsAdj22BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj22, (int)SwDrvOsAdj22BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj22, (int)SwDrvOsAdj22BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj22, (int)SwDrvOsAdj22BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj23, (int)SwDrvOsAdj23BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj23, (int)SwDrvOsAdj23BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj23, (int)SwDrvOsAdj23BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj23, (int)SwDrvOsAdj23BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj23, (int)SwDrvOsAdj23BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj23, (int)SwDrvOsAdj23BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj24, (int)SwDrvOsAdj24BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj24, (int)SwDrvOsAdj24BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj24, (int)SwDrvOsAdj24BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj24, (int)SwDrvOsAdj24BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj24, (int)SwDrvOsAdj24BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj24, (int)SwDrvOsAdj24BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj25, (int)SwDrvOsAdj25BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj25, (int)SwDrvOsAdj25BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj25, (int)SwDrvOsAdj25BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj25, (int)SwDrvOsAdj25BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj25, (int)SwDrvOsAdj25BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj25, (int)SwDrvOsAdj25BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj26, (int)SwDrvOsAdj26BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj26, (int)SwDrvOsAdj26BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj26, (int)SwDrvOsAdj26BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj26, (int)SwDrvOsAdj26BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj26, (int)SwDrvOsAdj26BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj26, (int)SwDrvOsAdj26BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj27, (int)SwDrvOsAdj27BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj27, (int)SwDrvOsAdj27BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj27, (int)SwDrvOsAdj27BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj27, (int)SwDrvOsAdj27BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj27, (int)SwDrvOsAdj27BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj27, (int)SwDrvOsAdj27BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj28, (int)SwDrvOsAdj28BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj28, (int)SwDrvOsAdj28BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj28, (int)SwDrvOsAdj28BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj28, (int)SwDrvOsAdj28BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj28, (int)SwDrvOsAdj28BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj28, (int)SwDrvOsAdj28BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj29, (int)SwDrvOsAdj29BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj29, (int)SwDrvOsAdj29BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj29, (int)SwDrvOsAdj29BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj29, (int)SwDrvOsAdj29BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj29, (int)SwDrvOsAdj29BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj29, (int)SwDrvOsAdj29BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj30, (int)SwDrvOsAdj30BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj30, (int)SwDrvOsAdj30BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj30, (int)SwDrvOsAdj30BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj30, (int)SwDrvOsAdj30BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj30, (int)SwDrvOsAdj30BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj30, (int)SwDrvOsAdj30BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj31, (int)SwDrvOsAdj31BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj31, (int)SwDrvOsAdj31BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj31, (int)SwDrvOsAdj31BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj31, (int)SwDrvOsAdj31BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj31, (int)SwDrvOsAdj31BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj31, (int)SwDrvOsAdj31BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj32, (int)SwDrvOsAdj32BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj32, (int)SwDrvOsAdj32BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj32, (int)SwDrvOsAdj32BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj32, (int)SwDrvOsAdj32BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj32, (int)SwDrvOsAdj32BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj32, (int)SwDrvOsAdj32BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj33, (int)SwDrvOsAdj33BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj33, (int)SwDrvOsAdj33BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj33, (int)SwDrvOsAdj33BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj33, (int)SwDrvOsAdj33BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj33, (int)SwDrvOsAdj33BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj33, (int)SwDrvOsAdj33BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj34, (int)SwDrvOsAdj34BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj34, (int)SwDrvOsAdj34BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj34, (int)SwDrvOsAdj34BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj34, (int)SwDrvOsAdj34BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj34, (int)SwDrvOsAdj34BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj34, (int)SwDrvOsAdj34BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj35, (int)SwDrvOsAdj35BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj35, (int)SwDrvOsAdj35BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj35, (int)SwDrvOsAdj35BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj35, (int)SwDrvOsAdj35BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj35, (int)SwDrvOsAdj35BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj35, (int)SwDrvOsAdj35BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj36, (int)SwDrvOsAdj36BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj36, (int)SwDrvOsAdj36BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj36, (int)SwDrvOsAdj36BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj36, (int)SwDrvOsAdj36BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj36, (int)SwDrvOsAdj36BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj36, (int)SwDrvOsAdj36BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj37, (int)SwDrvOsAdj37BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj37, (int)SwDrvOsAdj37BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj37, (int)SwDrvOsAdj37BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj37, (int)SwDrvOsAdj37BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj37, (int)SwDrvOsAdj37BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj37, (int)SwDrvOsAdj37BF.AdjEnBn, 15 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj38, (int)SwDrvOsAdj38BF.AdjA, 0, 5 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj38, (int)SwDrvOsAdj38BF.AdjEnAp, 6 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj38, (int)SwDrvOsAdj38BF.AdjEnAn, 7 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj38, (int)SwDrvOsAdj38BF.AdjB, 8, 13 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj38, (int)SwDrvOsAdj38BF.AdjEnBp, 14 ),
				new BitFieldDef( (int)WyvernReg.SwDrvOsAdj38, (int)SwDrvOsAdj38BF.AdjEnBn, 15 ),				
                #endregion
				
				#region Cal internal used WyvernRegs
				new BitFieldDef( (int)WyvernReg.FinalMuxInP0, (int)FinalMuxInP0BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP1, (int)FinalMuxInP1BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP2, (int)FinalMuxInP2BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP3, (int)FinalMuxInP3BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP4, (int)FinalMuxInP4BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP5, (int)FinalMuxInP5BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP6, (int)FinalMuxInP6BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP7, (int)FinalMuxInP7BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP8, (int)FinalMuxInP8BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP9, (int)FinalMuxInP9BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP10, (int)FinalMuxInP10BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP11, (int)FinalMuxInP11BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP12, (int)FinalMuxInP12BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP13, (int)FinalMuxInP13BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP14, (int)FinalMuxInP14BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP15, (int)FinalMuxInP15BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP16, (int)FinalMuxInP16BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP17, (int)FinalMuxInP17BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP18, (int)FinalMuxInP18BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP19, (int)FinalMuxInP19BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP20, (int)FinalMuxInP20BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP21, (int)FinalMuxInP21BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP22, (int)FinalMuxInP22BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP23, (int)FinalMuxInP23BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP24, (int)FinalMuxInP24BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP25, (int)FinalMuxInP25BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP26, (int)FinalMuxInP26BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP27, (int)FinalMuxInP27BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP28, (int)FinalMuxInP28BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP29, (int)FinalMuxInP29BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP30, (int)FinalMuxInP30BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP31, (int)FinalMuxInP31BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP32, (int)FinalMuxInP32BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP33, (int)FinalMuxInP33BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP34, (int)FinalMuxInP34BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP35, (int)FinalMuxInP35BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP36, (int)FinalMuxInP36BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP37, (int)FinalMuxInP37BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP38, (int)FinalMuxInP38BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP39, (int)FinalMuxInP39BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP40, (int)FinalMuxInP40BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP41, (int)FinalMuxInP41BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP42, (int)FinalMuxInP42BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP43, (int)FinalMuxInP43BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP44, (int)FinalMuxInP44BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP45, (int)FinalMuxInP45BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP46, (int)FinalMuxInP46BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP47, (int)FinalMuxInP47BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP48, (int)FinalMuxInP48BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP49, (int)FinalMuxInP49BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP50, (int)FinalMuxInP50BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP51, (int)FinalMuxInP51BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP52, (int)FinalMuxInP52BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP53, (int)FinalMuxInP53BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP54, (int)FinalMuxInP54BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP55, (int)FinalMuxInP55BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP56, (int)FinalMuxInP56BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP57, (int)FinalMuxInP57BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP58, (int)FinalMuxInP58BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP59, (int)FinalMuxInP59BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP60, (int)FinalMuxInP60BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP61, (int)FinalMuxInP61BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP62, (int)FinalMuxInP62BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInP63, (int)FinalMuxInP63BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS0, (int)FinalMuxInS0BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS1, (int)FinalMuxInS1BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS2, (int)FinalMuxInS2BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS3, (int)FinalMuxInS3BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS4, (int)FinalMuxInS4BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS5, (int)FinalMuxInS5BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS6, (int)FinalMuxInS6BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS7, (int)FinalMuxInS7BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS8, (int)FinalMuxInS8BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS9, (int)FinalMuxInS9BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS10, (int)FinalMuxInS10BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS11, (int)FinalMuxInS11BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS12, (int)FinalMuxInS12BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS13, (int)FinalMuxInS13BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS14, (int)FinalMuxInS14BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS15, (int)FinalMuxInS15BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS16, (int)FinalMuxInS16BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS17, (int)FinalMuxInS17BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS18, (int)FinalMuxInS18BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS19, (int)FinalMuxInS19BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS20, (int)FinalMuxInS20BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS21, (int)FinalMuxInS21BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS22, (int)FinalMuxInS22BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS23, (int)FinalMuxInS23BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS24, (int)FinalMuxInS24BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS25, (int)FinalMuxInS25BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS26, (int)FinalMuxInS26BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS27, (int)FinalMuxInS27BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS28, (int)FinalMuxInS28BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS29, (int)FinalMuxInS29BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS30, (int)FinalMuxInS30BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS31, (int)FinalMuxInS31BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS32, (int)FinalMuxInS32BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS33, (int)FinalMuxInS33BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS34, (int)FinalMuxInS34BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS35, (int)FinalMuxInS35BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS36, (int)FinalMuxInS36BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS37, (int)FinalMuxInS37BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS38, (int)FinalMuxInS38BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS39, (int)FinalMuxInS39BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS40, (int)FinalMuxInS40BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS41, (int)FinalMuxInS41BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS42, (int)FinalMuxInS42BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS43, (int)FinalMuxInS43BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS44, (int)FinalMuxInS44BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS45, (int)FinalMuxInS45BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS46, (int)FinalMuxInS46BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS47, (int)FinalMuxInS47BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS48, (int)FinalMuxInS48BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS49, (int)FinalMuxInS49BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS50, (int)FinalMuxInS50BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS51, (int)FinalMuxInS51BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS52, (int)FinalMuxInS52BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS53, (int)FinalMuxInS53BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS54, (int)FinalMuxInS54BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS55, (int)FinalMuxInS55BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS56, (int)FinalMuxInS56BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS57, (int)FinalMuxInS57BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS58, (int)FinalMuxInS58BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS59, (int)FinalMuxInS59BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS60, (int)FinalMuxInS60BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS61, (int)FinalMuxInS61BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS62, (int)FinalMuxInS62BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInS63, (int)FinalMuxInS63BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM0, (int)FinalMuxInM0BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM1, (int)FinalMuxInM1BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM2, (int)FinalMuxInM2BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM3, (int)FinalMuxInM3BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM4, (int)FinalMuxInM4BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM5, (int)FinalMuxInM5BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM6, (int)FinalMuxInM6BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM7, (int)FinalMuxInM7BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM8, (int)FinalMuxInM8BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM9, (int)FinalMuxInM9BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM10, (int)FinalMuxInM10BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM11, (int)FinalMuxInM11BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM12, (int)FinalMuxInM12BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM13, (int)FinalMuxInM13BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM14, (int)FinalMuxInM14BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM15, (int)FinalMuxInM15BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM16, (int)FinalMuxInM16BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM17, (int)FinalMuxInM17BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM18, (int)FinalMuxInM18BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM19, (int)FinalMuxInM19BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM20, (int)FinalMuxInM20BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM21, (int)FinalMuxInM21BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM22, (int)FinalMuxInM22BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM23, (int)FinalMuxInM23BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM24, (int)FinalMuxInM24BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM25, (int)FinalMuxInM25BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM26, (int)FinalMuxInM26BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM27, (int)FinalMuxInM27BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM28, (int)FinalMuxInM28BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM29, (int)FinalMuxInM29BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM30, (int)FinalMuxInM30BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM31, (int)FinalMuxInM31BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM32, (int)FinalMuxInM32BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM33, (int)FinalMuxInM33BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM34, (int)FinalMuxInM34BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM35, (int)FinalMuxInM35BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM36, (int)FinalMuxInM36BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM37, (int)FinalMuxInM37BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM38, (int)FinalMuxInM38BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM39, (int)FinalMuxInM39BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM40, (int)FinalMuxInM40BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM41, (int)FinalMuxInM41BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM42, (int)FinalMuxInM42BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM43, (int)FinalMuxInM43BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM44, (int)FinalMuxInM44BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM45, (int)FinalMuxInM45BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM46, (int)FinalMuxInM46BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM47, (int)FinalMuxInM47BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM48, (int)FinalMuxInM48BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM49, (int)FinalMuxInM49BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM50, (int)FinalMuxInM50BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM51, (int)FinalMuxInM51BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM52, (int)FinalMuxInM52BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM53, (int)FinalMuxInM53BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM54, (int)FinalMuxInM54BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM55, (int)FinalMuxInM55BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM56, (int)FinalMuxInM56BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM57, (int)FinalMuxInM57BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM58, (int)FinalMuxInM58BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM59, (int)FinalMuxInM59BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM60, (int)FinalMuxInM60BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM61, (int)FinalMuxInM61BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM62, (int)FinalMuxInM62BF.Value, 0, 15 ),
				new BitFieldDef( (int)WyvernReg.FinalMuxInM63, (int)FinalMuxInM63BF.Value, 0, 15 ),
				#endregion
            };

        #endregion constants/enums/lookup tables

        #region Properties

        internal IRegister DacConfig1
        {
            get;
            private set;
        }

        internal IRegister FinalMuxInCfg
        {
            get;
            private set;
        }

        internal IRegister Scale
        {
            get;
            private set;
        }

        #endregion Properties

        #region constructors

        /// <summary>
        /// Creates the module specific carrier register set "from scratch" using RegFactory and the definitions located
        /// in CommonCarrierRegisterSet (namely, mRegisterDefinitions and mBitFieldDefinitions).
        /// </summary>
        /// <param name="manager">IRegManager instance ... the created registers will be added as a group to this</param>
        /// <param name="module">The module these register are for (used for name, RegDriver, ...)</param>
        /// <param name="groupName">Name of the group the created registers will be registered as in IRegManager</param>
        /// <param name="baseAddr">Base address for register definitions (normally 0)</param>
        public WyvernRegisterSet(String moduleName, string groupName, IRegister wyvernRegister)
        {
            // Create the registers and bitfields...
            //RegFactory wyvernRegFactory = new RegFactory(module, AddrDataReg32.ConstructReg);
            RegFactory wyvernRegFactory = new RegFactory();

            // Custom common plugin registers...
            List<RegDef> registerDefs = new List<RegDef>(mRegisterDefinitions);
            List<BitFieldDef> bitFieldDefs = new List<BitFieldDef>(mBitFieldDefinitions);

            //object[] addrDataRegFields = 
            //{
            //    wyvernRegister.GetField( (int)WyvernBF.Address ),
            //    wyvernRegister.GetField( (int)WyvernBF.Value ),
            //    wyvernRegister.GetField( (int)WyvernBF.RW ),
            //    0,
            //    1
            //};
            object[] addrDataRegFields = 
            {
                0
            };

            Registers = wyvernRegFactory.CreateRegArray(mRegisterDefinitions,
                typeof(WyvernReg),
                moduleName,
                String.Empty,
                addrDataRegFields);

            wyvernRegFactory.CreateBitFields(mBitFieldDefinitions,
                Registers,
                moduleName
                );

            // Add the Wyvern registers to the register manager under the specified group name.
            DacConfig1 = Registers[(int)WyvernReg.DacCfg1];
            FinalMuxInCfg = Registers[(int)WyvernReg.FinalMuxInCfg];
            Scale = Registers[(int)WyvernReg.Scale];
        }

        #endregion constructors

        public void SetInitialValues()
        {
            //base.SetInitialValues();
            Registers[(int)WyvernReg.DacCfg1].Value32 = 0x3e40;
            Registers[(int)WyvernReg.DacCfg2].Fields[(int)DacCfg2BF.CmlIref1].Value = 63;
            Registers[(int)WyvernReg.DacCfg2].Fields[(int)DacCfg2BF.CmlIref2].Value = 63;
            Registers[(int)WyvernReg.DacCfg4].Fields[(int)DacCfg4BF.DacOutCasGateAdj].Value = 32;
            Registers[(int)WyvernReg.DacCfg4].Fields[(int)DacCfg4BF.DacSecFoldCurAdj].Value = 32;
            Registers[(int)WyvernReg.DacCfg5].Fields[(int)DacCfg5BF.CmlIrefp1].Value = 63;
            Registers[(int)WyvernReg.DacCfg5].Fields[(int)DacCfg5BF.CmlIrefp2].Value = 63;
            Registers[(int)WyvernReg.DacCfg6].Fields[(int)DacCfg6BF.CmlSwing1].Value = 32;
            Registers[(int)WyvernReg.DacCfg6].Fields[(int)DacCfg6BF.CmlSwing2].Value = 38;
            Registers[(int)WyvernReg.FinalMuxInCfg].Value32 = 0x0040;
            Registers[(int)WyvernReg.ScaleCfg].Value32 = 0x000b;
            Registers[(int)WyvernReg.Scale].Value32 = 0x4000;
            Registers[(int)WyvernReg.DllClkCfg].Fields[(int)DllClkCfgBF.DataMarkSelect].Value = 6;
            Registers[(int)WyvernReg.DllClkCfg].Fields[(int)DllClkCfgBF.Delay].Value = 256;
            Registers[(int)WyvernReg.DllPhaseDetCfg10].Fields[(int)DllPhaseDetCfg1BF.DelayDllClk].Value = 6;
            Registers[(int)WyvernReg.DllPhaseDetCfg20].Fields[(int)DllPhaseDetCfg2BF.BiasAdj].Value = 63;
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.filtShift].Value = 6;
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Fields[(int)LoopFilterCfg1BF.thresh].Value = 4;
            Registers[(int)WyvernReg.IsoLoopFilterCfg2].Fields[(int)LoopFilterCfg2BF.loadVal].Value = 1024;
            Registers[(int)WyvernReg.ClkdCfg].Fields[(int)ClkdCfgBF.FpgaClkCount].Value = 7;
            Registers[(int)WyvernReg.ClkdCfg].Fields[(int)ClkdCfgBF.FpgaClkMux].Value = 1;
            Registers[(int)WyvernReg.ClkaCfg].Fields[(int)ClkaCfgBF.IClk2Off].Value = 1;

            //reset calibration related registers.
            ResetCalRelatedRegisters();
        }

        /// <summary>
        /// Complete brute force approach to reset the registers to a known state.  Will set all the registers
        /// to the default values as per the ERS.
        /// </summary>
        public void ResetRegisters()
        {
            ResetGeneralRegisters();
            ResetMarkPropagationRegisters();
            ResetGncoRegisters();
            ResetSignalGenRegisters();
            ResetDdrInterpRegisters();
            ResetDemRegisters();
            ResetFinalMuxInRegisters();
            ResetFinalMuxRegisters();
            ResetDacCfgRegisters();
            ResetDacAnaClkSysRegisters();
        }

        public void ResetGeneralRegisters()
        {
            Registers[(int)WyvernReg.Mark].Value32 = 0x0;
            Registers[(int)WyvernReg.CClkGate].Value32 = 0x3ffd;
        }

        public void ResetMarkPropagationRegisters()
        {
            Registers[(int)WyvernReg.InMarkCfg].Value32 = 0x0;
            Registers[(int)WyvernReg.OutMarkCfg0].Value32 = 0x4006;
            Registers[(int)WyvernReg.OutMarkCfg1].Value32 = 0x4006;
            Registers[(int)WyvernReg.OutMarkCfg2].Value32 = 0x4006;
            Registers[(int)WyvernReg.OutMarkCfg3].Value32 = 0x4006;
        }

        public void ResetGncoRegisters()
        {
            Registers[(int)WyvernReg.GNcoCfg].Value32 = 0x0c00;
            Registers[(int)WyvernReg.GNcoDFL].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoDFU].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoPhaseOffsetL].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoPhaseOffsetU].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoScale].Value32 = 0x7fff;
            Registers[(int)WyvernReg.GNcoFreq_0].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoFreq_1].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoFreq_2].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoFreq_3].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoFreq_4].Value32 = 0x1800;
            Registers[(int)WyvernReg.GNcoMarkSel].Value32 = 0x0;
            Registers[(int)WyvernReg.GNcoMarkCfg].Value32 = 0x1555;
        }

        public void ResetSignalGenRegisters()
        {
            Registers[(int)WyvernReg.ScaleCfg].Value32 = 0x000b;
            Registers[(int)WyvernReg.Scale].Value32 = 0x4000;
        }

        public void ResetDdrInterpRegisters()
        {
            Registers[(int)WyvernReg.DdrInterpCfg].Value32 = 0x0;
        }

        public void ResetDemRegisters()
        {
            Registers[(int)WyvernReg.DemCfg].Value32 = 0x0001;
        }

        public void ResetFinalMuxInRegisters()
        {
            Registers[(int)WyvernReg.FinalMuxInCfg].Value32 = 0x0004;
            Registers[(int)WyvernReg.DllClkCreateMark].Value32 = 0x0;
            Registers[(int)WyvernReg.DllClkCfg].Value32 = 0x4006;
            Registers[(int)WyvernReg.DllClkWidth].Value32 = 0x0;
        }

        public void ResetFinalMuxRegisters()
        {
            Registers[(int)WyvernReg.FinalMuxCfg].Value32 = 0x0;
            Registers[(int)WyvernReg.ClkdCfg].Value32 = 0x000f;
            Registers[(int)WyvernReg.MuxClkDelayCfg].Value32 = 0x0;
            Registers[(int)WyvernReg.MuxClkDelayCoarseL].Value32 = 0x0;
            Registers[(int)WyvernReg.MuxClkDelayCoarseU].Value32 = 0x0;
            Registers[(int)WyvernReg.MuxClkDelayFineA].Value32 = 0x0;
            Registers[(int)WyvernReg.MuxClkDelayFineB].Value32 = 0x0;
            Registers[(int)WyvernReg.MuxClkDelayTweak].Value32 = 0x0;
            Registers[(int)WyvernReg.CustomLvdsEnable].Value32 = 0x001f;
            Registers[(int)WyvernReg.CustomLvdsForce].Value32 = 0x000f;
            Registers[(int)WyvernReg.IsoLoopFilterCfg1].Value32 = 0x1006;
            Registers[(int)WyvernReg.IsoLoopFilterCfg2].Value32 = 0x0400;
            Registers[(int)WyvernReg.IsoLoopFilterStatReq].Value32 = 0x0;
        }

        public void ResetDacCfgRegisters()
        {
            Registers[(int)WyvernReg.DacCfg1].Value32 = 0x3e40;
            Registers[(int)WyvernReg.DacCfg2].Value32 = 0x3f3f;
            Registers[(int)WyvernReg.DacCfg3].Value32 = 0x0;
            Registers[(int)WyvernReg.DacCfg4].Value32 = 0x0820;
            Registers[(int)WyvernReg.DacCfg5].Value32 = 0x3f3f;
            Registers[(int)WyvernReg.DacCfg6].Value32 = 0x09a0;
        }

        public void ResetDacAnaClkSysRegisters()
        {
            Registers[(int)WyvernReg.ClkaCfg].Value32 = 0x8000;
            Registers[(int)WyvernReg.LClkDelayCfg].Value32 = 0x0;
            Registers[(int)WyvernReg.LClkDelayCoarseL].Value32 = 0x0;
            Registers[(int)WyvernReg.LClkDelayCoarseU].Value32 = 0x0;
            Registers[(int)WyvernReg.LClkDelayFineA].Value32 = 0x0;
            Registers[(int)WyvernReg.LClkDelayFineB].Value32 = 0x0;
            Registers[(int)WyvernReg.LClkDelayTweak].Value32 = 0x0;
            Registers[(int)WyvernReg.GlobalDelayCfg].Value32 = 0x0;
            Registers[(int)WyvernReg.GlobalDelayCoarseL].Value32 = 0x0;
            Registers[(int)WyvernReg.GlobalDelayCoarseU].Value32 = 0x0;
            Registers[(int)WyvernReg.GlobalDelayFineA].Value32 = 0x0;
            Registers[(int)WyvernReg.GlobalDelayFineB].Value32 = 0x0;
            Registers[(int)WyvernReg.GlobalDelayTweak].Value32 = 0x0;
            Registers[(int)WyvernReg.RsLoopFilterCfg1].Value32 = 0x1006;
            Registers[(int)WyvernReg.RsLoopFilterCfg2].Value32 = 0x0400;
            Registers[(int)WyvernReg.RsLoopFilterStatReq].Value32 = 0x0;
            Registers[(int)WyvernReg.DllPhaseDetCfg10].Value32 = 0x0030;
            Registers[(int)WyvernReg.DllPhaseDetCfg11].Value32 = 0x0030;
            Registers[(int)WyvernReg.DllPhaseDetCfg20].Value32 = 0x003f;
            Registers[(int)WyvernReg.DllPhaseDetCfg21].Value32 = 0x003f;
            Registers[(int)WyvernReg.ClkaSpare].Value32 = 0x0;
        }

        public void ResetCalRelatedRegisters()
        {
            Registers[(int)WyvernReg.DacCurAdjMsbA0].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbA1].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbA2].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbA3].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbA4].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbA5].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbA6].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbA7].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbA8].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbA9].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbA10].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbA11].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbA12].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbA13].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbA14].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbA15].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjPLsbA0].Value32 = 15;
            Registers[(int)WyvernReg.DacCurAdjPLsbA1].Value32 = 112;
            Registers[(int)WyvernReg.DacCurAdjPLsbA2].Value32 = 128;
            Registers[(int)WyvernReg.DacCurAdjPLsbA3].Value32 = 256;
            Registers[(int)WyvernReg.DacCurAdjPLsbA4].Value32 = 512;
            Registers[(int)WyvernReg.DacCurAdjSLsbA0].Value32 = 15;
            Registers[(int)WyvernReg.DacCurAdjSLsbA1].Value32 = 112;
            Registers[(int)WyvernReg.DacCurAdjSLsbA2].Value32 = 128;
            Registers[(int)WyvernReg.DacCurAdjSLsbA3].Value32 = 256;
            Registers[(int)WyvernReg.DacCurAdjSLsbA4].Value32 = 512;
            Registers[(int)WyvernReg.DacCurAdjMsbB0].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbB1].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbB2].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbB3].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbB4].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbB5].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbB6].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbB7].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbB8].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbB9].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbB10].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbB11].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbB12].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbB13].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbB14].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjMsbB15].Value32 = 1024;
            Registers[(int)WyvernReg.DacCurAdjPLsbB0].Value32 = 15;
            Registers[(int)WyvernReg.DacCurAdjPLsbB1].Value32 = 112;
            Registers[(int)WyvernReg.DacCurAdjPLsbB2].Value32 = 128;
            Registers[(int)WyvernReg.DacCurAdjPLsbB3].Value32 = 256;
            Registers[(int)WyvernReg.DacCurAdjPLsbB4].Value32 = 512;
            Registers[(int)WyvernReg.DacCurAdjSLsbB0].Value32 = 15;
            Registers[(int)WyvernReg.DacCurAdjSLsbB1].Value32 = 112;
            Registers[(int)WyvernReg.DacCurAdjSLsbB2].Value32 = 128;
            Registers[(int)WyvernReg.DacCurAdjSLsbB3].Value32 = 256;
            Registers[(int)WyvernReg.DacCurAdjSLsbB4].Value32 = 512;
		Registers[(int)WyvernReg.SwDrvOsAdj0].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj1].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj2].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj3].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj4].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj5].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj6].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj7].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj8].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj9].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj10].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj11].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj12].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj13].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj14].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj15].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj16].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj17].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj18].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj19].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj20].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj21].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj22].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj23].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj24].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj25].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj26].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj27].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj28].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj29].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj30].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj31].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj32].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj33].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj34].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj35].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj36].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj37].Value32=0;
		Registers[(int)WyvernReg.SwDrvOsAdj38].Value32=0;			
            Registers[(int)WyvernReg.FinalMuxInP0].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP1].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP2].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP3].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP4].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP5].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP6].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP7].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP8].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP9].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP10].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP11].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP12].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP13].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP14].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP15].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP16].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP17].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP18].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP19].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP20].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP21].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP22].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP23].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP24].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP25].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP26].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP27].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP28].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP29].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP30].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP31].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP32].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP33].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP34].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP35].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP36].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP37].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP38].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP39].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP40].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP41].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP42].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP43].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP44].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP45].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP46].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP47].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP48].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP49].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP50].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP51].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP52].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP53].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP54].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP55].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP56].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP57].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP58].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP59].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP60].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP61].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP62].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInP63].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS0].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS1].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS2].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS3].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS4].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS5].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS6].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS7].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS8].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS9].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS10].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS11].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS12].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS13].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS14].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS15].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS16].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS17].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS18].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS19].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS20].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS21].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS22].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS23].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS24].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS25].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS26].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS27].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS28].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS29].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS30].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS31].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS32].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS33].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS34].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS35].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS36].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS37].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS38].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS39].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS40].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS41].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS42].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS43].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS44].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS45].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS46].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS47].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS48].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS49].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS50].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS51].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS52].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS53].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS54].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS55].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS56].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS57].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS58].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS59].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS60].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS61].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS62].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInS63].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM0].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM1].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM2].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM3].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM4].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM5].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM6].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM7].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM8].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM9].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM10].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM11].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM12].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM13].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM14].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM15].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM16].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM17].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM18].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM19].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM20].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM21].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM22].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM23].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM24].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM25].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM26].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM27].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM28].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM29].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM30].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM31].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM32].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM33].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM34].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM35].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM36].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM37].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM38].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM39].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM40].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM41].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM42].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM43].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM44].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM45].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM46].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM47].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM48].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM49].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM50].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM51].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM52].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM53].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM54].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM55].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM56].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM57].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM58].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM59].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM60].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM61].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM62].Value32 = 0;
            Registers[(int)WyvernReg.FinalMuxInM63].Value32 = 0;
        }
    }
}
