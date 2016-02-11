using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BlockConditionsWindow.View
{
    /// <summary>
    /// simple factory to choose  PositionInformation and SizeInformation to implement.
    /// </summary>
    class UserControlSimpleFactory
    {
        static public UserControl PositionInformation(string blockType)
        {
            switch (blockType)
            {
                case "":
                    return new UserControl();
                case "000":
                case "001":
                case "030":
                    return new PositionInformationUC.PositionInformation_1();
                case "009":
                case "020":
                case "031":
                    return new PositionInformationUC.PositionInformation_2();
                default:
                    return new PositionInformationUC.PositionInformation_3();
            }
        }

        static public UserControl SizeInformation(string blockType)
        {
            switch (blockType)
            {
                case "":
                    return new UserControl();
                case "000":
                case "001":
                case "002":
                case "003":
                    return new SizeInformationUC.SizeInformation_1();                    
                case "009":                                   
                    return new SizeInformationUC.SizeInformation_2();
                default:
                    return new UserControl();
            }
        } 
    }
}
