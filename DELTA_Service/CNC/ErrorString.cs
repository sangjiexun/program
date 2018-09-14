namespace DELTA_Form
{
    internal class ErrorString
    {
        /// <summary>
        /// error message
        /// </summary>
        /// <param name="errorcode"></param>
        /// <returns></returns>
        public string errorstring(int errorcode)
        {
            switch (errorcode)
            {
                case 2: return "檔案已經開啟";
                case 1: return "執行完成, 但過程中有加工狀態";
                case 0: return "執行成功";
                case -1: return "連線錯誤";
                case -2: return "檔案刪除錯誤";
                case -9: return "傳輸錯誤";
                case -10: return "系統加工中";
                case -11: return "系統其他連線傳輸中";
                case -12: return "無法寫入備份檔";
                case -13: return "傳輸資料超過系統BUFFSIZE";
                case -14: return "磁碟空間不足";
                case -15: return "CNC讀取檔案錯誤";
                case -16: return "CNC寫入檔案錯誤";
                case -17: return "輸入參數錯誤";
                case -18: return "軸不符合";
                case -19: return "認證密碼錯誤";
                case -20: return "設定錯誤";
                case -21: return "Client端檔案存取錯誤";
                case -23: return "網路回傳空白";
                case -24: return "連線中，無法設定";
                case -25: return "禁止連續匯入兩次";
                case -26: return "開啟主檔失敗";
                case -27: return "通道錯誤";
                case -28: return "相關參數未開啟";
                case -29: return "參數唯讀";
                case -30: return "系統權限認證錯誤";
                case -31: return "設備商權限認證錯誤";
                case -32: return "用戶權限1認證錯誤";
                case -33: return "用戶權限2認證錯誤";
                case -34: return "權限認證錯誤";
                case -35: return "密碼錯誤";
                case -36: return "API不存在";
                case -37: return "API不可使用";
                case -38: return "回傳指令錯誤";
                case -39: return "MLC讀取寫入Buffer為空白";
                case -44: return "伺服未備妥";
                case -45: return "輸入值超出範圍";
                case -46: return "通道不符合";
                case -47: return "feedhold狀態";
                case -48: return "Bit無法設定";
                case -49: return "當節未完成";
                case -50: return "伺服讀取資料失敗";
                case -51: return "主軸致能錯誤";
                case -52: return "伺服軸致能錯誤";
                case -53: return "不符合操作模式";
                case -54: return "伺服斷線";
                case -55: return "Port 已被使用";
                case -56: return "Port 超出範圍";
                case -57: return "顯示名稱超過範圍";
                case -58: return "RIO type 超過範圍";
                case -59: return "MLC 檔案不存在";
                case -60: return "MLC 開啟檔案錯誤";
                case -61: return "MLC 檔案 CRC 錯誤";
                case -62: return "MLC 檔案 FileCode 錯誤";
                case -63: return "MLC 檔案尚未編譯";
                case -64: return "MLC 載入模式錯誤";
                case -65: return "MLC 正在編輯中, 不允許讀取";
                case -66: return "MLC 權限錯誤";
                case -67: return "MLC 急停狀態錯誤";
                case -68: return "LAD 檔案不存在";
                case -69: return "LCM 檔案不存在";
                case -70: return "軟體面板PLCComm檔案錯誤";
                case -71: return "檔案為空白";
                case -72: return "暫存器不存在";
                case -73: return "動作禁止";
                case -74: return "使用時限到期";
                case -75: return "斷點搜尋執行中";
                case -76: return "斷點搜尋設定錯誤";
                case -77: return "檔案格式不符";
                case -78: return "韌體更新機種錯誤";
                case -79: return "韌體更新版本不支援";
                case -80: return "韌體更新失敗 (HMI)";
                case -81: return "韌體更新失敗 (MLC)";
                case -82: return "韌體更新失敗 (Motion)";
                case -83: return "韌體更新失敗 (FPGA)";
                case -84: return "韌體更新失敗 (COMM FAIL)";
                case -85: return "BUFFER 資料還未處理完";
                case -86: return "GCode Buffer Size錯誤";
                case -87: return "GCode Buffer 行數錯誤";
                case -88: return "GCode內容錯誤(含M98,M99)";
                case -89: return "GCode內容錯誤(含WHILE)";
                case -90: return "GCode內容錯誤(含GOTO)";
                case -91: return "GCode內容錯誤(含N Label)";
                case -92: return "GCode BUFFER 未啟動";
                case -93: return "GCode BUFFER 已啟動";
                case -94: return "馬達型態錯誤";
                case -95: return "MLC 監控錯誤";
                case -96: return "該刀具被封鎖";
                case -97: return "系統時限已經設定";
                case -98: return "機台鎖定碼設定錯誤";
                case -99: return "MDI FindMdiGcodeCmdToNc fail";
                case -999: return "未知錯誤";
                default: return "Error Code 超出範圍";
            }
        }
    }
}