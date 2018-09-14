using DELTA_Service;
using DELTA_Service.CNC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DELTA_Form
{
    //這個class 包含與Fanuc的連線以及斷線
    public class Fanuc_Controll
    {

        public int result;
       // public Service1 main = null;
        public ini_controll ic = new ini_controll();

        public string[] fanuc_IP = new string[30];
        public string[] port = new string[30];
        public string[] timeout = new string[30];
        public ushort flib_Handle;
        public string xml_name;
        public int no;
        public int count;
        //Fanuc data


        public void Connect(int no)
        {
            this.no = no;
            Service1 main = new Service1();
            main.eventLog1.Source = "MySource";
            Get_fanuc_data();
          
            
            switch (no) {
                case 0:
                try
            {
                result = Focas1.cnc_allclibhndl3((object)fanuc_IP[0], Convert.ToUInt16(port[0]), Convert.ToInt32(timeout[0]), out flib_Handle);
                       
            }
            catch (Exception ex)
            {
                main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
            }
                    break;
                case 1:
                    try
                    {

                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[1], Convert.ToUInt16(port[1]), Convert.ToInt32(timeout[1]), out flib_Handle);

                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 2:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[2], Convert.ToUInt16(port[2]), Convert.ToInt32(timeout[2]), out flib_Handle);
                    }
                    catch(Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 3:                 
                        try
                        {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[3], Convert.ToUInt16(port[3]), Convert.ToInt32(timeout[3]), out flib_Handle);
                    }
                        catch(Exception ex)
                        {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                        break;
                case 4:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[4], Convert.ToUInt16(port[4]), Convert.ToInt32(timeout[4]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 5:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[5], Convert.ToUInt16(port[5]), Convert.ToInt32(timeout[5]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 6:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[6], Convert.ToUInt16(port[6]), Convert.ToInt32(timeout[6]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 7:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[7], Convert.ToUInt16(port[7]), Convert.ToInt32(timeout[7]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 8:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[8], Convert.ToUInt16(port[8]), Convert.ToInt32(timeout[8]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 9:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[9], Convert.ToUInt16(port[9]), Convert.ToInt32(timeout[9]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 10:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[10], Convert.ToUInt16(port[10]), Convert.ToInt32(timeout[10]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 11:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[11], Convert.ToUInt16(port[11]), Convert.ToInt32(timeout[11]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 12:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[12], Convert.ToUInt16(port[12]), Convert.ToInt32(timeout[12]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 13:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[13], Convert.ToUInt16(port[13]), Convert.ToInt32(timeout[13]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 14:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[14], Convert.ToUInt16(port[14]), Convert.ToInt32(timeout[14]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 15:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[15], Convert.ToUInt16(port[15]), Convert.ToInt32(timeout[15]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 16:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[16], Convert.ToUInt16(port[16]), Convert.ToInt32(timeout[16]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 17:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[17], Convert.ToUInt16(port[17]), Convert.ToInt32(timeout[17]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 18:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[18], Convert.ToUInt16(port[18]), Convert.ToInt32(timeout[18]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 19:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[19], Convert.ToUInt16(port[19]), Convert.ToInt32(timeout[19]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 20:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[20], Convert.ToUInt16(port[20]), Convert.ToInt32(timeout[20]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 21:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[21], Convert.ToUInt16(port[21]), Convert.ToInt32(timeout[21]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 22:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[22], Convert.ToUInt16(port[22]), Convert.ToInt32(timeout[22]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 23:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[23], Convert.ToUInt16(port[23]), Convert.ToInt32(timeout[23]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 24:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[24], Convert.ToUInt16(port[24]), Convert.ToInt32(timeout[24]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 25:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[25], Convert.ToUInt16(port[25]), Convert.ToInt32(timeout[25]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 26:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[26], Convert.ToUInt16(port[26]), Convert.ToInt32(timeout[26]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 27:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[27], Convert.ToUInt16(port[27]), Convert.ToInt32(timeout[27]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 28:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[28], Convert.ToUInt16(port[28]), Convert.ToInt32(timeout[28]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 29:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[29], Convert.ToUInt16(port[29]), Convert.ToInt32(timeout[29]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                case 30:
                    try
                    {
                        result = Focas1.cnc_allclibhndl3((object)fanuc_IP[30], Convert.ToUInt16(port[30]), Convert.ToInt32(timeout[30]), out flib_Handle);
                    }
                    catch (Exception ex)
                    {
                        main.eventLog1.WriteEntry("Connect Fanuc fail,result:" + ex);
                    }
                    break;
                default:
                    main.eventLog1.WriteEntry("超出系統可支援的最大設備量");
                    break;
            }
            
        }
        public void DisConnect()
        {
              Service1 main = new Service1();
              Focas1.cnc_freelibhndl(flib_Handle);
              //main.eventLog1.WriteEntry("已終止連線");
        }
        //此部分會讀取ini裡面的設定
        public void Get_fanuc_data()
        {
            count= Convert.ToInt32(ic.read_ini("Count", "Fanuc", "1"));//最後面為Fanuc預設的IP

            int y = 1;
            for (int i = 0; i < count; i++)
            {
                fanuc_IP[i] = ic.read_ini("Fanuc Setting", "IP" + y, "192.168.152.89");
                port[i] = ic.read_ini("Fanuc Setting", "port" + y, "8193");
                timeout[i] = ic.read_ini("Fanuc Setting", "timeout" + y, "3000");
                Console.WriteLine("Fanuc IP=" + fanuc_IP[i] + "  ,port=" + port[i] + "   ,timeout=" + timeout[i]);
                Console.WriteLine("Fanuc Setting" + " " + "IP" + y + " " + "192.168.152.89");
                y++;
            }
        }
    }
}
