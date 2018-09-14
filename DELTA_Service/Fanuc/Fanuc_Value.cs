using DELTA_Form;
using DELTA_Service.CNC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DELTA_Service.Fanuc
{
   public class Fanuc_Value
    {


        ini_controll ic = new ini_controll();
        Service1 main = new Service1();



        public ushort flib_Handle;

        public string xml_name;
        //Fanuc data
        public double Angle;//微調角度
        public long VO;//主電源電壓
        public long VG;//伺服電壓
        public long SG;//伺服速度
        public long CS;//選擇電路的模式
        public long I;//脈沖放電時間
        public long OFF;//脈沖休止時間
        public long WS;//線速
        public long WT;//線張力
        public long FL;//變頻器頻率 (水壓 )
        public long WR;//水阻值
        public long HS_P;//HS参数
        public long AD_P;//AD参数
        public long N_P;//N參數
        public long Server_Mode;//伺服模式
        public long Pro_Volt;//加工電壓



        public void Update(string name, Fanuc_Controll F_Controll,int no)//name=tagname
        {
            main.eventLog1.Source = "MySource";
            try
            {
                switch (name)
                {                  
                    case "Angle":
                            Fanuc_Controll fc_Angle = new Fanuc_Controll();
                            fc_Angle.Connect(no);
                            int Angle_Temp = 2;
                            int Angle_byte = 0;
                            short Angle_result = Focas1.cnc_rdcexesram(fc_Angle.flib_Handle, 64, ref Angle_byte, ref Angle_Temp);
                            Angle = Convert.ToSingle(Angle_byte)*0.001;
                            fc_Angle.DisConnect();
                        break;
                    case "VO":
                            Fanuc_Controll fc_VO = new Fanuc_Controll();
                            fc_VO.Connect(no);
                            int VO_Temp = 2;
                            int VO_byte = 0;
                            short VO_result = Focas1.cnc_rdcexesram(fc_VO.flib_Handle, 0, ref VO_byte, ref VO_Temp);
                            VO = Convert.ToInt16(VO_byte);
                            fc_VO.DisConnect();
                        break;
                    case "VG":
                            Fanuc_Controll fc_VG = new Fanuc_Controll();
                            fc_VG.Connect(no);
                            int VG_Temp = 2;
                            int VG_byte = 0;
                            short VG_result = Focas1.cnc_rdcexesram(fc_VG.flib_Handle, 2, ref VG_byte, ref VG_Temp);
                            VG = Convert.ToInt16(VG_byte);
                            fc_VG.DisConnect();
                        break;
                    case "SG":
                            Fanuc_Controll fc_SG = new Fanuc_Controll();
                            fc_SG.Connect(no);
                            int SG_Temp = 2;
                            int SG_byte = 0;
                            short SG_result = Focas1.cnc_rdcexesram(fc_SG.flib_Handle, 4, ref SG_byte, ref SG_Temp);
                            SG = Convert.ToInt16(SG_byte);
                            fc_SG.DisConnect();
                        break;
                    case "CS":
                            Fanuc_Controll fc_CS = new Fanuc_Controll();
                            fc_CS.Connect(no);
                            int CS_Temp = 2;
                            int CS_byte = 0;
                            short CS_result = Focas1.cnc_rdcexesram(fc_CS.flib_Handle, 6, ref CS_byte, ref CS_Temp);
                            CS = Convert.ToInt16(CS_byte);
                            fc_CS.DisConnect();
                        break;
                    case "I":
                            Fanuc_Controll fc_I = new Fanuc_Controll();
                            fc_I.Connect(no);
                            int I_Temp = 2;
                            int I_byte = 0;
                            short I_result = Focas1.cnc_rdcexesram(fc_I.flib_Handle, 8, ref I_byte, ref I_Temp);
                            I = Convert.ToInt16(I_byte);
                            fc_I.DisConnect();
                        break;
                    case "OFF":
                            Fanuc_Controll fc_OFF = new Fanuc_Controll();
                            fc_OFF.Connect(no);
                            int OFF_Temp = 2;
                            int OFF_byte = 0;
                            short OFF_result = Focas1.cnc_rdcexesram(fc_OFF.flib_Handle, 10, ref OFF_byte, ref OFF_Temp);
                            OFF = Convert.ToInt16(OFF_byte);
                            fc_OFF.DisConnect();
                        break;
                    case "WS":
                            Fanuc_Controll fc_WS = new Fanuc_Controll();
                            fc_WS.Connect(no);
                            int WS_Temp = 2;
                            int WS_byte = 0;
                            short WS_result = Focas1.cnc_rdcexesram(fc_WS.flib_Handle, 16, ref WS_byte, ref WS_Temp);
                            WS = Convert.ToInt16(WS_byte);
                            fc_WS.DisConnect();
                        break;
                    case "WT":
                            Fanuc_Controll fc_WT = new Fanuc_Controll();
                            fc_WT.Connect(no);
                            int WT_Temp = 2;
                            int WT_byte = 0;
                            short WT_result = Focas1.cnc_rdcexesram(fc_WT.flib_Handle, 18, ref WT_byte, ref WT_Temp);
                            WT = Convert.ToInt16(WT_byte);
                            fc_WT.DisConnect();
                        break;
                    case "FL":
                            Fanuc_Controll fc_FL = new Fanuc_Controll();
                            fc_FL.Connect(no);
                            int FL_Temp = 2;
                            int FL_byte = 0;
                            short FL_result = Focas1.cnc_rdcexesram(fc_FL.flib_Handle, 20, ref FL_byte, ref FL_Temp);
                            FL = Convert.ToInt16(FL_byte);
                            fc_FL.DisConnect();           
                        break;
                    case "WR":                
                            Fanuc_Controll fc_WR = new Fanuc_Controll();
                            fc_WR.Connect(no);
                            int WR_Temp = 2;
                            int WR_byte = 0;
                            short WR_result = Focas1.cnc_rdcexesram(fc_WR.flib_Handle, 30, ref WR_byte, ref WR_Temp);
                            WR = Convert.ToInt16(WR_byte);
                            fc_WR.DisConnect();                      
                        break;
                    case "HS_P":                  
                            Fanuc_Controll fc_HS_P = new Fanuc_Controll();
                            fc_HS_P.Connect(no);
                            int HS_P_Temp = 2;
                            int HS_P_byte = 0;
                            short HS_P_result = Focas1.cnc_rdcexesram(fc_HS_P.flib_Handle, 12, ref HS_P_byte, ref HS_P_Temp);
                            HS_P = Convert.ToInt16(HS_P_byte);
                            fc_HS_P.DisConnect();                      
                        break;
                    case "AD_P":
                        
                            Fanuc_Controll fc_AD_P = new Fanuc_Controll();
                            fc_AD_P.Connect(no);
                            int AD_P_Temp = 2;
                            int AD_P_byte = 0;
                            short AD_P_result = Focas1.cnc_rdcexesram(fc_AD_P.flib_Handle, 14, ref AD_P_byte, ref AD_P_Temp);
                            AD_P = Convert.ToInt16(AD_P_byte);
                            fc_AD_P.DisConnect();                      
                        break;
                    case "N_P":                  
                            Fanuc_Controll fc_N_P = new Fanuc_Controll();
                            fc_N_P.Connect(no);
                            int N_P_Temp = 2;
                            int N_P_byte = 0;
                            short N_P_result = Focas1.cnc_rdcexesram(fc_N_P.flib_Handle, 28, ref N_P_byte, ref N_P_Temp);
                            N_P = Convert.ToInt16(N_P_byte);
                            fc_N_P.DisConnect();                     
                        break;
                    case "Server_Mode":                    
                            Fanuc_Controll fc_Server_Mode = new Fanuc_Controll();
                            fc_Server_Mode.Connect(no);
                            int Server_Mode_Temp = 2;
                            int Server_Mode_byte = 0;
                            short Server_Mode_result = Focas1.cnc_rdcexesram(fc_Server_Mode.flib_Handle, 38, ref Server_Mode_byte, ref Server_Mode_Temp);
                            Server_Mode = Convert.ToInt16(Server_Mode_byte);
                            fc_Server_Mode.DisConnect();                   
                        break;
                    case "Pro_Volt":
 
                            Fanuc_Controll fc_Pro_Volt = new Fanuc_Controll();
                            fc_Pro_Volt.Connect(no);
                            int Pro_Volt_Temp = 2;
                            int Pro_Volt_byte = 0;
                            short Pro_Volt_result = Focas1.cnc_rdcexesram(fc_Pro_Volt.flib_Handle, 36, ref Pro_Volt_byte, ref Pro_Volt_Temp);
                            Pro_Volt = Convert.ToInt64(Pro_Volt_byte/10);
                            fc_Pro_Volt.DisConnect();                  
                        break;
                    default:
                        main.eventLog1.WriteEntry("default name "+ name.ToString());
                        break;
                }
            }
            catch (Exception ex)
            {
                main.eventLog1.WriteEntry("Fanuc進行數值Update時發生錯誤,result is " + ex.ToString());
            }
            finally
            {
                GC.Collect();              //加上GC.Collect(); 回收暫用掉的記憶體
            }
        }
        public object ReturnValue(string name)
        {
            Service1 main = new Service1();
            main.eventLog1.Source = "MySource";

            switch (name)
            {
                case "Angle":              
                    return Angle;
                case "VO":
                    return VO;
                case "VG":
                    return VG;
                case "SG":
                    return SG;
                case "CS":
                    return CS;
                case "I":
                    return I;
                case "OFF":
                    return OFF;
                case "WS":
                    return WS;
                case "WT":
                    return WT;
                case "FL":
                    return FL;
                case "WR":
                    return WR;
                case "HS_P":
                    return HS_P;
                case "AD_P":
                    return AD_P;
                case "N_P":              
                    return N_P;
                case "Server_Mode":
                    return Server_Mode;
                case "Pro_Volt":
                    return Pro_Volt;
                default:
                    return 0;
            }


        }

    }
}
