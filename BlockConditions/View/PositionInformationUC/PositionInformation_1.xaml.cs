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
    /// Interaction logic for PositionInfromation_1.xaml
    /// </summary>
    public partial class PositionInformation_1 : UserControl
    {
        public PositionInformation_1()
        {
            InitializeComponent();
        }

        public PositionInformation_1(Model.BlockConditions blockConditionsModel)
        {
            InitializeComponent();
            //this.BCWSP = BCWSP;
            this.DataContext = blockConditionsModel;
        }
    }
}
