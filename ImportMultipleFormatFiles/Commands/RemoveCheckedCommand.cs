using ImportMultipleFormatFiles.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImportMultipleFormatFiles.Commands
{
    class RemoveCheckedCommand:ICommand
   {
      public MainViewModel Vm { get; set; }
      public RemoveCheckedCommand(MainViewModel vm)
      {
         this.Vm = vm;
      }
      public event EventHandler CanExecuteChanged
      {
         add { CommandManager.RequerySuggested += value; }
         remove { CommandManager.RequerySuggested -= value; }

      }
      public bool CanExecute(object parameter)
      {
         if (Vm.ChosenFiles.Count == 0 || Vm.ChosenFiles.Any(file => file.isChecked == true))
         {
            return false;
         }
         return true;
      }

      public void Execute(object parameter)
      {
         Vm.RemoveCheckedMethod();
      }
   }
}
