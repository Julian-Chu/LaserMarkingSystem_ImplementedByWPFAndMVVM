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
using System.Collections.ObjectModel;
using System.IO.Ports;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BlockConditionsWindow.View
{
    /// <summary>
    /// Interaction logic for BlockConditionsWindowView.xaml
    /// </summary>
    public partial class BlockConditionsWindowView : Window
    {
        private ViewModel.BlockConditionsWindowViewModel _viewModel;
        public ViewModel.BlockConditionsWindowViewModel ViewModel
        {
            get { return _viewModel; }
            set { _viewModel = value; }
        }

        public BlockConditionsWindowView() {
            
            InitializeComponent();
            _viewModel = new BlockConditionsWindow.ViewModel.BlockConditionsWindowViewModel();
            this.DataContext = ViewModel;
        }

        public BlockConditionsWindowView(SerialPort sp)
        {
            InitializeComponent();
            _viewModel = new BlockConditionsWindow.ViewModel.BlockConditionsWindowViewModel();
            this.DataContext = ViewModel;
        }


        private void Window_Closed(object sender, EventArgs e)
        {
            ViewModel = null;
            MessageBox.Show("Test");
            this.Close();
            
        }

        
    }
}
