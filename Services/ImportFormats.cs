using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsHouse.Models;
using Interop.IdeaLib;
using UtiltyCasewareIdea;
using System.Collections.ObjectModel;
using ModelsHouse.Models;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;

namespace Services
{
   public class ImportFormats
   {
      public string Start(ObservableCollection<ChosenFile> chosenFiles, string format, string definitionFilePath)
      {
         IdeaClient client = null;
         try
         {
            client = new IdeaClient();
            UtilityCasewareIdea.ShowWindow();

            string newFileName;
            string runningFile = "";
            string filename = "";
            string dbname;

            switch (format)
            {
               // "Excel","Access","XML","Print Report & Adobe Pdf","Text","dBase","AS400"
               #region Excel
               case "dBase":
                  {
                     dynamic task = null;
                     foreach (var item in chosenFiles)
                     {
                        try
                        {
                           runningFile = item.FullPath;

                           task = client.GetImportTask("DBaseImport");
                           //filename = LbxFilesOut.GetItemText(item);
                           //filename = ("\\" + filename);
                           task.InputFileName(item.FullPath);
                           newFileName = Path.GetFileNameWithoutExtension(item.FullPath);
                           task.OutputFileName = client.UniqueFileName(newFileName);
                           task.PerformTask();

                           Marshal.ReleaseComObject(task);
                           task = null;
                        }
                        catch (Exception ex)
                        {
                        
                           //probFile = Values.Dir + filename;
                           ex.Data["File"] = runningFile;

                           return ex.Message;
                        }
                        finally
                        {
                           UtilityCasewareIdea.DisposeCom(task);
                        }
                     }
                     client.ShowWindow();
                     client.RefreshFileExplorer();

                     // MsgBox.Info("הסתיים בהצלחה", " ");
                  }
                  break;

            }
            #endregion

         }
         catch (Exception ex)
         {

            return (ex.Message);
         }
         finally
         {
            UtilityCasewareIdea.DisposeCom(client);
         }


         return "";
      }
   }
}
