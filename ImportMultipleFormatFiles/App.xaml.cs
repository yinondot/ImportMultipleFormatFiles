using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UtiltyCasewareIdea;

namespace ImportMultipleFormatFiles
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application
   {
      public App()
      {
         try
         {
            //Task.Factory.StartNew(()=> UtilityCasewareIdea.ShowWindow());
           // UtilityCasewareIdea.ShowWindow();
         }
         catch (Exception ex)
         {

            MessageBox.Show(ex.Message + "App Start");
         }
         
      }
   }
}
