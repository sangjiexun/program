/*
 台達電機台ip:192.168.154.95
 */
using CNCNetLib;
using DELTA_Form;
using DELTA_Service.CID;
using DELTA_Service.CNC;
using DELTA_Service.Fanuc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DELTA_Service
{
    public partial class Service1 : ServiceBase
    {
        Fanuc_Controll F_Control = new Fanuc_Controll();
        ini_controll ic = new ini_controll();
        //新增機台部分
        //  string DELTA_PATH = System.Environment.CurrentDirectory + "/DELTA_Service.xml";//應用程式的所在路徑，將設為xml的創建地點
        //ini檔路徑需改為預設C槽路徑,C槽資料夾名稱為youngtec_delta     
        ErrorString errorstring = new ErrorString();//call DELTA API,get error message
        private string Connect_Error;
        
        private CNCNetClass CNCnet = new CNCNetClass();
        public Param new_para = new Param();
        public CNCInfoClass info = new CNCInfoClass();
        public static Thread Thread_IsConnect;
        //public static string[] Configs = new string[2] { "CNC1", "CNC2" };
        public static string[] Configs_Fanuc = new string[20];
        public static string[] Configs = new string[20] ;
        //public static string[] Configs_Fanuc = new string[] { "R1_5363_F_R60","R1_5363_F_R62"};
        //是否讀取CSV執行
        //台達電
        public class Basic_BP
        {
            public CNCInfoClass info;
            public string name;   
            public Basic_BP(string name, CNCInfoClass info)
            {
                this.name = name;
                this.info = info;
            }
        }
        //Fanuc
        public class Fanuc_FP
        {
            public Fanuc_Controll fc;
            public string name;
            public Fanuc_FP(string name,Fanuc_Controll fc)
            {
                this.name = name;
                this.fc = fc;
            }
          
        }

        public static List<Basic_BP> Lst_BP = new List<Basic_BP>();
        public static List<Fanuc_FP> Lst_FP = new List<Fanuc_FP>();
        //---------------------------------------------------------get ini connection infomation
        protected int DELTA_Count;
        //第一台連線資訊
        protected string DELTA_localIP1;
        protected string DELTA_remoteIP1;
        protected int DELTA_timeout1;
   
        //第二台連線資訊
        protected string DELTA_localIP2;
        protected string DELTA_remoteIP2;
        protected int DELTA_timeout2;
        //Fanuc CNC
        protected int Fanuc_Count;
        protected string Fanuc_IP;
        protected int Fanuc_Port;
        protected int Fanuc_timeout;
        //------------------------------------------------------------------------ form setting update is or not
        protected string DELTA_newtag;
        protected string DELTA_csvrun;
        protected string DELTA_CSV_PATH;
        //Open File
        OpenFileDialog openFileDialog2 = new OpenFileDialog();
        //CSV

        ListView DeviceList = new ListView();
        ListView[] TagList = new ListView[0];
        public static string StaticTag = "";
        public static string StaticDataType = "";
        public static string StaticGroup = "";
        public static string StaticEditTag = "";
        public static string StaticEditDataType = "";
        public static string StaticEditGroup = "";
        public static string StaticDevice = "";
        public static string StaticEditDevice = "";
        public static ListView[] NewList = null;
        private string[] args = new string[0];

        //*********************************************
        //機台細部ini設定
        public int ini_cutter_Index;
        public int ini_cutter_length;
        public int ini_magazine_magaID;
        public int ini_current_code_CodeCount;
        //*********************************************
        public Service1()
        {
            InitializeComponent();
            this.AutoLog = false;
            //創建Service的EventLog來源
            if (!EventLog.SourceExists("MySource"))//default Eventlog source
            {
                System.Diagnostics.EventLog.CreateEventSource("MySource", "DELTA_eventlog");
            }
            eventLog1.Source = "MySource";
        }

        //OnStart為程式初始讀取區塊,執行4個Function，分別是 read_ini(),Create_xml(),Run_DELTA(),Run_Fanuc()，以及一條執行緒:Thread_IsConnect
        protected override void OnStart(string[] args)
        {
            
            read_ini();//讀取ini設定檔
           
            Create_XML1();//創建XML，包含DELTA以及Fanuc

            Run_DELTA();//執行DELTA
            Run_Fanuc();//執行Fanuc
  
            Thread_IsConnect = new Thread(Check_Run);
           // Thread_IsConnect.Start();//執行執行緒來偵測是否有無斷線
        
        }

        private void Run_DELTA()
        {
            for (int i = 0; i < DELTA_Count; i++)//判斷台數
            {
                Configs[i] = ic.read_ini("DELTA setting", "name" + Convert.ToInt32(i + 1), "CNC1");
                bool exportConfig = false;//false表示為不再建立xml
                string strConfigName = Configs[i];//抓ini裡面所設定的xml名稱
         
                string strApplicationDir = "";

                GetConfigInfo(ref strConfigName, ref strApplicationDir, args, ref exportConfig);

                //設定連線訊息，此處未來應改為可以建立不同IP，甚至是更改IP
                info.SetConnectInfo(DELTA_localIP1, DELTA_remoteIP1, Convert.ToInt32(DELTA_timeout1));
                eventLog1.WriteEntry("CNC" + i.ToString() + " Connection state is localIP" + DELTA_localIP1 + ",remote IP" + DELTA_remoteIP1 + ",timeout" + DELTA_timeout1);
                int isFailed = info.Connect();
                
                if (isFailed.Equals(0))
                {
                    //此類別的List會用於之後各函式的更新執行緒
                    Lst_BP.Add(new Basic_BP(strConfigName, info));

                    //Start the interface to shared memory
                    MemInterface.loop_switch = true;

                    //建立實體
                    MemInterface interface_instance = new MemInterface();
                    interface_instance.main = this;
                    interface_instance.Start(args, strConfigName, strApplicationDir, exportConfig, "DELTA",0);
                    
                }
                else
                {
                    eventLog1.WriteEntry("LocalIP:"+DELTA_localIP1+"  remote IP:"+DELTA_remoteIP1+"\n連線失敗，請重新嘗試\n");
                }
            }
        }

        public void Run_Fanuc()
        {
            for (int i = 0; i < Fanuc_Count; i++)//判斷台數
            {
                Configs_Fanuc[i] = ic.read_ini("Fanuc Setting", "name" + Convert.ToInt32(i + 1), "null");
                try
                {
                    bool exportConfig = false; //false表示為不再建立xml
                    string strConfigName = Configs_Fanuc[i];//抓ini裡面所設定的xml名稱
                    string strApplicationDir = "";

                    GetConfigInfo(ref strConfigName, ref strApplicationDir, args, ref exportConfig);

                    //此類別的List會用於之後各函式的更新執行緒
                    Lst_FP.Add(new Fanuc_FP(strConfigName, F_Control));

                    //Start the interface to shared memory
                    MemInterface.loop_switch_Fanuc = true;

                    //建立實體
                    MemInterface interface_instance = new MemInterface();
                    interface_instance.main = this;
                    interface_instance.Start(args, strConfigName, strApplicationDir, exportConfig, "Fanuc",i);
                  
                }
                catch (Exception ex)
                {
                    eventLog1.WriteEntry("Run Fanuc Error,Result" + ex.ToString());
                }
            }
        }
        //偵測Thread存在與否
        private void Check_Run()
        {
            bool CheckStart = true;
            while (CheckStart)
            {
                if (!MemInterface.ReadThread.IsAlive)
                {
                    eventLog1.WriteEntry("ReadThread屬於停滯狀態");
                }
                if (!MemInterface.newthread.IsAlive)
                {
                    eventLog1.WriteEntry("newthread屬於停滯狀態");
                }
                if (!MemInterface.ReadThread_Fanuc.IsAlive)
                {
                    eventLog1.WriteEntry("ReadThread_Fanuc屬於停滯狀態");
                }
                if (!MemInterface.newthread_Fanuc.IsAlive)
                {
                    eventLog1.WriteEntry("newthread_Fanuc屬於停滯狀態");
                }
                if (MemInterface.ReadThread.IsAlive & MemInterface.newthread.IsAlive & MemInterface.ReadThread.IsAlive & MemInterface.newthread_Fanuc.IsAlive)
                {
                    eventLog1.WriteEntry("Thread皆正常運作");
                }

                if (!info.IsConnect())
                {
                    eventLog1.WriteEntry("DELTA發生斷線，將進行重新連接的動作");
                    //Reconnect
                    int result = info.SetConnectInfo(DELTA_localIP1, DELTA_remoteIP1, Convert.ToInt32(DELTA_timeout1));
                    ErrorString ES = new ErrorString();
                    eventLog1.WriteEntry(ES.errorstring(result));
                }
                Thread.Sleep(300000); //每5分鐘進行執行緒的狀態確認
            }
        }

        private void Create_XML1()
        {

            //Create DELTA XML
            for (int i = 0; i < DELTA_Count; i++)
            {                                                                                                                                     
                Configs[i] = ic.read_ini("DELTA setting","name"+Convert.ToInt32(i+1),"CNC1");//判斷DELTA台數
                bool exportConfig = true;//將exportConfig設定為true即表示會創建xml
                string strConfigName = Configs[i];//根據ini內容決定xml名稱
                eventLog1.WriteEntry("Create XML name"+strConfigName);
                string strApplicationDir = "";
                MemInterface interface_instance = new MemInterface();
                GetConfigInfo(ref strConfigName, ref strApplicationDir, args, ref exportConfig);
                interface_instance.Start(args, strConfigName, strApplicationDir, exportConfig,"DELTA",i);
            }
            //Create Fanuc XML
            //get Fanuc count
            
            //get Fanuc name
       
            for (int i = 0; i < Fanuc_Count; i++)
            {
                Configs_Fanuc[i] = ic.read_ini("Fanuc Setting", "name" + Convert.ToInt32(i + 1), "null");//判斷Fanuc台數
                bool exportConfig_Fanuc = true;//將exportConfig設定為true即表示會創建xml
                string strConfigName_Fanuc = Configs_Fanuc[i];//根據ini內容決定xml名稱
                string strApplicationDir_Fanic = "";
                MemInterface interface_fanuc = new MemInterface();
                GetConfigInfo(ref strConfigName_Fanuc, ref strApplicationDir_Fanic, args, ref exportConfig_Fanuc);
                interface_fanuc.Start(args, strConfigName_Fanuc, strApplicationDir_Fanic, exportConfig_Fanuc, "Fanuc",i);
            }
            
         }
     
        //此部分為CSV檔案的控制，目前沒有用到
        public void NowListData(ListView[] NowList)
        {
            ListView ListView_Device = new ListView();
            TagList = null;
            TagList = NowList;
            for (int i = 0; i < NowList.Length; i++)
            {
                ListView_Device.Items.Add(NowList[i].Name);
            }
        }
        //此部分為CSV檔案的控制，目前沒有用到
        //When using CSV to run CID,system imports CSV automatically
        private void import_CSV()
        {
            //開啟使用者更改過後的CSV檔案
            eventLog1.WriteEntry("Run in import CSV");
            openFileDialog2.FileName = DELTA_CSV_PATH;
            openFileDialog2.Filter = "CSV文件|*.csv|所有文件|*.*";


            string FileName = openFileDialog2.FileName;
            eventLog1.WriteEntry("FileName:"+FileName);
            try
            {
                FileStream fs_ex = new FileStream(FileName.ToString().Trim(), System.IO.FileMode.Open, System.IO.FileAccess.Read);
                StreamReader sr_ex = new StreamReader(fs_ex, System.Text.Encoding.Default);
                ArrayList al = new ArrayList();
                string strLine = "";
                string[] aryLine;
                bool IsFirst = true;
                int columnCount = 0;
                DataTable dt = new DataTable();
                try
                {
                    while ((strLine = sr_ex.ReadLine()) != null)
                    {
                        aryLine = strLine.Split(',');
                        if (IsFirst == true)
                        {
                            IsFirst = false;
                            //columnCount = aryLine.Length;
                            columnCount = 5;
                            for (int i = 0; i < columnCount; i++)
                            {
                                DataColumn dc = new DataColumn(aryLine[i]);
                                dt.Columns.Add(dc);
                            }

                        }
                        else
                        {
                            DataRow dr = dt.NewRow();
                            for (int j = 0; j < columnCount; j++)
                            {
                                if (j < aryLine.Length)
                                    dr[j] = aryLine[j];
                                else
                                    dr[j] = "";
                            }
                            dt.Rows.Add(dr);
                        }
                    }
                    sr_ex.Close();
                    fs_ex.Close();
                    ListLoad();
                    DeviceList = new ListView();
                    if (dt.Rows.Count == 0)
                    {
                        TagList = new ListView[0];
                        return;
                    }
                    else
                    {
                        al.Clear();
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (!al.Contains(dt.Rows[i][0].ToString()))
                                al.Add(dt.Rows[i][0].ToString());
                        }

                        TagList = new ListView[al.Count];

                        for (int i = 0; i < al.Count; i++)
                        {
                            DeviceList.Items.Add(al[i].ToString());                          
                            TagList[i] = new ListView();
                            TagList[i].Name = al[i].ToString();
                        }

                        for (int i = 0; i < TagList.Length; i++)
                        {
                            int TagListCount = 0;
                            for (int j = 0; j < dt.Rows.Count; j++)
                            {
                                if (dt.Rows[j][0].ToString() == TagList[i].Name)
                                {
                                    TagList[i].Items.Add(dt.Rows[j][1].ToString());
                                    TagList[i].Items[TagListCount].SubItems.Add(dt.Rows[j][2].ToString());
                                    TagList[i].Items[TagListCount].SubItems.Add(dt.Rows[j][3].ToString());
                                    TagList[i].Items[TagListCount].SubItems.Add(dt.Rows[j][4].ToString());
                                    TagListCount++;
                                }
                            }
                        }

                    }
                    eventLog1.WriteEntry("import CSV success");
                }
                catch(Exception ex)
                {
                    ListLoad();
                    DeviceList = new ListView();
                    if (dt.Rows.Count == 0)
                    {
                        TagList = new ListView[0];
                        return;
                    }
                    eventLog1.WriteEntry("import CSV fail(inside),result:"+ex);
                }
            }
            catch(Exception ex)
            {
                eventLog1.WriteEntry("import CSV fail(outside),result:" + ex);
            }

        }
        //Service Stop所運行的動作，將loop_switch設定為false，來跳脫while迴圈
        protected override void OnStop()
        {
            //Close Thread Fanuc and DELTA
            MemInterface.loop_switch = false;
            MemInterface.loop_switch_Fanuc = false;
            //Change bool to stop thread 
            //銷毀執行續
            MemInterface.newthread.Abort();
            MemInterface.newthread_Fanuc.Abort();
            MemInterface.ReadThread.Abort();
            MemInterface.ReadThread_Fanuc.Abort();

        }


        public static void GetConfigInfo(ref string strConfigName, ref string strApplicationDir, string[] args, ref bool exportConfig)
        {
            // Parse command line
            if (args.Length > 0)
            {
                // Export Configuration
                if (args[0] == "-exportconfig")
                {
                    exportConfig = true;
                }
                else
                {
                    string msg = "Invalid argument provided. Usage: " +
                        System.Reflection.Assembly.GetExecutingAssembly().GetName().Name +
                        " [-exportconfig]";
                    Console.WriteLine(msg);
                }
            }

            // Important: The following will generate a Configuration Name based on the application's binary file name.
            // This is not a requirement and can be anything the vendor desires.

            // Get the application's directory
            string strTempApplicationDir = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().GetName().CodeBase);

            // Trim the leading chars 'file:\'
            char[] TrimChar = { 'f', 'i', 'l', 'e', ':', '\\' };
            strApplicationDir = strTempApplicationDir.TrimStart(TrimChar);

        }



    

    
        public void write_ini()
        {
            ic.write_ini("Tag State", "newtag", "false");
        }
        /// <summary>
        /// Read ini setting data
        /// </summary>
        public void read_ini() //讀取ini的程式碼，詳情請參閱ini_controll
        {
            try
            {
                string[] IP_Addresses = CNCnet.GetCurrentIPAddress();
                DELTA_localIP1 = ic.read_ini("Connect setting", "localIP1", IP_Addresses[0]);
                DELTA_remoteIP1 = ic.read_ini("Connect setting","remoteIP1","192.168.154.95");
                DELTA_timeout1 = Convert.ToInt32(ic.read_ini("Connect setting","timeout1",5000.ToString()));
                //----------------------第二CNC連線資訊
                DELTA_localIP2 = ic.read_ini("Connect setting","localIP2",IP_Addresses[0]);
                DELTA_remoteIP2 = ic.read_ini("Connect setting","remoteIP2","192.168.154.95");
                DELTA_timeout2 = Convert.ToInt32(ic.read_ini("Connect setting","timeout2",5000.ToString()));
                //Other information
                DELTA_newtag = ic.read_ini("Tag State","newtag","false");
                DELTA_csvrun = ic.read_ini("Tag State","csvrun","false");
                DELTA_CSV_PATH = ic.read_ini("Tag State","CSVname","NoPath");
                //-----------------Read Fanuc
                Fanuc_IP = ic.read_ini("Fanuc Connect","IP", "192.168.152.57");  //192.168.152.57 is default IP
                Fanuc_Port = Convert.ToInt32(ic.read_ini("Fanuc Connect","port","8193"));//8193 is default port
                Fanuc_timeout = Convert.ToInt32(ic.read_ini("Fanuc Connect","timeout","3"));
                //-----------------------------------------read ini setting ----------------------------
                //cuttre_index的初始值設定為0
                ini_cutter_Index = Convert.ToInt32(ic.read_ini("CNC Information","cutter_Index",0.ToString()));
                //Length的初始值設定為5
                ini_cutter_length = Convert.ToInt32(ic.read_ini("CNC Information","cutter_length",5.ToString()));
                //MagaID 的初始值設定為0
                ini_magazine_magaID = Convert.ToInt32(ic.read_ini("CNC Information", "magazine_magaID", 0.ToString()));
                //current_code_CodeCount的初始值設定為5
                ini_current_code_CodeCount = Convert.ToInt32(ic.read_ini("CNC Information", "current_code_CodeCount", 5.ToString()));
                //eventLog1.WriteEntry("read ini"+ini_magazine_magaID);
                //-----------------------------------------讀取CNC細項設定------------------------------
                Connect_Error = ic.read_ini("Service Error","Connect Error","false");
                //get Fanuc and DELTA count
                DELTA_Count = Convert.ToInt32(ic.read_ini("Count","DELTA","1"));
                Fanuc_Count = Convert.ToInt32(ic.read_ini("Count","Fanuc","2"));
            }
            catch(Exception ex )
            {
                eventLog1.WriteEntry("ini讀取失敗,result:"+ex);
            }
        }
        //This method is detect connect state

      

        public bool NeedRefresh(string state)
        {
            if (state == "true")
            {
                return true;
            }
            else//false
            {
                return false;
            }
        }
        //此部分為CSV檔案的控制，目前沒有用到
        public void ListLoad()
        {
  
          
            DeviceList = null;
            TagList = null;

            StaticTag = "";
            StaticDataType = "";
            StaticGroup = "";
            StaticEditTag = "";
            StaticEditDataType = "";
            StaticEditGroup = "";

            StaticDevice = "";
            StaticEditDevice = "";

        }
    }
    //此部分為CSV檔案的控制，目前沒有用到，所以沒有被呼叫
    class DeviceDataConfig
    {
        public string DeviceName;
        public string DeviceID;
        public string DeviceSharedMemoryDeviceOffset;
        public TagDataConfig[] tagdata = null;

        public DeviceDataConfig(string _DeviceName, string _DeviceID, string _DeviceSharedMemoryDeviceOffset, TagDataConfig[] _tagdata)
        {
            DeviceName = _DeviceName;
            DeviceID = _DeviceID;
            DeviceSharedMemoryDeviceOffset = _DeviceSharedMemoryDeviceOffset;
            tagdata = _tagdata;
        }
    }
    //此部分為CSV檔案的控制，目前沒有用到，所以沒有被呼叫
    class TagDataConfig
    {
        public string TagNmae;
        public string TagAddress;
        public string TagDataType;
        public string TagReadWriteAccess;
        public string TagScanRateMilliseconds;
        public string TagDescription;
        public string TagGroup;
        public TagDataConfig(string _TagNmae, string _TagAddress, string _TagDataType, string _TagReadWriteAccess, string _TagScanRateMilliseconds, string _TagDescription, string _TagGroup)
        {
            TagNmae = _TagNmae;
            TagAddress = _TagAddress;
            TagDataType = _TagDataType;
            TagReadWriteAccess = _TagReadWriteAccess;
            TagScanRateMilliseconds = _TagScanRateMilliseconds;
            TagDescription = _TagDescription;
            TagGroup = _TagGroup;
        }
    }
}
