using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace LaserMarking.Model
{
    class FrequentlyUsedChangeCommandsAndRequestCommandsWithSerialport:Protocol.FrequentlyUsedChangeCommandsAndRequestCommands
    {
        SerialPort sp;

        public FrequentlyUsedChangeCommandsAndRequestCommandsWithSerialport(SerialPort sp)
        {
            this.sp = sp;
        }

        public override void ChangeCharacterString(string str)
        {
            this.CharacterString = str;            
            sp.WriteLine(CommandToChangeCharacterString);
        }

        public override void RequestCharacterString()
        {
            sp.WriteLine(CommandToRequestCharactertString);
            string response = sp.ReadExisting();
            SplitCharacterStringReponse(response);
        }

        
    }
}
