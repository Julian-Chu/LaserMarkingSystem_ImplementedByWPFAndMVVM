using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlockConditionsWindow.ViewModel;
using NUnit.Framework;


namespace BlockConditionsWindow.ViewModel.UnitTests
{
    [TestFixture()]
    public class RelayCommandUnitTests
    {
        RelayCommand command;
        bool _statusToChange;
        public void ChangeStatusToTrue(object obj)
        {
            _statusToChange = true;
        }
        public RelayCommand RelayCommandFactory(Action<object> method, bool CanCommandExecute)
        {
            return new RelayCommand(ChangeStatusToTrue, _ => CanCommandExecute);
        }
        bool CommandCanExecute = true;
        bool CommandCannotExecute = false;

        [Test()]
        public void Execute_InvokeMethodToChangeTrue_StatusChangeToTrue()
        {
            //Arrange            
            _statusToChange = false;            
            command = RelayCommandFactory(ChangeStatusToTrue, CommandCanExecute);
            //Act
            command.Execute(null);
            //Assert
            Assert.IsTrue(_statusToChange);
        }

        [Test()]
        public void Execute_InvokeMethodToChangeTrueWithCannotExecute_StatusDoesnotChange()
        {
            //Arrange
            _statusToChange = false;
            command = RelayCommandFactory(ChangeStatusToTrue, CommandCannotExecute);
            //Act
            command.Execute(null);
            //Assert
            Assert.IsFalse(_statusToChange);
        }

        [Test()]
        public void OnCanExecuteChanged_fireCanExecutedChangedEvent_OnCanExecuteChangedIsFired()
        {
            //Arragne
            bool OnCanExecuteChanged_Fired = false;
            command = RelayCommandFactory(ChangeStatusToTrue, CommandCanExecute);
            command.CanExecuteChanged += delegate { OnCanExecuteChanged_Fired = true; };
            //Act
            command.OnCanExecuteChanged();            
            //Assert
            Assert.IsTrue(OnCanExecuteChanged_Fired);
        }

        [Test()]
        public void Destroy_ImplementDestroy_StatusDoesntChange()
        {
            //Arrange
            bool _statusToChange = false;
            command = RelayCommandFactory(ChangeStatusToTrue, CommandCanExecute);
            //Act
            command.Destroy();
            //Assert
            Assert.IsFalse(_statusToChange);
        }
    }
}
