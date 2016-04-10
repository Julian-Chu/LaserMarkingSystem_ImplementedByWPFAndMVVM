using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockConditionsWindow.Model;
using NUnit.Framework;
namespace BlockConditionsWindow.Model.UnitTests
{
    [TestFixture()]
    public class ErrorCodeUnitTests
    {
        string requestingError = "EX,1,E001,E007,W000,\r";
        string ErrorResponse = "K3,1,S000";
        [Test()]
        public void IsNoErrorExistsTest_RequestingError()
        {
            //Assign
            ErrorCode errCode = new ErrorCode();
            //Act
            var actual = Assert.Catch<Exception>(() => errCode.IsNoErrorExists(requestingError));
            //Assert
            StringAssert.Contains("E001", actual.Message);
            StringAssert.Contains("E007", actual.Message);
            StringAssert.Contains("W000", actual.Message);
        }

        [Test()]
        public void IsNoErrorExistsTest_ErrorResponse()
        {
            //Assign
            ErrorCode errCode = new ErrorCode();
            //Act
            var actual = Assert.Catch<Exception>(() => errCode.IsNoErrorExists(ErrorResponse));
            //Assert
            StringAssert.Contains("S000", actual.Message);
        }
    }
}
