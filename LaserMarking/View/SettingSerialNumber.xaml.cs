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
using LaserMarking.Protocol;

namespace LaserMarking.View
{
    /// <summary>
    /// Interaction logic for SettingSerialNumber.xaml
    /// </summary>
    public partial class SettingSerialNumber : Window
    {
        SerialNumberForASG serialNumberForASG; //binding the xaml to the class to save the properties

        private string _sN1pass2MainWin;
        public string SN1pass2MainWin
        {
            get { return _sN1pass2MainWin; }
            set { _sN1pass2MainWin = value; }
        }

        private string _sN2pass2MainWin;
        public string SN2pass2MainWin
        {
            get { return _sN2pass2MainWin; }
            set { _sN2pass2MainWin = value; }
        }
        
        public SettingSerialNumber(string serialNumber1, string serialNumber2)
        {
            InitializeComponent();
            this.TBSerialNumber1.Text = serialNumber1;
            this.TBSerialNumber2.Text = serialNumber2;
        } 

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //TBSerialNumber1.Text = SerialNumber1;
            string serialNumber1 = this.TBSerialNumber1.Text;
            string serialNumber2 = this.TBSerialNumber2.Text;
            serialNumberForASG = new SerialNumberForASG(serialNumber1,serialNumber2);  //Construct the SerialNumber class by passing 2 serialnumbers
            serialNumberForASG.GeneratePropertyFromSerialNumber();
            
            this.DataContext = serialNumberForASG;
            this.TBSerialNumber1.Text=serialNumber1;
            this.TBSerialNumber2.Text = serialNumber2;
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show(serialNumberForASG.Project.ToString());
        }

        private void TBSerialNumber1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
            MessageBox.Show(serialNumberForASG.SerialNumber1.ToString());
        }


        private void GenerateNewSN_Click(object sender, RoutedEventArgs e)
        {
            serialNumberForASG.GenerateNewSerialNumber();
            TBSerialNumber1.Text = serialNumberForASG.SerialNumber1;
            TBSerialNumber2.Text = serialNumberForASG.SerialNumber2;
        }

        private void SettingBTN_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            SN1pass2MainWin = TBSerialNumber1.Text;
            SN2pass2MainWin = TBSerialNumber2.Text;
        }
    }
}
