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
using System.Collections.ObjectModel;
using System.Windows.Threading;
using System.Xml.Serialization;
using System.IO;

namespace LaserMarking.View
{
    /// <summary>
    /// Interaction logic for LaserMarkerWindow.xaml
    /// </summary>
    public partial class LaserMarkerWindow : Window
    {
        SerialPort sp;
        SerialPort spForTesting;
        Model.OperationWithSerialport _operationWithSerialport;        
        
        static readonly object _locker = new object();        
        TrayMatrix[] trayMatrix;        
        CommonMarkingConditionsModule.Model.CommonMarkingConditionsWithSerialPort _commonMarkingConditions;
        BlockConditionsWindow.Model.BlockConditionsWithSerialPort _blockConditions;
        List<BlockConditionsWindow.Model.BlockConditionsWithSerialPort> _blockConditionsList;

        public LaserMarkerWindow()
        {
            InitializeComponent();
            ReadSerialNumberAndProgramNoFromDefaultSetting();

            //Initial the final serial number=first serial number+35
            EndSerialNumber_2.Text = (int.Parse(StartSerialNumber_2.Text) + int.Parse(Qty.Text)-1).ToString();
            //pass the grid as the board to generate button ,textbox and image objects
            GeneratingTrayMatrix(TrayGrid);
            // spForTesting = new SerialPort("COM25", 38400, Parity.None, 8, StopBits.One);
            // spForTesting.DataReceived += spForTesting_DataReceived;                
        }

        private void ReadSerialNumberAndProgramNoFromDefaultSetting()
        {
            SerialNumber_1.Text = Properties.Settings.Default.SerialNumber1;
            StartSerialNumber_2.Text = Properties.Settings.Default.SerialNumber2;
            MainProgramNo.Text = Properties.Settings.Default.ProgramNo;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            sp = new SerialPort("COM9", 38400, Parity.None, 8, StopBits.One);
            try
            {
                //sp.Open(); 
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }

            _operationWithSerialport = new Model.OperationWithSerialport(sp, LB1);
            _commonMarkingConditions = new CommonMarkingConditionsModule.Model.CommonMarkingConditionsWithSerialPort(sp);
            _blockConditions = new BlockConditionsWindow.Model.BlockConditionsWithSerialPort(sp);

 
        }       

        void spForTesting_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort spForTesting = sender as SerialPort;
            string response=spForTesting.ReadExisting();
            if(response=="RE\r\n")
            {
                spForTesting.Close();
                spForTesting.Open();
                spForTesting.WriteLine("RE,0\r");                
            }
            MessageBox.Show(response);
        }

        async private void StartMarkingBtn_Click(object sender, RoutedEventArgs e)
        {
                        //messages in Listbox 
            #region
            /*
            #region
            if(LB1.Items.Count>15)
            { LB1.Items.RemoveAt(0); }
            LB1.Items.Add("Generating matrix");
            */
            #endregion
            // generate button matrix in old version code
            #region
            /*
            int count;
            yDim = 8;
            xDim = 5;
            buttonMatrix = new Button[yDim, xDim];
            WrapPanelArray = new WrapPanel[yDim];

            if (StackPanel1.Children.Count > 0)
            {
                count = StackPanel1.Children.Count;
                for (int i = 0; i < count; i++)
                    StackPanel1.Children.RemoveAt(0);
            }
            count = 0;
            for (int y = 0; y < yDim; y++)
            {
                WrapPanelArray[y] = new WrapPanel()
                {
                    Height = double.NaN,
                    Width = double.NaN,
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                };

                StackPanel1.Children.Add(WrapPanelArray[y]);
                for (int x = 0; x < xDim; x++)
                {
                    count += 1;
                    buttonMatrix[y, x] = new Button()
                    {
                        Width = 40,
                        Content = count.ToString(),
                        Margin = new Thickness(1, 1, 1, 1),
                    };
                    WrapPanelArray[y].Children.Add(buttonMatrix[y, x]);
                }
            }
             */
            #endregion            

            Button btn= sender as Button;
            btn.IsEnabled = false;
            #region
            /*
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromMilliseconds(150);
            _timer.Tick += (s, e2) =>
            {
                
                try
                {
                    //btn.IsEnabled = false;
                    lock (_locker) _operationWithSerialport.StartMarkingProcess();
                    _timer.Stop();
                    btn.IsEnabled = true;
                }
                catch (Exception)
                {
                    btn.IsEnabled = true;
                }
            };                        
            _timer.Start();
             * */
            #endregion
            
            await _operationWithSerialport.StartMarkingProcess();
            
            btn.IsEnabled = true;
        }                

        private void SwitchToOptions(object sender, RoutedEventArgs e)
        {
            var newWindow = new Window1();
            newWindow.ShowDialog();
            //newWindow.Focus();
        } 

        private void CommonMarkingConditionsWindow_Click(object sender, RoutedEventArgs e)
        {
            //string _currentProgramNumber=MainProgramNo.Text;
            var commonMarkingConditionsWindow = new CommonMarkingConditionsModule.View.CommonMarkingConditionsWindow(MainProgramNo.Text,sp);

            try
            {
                commonMarkingConditionsWindow.ShowDialog();

                if (commonMarkingConditionsWindow.DialogResult.HasValue && commonMarkingConditionsWindow.DialogResult.Value)
                {
                    this.MainProgramNo.Text = commonMarkingConditionsWindow.ProgramNo;
                    this._commonMarkingConditions = commonMarkingConditionsWindow.CommonMarkingConditions;

                    ///save new program number in default file
                    Properties.Settings.Default.ProgramNo = commonMarkingConditionsWindow.ProgramNo;
                    Properties.Settings.Default.Save();
                }
                else MessageBox.Show("ProgramNo Setting is cancelled!");
            }
            catch (IOException ex) { 
                MessageBox.Show(ex.Message); 
            //TODO  Recall RS232 port
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
            finally
            {
                commonMarkingConditionsWindow.Close();
            }

        }

        private void SettingSerialNumber_Click(object sender, RoutedEventArgs e)
        {
            var settingSerialNumber = new SettingSerialNumber(this.SerialNumber_1.Text,this.StartSerialNumber_2.Text);
            //settingSerialNumber.SN1pass2MainWin = this.SerialNumber_1.Text;

            //settingSerialNumber.ShowDialog();
            if (settingSerialNumber.ShowDialog().HasValue && settingSerialNumber.DialogResult.Value)
            {
                this.SerialNumber_1.Text = settingSerialNumber.SN1pass2MainWin;
                this.StartSerialNumber_2.Text = settingSerialNumber.SN2pass2MainWin;
                this.EndSerialNumber_2.Text = (int.Parse(StartSerialNumber_2.Text)+int.Parse(Qty.Text)-1).ToString();
                OrganizeTheSerialNumber();

                ///Save new SerialNumber to default 
                Properties.Settings.Default.SerialNumber1 = settingSerialNumber.SN1pass2MainWin;
                Properties.Settings.Default.SerialNumber2 = settingSerialNumber.SN2pass2MainWin;
                Properties.Settings.Default.Save();
            }

            ///cancel the serial number 
            else
                MessageBox.Show("SN Setting is cancelled!");            
        }

        bool[] IsButtonPressed = new bool[36];
        string[] SNForButtons = new string[36];
        
        /// <summary>
        /// generate the button by thr row and column of grid with SN number
        /// </summary>
        /// <param name="grid"></param>

        #region TrayGenerator
        private void GeneratingTrayMatrix(Grid grid)
        {            
            int row = grid.RowDefinitions.Count;
            int column = grid.ColumnDefinitions.Count;
            
            trayMatrix = new TrayMatrix[row * column];

            int CurrentItemIndex = 0;

            for(int i=0;i<row;i++)
                for(int j=0;j<column;j++)
                {
                    trayMatrix[CurrentItemIndex] = new TrayMatrix();
                  
                    trayMatrix[CurrentItemIndex].Name = "AF" + (CurrentItemIndex+1).ToString();
                    trayMatrix[CurrentItemIndex].Tag = CurrentItemIndex;
                    trayMatrix[CurrentItemIndex].Click += new System.Windows.RoutedEventHandler(TransparentButtonBackground);
                
                    /* Another way
                    trayMatrix[CurrentItemIndex].Click += (s, e) =>
                    {
                        Button button = s as Button;
                        if (button.Background != Brushes.Transparent)
                        {
                            button.Background = Brushes.Transparent;
                            button.Content = Grid.GetColumn(button).ToString() + "," + Grid.GetRow(button).ToString();
                        }
                        else
                        {
                            button.Background = Brushes.AliceBlue;
                            button.Content = button.Name;
                        }
                    };*/
                    Grid.SetColumn(trayMatrix[CurrentItemIndex].dockPanel, j);
                    Grid.SetRow(trayMatrix[CurrentItemIndex].dockPanel, i);
                    grid.Children.Add(trayMatrix[CurrentItemIndex].dockPanel);
                    trayMatrix[CurrentItemIndex].dockPanel.HorizontalAlignment = HorizontalAlignment.Stretch;
                    trayMatrix[CurrentItemIndex].dockPanel.VerticalAlignment = VerticalAlignment.Stretch;

                    trayMatrix[CurrentItemIndex].Background = Brushes.Transparent;
                    Grid.SetColumn(trayMatrix[CurrentItemIndex], j);
                    Grid.SetRow(trayMatrix[CurrentItemIndex], i);
                    grid.Children.Add(trayMatrix[CurrentItemIndex]);

                    IsButtonPressed[CurrentItemIndex] = true;
                    SNForButtons[CurrentItemIndex] = "";

                    CurrentItemIndex += 1;                        
                }
            OrganizeTheSerialNumber();
        }

        private void TransparentButtonBackground(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button.Background != Brushes.Transparent)
            {
                button.Background = Brushes.Transparent;
                IsButtonPressed[int.Parse(button.Tag.ToString())] = true;
                button.Content = null;
              //  button.Content = Grid.GetColumn(button).ToString()+","+Grid.GetRow(button).ToString();
                OrganizeTheSerialNumber();
            }
            else
            {
                button.Background = Brushes.AliceBlue;
                IsButtonPressed[int.Parse(button.Tag.ToString())] = false;
                OrganizeTheSerialNumber();
                button.Content = button.Name;               
            }
                
        }
     
        private void OrganizeTheSerialNumber()
        {
            int start = int.Parse(StartSerialNumber_2.Text);
            int end = int.Parse(EndSerialNumber_2.Text);

            List<string> serialNumberArray = new List<string>();
            //construct the serialnumberArray 
            do
            {
                serialNumberArray.Add(start.ToString());
                start += 1;
            } while (start <= end);

            int index=0;
            int SerialNumberArrayIndex=0;
            for(int i=0;i<6;i++)
                for(int j=0;j<6;j++)
                {
                    if (IsButtonPressed[index] == true && SerialNumberArrayIndex<serialNumberArray.Count)
                    {
                        //when Button is clicked, organize the serial number and assgin to textbox

                        //trayMatrix[index].Content = serialNumberArray[SerialNumberArrayIndex];
                        trayMatrix[index].textbox1.Text = SerialNumber_1.Text;
                        trayMatrix[index].textbox2.Text = serialNumberArray[SerialNumberArrayIndex];                        
                        SerialNumberArrayIndex += 1;
                    }
                    else
                    {
                        trayMatrix[index].Background = Brushes.AliceBlue;
                        //IsButtonPressed[int.Parse(trayMatrix[index].Name.Substring(6))] = false;
                        IsButtonPressed[int.Parse(trayMatrix[index].Tag.ToString())] = false;
                        trayMatrix[index].Content = trayMatrix[index].Name;
                        trayMatrix[index].textbox1.Text = null;
                        trayMatrix[index].textbox2.Text = null;
                    }
                    index += 1;
                }
        }

        private void DecreaseSerialNumber2_Click(object sender, RoutedEventArgs e)
        {
            int EndSN2;
            int StartSN2 = int.Parse(StartSerialNumber_2.Text);
            int quantity = int.Parse(Qty.Text);
            if (int.TryParse(EndSerialNumber_2.Text, out EndSN2) == true && quantity>1)
            {
                quantity -= 1;
                Qty.Text = quantity.ToString();
                EndSerialNumber_2.Text = (StartSN2+quantity-1).ToString();
                OrganizeTheSerialNumber();
            }
            else
                return;
        }

        private void IncreaseSerialNumber2_Click(object sender, RoutedEventArgs e)
        {
            int EndSerialNumber2;
            int StartSerialNumber2=int.Parse(StartSerialNumber_2.Text);
            int quantity = int.Parse(Qty.Text);
            if (int.TryParse(EndSerialNumber_2.Text, out EndSerialNumber2) == true && quantity < 36)
            {
                quantity += 1;
                Qty.Text = quantity.ToString();
                EndSerialNumber_2.Text = (StartSerialNumber2+ quantity-1).ToString();
                OrganizeTheSerialNumber();
            }
            else
                return;            
        }

        /// <summary>
        /// create a TrayMatrix class derived from Button,
        /// Add 1 dockpanel, 1 image , 1 stackpanel and 2 textboxes for each sample.
        /// </summary>
        public class TrayMatrix:Button
        {

            public Image image;
            public DockPanel dockPanel;
            public TextBox textbox1;  // for serial number 1
            public TextBox textbox2;  // for serial number 2
            public StackPanel stackPanel;

            public TrayMatrix()
            {                
                dockPanel = new DockPanel();
                dockPanel.LastChildFill=true;
                dockPanel.Margin = new Thickness(2);
                dockPanel.Background = Brushes.Salmon;
                //Contruct the image box, assign file to image and set position to the dockpanel
                image = new Image();
                image.MinWidth = 20;
                image.Width = 40;
                image.Margin = new Thickness(5,0,0,0);
                var uri = new Uri("pack://application:,,,/images/2DCode.png");
                BitmapImage Barcode=new BitmapImage();
                Barcode.BeginInit();
                Barcode.UriSource = uri;
                Barcode.EndInit();

                //image.Stretch = Stretch.Fill;
                image.Source = Barcode;
                DockPanel.SetDock(image, Dock.Left);
                dockPanel.Children.Add(image);

                stackPanel = new StackPanel();
                stackPanel.Orientation = Orientation.Vertical;
                stackPanel.Background = Brushes.Transparent;
                stackPanel.Margin = new Thickness(2);
                dockPanel.Children.Add(stackPanel);
            
                //Construct the textbox for serial number1 and 2
                textbox1=new TextBox();
                textbox1.MinHeight = 20;
                textbox1.MinWidth = 40;

                textbox1.Background = Brushes.LemonChiffon;
                textbox1.Margin = new Thickness(3);
                textbox1.Text = "035G122";
                textbox1.VerticalAlignment = VerticalAlignment.Center;
                textbox1.HorizontalAlignment = HorizontalAlignment.Center;
                stackPanel.Children.Add(textbox1);

                textbox2 = new TextBox();
                textbox2.MinHeight=20;
                textbox2.MinWidth = 15;
                textbox2.Background = Brushes.LemonChiffon;
                textbox2.Margin = new Thickness(3);
                textbox2.Text = "143412345";
                textbox2.VerticalAlignment = VerticalAlignment.Center;
                textbox2.HorizontalAlignment = HorizontalAlignment.Center;
                stackPanel.Children.Add(textbox2);
            }


        }
        #endregion

        private void BlockCondition_Click(object sender, RoutedEventArgs e)
        {
            var BlockConditionWin = new BlockConditionsWindow.View.BlockConditionsWindowView(sp);
            BlockConditionWin._CurrentblockConditionController.ProgramNo = MainProgramNo.Text;
            BlockConditionWin.blockConditionControllerList = this._blockConditionsList;
            try
            {
                BlockConditionWin.ShowDialog();
                if(BlockConditionWin.DialogResult.HasValue && BlockConditionWin.DialogResult.Value)
                {
                    this._blockConditionsList = BlockConditionWin.blockConditionControllerList;
                }
            }
            catch (IOException ex) { MessageBox.Show(ex.Message); }
            catch(Exception ex){MessageBox.Show(ex.Message);}

            finally { BlockConditionWin.Close(); }
        }
        
        private void Test1_Click(object sender, RoutedEventArgs e)
        {
            var _testingWindow = new Testing();
            _testingWindow.ShowDialog();            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Ready.Fill = Brushes.Lime;
            Stanby.Fill = Brushes.Yellow;
            Stop.Fill = Brushes.Red;
        }       

        private void SaveSettingAsXML_Click(object sender, RoutedEventArgs e)
        {
            List<BlockConditionsWindow.Model.BlockConditionsWithSerialPort> blockConditionsList = new List<BlockConditionsWindow.Model.BlockConditionsWithSerialPort>{
                new BlockConditionsWindow.Model.BlockConditionsWithSerialPort(){BlockNo="0"},
                new BlockConditionsWindow.Model.BlockConditionsWithSerialPort(){BlockNo="1"}
            };
            SerializationAndDeserialzation SAD = new SerializationAndDeserialzation();
            SAD.Serialize(_commonMarkingConditions, blockConditionsList);
        }

        private void OpenSettingFromXML_Click(object sender, RoutedEventArgs e)
        {
            SerializationAndDeserialzation SAD = new SerializationAndDeserialzation();
            SAD = SAD.Deserialize();
            this._blockConditionsList = SAD.blockConditionsList;
            this._commonMarkingConditions = SAD.commonMarkingConditions;
            
        }

        private void TryAvailablePorts()
        {
            string[] ports = SerialPort.GetPortNames();
            

            foreach(string port in ports)
            {
                sp = new SerialPort(port, 38400, Parity.None, 8, StopBits.One);
                try
                {
                    sp.Close();
                    sp.Open();
                }
                catch(IOException)
                { continue; }
                break;
            }

        }


    }
}
