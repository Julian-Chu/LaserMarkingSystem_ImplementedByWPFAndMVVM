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

namespace BlockConditions.View.SizeInformationUC
{
    /// <summary>
    /// Interaction logic for SizeInformation_2.xaml
    /// </summary>
    public partial class SizeInformation_2 : UserControl
    {
        public SizeInformation_2()
        {
            InitializeComponent();
        }

        public SizeInformation_2(Controller.BlockConditionsWithSerialPort BCWSP)
        {
            InitializeComponent();
            DataContext = BCWSP;
        }
    }
}
