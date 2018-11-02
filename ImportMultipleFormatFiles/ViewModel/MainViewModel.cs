using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportMultipleFormatFiles.Models;
using ImportMultipleFormatFiles.Converters;
using ImportMultipleFormatFiles.Commands;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace ImportMultipleFormatFiles.ViewModel
{
   public class MainViewModel
    {
    public   ComboNameConverter comboNameConverter;
     public ComboTextWidthConverter comboTextWidthConverter;

      public ChooseFolderCommand ChooseFolderCommand { get; set; }

      public MainViewModel()
      {
         comboNameConverter = new ComboNameConverter();
         comboTextWidthConverter = new ComboTextWidthConverter();

         ChooseFolderCommand = new ChooseFolderCommand(this);
      }


      public void ChooseFolderMethod()
      {
         CommonOpenFileDialog dlg = new CommonOpenFileDialog();
         dlg.IsFolderPicker = true;
         dlg.ShowDialog();
      }
    }
}
