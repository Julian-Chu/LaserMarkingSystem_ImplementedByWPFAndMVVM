using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using BlockConditionsWindow.Model;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;


namespace BlockConditionsWindow.ViewModel
{

    public class BlockConditionsWindowViewModel : INotifyPropertyChanged
    {
        #region interfaces
        internal IDialogService _dialogService;
        internal IKeyenceCommuniationService _keyenceCommunicationService;
        #endregion

        #region  DataContext
        private Model.BlockConditionsWithSerialPort _currentblockConditionModel;
        public Model.BlockConditionsWithSerialPort CurrentblockConditionModel
        {
            get { return _currentblockConditionModel; }
            set
            {
                _currentblockConditionModel = value;
                NotifyPropertyChanged();
            }
        }
        private List<Model.BlockConditionsWithSerialPort> _blockConditionModelList;
        public List<Model.BlockConditionsWithSerialPort> BlockConditionModelList
        {
            get { return _blockConditionModelList; }
            set
            {
                if (value.GetType() == typeof(Model.BlockConditionsWithSerialPort))
                    _blockConditionModelList = value;
                else
                    throw new ArgumentException("Invalid BlockConditionModelList");
            }
        }
        public bool? DialogResult { get; set; }

        private UserControl _positionInfromationUC;
        public UserControl PositionInformationUC
        {
            get{return _positionInfromationUC;}
            set 
            { 
                _positionInfromationUC = value;
                NotifyPropertyChanged();
            }
        }

        private UserControl _sizeInformationUC;
        public UserControl SizeInformationUC
        {
            get {return _sizeInformationUC;}
            set 
            { 
                _sizeInformationUC = value;
                NotifyPropertyChanged();
            }
        }

        #region Implement List<BlockType> blockTypes
        private List<BlockType> blockTypes = GetBlockTypes();
        public List<BlockType> BlockTypes
        {
            get { return blockTypes; }
            //set
            //{
            //    blockTypes = value;
            //    NotifyPropertyChanged();
            //}
        }
        private static List<BlockType> GetBlockTypes()
        {
            List<BlockType> blockTypes = new List<BlockType>
            {
                new BlockType("",""),
                new BlockType("Horzontal marking","000"),
                new BlockType("Vertical marking","001"),
                new BlockType("Barcode, 2D code","009"),
                new BlockType("GS1 DataBar && CC","020"),
                new BlockType("Dot Character","030"),
                new BlockType("Dot 2D Code","031"),
                new BlockType("Workpiece image logo","-02"),
            };
            return blockTypes;
        }
        private enum BlockTypeID
        {
            empty = 0,
            HorizontalMarking,
            VerticalMarking,
            Barcode2DCode,
            GS1Databar,
            DotCharacter,
            Dot2DCode,
            WorkpieceImageLogo
        }
        #endregion

        private BlockType _comboboxSelectedItem;
        public BlockType ComboboxSelectedItem
        {
            get {return _comboboxSelectedItem; }
            set
            {
                if (_comboboxSelectedItem != value)
                {
                    _comboboxSelectedItem = value;
                    CurrentblockConditionModel.BlockType = value.BlockTypeIDcode;
                    NotifyPropertyChanged("ComboboxSelectedItem");
                    OnBlockTypeChanged();
                }
            }
        }

        #endregion

        #region Constructor
        public BlockConditionsWindowViewModel()
        {            
            //Interface
            _dialogService = new DialogServiceToAddNewBlockCondition();
            _keyenceCommunicationService=new KeyenceCommunicationService(new System.IO.Ports.SerialPort());

            //Command
            Set = new RelayCommand(obj =>SetAsSetting(obj));
            Cancel = new RelayCommand(obj => CloseWindowWithoutSave(obj));
            PreviousBlockNumber = new RelayCommand(obj => ToNextBlockNumber());
            NextBlockNumber = new RelayCommand(obj => ToPreviousBlockNumber());
            Upload=new RelayCommand(obj=>BlockConditionUpload());
            Download = new RelayCommand(obj => BlockConditionDownload());

            if (_blockConditionModelList == null)
            {
                _blockConditionModelList = new List<Model.BlockConditionsWithSerialPort>();
                AddNewBlockConditionInList();                
            }

            CurrentblockConditionModel = _blockConditionModelList[0];
            BlockType_ChangedByBlockClass(CurrentblockConditionModel);
        }

        internal void AddNewBlockConditionInList()
        {
            _blockConditionModelList.Add(new Model.BlockConditionsWithSerialPort() { BlockNo = Convert.ToString(_blockConditionModelList.Count) });
        }
        #endregion

        #region ICommands
        private ICommand _previousBlockNumber;
        private ICommand _nextBlockNumber;
        private ICommand _download;
        private ICommand _upload;
        private ICommand _set;
        private ICommand _cancel;

        public ICommand PreviousBlockNumber
        {
            get { return _previousBlockNumber; }
            set { _previousBlockNumber = value; }
        }

        public ICommand NextBlockNumber
        {
            get { return _nextBlockNumber; }
            set { _nextBlockNumber = value; }
        }

        public ICommand Download
        {
            get { return _download; }
            set { _download = value; }
        }

        public ICommand Upload
        {
            get { return _upload; }
            set { _upload = value; }
        }

        public ICommand Set
        {
            get { return _set; }
            set { _set = value; }
        }

        public ICommand Cancel
        {
            get { return _cancel; }
            set { _cancel = value; }
        }
        #endregion

        #region INotifyPropertyChanged Implement
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Methods
        private void SetAsSetting(object obj)
        {
            this.BlockTypeChangedEvent += ChangeUserControl;
            OnBlockTypeChanged();
        }

        private void CloseWindowWithoutSave(object obj)
        {
            MessageBox.Show("TEST");
            //dialogResult = false;
            Application.Current.MainWindow.Close();
        }

        internal void BlockType_ChangedByBlockClass(Model.BlockConditionsWithSerialPort blockCondition)
        {
            this.BlockTypeChangedEvent+=ChangeUserControl;
            switch (blockCondition.BlockType)
            {
                case "000":
                    //ComboboxSelectedItem = blockTypes[(int)BlockTypeID.empty]; //triggle BlockType_SelectionChanged event
                    ComboboxSelectedItem = blockTypes[(int)BlockTypeID.HorizontalMarking];
                    break;
                case "001":
                    //ComboboxSelectedItem = blockTypes[(int)BlockTypeID.empty]; //triggle BlockType_SelectionChanged event
                    ComboboxSelectedItem = blockTypes[(int)BlockTypeID.VerticalMarking];
                    break;
                case "009":
                    //ComboboxSelectedItem = blockTypes[(int)BlockTypeID.empty]; //triggle BlockType_SelectionChanged event
                    ComboboxSelectedItem = blockTypes[(int)BlockTypeID.Barcode2DCode];
                    break;
                case "020":
                    //ComboboxSelectedItem = blockTypes[(int)BlockTypeID.empty]; //triggle BlockType_SelectionChanged event
                    ComboboxSelectedItem = blockTypes[(int)BlockTypeID.GS1Databar];
                    break;
                case "030":
                    //ComboboxSelectedItem = blockTypes[(int)BlockTypeID.empty]; //triggle BlockType_SelectionChanged event
                    ComboboxSelectedItem = blockTypes[(int)BlockTypeID.DotCharacter];
                    break;
                case "031":
                    //ComboboxSelectedItem = blockTypes[(int)BlockTypeID.empty]; //triggle BlockType_SelectionChanged event
                    ComboboxSelectedItem = blockTypes[(int)BlockTypeID.Dot2DCode];
                    break;
                case "-02":
                    ComboboxSelectedItem = blockTypes[(int)BlockTypeID.WorkpieceImageLogo];
                    break;
                default:
                    ComboboxSelectedItem = blockTypes[(int)BlockTypeID.empty];
                    break;
            }
            OnBlockTypeChanged();
        }



        #endregion

        #region Methods For Button
        internal void ToNextBlockNumber()
        {
            int _currentIndex = BlockConditionModelList.FindIndex(element => element.BlockNo == CurrentblockConditionModel.BlockNo);

            int _newIndex = _currentIndex + 1;

            if (_newIndex < BlockConditionModelList.Count )
            {
                CurrentblockConditionModel = BlockConditionModelList[_newIndex];

            }
            else if (_dialogService.ShowMessageBox("Add new BlockCondition?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                AddNewBlockConditionInList();
                CurrentblockConditionModel = BlockConditionModelList.Last();
            }


        }

        internal void ToPreviousBlockNumber()
        {
            int _currentIndex = int.Parse(CurrentblockConditionModel.BlockNo);
            int _newIndex = (_currentIndex - 1 >= 0) ? _newIndex = _currentIndex - 1 : _currentIndex;
            CurrentblockConditionModel = BlockConditionModelList[_newIndex];
        }

        internal void BlockConditionDownload()
        {
            if (_dialogService.ShowMessageBox("Continue data downloading from controller", "Confirm Window", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _keyenceCommunicationService.Download(CurrentblockConditionModel);
                BlockType_ChangedByBlockClass(CurrentblockConditionModel);
            }
        }

        internal void BlockConditionUpload()
        {
            if (_dialogService.ShowMessageBox("Continue data uploading to controller", "Confirm Window", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                _keyenceCommunicationService.Upload(CurrentblockConditionModel);
            }
        }
        #endregion

        public event EventHandler BlockTypeChangedEvent;
        public void OnBlockTypeChanged()
        {
            if(BlockTypeChangedEvent!=null)
            BlockTypeChangedEvent(this,new EventArgs());
        }

        //private void InstantiatingSpeedinformation(string blockType)
        //{
        //    switch (blockType)
        //    {
        //        case "030":
        //        case "031":
        //        case "032":
        //        case "033":
        //        case "034":
        //            if (!StackpanelForSpeedInformation.Children.Contains(NumberOfMultipunchers))
        //                StackpanelForSpeedInformation.Children.Add(NumberOfMultipunchers);
        //            if (!StackpanelForSpeedInformation.Children.Contains(MultipunchTime))
        //                StackpanelForSpeedInformation.Children.Add(MultipunchTime);
        //            break;
        //        default:
        //            StackpanelForSpeedInformation.Children.Remove(NumberOfMultipunchers);
        //            StackpanelForSpeedInformation.Children.Remove(MultipunchTime);
        //            break;
        //    }
        //}

        private void ChangeUserControl(object sender, EventArgs e)
        {

            if (CurrentblockConditionModel.BlockType!="")
            {
                //Position Information
                PositionInformationUC=View.UserControlSimpleFactory.PositionInformation(CurrentblockConditionModel.BlockType);
                

                //Speed Information
                //InstantiatingSpeedinformation(obj.BlockTypeIDcode);

                //Size Information               
                SizeInformationUC = View.UserControlSimpleFactory.SizeInformation(CurrentblockConditionModel.BlockType);

            }
            else //when (obj.BlockTypeIDcode==""), clear stackpanel of Position and Size
            {
                // StackPanelForPositionInformation.Children.Clear();
                //StackPanelForSizeInformation.Children.Clear(); 
            }
        }


    }
}
