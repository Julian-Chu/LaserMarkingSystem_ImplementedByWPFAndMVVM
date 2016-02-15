using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject
{
    public class MockCommonMarkingConditions:CommonMarkingConditionsModule.Protocol.CommonMarkingConditions
    {
        public override void DownloadMarkingConditions(string ProgramNo)
        {
           
        }

        public override void UploadMarkingConditions(string ProgramNo)
        {
            base.UploadMarkingConditions(ProgramNo);
        }
    
    }

    [Ignore]
    [TestClass]
    public class UnitTest_CommonMarkingConditionsModule
    {
        [TestMethod]
        public void K1_CommonMarkingConditionsTesting()
        {
            ///Testing Response and Setting Properties
            MockCommonMarkingConditions mockObject = new MockCommonMarkingConditions();
            string inputString = "K1,0,0,0,0,0,0,0,000.50,0000.0,0000,0000.0,0000.000,0000.000,00,00001,0000.0,0000.0,00000,00000,2,1\r";
            
            mockObject.SettingFromLMController = inputString; 
            string actual = mockObject.SettingToLMController;
            string[] actualArray = actual.Split('\r', ',',' ');
            
            string expect = "0,0,0,0,0,0,000.50,0000.0,0000,0000.0,0000.000,0000.000,00,00001,0000.0,0000.0,00000,00000,2,1\r" ;
            string[] expectArray = expect.Split(',', '\r',' ');
            
            bool compareResult = true;

            for (int i = 0; i < actualArray.Length-1;i++ )
            {
                if (Convert.ToDouble(actualArray[i]) == Convert.ToDouble(expectArray[i]))
                { }
                else
                    compareResult = false;
            }
                Assert.IsTrue(compareResult);
        }

  
    }
}
