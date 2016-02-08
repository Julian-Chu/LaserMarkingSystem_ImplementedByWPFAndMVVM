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

namespace ProgramNoSetting.Controller
{
    public class CommonMarkingConditionsWithSerialPort : Protocol.CommonMarkingConditions
    {
        SerialPort sp;

        public void SetSerialPort(SerialPort sp)
        {
            this.sp = sp;
        }

        public CommonMarkingConditionsWithSerialPort()
        { }

        public CommonMarkingConditionsWithSerialPort(SerialPort sp)
        {
            this.sp = sp;

            // length of MarkingCondition by command K0 and K1
            string[] ConditionArray = new string[22];

            for (int i = 0; i < ConditionArray.Length; i++)
                ConditionArray[i] = "";

            ///connect model and view model
            ///collect all properties in an array
            ConditionArray[(int)Properties.IDcode] = this.IDcode;
            ConditionArray[(int)Properties.ProgramNumber] = this.ProgramNo;
            ConditionArray[(int)Properties.SettingType] = this.SettingType;
            ConditionArray[(int)Properties.MovementDirectXY] = this.MovementDirection;
            ConditionArray[(int)Properties.FixedValue] = this.FixedValue;
            ConditionArray[(int)Properties.MarkingDirection] = this.MarkingDirection;
            ConditionArray[(int)Properties.MovementConditionXY] = this.MovementConditionXY;
            ConditionArray[(int)Properties.MovementConditionZ] = this.MovementConditionZ;
            ConditionArray[(int)Properties.MarkingTime] = this.MarkingTime;
            ConditionArray[(int)Properties.TriggerDelay] = this.TriggerDelay;
            ConditionArray[(int)Properties.NumberOfEncoderPulses] = this.NumberOfEncoderPulses;
            ConditionArray[(int)Properties.MinWorkpcsInterval] = this.MinimumWorkpieceInterval;
            ConditionArray[(int)Properties.MovementMarkingStartPosition] = this.MovementMarkingStartPosition;
            ConditionArray[(int)Properties.MovementMarkingEndPosition] = this.MovementMarkingEndPosition;
            ConditionArray[(int)Properties.FixedValue2] = this.FixedValue2;
            ConditionArray[(int)Properties.ContMarkRept] = this.ContMarkRept;
            ConditionArray[(int)Properties.ContMarkInterval] = this.ContMarkInterval;
            ConditionArray[(int)Properties.DistancePointerPosition] = this.DistancePointerPosition;
            ConditionArray[(int)Properties.ApproachScanSpeed] = this.ApproachScanSpeed;
            ConditionArray[(int)Properties.OptimiziedScanSpeed] = this.OptimizedScanSpeed;
            ConditionArray[(int)Properties.ScanOptimizationFlag] = this.ScanOptimizationFlag;
        }
        string[] ConditionArray = new string[22];

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
                string _command = HeaderToRequestCommonMarkingConditions + "," + ProgramNo  + Delimiter;
                sp.Write(_command);  //(K1,xxxx\r)
                Thread.Sleep(250);

                // string[] Commands = sp.ReadExisting().Split(delimiterChars);
                string _responseFromPort = sp.ReadExisting();
                Thread.Sleep(250);
                
                string[] responses = _responseFromPort.Split(delimiterString, System.StringSplitOptions.RemoveEmptyEntries);

                if (responses[1] == "0") //no error
                {
                    this.SettingFromLaserMakringController = _responseFromPort;
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
                sp.WriteLine(HeaderToSetCommonMarkingConditions + "," +ProgramNo+","+ SettingToLaserMarkingController);  //(K0,parameters...\r)
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
