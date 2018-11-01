using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using ImportMultipleFormatFiles.Models;

namespace ImportMultipleFormatFiles
{
    /// <summary>
    /// Interaction logic for temp1.xaml
    /// </summary>
    public partial class temp1 : Window
    {
      Employee em;
        public temp1()
        {
            InitializeComponent();
         em = new Employee("Yinnon", "Dotan", 41);
        }
    }
}
