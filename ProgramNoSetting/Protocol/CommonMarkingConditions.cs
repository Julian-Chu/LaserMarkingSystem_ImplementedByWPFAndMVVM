using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CommonMarkingConditionsModule.Protocol
{
    public abstract class CommonMarkingConditions:INotifyPropertyChanged,ICloneable
    {
        #region Marking Condition Properties
        //IDcode
        //private string _idCode = ""; //2 bytes
        public string IDcode
        {
            get;
            set;
        }  

        public string ErrorStatus
        { get; set; }

        //Program Number
        private string _programNo = "0000"; //4 bytes
        public string ProgramNo
        {
            get { return _programNo; }
            set
            {
                int result;
                if (int.TryParse(value, out result) == true && result < 2000 && result >= 0)
                    _programNo = value;
                else
                    MessageBox.Show("Please input ProgramNo. between 0000~1999");
                NotifyPropertyChanged();
            }
        }

        //Setting type
        private string _settingType = "Binding";
        public string SettingType
        {
            get { return _settingType; }
            set
            {
                int result;
                if (int.TryParse(value, out result) == true)
                {
                    switch (result)
                    {
                        case 0:
                        case 1:
                        case 4:
                            _settingType = value;
                            break;

                        default:
                            MessageBox.Show("Please input Setting Type 0 or 1 or 4");
                            break;
                    }
                    NotifyPropertyChanged();
                }
            }
        }

        //  considering better way in this field
        internal enum Direction
        {
            negY, posX, posY, negX,
        }
        private string _movementDirection = "Binding";
        public string MovementDirection
        {
            get { return _movementDirection; }
            set
            {
                int result;
                if (int.TryParse(value, out result) == true && result <= 4 && result >= 0)
                    _movementDirection = value;
                else
                    MessageBox.Show("Please input ProgramNo. between 0-4!");
                NotifyPropertyChanged();
            }
        }

        //Fixed Value
        private const string _fixedValue = "0";
        public string FixedValue { get { return _fixedValue; } set { } }

        //Marking Direction
        private string _markingDirection="Binding";
        public string MarkingDirection
        {
            get { return _markingDirection; }
            set
            {
                int result;
                if (int.TryParse(value, out result) == true && result >= 0 && result < 8)
                { _markingDirection = value; }
                else
                    MessageBox.Show("Please input value between 0-7!");
                NotifyPropertyChanged();
            }
        }

        //Movement Condition(XY)
        /*internal enum MovementXY
        {
            Stationary, EqualSpeed, Encoder,
        }*/
        private string _movementConditionXY = "Binding";
        public string MovementConditionXY
        {
            get { return _movementConditionXY; }
            set
            {
                int result;
                if (int.TryParse(value, out result) == true && result >= 0 && result < 4)
                { _movementConditionXY = value; }
                else
                    MessageBox.Show("Please input value between 0-3!");
                NotifyPropertyChanged();
            }
        }

        //Movement Condition(Z) for MDV-9900
        /* internal enum MovementZ
         { Stationary=0, Selection = 3, Analog = 4, Strobe = 5 }*/

        private string _movementConditionZ = "Binding";
        public string MovementConditionZ
        {
            get { return _movementConditionZ; }
            set
            {
                int result;
                if (int.TryParse(value, out result) == true)
                {
                    switch (result)
                    {
                        case 0:
                        case 3:
                        case 4:
                        case 5:
                            _movementConditionZ = value;
                            break;
                        default:
                            MessageBox.Show("Please input value between 0(Stationary),3(Selection),4(Analog),5(Strobe)!");
                            break;
                    }
                    NotifyPropertyChanged();
                }
            }
        }

        /// %% Marking Time/Line Speed/Maximum Line Speed
        /// the range of input value depends on the machine and Movement XY & Z
        /// MD-V9900:  0.01~300.0 (Marking Time)
        ///            0.1 ~ 4000.0 (line speed and max line speed)

        private string _markingTime = "Binding";
        public string MarkingTime
        {
            get { return _markingTime; }
            set { 
                _markingTime = value;
                 NotifyPropertyChanged();
            }
        }

        // the range of input value depends on the machine and Movement XY & Z
        private string _triggerDelay = "Binding";
        public string TriggerDelay
        {
            get { return _triggerDelay; }
            set { 
                _triggerDelay = value;
                NotifyPropertyChanged();
            }
        }

        //validate when "Encoder" is selected as Movement Condition (XY). Otherwise, it is fixed to "0".
        private string _numberOfEncoderPulses = "0";
        public string NumberOfEncoderPulses
        {
            get { return _numberOfEncoderPulses; }
            set
            {
                int result;
                if (MovementConditionXY == "2")
                {
                    if (int.TryParse(value, out result) && result <= 2000 && result >= 10)
                        _numberOfEncoderPulses = value;
                    else
                        MessageBox.Show("Invalided value!!!");
                }
                else
                    _numberOfEncoderPulses = "0";

                NotifyPropertyChanged();
            }
        }

        //Minimum Workpiece Interval
        private string _minimumWorkpieceInterval = "0";
        public string MinimumWorkpieceInterval
        {
            get { return _minimumWorkpieceInterval; }
            set
            {
                double result;
                if (MovementConditionXY == "2")
                {
                    if (double.TryParse(value, out result) && result >= 0000.1 && result <= 6500)
                        _minimumWorkpieceInterval = value;
                    else
                        MessageBox.Show("Invalided value!!!");
                }
                else
                    _minimumWorkpieceInterval = "0";
                NotifyPropertyChanged();
            }
        }

        //Movement Marking Start Position:-060.000 to 0060.000
        private string _movementMarkingStartPosition = "Binding";
        public string MovementMarkingStartPosition
        {
            get { return _movementMarkingStartPosition; }
            set
            {
                double result;
                if (double.TryParse(value, out result) && result >= -60 && result <= 60)
                    _movementMarkingStartPosition = value;
                else
                    MessageBox.Show("Please input the value -060.000 ~ 0060.000");
                NotifyPropertyChanged();
            }
        }

        //Movement Marking End Position:-060.000 to 0060.000
        private string _movementMarkingEndPosition = "Binding";
        public string MovementMarkingEndPosition
        {
            get { return _movementMarkingEndPosition; }
            set
            {
                double result;
                if (double.TryParse(value, out result) && result >= -60 && result <= 60)
                    _movementMarkingEndPosition = value;
                else
                    MessageBox.Show("Please input the value -060.000 ~ 0060.000");
                NotifyPropertyChanged();
            }
        }

        private const string _fixedValue2 = "00";
        public string FixedValue2
        { get { return _fixedValue2; } set { } }

        //ContMarkRept
        private string _contMarkRept="Binding";
        public string ContMarkRept
        {
            get { return _contMarkRept; }
            set
            {
                int result;
                if (int.TryParse(value, out result) && result >= 0 && result <= 65535)
                    _contMarkRept = value;
                NotifyPropertyChanged();
            }
        }

        //ContMarkInterval
        private string _contMarkInterval = "Binding";
        public string ContMarkInterval
        {
            get { return _contMarkInterval; }
            set
            {
                double result;
                if (double.TryParse(value, out result) && result >= 0 && result <= 1200)
                    _contMarkInterval = value;
                NotifyPropertyChanged();
            }
        }

        //DistancePointerPosition
        private string _distancePointerPosition = "Binding";
        public string DistancePointerPosition
        {
            get { return _distancePointerPosition; }
            set
            {
                double result;
                if (double.TryParse(value, out result) && result >= -21 && result <= 21)
                    _distancePointerPosition = value;
                NotifyPropertyChanged();
            }
        }

        //ApproachScanSpeed
        //0 : Uses the scan speed specified with block conditions for approach scan speed.
        //Values other than 0:Scans the approach at the specified speed.
        private string _approachScanSpeed = "Binding";
        public string ApproachScanSpeed
        {
            get { return _approachScanSpeed; }
            set
            {
                int result;
                if (int.TryParse(value, out result) && result >= 0 && result <= 4000)
                    _approachScanSpeed = value;
                NotifyPropertyChanged();
            }
        }

        //Optimized Scan Speed
        private string _optimizedScanSpeed = "00000";
        public string OptimizedScanSpeed
        {
            get { return _optimizedScanSpeed; }
            set { }
        }

        //Scan Optimization Flag
        private string _scanOptimizationFlag = "2";
        public string ScanOptimizationFlag
        {
            get { return _scanOptimizationFlag; }
            set { }
        }

        //Marking Order Flag
        private string _markingOrderFlag = "Binding";
        public string MarkingOrderFlag
        {
            get { return _markingOrderFlag; }
            set
            {
                int result;
                if (int.TryParse(value, out result) && result >= 0 && result <= 3)
                    _markingOrderFlag = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        public string SettingToLMController
        {
            //use K0 command
            get {return SettingType+","+MovementDirection+","+FixedValue+","+MarkingDirection+","+MovementConditionXY+","+MovementConditionZ+","+MarkingTime+","+
                TriggerDelay+","+ NumberOfEncoderPulses+","+MinimumWorkpieceInterval+","+MovementMarkingStartPosition+","
                +MovementMarkingEndPosition+","+FixedValue2+","+ContMarkRept+","+ContMarkInterval+","+
                DistancePointerPosition+","+ApproachScanSpeed+","+OptimizedScanSpeed+","+ScanOptimizationFlag+","+MarkingOrderFlag+'\r';}
        }

        public string SettingFromLMController // Split and Sort the response
        {
            //use K1 command,  if error exists, will be blocked by method.
            set 
            {
                string response = value;
                string[] responseArray = response.Split(',', '\r');
                IDcode = responseArray[0];
                ErrorStatus = responseArray[1];
                SettingType = responseArray[2];
                MovementDirection = responseArray[3];
                FixedValue = responseArray[4];
                MarkingDirection = responseArray[5];
                MovementConditionXY = responseArray[6];
                MovementConditionZ = responseArray[7];
                MarkingTime = responseArray[8];
                TriggerDelay = responseArray[9];
                NumberOfEncoderPulses = responseArray[10];
                MinimumWorkpieceInterval = responseArray[11];
                MovementMarkingStartPosition = responseArray[12];
                MovementMarkingEndPosition = responseArray[13];
                FixedValue2 = responseArray[14];
                ContMarkRept = responseArray[15];
                ContMarkInterval = responseArray[16];
                DistancePointerPosition = responseArray[17];
                ApproachScanSpeed = responseArray[18];
                OptimizedScanSpeed = responseArray[19];
                ScanOptimizationFlag = responseArray[20];
                MarkingOrderFlag = responseArray[21];
            }
        }
        protected string HeaderToSetCommonMarkingConditionsInLM = "KO";
        protected char Delimiter = '\r';
        protected string HeaderToRequestCommonMarkingConditionsFromLM = "K1";

        virtual public void DownloadMarkingConditions(string ProgramNo) {  }

        virtual public void UploadMarkingConditions(string ProgramNo) { }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public Object Clone()
        {
            return (Object)this.MemberwiseClone();
        }
    }
}
