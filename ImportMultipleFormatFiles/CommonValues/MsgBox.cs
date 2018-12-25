using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImportMultipleFormatFiles.CommonValues
{
   public static  class MsgBox
   {
      static string title = "IACS מאקרו";
      public static void Alert(string message, string title)
      {
         MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading | MessageBoxOptions.DefaultDesktopOnly);

      }
      public static void Alert(string message)
      {
         MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading | MessageBoxOptions.DefaultDesktopOnly);

      }
      public static void Simple(string message, string title)
      {
         MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading | MessageBoxOptions.DefaultDesktopOnly);
      }

      public static void Simple(string message)
      {
         MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading | MessageBoxOptions.DefaultDesktopOnly);

      }

      public static void Info(string message, string title)
      {
         MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1,  MessageBoxOptions.DefaultDesktopOnly);

      }
      public static void InfoHeb(string message, string title)
      {
         MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign | MessageBoxOptions.RtlReading | MessageBoxOptions.DefaultDesktopOnly);

      }
   }
}
