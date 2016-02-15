using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    [Ignore()]
    [TestClass]
    public class BlockConditionsTests
    {
        [TestMethod]
        public void K3_BlockConditionsTesting_1()
        {
            mockBlockConditions mockObject = new mockBlockConditions();
            string input = "K3,0,099,000,0000.827,0000.778,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.760,000.502,00.000,000,00.000,0,0,000.603,?O?R?T?f?P?Q?Q";
            string[] inputArray = input.Split(',', '\r');

            mockObject.SortBlockConditions(inputArray);
            string actual = mockObject.Setting;
            string expect = "099,000,0000.827,0000.778,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.760,000.502,00.000,000,00.000,0,0,000.603,?O?R?T?f?P?Q?Q";

            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void K3_BlockConditionsTesting_2()
        {
            mockBlockConditions mockObject = new mockBlockConditions();
            string input = "K3,0,099,009,0001.715,-001.042,0000.00,0000,090.00,1,0.50,0.250,00000,01200,090.0,100,000,000,10,00,000.000,0,0,000000,0,0.040,00.100,0004,0.000,000000,0000.0,0000,035G122-%2Y%2P0W%05P0C0C";
            string[] inputArray = input.Split(',', '\r');

            mockObject.SortBlockConditions(inputArray);
            string actual = mockObject.Setting;
            string expect = "099,009,0001.715,-001.042,0000.00,0000,090.00,1,0.50,0.250,00000,01200,090.0,100,000,000,10,00,000.000,0,0,000000,0,0.040,00.100,0004,0.000,000000,0000.0,0000,035G122-%2Y%2P0W%05P0C0C";

            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void K3_BlockConditionsTesting_3()
        {
            mockBlockConditions mockObject = new mockBlockConditions();
            string input = "K3,0,099,-02,0001.767,-001.467,0000.00,0000,090.00,007.840,001.770,1,0.50,0.250,00000,01200,000.0,100,000,000,%K<140429-???z-?????y>";
            string[] inputArray = input.Split('\r', ',');

            mockObject.SortBlockConditions(inputArray);
            string actual = mockObject.Setting;
            string expect = "099,-02,0001.767,-001.467,0000.00,0000,090.00,007.840,001.770,1,0.50,0.250,00000,01200,000.0,100,000,000,%K<140429-???z-?????y>";

            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void K3_BlockConditionsTesting_4()
        {
            mockBlockConditions mockObject = new mockBlockConditions();
            string input = "K3,0,099,000,0001.697,0000.658,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.700,000.462,00.000,000,00.000,0,0,000.555,%2Y%2P0W%05P0C0C";
            string[] inputArray = input.Split('\r', ',');

            mockObject.SortBlockConditions(inputArray);
            string actual = mockObject.Setting;
            string expect = "099,000,0001.697,0000.658,0000.00,0000,090.00,360.00,1,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.700,000.462,00.000,000,00.000,0,0,000.555,%2Y%2P0W%05P0C0C";

            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void K3_BlockConditionsTesting_5()
        {
            mockBlockConditions mockObject = new mockBlockConditions();
            string input = "K3,0,099,000,-000.293,-000.642,0000.00,0000,090.00,360.00,0,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.500,000.330,00.000,000,00.000,0,0,000.413,%H<000>";
            string[] inputArray = input.Split('\r', ',');

            mockObject.SortBlockConditions(inputArray);
            string actual = mockObject.Setting;
            string expect = "099,000,-000.293,-000.642,0000.00,0000,090.00,360.00,0,0.50,0.250,00000,01200,090.0,100,000,000,00,00,000.500,000.330,00.000,000,00.000,0,0,000.413,%H<000>";

            Assert.AreEqual(expect, actual);
        }
    }

    public class mockBlockConditions: BlockConditionsWindow.Protocol.BlockConditions
    {

    }

}
