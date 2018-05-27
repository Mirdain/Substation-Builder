using System.Windows;
using MahApps.Metro.Controls;

namespace Substation_Builder.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindowView : MetroWindow
    {

        public MainWindowView()
        {
            InitializeComponent();

        }

        public void Report_Bug(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Flyout Class");
        }

        private void Launch_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Launch");
        }

        private void DatabaseView(object sender, RoutedEventArgs e)
        {
            Window DatabaseView = new DatabaseView
            {
                Top = this.Top + 20,
                Left = this.Left + 20
            };

            DatabaseView.ShowDialog();
        }

        private void Fault_Analysis(object sender, RoutedEventArgs e)
        {


        }
    }
}
