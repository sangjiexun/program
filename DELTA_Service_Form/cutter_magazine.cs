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
    public partial class cutter_magazine : Form
    {
        //
        string ini_path = @"C:\youngtec_delta\DELTA.ini";
        bool IsConfirm;
        string cutter_Index;
        string cutter_length;
        string magazine_magaID;
        string current_code_CodeCount;
        public cutter_magazine()
        {
            InitializeComponent();
        }

        private void cutter_magazine_Load(object sender, EventArgs e)
        {
            Readini();
            //Get default value
            cutter_Index = txt_cutter_Index.Text.Trim();
            cutter_length = txt_cutter_length.Text.Trim();
            magazine_magaID = txt_magazine_magaID.Text.Trim();
            current_code_CodeCount = txt_current_code_CodeCount.Text.Trim();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            txt_cutter_Index.Text = 0.ToString();
            txt_cutter_length.Text = 5.ToString();
            txt_magazine_magaID.Text = 0.ToString();
            txt_current_code_CodeCount.Text = 5.ToString();

        }
        //ini dll import
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool WritePrivateProfileString(string sectionName, string keyName, string keyValue, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetPrivateProfileString(string sectionName, string keyName, string defaultReturnString, StringBuilder returnString, int returnStringLength, string filePath);

        private void Readini()
        {
            //cuttre_index的初始值設定為0
            StringBuilder cutter_index = new StringBuilder(255);
            GetPrivateProfileString("CNC Information", "cutter_lndex", "0", cutter_index, 255, ini_path);
            txt_cutter_Index.Text = Convert.ToString(cutter_index);
            //Length的初始值設定為5
            StringBuilder cutter_length = new StringBuilder(255);
            GetPrivateProfileString("CNC Information","cutter_length","5",cutter_length,255,ini_path);
            txt_cutter_length.Text = Convert.ToString(cutter_length);
            //MagaID 的初始值設定為0
            StringBuilder magazine_magaID = new StringBuilder(255);
            GetPrivateProfileString("CNC Information","magazine_magaID","0",magazine_magaID,255,ini_path);
            txt_magazine_magaID.Text = Convert.ToString(magazine_magaID);
            //current_code_CodeCount的初始值設定為5
            StringBuilder current_code_CodeCount = new StringBuilder(255);
            GetPrivateProfileString("CNC Information","current_code_CodeCount","5",current_code_CodeCount,255,ini_path);
            txt_current_code_CodeCount.Text = Convert.ToString(current_code_CodeCount);
        }
        private void Writeini()
        {
            //防止輸入數值超出範圍
            if (Convert.ToInt32(txt_cutter_Index.Text.Trim()) >= 0 && Convert.ToInt32(txt_cutter_length.Text.Trim()) >= 0 && Convert.ToInt32(txt_current_code_CodeCount.Text.Trim()) >= 1 && Convert.ToInt32(txt_current_code_CodeCount.Text.Trim()) <= 50)
            {
                WritePrivateProfileString("CNC Information", "cutter_Index", txt_cutter_Index.Text.Trim(), ini_path);
                WritePrivateProfileString("CNC Information", "cutter_length", txt_cutter_length.Text.Trim(), ini_path);
                WritePrivateProfileString("CNC Information", "magazine_magaID", txt_magazine_magaID.Text.Trim(), ini_path);
                WritePrivateProfileString("CNC Information", "current_code_CodeCount", txt_current_code_CodeCount.Text.Trim(), ini_path);
            }
            else
            {
                MessageBox.Show("所輸入的數值不符合範圍");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Writeini();
                MessageBox.Show("寫入成功!");
            }
            catch
            {
                MessageBox.Show("寫入ini檔發生錯誤");
            }
        }
        
        private void cutter_magazine_FormClosed(object sender, FormClosedEventArgs e)
        {
           
        }

        private void cutter_magazine_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!cutter_Index.Equals(txt_cutter_Index.Text.Trim())||!cutter_length.Equals(txt_cutter_length.Text.Trim())||!magazine_magaID.Equals(txt_magazine_magaID.Text.Trim())||!current_code_CodeCount.Equals(txt_current_code_CodeCount.Text.Trim()))
            {
                if (DialogResult.Yes == MessageBox.Show("有更改參數設定，是否不儲存離開?", "參數異動", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
                {
                    return;
                }
                else
                {
                    e.Cancel = true;

                }

            }          
        }

       
    }
}
