﻿using System;
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
using System.IO.Ports;


namespace BlockConditionsWindow.ViewModel
{
    public class BlockConditionsWindowViewModel : INotifyPropertyChanged
    {
<<<<<<< HEAD
        #region --interfaces--
=======
        #region interfaces
>>>>>>> 529f57d9a520ad54fb73546f7a54103842f4e447
        internal IDialogService _dialogService;
        internal IKeyenceCommuniationService _keyenceCommunicationService;
        #endregion

        #region  --Data Binding--
        private Model.BlockConditions _currentblockConditionModel;
        public Model.BlockConditions CurrentblockConditionModel
        {
            get { return _currentblockConditionModel; }
            set
            {
                try
                {
                    _currentblockConditionModel = value;
                    NotifyPropertyChanged();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private List<Model.BlockConditions> _blockConditionModelList;
        public List<Model.BlockConditions> BlockConditionModelList
        {
            get { return _blockConditionModelList; }
            set
            {
                if (value != null && value.GetType() == typeof(List<Model.BlockConditions>))
                    _blockConditionModelList = value;
                else
                    throw new ArgumentException("Invalid BlockConditionModelList");
            }
        }
        public bool? DialogResult { get; set; }

        private UserControl _positionInfromationUC;
        public UserControl PositionInformationUC
        {
<<<<<<< HEAD
            get { return _positionInfromationUC; }
            set
            {
=======
            get{return _positionInfromationUC;}
            set 
            { 
>>>>>>> 529f57d9a520ad54fb73546f7a54103842f4e447
                _positionInfromationUC = value;
                NotifyPropertyChanged();
            }
        }

        private UserControl _sizeInformationUC;
        public UserControl SizeInformationUC
        {
<<<<<<< HEAD
            get { return _sizeInformationUC; }
            set
            {
                _sizeInformationUC = value;
                NotifyPropertyChanged();
            }
        }

        private UserControl _speedInformationUC;
        public UserControl SpeedInformationUC
        {
            get { return _speedInformationUC; }
            set
            {
                _speedInformationUC = value;
                NotifyPropertyChanged();
=======
            get {return _sizeInformationUC;}
            set 
            { 
                _sizeInformationUC = value;
                NotifyPropertyChanged();
>>>>>>> 529f57d9a520ad54fb73546f7a54103842f4e447
            }
        }

        #region Implement List<BlockType> blockTypes
        private List<BlockType> blockTypes = GetBlockTypes();
        public List<BlockType> BlockTypes
        {
            get { return blockTypes; }
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
<<<<<<< HEAD
                }
                //NotifyPropertyChanged("ComboboxSelectedItem");
                NotifyPropertyChanged();
                OnBlockTypeChanged();
=======
                    NotifyPropertyChanged("ComboboxSelectedItem");
                    OnBlockTypeChanged();
                }
>>>>>>> 529f57d9a520ad54fb73546f7a54103842f4e447
            }
        }
        #endregion

<<<<<<< HEAD
        #region --ICommands Declaration--
=======
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
>>>>>>> 529f57d9a520ad54fb73546f7a54103842f4e447
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

        #region --Constructor--
        public BlockConditionsWindowViewModel()
        {
            #region --Instantiating Interfaces
            _dialogService = new DialogServiceToAddNewBlockCondition();
            _keyenceCommunicationService = new KeyenceCommunicationService(new SerialPort("COM6", 38400, Parity.None, 8, StopBits.One));
            //_keyenceCommunicationService = new MockKeyenceCommunicationService("K3,0,099,000,0000.827,0000.778,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.760,000.502,00.000,000,00.000,0,0,000.603,?O?R?T?f?P?Q?Q");
            #endregion
            InitializingBlockConditionsWindowViewModel();
        }
        public BlockConditionsWindowViewModel(SerialPort sp)
        {
            #region --Instantiating Interfaces
            _dialogService = new DialogServiceToAddNewBlockCondition();
            _keyenceCommunicationService = new KeyenceCommunicationService(sp);
            #endregion
            InitializingBlockConditionsWindowViewModel();
        }

        private void InitializingBlockConditionsWindowViewModel()
        {
            #region --Instantiating Commands--
            Set = new RelayCommand(obj => SetAsSetting(obj));
            Cancel = new RelayCommand(obj => CloseWindowWithoutSave(obj));
            PreviousBlockNumber = new RelayCommand(obj => ToPreviousBlockNumber());
            NextBlockNumber = new RelayCommand(obj => ToNextBlockNumber());
            Upload = new RelayCommand(obj => BlockConditionUpload());
            Download = new RelayCommand(obj => BlockConditionDownload());
            #endregion

            #region --InitialBlockCondition--
            if (_blockConditionModelList == null)
            {
                _blockConditionModelList = new List<Model.BlockConditions>();
                AddNewBlockConditionInList();
            }
            CurrentblockConditionModel = _blockConditionModelList[0];
            BlockType_ChangedByBlockClass();
            #endregion

            #region --Subscribing Events--
            BlockTypeChangedEvent += ChangeUserControl; //ToDo how to test it with raised UI?
            //BlockTypeChangedEvent += BlockType_ChangedByBlockClass;            
            #endregion
            OnBlockTypeChanged();
        }


        #endregion

        #region --INotifyPropertyChanged Implement--
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

<<<<<<< HEAD
        #region --Methods For Button--
        internal void ToNextBlockNumber()
        {
            int _currentIndex = BlockConditionModelList.FindIndex(element => element.BlockNo == CurrentblockConditionModel.BlockNo);
            int _newIndex = _currentIndex + 1;
            if (_newIndex < BlockConditionModelList.Count)
            {
                CurrentblockConditionModel = BlockConditionModelList[_newIndex];
                OnBlockTypeChanged();
                //ComboboxSelectedItem = BlockTypes.Single(i => i.BlockTypeIDcode == CurrentblockConditionModel.BlockType);
                BlockType_ChangedByBlockClass();
            }
            else if (_dialogService.ShowMessageBox("Add new BlockCondition?", "Warning", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                AddNewBlockConditionInList(ComboboxSelectedItem);
                CurrentblockConditionModel = BlockConditionModelList.Last();
            }
        }

        internal void ToPreviousBlockNumber()
        {
            int _currentIndex = int.Parse(CurrentblockConditionModel.BlockNo);
            int _newIndex = (_currentIndex - 1 >= 0) ? _newIndex = _currentIndex - 1 : _currentIndex;
            CurrentblockConditionModel = BlockConditionModelList[_newIndex];
            ComboboxSelectedItem = BlockTypes.Single(i => i.BlockTypeIDcode == CurrentblockConditionModel.BlockType);
=======
        #region Methods
        private void SetAsSetting(object obj)
        {
            this.BlockTypeChangedEvent += ChangeUserControl;
>>>>>>> 529f57d9a520ad54fb73546f7a54103842f4e447
            OnBlockTypeChanged();
        }

        internal void BlockConditionDownload()
        {
<<<<<<< HEAD
            if (_dialogService.ShowMessageBox("Continue data downloading from controller", "Confirm Window", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    _keyenceCommunicationService.Download(_currentblockConditionModel);
                    OnBlockTypeChanged();
                    BlockType_ChangedByBlockClass();
                    _comboboxSelectedItem = BlockTypes.Single(i => i.BlockTypeIDcode == CurrentblockConditionModel.BlockType);
                }
                catch (LaserMarkingErrorException e)
                {
                    //ToDo: Errlog
                    MessageBox.Show(e.Message + "catched by ViewModel");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "catched by ViewModel");
                }
            }
        }

        internal void BlockConditionUpload()
        {
            if (_dialogService.ShowMessageBox("Continue data uploading to controller", "Confirm Window", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                try
                {
                    _keyenceCommunicationService.Upload(CurrentblockConditionModel);
                }
                catch (LaserMarkingErrorException e)
                {
                    //ToDo: Errlog
                    MessageBox.Show(e.Message + "catched by ViewModel");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message + "catched by ViewModel");
                }
            }
        }

        private void SetAsSetting(object obj)
        { }

        private void CloseWindowWithoutSave(object obj)
        {
            MessageBox.Show("TEST");
            //dialogResult = false;
            Application.Current.MainWindow.Close();
        }
        #endregion

        #region --Other Methods--
        internal void BlockType_ChangedByBlockClass()
        {
            switch (_currentblockConditionModel.BlockType)
=======
            MessageBox.Show("TEST");
            //dialogResult = false;
            Application.Current.MainWindow.Close();
        }

        internal void BlockType_ChangedByBlockClass(Model.BlockConditionsWithSerialPort blockCondition)
        {
            this.BlockTypeChangedEvent+=ChangeUserControl;
            switch (blockCondition.BlockType)
>>>>>>> 529f57d9a520ad54fb73546f7a54103842f4e447
            {
                case "000":
                    ComboboxSelectedItem = blockTypes[(int)BlockTypeID.HorizontalMarking];
                    break;
                case "001":
                    ComboboxSelectedItem = blockTypes[(int)BlockTypeID.VerticalMarking];
                    break;
                case "009":
                    ComboboxSelectedItem = blockTypes[(int)BlockTypeID.Barcode2DCode];
                    break;
                case "020":
                    ComboboxSelectedItem = blockTypes[(int)BlockTypeID.GS1Databar];
                    break;
                case "030":
                    ComboboxSelectedItem = blockTypes[(int)BlockTypeID.DotCharacter];
                    break;
                case "031":
                    ComboboxSelectedItem = blockTypes[(int)BlockTypeID.Dot2DCode];
                    break;
                case "-02":
                    ComboboxSelectedItem = blockTypes[(int)BlockTypeID.WorkpieceImageLogo];
                    break;
                default:
                    ComboboxSelectedItem = blockTypes[(int)BlockTypeID.empty];
                    break;
            }
<<<<<<< HEAD
            NotifyPropertyChanged("ComboboxSelectedItem");
            //OnBlockTypeChanged();
        }

        public event EventHandler BlockTypeChangedEvent;
        public void OnBlockTypeChanged()
        {
            if (BlockTypeChangedEvent != null)
                BlockTypeChangedEvent(this, new EventArgs());
        }
=======
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

>>>>>>> 529f57d9a520ad54fb73546f7a54103842f4e447

        internal void ChangeUserControl(object sender, EventArgs e)
        {
            PositionInformationUC = View.UserControlSimpleFactory.PositionInformation(CurrentblockConditionModel);
            SizeInformationUC = View.UserControlSimpleFactory.SizeInformation(CurrentblockConditionModel);
            SpeedInformationUC = View.UserControlSimpleFactory.SpeedInformation(CurrentblockConditionModel);
        }

        internal void AddNewBlockConditionInList()
        {
            _blockConditionModelList.Add(new Model.BlockConditions() { ProgramNo = BlockConditionModelList.Count != 0 ? BlockConditionModelList[0].ProgramNo : "0000", BlockNo = Convert.ToString(_blockConditionModelList.Count) });
        }
        internal void AddNewBlockConditionInList(BlockType blockType)
        {
            _blockConditionModelList.Add(new Model.BlockConditions() { ProgramNo = BlockConditionModelList.Count != 0 ? BlockConditionModelList[0].ProgramNo : "0000", BlockNo = Convert.ToString(_blockConditionModelList.Count) });
            if (blockType != null)
                _blockConditionModelList.Last().BlockType = blockType.BlockTypeIDcode;
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
