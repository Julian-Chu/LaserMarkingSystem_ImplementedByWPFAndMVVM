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
        static public UserControl PositionInformation(Model.BlockConditions BCs)
        {
            switch (BCs.BlockType)
            {
                case "":
                    return new UserControl();
                case "000":
                case "001":
                case "030":
                    return new PositionInformationUC.PositionInformation_1_1(BCs);
                case "009":
                case "020":
                case "031":
                    return new PositionInformationUC.PositionInformation_1_2(BCs);
                default:
                    return new PositionInformationUC.PositionInformation_6(BCs);
            }
        }

        static public UserControl SizeInformation(Model.BlockConditions BCs)
        {
            switch (BCs.BlockType)
            {
                case "":
                    return new UserControl();
                case "000":
                case "001":
                case "002":
                case "003":
                    return new SizeInformationUC.SizeInformation_1(BCs);                    
                case "009":                                   
                    return new SizeInformationUC.SizeInformation_2(BCs);
                default:
                    return new UserControl();
            }
            
        } 

        static public UserControl SpeedInformation(Model.BlockConditions BCs)
        {
            switch(BCs.BlockType)
            {
                case "":
                    return new UserControl();
                case "030":
                case "031":
                case "032":
                case "033":
                case "034":
                    return new SpeedInformationUC.SpeedInformation_2();
                default:
                    return new SpeedInformationUC.SpeedInformation_1();
            }
        }
    }
}
