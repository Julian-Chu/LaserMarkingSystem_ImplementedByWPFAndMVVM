using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BlockConditionsWindow.ViewModel
{
    interface IDialogService
    {
         MessageBoxResult ShowMessageBox();
    }


    public class DialogServiceToAddNewBlockCondition:IDialogService
    {

        public MessageBoxResult ShowMessageBox()
        {
            return MessageBox.Show("Add new BlockCondition?", "Warning", MessageBoxButton.YesNo);
        }
    }

    public class MockDialogServiceAlwaysYes:IDialogService
    {
        public MessageBoxResult ShowMessageBox()
        {
            return MessageBoxResult.Yes;
        }
    }

    public class MockDialogServiceAlwaysNo:IDialogService
    {
        public MessageBoxResult ShowMessageBox()
        {
            return MessageBoxResult.No;
        }
    }
}
