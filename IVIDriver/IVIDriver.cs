using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Keysight.KtM941x;
using Keysight.KtM941xEx;
using Ivi.Driver;
using SliderDriver;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Keysight.ModularInstruments.KtM9347.Peripherals;
using Keysight.ModularInstruments.KtM9347.Registers;
using System.Reflection;
using System.Threading;
//using KtM9471xExService = Keysight.KtM9471xEx.KtM9471xExService;
using ExService = Keysight.KtM941xEx.KtM941xExService;
using Upconverter = Keysight.KtM941x.IKtM941xSource;
using Downconverter = Keysight.KtM941x.IKtM941xReceiver;
namespace BoardDriver
{
    public class MyEventArgs : EventArgs
    {
        public MyEventArgs(string msg)
        {
            Text = msg;
        }
        public string Text { set; get; }

    }
    public class IVIDriver:IBoard
    {
        #region Board Instance
        public CDigitalBoard DigitalBoard = new CDigitalBoard();

        #endregion 
        public bool Status = true;      
        public event MyEventHandler myEvHdl;
        public delegate void MyEventHandler(object sender, MyEventArgs e);
        public string Product = "M9415B";
        public string options=null, pxi_resource=null;
        public bool idquery, reset;
        public KtM941xEx driverEx;
        public KtM941x driver;
        public KtM941xExResource Resource;
        public ExService mService;
        public Port port;
        public Upconverter Source;
        public Downconverter Receiver;
        public string Serial, MNID="", SourceMNID;
        public IVIDriver(string ProductModel)
        {
            this.Product = ProductModel;
            AddDriverToBoard();
        }
        public IVIDriver()
        {

            AddDriverToBoard();
        }
        public void AddDriverToBoard()
        {
            //foreach (FieldInfo DriverField in this.GetType().GetFields())
            //{

            //    if (DriverField.Name.Contains("Board"))
            //    {
            //        if (IsBoard(DriverField.GetValue(this)))
            //        {
            //            dynamic Board = DriverField.GetValue(this);
            //            Board.GetType().GetField("Driver").SetValue(Board, this);
            //            Board.GetType().GetField("BoardName").SetValue(Board, DriverField.Name);
            //            SetDriverToEquipment(Board);
            //        }
            //    }

            //    CarrierBoard.Wyvern1 = new WyvernRegisterSet("SourceWyvern", "", null);
            //    foreach (Keysight.ModularInstruments.Core.Register.Register Register in CarrierBoard.Wyvern1.Registers)
            //    {
            //        Register.GetType().GetField("driver").SetValue(Register, this);
            //    }
            //    CarrierBoard.Wyvern2 = new WyvernRegisterSet("ReceiverWyvern", "", null);
            //    foreach (Keysight.ModularInstruments.Core.Register.Register Register in CarrierBoard.Wyvern2.Registers)
            //    {
            //        Register.GetType().GetField("driver").SetValue(Register, this);
            //    }
            //    CarrierBoard.Wyvern1Device = new WyvernDevice("Wyvern1", CarrierBoard.Wyvern1, "CLK1_IN_DET");
            //    CarrierBoard.Wyvern2Device = new WyvernDevice("Wyvern2", CarrierBoard.Wyvern2, "CLK2_IN_DET");

            //}
            ////CarrierBoard.GetType().GetField("Driver").SetValue(CarrierBoard, this);
            ////CarrierBoard.GetType().GetField("BoardName").SetValue(CarrierBoard, "CarrierBoard");
        }
        public virtual void SetDriverToEquipment(dynamic board)
        {
            foreach (FieldInfo BoardField in board.GetType().GetFields())
            {
                if (BoardField.GetValue(board) != null)
                {
                    object obj = BoardField.GetValue(board);
                    if (obj.GetType() == typeof(CEquipment))
                    {
                        obj.GetType().GetField("Driver").SetValue(obj, board.Driver);
                        obj.GetType().GetField("BoardName").SetValue(obj, board.BoardName);
                        obj.GetType().GetField("EquipmentName").SetValue(obj, BoardField.Name);
                        obj.GetType().GetField("BoardID").SetValue(obj, board.ID);
                    }
                }
                else
                {
                    continue;
                }

            }
        }
        public bool IsBoard(object obj)
        {
            FieldInfo[] fields = obj.GetType().GetFields();
            foreach (FieldInfo field in fields)
            {
                if (field.GetValue(obj) != null)
                {
                    object obj2 = field.GetValue(obj);
                    if (obj2.GetType().Name == "CEquipment")
                    {
                        return true;
                    }
                }
                else
                {
                    continue;
                }

            }
            return false;
        }
        //public bool Connect(string resource, string options)
        public bool Connect(string resource, bool idquery, bool reset, string options)
        {
            try
            {
                driver = null;
                Output("Connect Module");
                this.pxi_resource = resource;
                this.options = options;
                this.idquery = idquery;
                this.reset = reset;
                driver = new KtM941x(pxi_resource, idquery, reset, "simulate = false,DriverSetup=" + options);
                driverEx = new KtM941xEx((driver).GetInstance());
                mService = driverEx.Service;
                Resource = driverEx.Service.Resources[Product];
                Resource.SetValue("module;SETHEADERACTIVEBOARD", "Digital");
                Resource.Register.Write("Ad9689", "AnlogInput", 4);
                Source = driver.Source;
                Receiver = driver.Receiver;
                ErrorQueryResult error;
                string message = string.Empty;
                do
                {
                    error = driver.Utility.ErrorQuery();
                    if (error.Code != 0)
                    {
                        message = error.Message;
                        //MessageBox.Show(message);
                    }
                } while (error.Code != 0);
                if (driver == null)
                {
                    Output("Connection Fail");
                    Status = false;
                    return Status;

                }
                else
                {
                    //SetValue("Module;SETHEADERACTIVEBOARD", "M9471A MNID");
                    //Thread.Sleep(100);
                    MNID = GetValue("Module;GETMODULEMNID");
                    Output("Connection Succeed");
                    Status = true;
                    return Status;
                }
            }
            catch (Exception ex)
            {
                Output("Connection Fail");
                MessageBox.Show(ex.ToString(), "Error");
                Status = false;
                return Status;
            }



        }

        public void Close()
        {
            if (driver != null)
                driver.Close();
        }
        public void SetValue(string key,string value)
        {
             Resource.SetValue(key, value);

        }
        public string GetValue(string key)
        {
            return Resource.GetValue(key);
        }
        public void Write(string Group, string RegName, long value)
        {
            Resource.Register.Write(Group, RegName, value);
        }
        public  void WriteField(string Group, string RegName, string FieldName, long value)
        {
            Resource.Register.WriteField(Group, RegName, FieldName, value);
        }
        public long ReadField(string Group, string RegName, string FieldName)
        {  
            try
            {
                return Resource.Register.ReadField(Group, RegName, FieldName);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }
        }
        public long Read(string Group,string RegName)
        {
            try
            {
                return Resource.Register.Read(Group, RegName);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return 0;
            }
           
        }
        public double ReadAbus(string AbusName)
        {
            double value = 0;
            double[] measureAbus;
            double sum = 0;
            measureAbus = Resource.ABus.Measure(AbusName, 800, 10, 2, 10);
            for (int j = 0; j < measureAbus.Length; j++)
            {
                sum = sum + measureAbus[j];
            }
            value = sum / measureAbus.Length;
            return value;
        }
        public string[] GetAbusNames(int Category)
        {
            string[] AllAbusNames = Resource.ABus.GetNamesByCategory((ABusNodeCategoryEx)Category).Split(',');
            return AllAbusNames;     
        }
        public void LoadFile(string DiskName,string FileFullName)
        {
            int index = FileFullName.LastIndexOf('\\');
            string FileName  =  FileFullName.Substring(index + 1);
            var fileSystem = driverEx.Service.PrimaryResource.FileSystem;
            fileSystem.ActiveFileSystem = DiskName;
            fileSystem.Load(FileFullName, FileName);
            fileSystem.Apply();
        }
        public void SaveFile(string DiskName, string FileFullName)
        {
            int index = FileFullName.LastIndexOf('\\');
            string FileName = FileFullName.Substring(index + 1);
            var fileSystem = driverEx.Service.PrimaryResource.FileSystem;
            fileSystem.ActiveFileSystem = DiskName;
            fileSystem.Save(FileName,FileFullName);
            fileSystem.Apply();
        }
        public void DeleteFile(string DiskName, string FileName)
        {
            var fileSystem = driverEx.Service.PrimaryResource.FileSystem;
            fileSystem.Delete(FileName);
            fileSystem.Apply();
        }
        //public void WriteDAC(string Group, string DACName, string Command, string Addr, string Value)
        //{
        //    string sValue = Command + Addr + Value;
        //    int val = Convert.ToInt32(sValue, 2);
        //    Write(Group,DACName, Convert.ToInt32(sValue, 2));
        //}
        //======================================For Calibration Function=================================================
        public void WriteCalDate(Board eBoard,int iCorrectionId,int iIndex,double value)
        { 
            if(eBoard==Board.Rx)
            {
                SetValue("CORRECTION;SETBYINDEX;RxCorrectionCollection;" + (iCorrectionId + 1).ToString() + ";" + iIndex.ToString(), value.ToString());
            }
            else if(eBoard==Board.Tx)
            {
                SetValue("CORRECTION;SETBYINDEX;TxCorrectionCollection;" + (iCorrectionId + 1).ToString() + ";" + iIndex.ToString(), value.ToString());
            }

        }
        public double ReadCalDate(Board eBoard,int iCorrectionId,int iIndex)
        {
            double value=0;
            if (eBoard == Board.Rx)
            {
                value=double.Parse(GetValue("CORRECTION;GETBYINDEX;RxCorrectionCollection;" + (iCorrectionId + 1).ToString() + ";" + iIndex.ToString()));
            }
            else if (eBoard == Board.Tx)
            {
                value=double.Parse(GetValue("CORRECTION;GETBYINDEX;TxCorrectionCollection;" + (iCorrectionId + 1).ToString() + ";" + iIndex.ToString()));
            }

            return value;
        }
        public string ReadModuleHeader()
        {
            SetValue("Module;SETHEADERACTIVEBOARD", "Module");
            Thread.Sleep(100);
            string str = GetValue("Module;GETHEADERSTRING");
            return str;
        }
        public void WriteModuleSerial(string serial)
        {
            SetValue("Module;SETHEADERACTIVEBOARD", "Module");
            Thread.Sleep(100);
            string[] Header = GetValue("Module;GETHEADERSTRING").Split(',');
            Header[3] = serial;
            SetValue("Module;SETHEADERACTIVEBOARD", "Module");
            Thread.Sleep(100);
            MessageBox.Show("Module;SETHEADERWRITEWITHOUTAUTH"+":" + string.Join(",", Header));
            SetValue("Module;SETHEADERSTRING", string.Join(",",Header));
            SetValue("Module;SETHEADERWRITEWITHOUTAUTH", "");
        }
        public void WriteModuleOption(long value)
        {
            SetValue("Module;SETHEADERACTIVEBOARD", "Module");
            Thread.Sleep(100);
            string[] Header = GetValue("Module;GETHEADERSTRING").Split(',');
            Header[7] = value.ToString();
            SetValue("Module;SETHEADERACTIVEBOARD", "Module");
            Thread.Sleep(100);
            MessageBox.Show("Module;SETHEADERSTRING" + ":" + string.Join(",", Header));
            SetValue("Module;SETHEADERSTRING", string.Join(",", Header));
            SetValue("Module;SETHEADERWRITEWITHOUTAUTH","");
        }
        public string ReadModuleOption()
        {
            //GETHEADERSTRINGFORCE
            SetValue("Module;SETHEADERACTIVEBOARD", "Module");
            Thread.Sleep(100);
            string str = GetValue("Module;GETHEADERSTRINGFORCE");
            return str.Split(',')[7];
        }
        public string ReadModuleSerial()
        {
            SetValue("Module;SETHEADERACTIVEBOARD", "Module");
            Thread.Sleep(100);
            string str = GetValue("Module;GETHEADERSTRINGFORCE");
            return str.Split(',')[3];
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="board">"Carrier",Tx", "Mul", "PA", "Rx", "RxSub"</param>
        /// <returns></returns>
        public string ReadBoardHeader(string board)
        {
            SetValue("MODULE;SETHEADERACTIVEBOARD", board);
            Thread.Sleep(100);
            string str = GetValue("MODULE;GETHEADERSTRING");
            return str;
        }


        //=======================================================================================
        //=======================================================================================
        //=======================================================================================
        //=======================================================================================
        //=======================================================================================
        public enum Board
        {
            Rx ,
            Tx ,
        }
        public  enum TxCorrection
        {
            LoopbackMeasurements=2,
            PORT_TX_POWER=4,
            PORT_HD_POWER=5,
            SourceOffset=3,
        }
        public  enum TxIndex
        {
            LoopbackMeasurements_Temperature = 2,
            PORT_TX_POWER_Temperature = 6,
            PORT_HD_POWER_Temperature = 8,
            SourceOffset_Temperature = 4,
            LoopbackMeasurements_Cal_Date = 3,
            PORT_TX_POWER_Cal_Date = 7,
            PORT_HD_POWER_Cal_Date = 9,
            SourceOffset_Cal_Date = 5,
            SaveAll =990,
            SaveAllToFlash =891,
        }
        
        public enum RxCorrection
        {
            RxPortCorrection = 2,
            HDPortCorrection = 3,
            FactoryReferenceCorrection = 4,
            FastDynamicCorrection = 6,
        }

        public enum RxCIndex
        {
            RxPortCorrection_Temperature = 2,
            HDPortCorrection_Temperature = 4,
            FactoryReferenceCorrection_Temperature = 6,
            FastDynamicCorrection_Temperature = 10,
            RxPortCorrection_Cal_Date = 3,
            HDPortCorrection_Cal_Date = 5,
            FactoryReferenceCorrection_Cal_Date = 7,
            FastDynamicCorrection_Cal_Date = 11,
            SaveAll = 990,
            SaveAllToFlash = 891,
        }
        public void SetEventHandle(MyEventHandler kk)
        {
            myEvHdl += kk;
            
        }
        protected void OnOutPutData(MyEventArgs e)
        {
            if (myEvHdl != null)
            {
                myEvHdl(this, e); //调用委托
            }
        }
        private void Output(string msg)
        {
            MyEventArgs e = new MyEventArgs(msg);
            OnOutPutData(e);
        }
    }
}
