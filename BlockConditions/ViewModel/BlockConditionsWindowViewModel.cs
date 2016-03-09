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
        internal IDialogService _dialogService;

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

        public UserControl PositionInformationUC
        {
            get
            {
                UserControl positionUC = new View.PositionInformationUC.PositionInformation_1();
                positionUC.DataContext = CurrentblockConditionModel;
                return positionUC;
            }
        }

        public UserControl SizeInformationUC
        {
            get
            {
                UserControl sizeUC = new View.SizeInformationUC.SizeInformation_1();
                sizeUC.DataContext = CurrentblockConditionModel;
                return sizeUC;
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
            get { return _comboboxSelectedItem; }
            set
            {
                _comboboxSelectedItem = value;
                NotifyPropertyChanged();
            }
        }

        #endregion

        #region Constructor
        public BlockConditionsWindowViewModel()
        {
            _dialogService = new DialogServiceToAddNewBlockCondition();

            Set = new RelayCommand(obj =>
            {
                ShowMessage1(obj);
                ShowMessage2(obj);
            });

            Cancel = new RelayCommand(obj => CloseWindowWithoutSave(obj));
            if (_blockConditionModelList == null)
            {
                _blockConditionModelList = new List<Model.BlockConditionsWithSerialPort>();
                AddNewBlockConditionInList();
                
            }

            NextBlockNumber = new RelayCommand(obj => ToNextBlockNumber());
            PreviousBlockNumber = new RelayCommand(obj => ToPreviousBlockNumber());



            CurrentblockConditionModel = _blockConditionModelList[0];
        }


        internal void AddNewBlockConditionInList()
        {
            _blockConditionModelList.Add(new Model.BlockConditionsWithSerialPort() { BlockNo = Convert.ToString(_blockConditionModelList.Count) });
        }
        #endregion

        #region ICommands
        private ICommand _previousBlockNumber;
        private ICommand _nextBlockNumber;
        private ICommand _blockConditionDownload;
        private ICommand _blockConditionUpload;
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
            get { return _blockConditionDownload; }
            set { _blockConditionDownload = value; }
        }

        public ICommand Upload
        {
            get { return _blockConditionUpload; }
            set { _blockConditionUpload = value; }
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
        private void ShowMessage1(object obj)
        {
            MessageBox.Show("Test 1:");
        }

        private void ShowMessage2(object obj)
        {
            MessageBox.Show("Test 2:");
        }

        private void CloseWindowWithoutSave(object obj)
        {
            MessageBox.Show("TEST");
            //dialogResult = false;

            Application.Current.MainWindow.Close();

        }

        private void BlockConditionDownload(object obj)
        {
            var result = MessageBox.Show("Continue data downloading from controller", "Confirm Window", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                _currentblockConditionModel.DownloadBlockConditions();
                BlockType_ChangedByBlockClass(_currentblockConditionModel);
            }
        }

        public void BlockType_ChangedByBlockClass(Model.BlockConditionsWithSerialPort blockCondition)
        {

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
        }

        #endregion

        #region Method For Button
        public void ToNextBlockNumber()
        {
            int _currentIndex = BlockConditionModelList.FindIndex(element => element.BlockNo == CurrentblockConditionModel.BlockNo);

            int _newIndex = _currentIndex + 1;

            if (_newIndex < BlockConditionModelList.Count )
            {
                CurrentblockConditionModel = BlockConditionModelList[_newIndex];

            }
            else if (_dialogService.ShowMessageBox() == MessageBoxResult.Yes)
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
        #endregion
    }
}
