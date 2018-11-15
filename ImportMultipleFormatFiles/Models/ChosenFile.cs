using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ImportMultipleFormatFiles.Models
{
  public  class ChosenFile
    {
      public ChosenFile(string fullPath,bool isChecked)
      {
         FullPath = fullPath;
         Name = Path.GetFileName(fullPath);
         IsChecked = isChecked;
      }

      public string FullPath { get; set; }
      public string Name { get; set; }
      public bool IsChecked { get; set; }
   }
}
