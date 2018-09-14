using CNCNetLib;
using System;
using System.Windows.Forms;

namespace DELTA_Form
{
    public partial class Form1 : Form
    {
        private string[] args = new string[0];
        private CNCNetClass CNCnet = new CNCNetClass();

        public CNCInfoClass cnc_info;
        //public Param new_para;

        public Form1()
        {
            InitializeComponent();
        }
        /*
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Current_IP_Read();//IP讀取
            List_cns.Enabled = false;//封鎖List
            //Read_BroadCast();
        }

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

        private void Lv_Addresses_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void Lv_Addresses_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            /*if (DialogResult.Yes == MessageBox.Show("是否以此IP進行Broadcast搜尋?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                localIP = e.Item.Text;
                List_cns.Items.Clear();
                string[] Broadcast = CNCnet.BroadcastGetCNC(e.Item.Text, e.Item.SubItems[2].Text);
                if (Broadcast.Count() > 0)
                {
                    foreach (string broadcast in Broadcast)
                    {
                        List_cns.Items.Add(broadcast);
                    }
                    List_cns.Enabled = true;
                }
                if (List_cns.Items.Count == 0)
                {
                    List_cns.Items.Add("Can not find Broadcast!");
                }
            }
            else
            {
                return;
            }
        }

        /// <summary>
        /// start button Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (!Lv_Addresses.SelectedItems.Count.Equals(0))
            {
                bool exportConfig = false;
                string strConfigName = "";
                string strApplicationDir = "";
                //Get application info
                GetConfigInfo(ref strConfigName, ref strApplicationDir, args, ref exportConfig);

                CNCInfoClass info = new CNCInfoClass();
                info.SetConnectInfo(Lv_Addresses.SelectedItems[0].Text, "192.168.154.95", 5000);
                MessageBox.Show("IP 位址:" + Lv_Addresses.SelectedItems[0].Text);
                int isFailed = info.Connect();

                TagData.info = info;
                cnc_info = info;
                if (isFailed.Equals(0))
                {
                    Value.main_form = this;
                    MemInterface.main = this;

                    new_para = new Param(info, 0);
                    //Start the interface to shared memory

                    MemInterface.loop_switch = true;
                    MemInterface.Start(args, strConfigName, strApplicationDir, exportConfig);
                    txt_connect_state.Text = "CID 啟動中";
                }
                else
                {
                    MessageBox.Show("連線失敗，請重新嘗試");
                    txt_connect_state.Text = ErrorString.errorstring(isFailed);
                }
            }
            else
            {
                MessageBox.Show("請選擇 IP 再啟動程式");
                txt_connect_state.Text = "請選擇 IP 再啟動程式";
            }
        }
    
        private void List_cns_SelectedIndexChanged(object sender, EventArgs e)
        {
            /*if (DialogResult.Yes == MessageBox.Show("是否進行連線?", "確認視窗", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                //READ_Channel_information
                uint channel;
                ushort[] AxesOfChannel;
                ushort[] ServoAxesofChannel;
                ushort[] MaxAxisOfChannel;
                string[,] AxisNameOfChannel;
                ushort[] DecPointOfChannel;

                //READ_channel_setting
                ushort AxisNum;
                ushort[] AxisID;
                bool[] IsEnable;
                short[] AxisTypeID;
                short[] PortID;
                string[] AxisName;

                CNCInfoClass CNCInfo = new CNCInfoClass(localIP, List_cns.Items.ToString(), 3000); //local,remote,timeout
                int state = CNCInfo.Connect();
                if (state == 0)
                {
                    int state_channel = CNCInfo.READ_Channel_information(out channel, out AxesOfChannel, out ServoAxesofChannel, out MaxAxisOfChannel, out AxisNameOfChannel, out DecPointOfChannel);
                    txt_connect_state.Text = "Connect Success";
                    if (state_channel == 0)
                    {
                        Console.WriteLine("Channel=" + channel + Environment.NewLine + "AxesOfChannel=" + AxesOfChannel + Environment.NewLine + "ServoAxesofChannel=" + ServoAxesofChannel + Environment.NewLine + "MaxAxisOfChannel=" + MaxAxisOfChannel + Environment.NewLine + "AxisNameOfChannel" + AxisNameOfChannel + Environment.NewLine + "DecPointOfChannel" + DecPointOfChannel + Environment.NewLine);
                        //Run channel infomation
                        int Read_channel_Setting = CNCInfo.READ_channel_setting(Convert.ToUInt16(channel), out AxisNum, out AxisID, out IsEnable, out AxisTypeID, out PortID, out AxisName);
                        if (Read_channel_Setting == 0)
                        {
                            //Success! Add to ListView
                            //暫時先抓第一筆
                            ListViewItem Channel_Items = new ListViewItem();
                            Channel_Items.Text = Convert.ToString(AxisNum);
                            Channel_Items.SubItems.Add(Convert.ToString(AxisID[0]));
                            Channel_Items.SubItems.Add(Convert.ToString(IsEnable[0]));
                            Channel_Items.SubItems.Add(Convert.ToString(AxisTypeID[0]));
                            Channel_Items.SubItems.Add(Convert.ToString(PortID[0]));
                            Channel_Items.SubItems.Add(Convert.ToString(AxisName[0]));

                            List_Channels.Items.Add(Channel_Items);
                        }
                    }
                    else if (state_channel < 0)
                    {
                        Console.WriteLine("Read Channel infromation error! Result:" + ErrorString.errorstring(state_channel));
                    }
                    else
                    {
                        Console.WriteLine("Error code over range");
                    }
                }
                else if (state < 0)
                {
                    txt_connect_state.Text = "Connect Fail! Result : " + ErrorString.errorstring(state);
                }
                else
                {
                    txt_connect_state.Text = "Connect Fail!";
                    Console.WriteLine("Connect Fail! Error code is over range,Error code is " + state);
                }
            }
            else
            {
                return;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("");
        }

        private void btn_XML_Create_Click(object sender, EventArgs e)
        {
            try
            {
                bool exportConfig = true;
                string strConfigName = "";//Program name.
                string strApplicationDir = "";//Program path.

                // Get application info for naming the shared memory file and mutex
                GetConfigInfo(ref strConfigName, ref strApplicationDir, args, ref exportConfig);

                // Start the interface to shared memory
                MemInterface.Start(args, strConfigName, strApplicationDir, exportConfig);

                MessageBox.Show("XML 檔案產生完成!");
                txt_connect_state.Text = "已產生 XML ，請至程式目錄下開啟";
            }
            catch (Exception E)
            {
                Console.WriteLine(E);
                MessageBox.Show("XML 檔案產生失敗!");
                txt_connect_state.Text = "請重新產生 XML";
            }
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

        private void Btn_Stop_Click(object sender, EventArgs e)
        {
            MemInterface.loop_switch = false;
        }

        public static implicit operator Form1(global::DELTA_Service.Service1 v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator Form1(global::DELTA_Service.Service1 v)
        {
            throw new NotImplementedException();
        }
    */
    }
}