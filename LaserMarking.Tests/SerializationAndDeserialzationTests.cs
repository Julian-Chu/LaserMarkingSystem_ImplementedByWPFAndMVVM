using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LaserMarking;
using NUnit.Framework;
using BlockConditionsWindow.Model;
using CommonMarkingConditionsModule.Model;



namespace LaserMarking.UnitTests
{
    [TestFixture()]
    public class SerializationAndDeserialzationTests
    {
        [Ignore]
        [Test()]
        public void Serialize_SerializeAndDeserialize_ReturnsTrue()
        {
            CommonMarkingConditionsWithSerialPort _commonMarkingConditions = new CommonMarkingConditionsWithSerialPort();
            List<BlockConditions> _blockConditionsList = new List<BlockConditions>()
            {
                new BlockConditions(),
                new BlockConditions(),    
            };
            string input = "K1,0,0,0,0,0,0,0,000.50,0000.0,0000,0000.0,0000.000,0000.000,00,00001,0000.0,0000.0,00000,00000,2,1\r";
            _commonMarkingConditions.SettingFromLMController = input;

            input = "K3,0,099,000,0000.827,0000.778,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.760,000.502,00.000,000,00.000,0,0,000.603,?O?R?T?f?P?Q?Q";
            _blockConditionsList[0].SortBlockConditions(input);

            input="K3,0,099,009,0001.715,-001.042,0000.00,0000,090.00,1,0.50,0.250,00000,01200,090.0,100,000,000,10,00,000.000,0,0,000000,0,0.040,00.100,0004,0.000,000000,0000.0,0000,035G122-%2Y%2P0W%05P0C0C";

            SerializationAndDeserialzation SAD = new SerializationAndDeserialzation();

            
            Assert.Fail();
        }
    }


    
}
