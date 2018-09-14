using CNCNetLib;
using DELTA_Service;
using DELTA_Service.CNC;

/*
 * 沒有放入Functions:READ_RIO_setting_single: 讀取單筆RIO設定
 * READ_PLC_ADDR_BUFFSIZE: 取得PLC  Variable需要Buffer  Size
 * READ_dir_list: 讀取磁碟目錄清單
 * READ_CodeSearch_Status:讀取斷點搜尋狀態
 *
 */

namespace DELTA_Form
{
    public class Param
    {
        public Service1 main = null;
        Service_Controll sc = new Service_Controll();
        ini_controll ic = new ini_controll();

        public Param()
        {
        }

        public string xml_name;

        public bool CNCFlag_WorkingFlag;
        public bool CNCFlag_AlarmFlag;
        public byte CNCFlag_RestartAct;

        /**/
        public uint CNC_information_CncType;

        public ushort CNC_information_MaxChannels;
        /**/
        public ushort CNC_information_Series;

        public string CNC_information_Nc_Ver;
        public ushort CNC_information_ScrewUnit;
        public ushort CNC_information_DisplayUnit;
        public uint CNC_information_ApiVersion;

        public uint Channel_information_Channels;
        public ushort[] Channel_information_AxesOfChannel;
        public ushort[] Channel_information_ServoAxesofChannel;
        public ushort[] Channel_information_MaxAxisOfChannel;
        public string[,] Channel_information_AxisNameOfChannel;
        public ushort[] Channel_information_DecPointOfChannel;

        public ushort axis_count_AxisCount;
        public ushort spindle_count_AxisCount;

        //CNC 狀態
        public string status_MainProg;

        public string status_CurProg;
        public string status_ProgPath;
        public int status_CurSeq;
        public int status_MDICur;
        public int status_Mode;
        public int status_Status;
        public bool status_IsAlarm;
        public bool status_IsWorking;
        public byte status_RestartAct;

        //CNC Motion 狀態
        public ushort NCMOTION_Unit;

        public uint NCMOTION_iSpSpeed;
        public double NCMOTION_iFeed;
        public short NCMOTION_iLoad;
        public uint NCMOTION_iActSpeed;
        public double NCMOTION_iActFeed;
        public uint NCMOTION_iDwellTime;
        public short NCMOTION_iMCutter;
        public ushort NCMOTION_iCutterLib;
        public ushort NCMOTION_iCmdCutter;
        public ushort NCMOTION_iStandbyCutter;
        public ushort NCMOTION_iStandbyCast;
        public ushort NCMOTION_iDData;
        public ushort NCMOTION_iHData;
        public ushort NCMOTION_iTData;
        public ushort NCMOTION_iMData;
        public char[] NCMOTION_GGroup;
        public ushort NCMOTION_iSpeedF;
        public ushort NCMOTION_iSpeedRPD;
        public ushort NCMOTION_iSpeedS;
        public double NCMOTION_iSpeedJOG;
        public ushort NCMOTION_iSpeedMPG;

        //讀取進給率/轉速
        public double feed_spindle_OvFeed;

        public uint feed_spindle_OvSpindle;
        public double feed_spindle_ActFeed;
        public uint feed_spindle_ActSpindle;

        public string[] gcode_Gdata;

        public int othercode_HCode;
        public int othercode_DCode;
        public int othercode_TCode;
        public int othercode_MCode;
        public int othercode_FCode;
        public int othercode_SCode;

        public string CNC_HostName_strName;

        //全部目前警報個數
        public uint current_alarm_count_AlarmCount;

        //目前發生警報
        public ushort alm_current_AlarmCount;

        public ushort[] alm_current_AlarmCode;
        public ushort[] alm_current_AlarmDataLen;
        public ushort[,] alm_current_AlarmData;
        public string[] alm_current_AlarmMsg;
        public string[] alm_current_AlarmDateTime;

        //全部歷史警報個數
        public uint history_alarm_count_AlarmCount;

        /**/

        //伺服軸負載電流
        public ushort servo_current_AxisCount;

        public ushort[] servo_current_AxisNr;
        public bool[] servo_current_Result;
        public int[] servo_current_AxisValue;

        /**/

        //主軸負載電流
        public ushort spindle_current_AxisCount;

        public ushort[] spindle_current_AxisNr;
        public bool[] spindle_current_Result;
        public int[] spindle_current_AxisValue;

        /**/

        //伺服軸負載
        public ushort servo_load_AxisCount;

        public ushort[] servo_load_AxisNr;
        public bool[] servo_load_Result;
        public int[] servo_load_AxisValue;

        /**/

        //主軸負載
        public ushort spindle_load_AxisCount;

        public ushort[] spindle_load_AxisNr;
        public bool[] spindle_load_Result;
        public int[] spindle_load_AxisValue;

        /**/

        //伺服軸轉速
        public ushort servo_speed_AxisCount;

        public ushort[] servo_speed_AxisNr;
        public bool[] servo_speed_Result;
        public int[] servo_speed_AxisValue;

        /**/

        //主軸轉速
        public ushort spindle_speed_AxisCount;

        public ushort[] spindle_speed_AxisNr;
        public bool[] spindle_speed_Result;
        public int[] spindle_speed_AxisValue;

        /**/

        //伺服軸溫度
        public ushort servo_temperature_AxisCount;

        public ushort[] servo_temperature_AxisNr;
        public bool[] servo_temperature_Result;
        public int[] servo_temperature_AxisValue;

        /**/

        //主軸溫度
        public ushort spindle_temperature_AxisCount;

        public ushort[] spindle_temperature_AxisNr;
        public bool[] spindle_temperature_Result;
        public int[] spindle_temperature_AxisValue;

        //座標資訊
        //READ_POSITION: CNC各軸座標
        private ushort POSITION_Unit;

        private string[] POSITION_AxisName;
        private double[] POSITION_Coor_Mach, POSITION_Coor_Abs, POSITION_Coor_Rel, POSITION_Coor_Res, POSITION_Coor_Offset;

        //工件座標
        //READ_GPOSITION_Title: 讀取工件座標標題名稱
        private string[] GPOSITION_Title_GPOSITION_Title;

        //READ_GPOSITION: 讀取工件座標資料
        private double[,] GPOSITION_GPositionArray;

        //READ_Offset_Coord: 讀取偏移座標資料
        private double[] Offset_Coord_CoorArray;

        //READ_cutter_title  讀取刀具標題列
        public string[] cutter_title_CutterTitle;

        //READ_cutter_count 讀取刀具總筆數
        public ushort cutter_count_cutter_count;

        //READ_cutter: 讀取刀具資訊
        public double[,] cutter_CutterData;

        public ushort magazine_info_CutterNum;
        public ushort magazine_info_CMDCutterID;
        public ushort magazine_info_StandbyCutterID;
        public ushort magazine_info_StandbyMagaID;
        public short magazine_info_SPCutterID;
        public short[] magazine_info_CutterID;

        public uint processtime_TotalWorkTime;
        public uint processtime_SingleWorkTime;
        public ushort part_count_target_part_count;
        public ushort part_count_finish_part_count;

        //巨集變數
        //READ_MacroVariable: 讀取MMacro Variable
        private double[] MacroVariable_RetArray;

        //READ_MacroVariablebyID:  讀取Maccro  Variable
        private double MacroVariablebyID_RetValue;

        //PLC變數
        //READ_PLC_ADDR: 讀取PLC  Variable
        private byte[] PLC_ADDR_RetArray;

        //參數資訊
        //READ_CNCParameter_Single: 讀取單筆CNC
        private ushort CNCParameter_Single_GroupID, CNCParameter_Single_DataSize;

        private short CNCParameter_Single_SubGroupID, CNCParameter_Single_ParaChannel, CNCParameter_Single_ParaAxis, CNCParameter_Single_DataType;
        private byte[] CNCParameter_Single_Data;

        //系統資訊-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //READ_System_Time : 讀取系統日期時間
        public string System_Time_strSystemTime;

        //READ_system_variable : 讀取系統變數
        public ushort system_variable_SysVarNum;

        public ushort system_variable_AxisVarNum;
        public ushort[] system_variable_SysVarChannel;
        public ushort[] system_variable_SysVarID;
        public ushort[] system_variable_SysVarType;
        public byte[,] system_variable_SysVarValue;
        public ushort[] system_variable_AxisVarChannel;
        public ushort[] system_variable_AxisNum;
        public ushort[] system_variable_AxisVarID;
        public ushort[] system_variable_AxisVarType;
        public ushort[,] system_variable_AxisID;
        public byte[,,] system_variable_AxisVarValue;

        //READ_user_variable : 讀取用戶變數
        public ushort user_variable_VarNum;

        public ushort[] user_variable_VarReg;
        public ushort[] user_variable_VarValue;

        //READ_equip_variable : 讀取設備變數
        public ushort equip_variable_VarNum;

        public ushort[] equip_variable_VarReg;
        public ushort[] equip_variable_VarValue;

        //READ_system_status : 讀取系統狀態
        public ushort system_status_ParaCount;

        public ushort[] system_status_ParaID;
        public string[] system_status_ParaName;
        public ushort[] system_status_DataType;
        public ushort[] system_status_DataSize;
        public byte[,] system_status_Data;

        //READ_FW_version:  讀取韌體序號
        private ushort FW_version_ParaCount;

        private ushort[] FW_version_ParaID, FW_version_DataType, FW_version_DataSize;
        private string[] FW_version_ParaName;
        private byte[,] FW_version_Data;

        //READ_HW_version:  讀取硬體序號
        private ushort HW_version_ParaCount;

        private ushort[] HW_version_ParaID, HW_version_DataType, HW_version_DataSize;
        private string[] HW_version_ParaName;
        private byte[,] HW_version_Data;

        // READ_equip_infomation : 讀取設備資訊
        public ushort equip_infomation_InfoCount;

        public string[] equip_infomation_EquipInfo;

        //通道設定
        //READ_channel_setting: 讀取通道設定
        private ushort channel_setting_AxisNum;

        private ushort[] channel_setting_AxisID;
        private bool[] channel_setting_IsEnable;
        private short[] channel_setting_AxisTypeID, channel_setting_PortID;
        private string[] channel_setting_AxisName;

        //READ_port_info: 讀取埠設定
        private ushort port_info_PortNum;

        private ushort[] port_info_PortID;
        private bool[] port_info_IsEnable;
        private short[] port_info_ChannelID, port_info_AxisID, port_info_AxisTypeID;

        //RIO設定
        //READ_RIO_Status: 讀取RIO埠狀態
        private ushort RIO_Status_IO_Num;

        private bool[] RIO_Status_RIO_Status;

        //READ_RIO_setting_all 讀取全部RIO設定
        private ushort RIO_setting_all_IO_Num;

        private bool[] RIO_setting_all_IsEnable, RIO_setting_all_IsDisc;
        private short[] RIO_setting_all_RIOType;
        private uint[] RIO_setting_all_Polarity;

        //READ_RIO_home_limit: 讀取RIO原點極限設定
        private ushort RIO_home_limit_Port_Num;

        private bool[] RIO_home_limit_Homelimit;

        //READ_RIO_filter: 讀取RIO濾波等級
        private ushort RIO_filter_FilterLevel;

        //檔案程式資訊
        //READ_disk_quota:讀取磁碟剩餘空間
        private int disk_quota_diskquota;

        //READ_inter_disk_quota: 讀取Flash剩餘可用空間
        private uint inter_disk_quota_diskquota;

        //READ_nc_pointer : 讀取NC 執行行號
        public int nc_pointer_LineNum;

        public int nc_pointer_MDILineNum;

        //READ_preview_code: 讀取NC 預讀程式內容
        public uint[] preview_code_LineNo;

        public string[] preview_code_strCode;

        //READ_current_code : 讀取NC 當前程式內容
        public uint[] current_code_LineNo;

        public string[] current_code_strCode;

        //斷點搜尋
        //READ_CodeSearch_LineNo: 讀取斷點搜尋行號
        private int CodeSearch_LineNo_LineNo;

        //READ_CodeSearch_POSITION: 讀取斷點搜尋座標
        private ushort CodeSearch_POSITION_AxisNum;

        private ushort[] CodeSearch_POSITION_AxisID;
        private double[] CodeSearch_POSITION_Coor_Mach, CodeSearch_POSITION_Coor_Abs, CodeSearch_POSITION_Coor_Rel, CodeSearch_POSITION_Coor_Res, CodeSearch_POSITION_Coor_Offset;

        //手動輸入
        //READ_MDI_code: MDI執行內容讀取
        private uint MDI_code_LineNo;

        private string[] MDI_code_strCode;

        //系統監控
        //READ_RIO_MONITOR : I/O 監控
        public ushort RIO_MONITOR_RIONum;

        public bool[] RIO_MONITOR_IsEnable;
        public bool[] RIO_MONITOR_IsConnect;
        public ushort[] RIO_MONITOR_RIOType;

        //READ_SERVO_MONITOR: 伺服監控
        public ushort SERVO_MONITOR_Num;

        public short[] SERVO_MONITOR_Channel;
        public short[] SERVO_MONITOR_AxisID;
        public bool[] SERVO_MONITOR_IsConnect;
        public bool[] SERVO_MONITOR_IsServoOn;
        public short[] SERVO_MONITOR_Load;
        public short[] SERVO_MONITOR_Peak;
        public double[] SERVO_MONITOR_MechCoord;
        public bool[] SERVO_MONITOR_IsHome;
        public bool[] SERVO_MONITOR_AbsHomeSet;
        public ushort[] SERVO_MONITOR_EncoderType;

        /*請注意，為避免混淆，變數名稱定義與別處略有不同*/

        //READ_Variable_MONITOR_SysVar: 系統變數監控
        private ushort Variable_MONITOR_SysVar_Type0_Num, Variable_MONITOR_SysVar_Type1_Num;

        private ushort[] Variable_MONITOR_SysVar_Type0_Value;
        private float[] Variable_MONITOR_SysVar_Type1_Value;

        /*請注意，為避免混淆，變數名稱定義與別處略有不同*/

        //READ_Variable_MONITOR_ChannelVar: 通道變數監控
        private ushort Variable_MONITOR_ChannelVar_Type0_Num, Variable_MONITOR_ChannelVar_Type1_Num, Variable_MONITOR_ChannelVar_Type2_Num;

        private ushort[] Variable_MONITOR_ChannelVar_Type0_Value;
        private float[] Variable_MONITOR_ChannelVar_Type1_Value;
        private double[] Variable_MONITOR_ChannelVar_Type2_Value;

        /*請注意，為避免混淆，變數名稱定義與別處略有不同*/

        //READ_Variable_MONITOR_AxisVar: 軸變數監控
        private ushort Variable_MONITOR_AxisVar_AxisNum, Variable_MONITOR_AxisVar_Type0_Num, Variable_MONITOR_AxisVar_Type1_Num, Variable_MONITOR_AxisVar_Type2_Num;

        private ushort[] Variable_MONITOR_AxisVar_AxisID;
        private ushort[,] Variable_MONITOR_AxisVar_Type0_Value;
        private float[,] Variable_MONITOR_AxisVar_Type1_Value;
        private double[,] Variable_MONITOR_AxisVar_Type2_Value;

        /*請注意，為避免混淆，變數名稱定義與別處略有不同*/

        //READ_Variable_MONITOR_HMIVar 介面變數監控
        private ushort Variable_MONITOR_HMIVar_Type0_Num, Variable_MONITOR_HMIVar_Type1_Num, Variable_MONITOR_HMIVar_Type2_Num, Variable_MONITOR_HMIVar_Type3_Num;

        private ushort[] Variable_MONITOR_HMIVar_Type0_Value;
        private uint[] Variable_MONITOR_HMIVar_Type1_Value;
        private byte[,] Variable_MONITOR_HMIVar_Type2_Value;
        private string[] Variable_MONITOR_HMIVar_Type3_Value;

        /*請注意，為避免混淆，變數名稱定義與別處略有不同*/

        //READ_Variable_MONITOR_MLCVar: MLC變數監控
        private ushort Variable_MONITOR_MLCVar_VarNum;

        private ushort[] Variable_MONITOR_MLCVar_MLCVar;

        public void Update(string name, CNCInfoClass info)
        {
            switch (name)
            {
                case "CNCFlag":
                    bool var_CNCFlag_WorkingFlag, var_CNCFlag_AlarmFlag;
                    byte var_CNCFlag_RestartAct;
                    //READ_CNCFlag: 讀取CNC加工、Alarm 與RestartAct Flag

                    info.READ_CNCFlag(out var_CNCFlag_WorkingFlag, out var_CNCFlag_AlarmFlag, out var_CNCFlag_RestartAct);
                    CNCFlag_WorkingFlag = var_CNCFlag_WorkingFlag;
                    CNCFlag_AlarmFlag = var_CNCFlag_AlarmFlag;
                    CNCFlag_RestartAct = var_CNCFlag_RestartAct;
                    break;

                case "CNC_information":

                    uint var_CNC_information_CncType, var_CNC_information_ApiVersion;
                    ushort var_CNC_information_MaxChannels, var_CNC_information_Series, var_CNC_information_ScrewUnit, var_CNC_information_DisplayUnit;
                    string var_CNC_information_Nc_Ver;
                    //READ_CNC_information : CNC基本資訊
                    info.READ_CNC_information(out var_CNC_information_CncType, out var_CNC_information_MaxChannels, out var_CNC_information_Series, out var_CNC_information_Nc_Ver, out var_CNC_information_ScrewUnit, out var_CNC_information_DisplayUnit, out var_CNC_information_ApiVersion);
                    CNC_information_CncType = var_CNC_information_CncType;
                    CNC_information_MaxChannels = var_CNC_information_MaxChannels;
                    CNC_information_Series = var_CNC_information_Series;
                    CNC_information_Nc_Ver = var_CNC_information_Nc_Ver;
                    CNC_information_ScrewUnit = var_CNC_information_ScrewUnit;
                    CNC_information_DisplayUnit = var_CNC_information_DisplayUnit;
                    CNC_information_ApiVersion = var_CNC_information_ApiVersion;                
                    break;

                case "Channel_information":
                    uint var_Channel_information_Channels;
                    ushort[] var_Channel_information_AxesOfChannel, var_Channel_information_ServoAxesofChannel, var_Channel_information_MaxAxisOfChannel, var_Channel_information_DecPointOfChannel;
                    string[,] var_Channel_information_AxisNameOfChannel;
                    //READ Channel基本資訊
                    info.READ_Channel_information(out var_Channel_information_Channels, out var_Channel_information_AxesOfChannel, out var_Channel_information_ServoAxesofChannel, out var_Channel_information_MaxAxisOfChannel, out var_Channel_information_AxisNameOfChannel, out var_Channel_information_DecPointOfChannel);
                    Channel_information_Channels = var_Channel_information_Channels;
                    Channel_information_AxesOfChannel = var_Channel_information_AxesOfChannel;
                    Channel_information_ServoAxesofChannel = var_Channel_information_ServoAxesofChannel;
                    Channel_information_MaxAxisOfChannel = var_Channel_information_MaxAxisOfChannel;
                    Channel_information_AxisNameOfChannel = var_Channel_information_AxisNameOfChannel;
                    Channel_information_DecPointOfChannel = var_Channel_information_DecPointOfChannel;
                    break;

                case "axis_count":
                    ushort var_axis_count_AxisCount;
                    //Channel 伺服軸軸數
                    info.READ_axis_count(0, out var_axis_count_AxisCount);
                    axis_count_AxisCount = var_axis_count_AxisCount;
                    break;

                case "spindle_count":
                    ushort var_spindle_count_AxisCount;
                    //Channel Channel主軸軸數
                    info.READ_spindle_count(0, out var_spindle_count_AxisCount);
                    spindle_count_AxisCount = var_spindle_count_AxisCount;
                    break;

                case "status":
                    string var_status_MainProg, var_status_CurProg, var_status_ProgPath;
                    int var_status_CurSeq, var_status_MDICur, var_status_Mode, var_status_Status;
                    bool var_status_IsAlarm, var_status_IsWorking;
                    byte var_status_RestartAct;
                    //READ_status : CNC狀態
                    info.READ_status(0, out var_status_MainProg, out var_status_CurProg, out var_status_ProgPath, out var_status_CurSeq, out var_status_MDICur, out var_status_Mode, out var_status_Status, out var_status_IsAlarm, out var_status_IsWorking, out var_status_RestartAct);
                    status_MainProg = var_status_MainProg;
                    status_CurProg = var_status_CurProg;
                    status_ProgPath = var_status_ProgPath;
                    status_CurSeq = var_status_CurSeq;
                    status_MDICur = var_status_MDICur;
                    status_Mode = var_status_Mode;
                    status_Status = var_status_Status;
                    status_IsAlarm = var_status_IsAlarm;
                    status_IsWorking = var_status_IsWorking;
                    status_RestartAct = var_status_RestartAct;
                    break;

                case "NCMOTION":
                    uint var_NCMOTION_iSpSpeed, var_NCMOTION_iDwellTime;
                    double var_NCMOTION_iFeed, var_NCMOTION_iActFeed, var_NCMOTION_iSpeedJOG;
                    short var_NCMOTION_iLoad, var_NCMOTION_iMCutter;
                    uint var_NCMOTION_iActSpeed;
                    ushort var_NCMOTION_iCutterLib, var_NCMOTION_iCmdCutter, var_NCMOTION_iStandbyCutter,
                           var_NCMOTION_iStandbyCast, var_NCMOTION_iDData, var_NCMOTION_iHData,
                           var_NCMOTION_iTData, var_NCMOTION_iMData, var_NCMOTION_iSpeedF,
                           var_NCMOTION_iSpeedRPD, var_NCMOTION_iSpeedS, var_NCMOTION_iSpeedMPG;
                    char[] var_NCMOTION_GGroup;
                    //READ_NCMOTION : CNC Motion狀態資訊
                    info.READ_NCMOTION(0, (ushort)CNCUnit.metric, out var_NCMOTION_iSpSpeed, out var_NCMOTION_iFeed, out var_NCMOTION_iLoad, out var_NCMOTION_iActSpeed, out var_NCMOTION_iActFeed, out var_NCMOTION_iDwellTime, out var_NCMOTION_iMCutter, out var_NCMOTION_iCutterLib, out var_NCMOTION_iCmdCutter, out var_NCMOTION_iStandbyCutter, out var_NCMOTION_iStandbyCast, out var_NCMOTION_iDData, out var_NCMOTION_iHData, out var_NCMOTION_iTData, out var_NCMOTION_iMData, out var_NCMOTION_GGroup, out var_NCMOTION_iSpeedF, out var_NCMOTION_iSpeedRPD, out var_NCMOTION_iSpeedS, out var_NCMOTION_iSpeedJOG, out var_NCMOTION_iSpeedMPG);
                    NCMOTION_iSpSpeed = var_NCMOTION_iSpSpeed;
                    NCMOTION_iFeed = var_NCMOTION_iFeed;
                    NCMOTION_iLoad = var_NCMOTION_iLoad;
                    NCMOTION_iActSpeed = var_NCMOTION_iActSpeed;
                    NCMOTION_iActFeed = var_NCMOTION_iActFeed;
                    NCMOTION_iDwellTime = var_NCMOTION_iDwellTime;
                    NCMOTION_iMCutter = var_NCMOTION_iMCutter;
                    NCMOTION_iCutterLib = var_NCMOTION_iCutterLib;
                    NCMOTION_iCmdCutter = var_NCMOTION_iCmdCutter;
                    NCMOTION_iStandbyCutter = var_NCMOTION_iStandbyCutter;
                    NCMOTION_iStandbyCast = var_NCMOTION_iStandbyCast;
                    NCMOTION_iDData = var_NCMOTION_iDData;
                    NCMOTION_iHData = var_NCMOTION_iHData;
                    NCMOTION_iTData = var_NCMOTION_iTData;
                    NCMOTION_iMData = var_NCMOTION_iMData;
                    NCMOTION_GGroup = var_NCMOTION_GGroup;
                    NCMOTION_iSpeedF = var_NCMOTION_iSpeedF;
                    NCMOTION_iSpeedRPD = var_NCMOTION_iSpeedRPD;
                    NCMOTION_iSpeedS = var_NCMOTION_iSpeedS;
                    NCMOTION_iSpeedJOG = var_NCMOTION_iSpeedJOG;
                    NCMOTION_iSpeedMPG = var_NCMOTION_iSpeedMPG;
                    break;

                case "feed_spindle":
                    double var_feed_spindle_OvFeed, var_feed_spindle_ActFeed;
                    uint var_feed_spindle_OvSpindle, var_feed_spindle_ActSpindle;
                    //READ_feed_spindle : 讀取進給率/轉速
                    info.READ_feed_spindle(0, (ushort)CNCUnit.metric, out var_feed_spindle_OvFeed, out var_feed_spindle_OvSpindle, out var_feed_spindle_ActFeed, out var_feed_spindle_ActSpindle);
                    feed_spindle_OvFeed = var_feed_spindle_OvFeed;
                    feed_spindle_OvSpindle = var_feed_spindle_OvSpindle;
                    feed_spindle_ActFeed = var_feed_spindle_ActFeed;
                    feed_spindle_ActSpindle = var_feed_spindle_ActSpindle;
                    break;

                //預設陣列請取20
                case "gcode":
                    string[] var_gcode_Gdata;
                    //READ_gcode 讀取G Coode Data
                    info.READ_gcode(0, out var_gcode_Gdata);
                    gcode_Gdata = var_gcode_Gdata;
                    break;

                case "othercode":
                    int var_othercode_HCode;
                    int var_othercode_DCode;
                    int var_othercode_TCode;
                    int var_othercode_MCode;
                    int var_othercode_FCode;
                    int var_othercode_SCode;
                    //READ_othercode 讀取其他Code Data
                    info.READ_othercode(0, out var_othercode_HCode, out var_othercode_DCode, out var_othercode_TCode, out var_othercode_MCode, out var_othercode_FCode, out var_othercode_SCode);
                    othercode_HCode = var_othercode_HCode;
                    othercode_DCode = var_othercode_DCode;
                    othercode_TCode = var_othercode_TCode;
                    othercode_MCode = var_othercode_MCode;
                    othercode_FCode = var_othercode_FCode;
                    othercode_SCode = var_othercode_SCode;
                    break;

                case "CNC_HostName":
                    string var_CNC_HostName_strName;
                    //READ_CNC_HostName : 讀取NC主機名稱
                    info.READ_CNC_HostName(out var_CNC_HostName_strName);
                    CNC_HostName_strName = var_CNC_HostName_strName;
                    break;

                case "current_alarm":
                    uint var_current_alarm_count_AlarmCount;
                    //全部目前警報個數
                    info.READ_current_alarm_count(out var_current_alarm_count_AlarmCount);
                    current_alarm_count_AlarmCount = var_current_alarm_count_AlarmCount;
                    break;

                case "alm_current":
                    ushort var_alm_current_AlarmCount;
                    ushort[] var_alm_current_AlarmCode, var_alm_current_AlarmDataLen;
                    ushort[,] var_alm_current_AlarmData;
                    string[] var_alm_current_AlarmMsg, var_alm_current_AlarmDateTime;
                    //目前發生警報
                    info.READ_alm_current(out var_alm_current_AlarmCount, out var_alm_current_AlarmCode, out var_alm_current_AlarmDataLen, out var_alm_current_AlarmData, out var_alm_current_AlarmMsg, out var_alm_current_AlarmDateTime);
                    alm_current_AlarmCount = var_alm_current_AlarmCount;
                    alm_current_AlarmCode = var_alm_current_AlarmCode;
                    alm_current_AlarmDataLen = var_alm_current_AlarmDataLen;
                    alm_current_AlarmData = var_alm_current_AlarmData;
                    alm_current_AlarmMsg = var_alm_current_AlarmMsg;
                    alm_current_AlarmDateTime = var_alm_current_AlarmDateTime;
                    break;

                case "history_alarm":
                    uint var_history_alarm_count_AlarmCount;
                    //全部歷史警報個數
                    info.READ_history_alarm_count(out var_history_alarm_count_AlarmCount);
                    history_alarm_count_AlarmCount = var_history_alarm_count_AlarmCount;
                    break;

                case "servo_current":
                    ushort var_servo_current_AxisCount;
                    ushort[] var_servo_current_AxisNr;
                    bool[] var_servo_current_Result;
                    int[] var_servo_current_AxisValue;
                    //READ_servo_current: 讀取伺服軸負載電流
                    info.READ_servo_current(0, out var_servo_current_AxisCount, out var_servo_current_AxisNr, out var_servo_current_Result, out var_servo_current_AxisValue);
                    servo_current_AxisCount = var_servo_current_AxisCount;
                    servo_current_AxisNr = var_servo_current_AxisNr;
                    servo_current_Result = var_servo_current_Result;
                    servo_current_AxisValue = var_servo_current_AxisValue;
                    break;

                case "spindle_current":
                    ushort var_spindle_current_AxisCount;
                    ushort[] var_spindle_current_AxisNr;
                    bool[] var_spindle_current_Result;
                    int[] var_spindle_current_AxisValue;
                    //READ_spindle_current: 讀取主軸負載電流
                    info.READ_spindle_current(0, out var_spindle_current_AxisCount, out var_spindle_current_AxisNr, out var_spindle_current_Result, out var_spindle_current_AxisValue);
                    spindle_current_AxisCount = var_spindle_current_AxisCount;
                    spindle_current_AxisNr = var_spindle_current_AxisNr;
                    spindle_current_Result = var_spindle_current_Result;
                    spindle_current_AxisValue = var_spindle_current_AxisValue;
                    break;

                case "servo_load":
                    ushort var_servo_load_AxisCount;
                    ushort[] var_servo_load_AxisNr;
                    bool[] var_servo_load_Result;
                    int[] var_servo_load_AxisValue;
                    //READ_servo_load: 讀取伺服軸負載
                    info.READ_servo_load(0, out var_servo_load_AxisCount, out var_servo_load_AxisNr, out var_servo_load_Result, out var_servo_load_AxisValue);
                    servo_load_AxisCount = var_servo_load_AxisCount;
                    servo_load_AxisNr = var_servo_load_AxisNr;
                    servo_load_Result = var_servo_load_Result;
                    servo_load_AxisValue = var_servo_load_AxisValue;
                    break;

                case "spindle_load":
                    ushort var_spindle_load_AxisCount;
                    ushort[] var_spindle_load_AxisNr;
                    bool[] var_spindle_load_Result;
                    int[] var_spindle_load_AxisValue;
                    //READ_spindle_load: 讀取主軸負載
                    info.READ_spindle_load(0, out var_spindle_load_AxisCount, out var_spindle_load_AxisNr, out var_spindle_load_Result, out var_spindle_load_AxisValue);
                    spindle_load_AxisCount = var_spindle_load_AxisCount;
                    spindle_load_AxisNr = var_spindle_load_AxisNr;
                    spindle_load_Result = var_spindle_load_Result;
                    spindle_load_AxisValue = var_spindle_load_AxisValue;
                    break;

                case "servo_speed":
                    ushort var_servo_speed_AxisCount;
                    ushort[] var_servo_speed_AxisNr;
                    bool[] var_servo_speed_Result;
                    int[] var_servo_speed_AxisValue;
                    //READ_servo_speed: 讀取伺服軸轉速
                    info.READ_servo_speed(0, out var_servo_speed_AxisCount, out var_servo_speed_AxisNr, out var_servo_speed_Result, out var_servo_speed_AxisValue);
                    servo_speed_AxisCount = var_servo_speed_AxisCount;
                    servo_speed_AxisNr = var_servo_speed_AxisNr;
                    servo_speed_Result = var_servo_speed_Result;
                    servo_speed_AxisValue = var_servo_speed_AxisValue;
                    break;

                case "spindle_speed":
                    ushort var_spindle_speed_AxisCount;
                    ushort[] var_spindle_speed_AxisNr;
                    bool[] var_spindle_speed_Result;
                    int[] var_spindle_speed_AxisValue;
                    //READ_spindle_speed: 讀取主軸轉速
                    info.READ_spindle_speed(0, out var_spindle_speed_AxisCount, out var_spindle_speed_AxisNr, out var_spindle_speed_Result, out var_spindle_speed_AxisValue);
                    spindle_speed_AxisCount = var_spindle_speed_AxisCount;
                    spindle_speed_AxisNr = var_spindle_speed_AxisNr;
                    spindle_speed_Result = var_spindle_speed_Result;
                    spindle_speed_AxisValue = var_spindle_speed_AxisValue;
                    break;

                case "servo_temperature":
                    ushort var_servo_temperature_AxisCount;
                    ushort[] var_servo_temperature_AxisNr;
                    bool[] var_servo_temperature_Result;
                    int[] var_servo_temperature_AxisValue;
                    //READ_servo_temperature: 讀取伺服軸溫度
                    info.READ_servo_temperature(0, out var_servo_temperature_AxisCount, out var_servo_temperature_AxisNr, out var_servo_temperature_Result, out var_servo_temperature_AxisValue);
                    servo_temperature_AxisCount = var_servo_temperature_AxisCount;
                    servo_temperature_AxisNr = var_servo_temperature_AxisNr;
                    servo_temperature_Result = var_servo_temperature_Result;
                    servo_temperature_AxisValue = var_servo_temperature_AxisValue;
                    break;

                case "spindle_temperature":
                    ushort var_spindle_temperature_AxisCount;
                    ushort[] var_spindle_temperature_AxisNr;
                    bool[] var_spindle_temperature_Result;
                    int[] var_spindle_temperature_AxisValue;
                    //READ_spindle_temperature: 讀取主軸溫度
                    info.READ_spindle_temperature(0, out var_spindle_temperature_AxisCount, out var_spindle_temperature_AxisNr, out var_spindle_temperature_Result, out var_spindle_temperature_AxisValue);
                    spindle_temperature_AxisCount = var_spindle_temperature_AxisCount;
                    spindle_temperature_AxisNr = var_spindle_temperature_AxisNr;
                    spindle_temperature_Result = var_spindle_temperature_Result;
                    spindle_temperature_AxisValue = var_spindle_temperature_AxisValue;
                    break;

                //座標
                case "POSITION":
                    string[] var_POSITION_AxisName;
                    double[] var_POSITION_Coor_Mach;
                    double[] var_POSITION_Coor_Abs;
                    double[] var_POSITION_Coor_Rel;
                    double[] var_POSITION_Coor_Res;
                    double[] var_POSITION_Coor_Offset;
                    //READ_POSITION: CNC各軸軸座標
                    info.READ_POSITION(0, (ushort)CNCUnit.metric, out var_POSITION_AxisName, out var_POSITION_Coor_Mach, out var_POSITION_Coor_Abs, out var_POSITION_Coor_Rel, out var_POSITION_Coor_Res, out var_POSITION_Coor_Offset);
                    POSITION_AxisName = var_POSITION_AxisName;
                    POSITION_Coor_Mach = var_POSITION_Coor_Mach;
                    POSITION_Coor_Abs = var_POSITION_Coor_Abs;
                    POSITION_Coor_Rel = var_POSITION_Coor_Rel;
                    POSITION_Coor_Res = var_POSITION_Coor_Res;
                    POSITION_Coor_Offset = var_POSITION_Coor_Offset;
                    break;

                //工件座標
                case "GPOSITION_Title":
                    string[] var_GPOSITION_Title_GPOSITION_Title;
                    //READ_GPOSITION_Title:讀取工件座標標題名稱
                    info.READ_GPOSITION_Title(out var_GPOSITION_Title_GPOSITION_Title);
                    GPOSITION_Title_GPOSITION_Title = var_GPOSITION_Title_GPOSITION_Title;
                    break;

                //ini 要有此選項的index(讀取座標開始代碼)與length(讀取座標組數)
                case "GPOSITION":
                    double[,] var_GPOSITION_GPositionArray;
                    //READ_GPOSITION: 讀取工件座標資料
                    info.READ_GPOSITION(0, (ushort)CNCUnit.metric, 0, 10, out var_GPOSITION_GPositionArray);
                    GPOSITION_GPositionArray = var_GPOSITION_GPositionArray;
                    break;

                case "Offset_Coord":
                    double[] var_Offset_Coord_CoorArray;
                    //READ_Offset_Coord: 讀取偏移座標資料
                    info.READ_Offset_Coord(0, (ushort)CNCUnit.metric, out var_Offset_Coord_CoorArray);
                    Offset_Coord_CoorArray = var_Offset_Coord_CoorArray;
                    break;

                //刀具資訊
                case "cutter_title":
                    string[] var_cutter_title_CutterTitle;
                    info.READ_cutter_title(out var_cutter_title_CutterTitle);
                    cutter_title_CutterTitle = var_cutter_title_CutterTitle;
                    break;

                case "cutter_count":
                    ushort var_cutter_count_cutter_count;
                    info.READ_cutter_count(out var_cutter_count_cutter_count);
                    cutter_count_cutter_count = var_cutter_count_cutter_count;
                    break;

                //ini 要有此選項的index(變數Index)與length(讀取個數)
                case "cutter":
                    double[,] var_cutter_CutterData;
                    //READ_cutter : 讀取刀具資訊
                    info.READ_cutter((ushort)CNCUnit.metric, 0, 5, out var_cutter_CutterData);
                    cutter_CutterData = var_cutter_CutterData;
                    break;

                //刀庫資訊
                case "magazine_info":
                    ushort var_magazine_info_CutterNum, var_magazine_info_CMDCutterID, var_magazine_info_StandbyCutterID, var_magazine_info_StandbyMagaID;
                    short var_magazine_info_SPCutterID;
                    short[] var_magazine_info_CutterID;
                    //READ_magazine_info : 讀取刀庫資訊
                    info.READ_magazine_info(0, out var_magazine_info_CutterNum, out var_magazine_info_CMDCutterID, out var_magazine_info_StandbyCutterID, out var_magazine_info_StandbyMagaID, out var_magazine_info_SPCutterID, out var_magazine_info_CutterID);
                    magazine_info_CutterNum = var_magazine_info_CutterNum;
                    magazine_info_CMDCutterID = var_magazine_info_CMDCutterID;
                    magazine_info_StandbyCutterID = var_magazine_info_StandbyCutterID;
                    magazine_info_StandbyMagaID = var_magazine_info_StandbyMagaID;
                    magazine_info_SPCutterID = var_magazine_info_SPCutterID;
                    magazine_info_CutterID = var_magazine_info_CutterID;
                    break;

                //加工資訊
                case "processtime":
                    uint var_processtime_TotalWorkTime, var_processtime_SingleWorkTime;
                    //READ_processtime : 讀取加工時間
                    info.READ_processtime(out var_processtime_TotalWorkTime, out var_processtime_SingleWorkTime);
                    processtime_TotalWorkTime = var_processtime_TotalWorkTime;
                    processtime_SingleWorkTime = var_processtime_SingleWorkTime;
                    break;

                case "part_count":
                    ushort var_part_count_target_part_count, var_part_count_finish_part_count;
                    //READ_part_count : 讀取加工數
                    info.READ_part_count(out var_part_count_target_part_count, out var_part_count_finish_part_count);
                    part_count_target_part_count = var_part_count_target_part_count;
                    part_count_finish_part_count = var_part_count_finish_part_count;
                    break;

                //巨集變數
                //ini 要有此選項的VarType(Macro Variable Type)、VarIndex(變數Index)與VarLength (變數個數)
                case "MacroVariable":
                    double[] var_MacroVariable_RetArray;
                    //READ_MacroVariable: 讀取MMacro Variable
                    info.READ_MacroVariable(0, 0, 0, 10, out var_MacroVariable_RetArray);
                    MacroVariable_RetArray = var_MacroVariable_RetArray;
                    break;

                //ini 要有此選項的MacroID(Macro Variable ID)
                case "MacroVariablebyID":
                    double var_MacroVariablebyID_RetValue;
                    //READ_MacroVariable: 讀取Macro Variable
                    info.READ_MacroVariablebyID(0, 0, out var_MacroVariablebyID_RetValue);
                    MacroVariablebyID_RetValue = var_MacroVariablebyID_RetValue;
                    break;

                //PLC變數
                //ini 要有此選項的DevType(PLC Variable Type)、DevStart(變數Index)、DevNum(PLC Variable個數)
                case "PLC_ADDR":
                    byte[] var_PLC_ADDR_RetArray;
                    //READ_PLC_ADDR: 讀取PLC  Variable
                    info.READ_PLC_ADDR(0, 0, 10, out var_PLC_ADDR_RetArray);
                    PLC_ADDR_RetArray = var_PLC_ADDR_RetArray;
                    break;

                //參數資訊
                //ini 要有此選項的ParaID (參數號碼)、SubItem(子參數 (從0開始，不指定值為-1))、AXNr(軸參數)
                case "CNCParameter_Single":
                    ushort var_CNCParameter_Single_GroupID;
                    short var_CNCParameter_Single_SubGroupID;
                    short var_CNCParameter_Single_ParaChannel;
                    short var_CNCParameter_Single_ParaAxis;
                    short var_CNCParameter_Single_DataType;
                    ushort var_CNCParameter_Single_DataSize;
                    byte[] var_CNCParameter_Single_Data;
                    //READ_CNCParameter_Single: 讀取單筆CNC參數
                    info.READ_CNCParameter_Single(0, 0, 0, 0, out var_CNCParameter_Single_GroupID, out var_CNCParameter_Single_SubGroupID, out var_CNCParameter_Single_ParaChannel, out var_CNCParameter_Single_ParaAxis, out var_CNCParameter_Single_DataType, out var_CNCParameter_Single_DataSize, out var_CNCParameter_Single_Data);
                    CNCParameter_Single_GroupID = var_CNCParameter_Single_GroupID;
                    CNCParameter_Single_SubGroupID = var_CNCParameter_Single_SubGroupID;
                    CNCParameter_Single_ParaChannel = var_CNCParameter_Single_ParaChannel;
                    CNCParameter_Single_ParaAxis = var_CNCParameter_Single_ParaAxis;
                    CNCParameter_Single_DataType = var_CNCParameter_Single_DataType;
                    CNCParameter_Single_DataSize = var_CNCParameter_Single_DataSize;
                    CNCParameter_Single_Data = var_CNCParameter_Single_Data;
                    break;

                //系統資訊
                case "System_Time":
                    string var_System_Time_strSystemTime;
                    //READ_System_Time : 讀取系統日期時間
                   int i= info.READ_System_Time(out var_System_Time_strSystemTime);
                    System_Time_strSystemTime = var_System_Time_strSystemTime;
                        break;

                //陣列長度參考此函數有關個數的值
                case "system_variable":
                    ushort var_system_variable_SysVarNum, var_system_variable_AxisVarNum;
                    ushort[] var_system_variable_SysVarChannel, var_system_variable_SysVarID, var_system_variable_SysVarType,
                             var_system_variable_AxisVarChannel, var_system_variable_AxisNum, var_system_variable_AxisVarID,
                             var_system_variable_AxisVarType;
                    byte[,] var_system_variable_SysVarValue;
                    ushort[,] var_system_variable_AxisID;
                    byte[,,] var_system_variable_AxisVarValue;
                    //READ_system_variable:讀取系統變數
                    info.READ_system_variable(out var_system_variable_SysVarNum, out var_system_variable_AxisVarNum, out var_system_variable_SysVarChannel, out var_system_variable_SysVarID, out var_system_variable_SysVarType, out var_system_variable_SysVarValue, out var_system_variable_AxisVarChannel, out var_system_variable_AxisNum, out var_system_variable_AxisVarID, out var_system_variable_AxisVarType, out var_system_variable_AxisID, out var_system_variable_AxisVarValue);
                    system_variable_SysVarNum = var_system_variable_SysVarNum;
                    system_variable_AxisVarNum = var_system_variable_AxisVarNum;
                    system_variable_SysVarChannel = var_system_variable_SysVarChannel;
                    system_variable_SysVarID = var_system_variable_SysVarID;
                    system_variable_SysVarType = var_system_variable_SysVarType;
                    system_variable_SysVarValue = var_system_variable_SysVarValue;
                    system_variable_AxisVarChannel = var_system_variable_AxisVarChannel;
                    system_variable_AxisNum = var_system_variable_AxisNum;
                    system_variable_AxisVarID = var_system_variable_AxisVarID;
                    system_variable_AxisVarType = var_system_variable_AxisVarType;
                    system_variable_AxisID = var_system_variable_AxisID;
                    system_variable_AxisVarValue = var_system_variable_AxisVarValue;
                    break;

                //陣列長度參考此函數有關個數的值
                case "user_variable":
                    ushort var_user_variable_VarNum;
                    ushort[] var_user_variable_VarReg, var_user_variable_VarValue;
                    //READ_user_variable讀取用戶變數
                    info.READ_user_variable(out var_user_variable_VarNum, out var_user_variable_VarReg, out var_user_variable_VarValue);
                    user_variable_VarNum = var_user_variable_VarNum;
                    user_variable_VarReg = var_user_variable_VarReg;
                    user_variable_VarValue = var_user_variable_VarValue;
                    break;

                //陣列長度參考此函數有關個數的值
                case "equip_variable":
                    ushort var_equip_variable_VarNum;
                    ushort[] var_equip_variable_VarReg, var_equip_variable_VarValue;
                    //READ_equip_variable : 讀取設備變數
                    info.READ_equip_variable(out var_equip_variable_VarNum, out var_equip_variable_VarReg, out var_equip_variable_VarValue);
                    equip_variable_VarNum = var_equip_variable_VarNum;
                    equip_variable_VarReg = var_equip_variable_VarReg;
                    equip_variable_VarValue = var_equip_variable_VarValue;
                    break;

                //陣列長度參考此函數有關個數的值
                case "system_status":
                    ushort var_system_status_ParaCount;
                    ushort[] var_system_status_ParaID, var_system_status_DataType, var_system_status_DataSize;
                    string[] var_system_status_ParaName;
                    byte[,] var_system_status_Data;
                    //READ_system_status : 讀取系統狀態
                    info.READ_system_status(out var_system_status_ParaCount, out var_system_status_ParaID, out var_system_status_ParaName, out var_system_status_DataType, out var_system_status_DataSize, out var_system_status_Data);
                    system_status_ParaCount = var_system_status_ParaCount;
                    system_status_ParaID = var_system_status_ParaID;
                    system_status_ParaName = var_system_status_ParaName;
                    system_status_DataType = var_system_status_DataType;
                    system_status_DataSize = var_system_status_DataSize;
                    system_status_Data = var_system_status_Data;
                    break;

                //陣列長度參考此函數有關個數的值
                case "FW_version":
                    ushort var_FW_version_ParaCount;
                    ushort[] var_FW_version_ParaID;
                    string[] var_FW_version_ParaName;
                    ushort[] var_FW_version_DataType;
                    ushort[] var_FW_version_DataSize;
                    byte[,] var_FW_version_Data;
                    //READ_FW_version: 讀取韌韌體序號
                    info.READ_FW_version(out var_FW_version_ParaCount, out var_FW_version_ParaID, out var_FW_version_ParaName, out var_FW_version_DataType, out var_FW_version_DataSize, out var_FW_version_Data);
                    FW_version_ParaCount = var_FW_version_ParaCount;
                    FW_version_ParaID = var_FW_version_ParaID;
                    FW_version_ParaName = var_FW_version_ParaName;
                    FW_version_DataType = var_FW_version_DataType;
                    FW_version_DataSize = var_FW_version_DataSize;
                    FW_version_Data = var_FW_version_Data;
                    break;

                //陣列長度參考此函數有關個數的值
                case "HW_version":
                    ushort var_HW_version_ParaCount;
                    ushort[] var_HW_version_ParaID;
                    string[] var_HW_version_ParaName;
                    ushort[] var_HW_version_DataType;
                    ushort[] var_HW_version_DataSize;
                    byte[,] var_HW_version_Data;
                    //READ_HW_version: 讀取硬體序號
                    info.READ_HW_version(out var_HW_version_ParaCount, out var_HW_version_ParaID, out var_HW_version_ParaName, out var_HW_version_DataType, out var_HW_version_DataSize, out var_HW_version_Data);
                    HW_version_ParaCount = var_HW_version_ParaCount;
                    HW_version_ParaID = var_HW_version_ParaID;
                    HW_version_ParaName = var_HW_version_ParaName;
                    HW_version_DataType = var_HW_version_DataType;
                    HW_version_DataSize = var_HW_version_DataSize;
                    HW_version_Data = var_HW_version_Data;
                    break;

                //陣列長度參考此函數有關個數的值
                case "equip_infomation":
                    ushort var_equip_infomation_InfoCount;
                    string[] var_equip_infomation_EquipInfo;
                    // READ_equip_infomation : 讀取設備資訊
                    info.READ_equip_infomation(out var_equip_infomation_InfoCount, out var_equip_infomation_EquipInfo);
                    equip_infomation_InfoCount = var_equip_infomation_InfoCount;
                    equip_infomation_EquipInfo = var_equip_infomation_EquipInfo;
                    break;

                //通道設定
                //陣列長度參考此函數有關個數的值
                case "channel_setting":
                    ushort var_channel_setting_AxisNum;
                    ushort[] var_channel_setting_AxisID;
                    bool[] var_channel_setting_IsEnable;
                    short[] var_channel_setting_AxisTypeID;
                    short[] var_channel_setting_PortID;
                    string[] var_channel_setting_AxisName;
                    //READ_channel_setting:讀取通道設定
                    info.READ_channel_setting(0, out var_channel_setting_AxisNum, out var_channel_setting_AxisID, out var_channel_setting_IsEnable, out var_channel_setting_AxisTypeID, out var_channel_setting_PortID, out var_channel_setting_AxisName);
                    channel_setting_AxisNum = var_channel_setting_AxisNum;
                    channel_setting_AxisID = var_channel_setting_AxisID;
                    channel_setting_IsEnable = var_channel_setting_IsEnable;
                    channel_setting_AxisTypeID = var_channel_setting_AxisTypeID;
                    channel_setting_PortID = var_channel_setting_PortID;
                    channel_setting_AxisName = var_channel_setting_AxisName;
                    break;

                //陣列長度參考此函數有關個數的值
                case "port_info":
                    ushort var_port_info_PortNum;
                    ushort[] var_port_info_PortID;
                    bool[] var_port_info_IsEnable;
                    short[] var_port_info_ChannelID;
                    short[] var_port_info_AxisID;
                    short[] var_port_info_AxisTypeID;
                    //READ_port_info: 讀取埠設定
                    info.READ_port_info(out var_port_info_PortNum, out var_port_info_PortID, out var_port_info_IsEnable, out var_port_info_ChannelID, out var_port_info_AxisID, out var_port_info_AxisTypeID);
                    port_info_PortNum = var_port_info_PortNum;
                    port_info_PortID = var_port_info_PortID;
                    port_info_IsEnable = var_port_info_IsEnable;
                    port_info_ChannelID = var_port_info_ChannelID;
                    port_info_AxisID = var_port_info_AxisID;
                    port_info_AxisTypeID = var_port_info_AxisTypeID;
                    break;

                //RIO設定
                //陣列長度參考此函數有關個數的值
                case "RIO_Status":
                    ushort var_RIO_Status_IO_Num;
                    bool[] var_RIO_Status_RIO_Status;
                    //READ_RIO_Status:讀取RIO埠狀態
                    info.READ_RIO_Status(out var_RIO_Status_IO_Num, out var_RIO_Status_RIO_Status);
                    RIO_Status_IO_Num = var_RIO_Status_IO_Num;
                    RIO_Status_RIO_Status = var_RIO_Status_RIO_Status;
                    break;

                //陣列長度參考此函數有關個數的值
                case "RIO_setting":
                    ushort var_RIO_setting_all_IO_Num;
                    bool[] var_RIO_setting_all_IsEnable;
                    short[] var_RIO_setting_all_RIOType;
                    uint[] var_RIO_setting_all_Polarity;
                    bool[] var_RIO_setting_IsDisc;
                    //READ_RIO_setting_all: 讀取全部RIO設定
                    info.READ_RIO_setting_all(out var_RIO_setting_all_IO_Num, out var_RIO_setting_all_IsEnable, out var_RIO_setting_all_RIOType, out var_RIO_setting_all_Polarity, out var_RIO_setting_IsDisc);
                    RIO_setting_all_IO_Num = var_RIO_setting_all_IO_Num;
                    RIO_setting_all_IsEnable = var_RIO_setting_all_IsEnable;
                    RIO_setting_all_RIOType = var_RIO_setting_all_RIOType;
                    RIO_setting_all_Polarity = var_RIO_setting_all_Polarity;
                    RIO_setting_all_IsDisc = var_RIO_setting_IsDisc;
                    break;

                //陣列長度參考此函數有關個數的值
                case "RIO_home":
                    ushort var_RIO_home_limit_Port_Num;
                    bool[] var_RIO_home_limit_Homelimit;
                    //READ_RIO_home_limit: 讀取RIO原點極限設定
                    info.READ_RIO_home_limit(0, out var_RIO_home_limit_Port_Num, out var_RIO_home_limit_Homelimit);
                    RIO_home_limit_Port_Num = var_RIO_home_limit_Port_Num;
                    RIO_home_limit_Homelimit = var_RIO_home_limit_Homelimit;
                    break;

                case "RIO_filter":
                    ushort var_RIO_filter_FilterLevel;
                    //READ_RIO_filter: 讀取RIO濾波等級
                    info.READ_RIO_filter(out var_RIO_filter_FilterLevel);
                    RIO_filter_FilterLevel = var_RIO_filter_FilterLevel;
                    break;

                //ini 要有此選項的disktype (查詢磁碟代號(參考【數值說明】-【DiskType】)  )
                case "disk_quota":
                    int var_disk_quota_diskquota;
                    //READ_disk_quota: 讀取磁碟剩餘空間
                    info.READ_disk_quota('D', out var_disk_quota_diskquota);
                    disk_quota_diskquota = var_disk_quota_diskquota;
                    break;

                case "inter_disk":
                    uint var_inter_disk_quota_diskquota;
                    //手冊此function打錯
                    //READ_inter_disk_quota: 讀取Flash剩餘可用空間
                    info.READ_inter_disk_quota(out var_inter_disk_quota_diskquota);
                    inter_disk_quota_diskquota = var_inter_disk_quota_diskquota;
                    break;

                case "nc_pointer":
                    int var_nc_pointer_LineNum, var_nc_pointer_MDILineNum;
                    //READ_nc_pointer : 讀取NC 執行行號
                    info.READ_nc_pointer(out var_nc_pointer_LineNum, out var_nc_pointer_MDILineNum);
                    nc_pointer_LineNum = var_nc_pointer_LineNum;
                    nc_pointer_MDILineNum = var_nc_pointer_MDILineNum;
                    break;

                //ini 要有此選項的CodeCount(讀取行數，範圍1~50)
                case "preview_code":
                    uint[] var_preview_code_LineNo;
                    string[] var_preview_code_strCode;
                    //先讀5行
                    //READ_preview_code: 讀取NC 預讀程式內容
                    info.READ_preview_code(50, out var_preview_code_LineNo, out var_preview_code_strCode);
                    preview_code_LineNo = var_preview_code_LineNo;
                    preview_code_strCode = var_preview_code_strCode;
                    break;

                //ini 要有此選項的CodeCount(讀取行數，範圍1~50)
                case "current_code":
                    uint[] var_current_code_LineNo;
                    string[] var_current_code_strCode;
                    //READ_current_code : 讀取NC 當前程式內容
                    info.READ_current_code(50, out var_current_code_LineNo, out var_current_code_strCode);
                    current_code_LineNo = var_current_code_LineNo;
                    current_code_strCode = var_current_code_strCode;
                    break;

                //斷點搜尋
                case "CodeSearch_LineNo":
                    int var_CodeSearch_LineNo_LineNo;
                    //READ_CodeSearch_LineNo: 讀取斷點搜尋行號
                    info.READ_CodeSearch_LineNo(0, out var_CodeSearch_LineNo_LineNo);
                    CodeSearch_LineNo_LineNo = var_CodeSearch_LineNo_LineNo;
                    break;

                //陣列長度參考此函數有關個數的值
                case "CodeSearch_POSITION":
                    ushort var_CodeSearch_POSITION_AxisNum;
                    ushort[] var_CodeSearch_POSITION_AxisID;
                    double[] var_CodeSearch_POSITION_Coor_Mach;
                    double[] var_CodeSearch_POSITION_Coor_Abs;
                    double[] var_CodeSearch_POSITION_Coor_Rel;
                    double[] var_CodeSearch_POSITION_Coor_Res;
                    double[] var_CodeSearch_POSITION_Coor_Offset;
                    //READ_CodeSearch_POSITION:讀取斷點搜尋座標
                    info.READ_CodeSearch_POSITION(0, (ushort)CNCUnit.metric, out var_CodeSearch_POSITION_AxisNum, out var_CodeSearch_POSITION_AxisID, out var_CodeSearch_POSITION_Coor_Mach, out var_CodeSearch_POSITION_Coor_Abs, out var_CodeSearch_POSITION_Coor_Rel, out var_CodeSearch_POSITION_Coor_Res, out var_CodeSearch_POSITION_Coor_Offset);
                    CodeSearch_POSITION_AxisNum = var_CodeSearch_POSITION_AxisNum;
                    CodeSearch_POSITION_AxisID = var_CodeSearch_POSITION_AxisID;
                    CodeSearch_POSITION_Coor_Mach = var_CodeSearch_POSITION_Coor_Mach;
                    CodeSearch_POSITION_Coor_Abs = var_CodeSearch_POSITION_Coor_Abs;
                    CodeSearch_POSITION_Coor_Rel = var_CodeSearch_POSITION_Coor_Rel;
                    CodeSearch_POSITION_Coor_Res = var_CodeSearch_POSITION_Coor_Res;
                    CodeSearch_POSITION_Coor_Offset = var_CodeSearch_POSITION_Coor_Offset;
                    break;

                //手動輸入
                // strCode (MDI內容)最多14行，一行最長256
                case "MDI_code":
                    uint var_MDI_code_LineNo;
                    string[] var_MDI_code_strCode;
                    //READ_MDI_code:MDI執行內容讀取
                    info.READ_MDI_code(out var_MDI_code_LineNo, out var_MDI_code_strCode);
                    MDI_code_LineNo = var_MDI_code_LineNo;
                    MDI_code_strCode = var_MDI_code_strCode;
                    break;

                //系統監控
                //陣列長度參考此函數有關個數的值
                case "RIO_MONITOR":
                    ushort var_RIO_MONITOR_RIONum;
                    bool[] var_RIO_MONITOR_IsEnable, var_RIO_MONITOR_IsConnect;
                    ushort[] var_RIO_MONITOR_RIOType;
                    //READ_RIO_MONITOR : I/O 監控
                    info.READ_RIO_MONITOR(out var_RIO_MONITOR_RIONum, out var_RIO_MONITOR_IsEnable, out var_RIO_MONITOR_IsConnect, out var_RIO_MONITOR_RIOType);
                    RIO_MONITOR_RIONum = var_RIO_MONITOR_RIONum;
                    RIO_MONITOR_IsEnable = var_RIO_MONITOR_IsEnable;
                    RIO_MONITOR_IsConnect = var_RIO_MONITOR_IsConnect;
                    RIO_MONITOR_RIOType = var_RIO_MONITOR_RIOType;
                    break;

                //陣列長度參考此函數有關個數的值
                case "SERVO_MONITOR":
                    ushort var_SERVO_MONITOR_Num;
                    short[] var_SERVO_MONITOR_Channel, var_SERVO_MONITOR_AxisID, var_SERVO_MONITOR_Load,
                            var_SERVO_MONITOR_Peak;
                    bool[] var_SERVO_MONITOR_IsConnect, var_SERVO_MONITOR_IsServoOn, var_SERVO_MONITOR_IsHome, var_SERVO_MONITOR_AbsHomeSet;
                    double[] var_SERVO_MONITOR_MechCoord;
                    ushort[] var_SERVO_MONITOR_EncoderType;
                    //READ_SERVO_MONITOR: 伺服監控
                    info.READ_SERVO_MONITOR(out var_SERVO_MONITOR_Num, out var_SERVO_MONITOR_Channel, out var_SERVO_MONITOR_AxisID, out var_SERVO_MONITOR_IsConnect, out var_SERVO_MONITOR_IsServoOn, out var_SERVO_MONITOR_Load, out var_SERVO_MONITOR_Peak, out var_SERVO_MONITOR_MechCoord, out var_SERVO_MONITOR_IsHome, out var_SERVO_MONITOR_AbsHomeSet, out var_SERVO_MONITOR_EncoderType);
                    SERVO_MONITOR_Num = var_SERVO_MONITOR_Num;
                    SERVO_MONITOR_Channel = var_SERVO_MONITOR_Channel;
                    SERVO_MONITOR_AxisID = var_SERVO_MONITOR_AxisID;
                    SERVO_MONITOR_IsConnect = var_SERVO_MONITOR_IsConnect;
                    SERVO_MONITOR_IsServoOn = var_SERVO_MONITOR_IsServoOn;
                    SERVO_MONITOR_Load = var_SERVO_MONITOR_Load;
                    SERVO_MONITOR_Peak = var_SERVO_MONITOR_Peak;
                    SERVO_MONITOR_MechCoord = var_SERVO_MONITOR_MechCoord;
                    SERVO_MONITOR_IsHome = var_SERVO_MONITOR_IsHome;
                    SERVO_MONITOR_AbsHomeSet = var_SERVO_MONITOR_AbsHomeSet;
                    SERVO_MONITOR_EncoderType = var_SERVO_MONITOR_EncoderType;
                    break;

                //陣列長度參考此函數有關個數的值
                case "MONITOR_SysVar":
                    ushort var_Variable_MONITOR_SysVar_Type0_Num;
                    ushort var_Variable_MONITOR_SysVar_Type1_Num;
                    ushort[] var_Variable_MONITOR_SysVar_Type0_Value;
                    float[] var_Variable_MONITOR_SysVar_Type1_Value;
                    //READ_Variable_MONITOR_SysVar: 系統變數監控
                    info.READ_Variable_MONITOR_SysVar(out var_Variable_MONITOR_SysVar_Type0_Num, out var_Variable_MONITOR_SysVar_Type1_Num, out var_Variable_MONITOR_SysVar_Type0_Value, out var_Variable_MONITOR_SysVar_Type1_Value);
                    Variable_MONITOR_SysVar_Type0_Num = var_Variable_MONITOR_SysVar_Type0_Num;
                    Variable_MONITOR_SysVar_Type1_Num = var_Variable_MONITOR_SysVar_Type1_Num;
                    Variable_MONITOR_SysVar_Type0_Value = var_Variable_MONITOR_SysVar_Type0_Value;
                    Variable_MONITOR_SysVar_Type1_Value = var_Variable_MONITOR_SysVar_Type1_Value;
                    break;

                //陣列長度參考此函數有關個數的值
                case "MONITOR_ChannelVar":
                    ushort var_Variable_MONITOR_ChannelVar_Type0_Num;
                    ushort var_Variable_MONITOR_ChannelVar_Type1_Num;
                    ushort var_Variable_MONITOR_ChannelVar_Type2_Num;
                    ushort[] var_Variable_MONITOR_ChannelVar_Type0_Value;
                    float[] var_Variable_MONITOR_ChannelVar_Type1_Value;
                    double[] var_Variable_MONITOR_ChannelVar_Type2_Value;
                    //READ_Variable_MONITOR_ChannelVar: 通道變數監控
                    info.READ_Variable_MONITOR_ChannelVar(0, out var_Variable_MONITOR_ChannelVar_Type0_Num, out var_Variable_MONITOR_ChannelVar_Type1_Num, out var_Variable_MONITOR_ChannelVar_Type2_Num, out var_Variable_MONITOR_ChannelVar_Type0_Value, out var_Variable_MONITOR_ChannelVar_Type1_Value, out var_Variable_MONITOR_ChannelVar_Type2_Value);
                    Variable_MONITOR_ChannelVar_Type0_Num = var_Variable_MONITOR_ChannelVar_Type0_Num;
                    Variable_MONITOR_ChannelVar_Type1_Num = var_Variable_MONITOR_ChannelVar_Type1_Num;
                    Variable_MONITOR_ChannelVar_Type2_Num = var_Variable_MONITOR_ChannelVar_Type2_Num;
                    Variable_MONITOR_ChannelVar_Type0_Value = var_Variable_MONITOR_ChannelVar_Type0_Value;
                    Variable_MONITOR_ChannelVar_Type1_Value = var_Variable_MONITOR_ChannelVar_Type1_Value;
                    Variable_MONITOR_ChannelVar_Type2_Value = var_Variable_MONITOR_ChannelVar_Type2_Value;
                    break;

                //陣列長度參考此函數有關個數的值
                case "MONITOR_AxisVar":
                    ushort var_Variable_MONITOR_AxisVar_AxisNum;
                    ushort[] var_Variable_MONITOR_AxisVar_AxisID;
                    ushort var_Variable_MONITOR_AxisVar_Type0_Num;
                    ushort var_Variable_MONITOR_AxisVar_Type1_Num;
                    ushort var_Variable_MONITOR_AxisVar_Type2_Num;
                    ushort[,] var_Variable_MONITOR_AxisVar_Type0_Value;
                    float[,] var_Variable_MONITOR_AxisVar_Type1_Value;
                    double[,] var_Variable_MONITOR_AxisVar_Type2_Value;
                    //READ_Variable_MONITOR_AxisVar:軸變數監控
                    info.READ_Variable_MONITOR_AxisVar(0, out var_Variable_MONITOR_AxisVar_AxisNum, out var_Variable_MONITOR_AxisVar_AxisID, out var_Variable_MONITOR_AxisVar_Type0_Num, out var_Variable_MONITOR_AxisVar_Type1_Num, out var_Variable_MONITOR_AxisVar_Type2_Num, out var_Variable_MONITOR_AxisVar_Type0_Value, out var_Variable_MONITOR_AxisVar_Type1_Value, out var_Variable_MONITOR_AxisVar_Type2_Value);
                    Variable_MONITOR_AxisVar_AxisNum = var_Variable_MONITOR_AxisVar_AxisNum;
                    Variable_MONITOR_AxisVar_AxisID = var_Variable_MONITOR_AxisVar_AxisID;
                    Variable_MONITOR_AxisVar_Type0_Num = var_Variable_MONITOR_AxisVar_Type0_Num;
                    Variable_MONITOR_AxisVar_Type1_Num = var_Variable_MONITOR_AxisVar_Type1_Num;
                    Variable_MONITOR_AxisVar_Type2_Num = var_Variable_MONITOR_AxisVar_Type2_Num;
                    Variable_MONITOR_AxisVar_Type0_Value = var_Variable_MONITOR_AxisVar_Type0_Value;
                    Variable_MONITOR_AxisVar_Type1_Value = var_Variable_MONITOR_AxisVar_Type1_Value;
                    Variable_MONITOR_AxisVar_Type2_Value = var_Variable_MONITOR_AxisVar_Type2_Value;
                    break;

                //陣列長度參考此函數有關個數的值
                case "MONITOR_HMIVar":
                    ushort var_Variable_MONITOR_HMIVar_Type0_Num;
                    ushort var_Variable_MONITOR_HMIVar_Type1_Num;
                    ushort var_Variable_MONITOR_HMIVar_Type2_Num;
                    ushort var_Variable_MONITOR_HMIVar_Type3_Num;
                    ushort[] var_Variable_MONITOR_HMIVar_Type0_Value;
                    uint[] var_Variable_MONITOR_HMIVar_Type1_Value;
                    byte[,] var_Variable_MONITOR_HMIVar_Type2_Value;
                    string[] var_Variable_MONITOR_HMIVar_Type3_Value;
                    //READ_Variable_MONITOR_HMIVar: 介面變數監控
                    info.READ_Variable_MONITOR_HMIVar(out var_Variable_MONITOR_HMIVar_Type0_Num, out var_Variable_MONITOR_HMIVar_Type1_Num, out var_Variable_MONITOR_HMIVar_Type2_Num, out var_Variable_MONITOR_HMIVar_Type3_Num, out var_Variable_MONITOR_HMIVar_Type0_Value, out var_Variable_MONITOR_HMIVar_Type1_Value, out var_Variable_MONITOR_HMIVar_Type2_Value, out var_Variable_MONITOR_HMIVar_Type3_Value);
                    Variable_MONITOR_HMIVar_Type0_Num = var_Variable_MONITOR_HMIVar_Type0_Num;
                    Variable_MONITOR_HMIVar_Type1_Num = var_Variable_MONITOR_HMIVar_Type1_Num;
                    Variable_MONITOR_HMIVar_Type2_Num = var_Variable_MONITOR_HMIVar_Type2_Num;
                    Variable_MONITOR_HMIVar_Type3_Num = var_Variable_MONITOR_HMIVar_Type3_Num;
                    Variable_MONITOR_HMIVar_Type0_Value = var_Variable_MONITOR_HMIVar_Type0_Value;
                    Variable_MONITOR_HMIVar_Type1_Value = var_Variable_MONITOR_HMIVar_Type1_Value;
                    Variable_MONITOR_HMIVar_Type2_Value = var_Variable_MONITOR_HMIVar_Type2_Value;
                    Variable_MONITOR_HMIVar_Type3_Value = var_Variable_MONITOR_HMIVar_Type3_Value;
                    break;

                //陣列長度參考此函數有關個數的值
                case "MONITOR_MLCVar":
                    ushort var_Variable_MONITOR_MLCVar_VarNum;
                    ushort[] var_Variable_MONITOR_MLCVar_MLCVar;
                    //READ_Variable_MONITOR_MLCVar:  MLC變數監控
                    info.READ_Variable_MONITOR_MLCVar(out var_Variable_MONITOR_MLCVar_VarNum, out var_Variable_MONITOR_MLCVar_MLCVar);
                    Variable_MONITOR_MLCVar_VarNum = var_Variable_MONITOR_MLCVar_VarNum;
                    Variable_MONITOR_MLCVar_MLCVar = var_Variable_MONITOR_MLCVar_MLCVar;
                    break;
            }
        }

        public object ReturnValue(string name)
        {
            switch (name)
            {
                case "CNCFlag_WorkingFlag":
                    return CNCFlag_WorkingFlag;

                case "CNCFlag_AlarmFlag":
                    return CNCFlag_AlarmFlag;

                case "CNCFlag_RestartAct":
                    return CNCFlag_RestartAct;

                case "CNC_information_CncType":
                    return CNC_information_CncType;

                case "CNC_information_MaxChannels":
                    return CNC_information_MaxChannels;

                case "CNC_information_Series":
                    return CNC_information_Series;

                case "CNC_information_Nc_Ver":
                    return CNC_information_Nc_Ver;

                case "CNC_information_ScrewUnit":
                    return CNC_information_ScrewUnit;

                case "CNC_information_DisplayUnit":
                    return CNC_information_DisplayUnit;

                case "CNC_information_ApiVersion":
                    return CNC_information_ApiVersion;

                case "Channel_information_Channels":
                    return Channel_information_Channels;
                //手冊編錯
                case "axis_count_AxisCount":
                    return axis_count_AxisCount;
                //手冊編錯
                case "spindle_count_AxisCount":
                    return spindle_count_AxisCount;

                case "status_MainProg":
                    return status_MainProg;

                case "status_CurProg":
                    return status_CurProg;

                case "status_ProgPath":
                    return status_ProgPath;

                case "status_CurSeq":
                    return status_CurSeq;

                case "status_MDICur":
                    return status_MDICur;

                case "status_Mode":
                    return status_Mode;

                case "status_Status":
                    return status_Status;

                case "status_IsAlarm":
                    return status_IsAlarm;

                case "status_IsWorking":
                    return status_IsWorking;

                case "status_RestartAct":
                    return status_RestartAct;

                case "NCMOTION_Unit":
                    return NCMOTION_Unit;

                case "NCMOTION_iSpSpeed":
                    return NCMOTION_iSpSpeed;

                case "NCMOTION_iFeed":
                    return NCMOTION_iFeed;

                case "NCMOTION_iLoad":
                    return NCMOTION_iLoad;

                case "NCMOTION_iActSpeed":
                    return NCMOTION_iActSpeed;

                case "NCMOTION_iActFeed":
                    return NCMOTION_iActFeed;

                case "NCMOTION_iDwellTime":
                    return NCMOTION_iDwellTime;

                case "NCMOTION_iMCutter":
                    return NCMOTION_iMCutter;

                case "NCMOTION_iCutterLib":
                    return NCMOTION_iCutterLib;

                case "NCMOTION_iCmdCutter":
                    return NCMOTION_iCmdCutter;

                case "NCMOTION_iStandbyCutter":
                    return NCMOTION_iStandbyCutter;

                case "NCMOTION_iStandbyCast":
                    return NCMOTION_iStandbyCast;

                case "NCMOTION_iDData":
                    return NCMOTION_iDData;

                case "NCMOTION_iHData":
                    return NCMOTION_iHData;

                case "NCMOTION_iTData":
                    return NCMOTION_iTData;

                case "NCMOTION_iMData":
                    return NCMOTION_iMData;

                case "NCMOTION_GGroup":
                    return NCMOTION_GGroup;

                case "NCMOTION_iSpeedF":
                    return NCMOTION_iSpeedF;

                case "NCMOTION_iSpeedRPD":
                    return NCMOTION_iSpeedRPD;

                case "NCMOTION_iSpeedS":
                    return NCMOTION_iSpeedS;

                case "NCMOTION_iSpeedJOG":
                    return NCMOTION_iSpeedJOG;

                case "NCMOTION_iSpeedMPG":
                    return NCMOTION_iSpeedMPG;

                case "feed_spindle_OvFeed":
                    return feed_spindle_OvFeed;

                case "feed_spindle_OvSpindle":
                    return feed_spindle_OvSpindle;

                case "feed_spindle_ActFeed":
                    return feed_spindle_ActFeed;

                case "feed_spindle_ActSpindle":
                    return feed_spindle_ActSpindle;

                case "othercode_HCode":
                    return othercode_HCode;

                case "othercode_DCode":
                    return othercode_DCode;

                case "othercode_TCode":
                    return othercode_TCode;

                case "othercode_MCode":
                    return othercode_MCode;

                case "othercode_FCode":
                    return othercode_FCode;

                case "othercode_SCode":
                    return othercode_SCode;

                case "CNC_HostName_strName":
                    return CNC_HostName_strName;

                case "current_alarm_count_AlarmCount":
                    return current_alarm_count_AlarmCount;

                case "alm_current_AlarmCount":
                    return alm_current_AlarmCount;

                case "history_alarm_count_AlarmCount":
                    return history_alarm_count_AlarmCount;
                //手冊編誤
                case "servo_current_AxisCount":
                    return servo_current_AxisCount;
                //手冊編誤
                case "spindle_current_AxisCount":
                    return spindle_current_AxisCount;
                //手冊編誤
                case "servo_load_AxisCount":
                    return servo_load_AxisCount;

                //手冊編誤
                case "spindle_load_AxisCount":
                    return spindle_load_AxisCount;

                //手冊編誤
                case "servo_speed_AxisCount":
                    return servo_speed_AxisCount;

                //手冊編誤
                case "spindle_speed_AxisCount":
                    return spindle_speed_AxisCount;

                //手冊編誤
                case "servo_temperature_AxisCount":
                    return servo_temperature_AxisCount;

                //手冊編誤
                case "spindle_temperature_AxisCount":
                    return spindle_temperature_AxisCount;

                case "cutter_count_cutter_count":
                    return cutter_count_cutter_count;

                case "magazine_info_CutterNum":
                    return magazine_info_CutterNum;

                case "magazine_info_CMDCutterID":
                    return magazine_info_CMDCutterID;

                case "magazine_info_StandbyCutterID":
                    return magazine_info_StandbyCutterID;

                case "magazine_info_StandbyMagaID":
                    return magazine_info_StandbyMagaID;

                case "magazine_info_SPCutterID":
                    return magazine_info_SPCutterID;

                case "processtime_TotalWorkTime":
                    return processtime_TotalWorkTime;

                case "processtime_SingleWorkTime":
                    return processtime_SingleWorkTime;

                case "part_count_target_part_count":
                    return part_count_target_part_count;

                case "part_count_finish_part_count":
                    return part_count_finish_part_count;

                case "MacroVariablebyID_RetValue":
                    return MacroVariablebyID_RetValue;

                case "CNCParameter_Single_GroupID":
                    return CNCParameter_Single_GroupID;

                case "CNCParameter_Single_SubGroupID":
                    return CNCParameter_Single_SubGroupID;

                case "CNCParameter_Single_ParaChannel":
                    return CNCParameter_Single_ParaChannel;

                case "CNCParameter_Single_ParaAxis":
                    return CNCParameter_Single_ParaAxis;

                case "CNCParameter_Single_DataType":
                    return CNCParameter_Single_DataType;

                case "CNCParameter_Single_DataSize":
                    return CNCParameter_Single_DataSize;

                case "System_Time_strSystemTime":
                    return System_Time_strSystemTime;

                case "system_variable_SysVarNum":
                    return system_variable_SysVarNum;

                case "system_variable_AxisVarNum":
                    return system_variable_AxisVarNum;

                case "user_variable_VarNum":
                    return user_variable_VarNum;

                case "equip_variable_VarNum":
                    return equip_variable_VarNum;

                case "system_status_ParaCount":
                    return system_status_ParaCount;

                case "FW_version_ParaCount":
                    return FW_version_ParaCount;

                case "HW_version_ParaCount":
                    return HW_version_ParaCount;

                case "equip_infomation_InfoCount":
                    return equip_infomation_InfoCount;

                case "channel_setting_AxisNum":
                    return channel_setting_AxisNum;

                case "port_info_PortNum":
                    return port_info_PortNum;

                case "RIO_Status_IO_Num":
                    return RIO_Status_IO_Num;

                case "RIO_setting_all_IO_Num":
                    return RIO_setting_all_IO_Num;

                case "RIO_home_limit_Port_Num":
                    return RIO_home_limit_Port_Num;

                case "RIO_filter_FilterLevel":
                    return RIO_filter_FilterLevel;

                case "disk_quota_diskquota":
                    return disk_quota_diskquota;

                case "inter_disk_quota_diskquota":
                    return inter_disk_quota_diskquota;

                case "nc_pointer_LineNum":
                    return nc_pointer_LineNum;

                case "nc_pointer_MDILineNum":
                    return nc_pointer_MDILineNum;

                case "CodeSearch_LineNo_LineNo":
                    return CodeSearch_LineNo_LineNo;

                case "CodeSearch_POSITION_AxisNum":
                    return CodeSearch_POSITION_AxisNum;

                case "MDI_code_LineNo":
                    return MDI_code_LineNo;

                case "RIO_MONITOR_RIONum":
                    return RIO_MONITOR_RIONum;

                case "SERVO_MONITOR_Num":
                    return SERVO_MONITOR_Num;

                case "Variable_MONITOR_SysVar_Type0_Num":
                    return Variable_MONITOR_SysVar_Type0_Num;

                case "Variable_MONITOR_SysVar_Type1_Num":
                    return Variable_MONITOR_SysVar_Type1_Num;

                case "Variable_MONITOR_ChannelVar_Type0_Num":
                    return Variable_MONITOR_ChannelVar_Type0_Num;

                case "Variable_MONITOR_ChannelVar_Type1_Num":
                    return Variable_MONITOR_ChannelVar_Type1_Num;

                case "Variable_MONITOR_ChannelVar_Type2_Num":
                    return Variable_MONITOR_ChannelVar_Type2_Num;

                //手冊 in/out 標註缺失
                case "Variable_MONITOR_AxisVar_AxisNum":
                    return Variable_MONITOR_AxisVar_AxisNum;

                case "Variable_MONITOR_AxisVar_Type0_Num":
                    return Variable_MONITOR_AxisVar_Type0_Num;

                case "Variable_MONITOR_AxisVar_Type1_Num":
                    return Variable_MONITOR_AxisVar_Type1_Num;

                case "Variable_MONITOR_AxisVar_Type2_Num":
                    return Variable_MONITOR_AxisVar_Type2_Num;

                case "Variable_MONITOR_HMIVar_Type0_Num":
                    return Variable_MONITOR_HMIVar_Type0_Num;

                case "Variable_MONITOR_HMIVar_Type1_Num":
                    return Variable_MONITOR_HMIVar_Type1_Num;

                case "Variable_MONITOR_HMIVar_Type2_Num":
                    return Variable_MONITOR_HMIVar_Type2_Num;

                case "Variable_MONITOR_HMIVar_Type3_Num":
                    return Variable_MONITOR_HMIVar_Type3_Num;

                case "Variable_MONITOR_MLCVar_VarNum":
                    return Variable_MONITOR_MLCVar_VarNum;

                default:
                    return 0;
            }
        }

        public object ReturnValue(string name, int row, int col)
        {
            switch (name)
            {
                case "Channel_information_AxesOfChannel":
                    return Channel_information_AxesOfChannel[col];

                case "Channel_information_ServoAxesofChannel":
                    return Channel_information_ServoAxesofChannel[col];

                case "Channel_information_MaxAxisOfChannel":
                    return Channel_information_MaxAxisOfChannel[col];

                case "Channel_information_AxisNameOfChannel":
                    return Channel_information_AxisNameOfChannel[row, col];

                case "Channel_information_DecPointOfChannel":
                    return Channel_information_DecPointOfChannel[col];

                case "NCMOTION_GGroup":
                    return NCMOTION_GGroup[col];

                case "gcode_Gdata":
                    return gcode_Gdata[col];

                case "alm_current_AlarmCode":
                    return alm_current_AlarmCode[col];

                case "alm_current_AlarmDataLen":
                    return alm_current_AlarmDataLen[col];

                case "alm_current_AlarmData":
                    return alm_current_AlarmData[row, col];

                case "alm_current_AlarmMsg":
                    return alm_current_AlarmMsg[col];

                case "alm_current_AlarmDateTime":
                    return alm_current_AlarmDateTime[col];

                case "servo_current_AxisNr":
                    return servo_current_AxisNr[col];

                case "servo_current_Result":
                    return servo_current_Result[col];

                case "servo_current_AxisValue":
                    return servo_current_AxisValue[col];

                case "spindle_current_AxisNr":
                    return spindle_current_AxisNr[col];

                case "spindle_current_Result":
                    return spindle_current_Result[col];

                case "spindle_current_AxisValue":
                    return spindle_current_AxisValue[col];

                case "servo_load_AxisNr":
                    return servo_load_AxisNr[col];

                case "servo_load_Result":
                    return servo_load_Result[col];

                case "servo_load_AxisValue":
                    return servo_load_AxisValue[col];

                case "spindle_load_AxisNr":
                    return spindle_load_AxisNr[col];

                case "spindle_load_Result":
                    return spindle_load_Result[col];

                case "spindle_load_AxisValue":
                    return spindle_load_AxisValue[col];

                case "servo_speed_AxisNr":
                    return servo_speed_AxisNr[col];

                case "servo_speed_Result":
                    return servo_speed_Result[col];

                case "servo_speed_AxisValue":
                    return servo_speed_AxisValue[col];

                case "spindle_speed_AxisNr":
                    return spindle_speed_AxisNr[col];

                case "spindle_speed_Result":
                    return spindle_speed_Result[col];

                case "spindle_speed_AxisValue":
                    return spindle_speed_AxisValue[col];

                case "servo_temperature_AxisNr":
                    return servo_temperature_AxisNr[col];

                case "servo_temperature_Result":
                    return servo_temperature_Result[col];

                case "servo_temperature_AxisValue":
                    return servo_temperature_AxisValue[col];

                case "spindle_temperature_AxisNr":
                    return spindle_temperature_AxisNr[col];

                case "spindle_temperature_Result":
                    return spindle_temperature_Result[col];

                case "spindle_temperature_AxisValue":
                    return spindle_temperature_AxisValue[col];

                case "POSITION_AxisName":
                    return POSITION_AxisName[col];

                case "POSITION_Coor_Mach":
                    return POSITION_Coor_Mach[col];

                case "POSITION_Coor_Abs":
                    return POSITION_Coor_Abs[col];

                case "POSITION_Coor_Rel":
                    return POSITION_Coor_Rel[col];

                case "POSITION_Coor_Res":
                    return POSITION_Coor_Res[col];

                case "POSITION_Coor_Offset":
                    return POSITION_Coor_Offset[col];

                case "GPOSITION_Title_GPOSITION_Title":
                    return GPOSITION_Title_GPOSITION_Title[col];

                case "GPOSITION_GPositionArray":
                    return GPOSITION_GPositionArray[row, col];

                case "Offset_Coord_CoorArray":
                    return Offset_Coord_CoorArray[col];

                case "cutter_title_CutterTitle":
                    return cutter_title_CutterTitle[col];

                case "cutter_CutterData":
                    return cutter_CutterData[row, col];

                case "magazine_info_CutterID":
                    return magazine_info_CutterID[col];

                case "MacroVariable_RetArray":
                    return MacroVariable_RetArray[col];

                case "PLC_ADDR_RetArray":
                    return PLC_ADDR_RetArray[col];

                case "CNCParameter_Single_Data":
                    return CNCParameter_Single_Data[col];

                case "system_variable_SysVarChannel":
                    return system_variable_SysVarChannel[col];

                case "system_variable_SysVarID":
                    return system_variable_SysVarID[col];

                case "system_variable_SysVarType":
                    return system_variable_SysVarType[col];

                case "system_variable_SysVarValue":
                    return system_variable_SysVarValue[row, col];

                case "system_variable_AxisVarChannel":
                    return system_variable_AxisVarChannel[row];

                case "system_variable_AxisNum":
                    return system_variable_AxisNum[col];

                case "system_variable_AxisVarID":
                    return system_variable_AxisVarID[col];

                case "system_variable_AxisVarType":
                    return system_variable_AxisVarType[col];

                case "system_variable_AxisID":
                    return system_variable_AxisID[row, col];
                //z: 數值index先設定為0
                case "system_variable_AxisVarValue":
                    return system_variable_AxisVarValue[row, col, 0];

                case "user_variable_VarReg":
                    return user_variable_VarReg[col];

                case "user_variable_VarValue":
                    return user_variable_VarValue[col];

                case "equip_variable_VarReg":
                    return equip_variable_VarReg[col];

                case "equip_variable_VarValue":
                    return equip_variable_VarValue[col];

                case "system_status_ParaID":
                    return system_status_ParaID[col];

                case "system_status_ParaName":
                    return system_status_ParaName[col];

                case "system_status_DataType":
                    return system_status_DataType[col];

                case "system_status_DataSize":
                    return system_status_DataSize[col];

                case "system_status_Data":
                    return system_status_Data[row, col];

                case "FW_version_ParaID":
                    return FW_version_ParaID[col];

                case "FW_version_ParaName":
                    return FW_version_ParaName[col];

                case "FW_version_DataType":
                    return FW_version_DataType[col];

                case "FW_version_DataSize":
                    return FW_version_DataSize[col];

                case "FW_version_Data":
                    return FW_version_Data[row, col];

                case "HW_version_ParaID":
                    return HW_version_ParaID[col];

                case "HW_version_ParaName":
                    return HW_version_ParaName[col];

                case "HW_version_DataType":
                    return HW_version_DataType[col];

                case "HW_version_DataSize":
                    return HW_version_DataSize[col];

                case "HW_version_Data":
                    return HW_version_Data[row, col];

                case "equip_infomation_EquipInfo":
                    return equip_infomation_EquipInfo[col];

                case "channel_setting_AxisID":
                    return channel_setting_AxisID[col];

                case "channel_setting_IsEnable":
                    return channel_setting_IsEnable[col];

                case "channel_setting_AxisTypeID":
                    return channel_setting_AxisTypeID[col];

                case "channel_setting_PortID":
                    return channel_setting_PortID[col];

                case "channel_setting_AxisName":
                    return channel_setting_AxisName[col];

                case "port_info_PortID":
                    return port_info_PortID[col];

                case "port_info_IsEnable":
                    return port_info_IsEnable[col];

                case "port_info_ChannelID":
                    return port_info_ChannelID[col];

                case "port_info_AxisID":
                    return port_info_AxisID[col];

                case "port_info_AxisTypeID":
                    return port_info_AxisTypeID[col];

                case "RIO_Status_RIO_Status":
                    return RIO_Status_RIO_Status[col];

                case "RIO_setting_all_IsEnable":
                    return RIO_setting_all_IsEnable[col];

                case "RIO_setting_all_RIOType":
                    return RIO_setting_all_RIOType[col];

                case "RIO_setting_all_Polarity":
                    return RIO_setting_all_Polarity[col];

                case "RIO_setting_all_IsDisc":
                    return RIO_setting_all_IsDisc[col];

                case "RIO_home_limit_Homelimit":
                    return RIO_home_limit_Homelimit[col];

                //手冊編誤:程式內容為strCode
                case "preview_code_strCode":
                    return preview_code_strCode[col];

                case "current_code_LineNo":
                    return current_code_LineNo[col];
                //手冊編誤:程式內容為strCode
                case "current_code_strCode":
                    return current_code_strCode[col];

                case "CodeSearch_POSITION_AxisID":
                    return CodeSearch_POSITION_AxisID[col];

                case "CodeSearch_POSITION_Coor_Mach":
                    return CodeSearch_POSITION_Coor_Mach[col];

                case "CodeSearch_POSITION_Coor_Abs":
                    return CodeSearch_POSITION_Coor_Abs[col];

                case "CodeSearch_POSITION_Coor_Rel":
                    return CodeSearch_POSITION_Coor_Rel[col];

                case "CodeSearch_POSITION_Coor_Res":
                    return CodeSearch_POSITION_Coor_Res[col];

                case "CodeSearch_POSITION_Coor_Offset":
                    return CodeSearch_POSITION_Coor_Offset[col];

                case "MDI_code_strCode":
                    return MDI_code_strCode[col];

                case "RIO_MONITOR_IsEnable":
                    return RIO_MONITOR_IsEnable[col];

                case "RIO_MONITOR_IsConnect":
                    return RIO_MONITOR_IsConnect[col];

                case "RIO_MONITOR_RIOType":
                    return RIO_MONITOR_RIOType[col];

                case "SERVO_MONITOR_Channel":
                    return SERVO_MONITOR_Channel[col];

                case "SERVO_MONITOR_AxisID":
                    return SERVO_MONITOR_AxisID[col];

                case "SERVO_MONITOR_IsConnect":
                    return SERVO_MONITOR_IsConnect[col];

                case "SERVO_MONITOR_IsServoOn":
                    return SERVO_MONITOR_IsServoOn[col];

                case "SERVO_MONITOR_Load":
                    return SERVO_MONITOR_Load[col];

                case "SERVO_MONITOR_Peak":
                    return SERVO_MONITOR_Peak[col];

                case "SERVO_MONITOR_MechCoord":
                    return SERVO_MONITOR_MechCoord[col];

                case "SERVO_MONITOR_IsHome":
                    return SERVO_MONITOR_IsHome[col];

                case "SERVO_MONITOR_AbsHomeSet":
                    return SERVO_MONITOR_AbsHomeSet[col];

                case "SERVO_MONITOR_EncoderType":
                    return SERVO_MONITOR_EncoderType[col];

                case "Variable_MONITOR_SysVar_Type0_Value":
                    return Variable_MONITOR_SysVar_Type0_Value[col];

                case "Variable_MONITOR_SysVar_Type1_Value":
                    return Variable_MONITOR_SysVar_Type1_Value[col];

                case "Variable_MONITOR_ChannelVar_Type0_Value":
                    return Variable_MONITOR_ChannelVar_Type0_Value[col];

                case "Variable_MONITOR_ChannelVar_Type1_Value":
                    return Variable_MONITOR_ChannelVar_Type1_Value[col];

                case "Variable_MONITOR_ChannelVar_Type2_Value":
                    return Variable_MONITOR_ChannelVar_Type2_Value[col];

                case "Variable_MONITOR_AxisVar_AxisID":
                    return Variable_MONITOR_AxisVar_AxisID[col];

                case "Variable_MONITOR_AxisVar_Type0_Value":
                    return Variable_MONITOR_AxisVar_Type0_Value[row, col];

                case "Variable_MONITOR_AxisVar_Type1_Value":
                    return Variable_MONITOR_AxisVar_Type1_Value[row, col];

                case "Variable_MONITOR_AxisVar_Type2_Value":
                    return Variable_MONITOR_AxisVar_Type2_Value[row, col];

                case "Variable_MONITOR_HMIVar_Type0_Value":
                    return Variable_MONITOR_HMIVar_Type0_Value[col];

                case "Variable_MONITOR_HMIVar_Type1_Value":
                    return Variable_MONITOR_HMIVar_Type1_Value[col];

                case "Variable_MONITOR_HMIVar_Type2_Value":
                    return Variable_MONITOR_HMIVar_Type2_Value[row, col];

                case "Variable_MONITOR_HMIVar_Type3_Value":
                    return Variable_MONITOR_HMIVar_Type3_Value[col];

                case "Variable_MONITOR_MLCVar_MLCVar":
                    return Variable_MONITOR_MLCVar_MLCVar[col];

                default:
                    return 0;
            }
        }
    }
}