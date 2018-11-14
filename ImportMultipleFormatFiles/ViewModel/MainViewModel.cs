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
using ImportMultipleFormatFiles.CommonValues;
using System.IO;
using System.Windows.Input;
using System.Collections.Specialized;

namespace ImportMultipleFormatFiles.ViewModel
{
   public class MainViewModel : INotifyPropertyChanged 
   {
      private List<string> formats = Values.Formats;

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

         Format = "";
         ChosenFiles = new ObservableCollection<string>();
 

      }
      public event PropertyChangedEventHandler PropertyChanged;
     

      private string format = "";
      public string Format  // specifies the file format chosen
      {
         get { return format; }
         set
         {
            if (format != value && value != null)
            {
               format = value;
               OnPropertyChanged("Format");           
               MainHelper.SetFilters(format);
            
            }
         }
      }

  



      private List<string> chosenFiles;

      //public List<string> ChosenFiles
      //{
      //   get { return chosenFiles; }
      //   set
      //   {
      //      if (chosenFiles != value)
      //      {
      //         chosenFiles = value;
      //         OnPropertyChanged("ChosenFiles");
      //      }
      //   }
      //}

      public ObservableCollection<string> ChosenFiles { get; set; }

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
         if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
         {
            MainHelper.DirectoryToOpen = dlg.FileName; //updating the last chosen directory next time it will open in this dir

         GetFiles(MainHelper.DirectoryToOpen, MainHelper.Filters);
         }

      }

      private void GetFiles(string directory, List<string> filters)
      {
         foreach (var item in filters)
         {
            string[] temp = Directory.GetFiles(directory, "*" + item);
            foreach (string fileName in temp)
            {
               ChosenFiles.Add(fileName);
            }
             //  ChosenFiles.AddRange(Directory.GetFiles(directory, "*" + item));
            
         
         }
      }
   }


}
