using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserMarking.Protocol
{
    abstract class SettingCounterConditions
    {
        protected string HeaderToSetCounterConditions = "G6";

        private string _programNo;
        public string ProgramNo
        {
            get { return _programNo; }
            set { _programNo = value; }
        }

        private string _counterNo;
        public string CounterNo
        {
            get { return _counterNo; }
            set { _counterNo = value; }
        }

        private string _stepWidth;
        public string StepWidth
        {
            get { return _stepWidth; }
            set { _stepWidth = value; }
        }

        private string _counterInitialValue;
        public string CounterInitialValue
        {
            get { return _counterInitialValue; }
            set { _counterInitialValue = value; }
        }

        private string _counterTopValue;
        public string CounterTopValue
        {
            get { return _counterTopValue; }
            set { _counterTopValue = value; }
        }

        private string _counterFinalValue;
        public string CounterFinalValue
        {
            get { return _counterFinalValue; }
            set { _counterFinalValue=value; }
        }

        private string _numberOfTimesOfCounterMarkings;
        public string NumberOfTimesOfCounterMarkings
        {
            get { return _numberOfTimesOfCounterMarkings; }
            set { _numberOfTimesOfCounterMarkings = value; }
        }

        private string _resetTiming;
        public string ResetTiming
        {
            get { return _resetTiming; }
            set { _resetTiming = value; }
        }

        private string _countTiming;
        public string CountTiming
        {
            get { return _countTiming; }
            set { _countTiming = value; }
        }

        private string _base;
        public string Base
        {
            get { return _base; }
            set { _base = value; }
        }

        public string CommandToSetCounterCondtion
        {
            get
            {
                return "G6" + "," + ProgramNo + "," + CounterNo + "," + StepWidth + "," + CounterInitialValue + "," +
                    CounterTopValue + "," + CounterFinalValue + "," + NumberOfTimesOfCounterMarkings + "," +
                    ResetTiming + "," + CountTiming + "," + Base + '\r';
            }
        }

        public string CommandToRequestCounterCondition
        {
            get
            {
                return "F7" + "," + ProgramNo + "," + CounterNo + '\r';
            }
        }

        public void SplitTheCounterConditionArray(string ResponseFromController)
        {
            string[] ConditionArray = ResponseFromController.Split(',');
            StepWidth = ConditionArray[2];
            CounterInitialValue = ConditionArray[3];
            CounterTopValue = ConditionArray[4];
            CounterFinalValue = ConditionArray[5];
            NumberOfTimesOfCounterMarkings = ConditionArray[6];
            ResetTiming = ConditionArray[7];
            CountTiming = ConditionArray[8];
            Base = ConditionArray[9];
        }
        

    }
}
