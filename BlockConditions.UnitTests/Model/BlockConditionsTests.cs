using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockConditionsWindow.Model;

namespace BlockConditionsWindow.View.UnitTests
{
    [TestFixture]
    public class BlockConditionsTests
    {
        BlockConditions obj;

        [TestCase("1999")]
        [TestCase("0000")]
        public void ProgramNo_InputValueInRange_ReturnsInputValue(string input)
        {
            //Arrange
            BlockConditions testObj=new BlockConditions();
            string expected = input;
            string actual;
            //Act
            testObj.ProgramNo = input;
            actual = testObj.ProgramNo;
            //Assert
            Assert.AreSame(expected, actual);
        }
        [TestCase("2000")]
        [TestCase("-1")]
        [TestCase("abcd")]
        public void ProgramNo_InputValueNoOutOfRange_ThrowException(string input)
        {
            //Arrange
            BlockConditions obj = new BlockConditions();
            string expected = "Please input ProgramNo";
            //Act
            var actual = Assert.Catch<ArgumentOutOfRangeException>(() => obj.ProgramNo = input);
            //Assert
            StringAssert.Contains(expected, actual.Message);            
        }

        [TestCase("000")]
        [TestCase("255")]
        public void BlockNo_InputValueInRange_ReturnsInputValue(string input)
        {
            //Arrange
            obj = new BlockConditions();
            string expected = input;
            string actual;
            //Act
            obj.BlockNo = expected;
            actual = obj.BlockNo;
            //Assert
            Assert.AreSame(expected, actual);
        }

        [TestCase("-1")]
        [TestCase("256")]
        public void BlockNo_InputValueOutOfRange_ThrowException(string input)
        {
            //Arrange
            obj = new BlockConditions();
            string expected = "Please input BlockNo";
            //Act      
            var actualException = Assert.Catch(() => obj.BlockNo = input);
            //Assert
            StringAssert.Contains(expected, actualException.Message);        
        }

        [Test]
        public void BlockType_InputValueInRange_ReturnsInputValue()
        {
            //Arrange
            obj = new BlockConditions();
            string expected = "-04";
            string actual;
            //Act
            obj.BlockType = expected;
            actual = obj.BlockType;
            //Assert
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void BlockType_InputValueOutOfRange_ThrowsException()
        {
            //Arrange
            obj = new BlockConditions();
            string input = "010";
            string expected = "Invalid Value";
            //Act
            var actualException = Assert.Catch<ArgumentOutOfRangeException>(() => obj.BlockType = input);

            //Assert
            StringAssert.Contains(expected, actualException.Message);            
        }
                
        [TestCase("K3,0,099,000,0000.827,0000.778,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.760,000.502,00.000,000,00.000,0,0,000.603,?O?R?T?f?P?Q?Q"
            , "099,000,0000.827,0000.778,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.760,000.502,00.000,000,00.000,0,0,000.603,?O?R?T?f?P?Q?Q")]
        [TestCase("K3,0,099,009,0001.715,-001.042,0000.00,0000,090.00,1,0.50,0.250,00000,01200,090.0,100,000,000,10,00,000.000,0,0,000000,0,0.040,00.100,0004,0.000,000000,0000.0,0000,035G122-%2Y%2P0W%05P0C0C"
            ,"099,009,0001.715,-001.042,0000.00,0000,090.00,1,0.50,0.250,00000,01200,090.0,100,000,000,10,00,000.000,0,0,000000,0,0.040,00.100,0004,0.000,000000,0000.0,0000,035G122-%2Y%2P0W%05P0C0C")]
        [TestCase("K3,0,099,-02,0001.767,-001.467,0000.00,0000,090.00,007.840,001.770,1,0.50,0.250,00000,01200,000.0,100,000,000,%K<140429-???z-?????y>"
            , "099,-02,0001.767,-001.467,0000.00,0000,090.00,007.840,001.770,1,0.50,0.250,00000,01200,000.0,100,000,000,%K<140429-???z-?????y>")]
        [TestCase("K3,0,099,000,0001.697,0000.658,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.700,000.462,00.000,000,00.000,0,0,000.555,%2Y%2P0W%05P0C0C"
            , "099,000,0001.697,0000.658,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.700,000.462,00.000,000,00.000,0,0,000.555,%2Y%2P0W%05P0C0C")]
        [TestCase("K3,0,099,000,-000.293,-000.642,0000.00,0000,090.00,360.00,0,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.500,000.330,00.000,000,00.000,0,0,000.413,%H<000>"
            , "099,000,-000.293,-000.642,0000.00,0000,090.00,360.00,0,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.500,000.330,00.000,000,00.000,0,0,000.413,%H<000>")]
        public void BlockConditions_InputCorrectStringsByCommandK3_ReturnTheSameStringAsInput(string input, string output)
        {
            //Assign
            obj = new BlockConditions();
            string actual;
            string expect;
            //Act
            obj.SortBlockConditions(input);
            actual = obj.Setting;
            expect = output;
            //Assert
            Assert.AreEqual(expect, actual);
        }
    }
}
