using System.Windows.Controls;

namespace Substation_Builder.View
{
    /// <summary>
    /// Interaction logic for Transformer.xaml
    /// </summary>
    public partial class TransformerPage : Page
    {

        public Transformer2Page impedance = new Transformer2Page();

        public TransformerPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            impedance.DataContext = this.DataContext;
            NavigationService.Navigate(impedance);
        }
    }
}
