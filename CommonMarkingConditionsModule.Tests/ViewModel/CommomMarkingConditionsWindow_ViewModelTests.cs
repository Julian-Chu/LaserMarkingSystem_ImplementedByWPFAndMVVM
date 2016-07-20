using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonMarkingConditionsModule.ViewModel;
using NUnit.Framework;
using NSubstitute;


namespace CommonMarkingConditionsModule.ViewModel.Tests
{
    [TestFixture()]
    public class CommomMarkingConditionsWindow_ViewModelTests
    {
        [Test()]
        public void Constructor_CheckInitialIndexEquals0_IndexEquals0()
        {
            //Assign
            CommonMarkingConditionsWindow_ViewModel viewModel = new CommonMarkingConditionsWindow_ViewModel();
            int expect = 0;
            int act = viewModel.CurrentIndexOfCommonMarkingConditions;
            //Assert
            Assert.AreEqual(expect, act);
        }

        [Test()]
        public void AddNewCommonMarkingConditions_IntializingVewModelAndAddNewCommonMarkingConditions_CountEquals2()
        {
            //Assign
            CommonMarkingConditionsWindow_ViewModel viewModel = new CommonMarkingConditionsWindow_ViewModel();
            //Act            
            int expect = viewModel.CommonMarkingConditionsList.Count()+1;
            viewModel.addNewCommonMarkingConditions();
            int act=viewModel.CommonMarkingConditionsList.Count();
            //Assert
            Assert.AreEqual(expect, act);
        }

        [Test()]
        public void DeleteCurrentCommonMarkingConditions_AddOneInNewCommonMakringConditionsAndDeleteOne_CountEquals1()
        {
            //Assign
            CommonMarkingConditionsWindow_ViewModel viewModel = new CommonMarkingConditionsWindow_ViewModel();
            //Act
            viewModel.addNewCommonMarkingConditions();
            int expect = 1;
            viewModel.deleteCurrentCommonMarkingConditions();
            int act = viewModel.CommonMarkingConditionsList.Count();
            //Assert
            Assert.AreEqual(expect, act);
        }

        [Test()]
        public void DeleteCurrentCommonMarkingConditions_NewCommonMakringConditionsAndDeleteOne_ThrowException()
        {
            //Assign
            CommonMarkingConditionsWindow_ViewModel viewModel = new CommonMarkingConditionsWindow_ViewModel();
            //Act
            string expect="last one";
            
            //Assert
            var act=Assert.Catch<Exception>(()=>viewModel.deleteCurrentCommonMarkingConditions());
            StringAssert.Contains(expect, act.Message);
        }

        [Test()]
        public void Constructor_CheckCountOfInitialCommonMarkingConditionsList_Only1()
        {
            //Assign
            CommonMarkingConditionsWindow_ViewModel viewModel = new CommonMarkingConditionsWindow_ViewModel();
            //Act
            int expect = 1;
            int act = viewModel.CommonMarkingConditionsList.Count();
            //Assert
            Assert.AreEqual(expect, act);
        }

        [Test()]
        public void MoveToFirstProgram_AssignIndexEquals2AndThenExecuteThis_IndexEquals0()
        {
            //Assign
            CommonMarkingConditionsWindow_ViewModel viewModel = new CommonMarkingConditionsWindow_ViewModel();
            //Act
            viewModel.currentIndexOfCommonMarkingConditions = 2;
            viewModel.moveToFirstProgram();
            int expect = 0;
            int act = viewModel.CurrentIndexOfCommonMarkingConditions;
            //Assert
            Assert.AreEqual(expect, act);
        }

        [Test()]
        public void MoveToPreviousProgram_AssignIndexEquals2AndThenExecuteThis_IndexEquals1()
        {
            //Assign
            CommonMarkingConditionsWindow_ViewModel viewModel = new CommonMarkingConditionsWindow_ViewModel();
            //Act
            viewModel.addNewCommonMarkingConditions();
            viewModel.addNewCommonMarkingConditions();
            viewModel.currentIndexOfCommonMarkingConditions = 2;
            viewModel.moveToPreviousProgram();
            int expect = 1;
            int act = viewModel.CurrentIndexOfCommonMarkingConditions;
            //Assert
            Assert.AreEqual(expect, act);
        }

        [Test()]
        public void MoveToLastProgram_Add2ItemsInListAndThenExecureThis_IndexEquals2()
        {
            //Assign
            CommonMarkingConditionsWindow_ViewModel viewModel = new CommonMarkingConditionsWindow_ViewModel();
            viewModel.addNewCommonMarkingConditions();
            viewModel.addNewCommonMarkingConditions();
            //Act
            viewModel.moveToLastProgram();
            int expect = 2;
            int act = viewModel.CurrentIndexOfCommonMarkingConditions;
            //Assert
            Assert.AreEqual(expect, act);

        }
        
        [Test()]
        public void MoveToNextProgram_Add2ItemsInListAndThenExecureThis_IndexEquals1()
        {
            //Assign
            CommonMarkingConditionsWindow_ViewModel viewModel = new CommonMarkingConditionsWindow_ViewModel();
            //Act
            viewModel.addNewCommonMarkingConditions();
            viewModel.addNewCommonMarkingConditions();
            viewModel.moveToNextProgram();
            int expect = 1;
            int act = viewModel.CurrentIndexOfCommonMarkingConditions;
            //Assert
            Assert.AreEqual(expect, act);
        }

        [Test()]
        public void DownloadParameters_ExecuteThis_ExecutedIsTrue()
        {
            //Assign
            CommonMarkingConditionsWindow_ViewModel viewModel = new CommonMarkingConditionsWindow_ViewModel();
            bool DownloadExecuted=false;
            var keyenceCommunication = Substitute.For<DataAccessLayer.IKeyenceCommunication>();
            keyenceCommunication.When(x => x.Download(Arg.Any<Model.CommonMarkingConditions>())).Do(x => DownloadExecuted = true);
            viewModel._keyenceCommunicationService = keyenceCommunication;
            //Act
            viewModel.downloadParameters();
            //Assert
            Assert.True(DownloadExecuted);
        }

        [Test()]
        public void UploadParameters_ExecuteThis_ExecutedIsTrue()
        {
            //Assign
            CommonMarkingConditionsWindow_ViewModel viewModel = new CommonMarkingConditionsWindow_ViewModel();
            bool UploadExecuted = false;
            var keyenceCommunication=Substitute.For<DataAccessLayer.IKeyenceCommunication>();
            keyenceCommunication.When(x => x.Upload(Arg.Any<Model.CommonMarkingConditions>())).Do(x => UploadExecuted = true);
            viewModel._keyenceCommunicationService = keyenceCommunication;
            //Act
            viewModel.uploadParameters();
            //Assert
            Assert.True(UploadExecuted);
        }

        

        
    }
}
