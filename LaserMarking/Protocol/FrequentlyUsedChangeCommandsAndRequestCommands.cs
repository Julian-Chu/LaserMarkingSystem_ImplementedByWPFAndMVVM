using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace LaserMarking.Protocol
{
    internal abstract class FrequentlyUsedChangeCommandsAndRequestCommands
    {
        private string _programNo;
        public string ProgramNo
        {
            get { return _programNo; }
            set { _programNo = value; }
        }

        private string _blockNo;
        public string BlockNo
        {
            get { return _blockNo; }
            set { _blockNo = value; }
        }
        /// <summary>
        /// Changing And Requesting CharacterString
        /// </summary>
        #region
        private string _headerToChangeCharacterString = "C2";
        private string _headerToRequestCharacterString = "B3";

        private string _characterString;
        public string CharacterString
        {
            get { return _characterString; }
            set { _characterString = value; }
        }

        internal string CommandToChangeCharacterString
        {
            get
            {
                return _headerToChangeCharacterString + "," + ProgramNo + "," + BlockNo + "," + CharacterString + '\r';
            }
        }

        internal string CommandToRequestCharactertString
        {
            get
            {
                return _headerToRequestCharacterString + "," + ProgramNo + "," + BlockNo + '\r';
            }
        }

        internal void SplitCharacterStringReponse(string response)
        {
            string[] responseArray = response.Split(',');
            CharacterString = responseArray[2];
        }

        virtual public void ChangeCharacterString(string str)
        {
            //For example: using serialport
            //this.CharacterString = str;
            //sp.WriteLine(ChangingCharacterStringCommand());
        }

        virtual public void RequestCharacterString()
        { 
            //For example: using serialport
            //sp.WriteLine(RequestingCharactertStringCommand());
            //string response = sp.ReadExisting();
            //SplitCharacterStringReponse(response);        
        }
        #endregion

        /// <summary>
        /// Changing And Requesting 2D Block Position
        /// !!Not finished
        /// </summary>
        #region

        private string _headerToChange2DBlockPosition = "C0";
        private string _headerToRequest2DBlockPosition = "B1";
        private string _headerToChangeAll2DBlockPosition = "AG";

        private string _xCoordinate;
        public string XCoordinate
        {
            get { return _xCoordinate; }
            set { _xCoordinate = value; }
        }

        private string _yCoordinate;
        public string YCoordinate
        {
            get { return _yCoordinate; }
            set { _yCoordinate = value; }
        }

        //For Changing All 2D Block Condition
        private string _xCoordinateOffsetValue;
        public string XCoordinateOffsetValue
        {
            get { return _xCoordinate; }
            set { _xCoordinate = value; }
        }

        private string _yCoordinateOffsetValue;
        public string YCoordinateOffsetValue
        {
            get { return _yCoordinate; }
            set { _yCoordinate = value; }
        }

        private string _zCoordinateOffsetValue;
        public string ZCoordainteOffsetValue
        {
            get { return _zCoordinateOffsetValue; }
            set { _zCoordinateOffsetValue = value; }
        }

        private string _spotVariableOffsetValue;
        public string SpotVariableOffsetValue
        {
            get { return _spotVariableOffsetValue; }
            set { _spotVariableOffsetValue = value; }
        }

        internal string CommandToChange2DBlockPosition
        {
            get
            {
                return _headerToChange2DBlockPosition + "," + ProgramNo + "," + BlockNo + "," + XCoordinate + "," + YCoordinate + '\r';
            }
        }

        internal string CommandToRequest2DBlockPosition
        {
            get { return _headerToRequest2DBlockPosition + "," + ProgramNo + "," + BlockNo + '\r'; }
        }

        internal void Split2DBlockPositionResponse(string response)
        {
            string[] responseArray = response.Split(',');
            XCoordinate = responseArray[2];
            YCoordinate = responseArray[3];
        }

        internal string CommandToChangeAll2DBlockPosition
        {
            get
            {
                return _headerToChangeAll2DBlockPosition + "," + ProgramNo + "," + BlockNo + "," + XCoordinateOffsetValue + "," +
                YCoordinateOffsetValue + "," + ZCoordainteOffsetValue + "," + SpotVariableOffsetValue + '\r';
            }
        }

        public virtual void Change2DBlockPosition()
        { }

        public virtual void Request2DBlockPosition()
        { }

        public virtual void ChangeAll2DBlockPosition()
        { }
        #endregion


        private string _for2DMachineryOperationModeTypes;
        public string For2DMachinertOperationModeTypes
        {
            get { return _for2DMachineryOperationModeTypes; }
            set { _for2DMachineryOperationModeTypes = value; }
        }

        private string _for2DMachineryOperationModePositionInformation;
        public string For2DMachineryOperationModePositionInformation
        {
            get { return _for2DMachineryOperationModePositionInformation; }
            set { _for2DMachineryOperationModePositionInformation = value; }
        }
   

    }
}
