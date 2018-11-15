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
using ImportMultipleFormatFiles.Models;
using System.IO;
using System.Windows.Input;
using System.Collections.Specialized;
using System.Windows.Forms;

namespace ImportMultipleFormatFiles.ViewModel
{
   public class MainViewModel : INotifyPropertyChanged
   {
      private List<string> formats = Values.Formats;


      #region Commands
      public ChooseFolderCommand ChooseFolderCommand { get; set; }
      public ChooseFileCommand ChooseFileCommand { get; set; }
      public CheckBoxClickedCommand CheckBoxClickedCommand { get; set; }
      #endregion

      public ReadOnlyCollection<string> ImportFormats { get; set; }
      // public ObservableCollection<string> ChosenFiles { get; set; }

      public ObservableCollection<ChosenFile> ChosenFiles { get; set; }

      public MainViewModel()
      {
         Initialize();
      }

      private void Initialize()
      {
         ImportFormats = new ReadOnlyCollection<string>(formats);

         ChooseFolderCommand = new ChooseFolderCommand(this);
         ChooseFileCommand = new ChooseFileCommand(this);
         CheckBoxClickedCommand = new CheckBoxClickedCommand(this);

         Format = "";
         //  ChosenFiles = new ObservableCollection<string>();
         ChosenFiles = new ObservableCollection<ChosenFile>();

         MainHelper.LoadThisStaticClass = false;
      }

      public event PropertyChangedEventHandler PropertyChanged;
      public Action<string> action = new Action<string>(MainHelper.SetFileTypes);

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
               MainHelper.SetFileTypes(Format);

               ChosenFiles.Clear();

            }
         }
      }


      private void OnPropertyChanged(string propName)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
      }

      #region commands methods
      public void ChooseFileMethod()
      {
         OpenFileDialog dlg = new OpenFileDialog();

         dlg.InitialDirectory = MainHelper.SavedDirectory;
         dlg.Filter = MainHelper.GetFilterString(MainHelper.FileTypes, Format);
         if (dlg.ShowDialog() == DialogResult.OK)
         {
            MainHelper.SavedDirectory = Path.GetDirectoryName(dlg.FileName);

            //if (!ChosenFiles.Contains(dlg.FileName))
            //{
            //   ChosenFiles.Add(dlg.FileName);
            //}

            if (!ChosenFiles.Any(file=>file.FullPath==dlg.FileName))
            {
               ChosenFiles.Add(new ChosenFile(dlg.FileName, false));
            }

         }

      }

      public void ChooseFolderMethod()
      {

         CommonOpenFileDialog dlg = new CommonOpenFileDialog();
         dlg.IsFolderPicker = true;
         dlg.InitialDirectory = MainHelper.SavedDirectory;
         if (dlg.ShowDialog() == CommonFileDialogResult.Ok)
         {
            MainHelper.SavedDirectory = dlg.FileName; //updating the last chosen directory next time it will open in this dir

            foreach (var file in MainHelper.GetMatchingFiles(MainHelper.SavedDirectory, MainHelper.FileTypes))
            {
               if (!ChosenFiles.Any(_file => _file.FullPath == dlg.FileName))
               {
                  ChosenFiles.Add(new ChosenFile(file, false));
               }

            }
         }

      }

      public void CheckBoxClickedMethod(object parameter)
      {
         var cf = parameter as ChosenFile;
         if (cf!=null)
         {
            cf.IsChecked = !cf.IsChecked;
         }
      }
      #endregion

   }


}
