using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaserMarking.View;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows;

namespace LaserMarking.View.Tests
{
    [TestClass]
    public class LaserMarkerWindowTests
    {


        [TestMethod]
        public void SerializationAndDeserializationTesting()
        {
            //arrange
            ProgramNoSetting.Controller.CommonMarkingConditionsWithSerialPort _commonMarkingConditions = new ProgramNoSetting.Controller.CommonMarkingConditionsWithSerialPort() ;
            List<BlockConditionsWindow.Model.BlockConditionsWithSerialPort> _blockConditionsList = new List<BlockConditionsWindow.Model.BlockConditionsWithSerialPort>{
                new BlockConditionsWindow.Model.BlockConditionsWithSerialPort(){BlockNo="0"},
                new BlockConditionsWindow.Model.BlockConditionsWithSerialPort(){BlockNo="1"}
            };
            SerializationAndDeserialzation SAD = new SerializationAndDeserialzation();
            SerializationAndDeserialzation newSAD = new SerializationAndDeserialzation();

            //act
            SAD.commonMarkingConditions=_commonMarkingConditions;
            SAD.blockConditionsList=_blockConditionsList;
            SAD.Serialize(_commonMarkingConditions, _blockConditionsList);
            var expected = newSAD.Deserialize();
            
            //assert
            Assert.AreEqual(SAD.blockConditionsList[0].Setting, expected.blockConditionsList[0].Setting);
            

        }

        [TestMethod]
        public void SerializationAndDeserializationTesting_SubTesting1()
        {            //assert
            //arrange
            ProgramNoSetting.Controller.CommonMarkingConditionsWithSerialPort _commonMarkingConditions = new ProgramNoSetting.Controller.CommonMarkingConditionsWithSerialPort();
            List<BlockConditionsWindow.Model.BlockConditionsWithSerialPort> _blockConditionsList = new List<BlockConditionsWindow.Model.BlockConditionsWithSerialPort>{
                new BlockConditionsWindow.Model.BlockConditionsWithSerialPort(){BlockNo="0",BlockType="001"},
                new BlockConditionsWindow.Model.BlockConditionsWithSerialPort(){BlockNo="1" ,BlockType="001"}
            };
            SerializationAndDeserialzation SAD = new SerializationAndDeserialzation();
            SerializationAndDeserialzation newSAD = new SerializationAndDeserialzation();

            //act
            SAD.commonMarkingConditions = _commonMarkingConditions;
            SAD.blockConditionsList = _blockConditionsList;
            SAD.Serialize(_commonMarkingConditions, _blockConditionsList);
            var expected = newSAD.Deserialize();

            //assert
            Assert.AreEqual(SAD.blockConditionsList[1].Setting, expected.blockConditionsList[1].Setting);
        }

        [TestMethod]
        public void SerializationAndDeserializationTesting_SubTesting2()
        {            //assert
            //arrange
            ProgramNoSetting.Controller.CommonMarkingConditionsWithSerialPort _commonMarkingConditions = new ProgramNoSetting.Controller.CommonMarkingConditionsWithSerialPort();
            List<BlockConditionsWindow.Model.BlockConditionsWithSerialPort> _blockConditionsList = new List<BlockConditionsWindow.Model.BlockConditionsWithSerialPort>{
                new BlockConditionsWindow.Model.BlockConditionsWithSerialPort(){BlockNo="0",BlockType="001"},
                new BlockConditionsWindow.Model.BlockConditionsWithSerialPort(){BlockNo="1" ,BlockType="001"}
            };
            SerializationAndDeserialzation SAD = new SerializationAndDeserialzation();
            SerializationAndDeserialzation newSAD = new SerializationAndDeserialzation();

            //act
            SAD.commonMarkingConditions = _commonMarkingConditions;
            SAD.blockConditionsList = _blockConditionsList;
            SAD.Serialize(_commonMarkingConditions, _blockConditionsList);
            var expected = newSAD.Deserialize();

            //assert
            Assert.AreEqual(SAD.commonMarkingConditions.SettingToLaserMarkingController, expected.commonMarkingConditions.SettingToLaserMarkingController);
        }
    }
}
