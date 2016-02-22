using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows;
using System.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CommonMarkingConditionsModule.Model
{
    public class CommonMarkingConditionsWithSerialPort : Protocol.CommonMarkingConditions
    {
        SerialPort sp;

        public void SetSerialPort(SerialPort sp)
        {
            this.sp = sp;
        }

        //constructor without Args for Testing
        public CommonMarkingConditionsWithSerialPort()  
        { }

        public CommonMarkingConditionsWithSerialPort(SerialPort sp)
        {
            this.sp = sp;


        }
     

        /// <summary>
        /// Separate the command string
        /// </summary>
        //protected char[] delimiterChars = { ' ', ',', '\t', };
        protected string[] delimiterString = new string[] { "\r", "," };

        new public void DownloadMarkingConditions(string ProgramNo)
        {
            try
            {
                sp.Close();
                sp.Open();
                string _command = HeaderToRequestCommonMarkingConditionsFromLM + "," + ProgramNo  + Delimiter;
                sp.Write(_command);  //(K1,xxxx\r)
                Thread.Sleep(250);

                // string[] Commands = sp.ReadExisting().Split(delimiterChars);
                string _responseFromPort = sp.ReadExisting();
                //Thread.Sleep(250);
                
                string[] responses = _responseFromPort.Split(delimiterString, System.StringSplitOptions.RemoveEmptyEntries);

                if (responses[1] == "0") //no error
                {
                    this.SettingFromLMController = _responseFromPort;
                }
                else
                {
                    Protocol.ErrorCode _errorCode = new Protocol.ErrorCode();
                    _errorCode.NoErrorExists(_responseFromPort);
                }
            }
            catch (System.IO.IOException ex) { throw new System.IO.IOException(ex.Message); }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                sp.Close();
            }
        }

        new public void UploadMarkingConditions(string ProgramNo)
        {
            try
            {
                sp.Close();
                sp.Open();
                sp.WriteLine(HeaderToSetCommonMarkingConditionsInLM + "," +ProgramNo+","+ SettingToLMController);  //(K0,parameters...\r)
                Thread.Sleep(250);

                string Command = sp.ReadExisting();
                Thread.Sleep(250);
                sp.Close();
                string[] Commands = Command.Split(delimiterString, System.StringSplitOptions.RemoveEmptyEntries);

                if (Commands[1] == "0") //no error
                {
                    MessageBox.Show("Upload is finished!");
                }
                else
                {
                    Protocol.ErrorCode _errorCode = new Protocol.ErrorCode();
                    _errorCode.CommunicationErrorMsg(Command);
                    string error = Commands[2] + _errorCode.CommunicationErrorMsg(Command);
                    //ErrorLog function............
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        enum Properties
        {
            IDcode,
            ProgramNumber,
            SettingType,
            MovementDirectXY,
            FixedValue,
            MarkingDirection,
            MovementConditionXY,
            MovementConditionZ,
            MarkingTime,
            TriggerDelay,
            NumberOfEncoderPulses,
            MinWorkpcsInterval,
            MovementMarkingStartPosition,
            MovementMarkingEndPosition,
            FixedValue2,
            ContMarkRept,
            ContMarkInterval,
            DistancePointerPosition,
            ApproachScanSpeed,
            OptimiziedScanSpeed,
            ScanOptimizationFlag,
            MarkingOrderFlag,
        }


       
    }
}
