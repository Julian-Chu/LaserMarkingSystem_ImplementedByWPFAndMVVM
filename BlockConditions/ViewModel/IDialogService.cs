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
        MessageBoxResult ShowMessageBox(string Content, string title, MessageBoxButton button);
    }



    public class DialogServiceToAddNewBlockCondition : IDialogService
    {
        public MessageBoxResult ShowMessageBox(string Content, string title, MessageBoxButton button)
        {
            return MessageBox.Show(Content, title, button);
        }
    }

    public class StubDialogServiceAlwaysYes : IDialogService
    {
        public MessageBoxResult ShowMessageBox(string Content, string title, MessageBoxButton button)
        {
            return MessageBoxResult.Yes;
        }
    }

    public class StubDialogServiceAlwaysNo : IDialogService
    {
        public MessageBoxResult ShowMessageBox(string Content, string title, MessageBoxButton button)
        {
            return MessageBoxResult.No;
        }
    }
}
