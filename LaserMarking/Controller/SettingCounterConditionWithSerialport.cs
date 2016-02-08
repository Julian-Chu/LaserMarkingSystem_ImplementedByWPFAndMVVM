using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace LaserMarking.Protocol
{
    class SettingCounterConditionWithSerialport:Protocol.SettingCounterConditions
    {
        SerialPort sp;

        public void SetSerialPort(SerialPort sp)
        {
            this.sp = sp;
        }

        public void SettingCounterCondtion()
        {
            sp.WriteLine(CommandToSetCounterCondtion);
        }

        public void RequestingCounterCondition()
        {
            sp.WriteLine(CommandToRequestCounterCondition);
            SplitTheCounterConditionArray(sp.ReadExisting());
        }


        
    }
}
