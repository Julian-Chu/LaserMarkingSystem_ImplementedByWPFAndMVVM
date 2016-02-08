using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserMarking.Protocol
{
    public abstract class CommonPaletteConditions
    {
        private string HeaderToSetPalette = "G8";
        private string HeaderToRequestPalette = "F9";
        private string _programNo="";
        public string ProgramNo
        {
            get { return _programNo; }
            set { _programNo = value; }
        }

        private string _scanDirection="";
        public string ScanDirection
        {
            get { return _scanDirection; }
            set { _scanDirection = value; }
        }

        private string _numberOfColumns="";
        public string NumberOfColumns
        {
            get { return _numberOfColumns; }
            set { NumberOfColumns=value; }
        }

        private string _numberOfRows="";
        public string NumberOfRows
        {
            get { return _numberOfRows; }
            set { _numberOfRows = value; }
        }

        private string _columnPitch="";
        public string ColumnPitch
        {
            get { return _columnPitch; }
            set { _columnPitch = value; }
        }

        private string _rowPitch="";
        public string RowPitch
        {
            get { return _rowPitch; }
            set { _rowPitch = value; }
        }

        private string _markingStartPlaetteNo="";
        public string MarkingStartPaletteNo
        {
            get { return _markingStartPlaetteNo; }
            set { _markingStartPlaetteNo = value; }
        }

        private string _referencePositionXcoordinate="";
        public string ReferencePositionXcoordinate
        {
            get { return _referencePositionXcoordinate; }
            set { _referencePositionXcoordinate = value; }
        }

        private string _referencePositionYcoordinate="";
        public string ReferencePositionYcoordinate
        {
            get { return _referencePositionYcoordinate; }
            set { _referencePositionYcoordinate = value; }
        }

        public string SettingCommonPaletteConditionsCommand
        {
            get
            {
                return HeaderToSetPalette + "," + ProgramNo + "," + ScanDirection + "," + NumberOfColumns + "," +
                NumberOfRows + "," + ColumnPitch + "," + RowPitch + "," + MarkingStartPaletteNo + "," +
                ReferencePositionXcoordinate + "," + ReferencePositionYcoordinate + '\r';
            }
        }

        public string CommandToRequestCommonPaletteConditions
        {
            get
            {
                return HeaderToRequestPalette + "," + ProgramNo + '\r';
            }
        }

        public void SplitPaletteConditions(string ResponseFromController)
        {
            string[] PaletteConditionsArray = ResponseFromController.Split(',');
            ScanDirection = PaletteConditionsArray[2];
            NumberOfColumns = PaletteConditionsArray[3];
            NumberOfRows = PaletteConditionsArray[4];
            ColumnPitch = PaletteConditionsArray[5];
            RowPitch = PaletteConditionsArray[6];
            MarkingStartPaletteNo = PaletteConditionsArray[7];
            ReferencePositionXcoordinate = PaletteConditionsArray[8];
            ReferencePositionYcoordinate = PaletteConditionsArray[9];

        }


    }
}
