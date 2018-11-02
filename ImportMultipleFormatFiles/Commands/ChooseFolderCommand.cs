using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.WindowsAPICodePack.Dialogs;
using ImportMultipleFormatFiles.ViewModel;

namespace ImportMultipleFormatFiles.Commands
{
   public class ChooseFolderCommand : ICommand
   {
      public MainViewModel Vm { get; set; }

      public ChooseFolderCommand(MainViewModel vm)
      {
         Vm = vm;
      }
      public event EventHandler CanExecuteChanged;

      public bool CanExecute(object parameter)
      {
         return true;
      }

      public void Execute(object parameter)
      {
         Vm.ChooseFolderMethod();

      }
   }
}
