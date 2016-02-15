using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO.Ports;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BlockConditionsWindow.Protocol
{
    public  class BlockConditions:INotifyPropertyChanged,ICloneable
    {
        protected string HeaderToSetBlockCondition = "K2";
        protected string HeaderToRequestBlockCondition = "K3";
        protected char Delimiter = '\r';

        private string _programNo = "";
        public string ProgramNo
        {
            get { return _programNo; }
            set
            {
                int result;
                if (int.TryParse(value, out result) == true && result < 2000 && result >= 0)
                    _programNo = value;
                else
                    throw new ArgumentOutOfRangeException("Please input ProgramNo. 0000~1999");
                    //MessageBox.Show("Please input ProgramNo. 0000~1999");                
            }
        }

        private string _blockNo = "0";
        public string BlockNo
        {
            get { return _blockNo; }
            set
            {
                int result;
                if (int.TryParse(value, out result) == true && result <= 255 && result >= 0)
                    _blockNo = value;
                else
                    throw new ArgumentOutOfRangeException("Please input BlockNo. 000~255");
                    //MessageBox.Show("Please input BlockNo. 000~255");
                
            }
        }

        private string _block3DShape = "099";  //only for 2D setting
        public string Block3DShape
        {
            get { return _block3DShape; }
            set { }
        }

        private string _blockType = "030";  //now only for 2D setting
        public string BlockType
        {
            get { return _blockType; }
            set
            {
                switch (value)
                {
                    case "000":
                    case "001":
                    case "002":
                    case "003":
                    case "004":
                    case "005":
                    case "006":
                    case "007":
                    case "008":
                    case "009":
                    case "020":
                    case "030":
                    case "031":
                    case "032":
                    case "033":
                    case "034":
                    case "-01":
                    case "-02":
                    case "-03":
                    case "-04":
                        _blockType = value;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException("Invalid Value!");
                }
                NotifyPropertyChanged();
            }
        }
      
        /// <summary> Position information
        /// Properties for Position Information
        ///  (1) Block Type:000,001,009,020,30,21
        ///  -->XCoordinate,YCoordinate,ZCoordinate,SpotVariableValue,BlockAngle,
        ///     +CharacterAngle(only 000,001,030)
        ///     
        ///  (2) Block Type: 002 003
        ///  -->Center X-Coordinate, Center Y-Coordinate, Z-Coordinate, Spot Variable Value,
        ///  Arc Radius, Starting Angle, Character Angle
        ///  
        ///  (3) Block Type:004,034
        ///  -->Fixed Point x-Coordinate, Fixed Point y-Coordinate, Z-Coordinate, Spt Variable value
        ///  
        ///  (4) Block Type:005,006,032
        ///  -->Start X-coordinate, Start Y-coordinate, End Point X-coordinate,End Y-coordinate
        ///  Z-coordinate,Spot Variable Value 
        ///  + Solid Length, Pitch length, (only 006)
        ///  +(Number of dots, dot pitch (only 032,033)
        ///  
        ///  (5) Block Type:007,008
        ///  --> Center X-coordinate, Center Y-coordinate, Radius X,Radius Y,Z-coordinate,
        ///  Spot variable value,Starting Angle,Opening Angle,Block Angle
        ///  
        /// (6) Block Type: -01, -02,-04
        /// --> X-coordinate, Y-coordinate, Z-coordinate, Spot Variable value, Block Angle, Logo size(Width), Logo size(Height)
        /// 
        /// (7) -03
        /// --> X-coordinate, Y-coordinate, Z-coordinate,Spot variable value, Block Angle, Resolution,Reverse B/W
        /// Skipped dots, Concentration
        /// </summary>     
        /// 
        #region PositionInformation
        public string PositionInformation
        {
            get
            {
                var blockTypesForPositionInformation = new List<string>() { "000", "001", "009", "020", "030", "031" };
                if (blockTypesForPositionInformation.Contains(this.BlockType))
                {
                    string end = "";
                    var blockTypes1 = new List<string>() { "000", "001", "030" };
                    if (blockTypes1.Contains(this.BlockType)) end = "," + CharacterAngle;
                    return XCoordinate + "," + YCoordinate + "," + ZCoordinate + "," + SpotVariableValue + "," + BlockAngle + end;
                }

                blockTypesForPositionInformation = new List<string>() { "002", "003" };
                if (blockTypesForPositionInformation.Contains(this.BlockType))
                {
                    return CenterXCoordinate + "," + CenterYCoordinate + "," + ZCoordinate + "," + SpotVariableValue + "," +
                        ArcRadius + "," + StartingAngle + "," + CharacterAngle;
                }

                blockTypesForPositionInformation = new List<string>() { "-01", "-02", "-04" };
                if (blockTypesForPositionInformation.Contains(BlockType)) 
                    return XCoordinate + "," + YCoordinate + "," + ZCoordinate + "," + SpotVariableValue + "," + BlockAngle + "," + LogoSizeWidth + "," + LogoSizeHeight;                    

                //...........repeated work for block type
                MessageBox.Show("Invalid block type");
                return "";
            }

            set
            {
                string[] PositionInformationArray = value.Split(',');

                switch (this.BlockType)
                {
                    case "000":
                    case "001":
                    case "030":
                        XCoordinate = PositionInformationArray[0];
                        YCoordinate = PositionInformationArray[1];
                        ZCoordinate = PositionInformationArray[2];
                        SpotVariableValue = PositionInformationArray[3];
                        BlockAngle = PositionInformationArray[4];
                        CharacterAngle = PositionInformationArray[5];
                        break;
                    case "009":
                    case "020":
                    case "031":
                        XCoordinate = PositionInformationArray[0];
                        YCoordinate = PositionInformationArray[1];
                        ZCoordinate = PositionInformationArray[2];
                        SpotVariableValue = PositionInformationArray[3];
                        BlockAngle = PositionInformationArray[4];
                        break;
                    case "002":
                    case "003":
                        CenterXCoordinate = PositionInformationArray[0];
                        CenterYCoordinate = PositionInformationArray[1];
                        ZCoordinate = PositionInformationArray[2];
                        SpotVariableValue = PositionInformationArray[3];
                        ArcRadius = PositionInformationArray[4];
                        StartingAngle = PositionInformationArray[5];
                        CharacterAngle = PositionInformationArray[6];
                        break;

                    case "-01":
                    case "-02":
                    case "-03":
                        XCoordinate = PositionInformationArray[0];
                        YCoordinate = PositionInformationArray[1];
                        ZCoordinate = PositionInformationArray[2];
                        SpotVariableValue = PositionInformationArray[3];
                        BlockAngle = PositionInformationArray[4];
                        LogoSizeWidth = PositionInformationArray[5];
                        LogoSizeHeight = PositionInformationArray[6];
                        break;

                    default:
                        MessageBox.Show("Invalid block type");
                        break;
                }
            }
        }

        private string _xCoordinate = "Binding";
        public string XCoordinate
        {
            get { return _xCoordinate; }
            set { _xCoordinate = value;
                NotifyPropertyChanged();
            }
        }

        private string _yCoordinate = "Binding";
        public string YCoordinate
        {
            get { return _yCoordinate; }
            set { _yCoordinate = value;
                NotifyPropertyChanged();
            }
        }

        private string _zCoordinate = "Binding";
        public string ZCoordinate
        {
            get { return _zCoordinate; }
            set { _zCoordinate = value;
            NotifyPropertyChanged();
            }
        }

        private string _spotVariableValue = "Binding";
        public string SpotVariableValue
        {
            get { return _spotVariableValue; }
            set { _spotVariableValue = value; NotifyPropertyChanged(); }
        }

        private string _blockAngle = "Binding";
        public string BlockAngle
        {
            get { return _blockAngle; }
            set { _blockAngle = value; NotifyPropertyChanged(); }
        }

        private string _characterAngle = "Binding";
        public string CharacterAngle
        {
            get { return _characterAngle; }
            set { _characterAngle = value; NotifyPropertyChanged(); }
        }

        private string _centerXCoordinate = "Binding";
        public string CenterXCoordinate
        {
            get { return _centerXCoordinate; }
            set { _centerXCoordinate = value; NotifyPropertyChanged(); }
        }

        private string _centerYcoordinate = "Binding";
        public string CenterYCoordinate
        {
            get { return _centerYcoordinate; }
            set { _centerYcoordinate = value; NotifyPropertyChanged(); }
        }

        private string _arcRadius = "Binding";
        public string ArcRadius
        {
            get { return _arcRadius; }
            set { _arcRadius = value; NotifyPropertyChanged(); }
        }

        private string _startingAngle = "Binding";
        public string StartingAngle
        {
            get { return _startingAngle; }
            set { _startingAngle = value; NotifyPropertyChanged(); }
        }

        private string _radiusX = "Binding";
        public string RadiusX
        {
            get { return _radiusX; }
            set { _radiusX = value; NotifyPropertyChanged(); }
        }

        private string _radiusY = "Binding";
        public string RadiusY
        {
            get { return _radiusY; }
            set { _radiusY = value; NotifyPropertyChanged(); }
        }

        private string _logoSizeWidth = "Binding";
        public string LogoSizeWidth
        {
            get { return _logoSizeWidth; }
            set { _logoSizeWidth = value; NotifyPropertyChanged(); }
        }

        private string _logoSizeHeight = "Binding";
        public string LogoSizeHeight
        {
            get { return _logoSizeHeight; }
            set { _logoSizeHeight = value; NotifyPropertyChanged(); }
        }

        private string _resolution = "Binding";
        public string Resoulution
        {
            get { return _resolution; }
            set { _resolution = value; NotifyPropertyChanged(); }
        }

        private string _reverseBW = "Binding";
        public string ReverseBW
        {
            get { return _reverseBW; }
            set { _resolution = value; NotifyPropertyChanged(); }
        }

        private string _skippedDots = "Binding";
        public string SkippedDots
        {
            get { return _skippedDots; }
            set { _skippedDots = value; NotifyPropertyChanged(); }
        }

        private string _concentration;
        public string Concentration
        {
            get { return _concentration; }
            set { _concentration = value; NotifyPropertyChanged(); }
        }
        #endregion
 
        #region Speed Information for MD-V9900
        public string SpeedInformation
        {
            get
            {
                string end;
                var blockTypes = new List<string>() { "030", "031", "032", "033", "034" };

                if (blockTypes.Contains(BlockType)) end = "," + NumberOfMultiPunchers + "," + MultipunchTime;
                else end = "";

                return MarkingFlag + "," + Approach + "," + ApproachBetweenCharacters + "," + FixedValueK2 + "," + ScanSpeed + "," +
                    MarkingPower + "," + QSwitchFrequency + "," + InitialPulseApplicationValue + "," + InitialPulseApplicationTime + end;
            }
            set
            {
                string[] SpeedInformationArray = value.Split(',');
                MarkingFlag = SpeedInformationArray[0];
                Approach = SpeedInformationArray[1];
                ApproachBetweenCharacters = SpeedInformationArray[2];
                ScanSpeed = SpeedInformationArray[4];
                MarkingPower = SpeedInformationArray[5];
                QSwitchFrequency = SpeedInformationArray[6];
                InitialPulseApplicationValue = SpeedInformationArray[7];
                InitialPulseApplicationTime = SpeedInformationArray[8];

                switch (BlockType)
                {
                    case "030":
                    case "031":
                    case "032":
                    case "033":
                    case "034":
                        NumberOfMultiPunchers = SpeedInformationArray[9];
                        MultipunchTime = SpeedInformationArray[10];
                        break;
                    default:
                        break;
                }
                NotifyPropertyChanged();
            }
        }

        private string _markingFlag = "Binding";
        public string MarkingFlag
        {
            get { return _markingFlag; }
            set { _markingFlag = value; NotifyPropertyChanged(); }
        }

        private string _approach = "Binding";
        public string Approach
        {
            get { return _approach; }
            set { _approach = value; NotifyPropertyChanged(); }
        }

        private string _approachBetweenCharacters = "Binding";
        public string ApproachBetweenCharacters
        {
            get { return _approachBetweenCharacters; }
            set { _approachBetweenCharacters = value; NotifyPropertyChanged(); }
        }

        private string _fixedValueK2 = "00000";
        public string FixedValueK2
        {
            get { return _fixedValueK2; }
            //    set { _fixedValueK2 = value; }
        }

        private string _scanSpeed = "Binding";
        public string ScanSpeed
        {
            get { return _scanSpeed; }
            set { _scanSpeed = value; NotifyPropertyChanged(); }
        }

        private string _markingPower = "Binding";
        public string MarkingPower
        {
            get { return _markingPower; }
            set { _markingPower = value; NotifyPropertyChanged(); }
        }

        private string _qSwitchFrequency = "Binding";
        public string QSwitchFrequency
        {
            get { return _qSwitchFrequency; }
            set { _qSwitchFrequency = value; NotifyPropertyChanged(); }
        }

        private string _initialPulseApplicationValue = "Binding";
        public string InitialPulseApplicationValue
        {
            get { return _initialPulseApplicationValue; }
            set { _initialPulseApplicationValue = value; NotifyPropertyChanged(); }
        }

        private string _initialPulseApplicationTime = "Binding";
        public string InitialPulseApplicationTime
        {
            get { return _initialPulseApplicationTime; }
            set { _initialPulseApplicationTime = value; NotifyPropertyChanged(); }
        }

        private string _numberOfMultiPunchers = "Binding";
        public string NumberOfMultiPunchers
        {
            get { return _numberOfMultiPunchers; }
            set { _numberOfMultiPunchers = value; NotifyPropertyChanged(); }
        }

        private string _multipunchTime = "Binding";
        public string MultipunchTime
        {
            get { return _multipunchTime; }
            set { _multipunchTime = value; NotifyPropertyChanged(); }
        }

        #endregion

        /// <summary>
        /// Size information
        /// </summary>

        private string _sizeInformation="";
        public string SizeInformation
        {
            get
            {
                switch (BlockType)
                {
                    case "000":
                    case "001":
                    case "002":
                    case "003":
                        return "," + SizeInformation_1;
                    case "009":
                        return "," + SizeInformation_2;
                    case "030":
                        return "," + SizeInformation_4;
                    default:
                        return _sizeInformation;
                }
            }
            set
            {
                switch (BlockType)
                {
                    case "000":
                    case "001":
                    case "002":
                    case "003":
                        SizeInformation_1 = value;
                        break;
                    case "009":
                        SizeInformation_2 = value;
                        break;
                    case "030":
                        SizeInformation_4 = value;
                        break;
                    default:                        
                        break;
                }
                _sizeInformation = value;
            }
        }
       
        /// <summary>   
        ///   Size information of block condition setting command K2
        /// </summary>
        /// 
        #region (1) Block Type:000,001,002,003
        private string _sizeInformation_1;     
        public string SizeInformation_1
        {
            get
            {
                return LineType + "," + FontNo + "," + CharacterHeight + "," + CharacterWidth + "," +
                    SkipCross + "," + NumberOfLine + "," + ThickLineWidth + "," + TargetOfQuickChangeOfCharacter + ","
                    + RegularPitchLayoutFlag + "," + CharacterPitch_FullLength_PitchAngle_OpeningAngle;
            }
            set
            {
                _sizeInformation_1 = value;
                string[] SizeInformationArray = _sizeInformation_1.Split(',');
                LineType = SizeInformationArray[0];
                FontNo = SizeInformationArray[1];
                CharacterHeight = SizeInformationArray[2];
                CharacterWidth = SizeInformationArray[3];
                SkipCross = SizeInformationArray[4];
                NumberOfLine = SizeInformationArray[5];
                ThickLineWidth = SizeInformationArray[6];
                TargetOfQuickChangeOfCharacter = SizeInformationArray[7];
                RegularPitchLayoutFlag = SizeInformationArray[8];
                CharacterPitch_FullLength_PitchAngle_OpeningAngle = SizeInformationArray[9];
            }
        }

        private string _lineType = "Binding";
        public string LineType
        {
            get { return _lineType; }
            set { _lineType = value; NotifyPropertyChanged(); }
        }

        private string _fontNo = "Binding";
        public string FontNo
        {
            get { return _fontNo; }
            set { _fontNo = value; NotifyPropertyChanged(); }
        }

        private string _characterHeight = "Binding";
        public string CharacterHeight
        {
            get { return _characterHeight; }
            set { _characterHeight = value; NotifyPropertyChanged(); }
        }

        private string _characterWidth = "Binding";
        public string CharacterWidth
        {
            get { return _characterWidth; }
            set { _characterWidth = value; NotifyPropertyChanged(); }
        }

        private string _skipCross = "Binding";
        public string SkipCross
        {
            get { return _skipCross; }
            set { _skipCross = value; NotifyPropertyChanged(); }
        }

        private string _numberOfLine = "Binding";
        public string NumberOfLine
        {
            get { return _numberOfLine; }
            set { _numberOfLine = value; NotifyPropertyChanged(); }
        }

        private string _thickLineWidth = "Binding";
        public string ThickLineWidth
        {
            get { return _thickLineWidth; }
            set { _thickLineWidth = value; NotifyPropertyChanged(); }
        }

        private string _targetOfQuickChangeOfCharacter = "Binding";
        public string TargetOfQuickChangeOfCharacter
        {
            get { return _targetOfQuickChangeOfCharacter; }
            set { _targetOfQuickChangeOfCharacter = value; NotifyPropertyChanged(); }
        }

        private string _regularPitchLayoutFlag = "Binding";
        public string RegularPitchLayoutFlag
        {
            get { return _regularPitchLayoutFlag; }
            set { _regularPitchLayoutFlag = value; NotifyPropertyChanged(); }
        }

        private string _characterPitch_FullLength_PitchAngle_OpeningAngle = "Binding";
        public string CharacterPitch_FullLength_PitchAngle_OpeningAngle
        {
            get { return _characterPitch_FullLength_PitchAngle_OpeningAngle; }
            set { _characterPitch_FullLength_PitchAngle_OpeningAngle = value; NotifyPropertyChanged(); }
        }
        #endregion

        #region (2) Block Type "009" (Barcode, 2D code)

        private string _sizeInformation_2;        
        public string SizeInformation_2
        {
            get
            {
                string additions = "";
                List<string> codeTypes = new List<string>{"07", "08", "09", "10"};
                if (codeTypes.Contains(CodeType))
                    additions = "," + CellFineAdjustmentValue + "," + ScanSpeedFineAdjustmentValue + "," + MarkingPowerFineAdjustmentValue + "," + QswitchFrequencyAdjustmentValue;

                return CodeType + "," + QuietZoneWidth + "," + BarcodeHeight + "," + QRmode_CheckDigit + "," + QRPasswordValid_Invalid + ","
                  + QRPassword + "," + ErrorCorrectionRate + "," + MarkWidth + "," + BarcodeThinLineWidth_2DCodeCellSize + "," +
                  BarcodeThick_ThinRatio_QRVersion_SymbolSize+additions;
            }
            set
            {
                _sizeInformation_2 = value;
                string[] SizeInformationArray = _sizeInformation_2.Split(',');

                CodeType = SizeInformationArray[0];
                QuietZoneWidth = SizeInformationArray[1];
                BarcodeHeight = SizeInformationArray[2];
                QRmode_CheckDigit = SizeInformationArray[3];
                QRPasswordValid_Invalid = SizeInformationArray[4];
                QRPassword = SizeInformationArray[5];
                ErrorCorrectionRate = SizeInformationArray[6];
                MarkWidth = SizeInformationArray[7];
                BarcodeThinLineWidth_2DCodeCellSize = SizeInformationArray[8];
                BarcodeThick_ThinRatio_QRVersion_SymbolSize = SizeInformationArray[9];

                List<string> codeTypes = new List<string>{ "07", "08", "09", "10" };
                if(codeTypes.Contains(CodeType))
                {
                    CellFineAdjustmentValue = SizeInformationArray[10];
                    ScanSpeedFineAdjustmentValue = SizeInformationArray[11];
                    MarkingPowerFineAdjustmentValue = SizeInformationArray[12];
                    QswitchFrequencyAdjustmentValue = SizeInformationArray[13];
                }
            }
        }

        private string _codeType = "Binding";
        public string CodeType
        {
            get { return _codeType; }
            set { _codeType = value; NotifyPropertyChanged(); }
        }

        private string _quietZoneWidth = "Binding";
        public string QuietZoneWidth
        {
            get { return _quietZoneWidth; }
            set { _quietZoneWidth = value; NotifyPropertyChanged(); }
        }

        private string _barcodeHeight = "Binding";
        public string BarcodeHeight
        {
            get { return _barcodeHeight; }
            set { _barcodeHeight = value; NotifyPropertyChanged(); }
        }

        private string _qRmode_CheckDigit = "Binding";
        public string QRmode_CheckDigit
        {
            get { return _qRmode_CheckDigit; }
            set { _qRmode_CheckDigit = value; NotifyPropertyChanged(); }
        }

        private string _qRPasswordValid_Invalid = "Binding";
        public string QRPasswordValid_Invalid
        {
            get { return _qRPasswordValid_Invalid; }
            set { _qRPasswordValid_Invalid = value; NotifyPropertyChanged(); }
        }

        private string _qRPassword = "Binding";
        public string QRPassword
        {
            get { return _qRPassword; }
            set { _qRPassword = value; NotifyPropertyChanged(); }
        }

        private string _errorCorrectionRate = "Binding";
        public string ErrorCorrectionRate
        {
            get { return _errorCorrectionRate; }
            set { _errorCorrectionRate = value; NotifyPropertyChanged(); }
        }

        private string _markWidth = "Binding";
        public string MarkWidth
        {
            get { return _markWidth; }
            set { _markWidth = value; NotifyPropertyChanged(); }
        }

        private string _barcodeThinLineWidth_2DCodeCellSize = "Binding";
        public string BarcodeThinLineWidth_2DCodeCellSize
        {
            get { return _barcodeThinLineWidth_2DCodeCellSize; }
            set { _barcodeThinLineWidth_2DCodeCellSize = value; NotifyPropertyChanged(); }
        }

        private string _barcodeThick_ThinRatio_QRVersion_SymbolSize = "Binding";
        public string BarcodeThick_ThinRatio_QRVersion_SymbolSize
        {
            get { return _barcodeThick_ThinRatio_QRVersion_SymbolSize; }
            set { _barcodeThick_ThinRatio_QRVersion_SymbolSize = value; NotifyPropertyChanged(); }
        }

        private string _cellFineAdjustmentValue = "Binding";
        public string CellFineAdjustmentValue
        {
            get { return _cellFineAdjustmentValue; }
            set { _cellFineAdjustmentValue = value; NotifyPropertyChanged(); }
        }

        private string _scanSpeedFineAdjustmentValue = "Binding";
        public string ScanSpeedFineAdjustmentValue
        {
            get { return _scanSpeedFineAdjustmentValue; }
            set { _scanSpeedFineAdjustmentValue = value; NotifyPropertyChanged(); }
        }

        private string _markingPowerFineAdjustmentValue = "Binding";
        public string MarkingPowerFineAdjustmentValue
        {
            get { return _markingPowerFineAdjustmentValue; }
            set { _markingPowerFineAdjustmentValue = value; NotifyPropertyChanged(); }
        }

        private string _qswitchFrequencyAdjustmentValue = "Binding";
        public string QswitchFrequencyAdjustmentValue
        {
            get { return _qswitchFrequencyAdjustmentValue; }
            set { _qswitchFrequencyAdjustmentValue = value; NotifyPropertyChanged(); }
        }

        #endregion

        #region (4)Block Type "030" (Dot Character)
        private string _sizeInformation_4;        
        public string SizeInformation_4
        { 
            get {
                return FontNumber + "," + Pattern + "," + VerticalDotPitch + "," + HorizontalDotPitch + "," + NumberOfMultiPunchers + "," + MultiPunchAdjustmentTime + ","
                    + EquallyDistributedFlag + "," + CharacterPitchLengthFullWidth;}
            set { 
                _sizeInformation_4 = value;
                string[] SizeInformationArray = _sizeInformation_4.Split(',');
                FontNumber = SizeInformationArray[0];
                Pattern = SizeInformationArray[1];
                VerticalDotPitch = SizeInformationArray[2];
                HorizontalDotPitch = SizeInformationArray[3];
                NumberOfMultiPunchers = SizeInformationArray[4];
                MultiPunchAdjustmentTime = SizeInformationArray[5];
                EquallyDistributedFlag = SizeInformationArray[6];
                CharacterPitchLengthFullWidth = SizeInformationArray[7];
            }
        }
        private string _fontNumber = "SizeInformation_4";
        public string FontNumber
        {
            get { return _fontNumber; }
            set { _fontNumber = value; }
        }

        private string _pattern = "SizeInformation_4";
        public string  Pattern
        {
            get { return _pattern; }
            set { _pattern = value; }
        }

        private string _verticalDotPitch = "SizeInformation_4";
        public string VerticalDotPitch
        { 
            get {return _verticalDotPitch; }
            set { _verticalDotPitch = value; }
        }

        private string _horizontalDotPitch = "SizeInformation_4";
        public string HorizontalDotPitch
        { 
            get { return _horizontalDotPitch; }
            set { _horizontalDotPitch = value; }
        }

        private string _multiPunchAdjustmentTime = "SizeInformation_4";
        public string MultiPunchAdjustmentTime
        {
            get { return _multiPunchAdjustmentTime;  }
            set { _multiPunchAdjustmentTime = value; }
        }

        private string _equallyDistributedFlag="SizeInformation_4";
        public string EquallyDistributedFlag
        {
            get { return _equallyDistributedFlag; }
            set { _equallyDistributedFlag = value; }
        }

        private string _characterPitchLengthFullWidth="SizeInformation_4";
        public string CharacterPitchLengthFullWidth
        {
            get{return _characterPitchLengthFullWidth;}
            set { _characterPitchLengthFullWidth = value; }
        }
        #endregion

        #region (6) Block Type "-01" "-02" "-04"
        #endregion

        private string _characterStringInformation = "Binding";
        public string CharacterStringInformation
        {
            get { return _characterStringInformation; }
            set { _characterStringInformation = value; NotifyPropertyChanged(); }
        }

        public string Setting
        {
            get
            {
                return Block3DShape + "," + BlockType + "," + PositionInformation + "," + SpeedInformation +  SizeInformation +","  +CharacterStringInformation;
            }
            set { }
        }

        public void SortBlockConditions(string[] ReturnedBlockConditions)
        {
            try
            {
                Block3DShape = ReturnedBlockConditions[2];
                BlockType = ReturnedBlockConditions[3];

                int CurrentIndex = 0;

                //For Position Information
                string tempPositionInformation = "";

                switch (BlockType)
                {
                    case "000":
                    case "001":
                    case "030":
                        for (int i = 4; i <= 9; i++)
                        {
                            tempPositionInformation += ReturnedBlockConditions[i] + ",";
                            CurrentIndex = i;
                        }
                        break;
                    case "009":
                    case "020":
                    case "031":
                        for (int i = 4; i <= 8; i++)
                        {
                            tempPositionInformation += ReturnedBlockConditions[i]+",";
                            CurrentIndex = i;
                        }
                        break;
                    case "-01":
                    case "-02":
                    case "-04":
                        for(int i=4;i<=10;i++)
                        {
                            tempPositionInformation += ReturnedBlockConditions[i]+ ",";
                            CurrentIndex = i;
                        }
                        break;

                    default:
                        MessageBox.Show("Invalid BlockType!");
                        break;
                }

                PositionInformation = tempPositionInformation;

                ///For Speed Information
                //Common speed information
                string tempSpeedInformation = "";
                int start = CurrentIndex + 1;
                int end = CurrentIndex + 9;
                for (int i = start; i <= end; i++)
                {
                    tempSpeedInformation += ReturnedBlockConditions[i]+",";
                    CurrentIndex = i;
                
                }
                //additional speed information for 030,031,032,033,034
                switch (BlockType)
                {
                    case "030":
                    case "031":
                    case "032":
                    case "033":
                    case "034":
                        for (int i = 0; i < 2; i++)
                            tempSpeedInformation += ReturnedBlockConditions[++CurrentIndex] + ",";
                        break;
                    default:
                        break;
                }
                SpeedInformation = tempSpeedInformation;

                ///For Size Information
                
                string tempSizeInformation = "";
                start = CurrentIndex + 1;

                switch (BlockType)
                {
                    case "000":
                    case "001":
                    case "002":
                    case "003":
                        end = CurrentIndex + 10;
                        for (int i = start; i <= end; i++)
                        {
                            tempSizeInformation += ReturnedBlockConditions[i]+",";
                            CurrentIndex = i;
                        }

                        break;
                    case "009":
                        string codeType = ReturnedBlockConditions[CurrentIndex + 1];
                        List<string> codetypes = new List<string> { "07", "08", "09", "10" };
                        end = codetypes.Contains(codeType) ? CurrentIndex+ 14 : CurrentIndex+10;  //if code type is 07-10
                        for (int i = start; i <= end; i++)
                        {
                            tempSizeInformation += ReturnedBlockConditions[i]+",";
                            CurrentIndex = i;
                        }
                        break;

                    default:
                        break;
                }
                SizeInformation = tempSizeInformation;

                List<string> blockTypes = new List<string> { "004", "005", "006", "007", "008", "032", "033", "034" };
                if (!blockTypes.Contains(BlockType))
                    CharacterStringInformation = ReturnedBlockConditions[CurrentIndex + 1];
            }

            catch (Exception ex)
            { MessageBox.Show(ex.Message); }
        }

        virtual public string DownloadBlockConditions(string ProgramNo, string BlockNo) 
        {
            return "111";
        }

        virtual public void UploadBlockConditions(string ProgramNo, string BlockNo) { }

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
