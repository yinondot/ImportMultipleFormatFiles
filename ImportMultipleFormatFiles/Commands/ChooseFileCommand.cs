using ImportMultipleFormatFiles.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImportMultipleFormatFiles.Commands
{

   public class ChooseFileCommand : ICommand
   {
      public MainViewModel Vm { get; set; }

      public ChooseFileCommand(MainViewModel vm)
      {
         Vm = vm;
      }
      public event EventHandler CanExecuteChanged
      {
         add { CommandManager.RequerySuggested += value; }
         remove { CommandManager.RequerySuggested -= value; }

      }

      public bool CanExecute(object parameter)
      {
         return Vm.Format != "";
      }

      public void Execute(object parameter)
      {
         Vm.ChooseFileMethod();

      }
   }
}
