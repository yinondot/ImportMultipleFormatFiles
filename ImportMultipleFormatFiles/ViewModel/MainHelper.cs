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
      }
      public static string SavedDirectory { get; set; }
      public static List<string> FileTypes { get; set; }
    
      public static void SetFileTypes(string format)
      {
         FileTypes.Clear();
         switch (format)
         {
            case "Excel":
               {
                  FileTypes.Add(".xls");
                  FileTypes.Add(".xlsx");
                  break;
               }
            case "Access":
               {
                  FileTypes.Add(".mdb");
                  FileTypes.Add(".accdb");
                  break;
               }
            case "XML":
               {
                  FileTypes.Add(".xml");
                  break;
               }
            case "Print Report & Adobe Pdf":
               {
                  FileTypes.Add(".pdf");
                  FileTypes.Add(".prn");
                  FileTypes.Add(".lis");
                  FileTypes.Add(".txt");

                  break;
               }
            case "Text":
               {
                  FileTypes.Add(".txt");
                  FileTypes.Add(".asc");
                  FileTypes.Add(".csv");

                  break;
               }
            case "dBase":
               {
                  FileTypes.Add(".dbf");
                  break;
               }
            case "AS400":
               {
                  FileTypes.Add(".dat");
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
            string[] temp = (Directory.GetFiles(directory, "*" + item));
            
       
            foreach (string fileName in temp.Where(file => file.ToLower().EndsWith(item)))
            {
               fileList.Add(fileName);
            }
         }
         return fileList;
      }

      internal static string GetFilterString(List<string> fileTypes,string format)
      {
         string temp = "";
         foreach (string item in fileTypes) // assumes filetypes are of the pattern ".xls"
         {
            temp = temp + "*" + item+";";
         }
         temp = temp.Remove(temp.Length - 1, 1);
         temp = format + "|" + temp ;
         return temp;
      }
   }
}
