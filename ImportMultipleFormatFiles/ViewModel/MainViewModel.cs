using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportMultipleFormatFiles.Models;
using ImportMultipleFormatFiles.Converters;
using ImportMultipleFormatFiles.Commands;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace ImportMultipleFormatFiles.ViewModel
{
   public class MainViewModel : INotifyPropertyChanged
   {
      private List<string> formats = new List<string>()
      {
         "Excel","Access","XML","Print Report & Adobe Pdf","dBase","AS400"
      };

      public ReadOnlyCollection<string> ImportFormats { get; set; }

      public ComboNameConverter comboNameConverter;
      public ComboTextWidthConverter comboTextWidthConverter;

      public ChooseFolderCommand ChooseFolderCommand { get; set; }
      public ChooseFileCommand ChooseFileCommand { get; set; }


      public MainViewModel()
      {
         ImportFormats = new ReadOnlyCollection<string>(formats);

         comboNameConverter = new ComboNameConverter();
         comboTextWidthConverter = new ComboTextWidthConverter();

         ChooseFolderCommand = new ChooseFolderCommand(this);
         ChooseFileCommand = new ChooseFileCommand(this);

         
      }
      public event PropertyChangedEventHandler PropertyChanged;

      private string format = "";
      public string Format  // specifies the file format chosen
      {
         get { return format; }
         set
         {
            if (format != value)
            {
               format = value;
               OnPropertyChanged("Format");
              Filter= SetFilter(format);
            }
         }
      }

      private string SetFilter(string format)
      {
         return "";
      }

      private string filter;

      public string Filter
      {
         get { return filter; }
         set { filter = value; }
      }


      private List<string> chosenFiles;

      public List<string> ChosenFiles
      {
         get { return chosenFiles; }
         set {
            if (chosenFiles != value)
            {
               chosenFiles = value;
               OnPropertyChanged("ChosenFiles");
            } 
         }
      }


      private void OnPropertyChanged(string propName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
      }

      public void ChooseFileMethod()
      {
         CommonOpenFileDialog dlg = new CommonOpenFileDialog();
         dlg.IsFolderPicker = false;
       
         dlg.ShowDialog();
      }

      public void ChooseFolderMethod()
      {
         CommonOpenFileDialog dlg = new CommonOpenFileDialog();
         dlg.IsFolderPicker = true;
         dlg.InitialDirectory = MainHelper.DirectoryToOpen;
         if (dlg.ShowDialog()==CommonFileDialogResult.Ok)
         {
            MainHelper.DirectoryToOpen = dlg.FileName;
           
         }
         
      }
   }


}
