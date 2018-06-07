using System.Windows.Controls;

namespace Substation_Builder.View
{
    /// <summary>
    /// Interaction logic for Transformer.xaml
    /// </summary>
    public partial class TransformerView : Page
    {

        public Transformer2View impedance = new Transformer2View();
        public CTView AddCT = new CTView();

        public TransformerView()
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
