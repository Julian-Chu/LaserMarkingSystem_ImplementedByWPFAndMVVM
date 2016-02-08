using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserMarking.Protocol
{
    abstract class SettingAndRequestingIndividualPaletteConditions
    {
        private string SettingIndividualPaletteConditionHeader = "KW";
        private string RequestingIndividualPaletteConditionHeader = "KX";

        private string _identificationCode;
        public string IdentificationCode
        {
            get { return _identificationCode; }
            set { _identificationCode = value; }
        }

        private string _programNo;
        public string ProgramNo
        {
            get { return _programNo; }
            set { _programNo = value; }
        }

        private string _paletteNo;
        public string PaletteNo
        {
            get { return _paletteNo; }
            set { _paletteNo = value; }
        }

        private string _toMark_NotToMarkFlag;
        public string ToMark_NotToMarkFlag
        {
            get { return _toMark_NotToMarkFlag; }
            set { _toMark_NotToMarkFlag = value; }
        }

        private string _paletteNoToBeMarkedNext;
        public string PaletteNoToBeMarkedNext
        {
            get { return _paletteNoToBeMarkedNext; }
            set { _paletteNoToBeMarkedNext = value; }
        }

        private string _coordinateOffsetX;
        public string CoordinateOffsetX
        {
            get { return _coordinateOffsetX; }
            set { _coordinateOffsetX = value; }
        }

        private string _coordinateOffsetY;
        public string CoordinateOffsetY
        {
            get { return _coordinateOffsetY; }
            set { _coordinateOffsetY = value; }
        }

        private string _coordinateOffsetZ;
        public string CoordinateOffsetZ
        {
            get { return _coordinateOffsetZ; }
            set { _coordinateOffsetZ = value; }
        }

        private string _coordinateOffsetAngle;
        public string CoordinateOffsetAngle
        {
            get { return _coordinateOffsetAngle; }
            set { _coordinateOffsetAngle = value; }
        }

        
        public string SettingIndividualPaletteConditionsCommand()
        {
            return SettingIndividualPaletteConditionHeader + "," + ProgramNo + "," + PaletteNo + ","
                + ToMark_NotToMarkFlag + "," + PaletteNoToBeMarkedNext + "," + CoordinateOffsetX + "," +
                CoordinateOffsetY + "," + CoordinateOffsetZ + "," + CoordinateOffsetAngle + '\r';
        }

        public string RequestingIndividualPaletteConditionsCommand()
        {
            return RequestingIndividualPaletteConditionHeader + "," + ProgramNo + "," + PaletteNo + '\r';
        }

        public void SplitTheResponseStrings(string Response)
        {
            string[] ResponseArray = Response.Split(',');
            ProgramNo = ResponseArray[1];
            PaletteNo = ResponseArray[2];
            ToMark_NotToMarkFlag = ResponseArray[3];
            PaletteNoToBeMarkedNext = ResponseArray[4];

            CoordinateOffsetX = ResponseArray[5];
            CoordinateOffsetY = ResponseArray[6];
            CoordinateOffsetZ = ResponseArray[7];
            CoordinateOffsetAngle = ResponseArray[8];
        }

    }


    

    
}
