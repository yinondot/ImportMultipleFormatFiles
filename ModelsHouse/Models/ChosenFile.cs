using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ModelsHouse.Models
{
   public class ChosenFile : INotifyPropertyChanged
   {
      public ChosenFile(string fullPath, bool isChecked)
      {
         FullPath = fullPath;
         Name = Path.GetFileName(fullPath);
         IsChecked = isChecked;
      }


      private string _fullPath;
      public string FullPath
      {
         get { return _fullPath; }
         set
         {
            _fullPath = value;
            OnPropertyChanged("FullPath");
         }
      }
      private string _name;
      public string Name
      {
         get { return _name; }
         set
         {
            _name = value;
            OnPropertyChanged("Name");
         }
      }
      //public string FullPath { get; set; }
      //public string Name { get; set; }
      // public bool IsChecked { get; set; }

      public bool isChecked;

      public bool IsChecked
      {
         get { return isChecked; }
         set
         {
            if (isChecked != value)
            {
               isChecked = value;
               OnPropertyChanged("IsChecked");
            }

         }
      }

      private void OnPropertyChanged([CallerMemberName] string propName = "")
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
      }

      //void OnPropertyChanged(string property)
      //{
      //   if (PropertyChanged!=null)
      //   {
      //      PropertyChanged(this, new PropertyChangedEventArgs(property));
      //   }
      //}

      public event PropertyChangedEventHandler PropertyChanged;
   }
}
