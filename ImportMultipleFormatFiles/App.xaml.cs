using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using UtiltyCasewareIdea;
using ImportMultipleFormatFiles.CommonValues;

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
      private void Application_Startup(object sender, StartupEventArgs e)
      {
#if !DEBUG
         if (Environment.GetCommandLineArgs().Length < 2)
         {
            //MessageBox.Show("מאקרו זה ניתן להרצה דרך מערכת DMR בלבד." + "\n" +
            //    "לצורך רכישת מערכת DMR, אנא צור קשר: http://www.iacs.co.il", "IACS - מאקרו");
            MsgBox.Alert("מאקרו זה ניתן להרצה דרך מערכת DMR בלבד." + "\n" +
                   "לצורך רכישת מערכת DMR, אנא צור קשר: http://www.iacs.co.il", "IACS - מאקרו");
            Application.Current.Shutdown();
         }
         else
         {
            if (Environment.GetCommandLineArgs()[1] != "3AA34AEF-8144-4606-90B3-89252AB84D37")
            {
               //MessageBox.Show("מאקרו זה ניתן להרצה דרך מערכת DMR בלבד." + "\n" +
               // "לצורך רכישת מערכת DMR, אנא צור קשר: http://www.iacs.co.il", "IACS - מאקרו");
               MsgBox.Alert("מאקרו זה ניתן להרצה דרך מערכת DMR בלבד." + "\n" +
                     "לצורך רכישת מערכת DMR, אנא צור קשר: http://www.iacs.co.il", "IACS - מאקרו");
               Application.Current.Shutdown();
            }
         }
#endif
      }
   }
}
