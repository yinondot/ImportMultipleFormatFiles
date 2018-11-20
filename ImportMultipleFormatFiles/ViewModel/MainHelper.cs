using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtiltyCasewareIdea;

namespace ImportMultipleFormatFiles.ViewModel
{
   public static class MainHelper
   {
      public static bool LoadThisStaticClass { get; set; }
      static MainHelper()
      {
         SavedDirectory = UtilityCasewareIdea.GetProjectDirectory() + @"Source Files.ILB";
         FileTypes = new List<string>();
         DefinitionFileType = "";
      }
      public static string SavedDirectory { get; set; }
      public static List<string> FileTypes { get; set; }
      public static string DefinitionFileType { get; set; }

      public static void SetFileTypesAndBtnDefinition_CheckBoxVisibility(MainViewModel vm)
      {
         FileTypes.Clear();
         switch (vm.Format)
         {
            case "Excel":
               {
                  FileTypes.Add("xls");
                  FileTypes.Add("xlsx");
                  vm.Visible = System.Windows.Visibility.Hidden;
                  vm.CheckBoxVisiblity = System.Windows.Visibility.Visible;
                  break;
               }
            case "Access":
               {
                  FileTypes.Add("mdb");
                  FileTypes.Add("accdb");
                  vm.Visible = System.Windows.Visibility.Hidden;
                  vm.CheckBoxVisiblity = System.Windows.Visibility.Hidden;
                  break;
               }
            case "XML":
               {
                  FileTypes.Add("xml");
                  vm.Visible = System.Windows.Visibility.Hidden;
                  vm.CheckBoxVisiblity = System.Windows.Visibility.Hidden;
                  break;
               }
            case "Print Report & Adobe Pdf":
               {
                  FileTypes.Add("pdf");
                  FileTypes.Add("prn");
                  FileTypes.Add("lis");
                  FileTypes.Add("txt");
                  vm.Visible = System.Windows.Visibility.Visible;
                  DefinitionFileType = "jpm";
                  vm.CheckBoxVisiblity = System.Windows.Visibility.Hidden;
                  break;
               }
            case "Text":
               {
                  FileTypes.Add("txt");
                  FileTypes.Add("asc");
                  FileTypes.Add("csv");
                  vm.Visible = System.Windows.Visibility.Visible;
                  vm.CheckBoxVisiblity = System.Windows.Visibility.Hidden;
                  if (CommonValues.Values.Idea_Type==IdeaType.UNICODE)
                  {
                     DefinitionFileType = "rdm";
                  }
                  else
                  {
                     DefinitionFileType = "rdf";
                  }
                  break;
               }
            case "dBase":
               {
                  FileTypes.Add("dbf");
                  vm.Visible = System.Windows.Visibility.Hidden;
                  vm.CheckBoxVisiblity = System.Windows.Visibility.Hidden;
                  break;
               }
            case "AS400":
               {
                  FileTypes.Add("dat");
                  vm.Visible = System.Windows.Visibility.Visible;
                  vm.CheckBoxVisiblity = System.Windows.Visibility.Hidden;
                  DefinitionFileType = "fdf";
                  break;
               }



            default:
               break;
         }
      }

   

      public static List<string> GetMatchingFiles(string directory, List<string> filters)
      {
         List<string> fileList = new List<string>();
         foreach (var item in filters)
         {
            string[] temp = (Directory.GetFiles(directory, "*." + item));
            
       
            foreach (string fileName in temp.Where(file => file.ToLower().EndsWith(item)))
            {
               if (!fileName.Contains("~"))
               {
                  fileList.Add(fileName);
               }
              
            }
         }
         return fileList;
      }
      internal static string GetDefinitionFilterString(string definitionStr)
      {
         string temp = "";
         temp = " |*." + definitionStr;
         return temp;
      }


      internal static string GetFilterString(List<string> fileTypes,string format)
      {
         string temp = "";
         foreach (string item in fileTypes) // assumes filetypes are of the pattern ".xls"
         {
            temp = temp + "*." + item+";";
         }
         temp = temp.Remove(temp.Length - 1, 1);
         temp = format + "|" + temp ;
         return temp;
      }
   }
}
