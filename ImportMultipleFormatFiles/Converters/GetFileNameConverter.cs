using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.IO;
using System.Collections.ObjectModel;

namespace ImportMultipleFormatFiles.Converters
{
   public class GetFileNameConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         ObservableCollection<string> convertible = null;
         var result = value as ObservableCollection<string>;
         if (result != null && result.Count!=0)
         {
            convertible = new ObservableCollection<string>();
            foreach (var item in result)
            {
               convertible.Add(Path.GetFileName(item));
            }
               return convertible;
           
           
         }
         return value;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         return null;
      }
   }
}
