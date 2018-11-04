using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace ImportMultipleFormatFiles.Converters
{
   public class ComboTextWidthConverter : IValueConverter
   {
      public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
      {
         double width = Double.Parse(values.ToString());
         return width - int.Parse(parameter.ToString());
      }

      public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
      {
         return null;
      }
   }
}
