using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImportMultipleFormatFiles.Models;
using ImportMultipleFormatFiles.Converters;

namespace ImportMultipleFormatFiles.ViewModel
{
   public class MainViewModel
    {
      ComboNameConverter comboNameConverter;
      ComboTextWidthConverter comboTextWidthConverter;
      public MainViewModel()
      {
         comboNameConverter = new ComboNameConverter();
         comboTextWidthConverter = new ComboTextWidthConverter();
      }
    }
}
