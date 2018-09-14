using CNCNetLib;
using DELTA_Form;
using DELTA_Service.CID;
using DELTA_Service.CNC;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace DELTA_Service_Form
{
    public partial class Form1 : Form
    {
        private CNCNetClass CNCnet = new CNCNetClass();
        private CNCInfoClass CNC_info = new CNCInfoClass();
        private OpenFileDialog openFileDialog1 = new OpenFileDialog();
        private SaveFileDialog saveFileDialog1 = new SaveFileDialog();
        string ini_path = @"C:\youngtec_delta\DELTA.ini";

        //ini路徑需要討論，我暫時放在C朝的Youngtec_DELTA資料夾中

        private string[] args = new string[0];
        string service_name = "SDI_CID";
        //Read XML Path 
        public static string LoadPath = "";
        public static string LoadName = "";
        //======================test====================
        int ini_cutter_Index;
        int ini_cutter_length;
        int ini_magazine_magaID;
        int ini_current_code_CodeCount;
        //======================test====================
        //CSV項目
        ListView DeviceList = new ListView();
        ListView[] TagList = new ListView[0];
        DeviceDataConfig[] devicedata = null;
        DeviceDataConfig[] devicedata1 = null;
        string NowDeviceName = "";
        string NowTagName = "";
        public static string StaticTag = "";
        public static string StaticDataType = "";
        public static string StaticGroup = "";
        public static string StaticEditTag = "";
        public static string StaticEditDataType = "";
        public static string StaticEditGroup = "";
        public static string StaticDevice = "";
        public static string StaticEditDevice = "";
        string CSV_FileName = "";
        private bool Create_CSV = false;
        private int count;
        public string[] fanuc_IP = new string[50];
        public string[] port = new string[50];
        public string[] timeout = new string[50];

        public Form1()
        {
            InitializeComponent();

        }

        private void btn_CreateXML_Click(object sender, EventArgs e)
        {
            //When Service is start,CID program is running automatically
            try
            {
                bool exportConfig = true;
                string strConfigName = "";//Program name.
                string strApplicationDir = "";//Program path.

                // Get application info for naming the shared memory file and mutex
                GetConfigInfo(ref strConfigName, ref strApplicationDir, args, ref exportConfig);

                // Start the interface to shared memory
         //       MemInterface.Start(args, strConfigName, strApplicationDir, exportConfig);

                MessageBox.Show("XML 檔案產生完成!");
                updateINFO("已產生 XML ，請至程式目錄下開啟");
            }
            catch (Exception E)
            {
                Console.WriteLine(E);
                MessageBox.Show("XML 檔案產生失敗!");
                updateINFO("請重新產生 XML");
            }
        }

        private void btn_Service_Click(object sender, EventArgs e)
        {
            start_service();
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

            // Get the application's name without the .exe extension
            strConfigName = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //判斷Service是否運行
            Current_IP_Read();
            IsService();
            read_ini();
            File_Dection();
            information_intention();
        }

        private void information_intention()
        {
            updateINFO("如果有需要進行Tag異動，請先勾選Tag異動的Checkbox，程式會依照修正的CSV檔去產生xml，產生xml完之後請在KEPServerEX手動更新XML檔");
        }

        /// <summary>
        /// 偵測C\youngtec_delta這個資料夾的路徑是否存在
        /// </summary>
        private void File_Dection()
        {
            if (Directory.Exists(@"C:\youngtec_delta"))
            {
                updateINFO("ini設定檔儲存路徑為C:\\youngtec_delta");
            }
            else
            {
                //未偵測到資料夾，新增
                try
                {
                    Directory.CreateDirectory(@"C:\youngtec_delta");
                    updateINFO("未偵測到預設資料夾，新建設定資料夾路徑為C:\\youngtec_delta");
                }
                catch
                {
                    updateINFO("資料夾創建失敗");
                }
            }
        }

        private void updateINFO(String info)
        {
            txt_info.Text += info + Environment.NewLine;
            txt_info.ScrollToCaret();

        }
        /// <summary>
        /// 偵測Service當前狀態
        /// </summary>
        public void IsService()
        {
            ServiceController service = new ServiceController(service_name);
            try
            {
                if (service.Status != ServiceControllerStatus.Stopped && service.Status != ServiceControllerStatus.StopPending)
                {
                    //Service正在運行
                    updateINFO("Service正在運行");
                }
                else
                {
                    //Service停止運作
                    updateINFO("Service尚未啟動");
                }
            }
            catch
            {
                updateINFO("未安裝youngtec Service，請先安裝Service再執行本程式");

            }
        }

        private void Lv_Addresses_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txt_info_TextChanged(object sender, EventArgs e)
        {

        }



        private void start_service()
        {
            //啟動Service，並設定timeout為30秒
            try
            {
                ServiceController service = new ServiceController(service_name);
                TimeSpan timeout = TimeSpan.FromMilliseconds(1000 * 30);//30秒
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);
                updateINFO("Service重新啟動成功");
            }
            catch (Exception ex)
            {
                updateINFO("Service啟動失敗" + ex);
            }
        }

        private void shutdown_service()
        {
            //關閉Service，並設定timeout為30秒
            try
            {
                ServiceController service = new ServiceController(service_name);
                TimeSpan timeout = TimeSpan.FromMilliseconds(1000 * 30);//30秒
                if (service.Status != ServiceControllerStatus.Stopped && service.Status != ServiceControllerStatus.StopPending)
                {
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                    updateINFO("關閉Service成功");
                }
            }
            catch 
            {
                updateINFO("Service關閉失敗!請在系統管理員的權限下執行本程式。");
            }
        }

        private void btn_Service_Close_Click(object sender, EventArgs e)
        {
            shutdown_service();
        }

        //按下設定按鈕會記錄local ip,remote ip,timeout,是否有更新Tag，利用Checkbox勾選寫到ini設定檔
        private void btn_setting_Click_1(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("設定完需要重啟服務", "是否要重啟服務", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                if (!Lv_Addresses.SelectedItems.Count.Equals(0))
                {
                    CNC_info.SetConnectInfo(Lv_Addresses.SelectedItems[0].Text.Trim(), txt_RemoteIP.Text.Trim(), Convert.ToInt32(txt_Timeout.Text.Trim()));
                    int Isfail = CNC_info.Connect();

                    if (Isfail == 0)
                    {
                        write_ini();
                        updateINFO("寫入ini記錄檔完成");
                        updateINFO("重啟服務中...");
                   
                        
                        ServiceController service = new ServiceController(service_name);
                        if (service.Status == ServiceControllerStatus.Running)
                        {
                            try
                            {
                                shutdown_service();
                                start_service();
                                updateINFO("重啟完成...");
                            }
                            catch
                            {
                                updateINFO("服務重啟失敗");
                            }
                        }
                        else if (service.Status == ServiceControllerStatus.Stopped)
                        {
                            start_service();
                        }
                        
                        updateINFO("服務重啟完成!");
                    }
                    else
                    {
                        //如果回傳數值非0則跳出詢問是否仍寫入至ini設定檔
                        if (DialogResult.Yes == MessageBox.Show("無法連線至機台，仍要寫入設定檔嗎?", "連線錯誤", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                        {
                            write_ini();
                            updateINFO("寫入ini設定檔完成");
                            //之後砍掉
                            ServiceController service = new ServiceController(service_name);
                            if (service.Status == ServiceControllerStatus.Running)
                            {
                                try
                                {
                                    shutdown_service();
                                    start_service();
                                    updateINFO("重啟完成...");
                                }
                                catch
                                {
                                    updateINFO("服務重啟失敗");
                                }
                            }
                            else if (service.Status == ServiceControllerStatus.Stopped)
                            {
                                start_service();
                            }
                        }
                        else
                        {
                            //按下否後不做任何事情
                        }
                    }
                }
                else
                {
                    //IP必須選定
                    MessageBox.Show("請先選擇上方的Local IP");
                }
            }
            else
            {
                //按下取消後不做任何反應
            }
            //Youngtec
        }


        //string ini_name = @"C:\Users\Youngtec\Documents\youngtec_config.ini";

        //ini dll import
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool WritePrivateProfileString(string sectionName, string keyName, string keyValue, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetPrivateProfileString(string sectionName, string keyName, string defaultReturnString, StringBuilder returnString, int returnStringLength, string filePath);
        //write ini事項
        public void write_ini()
        {
            if (!Lv_Addresses.SelectedItems.Count.Equals(0))
            {
                WritePrivateProfileString("Connect setting", "localIP", Lv_Addresses.SelectedItems[0].Text.Trim(), ini_path);
                WritePrivateProfileString("Connect setting", "remoteIP", txt_RemoteIP.Text.Trim(), ini_path);
                WritePrivateProfileString("Connect setting", "timeout", txt_Timeout.Text.Trim(), ini_path);
                WritePrivateProfileString("Connect setting","localIP2",Lv_Addresses.SelectedItems[0].Text.Trim(),ini_path);
                WritePrivateProfileString("CNC","Count",1.ToString(),ini_path);
                if (check_new_tag.Checked == true)
                {
                    if (!string.IsNullOrWhiteSpace(txt_XML_Path.Text.Trim()) && Create_CSV == true)
                    {
                        WritePrivateProfileString("Tag State", "newtag", "true", ini_path);
                        WritePrivateProfileString("Tag State", "CSVname", CSV_FileName, ini_path);
                        WritePrivateProfileString("Tag State", "csvrun", "true", ini_path);
                    }
                    else
                    {
                        MessageBox.Show("請指定xml路徑並且創建CSV檔才有辦法寫入至ini設定檔");
                    }
                }
                else
                {
                    //為勾選會設定為false，Service會依原本設定或是已產生的CSV檔去執行
                    WritePrivateProfileString("Tag State", "newtag", "false", ini_path);
                }
            }
            else
            {

            }
            Console.WriteLine(ini_path);
        }
     //讀取ini項目
        public void read_ini()
        {
         //GetPrivateProfileString("ini項目名","該項目資訊","預設值","StringBuilder","回傳字串長度","ini路徑");
            //第一台機檯
            StringBuilder remoteIP = new StringBuilder(255);
            GetPrivateProfileString("Connect setting", "remoteIP", "", remoteIP, 255, ini_path);
            txt_RemoteIP.Text = Convert.ToString(remoteIP);

            StringBuilder timeout = new StringBuilder(255);
            GetPrivateProfileString("Connect setting", "timeout", "", timeout, 255, ini_path);
            txt_Timeout.Text = Convert.ToString(timeout);


            StringBuilder newtag = new StringBuilder(255);
            GetPrivateProfileString("Tag State", "newtag", "", newtag, 255, ini_path);
            // chechbox_newtag_state = Convert.ToBoolean(newtag);
            //第二台機台
      
        }
        /// <summary>
        /// 讀取當前IP
        /// </summary>
        private void Current_IP_Read()
        {
            //連線設定
            string Config_mac;
            string Config_mask;
            string Config_gateway;
            bool Config_dhcp;
            string[] IP_Addresses = CNCnet.GetCurrentIPAddress();
            foreach (string address in IP_Addresses)
            {
                int fail = 0;
                fail = CNCnet.GetNetConfig(address, out Config_mac, out Config_mask, out Config_gateway, out Config_dhcp);
                ListViewItem Config_item = new ListViewItem();
                if (fail.Equals(0))
                {
                    Config_item.Text = address;
                    Config_item.SubItems.Add(Config_mac);
                    Config_item.SubItems.Add(Config_mask);
                    Config_item.SubItems.Add(Config_gateway);
                    if (Config_dhcp)
                    {
                        Config_item.SubItems.Add("是");
                    }
                    else
                    {
                        Config_item.SubItems.Add("否");
                    }

                    Lv_Addresses.Items.Add(Config_item);
                }
            }
        }

        public bool chechbox_newtag_state(string state)
        {
            if (state == "true")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //xml
        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = "";
            openFileDialog1.Filter = "XML文件|*.xml|所有文件|*.*";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                string fileName = openFileDialog1.FileName;
                txt_XML_Path.Text = fileName;
                LoadName = openFileDialog1.SafeFileName;
                LoadPath = openFileDialog1.FileName;
                updateINFO("讀取XML成功!");
            }
        }
        //匯出CSV，並指定路徑
        private void btn_export_csv_Click(object sender, EventArgs e)
        {

            Load_XML();//讀取xml的Tag
            saveFileDialog1.Filter = "CSV文件|*.csv|所有文件|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                try
                {
                    string FileName = saveFileDialog1.FileName;
                    FileStream fs_in = new FileStream(FileName, System.IO.FileMode.Create, System.IO.FileAccess.Write);
                    StreamWriter sw_in = new StreamWriter(fs_in, System.Text.Encoding.Default);
                    string data = "";
                    ArrayList al = new System.Collections.ArrayList();
                    al.Add("DeviceName");
                    al.Add("TagName");
                    al.Add("TagDataType");
                    al.Add("TagGroup");
                    al.Add("Description");
                    //-------------------------new----------------------------------

                    al.Add("StringSize");
                    al.Add("ArrayRows");
                    al.Add("ArrayCols");
                    al.Add("DefaultTagName");

                    for (int i = 0; i < al.Count; i++)
                    {
                        data += al[i];
                        if (i < al.Count - 1)
                        {
                            data += ",";
                        }
                    }
                    sw_in.WriteLine(data);

                    /*
                    for (int i = 0; i < DeviceList.Items.Count; i++)
                    {
                        data = "";
                        for (int j = 0; j < TagList[i].Items.Count; j++)
                        {
                            data = "";
                            data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text+','+TagList[i].Items[j].SubItems[3].Text;
                            sw_in.WriteLine(data);
                        }
                    }
                    */

                    for (int i = 0; i < DeviceList.Items.Count; i++)
                    {
                        data = "";
                        for (int j = 0; j < TagList[i].Items.Count; j++)
                        {
                            switch (TagList[i].Items[j].SubItems[1].Text)
                            {
                                case "Boolean":
                                    data = "";
                                    data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text + ',' + TagList[i].Items[j].SubItems[3].Text + ',' + 0 + ',' + 0 + ',' + 0 + ',' + TagList[i].Items[j].Text;
                                    sw_in.WriteLine(data);
                                    break;
                                case "Byte":
                                    data = "";
                                    data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text + ',' + TagList[i].Items[j].SubItems[3].Text + ',' + 0 + ',' + 0 + ',' + 0 + ',' + TagList[i].Items[j].Text;
                                    sw_in.WriteLine(data);
                                    break;
                                case "String":
                                    data = "";
                                    data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text + ',' + TagList[i].Items[j].SubItems[3].Text + ',' + 15 + ',' + 0 + ',' + 0 + ',' + TagList[i].Items[j].Text;
                                    sw_in.WriteLine(data);
                                    break;
                                case "Word":
                                    data = "";
                                    data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text + ',' + TagList[i].Items[j].SubItems[3].Text + ',' + 0 + ',' + 0 + ',' + 0 + ',' + TagList[i].Items[j].Text;
                                    sw_in.WriteLine(data);
                                    break;
                                case "DWord":
                                    data = "";
                                    data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text + ',' + TagList[i].Items[j].SubItems[3].Text + ',' + 0 + ',' + 0 + ',' + 0 + ',' + TagList[i].Items[j].Text;
                                    sw_in.WriteLine(data);
                                    break;
                                case "Long":
                                    data = "";
                                    data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text + ',' + TagList[i].Items[j].SubItems[3].Text + ',' + 0 + ',' + 0 + ',' + 0 + ',' + TagList[i].Items[j].Text;
                                    sw_in.WriteLine(data);
                                    break;
                                case "Double":
                                    data = "";
                                    data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text + ',' + TagList[i].Items[j].SubItems[3].Text + ',' + 0 + ',' + 0 + ',' + 0 + ',' + TagList[i].Items[j].Text;
                                    sw_in.WriteLine(data);
                                    break;
                                case "Short":
                                    data = "";
                                    data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text + ',' + TagList[i].Items[j].SubItems[3].Text + ',' + 0 + ',' + 0 + ',' + 0 + ',' + TagList[i].Items[j].Text;
                                    sw_in.WriteLine(data);
                                    break;
                                case "Char Array":
                                    data = "";
                                    data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text + ',' + TagList[i].Items[j].SubItems[3].Text + ',' + 0 + ',' + 1 + ',' + 3 + ',' + TagList[i].Items[j].Text;
                                    sw_in.WriteLine(data);
                                    break;
                                case "Word Array":
                                    data = "";
                                    data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text + ',' + TagList[i].Items[j].SubItems[3].Text + ',' + 0 + ',' + 1 + ',' + 3 + ',' + TagList[i].Items[j].Text;
                                    sw_in.WriteLine(data);
                                    break;
                                case "String Array":
                                    data = "";
                                    data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text + ',' + TagList[i].Items[j].SubItems[3].Text + ',' + 60 + ',' + 1 + ',' + 2 + ',' + TagList[i].Items[j].Text;
                                    sw_in.WriteLine(data);
                                    break;
                                case "Boolean Array":
                                    data = "";
                                    data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text + ',' + TagList[i].Items[j].SubItems[3].Text + ',' + 0 + ',' + 1 + ',' + 3 + ',' + TagList[i].Items[j].Text;
                                    sw_in.WriteLine(data);
                                    break;
                                case "Long Array":
                                    data = "";
                                    data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text + ',' + TagList[i].Items[j].SubItems[3].Text + ',' + 0 + ',' + 1 + ',' + 1 + ',' + TagList[i].Items[j].Text;
                                    sw_in.WriteLine(data);
                                    break;
                                case "Double Array":
                                    data = "";
                                    data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text + ',' + TagList[i].Items[j].SubItems[3].Text + ',' + 0 + ',' + 1 + ',' + 3 + ',' + TagList[i].Items[j].Text;
                                    sw_in.WriteLine(data);
                                    break;
                                case "Short Array":
                                    data = "";
                                    data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text + ',' + TagList[i].Items[j].SubItems[3].Text + ',' + 0 + ',' + 1 + ',' + 3 + ',' + TagList[i].Items[j].Text;
                                    sw_in.WriteLine(data);
                                    break;
                                case "Byte Array":
                                    data = "";
                                    data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text + ',' + TagList[i].Items[j].SubItems[3].Text + ',' + 0 + ',' + 1 + ',' + 3 + ',' + TagList[i].Items[j].Text;
                                    sw_in.WriteLine(data);
                                    break;
                                case "DWord Array":
                                    data = "";
                                    data += DeviceList.Items[i].Text + ',' + TagList[i].Items[j].Text + ',' + TagList[i].Items[j].SubItems[1].Text + ',' + TagList[i].Items[j].SubItems[2].Text + ',' + TagList[i].Items[j].SubItems[3].Text + ',' + 0 + ',' + 1 + ',' + 5 + ',' + TagList[i].Items[j].Text;
                                    sw_in.WriteLine(data);
                                    break;
                            }
                        }
                    }
                    sw_in.Close();
                    fs_in.Close();
                    CSV_FileName = FileName;
                    updateINFO("匯出CSV檔成功，請使用CSV檔編輯點位後，將CSV檔放在Service執行路徑下!");
                    Create_CSV = true;
                }
                catch
                {
                    MessageBox.Show("請先關閉CSV檔案，再進行覆蓋動作");
                    updateINFO("存入，請先關閉CSV檔案，再進行覆蓋動作");
                }
            }
        }
       //-----------------------------這邊是跑CSV的，DEMO暫時不會用到------------------------------------------
        public void ListLoad()
        {
            devicedata = null;
            devicedata1 = null;
            NowDeviceName = "";
            NowTagName = "";
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
        public void Load_XML()
        {
            ListLoad();
            try
            {
                XmlTextReader reader = new XmlTextReader(txt_XML_Path.Text);
                ArrayList deviceArray = new ArrayList();
                ArrayList tagArray = new ArrayList();
                devicedata = null;
                devicedata1 = null;
                int deviceNumber = 0, TagNumber = 0, AllNumber = 1;
                try
                {
                    while (reader.Read())
                    {
                        if (reader.Name == "custom_interface_config:Device")
                        {
                            while (reader.Read())
                            {
                                if (reader.Name == "custom_interface_config:Name")
                                {
                                    deviceNumber++;
                                    reader.Read();
                                    deviceArray.Add(reader.Value);
                                    reader.Read();
                                }
                                if (reader.Name == "custom_interface_config:ID")
                                {
                                    reader.Read();
                                    deviceArray.Add(reader.Value);
                                    reader.Read();
                                }
                                if (reader.Name == "custom_interface_config:SharedMemoryDeviceOffset")
                                {
                                    reader.Read();
                                    deviceArray.Add(reader.Value);
                                    reader.Read();
                                    break;
                                }
                            }
                        }
                        if (reader.Name == "custom_interface_config:TagList")
                        {
                            while (reader.Read())
                            {
                                if (reader.Name == "custom_interface_config:Device")
                                {
                                    if (AllNumber == deviceNumber)
                                    {
                                        TagDataConfig[] tagdata = new TagDataConfig[TagNumber];
                                        int tagArrayNumber = 0, deviceArrayNumber = 0;
                                        for (int i = 0; i < TagNumber; i++)
                                        {
                                            tagdata[i] = new TagDataConfig(tagArray[tagArrayNumber].ToString(), tagArray[++tagArrayNumber].ToString(), tagArray[++tagArrayNumber].ToString(), tagArray[++tagArrayNumber].ToString(), tagArray[++tagArrayNumber].ToString(), tagArray[++tagArrayNumber].ToString(), tagArray[++tagArrayNumber].ToString());
                                            tagArrayNumber++;
                                        }
                                        devicedata = new DeviceDataConfig[deviceNumber];
                                        for (int i = 0; i < devicedata.Length; i++)
                                        {
                                            devicedata[i] = new DeviceDataConfig(deviceArray[deviceArrayNumber].ToString(), deviceArray[++deviceArrayNumber].ToString(), deviceArray[++deviceArrayNumber].ToString(), tagdata);
                                            deviceArrayNumber++;
                                        }
                                    }
                                    else
                                    {
                                        devicedata1 = new DeviceDataConfig[deviceNumber];
                                        for (int i = 0; i < AllNumber; i++)
                                        {
                                            devicedata1[i] = devicedata[i];
                                        }
                                        TagDataConfig[] tagdata = new TagDataConfig[TagNumber];
                                        int tagArrayNumber = 0, deviceArrayNumber = 0;
                                        for (int i = 0; i < TagNumber; i++)
                                        {
                                            tagdata[i] = new TagDataConfig(tagArray[tagArrayNumber].ToString(), tagArray[++tagArrayNumber].ToString(), tagArray[++tagArrayNumber].ToString(), tagArray[++tagArrayNumber].ToString(), tagArray[++tagArrayNumber].ToString(), tagArray[++tagArrayNumber].ToString(), tagArray[++tagArrayNumber].ToString());
                                            tagArrayNumber++;
                                        }

                                        devicedata = new DeviceDataConfig[1];
                                        for (int i = 0; i < devicedata.Length; i++)
                                        {
                                            devicedata[i] = new DeviceDataConfig(deviceArray[deviceArrayNumber].ToString(), deviceArray[++deviceArrayNumber].ToString(), deviceArray[++deviceArrayNumber].ToString(), tagdata);
                                            deviceArrayNumber++;
                                        }
                                        devicedata1[AllNumber] = devicedata[0];
                                        devicedata = new DeviceDataConfig[deviceNumber];
                                        int Reciprocal = devicedata1.Length;
                                        for (int i = 0; i < devicedata.Length; i++)
                                        {
                                            devicedata[i] = devicedata1[i];
                                        }
                                    }
                                    tagArray.Clear();
                                    deviceArray.Clear();
                                    AllNumber = deviceNumber;
                                    TagNumber = 0;
                                    break;
                                }
                                switch (reader.Name)
                                {
                                    case "custom_interface_config:Name":
                                        TagNumber++;
                                        reader.Read();
                                        tagArray.Add(reader.Value);
                                        reader.Read();
                                        break;
                                    case "custom_interface_config:Address":
                                        reader.Read();
                                        tagArray.Add(reader.Value);
                                        reader.Read();
                                        break;
                                    case "custom_interface_config:DataType":
                                        reader.Read();
                                        tagArray.Add(reader.Value);
                                        reader.Read();
                                        break;
                                    case "custom_interface_config:ReadWriteAccess":
                                        reader.Read();
                                        tagArray.Add(reader.Value);
                                        reader.Read();
                                        break;
                                    case "custom_interface_config:ScanRateMilliseconds":
                                        reader.Read();
                                        tagArray.Add(reader.Value);
                                        reader.Read();
                                        break;
                                    case "custom_interface_config:Description":
                                        reader.Read();
                                        if (reader.Name == "custom_interface_config:Description")
                                        {
                                            tagArray.Add(reader.Value);
                                            break;
                                        }
                                        tagArray.Add(reader.Value);
                                        reader.Read();
                                        break;
                                    case "custom_interface_config:GroupName":
                                        reader.Read();
                                        if (reader.Name == "custom_interface_config:GroupName")
                                        {
                                            tagArray.Add(reader.Value);
                                            break;
                                        }
                                        tagArray.Add(reader.Value);
                                        reader.Read();
                                        break;
                                    case "custom_interface_config:TagList":
                                        break;
                                }
                            }
                        }
                    }
                    reader.Close();
                }
                catch
                {
                    if (reader != null)
                        reader.Close();
                }
            }
            catch
            {

            }
            DeviceList = new ListView();
            if (devicedata == null)
            {
                TagList = new ListView[0];
                return;
            }
            else
            {
                TagList = new ListView[devicedata.Length];

                for (int i = 0; i < devicedata.Length; i++)
                {
                    DeviceList.Items.Add(devicedata[i].DeviceName);                
                    TagList[i] = new ListView();
                    TagList[i].Name = devicedata[i].DeviceName;
                }
                for (int i = 0; i < devicedata.Length; i++)
                {
                    for (int j = 0; j < devicedata[i].tagdata.Length; j++)
                    {
                        TagList[i].Items.Add(devicedata[i].tagdata[j].TagNmae);
                        TagList[i].Items[j].SubItems.Add(devicedata[i].tagdata[j].TagDataType);
                        TagList[i].Items[j].SubItems.Add(devicedata[i].tagdata[j].TagGroup);
                        TagList[i].Items[j].SubItems.Add(devicedata[i].tagdata[j].TagDescription);
                    }
                }
            }
        }
      

        //
        private void check_new_tag_CheckedChanged(object sender, EventArgs e)
        {
            if (check_new_tag.Checked == true)
            {
                txt_XML_Path.Enabled = true;
                btn_export_csv.Enabled = true;
                btn_xml_read.Enabled = true;
            }
            else
            {
                txt_XML_Path.Enabled = false;
                btn_export_csv.Enabled = false;
                btn_xml_read.Enabled = false;
            }
        }
        //將ini設定為初始值
        private void btn_origin_Click(object sender, EventArgs e)
        {
            try
            {
                WritePrivateProfileString("Connect setting", "localIP", "", ini_path);
                WritePrivateProfileString("Connect setting", "remoteIP", "", ini_path);
                WritePrivateProfileString("Connect setting", "timeout", "", ini_path);

                WritePrivateProfileString("Tag State", "newtag", "false", ini_path);
                WritePrivateProfileString("Tag State", "CSVname", "", ini_path);
                WritePrivateProfileString("Tag State", "csvrun", "false", ini_path);
                MessageBox.Show("回復至初始設定時成功");
            }
            catch
            {
                MessageBox.Show("回復至初始設定時失敗。");
            }
        }

        private void btn_new_cnc_Click(object sender, EventArgs e)
        {
            new_cnc new_cnc_form = new new_cnc();
            new_cnc_form.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            cutter_magazine cm = new cutter_magazine();
            cm.Show();

        }

        private void txt_info_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void cbox_second_cnc_CheckedChanged(object sender, EventArgs e)
        {
          
        }

        private void txt_RemoteIP_Leave(object sender, EventArgs e)
        {
            IPAddress ipAddress;
            if (!IPAddress.TryParse(txt_RemoteIP.Text, out ipAddress))
            {
                MessageBox.Show("請輸入正確IP");
            }
           
        }

        private void btn_new_CNC_Click_1(object sender, EventArgs e)
        {
            new_cnc NewForm = new new_cnc();
            NewForm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ini_controll ic = new ini_controll();
            count = Convert.ToInt32(ic.read_ini("Count", "Fanuc", "1"));//最後面為Fanuc預設的IP
            int y = 1;
            for (int i = 0; i < count; i++)
            {
                fanuc_IP[i] = ic.read_ini("Fanuc Setting", "IP" +y, "192.168.152.89");
                port[i] = ic.read_ini("Fanuc Setting", "port" + y, "8193");
                timeout[i] = ic.read_ini("Fanuc Setting", "timeout" +y, "3000");
                Console.WriteLine("Fanuc IP="+fanuc_IP[i]+"  ,port="+port[i]+"   ,timeout="+timeout[i]);
                Console.WriteLine("Fanuc Setting"+" "+"IP" + y+" "+"192.168.152.89");
                y++;
            }

        }
    }
    /// <summary>
    /// Device設定資料
    /// </summary>
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
    /// <summary>
    /// TagData設定資料
    /// </summary>
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
