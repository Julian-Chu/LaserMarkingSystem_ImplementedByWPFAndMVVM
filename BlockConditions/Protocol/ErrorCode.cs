using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BlockConditionsWindow.Protocol
{
    public class ErrorCode
    {
        // Identification code(2byte)+,+ IsError(0 or 1)+ ErrorCode
        public bool NoErrorExists(string response)
        {
            try
            {
                string[] responseArray = response.Split(',', '\r');
                if (responseArray[1] == "0")
                    return true;
                else
                {
                    if (responseArray[0] == "EX")
                    {
                        string ErrMsg = ExtractExMsg(response);
                        MessageBox.Show(ErrMsg);
                        return false;
                    }
                    else
                    {
                        string ErrMsg = CommunicationErrorMsg(response);
                        MessageBox.Show(ErrMsg);
                        return false;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        internal string CommunicationErrorMsg(string response)
        {

            try
            {
                string[] responseArray = response.Split(',', '\r');
                string IDcode = responseArray[0];
                string _errorCode = responseArray[2];  //substract the error code


                //_errorCode += IDcode +" : ";  //attach the ID code to ErrorCode
                #region
                switch (_errorCode)
                {
                    case "S000":
                        return _errorCode + ":" + "Program Incorrect Error";
                    case "S001":
                        return _errorCode + ":" + "Program Memory Full Error ";
                    case "S002":
                        return _errorCode + ":" + "Built-in Memory Card Full Error";
                    case "S003":
                        return _errorCode + ":" + "External Memory Card Full Error";
                    case "S004":
                        return _errorCode + ":" + "External Memory Card Not Inserted Error";
                    case "S005":
                        return _errorCode + ":" + "External Memoey Card Unrecognizable Error";
                    case "S006":
                        return _errorCode + ":" + "Priority Error";
                    case "S008":
                        return _errorCode + ":" + "No-File Error";
                    case "S009":
                        return _errorCode + ":" + "Busy Error";
                    case "S010":
                        return _errorCode + ":" + "No Marking Block Error";
                    case "S011":
                        return _errorCode + ":" + "Logo/Custom Character Number Exceed Error";
                    case "S012":
                        return _errorCode + ":" + "Incorrect Optimization Error";
                    case "S014":
                        return _errorCode + ":" + "Current Program Operation Error";
                    case "S015":
                        return _errorCode + ":" + "Logo/Custom Character File Operation Error";
                    case "S016":
                        return _errorCode + ":" + "Tesk Mark Unexecutable Error";
                    case "S018":
                        return _errorCode + ":" + "Barcode/2D Code Program Incorrect Error";
                    case "S019":
                        return _errorCode + ":" + "All-Setup Restoration Error";
                    case "S020":
                        return _errorCode + ":" + "Data Length Error";
                    case "S021":
                        return _errorCode + ":" + "Program Number Not Registered Error";
                    case "S022":
                        return _errorCode + ":" + "Block Number Not Registered Error";
                    case "S024":
                        return _errorCode + ":" + "Illegal Command Error";
                    case "S025":
                        return _errorCode + ":" + "Checksum Error";
                    case "S026":
                        return _errorCode + ":" + "Format Error";
                    case "S027":
                        return _errorCode + ":" + "Command Unrecognizable Error";
                    case "S028":
                        return _errorCode + ":" + "Response Data Length Error";
                    case "S029":
                        return _errorCode + ":" + "Mark Data Request Error";
                    case "S030":
                        return _errorCode + ":" + "Group Number Not Registered Error";
                    case "S050":
                        return _errorCode + ":" + "Quick Change of Character Setup Error";
                    case "S051":
                        return _errorCode + ":" + "Sample Marking Unexecutable Error";
                    case "S052":
                        return _errorCode + ":" + "Laser Inspection Unexecutable Error";
                    case "S060":
                        return _errorCode + ":" + "Block Type Program Incorrect Error";
                    case "S061":
                        return _errorCode + ":" + "Block Position Program Incorrect Error";
                    case "S062":
                        return _errorCode + ":" + "Character Size Program Incorrect Error";
                    case "S063":
                        return _errorCode + ":" + "Character Layout Program Incorrect Error";
                    case "S064":
                        return _errorCode + ":" + "Character Details Program Incorrect Error";
                    case "S065":
                        return _errorCode + ":" + "Marking Parameters Program Incorrect Error";
                    case "S066":
                        return _errorCode + ":" + "Barcode/2D Code Condition Program Incorrect Error";
                    case "S067":
                        return _errorCode + ":" + "Continous Marking Program Incorrect Error";
                    case "S068":
                        return _errorCode + ":" + "Movement/Marking Direction Program Incorrect Error";
                    case "S069":
                        return _errorCode + ":" + "Line Settngs Program Incorrect Error";
                    case "S070":
                        return _errorCode + ":" + "Palette Information Program Incorrect Error";
                    case "S071":
                        return _errorCode + ":" + "Palette Workpiece Information Program Incorrect Error";
                    case "S072":
                        return _errorCode + ":" + "String Program Incorrect Error";
                    case "S073":
                        return _errorCode + ":" + "Individual Counter Program Incorrect Error";
                    case "S074":
                        return _errorCode + ":" + "Common Counter Program Incorrect Error";
                    case "S075":
                        return _errorCode + ":" + "Preset Information Program Incorrect Error";
                    case "S076":
                        return _errorCode + ":" + "System Information Program Incorrect Error";
                    case "S077":
                        return _errorCode + ":" + "Font Replacemebt Information Program Incorrect Error";
                    case "S078":
                        return _errorCode + ":" + "Font Scaling Information Program Incorrect Error";
                    case "S079":
                        return _errorCode + ":" + "Font Skip Cross Width Information Program Incorrect Error";
                    case "S080":
                        return _errorCode + ":" + "Logo/Custom Character Buffer Information Program Incorrect Error";
                    case "S081":
                        return _errorCode + ":" + "Current Value Information Program Incorrect Error";
                    case "S082":
                        return _errorCode + ":" + "3D System information Program Incorrect Error";
                    case "S083":
                        return _errorCode + ":" + "3D Information Program Incorrect Error";
                    case "S084":
                        return _errorCode + ":" + "Operation Limit Error";
                    case "S086":
                        return _errorCode + ":" + "Wobble Incorrect Setting Error";
                    case "S090":
                        return _errorCode + ":" + "Registration Barcode Error";
                    case "S091":
                        return _errorCode + ":" + "Barcode and 2D Code Link Setting Error";
                    case "S092":
                        return _errorCode + ":" + "Barcode Registration Incorrect Error";
                    default:
                        return _errorCode + ":" + "No default Msg";
                }

                #endregion
            }

            catch (IndexOutOfRangeException e) { return e.Message + "\rResponse is illegal!"; }
            catch (Exception e) { return e.Message; }

        }

        /// <summary>
        /// The response is generated by header "EX"
        /// 
        /// Extract ControllerErrorMsg by recursion
        /// 

        internal string ExtractExMsg(string EXresponse)
        {
            string[] _EXresponseArray = EXresponse.Split(',', '\r');
            try
            {
                int index = 2;
                string ErrorMsg = "";
                while (_EXresponseArray[index] != "")
                {
                    ErrorMsg += ControllerErrorMsg(_EXresponseArray[index]) + '\r';
                    index++;
                }
                return ErrorMsg;
            }
            catch (IndexOutOfRangeException e) { return e.Message + "\rResponse is illegal!"; }
            catch (Exception e) { return e.Message; }

        }

        //Method to verify the ErrorCode from controller
        private string ControllerErrorMsg(string _errorCode)
        {
            switch (_errorCode)
            {
                case "E000":
                    return _errorCode + ":" + "Laser Reflecting Wave Error";
                case "E001":
                    return _errorCode + ":" + "Laser High-Temperature Error";
                case "E002":
                    return _errorCode + ":" + "Laser Excess Voltage Error";
                case "E003":
                    return _errorCode + ":" + "Marking Uni Communication Error";
                case "E004":
                    return _errorCode + ":" + "Scanner Error";
                case "E005":
                    return _errorCode + ":" + "Shutter Error(out of order)";
                case "E006":
                    return _errorCode + ":" + "Marking Unit Not Connected Error";
                case "E007":
                    return _errorCode + ":" + "Marking Unit Model Error";
                case "E008":
                    return _errorCode + ":" + "Controller FPGA Version Error";
                case "E009":
                    return _errorCode + ":" + "Marking Unit FPGA Version Error";
                case "E010":
                    return _errorCode + ":" + "No Marking Block Error";
                case "E011":
                    return _errorCode + ":" + "Built-in Memory Card Unrecognizable Error";
                case "E012":
                    return _errorCode + ":" + "Marking Unit Data Error";
                case "E013":
                    return _errorCode + ":" + "Expansion Memory Full Error";
                case "E014":
                    return _errorCode + ":" + "Mark Memory Full Error";
                case "E015":
                    return _errorCode + ":" + "No Program Error";
                case "E016":
                    return _errorCode + ":" + "Not Optimized Error";
                case "E017":
                    return _errorCode + ":" + "No Font File Error";
                case "E018":
                    return _errorCode + ":" + "Encoder Marking Over-/speed Error";
                case "E019":
                    return _errorCode + ":" + "Mark Trigger Error";
                case "E020":
                    return _errorCode + ":" + "Expansion Memory Full Error 2";
                case "E021":
                    return _errorCode + ":" + "Sensor Timeout Error";
                case "E022":
                    return _errorCode + ":" + "Over-Area Error";
                case "E023":
                    return _errorCode + ":" + "Movement Marking Over-Area Error";
                case "E025":
                    return _errorCode + ":" + "Log File Error";
                case "E026":
                    return _errorCode + ":" + "Custom Character File Error";
                case "E027":
                    return _errorCode + ":" + "Encoding Disabled Error";
                case "E028":
                    return _errorCode + ":" + "Switching Program Unexecutable Error";
                case "E029":
                    return _errorCode + ":" + "Scanner Error 2";
                case "E030":
                    return _errorCode + ":" + "Limit Setting Error";
                case "E031":
                    return _errorCode + ":" + "Restart Error";
                case "E032":
                    return _errorCode + ":" + "Logo/Custom Character Enlargement Error";
                case "E033":
                    return _errorCode + ":" + "Skip Cross Error";
                case "E034":
                    return _errorCode + ":" + "Encoding Disabled Error";
                case "E035":
                    return _errorCode + ":" + "Quick Change of Character Setup Error";
                case "E037":
                    return _errorCode + ":" + "Machinery Oval Setting Error";
                case "E038":
                    return _errorCode + ":" + "Logo/Custom Char. Buffer Full Error";
                case "E039":
                    return _errorCode + ":" + "Wobble/Scratch incorrect setting error";
                case "E040":
                    return _errorCode + ":" + "Link Block Error";
                case "E041":
                    return _errorCode + ":" + "3D Postion Incoreect Error";
                case "E042":
                    return _errorCode + ":" + "Marking Omission Detection Error";
                case "E043":
                    return _errorCode + ":" + "Error Emission Detection Error";
                case "E044":
                    return _errorCode + ":" + "Z Over-Area Eror";
                case "E045":
                    return _errorCode + ":" + "Barcode Not Registered Error";
                case "E046":
                    return _errorCode + ":" + "Warm Up Setting Error";
                case "E047":
                    return _errorCode + ":" + "3D Block Size Error";
                case "E048":
                    return _errorCode + ":" + "Z-MAP File Error";
                case "E049":
                    return _errorCode + ":" + "No Font Error";
                case "E050":
                    return _errorCode + ":" + "Marking Data Generation Error";
                case "E051":
                    return _errorCode + ":" + "System Error 2";
                case "E052":
                    return _errorCode + ":" + "System Error 3";
                case "E053":
                    return _errorCode + ":" + "System Error 4";
                case "E054":
                    return _errorCode + ":" + "System Error 5";
                case "E055":
                    return _errorCode + ":" + "System Error 6";
                case "E056":
                    return _errorCode + ":" + "System Error 7";
                case "E057":
                    return _errorCode + ":" + "System Error 8";
                case "E058":
                    return _errorCode + ":" + "System Error 9";
                case "E059":
                    return _errorCode + ":" + "System Error 10";
                case "E060":
                    return _errorCode + ":" + "System Error 11";
                case "E061":
                    return _errorCode + ":" + "System Error 12";
                case "E062":
                    return _errorCode + ":" + "System Error 13";
                case "E063":
                    return _errorCode + ":" + "System Error 14";
                case "E064":
                    return _errorCode + ":" + "System Error 15";
                case "E065":
                    return _errorCode + ":" + "System Error 16";
                case "E066":
                    return _errorCode + ":" + "System Error 17";
                case "E067":
                    return _errorCode + ":" + "System Error 18";
                case "E068":
                    return _errorCode + ":" + "System Error 19";
                case "E069":
                    return _errorCode + ":" + "System Error 20";
                case "E090":
                    return _errorCode + ":" + "Internal Clock Not Set Error";
                case "E091":
                    return _errorCode + ":" + "Ethernet Bersion Error";
                case "E100":
                    return _errorCode + ":" + "LD High-Temperature Error";
                case "E101":
                    return _errorCode + ":" + "LD Low-Temperature Error";
                case "E102":
                    return _errorCode + ":" + "Oscillator High-Temperature Error";
                case "E103":
                    return _errorCode + ":" + "Oscillator Low-Temperature Error";
                case "E104":
                    return _errorCode + ":" + "Q Switch Disabled Error";
                case "E106":
                    return _errorCode + ":" + "Q Switch Control Error";
                case "E107":
                    return _errorCode + ":" + "Q Swtich Operation Check Error";
                case "E110":
                    return _errorCode + ":" + "Laser Power Auto Calibration Error";
                case "E120":
                    return _errorCode + ":" + "Oscillator High-Temperature Error 2";
                case "E121":
                    return _errorCode + ":" + "Oscillator Low-Temperature Error 2";
                case "E122":
                    return _errorCode + ":" + "Unconnected Temperature Control Cable Error";
                case "E123":
                    return _errorCode + ":" + "Oscillator Power Error";
                case "E130":
                    return _errorCode + ":" + "System Error 21";
                case "E131":
                    return _errorCode + ":" + "System Error 22";
                case "E132":
                    return _errorCode + ":" + "System Error 23";
                case "E133":
                    return _errorCode + ":" + "System Error 24";
                case "E134":
                    return _errorCode + ":" + "System Error 25";
                case "E135":
                    return _errorCode + ":" + "System Error 26";
                case "E136":
                    return _errorCode + ":" + "System Error 27";
                case "E137":
                    return _errorCode + ":" + "System Error 28";
                case "E138":
                    return _errorCode + ":" + "System Error 29";
                case "E139":
                    return _errorCode + ":" + "System Error 30";
                case "E140":
                    return _errorCode + ":" + "System Error 31";
                case "E141":
                    return _errorCode + ":" + "System Error 32";
                case "E142":
                    return _errorCode + ":" + "System Error 33";
                case "E143":
                    return _errorCode + ":" + "System Error 34";  //? System Error 33 in manual
                case "E145":
                    return _errorCode + ":" + "Trimming Incorrect Setting Error";
                case "E146":
                    return _errorCode + ":" + "Trimming Over-Area Error";
                case "E204":
                    return _errorCode + ":" + "Marking Unit Control Cable Not Connected Error";
                case "E220":
                    return _errorCode + ":" + "System Error 34";
                case "E221":
                    return _errorCode + ":" + "System Error 35";
                case "E222":
                    return _errorCode + ":" + "System Error 36";
                case "E223":
                    return _errorCode + ":" + "System Error 37";
                case "E224":
                    return _errorCode + ":" + "System Error 38";
                case "E225":
                    return _errorCode + ":" + "System Error 39";
                case "E226":
                    return _errorCode + ":" + "System Error 40";
                case "E227":
                    return _errorCode + ":" + "System Error 41";
                case "E228":
                    return _errorCode + ":" + "System Error 42";
                case "E229":
                    return _errorCode + ":" + "System Error 43";
                case "E230":
                    return _errorCode + ":" + "System Error 44";
                case "E231":
                    return _errorCode + ":" + "System Error 45";
                case "E232":
                    return _errorCode + ":" + "System Error 46";
                case "E233":
                    return _errorCode + ":" + "System Error 47";
                case "E234":
                    return _errorCode + ":" + "System Error 48";
                case "E235":
                    return _errorCode + ":" + "System Error 49";
                case "E250":
                    return _errorCode + ":" + "Head Cover Open Error";
                case "E251":
                    return _errorCode + ":" + "Shutter Error 2";
                case "E252":
                    return _errorCode + ":" + "Scanner Error 3";
                case "E253":
                    return _errorCode + ":" + "Scanner Error 4";
                case "E254":
                    return _errorCode + ":" + "Scanner Error 5";
                case "E255":
                    return _errorCode + ":" + "Scanner Error 6";
                case "E256":
                    return _errorCode + ":" + "Head High-Temperature Error 1";
                case "E257":
                    return _errorCode + ":" + "Head High-Temperature Error 2";
                case "E258":
                    return _errorCode + ":" + "Head High-Temperature Error 3";
                case "E259":
                    return _errorCode + ":" + "Head High-Temperature Error 4";
                case "E260":
                    return _errorCode + ":" + "Head High-Temperature Error 5";
                case "E261":
                    return _errorCode + ":" + "Head Low-Temperature Error 1";
                case "E262":
                    return _errorCode + ":" + "Head Low-Temperature Error 2";
                case "E263":
                    return _errorCode + ":" + "Head Low-Temperature Error 3";
                case "E264":
                    return _errorCode + ":" + "Head Low-Temperature Error 4";
                case "E265":
                    return _errorCode + ":" + "Head Low-Temperature Error 5";
                case "E266":
                    return _errorCode + ":" + "LD Under-Current Error";
                case "E300":
                    return _errorCode + ":" + "Memory Check Error 1";
                case "E301":
                    return _errorCode + ":" + "Memory Check Error 2";
                case "E302":
                    return _errorCode + ":" + "Memory Check Error 3";
                case "E303":
                    return _errorCode + ":" + "Memory Check Error 4";
                case "E304":
                    return _errorCode + ":" + "Memory Check Error 5";
                case "E305":
                    return _errorCode + ":" + "Memory Check Error 6";
                case "E306":
                    return _errorCode + ":" + "Memory Check Error 7";
                case "E307":
                    return _errorCode + ":" + "Memory Check Error 8";
                case "E308":
                    return _errorCode + ":" + "Memory Check Error 9";
                case "E309":
                    return _errorCode + ":" + "Memory Check Error 10";
                case "E310":
                    return _errorCode + ":" + "Memory Check Error 11";
                case "E311":
                    return _errorCode + ":" + "Memory Check Error 12";
                case "E312":
                    return _errorCode + ":" + "Memory Check Error 13";
                case "E313":
                    return _errorCode + ":" + "Memory Check Error 14";
                case "E314":
                    return _errorCode + ":" + "Memory Check Error 15";
                case "E315":
                    return _errorCode + ":" + "Memory Check Error 16";
                case "E316":
                    return _errorCode + ":" + "Memory Check Error 17";
                case "E317":
                    return _errorCode + ":" + "Memory Check Error 18";
                case "E318":
                    return _errorCode + ":" + "Memory Check Error 19";
                case "E319":
                    return _errorCode + ":" + "Memory Check Error 20";
                ///-----Warning--------///
                case "W000":
                    return _errorCode + ":" + "Battery Life Warning";
                case "W001":
                    return _errorCode + ":" + "Laser Temperature Warning";
                case "W100":
                    return _errorCode + ":" + "LD Temperature Control Warning";
                case "W101":
                    return _errorCode + ":" + "Head Temperature Control Warning";
                case "W110":
                    return _errorCode + ":" + "Laser Power Output Low Error";
                case "W111":
                    return _errorCode + ":" + "Marking Energy Shortage Error";
                case "W112":
                    return _errorCode + ":" + "Excess Marking Energy Alarm";
                case "W120":
                    return _errorCode + ":" + "LD Current Alarm";
                case "W121":
                    return _errorCode + ":" + "Laser Unit Temperature Warning 1";
                case "W122":
                    return _errorCode + ":" + "Laser Unit Temperature Warning 2";
                case "W123":
                    return _errorCode + ":" + "Reflection Light Warning";
                case "W124":
                    return _errorCode + ":" + "LD Life Warning";
                case "W125":
                    return _errorCode + ":" + "Voltage drop warning";
                case "W130":
                    return _errorCode + ":" + "Head High-Temperature Warning 1";
                case "W131":
                    return _errorCode + ":" + "Head High-Temperature Warning 2";
                case "W132":
                    return _errorCode + ":" + "Head High-Temperature Warning 3";
                case "W133":
                    return _errorCode + ":" + "Head High-Temperature Warning 4";
                case "W134":
                    return _errorCode + ":" + "Head High-Temperature Warning 5";
                case "W135":
                    return _errorCode + ":" + "Head Low-Temperature Warning 1";
                case "W136":
                    return _errorCode + ":" + "Head Low-Temperature Warning 2";
                case "W137":
                    return _errorCode + ":" + "Head Low-Temperature Warning 3";
                case "W138":
                    return _errorCode + ":" + "Head Low-Temperature Warning 4";
                case "W139":
                    return _errorCode + ":" + "Head Low-Temperature Warning 5";
                case "W140":
                    return _errorCode + ":" + "Laser Resonator High-Temperature Warning";
                case "W141":
                    return _errorCode + ":" + "Laser Resonator Low-Temperature Warning";
                case "W142":
                    return _errorCode + ":" + "Laser Resonator High-Temperature Warning";
                case "W143":
                    return _errorCode + ":" + "Laser Resonator Low-Temperature Warning";

                case "T000":
                    return _errorCode + ":" + "Emergency Stop/Remote interlock in use";
                case "T001":
                    return _errorCode + ":" + "Controlling Shutter";
                case "T002":
                    return _errorCode + ":" + "Trigger Inhibited";
                case "T003":
                    return _errorCode + ":" + "Marking Laser Disabled";
                case "T004":
                    return _errorCode + ":" + "Machinery Operation Mode Disabled";
                case "T005":
                    return _errorCode + ":" + "Distance Pointer ON";
                case "T006":
                    return _errorCode + ":" + "Laser Not Excited";
                case "T007":
                    return _errorCode + ":" + "LD temperature being adjusted/Laser unit waiting to start up";
                case "T008":
                    return _errorCode + ":" + "Warming Up";
                case "T009":
                    return _errorCode + ":" + "Auto-calibrating Laser";
                case "T010":
                    return _errorCode + ":" + "Oscillator temperature being adjusted";
                case "T011":
                    return _errorCode + ":" + "Contactor input OFF";

                ///---Communication Errors--///
                case "S025":
                    return _errorCode + ":" + "Checksum Error";
                case "S026":
                    return _errorCode + ":" + "Format Error";
                case "S027":
                    return _errorCode + ":" + "Command Unrecognizable Error";
                default:
                    return _errorCode + ":" + "Unknown Error";
            }
        }

        /// <summary>
        /// Response Timeout Error
        /// </summary>
        static internal void IsResponseTimeOut()
        {
            System.Windows.Threading.DispatcherTimer _dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            _dispatcherTimer.Tick += _dispatcherTimer_Tick;
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 1);  //1sec wait for Reponse
            _dispatcherTimer.Start();
        }

        static private void _dispatcherTimer_Tick(object sender, EventArgs e)
        {
            MessageBox.Show("Response Timeout");
        }
    }
}
