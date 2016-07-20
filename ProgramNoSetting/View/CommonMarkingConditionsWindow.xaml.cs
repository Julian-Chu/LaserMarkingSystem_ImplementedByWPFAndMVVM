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
using System.Windows.Shapes;
using System.IO.Ports;

namespace CommonMarkingConditionsModule.View
{
    /// <summary>
    /// Interaction logic for CommonMarkingConditionsModuleWindow.xaml
    /// </summary>
    public partial class CommonMarkingConditionsWindow : Window
    {
        private ViewModel.CommonMarkingConditionsWindow_ViewModel _viewModel;
        public ViewModel.CommonMarkingConditionsWindow_ViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        public CommonMarkingConditionsWindow()
        {
            InitializeComponent();
            _viewModel = new ViewModel.CommonMarkingConditionsWindow_ViewModel();
            this.DataContext = ViewModel;
        }

        public CommonMarkingConditionsWindow(string CurrentProgramNo,SerialPort sp)
        {
            InitializeComponent();
            _viewModel = new ViewModel.CommonMarkingConditionsWindow_ViewModel();
            this.DataContext = ViewModel;
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void DownloadFromController_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Continue data downloading from controller", "Confirm Window", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                //_commonMarkingConditionsWithSerialPort.DownloadMarkingConditions(TBProgramNumber.Text);                
            }
        }

    }
}
