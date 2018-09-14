using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DELTA_Service.CNC
{
  public class ini_controll
    {
        public string FilePath = @"C:\youngtec_delta\DELTA.ini";// ini path
        //this class for write and read with ini
        //ini dll import
        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool WritePrivateProfileString(string sectionName, string keyName, string keyValue, string filePath);

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern int GetPrivateProfileString(string sectionName, string keyName, string defaultReturnString, StringBuilder returnString, int returnStringLength, string filePath);
        /// <summary>
        /// 讀取ini檔中的資料 
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="keyname"></param>
        /// <param name="defaultReturnString"></param>

        /// <returns></returns>
        public string read_ini(string sectionName,string keyname,string defaultReturnString)
        {
            string outprint="";
            StringBuilder temp_read_text=new StringBuilder();
            GetPrivateProfileString(sectionName, keyname, defaultReturnString, temp_read_text, 255, FilePath);
            outprint = Convert.ToString(temp_read_text);
            return outprint;
        }
        /// <summary>
        /// 寫入ini檔
        /// </summary>
        /// <param name="sectionName"></param>
        /// <param name="keyname"></param>
        /// <param name="text"></param>
        public void write_ini(string sectionName,string keyname,string text)
        {
            WritePrivateProfileString(sectionName,keyname,text,FilePath);
        }
    }
}
