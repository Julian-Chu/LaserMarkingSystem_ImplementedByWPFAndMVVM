using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports; 

namespace LaserMarking.Protocol
{
    class SettingAndRequestingTitleWithSerialPort:Protocol.SettingAndRequestingTitle
    {
        SerialPort sp;

        public void SetSerialPort(SerialPort sp)
        {
            this.sp = sp;
        }

        public void SettingTitle(string ProgramNo, string TitleName)
        {
            sp.WriteLine(SetTitleCommand(ProgramNo, TitleName));
        }

        public string RequestingTitle(string ProgramNo)
        {
            sp.WriteLine(RequestTitleCommand(ProgramNo));
            string[] stringArray=sp.ReadExisting().Split(',');
            string Title=stringArray[2];
            return Title;
        }
    }
}
