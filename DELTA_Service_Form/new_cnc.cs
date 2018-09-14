using CNCNetLib;
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

namespace DELTA_Service_Form
{

    public partial class new_cnc : Form
    {
        string ini_path = @"C:\youngtec_delta\DELTA.ini";
        private CNCNetClass CNCnet = new CNCNetClass();
        private string default_localIP;
        public new_cnc()
        {
            InitializeComponent();
        }

        private void new_cnc_Load(object sender, EventArgs e)
        {
            Current_IP_Read();
            Lv_Addresses.Enabled = false;
            Readini();
           
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
            //------------------------get default CNC Connect Information
           
        }

        private void checkBox_Different_LocalIP_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_Different_LocalIP.Checked == true)
            {
                Lv_Addresses.Enabled = true;
            }
            else
            {
                Lv_Addresses.Enabled = false;
            }
        }

        //ini dll import
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool WritePrivateProfileString(string sectionName, string keyName, string keyValue, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetPrivateProfileString(string sectionName, string keyName, string defaultReturnString, StringBuilder returnString, int returnStringLength, string filePath);

        private void Readini()
        {
            ListViewItem LVI = new ListViewItem();
        
            //cuttre_index的初始值設定為0
            StringBuilder localIP = new StringBuilder(255);
            GetPrivateProfileString("Connect setting", "localIP", "0", localIP, 255, ini_path);
            LV_ALLCNC.Items.Add(1.ToString(),Convert.ToString(localIP),0);
            default_localIP = Convert.ToString(localIP);

            StringBuilder remoteIP = new StringBuilder(255);
            GetPrivateProfileString("Connect setting","remoteIP","0",remoteIP,255,ini_path);
            LV_ALLCNC.Items[1.ToString()].SubItems.Add(Convert.ToString(remoteIP));

            StringBuilder timeout = new StringBuilder(255);
            GetPrivateProfileString("Connect setting", "timeout", "0", timeout, 255, ini_path);
            LV_ALLCNC.Items[1.ToString()].SubItems.Add(Convert.ToString(timeout));

  
           

        }
        private void Writeini()
        {
            // WritePrivateProfileString("CNC Information", "cutter_Index", txt_cutter_Index.Text.Trim(), ini_path);
            WritePrivateProfileString("CNC","Count",Convert.ToString(LV_ALLCNC.Items.Count),ini_path);

            if (checkBox_Different_LocalIP.Checked == false)
            {
                WritePrivateProfileString("Connect setting", "localIP2", LV_ALLCNC.SelectedItems[0].Text.Trim(), ini_path);
            }
            else
            {
                WritePrivateProfileString("Connect setting", "localIP2", default_localIP, ini_path);
            }
            WritePrivateProfileString("Connect setting", "remoteIP2", txt_RemoteIP.Text.Trim(), ini_path);
            WritePrivateProfileString("Connect setting","timeout2",txt_Timeout.Text.Trim(),ini_path);
        }
        private void btn_new_connectInformation_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(txt_RemoteIP.Text) && !string.IsNullOrWhiteSpace(txt_Timeout.Text))
                {
                    Writeini();
                    MessageBox.Show("寫入設定檔完成");
                }
                else
                {
                    MessageBox.Show("remote IP,timeout 不得為空");
                }         
            }
            catch
            {
                MessageBox.Show("寫入設定檔失敗");
            }
        }
    }
}
