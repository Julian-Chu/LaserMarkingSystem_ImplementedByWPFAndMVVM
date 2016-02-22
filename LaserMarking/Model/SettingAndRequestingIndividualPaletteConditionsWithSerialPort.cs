using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace LaserMarking.Model
{
    class SettingAndRequestingIndividualPaletteConditionsWithSerialPort:Protocol.SettingAndRequestingIndividualPaletteConditions
    {
        SerialPort sp = new SerialPort();

        public SettingAndRequestingIndividualPaletteConditionsWithSerialPort(SerialPort sp)
        {
            this.sp = sp;
        }

        public void SettingIndividualPaletteConditions()
        {
            sp.WriteLine(SettingIndividualPaletteConditionsCommand());
        }

        public void RequestingIndividualPaletteConditions()
        {
            sp.ReadExisting();
            sp.WriteLine(RequestingIndividualPaletteConditionsCommand());
            SplitTheResponseStrings(sp.ReadExisting());
        }
    }
}
