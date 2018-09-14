using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DELTA_Service.Fanuc
{
    class Fanuc_ErrorString
    {
        public static string Description(int error_code)
        {
            switch (error_code)
            {
                case -17:
                    return "Protocol error\nData from Ethernet Board is incorrect. Contact with the service section or the section in charge. ";

                case -16:
                    return "Socket error\nInvestigate CNC power supply, Ethernet cable and I/F board. ";

                case -15:
                    return "DLL file error \nThere is no DLL file for each CNC series corresponding to specified node. ";

                case -11:
                    return "Bus error\nA bus error of CNC system occurred. Contact with the service section or the section in charge. ";

                case -10:
                    return "System error\nA system error of CNC system occurred. Contact with the service section or the section in charge. ";

                case -9:
                    return "Communication error of HSSB\nInvestigate the serial line or I/F board of HSSB. ";

                case -8:
                    return "Handle number error\nGet the library handle number.  ";

                case -7:
                    return "Version mismatch between the CNC/PMC and library \nThe CNC/PMC version does not match that of the library.Replace the library or the CNC/ PMC control software. ";

                case -6:
                    return "Abnormal library state \nAn unanticipated error occurred. Contact with the section in charge. ";

                case -5:
                    return "System error\nA system error of CNC occurred. Contact with the service section or the section in charge. ";

                case -4:
                    return "Shared RAM parity error\nA hardware error occurred. Contact with the service section. ";

                case -3:
                    return "FANUC drivers installation error\n The drivers required for execution are not installed. ";

                case -2:
                    return "Reset or stop request\nThe RESET or STOP button was pressed. Call the termination function.  ";

                case -1:
                    return "Busy\nWait until the completion of CNC processing, or retry. ";

                case 0:
                    return "Normal";

                case 1:
                    return "Error(function is not executed, or not available) \nSpecific function which must be executed beforehand has not been executed. Otherwise that function is not available. ";

                case 2:
                    return "Error(data block length error, error of number of data) \nCheck and correct the data block length or number of data. ";

                case 3:
                    return "Error(data number error) \nCheck and correct the data number. ";

                case 4:
                    return "Error(data attribute error) \nCheck and correct the data attribute. ";

                case 5:
                    return "Error(data error) \nCheck and correct the data.";

                case 6:
                    return "Error(no option) \nThere is no corresponding CNC option. ";
                case 7:
                    return "Error(write protection) \nWrite operation is prohibited. ";
                case 8:
                    return "Error(memory overflow) \nCNC tape memory is overflowed. ";
                case 9:
                    return "Error(CNC parameter error) \nCNC parameter is set incorrectly. ";
                case 10:
                    return "Error(buffer empty/full) \nThe buffer is empty or full. Wait until completion of CNC processing, or retry. ";
                case 11:
                    return "Error(path number error) \nA path number is incorrect. ";
                case 12:
                    return "Error(CNC mode error) \nThe CNC mode is incorrect. Correct the CNC mode. ";
                case 13:
                    return "Error(CNC execution rejection) \nThe execution at the CNC is rejected. Check the condition of execution. ";
                case 14:
                    return "Error(Data server error) \nSome errors occur at the data server. ";
                case 15:
                    return "Error(alarm) \nThe function cannot be executed due to an alarm in CNC. Remove the cause of alarm. ";
                case 16:
                    return "Error(stop) \nCNC status is stop or emergency. ";
                case 17:
                    return "Error(State of data protection) \nData is protected by the CNC data protection function. ";


                default:
                    return "Unknown Error";
            }
        }
    }
}
