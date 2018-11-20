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
using System.Data.OleDb;
using System.Data;

namespace Services
{
   public class ImportFormats
   {
      public string Start(ObservableCollection<ChosenFile> chosenFiles, string format, string definitionFilePath, bool? isFirstRowHeader)
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
               case "Excel":
                  {
                     dynamic task = null;
                     List<string> _sheets = new List<string>();
                     foreach (var item in chosenFiles.Where(file => file.IsChecked == true))
                     {
                        try
                        {
                           bool isHeader = false;
                           if (isFirstRowHeader == null || isFirstRowHeader == false)
                           {
                              isHeader = false;
                           }
                           else
                           {
                              isHeader = true;
                           }
                           runningFile = item.FullPath;
                           _sheets = getExcelSheetNames(item.FullPath);
                           foreach (string sheetName in _sheets)
                           {
                              string newSheetName = sheetName.Replace("$", "");
                              newSheetName = newSheetName.Replace("'", "");
                              task = client.GetImportTask("ImportExcel");

                              task.FirstRowIsFieldName = isHeader;
                              task.SheetToImport = newSheetName;
                              task.FileToImport = item.FullPath;
                              newFileName = Path.GetFileNameWithoutExtension(item.FullPath);

                              task.OutputFilePrefix = newFileName;
                              task.EmptyNumericFieldAsZero = false;
                              task.UniqueFilePrefix();
                              task.PerformTask();

                              Marshal.ReleaseComObject(task);
                              task = null;

                              client.ShowWindow();
                              client.RefreshFileExplorer();
                           }

                        }
                        catch (Exception ex)
                        {

                           ex.Data["File"] = runningFile;

                           //  return ex.Message;
                        }
                        finally
                        {
                           UtilityCasewareIdea.DisposeCom(task);
                        }
                     }
                     break;
                  }
               #endregion
               #region Access
               case "Access":
                  {
                     dynamic task = null;
                     try
                     {
                        foreach (var item in chosenFiles.Where(file => file.IsChecked == true))
                        {
                           runningFile = item.FullPath;
                           try
                           {
                              task = client.GetImportTask("Access");
                              //filename = Values.LbxFilesOut.GetItemText(item);
                              //filename = ("\\" + filename);
                              task.InputFileName = (item.FullPath);
                              newFileName = Path.GetFileNameWithoutExtension(item.FullPath);
                              task.OutputFileNamePrefix = task.UniqueFileNamePrefix(newFileName);
                              task.DetermineMaximumCharacterFieldLengths = -1;
                              task.AddAllTables();
                              task.PerformTask();

                              Marshal.ReleaseComObject(task);
                              task = null;
                              client.ShowWindow();
                              client.RefreshFileExplorer();
                           }
                           catch (Exception ex)
                           {
                              Marshal.ReleaseComObject(task);
                              task = null;
                              //probFile = Values.Dir + filename;
                              ex.Data["File"] = runningFile;

                           }
                        }
                     }
                     finally
                     {
                        UtilityCasewareIdea.DisposeCom(task);
                     }





                  }
                  break;
               #endregion
               #region dBase
               case "dBase":
                  {
                     dynamic task = null;
                     foreach (var item in chosenFiles.Where(file => file.IsChecked == true))
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

               #endregion
               #region xml
               case "XML":
                  {

                     dynamic task = null;
                     foreach (var item in chosenFiles.Where(file => file.IsChecked == true))
                     {
                        try
                        {

                           runningFile = item.FullPath;

                           task = client.GetImportTask("ImportXML");
                           //filename = LbxFilesOut.GetItemText(item);
                           //filename = ("\\" + filename);
                           task.InputFileName = (item.FullPath);
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


               #endregion
               #region Text
               case "Text":
                  {

                     dynamic task = null;
                     foreach (var item in chosenFiles.Where(file => file.IsChecked == true))
                     {
                        try
                        {

                           runningFile = item.FullPath;


                           //filename = LbxFilesOut.GetItemText(item);
                           //filename = ("\\" + filename);
                           //   task.InputFileName = (item.FullPath);
                           newFileName = Path.GetFileNameWithoutExtension(item.FullPath);
                           dbname = client.UniqueFileName(newFileName);
                           client.ImportDelimFile(item.FullPath, dbname, 0, "", definitionFilePath, 0);
                           task.OutputFileName = client.UniqueFileName(newFileName);



                        }
                        catch (Exception ex)
                        {

                           //probFile = Values.Dir + filename;
                           ex.Data["File"] = runningFile;


                        }
                        finally
                        {

                        }
                     }
                     client.ShowWindow();
                     client.RefreshFileExplorer();

                     // MsgBox.Info("הסתיים בהצלחה", " ");
                  }
                  break;


               #endregion
               #region AS400
               case "AS400":
                  {

                     dynamic task = null;
                     foreach (var item in chosenFiles.Where(file => file.IsChecked == true))
                     {
                        try
                        {

                           runningFile = item.FullPath;

                           task = client.GetImportTask("AS400");
                           task.InputFDFFileName = definitionFilePath;
                           //filename = LbxFilesOut.GetItemText(item);
                           //filename = ("\\" + filename);
                           // task.InputFileName = (item.FullPath);
                           newFileName = Path.GetFileNameWithoutExtension(item.FullPath);
                           dbname = client.UniqueFileName(newFileName);
                           task.OutputFileName = dbname;
                           task.InputDATFilename = item.FullPath;
                           task.PerformTask();

                           Marshal.ReleaseComObject(task);
                           task = null;
                        }
                        catch (Exception ex)
                        {

                           //probFile = Values.Dir + filename;
                           ex.Data["File"] = runningFile;


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


               #endregion
               #region xml
               case "Print Report & Adobe Pdf":
                  {

                     dynamic task = null;
                     foreach (var item in chosenFiles.Where(file => file.IsChecked == true))
                     {
                        try
                        {

                           runningFile = item.FullPath;


                           //filename = LbxFilesOut.GetItemText(item);
                           //filename = ("\\" + filename);

                           newFileName = Path.GetFileNameWithoutExtension(item.FullPath);
                           dbname = client.UniqueFileName(newFileName);
                           client.ImportPrintReport(definitionFilePath, item.FullPath, dbname, 0);

                        }
                        catch (Exception ex)
                        {

                           //probFile = Values.Dir + filename;
                           ex.Data["File"] = runningFile;


                        }
                        finally
                        {

                        }
                     }
                     client.ShowWindow();
                     client.RefreshFileExplorer();

                     // MsgBox.Info("הסתיים בהצלחה", " ");
                  }
                  break;


                  #endregion

            }
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

      private static List<string> getExcelSheetNames(string fileName)
      {
         string connString = "";

         List<string> _sheets = new List<string>();

         if (System.IO.Path.GetExtension(fileName).ToLower() == ".xls")
            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
         //  connString = "Provider=Microsoft.Jet.Oledb.4.0; Data Source=" + fileName + ";Extended Properties=\"Excel 8.0;HDR=No;IMEX=1\"";
         else if (System.IO.Path.GetExtension(fileName).ToLower() == ".xlsx")
            connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;HDR=No;IMEX=1\"";
         else
            return Enumerable.Empty<string>().ToList();

         using (OleDbConnection conn = new OleDbConnection(connString))
         {
            conn.Open();
            System.Data.DataTable sheetsTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            foreach (DataRow item in sheetsTable.Rows)
            {
               if (item["TABLE_NAME"].ToString().Contains("$"))//checks whether row contains '_xlnm#_FilterDatabase' or sheet name(i.e. sheet name always ends with $ sign)
               {

                  _sheets.Add(item["TABLE_NAME"].ToString());
               }
            }
         }

         return _sheets;

      }
   }
}
