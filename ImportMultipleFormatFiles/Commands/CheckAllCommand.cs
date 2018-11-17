using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ImportMultipleFormatFiles.ViewModel;

namespace ImportMultipleFormatFiles.Commands
{
   public class CheckAllCommand : ICommand
   {
      public MainViewModel Vm { get; set; }
      public CheckAllCommand(MainViewModel vm)
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
         if (Vm.ChosenFiles.Count == 0)
         {
            return false;
         }
         return true;
      }

      public void Execute(object parameter)
      {
         Vm.CheckAllMethod();
      }
   }
}
