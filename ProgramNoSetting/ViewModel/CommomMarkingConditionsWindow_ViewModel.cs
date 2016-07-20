using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO.Ports;
using System.Windows.Input;
using System.Windows;

namespace CommonMarkingConditionsModule.ViewModel
{
    public class CommonMarkingConditionsWindow_ViewModel : INotifyPropertyChanged
    {
        #region --Interface--
        internal DataAccessLayer.IKeyenceCommunication _keyenceCommunicationService;
        #endregion

        #region --Data Binding--
        internal int currentIndexOfCommonMarkingConditions = 0;
        public int CurrentIndexOfCommonMarkingConditions
        {
            get { return currentIndexOfCommonMarkingConditions; }
            set
            {
                currentIndexOfCommonMarkingConditions = value;
                NotifyPropertyChanged();
            }

        }
        public Model.CommonMarkingConditions CurrentCommonMarkingConditons
        {
            get { return commonMarkingConditionsList[currentIndexOfCommonMarkingConditions]; }
            set
            {
             //       commonMarkingConditionsList[currentIndexOfCommonMarkingConditions] = value;
                    NotifyPropertyChanged();
            }
        }

        private List<Model.CommonMarkingConditions> commonMarkingConditionsList;
        public List<Model.CommonMarkingConditions> CommonMarkingConditionsList
        {
            get { return commonMarkingConditionsList; }
            set
            {
                if (value != null && value.GetType() == typeof(List<Model.CommonMarkingConditions>))
                    commonMarkingConditionsList = value;
                else
                    throw new ArgumentException("Invalid CommonMarkingConditionsList");
                NotifyPropertyChanged();
            }
        }

        public int CommonMarkingConditionsListCount
        {
            
            get { return CommonMarkingConditionsList.Count(); }
            set { NotifyPropertyChanged(); }
            
        }


        #endregion

        #region --ICommand Declaration--
        private ICommand MoveToPreviousProgram;
        private ICommand MoveToNextProgram;
        private ICommand MoveToFirstProgram;
        private ICommand MoveToLastProgram;
        private ICommand DownloadParameters;
        private ICommand UploadParameters;
        private ICommand setProgramNo;
        private ICommand cancel;
        private ICommand AddNewProgram;
        private ICommand DeleteProgram;

        public ICommand IMoveToPreviousProgram
        {
            get { return MoveToPreviousProgram; }
            set { MoveToPreviousProgram = value; }
        }

        public ICommand IMoveToNextProgram
        {
            get { return MoveToNextProgram; }
            set { MoveToNextProgram = value; }
        }

        public ICommand IMoveToFirstProgram
        {
            get { return MoveToFirstProgram; }
            set { MoveToFirstProgram = value; }
        }

        public ICommand IMoveToLastProgram
        {
            get { return MoveToLastProgram; }
            set { MoveToLastProgram = value; }
        }

        public ICommand IDownloadParameters
        {
            get { return DownloadParameters; }
            set { DownloadParameters = value; }
        }

        public ICommand IUploadParameters
        {
            get { return UploadParameters; }
            set { UploadParameters = value; }
        }

        public ICommand SetProgramNo
        {
            get { return setProgramNo; }
            set { setProgramNo = value; }
        }

        public ICommand Cancel
        {
            get { return cancel; }
            set { cancel = value; }
        }

        public ICommand IAddNewProgram
        {
            get { return AddNewProgram; }
            set { AddNewProgram = value; }
        }

        public ICommand IDeleteProgram
        {
            get { return DeleteProgram; }
            set { DeleteProgram = value; }
        }

        #endregion

        #region --Constructor--
        public CommonMarkingConditionsWindow_ViewModel()
        {
            _keyenceCommunicationService = new DataAccessLayer.KeyenceCommunicationService_Serialport(new SerialPort());
            InitializingCommonMarkingConditionsList();
            InitializingCommands();
        }

        public CommonMarkingConditionsWindow_ViewModel(SerialPort sp)
        {
            _keyenceCommunicationService = new DataAccessLayer.KeyenceCommunicationService_Serialport(sp);
            InitializingCommonMarkingConditionsList();
            InitializingCommands();
        }

        public void InitializingCommands()
        {
            IMoveToFirstProgram = new RelayCommand(obj=>moveToFirstProgram());
            IMoveToLastProgram = new RelayCommand(obj => moveToLastProgram());
            IMoveToNextProgram = new RelayCommand(obj => moveToNextProgram());
            IMoveToPreviousProgram = new RelayCommand(obj => moveToPreviousProgram());
            IUploadParameters = new RelayCommand(obj => uploadParameters());
            IDownloadParameters = new RelayCommand(obj => downloadParameters());
            IAddNewProgram = new RelayCommand(obj => addNewCommonMarkingConditions());
            IDeleteProgram = new RelayCommand(obj => deleteCurrentCommonMarkingConditions(),canExec=>DeleteProgram_CanExecute());
        }
        
        public void InitializingCommonMarkingConditionsList()
        {
            commonMarkingConditionsList = new List<Model.CommonMarkingConditions>();
            addNewCommonMarkingConditions();
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

        #region --Methods For Buttons Function--
        internal void moveToFirstProgram()
        {
            CurrentIndexOfCommonMarkingConditions = 0;
        }

        internal void moveToPreviousProgram()
        {
            if (CurrentIndexOfCommonMarkingConditions >= 1)
            {
                CurrentIndexOfCommonMarkingConditions -= 1;
                CurrentCommonMarkingConditons = commonMarkingConditionsList[CurrentIndexOfCommonMarkingConditions];
            }
        }

        internal void moveToNextProgram()
        {
            if (CurrentIndexOfCommonMarkingConditions < (commonMarkingConditionsList.Count() - 1))
            {
                CurrentIndexOfCommonMarkingConditions += 1;
                CurrentCommonMarkingConditons = commonMarkingConditionsList[CurrentIndexOfCommonMarkingConditions];
            }
        }

        internal void moveToLastProgram()
        {
            CurrentIndexOfCommonMarkingConditions = (commonMarkingConditionsList.Count() - 1);
            CurrentCommonMarkingConditons = commonMarkingConditionsList[CurrentIndexOfCommonMarkingConditions];
        }

        internal void addNewCommonMarkingConditions()
        {
            CommonMarkingConditionsList.Add(new Model.CommonMarkingConditions());
            CommonMarkingConditionsListCount = CommonMarkingConditionsList.Count();
        }

       internal void deleteCurrentCommonMarkingConditions()
        {
            if (CommonMarkingConditionsList.Count > 1)
            {
                CommonMarkingConditionsList.RemoveAt(CurrentIndexOfCommonMarkingConditions);
                if (CurrentIndexOfCommonMarkingConditions > CommonMarkingConditionsList.Count() - 1) 
                    CurrentIndexOfCommonMarkingConditions = CommonMarkingConditionsList.Count() - 1;
                CurrentCommonMarkingConditons = commonMarkingConditionsList[CurrentIndexOfCommonMarkingConditions];
            }
            else
                throw new Exception("last one element in list");
        }

        internal void downloadParameters()
        {
            try
            {
                _keyenceCommunicationService.Download(CurrentCommonMarkingConditons);
            }
            catch(System.IO.IOException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        internal void uploadParameters()
        {
            _keyenceCommunicationService.Upload(CurrentCommonMarkingConditons);
        }
        #endregion

        #region --Other Methods--
        #endregion

        #region --Button Enable/Disable Control--
        public bool DeleteProgram_CanExecute()
        {
            if (commonMarkingConditionsList.Count > 1)
                return true;
            return false;
        }

        #endregion
    }
}
