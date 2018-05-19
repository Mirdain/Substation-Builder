using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using MahApps.Metro.Controls;
using System.Web;
using Substation_Builder.ViewModel;

namespace Substation_Builder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {

        public MainWindow()
        {
            InitializeComponent();

        }

        public void Report_Bug(object sender, RoutedEventArgs e)
        {
            AboutLink handler = new AboutLink();
        }

        private void Launch_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Launch");
        }

        private void One_Line(object sender, RoutedEventArgs e)
        {
            Window OnelineWindow = new Oneline
            {
                Top = this.Top + 20,
                Left = this.Left + 20
            };
            OnelineWindow.ShowDialog();
        }

        private void Fault_Analysis(object sender, RoutedEventArgs e)
        {


        }
    }
}
