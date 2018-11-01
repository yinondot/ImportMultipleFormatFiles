using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace ImportMultipleFormatFiles
{
   /// <summary>
   /// Interaction logic for MainWindow.xaml
   /// </summary>
   public partial class MainWindow : Window
   {
      //Grid grid = new Grid();
      //Button button = new Button();

      public MainWindow()
      {
         InitializeComponent();
         //button.SetValue(Grid.RowProperty, 4);
      }

      private void Button_Click(object sender, RoutedEventArgs e)
      {
         cbFileFormats.IsDropDownOpen = true;

      }

   

      //public System.Windows.DependencyProperty ColumnProperty

      //{
      //   get { return (ImageSource)GetValue(ImageSourceProperty); }
      //   set { SetValue(ImageSourceProperty, value); }
      //}
   }

   public class ComboNameConverter : IValueConverter
   {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         if (value!=null)
         {
            var temp = value.ToString();
            if (temp != null)
            {
               temp = (temp.Split(':'))[1];
            }
            return temp;
         }
         return null;

      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         throw new NotImplementedException();
      }
   }

   public class ComboTextWidthConverter : IValueConverter
   {
      public object Convert(object values, Type targetType, object parameter, CultureInfo culture)
      {
         double width = Double.Parse(values.ToString());
         return width-SystemParameters.VerticalScrollBarWidth;
      }

      public object ConvertBack(object value, Type targetTypes, object parameter, CultureInfo culture)
      {
         return null;
      }
   }
}
