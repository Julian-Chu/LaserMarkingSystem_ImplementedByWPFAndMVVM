using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace BlockConditionsWindow.ViewModel
{
    interface IDialogService
<<<<<<< HEAD
    {        
=======
    {
>>>>>>> 529f57d9a520ad54fb73546f7a54103842f4e447
        MessageBoxResult ShowMessageBox(string Content, string title, MessageBoxButton button);
    }


<<<<<<< HEAD

=======
>>>>>>> 529f57d9a520ad54fb73546f7a54103842f4e447
    public class DialogServiceToAddNewBlockCondition : IDialogService
    {
        public MessageBoxResult ShowMessageBox(string Content, string title, MessageBoxButton button)
        {
            return MessageBox.Show(Content, title, button);
        }
    }

<<<<<<< HEAD
    public class StubDialogServiceAlwaysYes : IDialogService
=======
    public class MockDialogServiceAlwaysYes : IDialogService
>>>>>>> 529f57d9a520ad54fb73546f7a54103842f4e447
    {
        public MessageBoxResult ShowMessageBox(string Content, string title, MessageBoxButton button)
        {
            return MessageBoxResult.Yes;
        }
    }

<<<<<<< HEAD
    public class StubDialogServiceAlwaysNo : IDialogService
=======
    public class MockDialogServiceAlwaysNo : IDialogService
>>>>>>> 529f57d9a520ad54fb73546f7a54103842f4e447
    {
        public MessageBoxResult ShowMessageBox(string Content, string title, MessageBoxButton button)
        {
            return MessageBoxResult.No;
        }
    }
}
