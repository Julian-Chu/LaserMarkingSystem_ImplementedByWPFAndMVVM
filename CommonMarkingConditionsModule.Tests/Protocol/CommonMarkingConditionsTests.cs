using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonMarkingConditionsModule;
using NUnit.Framework;

namespace CommonMarkingConditionsModule.UnitTests
{
    [TestFixture()]
    public class CommonMarkingConditionsTests
    {

        [Test]
        public void TestSettingProperty_Input_SettingAsExpected()
        {
            ///Arrange
            CommonMarkingConditionsModule.Protocol.CommonMarkingConditions obj = new CommonMarkingConditionsModule.Protocol.CommonMarkingConditions();
            string input = "K1,0,0,0,0,0,0,0,000.50,0000.0,0000,0000.0,0000.000,0000.000,00,00001,0000.0,0000.0,00000,00000,2,1\r";
            string actual;
            //string expect = "0,0,0,0,0,0,000.50,0000.0,0,0,0000.000,0000.000,00,00001,0000.0,0000.0,00000,00000,2,1\r";
            string expect = "0,0,0,0,0,0,000.50,0000.0,0000,0000.0,0000.000,0000.000,00,00001,0000.0,0000.0,00000,00000,2,1\r";

            ///Act
            obj.SettingFromLMController = input;
            actual = obj.SettingToLMController;

            ///Assert
            string[] actualArray = actual.Split('\r', ',', ' ').Where(x => x != "").ToArray();
            string[] expectArray = expect.Split('\r', ',', ' ').Where(x => x != "").ToArray();

            List<double> actualList = new List<double>();
            foreach (var element in actualArray)
            {
                actualList.Add(Convert.ToDouble(element));
            }
            List<double> expectList = new List<double>();

            foreach (var element in expectArray)
            {
                expectList.Add(Convert.ToDouble(element));
            }

            Assert.AreEqual(expectList, actualList);
        }
        [Ignore]
        [Test()]
        public void DownloadMarkingConditionsTest()
        {
            Assert.Fail();
        }
        [Ignore]
        [Test()]
        public void UploadMarkingConditionsTest()
        {
            Assert.Fail();
        }
        [Ignore]
        [Test()]
        public void CloneTest()
        {
            Assert.Fail();
        }
    }
}
