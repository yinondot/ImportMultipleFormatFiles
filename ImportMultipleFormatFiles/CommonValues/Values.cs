using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UtiltyCasewareIdea;

namespace ImportMultipleFormatFiles.CommonValues
{
  public static class Values
   {
      public static List<string> Formats = new List<string>()
      {
         "Excel","Access","XML","Print Report & Adobe Pdf","Text","dBase","AS400"
      };

      public static IdeaType Idea_Type { get; set; }
   }
}
