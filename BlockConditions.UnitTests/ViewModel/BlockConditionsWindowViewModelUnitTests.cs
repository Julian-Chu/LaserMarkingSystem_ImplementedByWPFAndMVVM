using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockConditionsWindow.ViewModel;
using NUnit.Framework;
using BlockConditionsWindow.Model;
using NSubstitute;
using System.ComponentModel;

namespace BlockConditionsWindow.ViewModel.UnitTests
{
    [TestFixture, Apartment(System.Threading.ApartmentState.STA)]
    public class BlockConditionsWindowViewModelUnitTests
    {
        BlockConditions obj;
        BlockConditionsWindowViewModel ViewModel;

        [TestCase("K3,0,099,000,0000.827,0000.778,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.760,000.502,00.000,000,00.000,0,0,000.603,?O?R?T?f?P?Q?Q"
            , "099,000,0000.827,0000.778,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.760,000.502,00.000,000,00.000,0,0,000.603,?O?R?T?f?P?Q?Q")]
        [TestCase("K3,0,099,009,0001.715,-001.042,0000.00,0000,090.00,1,0.50,0.250,00000,01200,090.0,100,000,000,10,00,000.000,0,0,000000,0,0.040,00.100,0004,0.000,000000,0000.0,0000,035G122-%2Y%2P0W%05P0C0C"
            , "099,009,0001.715,-001.042,0000.00,0000,090.00,1,0.50,0.250,00000,01200,090.0,100,000,000,10,00,000.000,0,0,000000,0,0.040,00.100,0004,0.000,000000,0000.0,0000,035G122-%2Y%2P0W%05P0C0C")]
        [TestCase("K3,0,099,-02,0001.767,-001.467,0000.00,0000,090.00,007.840,001.770,1,0.50,0.250,00000,01200,000.0,100,000,000,%K<140429-???z-?????y>"
            , "099,-02,0001.767,-001.467,0000.00,0000,090.00,007.840,001.770,1,0.50,0.250,00000,01200,000.0,100,000,000,%K<140429-???z-?????y>")]
        [TestCase("K3,0,099,000,0001.697,0000.658,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.700,000.462,00.000,000,00.000,0,0,000.555,%2Y%2P0W%05P0C0C"
            , "099,000,0001.697,0000.658,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.700,000.462,00.000,000,00.000,0,0,000.555,%2Y%2P0W%05P0C0C")]
        [TestCase("K3,0,099,000,-000.293,-000.642,0000.00,0000,090.00,360.00,0,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.500,000.330,00.000,000,00.000,0,0,000.413,%H<000>"
            , "099,000,-000.293,-000.642,0000.00,0000,090.00,360.00,0,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.500,000.330,00.000,000,00.000,0,0,000.413,%H<000>")]
        public void BlockType_ChangeBlockType_ComboboxSelectedItemChangeToCorrectBlockType(string input, string output)
        {
            //Assign            
            ViewModel = new BlockConditionsWindowViewModel();
            obj = ViewModel.CurrentblockConditionModel;

            string actual;
            string expect;
            //Act
            obj.SortBlockConditions(input);
            ViewModel.BlockType_ChangedByBlockClass();

            actual = obj.BlockType.ToString();
            expect = ViewModel.ComboboxSelectedItem.BlockTypeIDcode.ToString();
            //Assert
            Assert.AreEqual(expect, actual);
        }

        [TestCase("K3,0,099,000,0000.827,0000.778,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.760,000.502,00.000,000,00.000,0,0,000.603,?O?R?T?f?P?Q?Q"
        , "099,000,0000.827,0000.778,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.760,000.502,00.000,000,00.000,0,0,000.603,?O?R?T?f?P?Q?Q")]
        [TestCase("K3,0,099,009,0001.715,-001.042,0000.00,0000,090.00,1,0.50,0.250,00000,01200,090.0,100,000,000,10,00,000.000,0,0,000000,0,0.040,00.100,0004,0.000,000000,0000.0,0000,035G122-%2Y%2P0W%05P0C0C"
            , "099,009,0001.715,-001.042,0000.00,0000,090.00,1,0.50,0.250,00000,01200,090.0,100,000,000,10,00,000.000,0,0,000000,0,0.040,00.100,0004,0.000,000000,0000.0,0000,035G122-%2Y%2P0W%05P0C0C")]
        [TestCase("K3,0,099,-02,0001.767,-001.467,0000.00,0000,090.00,007.840,001.770,1,0.50,0.250,00000,01200,000.0,100,000,000,%K<140429-???z-?????y>"
            , "099,-02,0001.767,-001.467,0000.00,0000,090.00,007.840,001.770,1,0.50,0.250,00000,01200,000.0,100,000,000,%K<140429-???z-?????y>")]
        [TestCase("K3,0,099,000,0001.697,0000.658,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.700,000.462,00.000,000,00.000,0,0,000.555,%2Y%2P0W%05P0C0C"
            , "099,000,0001.697,0000.658,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.700,000.462,00.000,000,00.000,0,0,000.555,%2Y%2P0W%05P0C0C")]
        [TestCase("K3,0,099,000,-000.293,-000.642,0000.00,0000,090.00,360.00,0,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.500,000.330,00.000,000,00.000,0,0,000.413,%H<000>"
            , "099,000,-000.293,-000.642,0000.00,0000,090.00,360.00,0,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.500,000.330,00.000,000,00.000,0,0,000.413,%H<000>")]
        public void Download_ComboboxSelectedItemChangeToCorrectBlockType(string input, string ouput)
        {
            //Assign
            ViewModel = new BlockConditionsWindowViewModel();
            ViewModel._keyenceCommunicationService = new MockKeyenceCommunicationService(input);
            ViewModel._dialogService = new StubDialogServiceAlwaysYes();
            string actual;
            string expect;
            string[] inputStringArray = input.Split(',');
            //Act
            ViewModel.BlockConditionDownload();
            expect = inputStringArray[3];
            //actual = ViewModel.CurrentblockConditionModel.BlockType;
            ViewModel.BlockType_ChangedByBlockClass();
            ViewModel.OnBlockTypeChanged();
            actual = ViewModel.ComboboxSelectedItem.BlockTypeIDcode;

            //Assert
            Assert.AreEqual(expect, actual);
        }


        [Test]
        public void ToNextBlockNumber_OutOfIndexAndNoAddNewBlock_NoNewItemAndKeepCurrentBlock()
        {
            //Assign
            ViewModel = new BlockConditionsWindowViewModel();
            ViewModel._dialogService=new StubDialogServiceAlwaysNo();
            string actualIndex;
            string expectedIndex;
            //Act
            ViewModel.ToNextBlockNumber();
            actualIndex = ViewModel.CurrentblockConditionModel.BlockNo;
            expectedIndex = Convert.ToString(ViewModel.BlockConditionModelList.Count - 1);
            //Assert
            Assert.AreEqual(expectedIndex, actualIndex);
        }

        [Test]
        public void ToNextBlockNumber_OutOfIndexThenAddItem_AddedNewItemAndCurrentBlockMoveToNewItem()
        {
            //Assign
            ViewModel = new BlockConditionsWindowViewModel();
            ViewModel._dialogService = new StubDialogServiceAlwaysYes();
            string actualIndex;
            string expectIndex;
            //Act
            ViewModel.ToNextBlockNumber();
            actualIndex = ViewModel.CurrentblockConditionModel.BlockNo;
            expectIndex = Convert.ToString(ViewModel.BlockConditionModelList.Count - 1);
            //Assert
            Assert.AreEqual(expectIndex, actualIndex);
        }

        [Test]
        public void ToNextBlockNumber_OutOfIndexThenAddItem_AddedNewItemWhichHasTheSameProgramNo()
        {
            //Assign
            ViewModel = new BlockConditionsWindowViewModel();
            ViewModel._dialogService = new StubDialogServiceAlwaysYes();
            string actual;
            string expect;
            //Act
            ViewModel.CurrentblockConditionModel.ProgramNo = "0010";
            ViewModel.ToNextBlockNumber();
            actual = ViewModel.CurrentblockConditionModel.ProgramNo;
            expect = "0010";
            //Assert
            Assert.AreEqual(expect, actual);
        }

        [Test]
        public void ToNextBlockNumber_InIndexOfList_CurrentBlockMoveToNextOneInList()
        {
            //Assign
            ViewModel = new BlockConditionsWindowViewModel();
            string actual;
            string expect;
            //Act
            expect = Convert.ToString(int.Parse(ViewModel.CurrentblockConditionModel.BlockNo) + 1);
            ViewModel.AddNewBlockConditionInList();
            ViewModel.ToNextBlockNumber();
            actual = ViewModel.CurrentblockConditionModel.BlockNo;
            //Assert
            Assert.AreEqual(expect, actual);
        }

        [Test]
        public void ToPreviousBlockNumber_OutofIndex_KeepBlockNumber()
        {
            //Assign
            ViewModel = new BlockConditionsWindowViewModel();
            string actual;
            string expected;
            //Act
            ViewModel.CurrentblockConditionModel = ViewModel.BlockConditionModelList[0];
            ViewModel.ToPreviousBlockNumber();
            actual = ViewModel.CurrentblockConditionModel.BlockNo;
            expected = ViewModel.BlockConditionModelList[0].BlockNo;
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void ToPreviousBlockNumber_InRangeOfIndex_MoveToPreviousBlockNumber()
        {
            //Assign
            ViewModel = new BlockConditionsWindowViewModel();
            string actual;
            string expected;
            //Act
            ViewModel.AddNewBlockConditionInList();
            ViewModel.CurrentblockConditionModel = ViewModel.BlockConditionModelList[1];
            ViewModel.ToPreviousBlockNumber();
            actual = ViewModel.CurrentblockConditionModel.BlockNo;
            expected = ViewModel.BlockConditionModelList[0].BlockNo;
            //Assert
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void BlockTypeChangedEvent_Raised_true()
        {
            //Assign
            bool wasRaised = false;
            ViewModel = new BlockConditionsWindowViewModel();
            ViewModel.BlockTypeChangedEvent += (s, e) => wasRaised = true;
            //Act
            ViewModel.OnBlockTypeChanged();
            //Assert
            Assert.That(wasRaised);
        }

        [Test]
        public void PropertyChanged_Raised_true()
        {
            //Assign
            var sub = Substitute.For<INotifyPropertyChanged>();
            bool wasRaised = false;            
            //Act
            sub.PropertyChanged += (s, e) => wasRaised = true;
            sub.PropertyChanged += Raise.Event<PropertyChangedEventHandler>(this, new PropertyChangedEventArgs(""));
            //Assert
            Assert.That(wasRaised);
        }




    }
}
