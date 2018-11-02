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
using ImportMultipleFormatFiles.ViewModel;


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

   
}
