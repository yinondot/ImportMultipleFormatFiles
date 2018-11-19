using ImportMultipleFormatFiles.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImportMultipleFormatFiles.Commands
{
  public  class RunCommand : ICommand
    {
      public MainViewModel Vm { get; set; }
      public RunCommand(MainViewModel vm)
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

         if (Vm.ChosenFiles.Any(file => file.IsChecked == true) && ((Vm.Visible==System.Windows.Visibility.Visible && Vm.DefinitionFilePath != "") || Vm.Visible == System.Windows.Visibility.Hidden) )
         {
            return true;
         }
         return false;
      }

      public  void Execute(object parameter)
      {
         //Task task=new Task(()=> Vm.RunMethod());
         // task.Start();
         Vm.RunMethod();
      }
   }
}

