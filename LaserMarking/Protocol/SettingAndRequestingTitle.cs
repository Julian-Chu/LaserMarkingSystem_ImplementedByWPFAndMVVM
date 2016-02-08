using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LaserMarking.Protocol
{
    public abstract class SettingAndRequestingTitle
    {
        public string SetTitleCommand(string ProgramNo, string TitleName)
        {
            return "G4" + "," + ProgramNo + "," + TitleName + '\r';
        }

        public string RequestTitleCommand(string ProgramNo)
        {
            return "F5" + "," + ProgramNo + '\r';
        }
    }
}
