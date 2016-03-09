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
    public partial class BlockConditionsWindowView : Window,INotifyPropertyChanged
    {
        public Model.BlockConditionsWithSerialPort _CurrentblockConditionController;
        public List<Model.BlockConditionsWithSerialPort> blockConditionControllerList;
        private ObservableCollection<BlockType> _blockTypes;
        public ObservableCollection<BlockType> blockTypes 
        { 
            get{return GetBlockTypes();}
            set {
                _blockTypes = value;                
                NotifyPropertyChanged(); }
        }
        
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public SerialPort sp;

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
            _blockTypes = GetBlockTypes();  //initialize blockTypes
           // CB_BlockType.ItemsSource = blockTypes;
        }

        public BlockConditionsWindowView(SerialPort sp)
        {
            InitializeComponent();
            //blockTypes = GetBlockTypes();  //initialize blockTypes
            CB_BlockType.ItemsSource = blockTypes;
            
            if (blockConditionControllerList == null)
            {
                blockConditionControllerList = new List<Model.BlockConditionsWithSerialPort>();
                blockConditionControllerList.Add(new Model.BlockConditionsWithSerialPort(sp));
            }
            _CurrentblockConditionController = blockConditionControllerList[0];

            DataContext = _CurrentblockConditionController;
            BlockType_ChangedByBlockClass(_CurrentblockConditionController);
            BlockNumber.Text = _CurrentblockConditionController.BlockNo;
        
        }

        private void BlockType_ChangedByBlockClass(Model.BlockConditionsWithSerialPort blockCondition)
        { 
            
            switch(blockCondition.BlockType)
            {
                case "000":
                    CB_BlockType.SelectedItem = blockTypes[(int)BlockTypeID.empty]; //triggle BlockType_SelectionChanged event
                    CB_BlockType.SelectedItem = blockTypes[(int)BlockTypeID.HorizontalMarking];
                    break;
                case "001":
                    CB_BlockType.SelectedItem = blockTypes[(int)BlockTypeID.empty]; //triggle BlockType_SelectionChanged event
                    CB_BlockType.SelectedItem = blockTypes[(int)BlockTypeID.VerticalMarking];
                    break;
                case "009":
                    CB_BlockType.SelectedItem = blockTypes[(int)BlockTypeID.empty]; //triggle BlockType_SelectionChanged event
                    CB_BlockType.SelectedItem = blockTypes[(int)BlockTypeID.Barcode2DCode];
                    break;
                case "020":
                    CB_BlockType.SelectedItem = blockTypes[(int)BlockTypeID.empty]; //triggle BlockType_SelectionChanged event
                    CB_BlockType.SelectedItem = blockTypes[(int)BlockTypeID.GS1Databar];
                    break;
                case "030":
                    CB_BlockType.SelectedItem = blockTypes[(int)BlockTypeID.empty]; //triggle BlockType_SelectionChanged event
                    CB_BlockType.SelectedItem = blockTypes[(int)BlockTypeID.DotCharacter];
                    break;
                case "031":
                    CB_BlockType.SelectedItem = blockTypes[(int)BlockTypeID.empty]; //triggle BlockType_SelectionChanged event
                    CB_BlockType.SelectedItem = blockTypes[(int)BlockTypeID.Dot2DCode];
                    break;
                default:
                    CB_BlockType.SelectedItem = blockTypes[(int)BlockTypeID.empty];
                    break;
            }
        }

        private void BlockType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox comboBox = sender as ComboBox;
            BlockType obj = comboBox.SelectedItem as BlockType;
           
            if (obj.BlockTypeIDcode != "")
            {
                _CurrentblockConditionController.BlockType = obj.BlockTypeIDcode;

                //Position Information
                UserControl userControl_PositionInfo;                             
                userControl_PositionInfo = UserControlSimpleFactory.PositionInformation(obj.BlockTypeIDcode);
                StackPanelForPositionInformation.Children.Clear();   
                StackPanelForPositionInformation.Children.Add(userControl_PositionInfo);
                userControl_PositionInfo.DataContext = _CurrentblockConditionController;                

                //Speed Information
                InstantiatingSpeedinformation(obj.BlockTypeIDcode);

                //Size Information               
                UserControl usercontrol_SizeInfo;                
                usercontrol_SizeInfo = UserControlSimpleFactory.SizeInformation(obj.BlockTypeIDcode);
                StackPanelForSizeInformation.Children.Clear();
                StackPanelForSizeInformation.Children.Add(usercontrol_SizeInfo);
                usercontrol_SizeInfo.DataContext = _CurrentblockConditionController;
            }
            else //when (obj.BlockTypeIDcode==""), clear stackpanel of Position and Size
            {
               // StackPanelForPositionInformation.Children.Clear();
                //StackPanelForSizeInformation.Children.Clear(); 
            }
        }  

        private void InstantiatingSpeedinformation(string blockType)
        {
            switch (blockType)
            {
                case "030":
                case "031":
                case "032":
                case "033":
                case "034":
                    if (!StackpanelForSpeedInformation.Children.Contains(NumberOfMultipunchers))
                        StackpanelForSpeedInformation.Children.Add(NumberOfMultipunchers);
                    if (!StackpanelForSpeedInformation.Children.Contains(MultipunchTime))
                        StackpanelForSpeedInformation.Children.Add(MultipunchTime);
                    break;
                default:
                    StackpanelForSpeedInformation.Children.Remove(NumberOfMultipunchers);
                    StackpanelForSpeedInformation.Children.Remove(MultipunchTime);
                    break;
            }
        }          

        #region initializing BlockType class for combobox itemsource
        private static ObservableCollection<BlockType> GetBlockTypes()
        {
            ObservableCollection<BlockType> blockTypes = new ObservableCollection<BlockType>
            {
                new BlockType("",""),
                new BlockType("Horzontal marking","000"),
                new BlockType("Vertical marking","001"),
                new BlockType("Barcode, 2D code","009"),
                new BlockType("GS1 DataBar && CC","020"),
                new BlockType("Dot Character","030"),
                new BlockType("Dot 2D Code","031"),
            };

            return blockTypes;
        }

        private enum BlockTypeID
        {
            empty=0,
            HorizontalMarking,
            VerticalMarking,
            Barcode2DCode,
            GS1Databar,
            DotCharacter,
            Dot2DCode
        }

        //BlockType class
        public class BlockType
        {
            private string _type;
            public string Type
            {
                get { return _type; }
                set { }
            }

            private string _idCode;
            public string BlockTypeIDcode
            {
                get { return _idCode; }
                set { }
            }

            public BlockType(string Type, string IDcode)
            {
                this._type = Type;
                this._idCode = IDcode;
            }

            public override string ToString()
            {
                return Type;
            }
        }
        #endregion

        private void BlockConditionDownload_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Continue data downloading from controller", "Confirm Window", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _CurrentblockConditionController.DownloadBlockConditions();
                BlockType_ChangedByBlockClass(_CurrentblockConditionController);
            }
        }

        private void BlockConditionUpload_Click(object sender, RoutedEventArgs e)
        {

            var result = MessageBox.Show("Continue data uploading to controller", "Confirm Window", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _CurrentblockConditionController.UploadBlockConditions();

            }
        }

        private void BlockNo_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int index = int.Parse(BlockNumber.Text);
            
            
            if (index < blockConditionControllerList.Count)
            {                
                _CurrentblockConditionController = blockConditionControllerList[index];
                this.DataContext = _CurrentblockConditionController;
                BlockType_ChangedByBlockClass(_CurrentblockConditionController);
               
            }
            else
            {
                var result = MessageBox.Show("Only " + blockConditionControllerList.Count + " block conditions in the memory" + "\r\n" +
                    "Add new block condition?", "Warning", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    blockConditionControllerList.Add(new Model.BlockConditionsWithSerialPort(_CurrentblockConditionController.GetSerialPort()));
                    for(int i=0;i<blockConditionControllerList.Count;i++)
                    {
                        blockConditionControllerList[i].BlockNo = i.ToString();
                        blockConditionControllerList[i].ProgramNo = _CurrentblockConditionController.ProgramNo;
                    }
                }
                index=(blockConditionControllerList.Count - 1);
                BlockNumber.Text = index.ToString();
                _CurrentblockConditionController = blockConditionControllerList[index];
                this.DataContext = _CurrentblockConditionController;
                BlockType_ChangedByBlockClass(_CurrentblockConditionController);
                }
            }
            catch (FormatException ex) { MessageBox.Show(ex.Message); }
        }

        private void Set_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Set_Click");
            //DialogResult = true;
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            this.Close();
        }

        private void BlockNumberDecrement_Click(object sender, RoutedEventArgs e)
        {

            int NewBlockNumber = Convert.ToInt32(BlockNumber.Text) - 1;
            if (NewBlockNumber < 0)
                BlockNumber.Text = "0";
            else
                BlockNumber.Text = NewBlockNumber.ToString();

                    
        }

        private void BlockNumberIncrement_Click(object sender, RoutedEventArgs e)
        {
            int NewBlockNumber = Convert.ToInt32(BlockNumber.Text) + 1;
            if (NewBlockNumber < 0)
                BlockNumber.Text = "0";
            else
                BlockNumber.Text = NewBlockNumber.ToString();
        }



        public event PropertyChangedEventHandler PropertyChanged;

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            ViewModel = null;
            MessageBox.Show("Test");
            this.Close();
            
        }

        
    }
}
