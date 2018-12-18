using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
using System.Windows;
using UtiltyCasewareIdea;
using System.Threading;
using ModelsHouse.Models;
using Services;


namespace ImportMultipleFormatFiles.ViewModel
{
   public class MainViewModel : INotifyPropertyChanged
   {
      private List<string> formats = CommonValues.Values.Formats;




      #region Commands
      public ChooseFolderCommand ChooseFolderCommand { get; set; }
      public ChooseFileCommand ChooseFileCommand { get; set; }
      public CheckAllCommand CheckAllCommand { get; set; }
      public RemoveCheckedCommand RemoveCheckedCommand { get; set; }
      public ChooseDefinitionFileCommand ChooseDefinitionFileCommand { get; set; }
      public RunCommand RunCommand { get; set; }
      #endregion

      public ReadOnlyCollection<string> ImportFormats { get; set; }
      // public ObservableCollection<string> ChosenFiles { get; set; }

      public ObservableCollection<ChosenFile> ChosenFiles { get; set; }

      public MainViewModel()
      {
         try
         {
            Initialize();
         }
         catch (Exception ex)
         {

            System.Windows.MessageBox.Show(ex.Message + " On Initialize");
         }
        
      }

      private async void Initialize()
      {
         Task<IdeaType> task = new Task<IdeaType>(UtilityCasewareIdea.GetIdeaType);
         task.Start();
         CommonValues.Values.Idea_Type = UtilityCasewareIdea.GetIdeaType();
         ImportFormats = new ReadOnlyCollection<string>(formats);

         ChooseFolderCommand = new ChooseFolderCommand(this);
         ChooseFileCommand = new ChooseFileCommand(this);
         CheckAllCommand = new CheckAllCommand(this);
         RemoveCheckedCommand = new RemoveCheckedCommand(this);
         ChooseDefinitionFileCommand = new ChooseDefinitionFileCommand(this);
         RunCommand = new RunCommand(this);

         Format = "";
         Visible = Visibility.Hidden;
         checkBoxVisiblity = Visibility.Hidden;
         CanRun = true;
         //  ChosenFiles = new ObservableCollection<string>();
         ChosenFiles = new ObservableCollection<ChosenFile>();

         MainHelper.LoadThisStaticClass = false;// dong this inorder to load the static class so not to block the UI.

         CommonValues.Values.Idea_Type = await task;
      }

      public event PropertyChangedEventHandler PropertyChanged;
      //   public Action<string> action = new Action<string>(MainHelper.SetFileTypes);

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
               MainHelper.SetFileTypesAndBtnDefinition_CheckBoxVisibility(this);
               DefinitionFilePath = "";
               ChosenFiles.Clear();

            }
         }
      }

      private Visibility visible;
      public Visibility Visible
      {
         get { return visible; }
         set
         {
            if (visible != value)
            {
               visible = value;
               OnPropertyChanged("Visible");
            }

         }
      }

      private Visibility checkBoxVisiblity;
      public Visibility CheckBoxVisiblity
      {
         get { return checkBoxVisiblity; }
         set
         {
            if (checkBoxVisiblity != value)
            {
               checkBoxVisiblity = value;
               OnPropertyChanged("CheckBoxVisiblity");
            }

         }
      }

      private bool? ischeckedFirstRow;
      public bool? IscheckedFirstRow
      {
         get { return ischeckedFirstRow; }
         set
         {
            if (ischeckedFirstRow != value)
            {
               ischeckedFirstRow = value;
               OnPropertyChanged("IscheckedFirstRow");
            }

         }
      }

      private bool? ischeckedEmptyZeros;
      public bool? IscheckedEmptyZeros
      {
         get { return ischeckedEmptyZeros; }
         set
         {
            if (ischeckedEmptyZeros != value)
            {
               ischeckedEmptyZeros = value;
               OnPropertyChanged("IscheckedEmptyZeros");
            }

         }
      }

      private string definitionFilePath;
      public string DefinitionFilePath
      {
         get { return definitionFilePath; }
         set {
            if (definitionFilePath != value)
            {
               definitionFilePath = value;
               OnPropertyChanged("DefinitionFilePath");
            }

         }
      }

      private bool canRun;
      public bool CanRun
      {
         get { return canRun; }
         set
         {
            if (canRun != value)
            {
               canRun = value;
               //OnPropertyChanged("CanRun");
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
         for (int i = ChosenFiles.Count - 1; i >= 0; i--)
         {
            if (ChosenFiles[i].isChecked == true)
            {
               ChosenFiles.RemoveAt(i);
            }
         }
      }

      internal void ChooseDefinitionFileMethod()
      {
         OpenFileDialog dlg = new OpenFileDialog();

         dlg.InitialDirectory = MainHelper.SavedDirectory;
         dlg.Filter = MainHelper.GetDefinitionFilterString(MainHelper.DefinitionFileType);
         if (dlg.ShowDialog() == DialogResult.OK)
         {
            MainHelper.SavedDirectory = Path.GetDirectoryName(dlg.FileName);
            DefinitionFilePath = dlg.FileName;

         }
      }

      internal async void RunMethod()
      {
         ImportFormats importFormats = new ImportFormats();
         Task<string> task = new Task<string>(() => importFormats.Start(ChosenFiles, Format, DefinitionFilePath,IscheckedFirstRow,IscheckedEmptyZeros));
         task.Start();

         if (await task=="")
         {
            System.Windows.MessageBox.Show("הסתיים בהצלחה");
         }
         CanRun = true; ;
      }
      #endregion

   }


}
