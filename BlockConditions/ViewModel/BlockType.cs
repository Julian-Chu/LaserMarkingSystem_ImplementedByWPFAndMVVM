using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlockConditionsWindow.ViewModel
{

        public class BlockType
        {
            private string _type;
            public string Type
            {
                get { return _type; }
                set { }
            }

            private string _idCode;
            public string BlockTypeIDcode
            {
                get { return _idCode; }
                set { }
            }

            public BlockType(string Type, string IDcode)
            {
                this._type = Type;
                this._idCode = IDcode;
            }

            public override string ToString()
            {
                return Type;
            }
        }
 
}
