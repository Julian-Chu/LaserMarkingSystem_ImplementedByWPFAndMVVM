using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BlockConditionsWindow.View.PositionInformationUC
{
    /// <summary>
    /// Interaction logic for PositionInformation_3.xaml
    /// </summary>
    public partial class PositionInformation_3 : UserControl
    {
        public PositionInformation_3()
        {
            InitializeComponent();
        }

        public PositionInformation_3(Model.BlockConditionsWithSerialPort BCWSP)
        {
            InitializeComponent();            
            DataContext = BCWSP;
        }
    }
}
