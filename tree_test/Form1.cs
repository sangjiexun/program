/*勾選號寫入ini
 * 程式開啟時會讀取ini後自動勾選(依上一次紀錄)
 * ini存放位置為youngtec_delta
 * 存檔檔名為DELTA_TAG
 * 尚未完成
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tree_test
{
    public partial class Form1 : Form
    {
        string ini_path = @"C:\youngtec_delta\DELTA_TAG.ini";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            read_ini();
            treeView1.CheckBoxes = true;
            treeView1.ShowLines = true;
            DataTable dt = GetTreeData();
            TreeBuild(dt);
            treeView1.ExpandAll();
            treeView1.AfterSelect += treeView1_AfterSelect;
            treeView1.AfterCheck += treeView1_AfterCheck;
            FocusOnRoot();
      
        }

        private void FocusOnRoot()
        {
            this.treeView1.SelectedNode = this.treeView1.Nodes[0];
        }

        private void TreeBuild(DataTable dt)
        {
            TreeRootExist(dt);
            CreateRootNode(this.treeView1, dt);
        }

        private void CreateRootNode(TreeView tree, DataTable dt)
        {
            DataRow Root = GetTreeRoot(dt);
            TreeNode Node = new TreeNode();
            Node.Text = Root.Field<string>(this.TextColIndex);
            Node.Name = Root.Field<string>(this.IDColIndex);
            tree.Nodes.Add(Node);

            CreateNode(tree,dt,Node);
        }

        private void CreateNode(TreeView tree, DataTable dt, TreeNode Node)
        {
            IEnumerable<DataRow> Rows = GetTreeNodes(dt, Node);
            TreeNode NewNode;
            foreach(DataRow r in Rows)
            {
                NewNode = new TreeNode();
                NewNode.Name = r.Field<string>(this.IDColIndex);
                NewNode.Text = r.Field<string>(this.TextColIndex);
                Node.Nodes.Add(NewNode);

                CreateNode(tree, dt, NewNode);
            }
        }
        //all tag 
        private DataTable GetTreeData()
        {

            DataTable dt = new DataTable();
            dt.Columns.Add("DepID", typeof(string));
            dt.Columns.Add("ParentID", typeof(string));
            dt.Columns.Add("DepName", typeof(string));
            dt.Columns.Add("Instruction", typeof(string));

            dt.Rows.Add("01", null, "順德工業");
            dt.Rows.Add("02", "01", "機台A");
            dt.Rows.Add("03", "02", "通道0");
            dt.Rows.Add("04", "03", "基本資訊");
            dt.Rows.Add("05", "04", "READ_CNCFlag: 讀取CNC加工、Alarm 與RestartAct Flag","test");
            dt.Rows.Add("06", "05", "CNCFlag_WorkingFlag");
            dt.Rows.Add("07", "05", "CNCFlag_AlarmFlag");
            dt.Rows.Add("08", "05", "CNCFlag_RestartAct");
            dt.Rows.Add("08", "05", "CNCFlag_RestartAct");
            dt.Rows.Add("09", "04", "READ_CNC_information : CNC基本資訊");
            dt.Rows.Add("10", "09", "CNC_information_CncType");
            dt.Rows.Add("11", "09", "CNC_information_MaxChannels");
            dt.Rows.Add("12", "09", "CNC_information_Series");
            dt.Rows.Add("13", "09", "CNC_information_Nc_Ver");
            dt.Rows.Add("14", "09", "CNC_information_ScrewUnit");
            dt.Rows.Add("15", "09", "CNC_information_DisplayUnit");
            dt.Rows.Add("16", "09", "CNC_information_ApiVersion");
            dt.Rows.Add("17", "04", "READ_status : CNC狀態");
            dt.Rows.Add("18", "17", "status_MainProg");
            dt.Rows.Add("19", "17", "status_CurProg");
            dt.Rows.Add("20", "17", "status_ProgPath");
            dt.Rows.Add("21", "17", "status_CurSeq");
            dt.Rows.Add("22", "17", "status_MDICur");
            dt.Rows.Add("23", "17", "status_Mode");
            dt.Rows.Add("24", "17", "status_Status");
            dt.Rows.Add("25", "17", "status_IsAlarm");
            dt.Rows.Add("26", "17", "status_IsWorking");
            dt.Rows.Add("27", "17", "status_RestartAct");
            dt.Rows.Add("28", "04", "READ_NCMOTION: CNC Motion狀態資訊");
            dt.Rows.Add("29", "28", "NCMOTION_Unit");
            dt.Rows.Add("30", "28", "NCMOTION_iSpSpeed");
            dt.Rows.Add("31", "28", "NCMOTION_iFeed");
            dt.Rows.Add("32", "28", "NCMOTION_iLoad");
            dt.Rows.Add("33", "28", "NCMOTION_iActSpeed");
            dt.Rows.Add("34", "28", "NCMOTION_iActFeed");
            dt.Rows.Add("35", "28", "NCMOTION_iDwellTime");
            dt.Rows.Add("36", "28", "NCMOTION_iMCutter");
            dt.Rows.Add("37", "28", "NCMOTION_iCutterLib");
            dt.Rows.Add("38", "28", "NCMOTION_iCmdCutter");
            dt.Rows.Add("39", "28", "NCMOTION_iStandbyCutter");
            dt.Rows.Add("40", "28", "NCMOTION_iStandbyCast");
            dt.Rows.Add("41", "28", "NCMOTION_iDData");
            dt.Rows.Add("42", "28", "NCMOTION_iHData");
            dt.Rows.Add("43", "28", "NCMOTION_iTData");
            dt.Rows.Add("44", "28", "NCMOTION_iMData");
            dt.Rows.Add("45", "28", "NCMOTION_GGroup");
            dt.Rows.Add("46", "28", "NCMOTION_iSpeedF");
            dt.Rows.Add("47", "28", "NCMOTION_iSpeedRPD");
            dt.Rows.Add("48", "28", "NCMOTION_iSpeedS");
            dt.Rows.Add("49", "28", "NCMOTION_iSpeedJOG");
            dt.Rows.Add("50", "28", "NCMOTION_iSpeedMPG");
            dt.Rows.Add("51", "04", "READ_feed_spindle : 讀取進給率/轉速");
            dt.Rows.Add("52", "51", "feed_spindle_OvFeed");
            dt.Rows.Add("53", "51", "feed_spindle_OvSpindle");
            dt.Rows.Add("54", "51", "feed_spindle_ActFeed");
            dt.Rows.Add("55", "51", "feed_spindle_ActSpindle");
            dt.Rows.Add("56", "04", "READ_CNC_HostName : 讀取NC主機名稱");
            dt.Rows.Add("57", "56", "CNC_HostName_strName");
            dt.Rows.Add("58", "04", "READ_current_alarm_count: 讀取全部目前警報個數");
            dt.Rows.Add("59", "58", "current_alarm_count_AlarmCount");
            dt.Rows.Add("60", "04", "READ_alm_current: 讀取目前發生警報");
            dt.Rows.Add("61", "60", "alm_current_AlarmCount");
            dt.Rows.Add("62", "60", "alm_current_AlarmCode");
            dt.Rows.Add("63", "60", "alm_current_AlarmDataLen");
            dt.Rows.Add("64", "60", "alm_current_AlarmData");
            dt.Rows.Add("65", "60", "alm_current_AlarmMsg");
            dt.Rows.Add("66", "60", "alm_current_AlarmDateTime");
            dt.Rows.Add("67", "04", "READ_history_alarm_count: 讀取全部歷史警報個數");
            dt.Rows.Add("68", "67", "history_alarm_count_AlarmCount");
            dt.Rows.Add("69", "04", "READ_alm_history_all: 讀取全部歷史警報訊息");
            dt.Rows.Add("70", "69", "alm_history_all_AlarmCount");
            dt.Rows.Add("71", "69", "alm_history_all_AlarmCode");
            dt.Rows.Add("72", "69", "alm_history_all_AlarmDataLen");
            dt.Rows.Add("73", "69", "alm_history_all_AlarmData");
            dt.Rows.Add("74", "69", "alm_history_all_AlarmMsg");
            dt.Rows.Add("75", "69", "alm_history_all_AlarmDateTime");
            dt.Rows.Add("76", "04", "READ_alm_history: 讀取歷史警報訊息");
            dt.Rows.Add("77", "76", "alm_history_AlarmCount");
            dt.Rows.Add("78", "76", "alm_history_AlarmCode");
            dt.Rows.Add("79", "76", "alm_history_AlarmDataLen");
            dt.Rows.Add("80", "76", "alm_history_AlarmData");
            dt.Rows.Add("81", "76", "alm_history_AlarmMsg");
            dt.Rows.Add("82", "76", "alm_history_AlarmDateTime");
            dt.Rows.Add("83", "04", "READ_servo_current: 讀取伺服軸負載電流");
            dt.Rows.Add("84", "83", "servo_current_AxisCount");
            dt.Rows.Add("85", "84", "servo_current_AxisNr");
            dt.Rows.Add("86", "84", "servo_current_Result");
            dt.Rows.Add("87", "84", "servo_current_AxisValue");
            dt.Rows.Add("88", "04", "READ_spindle_current: 讀取主軸負載電流");
            dt.Rows.Add("89", "88", "spindle_current_AxisCount");
            dt.Rows.Add("90", "88", "spindle_current_AxisNr");
            dt.Rows.Add("91", "88", "spindle_current_Result");
            dt.Rows.Add("92", "88", "spindle_current_AxisValue");
            dt.Rows.Add("93", "04", "READ_servo_load: 讀取伺服軸負載");
            dt.Rows.Add("94", "93", "servo_load_AxisCount");
            dt.Rows.Add("95", "93", "servo_load_AxisNr");
            dt.Rows.Add("96", "93", "servo_load_Result");
            dt.Rows.Add("97", "93", "servo_load_AxisValue");
            dt.Rows.Add("98", "04", "READ_spindle_load: 讀取主軸負載");
            dt.Rows.Add("99", "98", "spindle_load_AxisCount");
            dt.Rows.Add("100", "98", "spindle_load_AxisNr");
            dt.Rows.Add("101", "98", "spindle_load_Result");
            dt.Rows.Add("102", "98", "spindle_load_AxisValue");
            dt.Rows.Add("103", "04", "READ_spindle_speed: 讀取主軸轉速");
            dt.Rows.Add("104", "103", "spindle_speed_AxisCount");
            dt.Rows.Add("105", "103", "spindle_speed_AxisNr");
            dt.Rows.Add("106", "103", "spindle_speed_Result");
            dt.Rows.Add("107", "103", "spindle_speed_AxisValue");
            dt.Rows.Add("108", "04", "READ_spindle_temperature: 讀取主軸溫度");
            dt.Rows.Add("109", "108", "spindle_speed_AxisValue");
            return dt;
        }

        private void TreeRootExist(DataTable dt)
        {
            EnumerableRowCollection<DataRow> result = dt.AsEnumerable().Where(r => r.Field<string>(this.ParentIDColIndex) == null);
            if (result.Any() == false)
                throw new Exception("沒有Root節點的資料，建立TreeView失敗");
            if (result.Count() > 1)
                throw new Exception("由於多個Root故無法建立TreeView");
        }

        private DataRow GetTreeRoot(DataTable dt)
        {
            return dt.AsEnumerable().Where(r => r.Field<string>(this.ParentIDColIndex) == null).First();
        }

        private IEnumerable<DataRow>GetTreeNodes(DataTable dt,TreeNode Node)
        {
            return dt.AsEnumerable().Where(r => r.Field<string>(this.ParentIDColIndex) == Node.Name).OrderBy(r => r.Field<string>(this.IDColIndex));

        }
       

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //Index,Name,Text,Tag
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Index:{e.Node.Index}");
            sb.AppendLine($"Name:{e.Node.Name}");
            sb.AppendLine($"Text:{e.Node.Text}");
            sb.AppendLine($"Tag:{e.Node.Tag}");

            //父節點
            string parent = e.Node.Parent == null ? "Root" : e.Node.Parent.Text;
            sb.AppendLine($"Parent:{ parent}");
            sb.AppendLine($"Count:{e.Node.GetNodeCount(false)}");
            sb.AppendLine($"FullPath:{e.Node.FullPath}");
            sb.AppendLine($"FullPath:{ e.Node.Level}");
            //sb.AppendLine($"Instruction:");
            txtNodeInfo.Text = sb.ToString();
        }

        private void treeView1_AfterCheck(object sender, TreeViewEventArgs e)
        {

            foreach(TreeNode ChildeNode in e.Node.Nodes)
            {
                ChildeNode.Checked = e.Node.Checked;

            }
        }

        private int IDColIndex = 0;
        private int ParentIDColIndex = 1;
        private int TextColIndex = 2;
        //ini dll import
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool WritePrivateProfileString(string sectionName, string keyName, string keyValue, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetPrivateProfileString(string sectionName, string keyName, string defaultReturnString, StringBuilder returnString, int returnStringLength, string filePath);
        //卡這邊 怪怪的
        public void write_ini(TreeViewEventArgs e)
        {
            if (e.Node.Checked == true)
            {
                //write to ini
                for (int i = 0; i <= e.Node.Nodes.Count;i++)
                {
                    WritePrivateProfileString("Tag Check", e.Node.Text, "true", ini_path);
                }
            }

        }
        //讀取ini項目
        public void read_ini()
        {
            //NA表示為預設值
            StringBuilder remoteIP = new StringBuilder(255);
            GetPrivateProfileString("Connect setting", "remoteIP", "NA", remoteIP, 255, ini_path);
            //txt_RemoteIP.Text = Convert.ToString(remoteIP);


            // chechbox_newtag_state = Convert.ToBoolean(newtag);

        }

        private void btn_confilm_checkbox_Click(object sender, EventArgs e)
        {
         
          
        }
    }
}
