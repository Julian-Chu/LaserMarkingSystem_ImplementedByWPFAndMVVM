using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO.Ports;
using System.Runtime.Serialization.Formatters.Binary;
namespace LaserMarking.Protocol
{
    public abstract class Operation
    {
        private string _programNo;
        internal string ProgramNo
        {
            get { return _programNo; }
            set { _programNo = value; }
        }
  
        #region Requesting READY Status

        private string _headerToRequestREADYstatus = "RE";

        protected string CommandToRequestREADYStatus
        {
            get { return _headerToRequestREADYstatus; }
        }

        public virtual string RequestREADYStatus()
        { return null; }
        #endregion

        #region Switching program No.
        private string _headerToSwtichProgramNo = "GA";
        protected string CommandToSwitchProgramNo
        {
            get { return _headerToSwtichProgramNo+","+ProgramNo; }
        }

        public virtual string SwitchProgramNo() { return null; }
        #endregion

        #region Requesting program No.
        private string _headerToRequestProgramNo = "FE";
        protected string CommandToRequestProgramNo
        {
            get { return _headerToRequestProgramNo; }
        }

        public virtual void RequestProgramNo(){ }
        #endregion
 
        #region Marking Start and Completion
        private string _markingStartHeader = "NT";
        public string MarkingStartCommand
        {
            get { return _markingStartHeader; }
        }

        internal void MarkingCompleted(string response)
        {
            string[] responseArray = response.Split(',');
            if (responseArray[0] != "NT")
                MessageBox.Show("Error");
            else
            {
                switch(responseArray[1])
                {
                    case "0":
                        MessageBox.Show("Marking is completed!");
                        break;
                    case "1":
                        MessageBox.Show("Error");
                        break;
                    default:
                        MessageBox.Show("Unknown Error");
                        break;
                }
            }

        }

        public virtual void MarkingStart(){ }
        #endregion

        #region StoppingMarkingLaserCommand
        private string _headerToStopMarkingLaser = "LQ";

        private string _controlFlag;   //0:Cancel  1:Stop
        public string ControlFlag
        {
            get { return _controlFlag; }
            set { _controlFlag = value; }
        }

        public string CommandToStopMarkingLaser
        {
            get { return _headerToStopMarkingLaser; }
        }

        public virtual void StopMarkingLaser(){ }

        private string _headerToRequestMarkingLaserSTOPstatus = "LS";

        public string CommandToRequestMarkingLaserSTOPstatus
        {
            get { return _headerToRequestMarkingLaserSTOPstatus; }
        }

        public virtual void RequestMarkingLaserSTOPstatus() { }
        #endregion 

        #region Current Value of a counter
        //Changing current Value of a counter
        private string _headerToChangeCurrentCounterValue = "CM";
        
        private string _counterNo;
        public string CountNo
        {
            get { return _counterNo; }
            set { _counterNo = value; }
        }

        private string _countCurrentValue="";
        public string CountCurrentValue
        {
            get { return _counterNo; }
            set { CountCurrentValue = value; }
        }

        public string CommandToChangeCurrentCounterValue
        {
            get { return _headerToChangeCurrentCounterValue+","+_counterNo+","+_countCurrentValue+'\r';}
        }        

        // Requesting current Value of a counter
        private string _headerToRequestTheCurrentValue = "CN";

        private string _requiredNumber;
        public string RequireNumber
        {
            get { return _requiredNumber; }
        }

        public string CommandToRequestTheCurrentValue
        {
            get { return _headerToRequestTheCurrentValue + "," + _counterNo + "," + _requiredNumber + '\r'; }
        }

        public void RequestTheCurrenValueResponse(string response)
        {
                string[] responseArray = response.Split(',');
                CountCurrentValue = responseArray[2];            
        }
        #endregion

    }


}
