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

        private void SetProgramNo_Click(object sender, RoutedEventArgs e)
        {
            int Number;
            if (int.TryParse(TBProgramNumber.Text, out Number) && Number >= 0 && Number < 10000)
            {
                this.DialogResult = true;
                this.Close();
            }
            else
            { MessageBox.Show("Please input the value 0000~9999"); }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
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

        private void Decrement_Click(object sender, RoutedEventArgs e)
        {
            int result;
            if (int.TryParse(TBProgramNumber.Text, out result) && result >= 0 && result < 2000)
            {
                if (result - 1 < 0) return;
                else TBProgramNumber.Text = (result - 1).ToString("0000");
            }
            else
                MessageBox.Show("Please input the value between 0000~1999");

        }

        private void Increment_Click(object sender, RoutedEventArgs e)
        {
            int result;
            if (int.TryParse(TBProgramNumber.Text, out result) && result >= 0 && result < 2000)
            {
                if (result + 1 > 1999) return;
                else TBProgramNumber.Text = (result + 1).ToString("0000");
            }
            else
                MessageBox.Show("Please input the value between 0000~1999");
        }

        private void FirstProgramNo_Click(object sender, RoutedEventArgs e)
        {
            TBProgramNumber.Text = "0000";
        }

        private void LastProgramNo_Click(object sender, RoutedEventArgs e)
        {
            TBProgramNumber.Text = "1999";
        }

        private void TBProgramNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            //_commonMarkingConditionsWithSerialPort.ProgramNo = TBProgramNumber.Text;
        }
    }
}
