﻿using System;
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
using System.Windows.Forms;

namespace ImportMultipleFormatFiles.ViewModel
{
   public class MainViewModel : INotifyPropertyChanged
   {
      private List<string> formats = Values.Formats;


      #region Commands
      public ChooseFolderCommand ChooseFolderCommand { get; set; }
      public ChooseFileCommand ChooseFileCommand { get; set; }
      public CheckAllCommand CheckAllCommand { get; set; }
      public RemoveCheckedCommand RemoveCheckedCommand { get; set; }
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
         CheckAllCommand = new CheckAllCommand(this);
         RemoveCheckedCommand = new RemoveCheckedCommand(this);

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

            if (!ChosenFiles.Any(file => file.FullPath == dlg.FileName))
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
               if (!ChosenFiles.Any(_file => _file.FullPath == file))
               {
                  ChosenFiles.Add(new ChosenFile(file, false));
               }

            }
         }

      }

      public void CheckAllMethod()
      {
         foreach (var item in ChosenFiles)
         {
            item.IsChecked = true;
         }
      }

      internal void RemoveCheckedMethod()
      {
         for (int i = ChosenFiles.Count-1; i >= 0; i--)
         {
            if (ChosenFiles[i].isChecked==true)
            {
               ChosenFiles.RemoveAt(i);
            }
         }
      }
      #endregion

   }


}
