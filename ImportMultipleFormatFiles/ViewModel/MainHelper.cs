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
      static MainHelper()
      {
         DirectoryToOpen = UtilityCasewareIdea.GetProjectDirectory() + @"Source Files.ILB";
         Filters = new List<string>();
      }
      public static string DirectoryToOpen { get; set; }
      public static List<string> Filters { get; set; }
    
      public static void SetFilters(string format)
      {
         Filters.Clear();
         switch (format)
         {
            case "Excel":
               {
                  Filters.Add(".xls");
                  Filters.Add(".xlsx");
                  break;
               }
            case "Access":
               {
                  Filters.Add(".mdb");
                  Filters.Add(".accdb");
                  break;
               }
            case "XML":
               {
                  Filters.Add(".xml");
                  break;
               }
            case "Print Report & Adobe Pdf":
               {
                  Filters.Add(".pdf");
                  Filters.Add(".prn");
                  Filters.Add(".lis");
                  Filters.Add(".txt");

                  break;
               }
            case "Text":
               {
                  Filters.Add(".txt");
                  Filters.Add(".asc");
                  Filters.Add(".csv");

                  break;
               }
            case "dBase":
               {
                  Filters.Add(".dbf");
                  break;
               }
            case "AS400":
               {
                  Filters.Add(".dat");
                  break;
               }



            default:
               break;
         }
      }

   }
}
