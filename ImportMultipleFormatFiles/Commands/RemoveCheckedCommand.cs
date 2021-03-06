﻿using ImportMultipleFormatFiles.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ImportMultipleFormatFiles.Commands
{
  public  class RemoveCheckedCommand:ICommand
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
         return true;
         //if (Vm.ChosenFiles.Count != 0 && Vm.ChosenFiles.Any(file => file.isChecked == true))
         //{
         //   return true;
         //}
         //return false;
      }

      public void Execute(object parameter)
      {
         Vm.RemoveCheckedMethod();
      }
   }
}
