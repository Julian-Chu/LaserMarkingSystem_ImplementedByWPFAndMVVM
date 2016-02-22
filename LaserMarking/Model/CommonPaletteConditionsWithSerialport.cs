using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace LaserMarking.Model
{
    public class CommonPaletteConditionsWithSerialport:Protocol.CommonPaletteConditions
    {
        SerialPort sp;

        public void SetSerialPort(SerialPort sp)
        {
            this.sp = sp;
        }

        public CommonPaletteConditionsWithSerialport(SerialPort sp)
        {
            this.sp = sp;
        }

        public void SettingCommonPaletteConditions(string ProgramNo)
        {
            this.ProgramNo = ProgramNo;
            sp.WriteLine(SettingCommonPaletteConditionsCommand);
        }

        public void RequestingCommonPaletteConditions(string ProgramNo)
        {
            this.ProgramNo=ProgramNo;
            sp.WriteLine(CommandToRequestCommonPaletteConditions);
            SplitPaletteConditions(sp.ReadExisting());
        }
        
    }
}
