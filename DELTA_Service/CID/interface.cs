// **************************************************************************
// File:  interface.cs
// Created:  11/30/2009 Copyright (c) 2009
//
// Description:  Based on command line argument, this will initiate configuration
// export or initialize and start the shared memory server.
//
// At least one device entry must be defined in the DeviceTable array. Tags
// are usually defined at compile time. However, devices may be created with
// no tags for applications that will dynamically create tags at runtime.
//
// Shared memory is configured at startup only for devices with defined tags.
// An application that creates tags dynamically would have to release, close,
// and regenerate shared memory. Any application with a reference to shared
// memory, such as KEPServer runtime, would need to stop and release the reference
// before shared memory could be closed.
//
// **************************************************************************

// class library references
using DELTA_Service;
using DELTA_Service.Fanuc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using static DELTA_Service.Service1;

// C# equivalent of aliasing for readability
using DWORD = System.UInt32;

// common namespace for related files in project
namespace DELTA_Form
{
    // This application uses a pointer to unmanaged (shared) memory.
    // You must enable "Allow unsafe code" in project build properties.
    public unsafe class MemInterface
    {
        public Service1 main = null;

        public Device newDeviceGetNextTagInMainLoop;

        private Param Values = new Param();
        public Fanuc_Value fv = new Fanuc_Value();
        
        public static bool loop_switch = true;//for DELTA
        public static bool loop_switch_Fanuc = true;// for Fanic
        // Define the devices that we will use.
        // For test purposes, you may comment out a device you do not wish to create.
        public Device.DEVICEENTRY[] DeviceTable =
            {
            //                         Name,				ID
			//new Device.DEVICEENTRY ("Device1",		        "1"),
			//new Device.DEVICEENTRY ("MotionController1",	"192.168.1.10"),
            new Device.DEVICEENTRY("MC",   "1"), //MC表示為台達電
            };
        public Device.DEVICEENTRY[] DeviceTable_Fanuc =
        {
            new Device.DEVICEENTRY("WC", "2"),//WC表示為Fanuc
        };
        // List of devices
        public List<Device> deviceSet = new List<Device>();

        public int nextDeviceIndex;      // Next device to provide to GetNextTag

        //只有一個Device，所以固定長度為1
        public List<Tag.TAGENTRY>[] tagEntryList = new List<Tag.TAGENTRY>[1];

        //重要:MemInterface實體獨自使用自己的SharedMemServer、Mutex
        // shared memory class and related stream
        public SharedMemServer refSharedMemory = new SharedMemServer();
        public UnmanagedMemoryStream memStream;

        //CSharp mutex handling
        // Create a security object that grants no access.
        public MutexSecurity mSec = new MutexSecurity();

        public Mutex mutex = null;

        private DWORD sharedMemorySize;
        private int maxSharedMemSize = SharedMemServer.MAPSIZE;
        public bool exitFlag = false;

        public static Thread ReadThread = null;
        public static Thread newthread = null;
        public static Thread ReadThread_Fanuc = null;
        public static Thread newthread_Fanuc = null;
        // *************************************************************************************
        //interface 最主要的Function
        public void Start(string[] args, string strConfigName, string strApplicationDir, bool exportConfig,string CNC_Name,int no)
        {
            try
            {
                //Set up a mutex to control access to shared memory
                SetupMutex(strConfigName);//此為xml名稱

                // Open the Shared Memory File with a name
                refSharedMemory.Open(strConfigName);//執行SharedMemory

                // Load the TAGENTRY lists for each device.
                // A device may have no tags defined at startup.
                // At startup, shared memory is configured only for defined tags.
                //判別是DELTA還是Fanuc
                switch (CNC_Name) {
                    case "DELTA":
                        LoadTagEntryLists(CNC_Name);//建立點位
                        LoadTables();
                        break;
                    case "Fanuc":
                        LoadTagEntryLists(CNC_Name);
                        LoadTables_ForFanuc();
                        break;
                    default:
                        main.eventLog1.WriteEntry("Error CNC Name,Name="+CNC_Name);
                        break;
                }
              
                // Load the device and tag tables into lists to initialize shared memory


                // Are we exporting the configuration or running a shared memory server?
                if (exportConfig == true)
                {
                    try
                    {
                        StartExportConfiguration(strApplicationDir, strConfigName);
                    }
                    catch (Exception r)
                    {
                        main.eventLog1.WriteEntry("interface Start Error(create xml),result:"+r.Message);
                        Console.WriteLine(r);
                    }
                }
                else
                {
                    switch (CNC_Name)
                    {
                        case "DELTA":
                            //Get DELTA CNC 
                            Param cid_value = new Param();

                            cid_value.xml_name = strConfigName;

                            List<string> List_Tag = new List<string>();

                            foreach (Tag.TAGENTRY entry in tagEntryList[0])
                            {
                                if (!List_Tag.Contains(FunctionThread(entry.strName)))
                                {
                                    List_Tag.Add(FunctionThread(entry.strName));
                                }
                            }

                            //讀取執行緒
                            foreach (string tag in List_Tag)
                            {
                                //每個函式一個執行緒
                                ReadThread = new Thread(CallingAPI);
                                ReadThread.Start(strConfigName + "/" + tag);
                                
                            }

                            Thread.Sleep(500);

                            newthread = new Thread(mainLoop);
                            newthread.Start();
                            break;
                        case "Fanuc":
                            
                                Fanuc_Controll fc = new Fanuc_Controll(); //建立Fanuc_Controll，用來處理xml_name名稱，以及連線、斷線
                            
                                fc.xml_name = strConfigName;
                                List<string> Fanuc_tag = new List<string>();
                                foreach (Tag.TAGENTRY entry in tagEntryList[0])
                                {
                                    if (!Fanuc_tag.Contains(FunctionThread_Fanuc(entry.strName)))
                                    {
                                        Fanuc_tag.Add(FunctionThread_Fanuc(entry.strName));
                                    }
                                }
                                //Read Thread
                                foreach (string tag in Fanuc_tag)
                                {
                                    ReadThread_Fanuc = new Thread(CallingFanucAPI);
                                    ReadThread_Fanuc.Start(strConfigName + "/" + tag + "/" +no);//以字串分割的方式來判別strConfigName(xml名稱)，tag，no
                                }
                    
                                newthread_Fanuc = new Thread(FanucLoop);
                                newthread_Fanuc.Start();
                            
                          
                                break;
                    }
                   
                    //-----------------------------------------------------------------
                  
                }
            }
            catch (Exception E)
            {
                main.eventLog1.WriteEntry("interface Start Error result:"+E);
            
            }
        } // Start
        //呼叫Fanuc所提供的API，請參閱Fanuc_Value裡面的註解
        public void CallingFanucAPI(object name)
        {
             
                while (loop_switch_Fanuc == true) //loop_switch_Fanuc
                {
                     
                        string[] opo = name.ToString().Split('/');//[xml name/tag]                     
                        fv.Update(opo[1], Service1.Lst_FP.Find(x => x.name == opo[0]).fc, Convert.ToInt32(opo[2]));
                Thread.Sleep(60000);  //經順德要求先把時間修正為10分鐘
            }         
        }
        //藉由_來判斷Tag Name
        private string FunctionThread_Fanuc(string name)
        {
            switch (name.Split('_').Count())
            {
                case 0:
                    return name;

                case 1: 
                    return name;

                case 2:
                    return name;

                default:
                    if (name.Split('_')[0] == "Variable" && name.Split('_')[1] == "MONITOR")
                    {
                        return name.Split('_')[1] + "_" + name.Split('_')[2];
                    }
                    else
                    {
                        return name.Split('_')[0] + "_" + name.Split('_')[1];    //無奈 \('_')/ 超無奈\("_")/ 
                    }
            }
        }
        //呼叫台達電的API，頻率為2S
        public void CallingAPI(object name)
        {
            try
            {
                while (loop_switch == true)
                {
                    string[] opo = name.ToString().Split('/');
                    Thread.Sleep(2000);

                    Values.Update(opo[1], Service1.Lst_BP.Find(x => x.name == opo[0]).info);
                }
            }
            catch (Exception e)
            {
                main.eventLog1.WriteEntry("interface CallingAPI Error,result:"+e.Message);
          
            }
        }

        // *************************************************************************************
     
        //**************************************************************************************
        public void LoadTagEntryLists(string CNC_Name) //這邊藉由CNC_Name來判斷DELTA和Fanuc所產生出來的Tag，如果有要做Tag的變動請在這邊修正後，到各對應的class (Param),(Fanuc_Value)修正對應關係
        {
            switch (CNC_Name) {
                case "DELTA":
                    int deviceNum;
                    //match the tags to the devices in your device table

                    deviceNum = 0; // device 1 (zero-based)
                    if (deviceNum >= 0 && deviceNum < DeviceTable.Count())
                    {
                        //always instantiate the TAGENTRY list
                        tagEntryList[deviceNum] = new List<Tag.TAGENTRY>();

                        //READ_CNCFlag: 讀取CNC加工、Alarm 與RestartAct Flag
                        //依序為Tag名稱，字串長度，陣列row，陣列col，資料型態，讀寫相關權限，註解，Tag Group
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("CNCFlag_WorkingFlag", 0, 0, 0, Value.T_BOOL, AccessType.READWRITE, "是否加工中", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("CNCFlag_AlarmFlag", 0, 0, 0, Value.T_BOOL, AccessType.READWRITE, "是否有警報", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("CNCFlag_RestartAct", 0, 0, 0, Value.T_BYTE, AccessType.READWRITE, "系統重新動作", ""));

                        //READ_CNC_information : CNC基本資訊
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("CNC_information_CncType", 20, 0, 0, Value.T_STRING, AccessType.READWRITE, "CNC Type", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("CNC_information_MaxChannels", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "最大通道數", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("CNC_information_Series", 1, 0, 0, Value.T_STRING, AccessType.READWRITE, "Series (M/T Type)", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("CNC_information_Nc_Ver", 15, 0, 0, Value.T_STRING, AccessType.READWRITE, "NC版本", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("CNC_information_ScrewUnit", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "螺桿單位", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("CNC_information_DisplayUnit", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "HMI顯示單位", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("CNC_information_ApiVersion", 0, 0, 0, Value.T_DWORD, AccessType.READWRITE, "API版本", ""));

                        //READ_status : CNC狀態
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("status_MainProg", 30, 0, 0, Value.T_STRING, AccessType.READWRITE, "主程式檔名", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("status_CurProg", 30, 0, 0, Value.T_STRING, AccessType.READWRITE, "目前執行程式檔名", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("status_ProgPath", 80, 0, 0, Value.T_STRING, AccessType.READWRITE, "執行路徑", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("status_CurSeq", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "目前執行行號", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("status_MDICur", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "MDI模式游標所在行號", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("status_Mode", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "CNC Mode(參考【數值說明】-【CNCMode】)", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("status_Status", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "CNC Status(參考【數值說明】-【CNCStatus】", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("status_IsAlarm", 0, 0, 0, Value.T_BOOL, AccessType.READWRITE, "是否有警報", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("status_IsWorking", 0, 0, 0, Value.T_BOOL, AccessType.READWRITE, "是否有在加工切削", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("status_RestartAct", 0, 0, 0, Value.T_BYTE, AccessType.READWRITE, "系統重新動作(bit定義參考【數值說明】-【CNC Restart Action】)", ""));

                        //READ_NCMOTION : CNC Motion狀態資訊
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_Unit", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "讀取數值單位(參考【數值說明】-【CNC數值單位】)", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iSpSpeed", 0, 0, 0, Value.T_DWORD, AccessType.READWRITE, "主軸轉速", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iFeed", 0, 0, 0, Value.T_DOUBLE, AccessType.READWRITE, "切削進給", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iLoad", 0, 0, 0, Value.T_SHORT, AccessType.READWRITE, "主軸負載(小於0為無效值)", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iActSpeed", 0, 0, 0, Value.T_DWORD, AccessType.READWRITE, "實際轉速", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iActFeed", 0, 0, 0, Value.T_DOUBLE, AccessType.READWRITE, "實際進給", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iDwellTime", 0, 0, 0, Value.T_DWORD, AccessType.READWRITE, "暫停時間", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iMCutter", 0, 0, 0, Value.T_SHORT, AccessType.READWRITE, "主軸刀號", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iCutterLib", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "刀庫號碼", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iCmdCutter", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "主軸刀號", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iStandbyCutter", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "待命刀號", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iStandbyCast", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "待命刀套", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iDData", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "D data", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iHData", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "H data", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iTData", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "T data", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iMData", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "M data", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_GGroup", 0, 1, 20, Value.T_CHAR | Value.T_ARRAY, AccessType.READWRITE, "G Group (ff為無效值)", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iSpeedF", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "倍率 F", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iSpeedRPD", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "倍率 RPD", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iSpeedS", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "倍率 S", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iSpeedJOG", 0, 0, 0, Value.T_DOUBLE, AccessType.READWRITE, "倍率 JOG", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("NCMOTION_iSpeedMPG", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "倍率 MPG", ""));

                        //READ_feed_spindle : 讀取進給率/轉速
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("feed_spindle_OvFeed", 0, 0, 0, Value.T_DOUBLE, AccessType.READWRITE, "切削進給", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("feed_spindle_OvSpindle", 0, 0, 0, Value.T_DWORD, AccessType.READWRITE, "轉速", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("feed_spindle_ActFeed", 0, 0, 0, Value.T_DOUBLE, AccessType.READWRITE, "實際進給", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("feed_spindle_ActSpindle", 0, 0, 0, Value.T_DWORD, AccessType.READWRITE, "實際轉速", ""));

                        //READ_CNC_HostName : 讀取NC主機名稱
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("CNC_HostName_strName", 256, 0, 0, Value.T_STRING, AccessType.READWRITE, "NC主機名稱", ""));


                        //警報資訊------------------------------------------------------------------------------------------------------------------------------------ -
                        //READ_current_alarm_count: 讀取全部目前警報個數
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("current_alarm_count_AlarmCount", 0, 0, 0, Value.T_DWORD, AccessType.READWRITE, "全部目前警報個數", ""));

                        //READ_alm_current: 讀取目前發生警報
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("alm_current_AlarmCount", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "目前發生警報個數", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("alm_current_AlarmCode", 0, 1, 2, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "警報代碼", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("alm_current_AlarmDataLen", 0, 1, 2, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "警報附帶資料長度", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("alm_current_AlarmData", 0, 2, 2, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "警報附帶資料(x:警報Index;y: 附帶資料Index)", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("alm_current_AlarmMsg", 60, 1, 2, Value.T_STRING | Value.T_ARRAY, AccessType.READWRITE, "警報資訊", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("alm_current_AlarmDateTime", 30, 1, 2, Value.T_STRING | Value.T_ARRAY, AccessType.READWRITE, "警報日期時間", ""));
                        //根據順德需求，歷史訊息不需要增加

                        //伺服資訊-------------------------------------------------------------------------------------------------------------------------
                        //READ_servo_current: 讀取伺服軸負載電流
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("servo_current_AxisCount", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "回傳軸個數", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("servo_current_AxisNr", 0, 1, 3, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "軸代碼(參考【數值說明】-【軸代碼】)", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("servo_current_Result", 0, 1, 3, Value.T_BOOL | Value.T_ARRAY, AccessType.READWRITE, "是否讀取成功", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("servo_current_AxisValue", 0, 1, 3, Value.T_LONG | Value.T_ARRAY, AccessType.READWRITE, "讀取數值(%)", ""));

                        //READ_spindle_current: 讀取主軸負載電流
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("spindle_current_AxisCount", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "回傳軸個數", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("spindle_current_AxisNr", 0, 1, 1, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "軸代碼(參考【數值說明】-【軸代碼】)", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("spindle_current_Result", 0, 1, 1, Value.T_BOOL | Value.T_ARRAY, AccessType.READWRITE, "是否讀取成功", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("spindle_current_AxisValue", 0, 1, 1, Value.T_LONG | Value.T_ARRAY, AccessType.READWRITE, "讀取數值(%)", ""));

                        //READ_servo_load: 讀取伺服軸負載
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("servo_load_AxisCount", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "回傳軸個數", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("servo_load_AxisNr", 0, 1, 3, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "軸代碼(參考【數值說明】-【軸代碼】)", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("servo_load_Result", 0, 1, 3, Value.T_BOOL | Value.T_ARRAY, AccessType.READWRITE, "是否讀取成功", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("servo_load_AxisValue", 0, 1, 3, Value.T_LONG | Value.T_ARRAY, AccessType.READWRITE, "讀取數值(%)", ""));

                        //READ_spindle_load: 讀取主軸負載
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("spindle_load_AxisCount", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "回傳軸個數", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("spindle_load_AxisNr", 0, 1, 1, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "軸代碼(參考【數值說明】-【軸代碼】)", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("spindle_load_Result", 0, 1, 1, Value.T_BOOL | Value.T_ARRAY, AccessType.READWRITE, "是否讀取成功", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("spindle_load_AxisValue", 0, 1, 1, Value.T_LONG | Value.T_ARRAY, AccessType.READWRITE, "讀取數值(%)", ""));

                        /*
                        //READ_servo_speed: 讀取伺服軸轉速
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("servo_speed_AxisCount", 0, 0, 0, Value.T_WORD , AccessType.READWRITE, "回傳軸個數", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("servo_speed_AxisNr", 0, 1, 3, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "軸代碼(參考【數值說明】-【軸代碼】)", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("servo_speed_Result", 0, 1, 3, Value.T_BOOL | Value.T_ARRAY, AccessType.READWRITE, "是否讀取成功", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("servo_speed_AxisValue", 0, 1, 3, Value.T_LONG | Value.T_ARRAY, AccessType.READWRITE, "讀取數值(0.1 r/min)", ""));
                        */
                        //READ_spindle_speed: 讀取主軸轉速
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("spindle_speed_AxisCount", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "回傳軸個數", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("spindle_speed_AxisNr", 0, 1, 1, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "軸代碼(參考【數值說明】-【軸代碼】)", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("spindle_speed_Result", 0, 1, 1, Value.T_BOOL | Value.T_ARRAY, AccessType.READWRITE, "是否讀取成功", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("spindle_speed_AxisValue", 0, 1, 1, Value.T_LONG | Value.T_ARRAY, AccessType.READWRITE, "讀取數值(0.1 r/min)", ""));

                        /*
                        //READ_servo_temperature: 讀取伺服軸溫度
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("servo_temperature_AxisCount", 0, 0, 0, Value.T_WORD , AccessType.READWRITE, "回傳軸個數", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("servo_temperature_AxisNr", 0, 1, 3, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "軸代碼(參考【數值說明】-【軸代碼】)", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("servo_temperature_Result", 0, 1, 3, Value.T_BOOL | Value.T_ARRAY, AccessType.READWRITE, "是否讀取成功", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("servo_temperature_AxisValue", 0, 1, 3, Value.T_LONG | Value.T_ARRAY, AccessType.READWRITE, "讀取數值(℃)", ""));
                   */
                        //READ_spindle_temperature: 讀取主軸溫度
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("spindle_temperature_AxisCount", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "回傳軸個數", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("spindle_temperature_AxisNr", 0, 1, 1, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "軸代碼(參考【數值說明】-【軸代碼】)", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("spindle_temperature_Result", 0, 1, 1, Value.T_BOOL | Value.T_ARRAY, AccessType.READWRITE, "是否讀取成功", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("spindle_temperature_AxisValue", 0, 1, 1, Value.T_LONG | Value.T_ARRAY, AccessType.READWRITE, "讀取數值(℃)", ""));

                        //刀具資訊---------------------------------------------------------------------------------------------------------------------------------------------------------------------------

                        /*
                        //READ_cutter_title: 讀取刀具標題列
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("cutter_title_CutterTitle", 30, 1, 5, Value.T_STRING | Value.T_ARRAY, AccessType.READWRITE, "標題列", ""));

                        //READ_cutter_count : 讀取刀具總筆數
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("cutter_count_cutter_count", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "刀具總筆數", ""));
                        */

                        //READ_cutter : 讀取刀具資訊
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("cutter_CutterData", 0, 1, 3, Value.T_DOUBLE | Value.T_ARRAY, AccessType.READWRITE, "讀取刀具資訊陣列(x:刀具Index;y: 標題列Index)", ""));

                        //刀庫資訊---------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                        //READ_magazine_info : 讀取刀庫資訊
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("magazine_info_CutterNum", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "回傳刀套個數", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("magazine_info_CMDCutterID", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "命令刀號", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("magazine_info_StandbyCutterID", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "待命刀號", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("magazine_info_StandbyMagaID", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "待命刀套", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("magazine_info_SPCutterID", 0, 0, 0, Value.T_SHORT, AccessType.READWRITE, "主軸刀號", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("magazine_info_CutterID", 0, 1, 3, Value.T_SHORT | Value.T_ARRAY, AccessType.READWRITE, "刀庫內所有刀套內容 陣列長度為回傳刀套個數", ""));


                        //加工資訊-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                        //READ_processtime : 讀取加工時間
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("processtime_TotalWorkTime", 0, 0, 0, Value.T_DWORD, AccessType.READWRITE, "總加工時間(秒)", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("processtime_SingleWorkTime", 0, 0, 0, Value.T_DWORD, AccessType.READWRITE, "單加工時間(秒)", ""));

                        //READ_part_count : 讀取加工數
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("part_count_target_part_count", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "目標加工數", ""));

                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("part_count_finish_part_count", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "完成加工數", ""));

                        //系統資訊-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                        //READ_System_Time : 讀取系統日期時間
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("System_Time_strSystemTime", 60, 0, 0, Value.T_STRING, AccessType.READWRITE, "系統日期時間", ""));

                        //READ_system_variable : 讀取系統變數
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_variable_SysVarNum", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "回傳全部數值類變數個數", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_variable_AxisVarNum", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "回傳全部軸類變數個數", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_variable_SysVarChannel", 0, 1, 3, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "數值類變數通道值", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_variable_SysVarID", 0, 1, 5, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "數值類變數ID", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_variable_SysVarType", 0, 1, 5, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "數值類變數資料型態", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_variable_SysVarValue", 0, 1, 3, Value.T_BYTE | Value.T_ARRAY, AccessType.READWRITE, "數值類變數數值，一個數值資料為8 bytes。x: 數值類變數index，長度為SysVarNum。y:數值，長度為8", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_variable_AxisVarChannel", 0, 1, 5, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "軸類變數通道值", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_variable_AxisNum", 0, 1, 3, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "軸類變數使用軸個數", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_variable_AxisVarID", 0, 1, 3, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "軸類變數ID", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_variable_AxisVarType", 0, 1, 5, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "軸類變數資料型態", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_variable_AxisID", 0, 1, 5, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "軸類變數軸ID。x: 軸類變數index，長度為AxisVarNum。y: 軸ID，長度為AxisNum", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_variable_AxisVarValue", 0, 1, 3, Value.T_BYTE | Value.T_ARRAY, AccessType.READWRITE, "軸類變數數值", ""));

                        //READ_user_variable : 讀取用戶變數
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("user_variable_VarNum", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "回傳用戶變數個數", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("user_variable_VarReg", 0, 1, 5, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "暫存器數值", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("user_variable_VarValue", 0, 1, 5, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "用戶變數數值", ""));

                        //READ_equip_variable : 讀取設備變數
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("equip_variable_VarNum", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "回傳設備變數個數", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("equip_variable_VarReg", 0, 1, 6, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "暫存器數值", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("equip_variable_VarValue", 0, 1, 6, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "設備變數數值", ""));

                        //READ_system_status : 讀取系統狀態
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_status_ParaCount", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "回傳系統狀態個數", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_status_ParaID", 0, 1, 6, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "系統狀態參數ID回傳陣列長度為ParaCount", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_status_ParaName", 30, 1, 8, Value.T_STRING | Value.T_ARRAY, AccessType.READWRITE, "系統狀態參數名稱回傳陣列長度為ParaCount", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_status_DataType", 0, 1, 6, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "資料型態(參考【數值說明】-【Data Type】)回傳陣列長度為ParaCount", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_status_DataSize", 0, 1, 6, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, " 讀取資料大小(Byte)回傳陣列長度為ParaCount", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("system_status_Data", 0, 1, 3, Value.T_BYTE | Value.T_ARRAY, AccessType.READWRITE, "讀取資料 x: 系統狀態參數Index y: 讀取資料數值", ""));

                        // READ_equip_infomation : 讀取設備資訊
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("equip_infomation_InfoCount", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "回傳設備資訊個數", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("equip_infomation_EquipInfo", 60, 1, 5, Value.T_STRING | Value.T_ARRAY, AccessType.READWRITE, "設備資訊回傳陣列長度為InfoCount", ""));


                        //系統資訊-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                        //READ_nc_pointer : 讀取NC 執行行號
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("nc_pointer_LineNum", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "NC執行行號", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("nc_pointer_MDILineNum", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "MDI模式執行行號", ""));

                        //READ_preview_code: 讀取NC 預讀程式內容
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("preview_code_LineNo", 0, 1, 50, Value.T_DWORD | Value.T_ARRAY, AccessType.READWRITE, "行號", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("preview_code_strCode", 128, 1, 50, Value.T_STRING | Value.T_ARRAY, AccessType.READWRITE, "程式內容", ""));

                        //READ_current_code : 讀取NC 當前程式內容
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("current_code_LineNo", 0, 1, 50, Value.T_DWORD | Value.T_ARRAY, AccessType.READWRITE, "行號", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("current_code_strCode", 128, 1, 50, Value.T_STRING | Value.T_ARRAY, AccessType.READWRITE, "程式內容", ""));

                        //系統監控-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                        //READ_RIO_MONITOR : I/O 監控
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("RIO_MONITOR_RIONum", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "RIO個數", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("RIO_MONITOR_IsEnable", 0, 1, 3, Value.T_BOOL | Value.T_ARRAY, AccessType.READWRITE, "是否啟用", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("RIO_MONITOR_IsConnect", 0, 1, 3, Value.T_BOOL | Value.T_ARRAY, AccessType.READWRITE, "連線狀態", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("RIO_MONITOR_RIOType", 0, 1, 3, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "RIO型態(參考【數值說明】-【RIO Type】) ", ""));

                        //READ_SERVO_MONITOR: 伺服監控
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("SERVO_MONITOR_Num", 0, 0, 0, Value.T_WORD, AccessType.READWRITE, "Port個數", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("SERVO_MONITOR_Channel", 0, 1, 3, Value.T_SHORT | Value.T_ARRAY, AccessType.READWRITE, "通道", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("SERVO_MONITOR_AxisID", 0, 1, 3, Value.T_SHORT | Value.T_ARRAY, AccessType.READWRITE, "軸代碼(參考【數值說明】-【軸代碼】) ", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("SERVO_MONITOR_IsConnect", 0, 1, 3, Value.T_BOOL | Value.T_ARRAY, AccessType.READWRITE, "連線狀態", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("SERVO_MONITOR_IsServoOn", 0, 1, 3, Value.T_BOOL | Value.T_ARRAY, AccessType.READWRITE, "伺服是否備妥", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("SERVO_MONITOR_Load", 0, 1, 3, Value.T_SHORT | Value.T_ARRAY, AccessType.READWRITE, "負載", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("SERVO_MONITOR_Peak", 0, 1, 3, Value.T_SHORT | Value.T_ARRAY, AccessType.READWRITE, "峰值", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("SERVO_MONITOR_MechCoord", 0, 1, 3, Value.T_DOUBLE | Value.T_ARRAY, AccessType.READWRITE, "機械座標", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("SERVO_MONITOR_IsHome", 0, 1, 3, Value.T_BOOL | Value.T_ARRAY, AccessType.READWRITE, "原點狀態", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("SERVO_MONITOR_AbsHomeSet", 0, 1, 3, Value.T_BOOL | Value.T_ARRAY, AccessType.READWRITE, "絕對歸零", ""));
                        tagEntryList[deviceNum].Add(new Tag.TAGENTRY("SERVO_MONITOR_EncoderType", 0, 1, 3, Value.T_WORD | Value.T_ARRAY, AccessType.READWRITE, "編碼器形式(0:增量型; 1:絕對型)", ""));

                        //always assign the device TAGENTRY list
                        DeviceTable[deviceNum].tagEntryList = tagEntryList[deviceNum];
                    }
                    break;
                case "Fanuc":
                    int deviceNum_Fanuc;
                    deviceNum_Fanuc = 0; // device 1 (zero-based)
                    if (deviceNum_Fanuc >= 0 && deviceNum_Fanuc < DeviceTable_Fanuc.Count())
                    {
                        //always instantiate the TAGENTRY list
                        tagEntryList[deviceNum_Fanuc] = new List<Tag.TAGENTRY>();

                        //READ_CNCFlag: 讀取CNC加工、Alarm 與RestartAct Flag
                        tagEntryList[deviceNum_Fanuc].Add(new Tag.TAGENTRY("Angle", 0, 0, 0, Value.T_DOUBLE, AccessType.READWRITE, "微調角度", ""));
                        tagEntryList[deviceNum_Fanuc].Add(new Tag.TAGENTRY("VO", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "主電源電壓", ""));
                        tagEntryList[deviceNum_Fanuc].Add(new Tag.TAGENTRY("VG", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "伺服電壓", ""));
                        tagEntryList[deviceNum_Fanuc].Add(new Tag.TAGENTRY("SG", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "伺服速度", ""));
                        tagEntryList[deviceNum_Fanuc].Add(new Tag.TAGENTRY("CS", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "選擇電路的模式", ""));
                        tagEntryList[deviceNum_Fanuc].Add(new Tag.TAGENTRY("I", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "脈沖放電時間", ""));
                        tagEntryList[deviceNum_Fanuc].Add(new Tag.TAGENTRY("OFF", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "脈沖休止時間", ""));
                        tagEntryList[deviceNum_Fanuc].Add(new Tag.TAGENTRY("WS", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "線速", ""));
                        tagEntryList[deviceNum_Fanuc].Add(new Tag.TAGENTRY("WT", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "線張力", ""));
                        tagEntryList[deviceNum_Fanuc].Add(new Tag.TAGENTRY("FL", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "變頻器頻率 (水壓 )", ""));
                        tagEntryList[deviceNum_Fanuc].Add(new Tag.TAGENTRY("WR", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "水阻值", ""));
                        tagEntryList[deviceNum_Fanuc].Add(new Tag.TAGENTRY("HS_P", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "HS参数", ""));
                        tagEntryList[deviceNum_Fanuc].Add(new Tag.TAGENTRY("AD_P", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "AD参数", ""));
                        tagEntryList[deviceNum_Fanuc].Add(new Tag.TAGENTRY("N_P", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "N參數", ""));
                        tagEntryList[deviceNum_Fanuc].Add(new Tag.TAGENTRY("Server_Mode", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "伺服模式", ""));
                        tagEntryList[deviceNum_Fanuc].Add(new Tag.TAGENTRY("Pro_Volt", 0, 0, 0, Value.T_LONG, AccessType.READWRITE, "加工電壓", ""));

                        //always assign the device TAGENTRY list
                        DeviceTable_Fanuc[deviceNum_Fanuc].tagEntryList = tagEntryList[deviceNum_Fanuc];
                    }
                    break;
            }
        }

        // *************************************************************************************
        public void LoadTables()
        {
           
            // If Shared Memory File opened successfully, go on to define our registers
            
                if (refSharedMemory.IsOpen())
                {
                    byte* sharedMemPtr = (byte*)refSharedMemory.Root.ToPointer();

                    // Create an UnmanagedMemoryStream object using a pointer to unmanaged memory.
                    memStream = new UnmanagedMemoryStream(sharedMemPtr, maxSharedMemSize, maxSharedMemSize, FileAccess.ReadWrite);

                    // Important: Get the lock once.  All Shared Memory Registers will be defined while we own this lock.
                    // This will eliminate the need to repeatedly lock/unlock, causing unnecessary CPU operations.
                    if (mutex.WaitOne() == true)
                    {
                        byte* pSharedMemoryBaseMem = refSharedMemory.pMem; //new mutex testing

                        // Walk device table
                        int nDeviceTableIndex = 0;
                        DWORD nextAvailableDeviceOffset = 0;
                        DWORD nextAvailableTagOffset = 0;

                        while (nDeviceTableIndex < DeviceTable.Length)
                        {
                            // Create new Device
                            //Device device = new Device((Device.DEVICEENTRY)DeviceTable[nDeviceTableIndex]);
                            newDeviceGetNextTagInMainLoop = new Device((Device.DEVICEENTRY)DeviceTable[nDeviceTableIndex]);

                            if (newDeviceGetNextTagInMainLoop.Equals(null))
                            {
                                break;
                            }

                            if (DeviceTable[nDeviceTableIndex].tagEntryList.Count() == 0)
                            {
                                ++nDeviceTableIndex;
                                continue;
                            }

                            // Dynamically assign device's shared memory offset based on the previous device's allocation
                            newDeviceGetNextTagInMainLoop.SetSharedMemoryOffset(nextAvailableDeviceOffset);
                            Trace.WriteLine("Device " + newDeviceGetNextTagInMainLoop.GetName().ToString() +
                                "offset within shared memory = " + newDeviceGetNextTagInMainLoop.GetSharedMemoryOffset().ToString());

                            // Important: For this reference implementation, we will assign each device its offset within Shared Memory.
                            // This means each tag's offset must be relative to it's device's offset.
                            // In a commericial application you can choose to do it this way, or define all tag offsets relative to the
                            // beginning of Shared Memory.  In the latter case, all device offsets would be 0.
                            nextAvailableTagOffset = 0;

                            deviceSet.Clear();

                            // Add device to device set
                            deviceSet.Add(newDeviceGetNextTagInMainLoop);

                            foreach (Tag.TAGENTRY tagEntry in DeviceTable[nDeviceTableIndex].tagEntryList)
                            {
                                nextAvailableTagOffset = newDeviceGetNextTagInMainLoop.AddTag(tagEntry, nextAvailableTagOffset, this);
                            }

                            nextAvailableDeviceOffset += nextAvailableTagOffset;
                            ++nDeviceTableIndex;
                        }
                        // Size of the map is based on the last tag allocation for the last device allocation
                        sharedMemorySize = nextAvailableDeviceOffset;

                        // Release SharedMemory
                        mutex.ReleaseMutex();
                        pSharedMemoryBaseMem = null;
                    } // if (mutex.WaitOne () == true)
                    else
                    {
                        System.Console.WriteLine("CRuntime.Initialize failed to acquirelock");
                    }      
            } //if (refSharedMemory.IsOpen ())
        } // public static void LoadTables ()

        public void LoadTables_ForFanuc()
        {
            // If Shared Memory File opened successfully, go on to define our registers

            if (refSharedMemory.IsOpen())
            {
                byte* sharedMemPtr = (byte*)refSharedMemory.Root.ToPointer();

                // Create an UnmanagedMemoryStream object using a pointer to unmanaged memory.
                memStream = new UnmanagedMemoryStream(sharedMemPtr, maxSharedMemSize, maxSharedMemSize, FileAccess.ReadWrite);

                // Important: Get the lock once.  All Shared Memory Registers will be defined while we own this lock.
                // This will eliminate the need to repeatedly lock/unlock, causing unnecessary CPU operations.
                if (mutex.WaitOne() == true)
                {
                    byte* pSharedMemoryBaseMem = refSharedMemory.pMem; //new mutex testing

                    // Walk device table
                    int nDeviceTableIndex = 0;
                    DWORD nextAvailableDeviceOffset = 0;
                    DWORD nextAvailableTagOffset = 0;

                    while (nDeviceTableIndex < DeviceTable_Fanuc.Length)
                    {
                        // Create new Device
                        //Device device = new Device((Device.DEVICEENTRY)DeviceTable[nDeviceTableIndex]);
                        newDeviceGetNextTagInMainLoop = new Device((Device.DEVICEENTRY)DeviceTable_Fanuc[nDeviceTableIndex]);

                        if (newDeviceGetNextTagInMainLoop.Equals(null))
                        {
                            break;
                        }

                        if (DeviceTable_Fanuc[nDeviceTableIndex].tagEntryList.Count() == 0)
                        {
                            ++nDeviceTableIndex;
                            continue;
                        }

                        // Dynamically assign device's shared memory offset based on the previous device's allocation
                        newDeviceGetNextTagInMainLoop.SetSharedMemoryOffset(nextAvailableDeviceOffset);
                        Trace.WriteLine("Device " + newDeviceGetNextTagInMainLoop.GetName().ToString() +
                            "offset within shared memory = " + newDeviceGetNextTagInMainLoop.GetSharedMemoryOffset().ToString());

                        // Important: For this reference implementation, we will assign each device its offset within Shared Memory.
                        // This means each tag's offset must be relative to it's device's offset.
                        // In a commericial application you can choose to do it this way, or define all tag offsets relative to the
                        // beginning of Shared Memory.  In the latter case, all device offsets would be 0.
                        nextAvailableTagOffset = 0;

                        deviceSet.Clear();

                        // Add device to device set
                        deviceSet.Add(newDeviceGetNextTagInMainLoop);

                        foreach (Tag.TAGENTRY tagEntry in DeviceTable_Fanuc[nDeviceTableIndex].tagEntryList)
                        {
                            nextAvailableTagOffset = newDeviceGetNextTagInMainLoop.AddTag(tagEntry, nextAvailableTagOffset, this);
                        }

                        nextAvailableDeviceOffset += nextAvailableTagOffset;
                        ++nDeviceTableIndex;
                    }
                    // Size of the map is based on the last tag allocation for the last device allocation
                    sharedMemorySize = nextAvailableDeviceOffset;

                    // Release SharedMemory
                    mutex.ReleaseMutex();
                    pSharedMemoryBaseMem = null;
                } // if (mutex.WaitOne () == true)
                else
                {
                    System.Console.WriteLine("CRuntime.Initialize failed to acquirelock");
                }
            } //if (refSharedMemory.IsOpen ())
        } // public static void LoadTables

        // *************************************************************************************
        public void mainLoop()//此mainloop是for台達電的
        {
            if (exitFlag == true) // may be set after exporting config
            {
                return;
            }
            Thread.Sleep(1);

            Console.WriteLine("CIDA C# Reference Implementation is currently running.");
            Console.WriteLine("Press 'q' to quit");

            // **** set up and enter the main scan loop ****
            int nRC = TagData.SMRC_NO_ERROR;
            byte* pSharedMemoryBaseMem = null;
            Tag refTag = null;
            //string msg = "";
            int lockCount = 0;

            while (loop_switch == true)
            {
                // loop til signaled to quit
                if (exitFlag == true) //the thread should set this
                    break;
                Thread.Sleep(5);
                // refTag is assigned after Read/Write to "Device" and Process Read/Write Response (Shared Memory).
                // The reason we don't assign the tag now is that we would need to lock Shared Memory to check for pending requests,
                // unlock Shared Memory, read/write to "device", then lock Shared Memory again to set responses.  Locking
                // and unlocking Shared Memory twice for every tag is not desirable.  The side effect is that once a tag is assigned
                // we will need to wait one tick before we can perform the read/write.
                if (refTag != null)
                {
                    // **************************
                    // Read/Write to “Device”
                    // **************************
                    if (refTag.tagWriteRequestPending)
                    {
                        // Important: In a commercial application, this is where you would send the write request to the device.
                        // Since data is simulated, the write response is immediately available.

                        refTag.tagReadData.value = refTag.tagWriteData.value;       // assign value written to value to be read
                        refTag.tagReadData.quality = refTag.tagWriteData.quality;
                        refTag.tagReadData.timeStamp = refTag.tagWriteData.timeStamp;

                        refTag.tagWriteData.errorCode = 0;
                        refTag.tagWriteData.quality = TagData.OPC_QUALITY_GOOD_NON_SPECIFIC;
                        GetFtNow(ref refTag.tagWriteData.timeStamp);

                        refTag.tagWriteRequestPending = false;
                        refTag.tagWriteResponsePending = true;
                    }

                    if (refTag.tagReadRequestPending)
                    {
                        // Important: In a commercial application, this is where you would send the read request to the device
                        // Since data is simulated, the read response is immediately available.
                        if (refTag.IsWriteable() == true)
                        {
                            try
                            {
                                refTag.tagReadData.value.SendData(refTag.GetName(), Values);
                            }
                            catch (Exception e)
                            {
                                main.eventLog1.WriteEntry("interface mainloop Error,result:"+e.Message);
                            }
                        }
                        refTag.tagReadData.errorCode = 0;
                        refTag.tagReadData.quality = TagData.OPC_QUALITY_GOOD_NON_SPECIFIC;
                        GetFtNow(ref refTag.tagReadData.timeStamp);

                        refTag.tagReadRequestPending = false;
                        refTag.tagReadResponsePending = true;
                    }

                    // ********************************************
                    // Process Read/Write Response (Shared Memory)
                    // ********************************************
                    if (refTag.tagWriteResponsePending == true || refTag.tagReadResponsePending == true)
                    {
                        Debug.Assert(pSharedMemoryBaseMem == null);

                        if (mutex.WaitOne() == true) //if we lock
                        {
                            pSharedMemoryBaseMem = refSharedMemory.pMem;
                        }

                        // Process responses only if we have access to Shared Memory (valid pointer)
                        if (pSharedMemoryBaseMem == null)
                        {
                            mutex.ReleaseMutex();
                            continue;
                        }

                        if (refTag.tagWriteResponsePending)
                        {
                            // Get the write response pending flag so we can ASSERT that its not set.
                            bool bWriteResponsePending = false;
                            Register.GetWriteResponsePending(memStream, refTag.GetSharedMemoryOffset(), ref bWriteResponsePending);

#if TRACE_SM_ACCESS
                            msg = string.Format ("{0,8:0D}: Tag " +
                                "{1,0:T}: GetWriteResponsePending nRC = " +
                                    "{2,0:D}, bWriteResponsePending = {3,0:D}", Device.GetTickCount (), refTag.GetName (), nRC, bWriteResponsePending);
                            Trace.WriteLine (msg);
#endif//TRACE_SM_ACCESS

                            if (nRC == TagData.SMRC_NO_ERROR)
                            {
                                // CID driver should not issue a write before completing the last write
                                Debug.Assert(!bWriteResponsePending);
                                Register.SetWriteResponse(memStream, refTag.GetSharedMemoryOffset(), refTag.tagWriteData.errorCode != 0, refTag.tagWriteData.errorCode, refTag.tagWriteData.quality, refTag.tagWriteData.timeStamp);

#if TRACE_SM_ACCESS
                                msg = string.Format ("{0,8:0D}: Tag " +
                                    "{1,0:T}: SetWriteResponse nRC = " +
                                        "{2,0:D}", Device.GetTickCount (), refTag.GetName (), nRC);
                                Trace.WriteLine (msg);
#endif//TRACE_SM_ACCESS
                                if (nRC == TagData.SMRC_NO_ERROR)
                                {
                                    refTag.tagWriteResponsePending = false;
                                }
                            }
                        } // if write response pending

                        if (refTag.tagReadResponsePending == true)
                        {
                            // Get the read response pending flag so we can ASSERT that its not set.
                            bool bReadResponsePending = false;

                            Register.GetReadResponsePending(memStream, refTag.GetSharedMemoryOffset(), ref bReadResponsePending);

#if TRACE_SM_ACCESS
                            msg = string.Format ("{0,8:0D}: Tag " +
                                "{1,0:T}: GetReadResponsePending nRC = " +
                                    "{2,0:D}, bReadResponsePending = {3,0:D}", Device.GetTickCount (), refTag.GetName (), nRC, bReadResponsePending);
                            Trace.WriteLine (msg);
#endif//TRACE_SM_ACCESS

                            if (nRC == TagData.SMRC_NO_ERROR)
                            {
                                // CID driver should not issue a read before completing the last read.
                                Debug.Assert(!bReadResponsePending);
                                Register.SetReadResponse(memStream, refTag.GetSharedMemoryOffset(), refTag.tagReadData.value, refTag.tagReadData.errorCode != 0, refTag.tagReadData.errorCode, refTag.tagReadData.quality, refTag.tagReadData.timeStamp);

#if TRACE_SM_ACCESS
                                msg = string.Format ("{0,8:0D}: Tag " +
                                    "{1,0:T}: SetReadResponse nRC = " +
                                        "{2,0:D}", Device.GetTickCount (), refTag.GetName (), nRC);
                                Trace.WriteLine (msg);
#endif//TRACE_SM_ACCESS

                                if (nRC == TagData.SMRC_NO_ERROR)
                                {
                                    refTag.tagReadResponsePending = false;
                                }
                            }
                        } // if read response pending
                    } // if read or write responses are pending
                } // if (refTag != null)

                // Important: In a commercial application, you may not want to process one tag at a time as this reference implementation does.  Instead you would
                // want to process as many tags as possible, processing requests and responses.

                try
                {
                    // Get our next tag to process.
                    refTag = GetNextTag();
                }
                catch (Exception e)
                {
                    main.eventLog1.WriteEntry("interface mainloop(GetNextTag)error,result:"+e.Message);
                }

                if (refTag != null)
                {
                    // ********************************************
                    // Process Read/Write Requests (Shared Memory)
                    // ********************************************

                    // May have been locked above
                    if (pSharedMemoryBaseMem == null)
                    {
                        try
                        {
                            if (mutex.WaitOne() == true)
                            {
                                lockCount++;
                                pSharedMemoryBaseMem = refSharedMemory.pMem;
                            }
                        }
                        catch (AbandonedMutexException ex)
                        {
                            main.eventLog1.WriteEntry("Exception on return from WaitOne.\n" + ex.Message);
                            Console.WriteLine("Exception on return from WaitOne." +
                                "\r\n\tMessage: {0}", ex.Message);
                        }
                    }
                    // Process requests only if we have access to Shared Memory (valid pointer)
                    if (pSharedMemoryBaseMem == null)
                    {
                        if (lockCount > 0)
                        {
                            mutex.ReleaseMutex();
                            lockCount--;
                        }
                        continue;
                    }

                    // Check for pending write requests
                    bool WriteRequestPending = false;
                    nRC = Register.GetWriteRequestPending(memStream, refTag.GetSharedMemoryOffset(), ref WriteRequestPending);

#if TRACE_SM_ACCESS
                msg = string.Format ("{0,8:0D}: Tag " +
                    "{1,0:T}: GetWriteRequestPending nRC = " +
                        "{2,0:D}, WriteRequestPending = {3,0:D}", Device.GetTickCount (), refTag.GetName (), nRC, bWriteRequestPending);
                Trace.WriteLine (msg);
#endif//TRACE_SM_ACCESS

                    if (nRC == TagData.SMRC_NO_ERROR)
                    {
                        if (WriteRequestPending)
                        {
                            nRC = Register.GetWriteRequest(memStream, refTag.GetSharedMemoryOffset(), ref refTag.tagWriteData.value, ref refTag.tagWriteData.quality, ref refTag.tagWriteData.timeStamp);

#if TRACE_SM_ACCESS
							msg = string.Format ("{0,8:0D}: Tag " +
								"{1,0:T}: GetWriteRequest nRC = " +
									"{2,0:D}", Device.GetTickCount (), refTag.GetName (), nRC);
							Trace.WriteLine (msg);
#endif//TRACE_SM_ACCESS

                            if (nRC == TagData.SMRC_NO_ERROR)
                            {
                                refTag.tagWriteRequestPending = true;
                            }
                        }
                    }

                    // Check for pending read requests
                    bool bReadRequestPending = false;
                    nRC = Register.GetReadRequestPending(memStream, refTag.GetSharedMemoryOffset(), ref bReadRequestPending);

#if TRACE_SM_ACCESS
                    msg = string.Format ("{0,8:0D}: Tag " +
                        "{1,0:T}: GetReadRequestPending nRC = " +
                            "{2,0:D}, bReadRequestPending = {3,0:D}", Device.GetTickCount (), refTag.GetName (), nRC, bReadRequestPending);
                    Trace.WriteLine (msg);
#endif//TRACE_SM_ACCESS

                    if (nRC == TagData.SMRC_NO_ERROR)
                    {
                        if (bReadRequestPending)
                        {
                            nRC = Register.GetReadRequest(memStream, refTag.GetSharedMemoryOffset());

#if TRACE_SM_ACCESS
                            msg = string.Format ("{0,8:0D}: Tag " +
                                "{1,0:T}: GetReadRequest nRC = " +
                                    "{2,0:D}", Device.GetTickCount (), refTag.GetName (), nRC);
                            Trace.WriteLine (msg);
#endif//TRACE_SM_ACCESS

                            if (nRC == TagData.SMRC_NO_ERROR)
                            {
                                refTag.tagReadRequestPending = true;
                            }
                        }
                    }

                    try
                    {
                        mutex.ReleaseMutex();
                        lockCount--;
                    }
                    catch (SystemException ex)
                    {
                        Console.WriteLine("Exception on return from ReleaseMutex ()." +
                            "\r\n\tMessage: {0}", ex.Message);
                    }
                    pSharedMemoryBaseMem = null;
                }
            } // while (true)

            // The only time we should fall out of this function is if the quit event is set
            // and that only happens when we are shutting down
        } // public static void mainLoop ()

        public void FanucLoop() //執行FanucLoop，將資料Sene至CID
        {
            
            if (exitFlag == true) // may be set after exporting config
            {
                return;
            }
            Thread.Sleep(1);
            // **** set up and enter the main scan loop ****
            int nRC = TagData.SMRC_NO_ERROR;
            byte* pSharedMemoryBaseMem = null;
            Tag refTag = null;
            //string msg = "";
            int lockCount = 0;

            while (loop_switch_Fanuc == true)
            {
                if (exitFlag == true) 
                    break;
                Thread.Sleep(5);

                if (refTag != null)
                {
                    // **************************
                    // Read/Write to “Device”
                    // **************************
                    if (refTag.tagWriteRequestPending)
                    {
                        // Important: In a commercial application, this is where you would send the write request to the device.
                        // Since data is simulated, the write response is immediately available.

                        refTag.tagReadData.value = refTag.tagWriteData.value;       // assign value written to value to be read
                        refTag.tagReadData.quality = refTag.tagWriteData.quality;
                        refTag.tagReadData.timeStamp = refTag.tagWriteData.timeStamp;

                        refTag.tagWriteData.errorCode = 0;
                        refTag.tagWriteData.quality = TagData.OPC_QUALITY_GOOD_NON_SPECIFIC;
                        GetFtNow(ref refTag.tagWriteData.timeStamp);

                        refTag.tagWriteRequestPending = false;
                        refTag.tagWriteResponsePending = true;
                    }

                    if (refTag.tagReadRequestPending)
                    {
                        // Important: In a commercial application, this is where you would send the read request to the device
                        // Since data is simulated, the read response is immediately available.
                        if (refTag.IsWriteable() == true)
                        {
                            try
                            {
                               //  refTag.tagReadData.value.SendFanucData(refTag.GetName(),fv); 
                                 refTag.tagReadData.value.SendFanucData(refTag.GetName(), fv);
                    
                               //    main.eventLog1.WriteEntry("test"+((Fanuc_Value)fv).GetType().ToString());
                               //  main.eventLog1.WriteEntry(fv.GetType().ToString());                              
                            }
                            catch (Exception e)
                            {
                                 // main.eventLog1.WriteEntry("FanucLoop Error,result"+e);
                                  MemInterface.loop_switch_Fanuc = false;                       
                            }
                        }
                        refTag.tagReadData.errorCode = 0;
                        refTag.tagReadData.quality = TagData.OPC_QUALITY_GOOD_NON_SPECIFIC;
                        GetFtNow(ref refTag.tagReadData.timeStamp);

                        refTag.tagReadRequestPending = false;
                        refTag.tagReadResponsePending = true;
                    }

                    // ********************************************
                    // Process Read/Write Response (Shared Memory)
                    // ********************************************
                    if (refTag.tagWriteResponsePending == true || refTag.tagReadResponsePending == true)
                    {
                        Debug.Assert(pSharedMemoryBaseMem == null);

                        if (mutex.WaitOne() == true) //if we lock
                        {
                            pSharedMemoryBaseMem = refSharedMemory.pMem;
                        }

                        // Process responses only if we have access to Shared Memory (valid pointer)
                        if (pSharedMemoryBaseMem == null)
                        {
                            mutex.ReleaseMutex();
                            continue;
                        }

                        if (refTag.tagWriteResponsePending)
                        {
                            // Get the write response pending flag so we can ASSERT that its not set.
                            bool bWriteResponsePending = false;
                            Register.GetWriteResponsePending(memStream, refTag.GetSharedMemoryOffset(), ref bWriteResponsePending);

#if TRACE_SM_ACCESS
                            msg = string.Format ("{0,8:0D}: Tag " +
                                "{1,0:T}: GetWriteResponsePending nRC = " +
                                    "{2,0:D}, bWriteResponsePending = {3,0:D}", Device.GetTickCount (), refTag.GetName (), nRC, bWriteResponsePending);
                            Trace.WriteLine (msg);
#endif//TRACE_SM_ACCESS

                            if (nRC == TagData.SMRC_NO_ERROR)
                            {
                                // CID driver should not issue a write before completing the last write
                                Debug.Assert(!bWriteResponsePending);
                                Register.SetWriteResponse(memStream, refTag.GetSharedMemoryOffset(), refTag.tagWriteData.errorCode != 0, refTag.tagWriteData.errorCode, refTag.tagWriteData.quality, refTag.tagWriteData.timeStamp);

#if TRACE_SM_ACCESS
                                msg = string.Format ("{0,8:0D}: Tag " +
                                    "{1,0:T}: SetWriteResponse nRC = " +
                                        "{2,0:D}", Device.GetTickCount (), refTag.GetName (), nRC);
                                Trace.WriteLine (msg);
#endif//TRACE_SM_ACCESS
                                if (nRC == TagData.SMRC_NO_ERROR)
                                {
                                    refTag.tagWriteResponsePending = false;
                                }
                            }
                        } // if write response pending

                        if (refTag.tagReadResponsePending == true)
                        {
                            // Get the read response pending flag so we can ASSERT that its not set.
                            bool bReadResponsePending = false;

                            Register.GetReadResponsePending(memStream, refTag.GetSharedMemoryOffset(), ref bReadResponsePending);

#if TRACE_SM_ACCESS
                            msg = string.Format ("{0,8:0D}: Tag " +
                                "{1,0:T}: GetReadResponsePending nRC = " +
                                    "{2,0:D}, bReadResponsePending = {3,0:D}", Device.GetTickCount (), refTag.GetName (), nRC, bReadResponsePending);
                            Trace.WriteLine (msg);
#endif//TRACE_SM_ACCESS

                            if (nRC == TagData.SMRC_NO_ERROR)
                            {
                                // CID driver should not issue a read before completing the last read.
                                Debug.Assert(!bReadResponsePending);
                                Register.SetReadResponse(memStream, refTag.GetSharedMemoryOffset(), refTag.tagReadData.value, refTag.tagReadData.errorCode != 0, refTag.tagReadData.errorCode, refTag.tagReadData.quality, refTag.tagReadData.timeStamp);

#if TRACE_SM_ACCESS
                                msg = string.Format ("{0,8:0D}: Tag " +
                                    "{1,0:T}: SetReadResponse nRC = " +
                                        "{2,0:D}", Device.GetTickCount (), refTag.GetName (), nRC);
                                Trace.WriteLine (msg);
#endif//TRACE_SM_ACCESS

                                if (nRC == TagData.SMRC_NO_ERROR)
                                {
                                    refTag.tagReadResponsePending = false;
                                }
                            }
                        } // if read response pending
                    } // if read or write responses are pending
                } // if (refTag != null)

                // Important: In a commercial application, you may not want to process one tag at a time as this reference implementation does.  Instead you would
                // want to process as many tags as possible, processing requests and responses.

                try
                {
                    // Get our next tag to process.
                    refTag = GetNextTag();
                }
                catch (Exception e)
                {
                    main.eventLog1.WriteEntry("Fanuc loop Error,result:"+e.Message);
                }

                if (refTag != null)
                {
                    // ********************************************
                    // Process Read/Write Requests (Shared Memory)
                    // ********************************************

                    // May have been locked above
                    if (pSharedMemoryBaseMem == null)
                    {
                        try
                        {
                            if (mutex.WaitOne() == true)
                            {
                                lockCount++;
                                pSharedMemoryBaseMem = refSharedMemory.pMem;
                            }
                        }
                        catch (AbandonedMutexException ex)
                        {
                            main.eventLog1.WriteEntry("Exception on return from WaitOne.\n" + ex.Message);
                            Console.WriteLine("Exception on return from WaitOne." +
                                "\r\n\tMessage: {0}", ex.Message);
                        }
                    }
                    // Process requests only if we have access to Shared Memory (valid pointer)
                    if (pSharedMemoryBaseMem == null)
                    {
                        if (lockCount > 0)
                        {
                            mutex.ReleaseMutex();
                            lockCount--;
                        }
                        continue;
                    }

                    // Check for pending write requests
                    bool WriteRequestPending = false;
                    nRC = Register.GetWriteRequestPending(memStream, refTag.GetSharedMemoryOffset(), ref WriteRequestPending);

#if TRACE_SM_ACCESS
                msg = string.Format ("{0,8:0D}: Tag " +
                    "{1,0:T}: GetWriteRequestPending nRC = " +
                        "{2,0:D}, WriteRequestPending = {3,0:D}", Device.GetTickCount (), refTag.GetName (), nRC, bWriteRequestPending);
                Trace.WriteLine (msg);
#endif//TRACE_SM_ACCESS

                    if (nRC == TagData.SMRC_NO_ERROR)
                    {
                        if (WriteRequestPending)
                        {
                            nRC = Register.GetWriteRequest(memStream, refTag.GetSharedMemoryOffset(), ref refTag.tagWriteData.value, ref refTag.tagWriteData.quality, ref refTag.tagWriteData.timeStamp);

#if TRACE_SM_ACCESS
							msg = string.Format ("{0,8:0D}: Tag " +
								"{1,0:T}: GetWriteRequest nRC = " +
									"{2,0:D}", Device.GetTickCount (), refTag.GetName (), nRC);
							Trace.WriteLine (msg);
#endif//TRACE_SM_ACCESS

                            if (nRC == TagData.SMRC_NO_ERROR)
                            {
                                refTag.tagWriteRequestPending = true;
                            }
                        }
                    }

                    // Check for pending read requests
                    bool bReadRequestPending = false;
                    nRC = Register.GetReadRequestPending(memStream, refTag.GetSharedMemoryOffset(), ref bReadRequestPending);

#if TRACE_SM_ACCESS
                    msg = string.Format ("{0,8:0D}: Tag " +
                        "{1,0:T}: GetReadRequestPending nRC = " +
                            "{2,0:D}, bReadRequestPending = {3,0:D}", Device.GetTickCount (), refTag.GetName (), nRC, bReadRequestPending);
                    Trace.WriteLine (msg);
#endif//TRACE_SM_ACCESS

                    if (nRC == TagData.SMRC_NO_ERROR)
                    {
                        if (bReadRequestPending)
                        {
                            nRC = Register.GetReadRequest(memStream, refTag.GetSharedMemoryOffset());

#if TRACE_SM_ACCESS
                            msg = string.Format ("{0,8:0D}: Tag " +
                                "{1,0:T}: GetReadRequest nRC = " +
                                    "{2,0:D}", Device.GetTickCount (), refTag.GetName (), nRC);
                            Trace.WriteLine (msg);
#endif//TRACE_SM_ACCESS

                            if (nRC == TagData.SMRC_NO_ERROR)
                            {
                                refTag.tagReadRequestPending = true;
                            }
                        }
                    }

                    try
                    {
                        mutex.ReleaseMutex();
                        lockCount--;
                    }
                    catch (SystemException ex)
                    {
                        Console.WriteLine("Exception on return from ReleaseMutex ()." +
                            "\r\n\tMessage: {0}", ex.Message);
                    }
                    pSharedMemoryBaseMem = null;
                }
            } // while (true)

            // The only time we should fall out of this function is if the quit event is set
            // and that only happens when we are shutting down
        } // public static void mainLoop ()

        // *************************************************************************************
        public void StartExportConfiguration(string strApplicationDir, string strConfigName)
        {
            // Create a configuration file in the application's directory
            string strConfigFile = strApplicationDir;
            strConfigFile += "\\" + strConfigName;
            strConfigFile += ".xml";

            FileStream sFile = null;
            bool goodStream = true;
            try
            {
                sFile = new FileStream(strConfigFile, FileMode.Create, FileAccess.Write, FileShare.Read);
            }
            catch (SystemException ex)
            {
                Console.WriteLine("Unable to export configuration.  FileStream failed with error " + ex.Message);
                goodStream = false;
            }

            if (goodStream == true)
            {
                using (sFile)
                {
                    // Export configuration
                    // Release file
                    sFile.Close();
                    ExportConfiguration(strConfigFile, strConfigName);
                    Console.WriteLine("Created xml config file. Press a key to finish");
                    //Console.ReadKey ();
                }
            }
        } // StartExportConfiguration ()

        // *************************************************************************************
        public void ExportConfiguration(string strConfigFile, string strConfigName)
        {
            // Create the Configuration File in XML format

            // Important: For the sake of example, the reference implementation will create the XML file in a rudimentary fashion.
            // The vendor is free to utilize preferred techniques for generating XML, such as DOM.

            if (File.Exists(strConfigFile))
            {
                // Create a file to write to.
                File.AppendAllText(strConfigFile, "<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                File.AppendAllText(strConfigFile, "<custom_interface_config:Configuration xmlns:custom_interface_config=\"http://www.kepware.com/schemas/custom_interface_config\">");
                File.AppendAllText(strConfigFile, "<custom_interface_config:Copyright>The CID Interface and CID Definition file formats are the Copyright of Kepware Technologies and may only be used with KEPServer based products.  Use of the CID interfaces and file formats in any other manner is strictly forbidden.</custom_interface_config:Copyright>");
                File.AppendAllText(strConfigFile, "<custom_interface_config:Name>" + strConfigName + "</custom_interface_config:Name>");
                //// Support Info
                File.AppendAllText(strConfigFile, "<custom_interface_config:SupportInfo>");
                File.AppendAllText(strConfigFile, "<custom_interface_config:ContactInfo>");
                File.AppendAllText(strConfigFile, "<custom_interface_config:CompanyName>Youngtec</custom_interface_config:CompanyName>");
                File.AppendAllText(strConfigFile, "<custom_interface_config:Phone></custom_interface_config:Phone>");
                File.AppendAllText(strConfigFile, "<custom_interface_config:Email></custom_interface_config:Email>");
                File.AppendAllText(strConfigFile, "<custom_interface_config:WebURL>www.youngtec.com.tw</custom_interface_config:WebURL>");
                File.AppendAllText(strConfigFile, "<custom_interface_config:ContactAdditional></custom_interface_config:ContactAdditional>");
                File.AppendAllText(strConfigFile, "</custom_interface_config:ContactInfo>\r\n");
                File.AppendAllText(strConfigFile, "<custom_interface_config:ConfigurationLaunchHint>To export configuration, use argument -exportconfig.  Example: cidarefimplcpp.exe -exportconfig</custom_interface_config:ConfigurationLaunchHint>");
                File.AppendAllText(strConfigFile, "<custom_interface_config:RuntimeLaunchHint>Run without any arguments.  Example: cidarefimplcpp.exe</custom_interface_config:RuntimeLaunchHint>");
                File.AppendAllText(strConfigFile, "<custom_interface_config:HelpLaunchHint></custom_interface_config:HelpLaunchHint>");
                File.AppendAllText(strConfigFile, "<custom_interface_config:SupportAdditional></custom_interface_config:SupportAdditional>");
                File.AppendAllText(strConfigFile, "</custom_interface_config:SupportInfo>");
            }

            if (deviceSet.Count > 0)
            {
                // Walk the device set
                // Shared Memory Size
                File.AppendAllText(strConfigFile, "<custom_interface_config:SharedMemorySize>" + sharedMemorySize + "</custom_interface_config:SharedMemorySize>");
                // <device list>
                File.AppendAllText(strConfigFile, "<custom_interface_config:DeviceList>");
                // Have each device export its configuration
                foreach (Device device in deviceSet)
                {
                    // Call on device to export its configuration
                    device.ExportConfiguration(strConfigFile);
                }
                File.AppendAllText(strConfigFile, "</custom_interface_config:DeviceList>");
            }
            File.AppendAllText(strConfigFile, "</custom_interface_config:Configuration>");
        } // ExportConfiguration ()

        // *************************************************************************************
        public void SetupMutex(string strConfigName)
        {
            //Set up a mutex to control access to shared memory
            // The value of this variable is set by the mutex
            // constructor. It is true if the named system mutex was
            // created, and false if the named mutex already existed.
            //
            bool createdNewMutex = false;

            //string strConfigName = "cidarefimplcsharp";
            // We need to have a name
            string strName = strConfigName;
            Debug.Assert(!strName.Equals(""));

            // Name should be prefixed with 'Global\\' for Vista support
            /*if (strName.Substring(0, 7) != "Global\\")
            {
                strName = "Global\\" + strName;
            }*/
            strName = "Global\\" + strName;
            strName += "_sm_lock";

            MutexAccessRule rule = new MutexAccessRule(
                new SecurityIdentifier(WellKnownSidType.WorldSid, null),
                MutexRights.Synchronize | MutexRights.Modify |
                MutexRights.ReadPermissions | MutexRights.FullControl |
                MutexRights.TakeOwnership,
                AccessControlType.Allow);

            mSec.AddAccessRule(rule);

            mutex = new Mutex(true, strName, //creates and requesting ownership
                out createdNewMutex, mSec);

            if (createdNewMutex == true)
            {
                //System.Console.WriteLine ("Success: created mutex {0}", strName);
                mutex.ReleaseMutex();//that was requested at creation
            }
            else
            {
                //System.Console.WriteLine ("Failed to create mutex {0}", strName);
            }
        } // SetUpMutex ()

        // **************************************************************************
        // GetNextTag
        // Purpose: Provide caller with the next tag to work with.  Next tag is based
        // on order of the tag set within each device set, which is also ordered.
        // **************************************************************************
        public Tag GetNextTag()
        {
            // Look for empty set
            if (deviceSet.Count == 0)
            {
                return (null);
            }

            // Upon rollover, start from beginning
            if (deviceSet.Count == 1)
            {
                nextDeviceIndex = 0;
            }
            else if (nextDeviceIndex > deviceSet.Count - 1) // if at the last index
            {
                nextDeviceIndex = 0;
                //also reset the tag iterator for this device for the next pass
                deviceSet[nextDeviceIndex].ResetTagIterator();
            }

            Device refDevice = deviceSet[nextDeviceIndex];

            bool bIsLast = false;

            Tag refTag = null;

            if (refDevice != null)
            {
                refTag = newDeviceGetNextTagInMainLoop.GetNextTag(ref refDevice, ref bIsLast);

                // If refTag is last tag current device's list, move to next device
                if (bIsLast)
                    nextDeviceIndex++;
            }

            return (refTag);
        } // public static Tag GetNextTag ()

        // *************************************************************************************
        public static void GetFtNow(ref FILETIME ft)
        {
            long hFT1 = DateTime.Now.ToFileTime();
            ft.dwLowDateTime = (UInt32)(hFT1 & 0xFFFFFFFF);
            ft.dwHighDateTime = (UInt32)(hFT1 >> 32);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>

        private static string FunctionThread(string name)
        {
            switch (name.Split('_').Count())
            {
                case 0:
                    return "";

                case 1: //for fanuc
                    return "";

                case 2:
                    return name.Split('_')[0];

                default:
                    if (name.Split('_')[0] == "Variable" && name.Split('_')[1] == "MONITOR")
                    {
                        return name.Split('_')[1] + "_" + name.Split('_')[2];
                    }
                    else
                    {
                        return name.Split('_')[0] + "_" + name.Split('_')[1];
                    }
            }
        }
    } // class MemInterface
} //namespace CidaRefImplCsharp