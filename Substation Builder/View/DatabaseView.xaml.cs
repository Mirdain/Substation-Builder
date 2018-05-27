using MahApps.Metro.Controls;
using System.Windows;
using Substation_Builder.Model;
using System.Windows.Controls;
using System;

namespace Substation_Builder.View
{
    /// <summary>
    /// Interaction logic for Oneline.xaml
    /// </summary>
    /// 
    public partial class DatabaseView : MetroWindow
    {
        private TheveninView theveninview = new TheveninView();
        private SubstationView substationview = new SubstationView();
        private RelayView relayview = new RelayView();
        private TransformerView transformerview = new TransformerView();

        public DatabaseView()
        {
            InitializeComponent();
        }

        //used to navigate to forms located in the xaml folder and bind to classes
        private void LoadPage(object sender, RoutedEventArgs e)
        {
            TreeView treepart = (TreeView)sender;
            string teststring = treepart.SelectedItem.ToString();

            if (Subdata.IsSelected)
            {
                substationview.DataContext = this.DataContext;
                pagenavigation.Navigate(substationview);
            }
            else if (teststring == "Substation_Builder.Model.Thevenin")
            {
                theveninview.DataContext = treepart.SelectedItem;
                pagenavigation.Navigate(theveninview);
            }
            else if (teststring == "Substation_Builder.Model.Relay")
            {
                relayview.DataContext = treepart.SelectedItem;
                pagenavigation.Navigate(relayview);
            }
            else if (teststring == "Substation_Builder.Model.Transformer")
            {
                transformerview.DataContext = treepart.SelectedItem;
                pagenavigation.Navigate(transformerview);
            }
            else
            {
                pagenavigation.Source = new Uri("DefaultView.xaml", UriKind.Relative);
            }
        }

    }
}
