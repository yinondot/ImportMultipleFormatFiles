using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtiltyCasewareIdea;

namespace ImportMultipleFormatFiles.ViewModel
{
  public static class MainHelper
   {
      static  MainHelper()
      {
         DirectoryToOpen = UtilityCasewareIdea.GetProjectDirectory() + @"Source Files.ILB";
      }
      public static string DirectoryToOpen { get; set; }

   }
}
