/*
 Tag 異動 OK
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DELTA_Service.CID
{
    class Tag_Transaction
    {
        //READ_CNCFlag: 讀取CNC加工、Alarm 與RestartAct Flag
        public bool CNCFlag_WorkingFlag = false;
        public bool CNCFlag_AlarmFlag = false;
        public bool CNCFlag_RestartAct = false;

        //READ_CNC_information : CNC基本資訊
        public bool CNC_information_CncType = false;
        public bool CNC_information_MaxChannels = false;
        public bool CNC_information_Series = false;
        public bool CNC_information_Nc_Ver = false;
        public bool CNC_information_ScrewUnit = false;
        public bool CNC_information_DisplayUnit = false;
        public bool CNC_information_ApiVersion = false;
        //READ_status : CNC狀態
        public bool status_MainProg = false;
        public bool status_CurProg = false;
        public bool status_ProgPath = false;
        public bool status_CurSeq = false;
        public bool status_MDICur = false;
        public bool status_Mode = false;
        public bool status_Status = false;
        public bool status_IsAlarm = false;
        public bool status_IsWorking = false;
        public bool status_RestartAct = false;
        //READ_NCMOTION : CNC Motion狀態資訊
        public bool NCMOTION_Unit = false;
        public bool NCMOTION_iSpSpeed = false;
        public bool NCMOTION_iFeed = false;
        public bool NCMOTION_iLoad = false;
        public bool NCMOTION_iActSpeed = false;
        public bool NCMOTION_iActFeed = false;
        public bool NCMOTION_iDwellTime = false;
        public bool NCMOTION_iMCutter = false;
        public bool NCMOTION_iCutterLib = false;
        public bool NCMOTION_iCmdCutter = false;
        public bool NCMOTION_iStandbyCutter = false;
        public bool NCMOTION_iStandbyCast = false;
        public bool NCMOTION_iDData = false;
        public bool NCMOTION_iHData = false;
        public bool NCMOTION_iTData = false;
        public bool NCMOTION_iMData = false;
        public bool NCMOTION_GGroup = false;
        public bool NCMOTION_iSpeedF = false;
        public bool NCMOTION_iSpeedRPD = false;
        public bool NCMOTION_iSpeedS = false;
        public bool NCMOTION_iSpeedJOG = false;
        public bool NCMOTION_iSpeedMPG = false;
        //READ_feed_spindle : 讀取進給率/轉速
        public bool feed_spindle_OvFeed = false;
        public bool feed_spindle_OvSpindle = false;
        public bool feed_spindle_ActFeed = false;
        public bool feed_spindle_ActSpindle = false;
        //READ_current_alarm_count: 讀取全部目前警報個數
        public bool current_alarm_count_AlarmCount = false;
        //READ_alm_current: 讀取目前發生警報
        public bool alm_current_AlarmCount = false;
        public bool alm_current_AlarmCode = false;
        public bool alm_current_AlarmDataLen = false;
        public bool alm_current_AlarmData = false;
        public bool alm_current_AlarmMsg = false;
        public bool alm_current_AlarmDateTime = false;
        //READ_history_alarm_count: 讀取全部歷史警報個數
        public bool history_alarm_count_AlarmCount = false;
        //READ_alm_history_all: 讀取全部歷史警報訊息
        public bool alm_history_all_AlarmCount = false;
        public bool alm_history_all_AlarmCode = false;
        public bool alm_history_all_AlarmDataLen = false;
        public bool alm_history_all_AlarmData = false;
        public bool alm_history_all_AlarmMsg = false;
        public bool alm_history_all_AlarmDateTime = false;
        //READ_alm_history: 讀取歷史警報訊息
        public bool alm_history_AlarmCount = false;
        public bool alm_history_AlarmCode = false;
        public bool alm_history_AlarmDataLen = false;
        public bool alm_history_AlarmData = false;
        public bool alm_history_AlarmMsg = false;
        public bool alm_history_AlarmDateTime = false;
        //READ_servo_current: 讀取伺服軸負載電流
        public bool servo_current_AxisCount = false;
        public bool servo_current_AxisNr = false;
        public bool servo_current_Result = false;
        public bool servo_current_AxisValue = false;
        //READ_spindle_current: 讀取主軸負載電流
        public bool spindle_current_AxisCount = false;
        public bool spindle_current_AxisNr = false;
        public bool spindle_current_Result = false;
        public bool spindle_current_AxisValue = false;
        //READ_servo_load: 讀取伺服軸負載
        public bool servo_load_AxisCount = false;
        public bool servo_load_AxisNr = false;
        public bool servo_load_Result = false;
        public bool servo_load_AxisValue = false;
        //READ_spindle_load: 讀取主軸負載
        public bool spindle_load_AxisCount = false;
        public bool spindle_load_AxisNr = false;
        public bool spindle_load_Result = false;
        public bool spindle_load_AxisValue = false;
        //READ_spindle_speed: 讀取主軸轉速
        public bool spindle_speed_AxisCount = false;
        public bool spindle_speed_AxisNr = false;
        public bool spindle_speed_Result = false;
        public bool spindle_speed_AxisValue = false;
        //READ_cutter : 讀取刀具資訊
        public bool cutter_CutterData = false;
        //READ_magazine_info : 讀取刀庫資訊
        public bool magazine_info_CutterNum = false;
        public bool magazine_info_CMDCutterID = false;
        public bool magazine_info_StandbyCutterID = false;
        public bool magazine_info_StandbyMagaID = false;
        public bool magazine_info_SPCutterID = false;
        public bool magazine_info_CutterID = false;
        //READ_processtime : 讀取加工時間
        public bool processtime_TotalWorkTime = false;
        public bool processtime_SingleWorkTime = false;
        //READ_part_count : 讀取加工數
        public bool part_count_target_part_count = false;
        public bool part_count_finish_part_count = false;
        //系統資訊-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //READ_System_Time : 讀取系統日期時間
        public bool System_Time_strSystemTime = false;
        //READ_system_variable : 讀取系統變數
        public bool system_variable_SysVarNum = false;
        public bool system_variable_AxisVarNum = false;
        public bool system_variable_SysVarChannel = false;
        public bool system_variable_SysVarID = false;
        public bool system_variable_SysVarType = false;
        public bool system_variable_SysVarValue = false;
        public bool system_variable_AxisVarChannel = false;
        public bool system_variable_AxisNum = false;
        public bool system_variable_AxisVarID = false;
        public bool system_variable_AxisVarType = false;
        public bool system_variable_AxisID = false;
        public bool system_variable_AxisVarValue = false;
        //READ_user_variable : 讀取用戶變數
        public bool user_variable_VarNum = false;
        public bool user_variable_VarReg = false;
        public bool user_variable_VarValue = false;
        //READ_equip_variable : 讀取設備變數
        public bool equip_variable_VarNum = false;
        public bool equip_variable_VarReg = false;
        //READ_system_status : 讀取系統狀態
        public bool system_status_ParaCount = false;
        public bool system_status_ParaID = false;
        public bool system_status_ParaName = false;
        public bool system_status_DataType = false;
        public bool system_status_DataSize = false;
        public bool system_status_Data = false;
        // READ_equip_infomation : 讀取設備資訊
        public bool equip_infomation_InfoCount = false;
        public bool equip_infomation_EquipInfo = false;
        //READ_nc_pointer : 讀取NC 執行行號
        public bool nc_pointer_LineNum = false;
        public bool nc_pointer_MDILineNum = false;
        //READ_preview_code: 讀取NC 預讀程式內容
        public bool preview_code_LineNo = false;
        public bool preview_code_strCode = false;
        //READ_current_code : 讀取NC 當前程式內容
        public bool current_code_LineNo = false;
        public bool current_code_strCode = false;
        //READ_RIO_MONITOR : I/O 監控
        public bool RIO_MONITOR_RIONum = false;
        public bool RIO_MONITOR_IsEnable = false;
        public bool RIO_MONITOR_IsConnect = false;
        public bool RIO_MONITOR_RIOType = false;
        //READ_SERVO_MONITOR: 伺服監控
        public bool SERVO_MONITOR_Num = false;
        public bool SERVO_MONITOR_Channel = false;
        public bool SERVO_MONITOR_AxisID = false;
        public bool SERVO_MONITOR_IsConnect = false;
        public bool SERVO_MONITOR_IsServoOn = false;
        public bool SERVO_MONITOR_Load = false;
        public bool SERVO_MONITOR_Peak = false;
        public bool SERVO_MONITOR_MechCoord = false;
        public bool SERVO_MONITOR_IsHome = false;
        public bool SERVO_MONITOR_AbsHomeSet = false;
        public bool SERVO_MONITOR_EncoderType = false;
    }
}
