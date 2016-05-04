using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;


namespace LaserMarking.ViewModel
{
    public class LaserMarkingWindowViewModel
    {
        #region <--Property and Field-->
        public int ProgramNumber{get;set;}

        private System.Windows.Controls.Grid trayGrid;
        public System.Windows.Controls.Grid TrayGrid
        {
            get { return trayGrid; }
            set { trayGrid = value; }
        }
        #endregion

        #region <--Constructor-->
        public LaserMarkingWindowViewModel()
        { }

        public LaserMarkingWindowViewModel(SerialPort sp)
        { }
        #endregion

        #region <--Methods For Buttons-->
        private void OpenSettingProgramSettingWindow()
        { }

        private void OpenSettingSerialNumberWindow()
        { }

        private void OpenSettingBlockConditionsWindow()
        { }

        private void StartMarking()
        { }
        #endregion

        #region
        public class TrayMatrix : Button
        {
            public Image BarcodeImage;
            public DockPanel dockPanel;
            public TextBox serialNumber1;  // for serial number 1
            public TextBox serialNumber2;  // for serial number 2
            public StackPanel stackPanel;

            public TrayMatrix()
            {
                dockPanel = new DockPanel();
                dockPanel.LastChildFill = true;
                dockPanel.Margin = new Thickness(2);
                dockPanel.Background = Brushes.Salmon;
                //Contruct the image box, assign file to image and set position to the dockpanel
                BarcodeImage = new Image();
                BarcodeImage.MinWidth = 20;
                BarcodeImage.Width = 40;
                BarcodeImage.Margin = new Thickness(5, 0, 0, 0);
                var uri = new Uri("pack://application:,,,/images/2DCode.png");
                BitmapImage Barcode = new BitmapImage();
                Barcode.BeginInit();
                Barcode.UriSource = uri;
                Barcode.EndInit();

                //image.Stretch = Stretch.Fill;
                BarcodeImage.Source = Barcode;
                DockPanel.SetDock(BarcodeImage, Dock.Left);
                dockPanel.Children.Add(BarcodeImage);

                stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Vertical;
                stackPanel.Background = Brushes.Transparent;
                stackPanel.Margin = new Thickness(2);
                dockPanel.Children.Add(stackPanel);

                //Construct the textbox for serial number1 and 2
                serialNumber1 = new TextBox();
                serialNumber1.MinHeight = 20;
                serialNumber1.MinWidth = 40;

                serialNumber1.Background = Brushes.LemonChiffon;
                serialNumber1.Margin = new Thickness(3);
                serialNumber1.Text = "035G122";
                serialNumber1.VerticalAlignment = VerticalAlignment.Center;
                serialNumber1.HorizontalAlignment = HorizontalAlignment.Center;
                stackPanel.Children.Add(serialNumber1);

                serialNumber2 = new TextBox();
                serialNumber2.MinHeight = 20;
                serialNumber2.MinWidth = 15;
                serialNumber2.Background = Brushes.LemonChiffon;
                serialNumber2.Margin = new Thickness(3);
                serialNumber2.Text = "143412345";
                serialNumber2.VerticalAlignment = VerticalAlignment.Center;
                serialNumber2.HorizontalAlignment = HorizontalAlignment.Center;
                stackPanel.Children.Add(serialNumber2);
            }


        }
        #endregion
    }
}
