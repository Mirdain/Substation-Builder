using MahApps.Metro.Controls;
using System.Windows;
using Substation_Builder.Model;
using Substation_Builder.ViewModel;
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
                Substation substation = new Substation();
                pagenavigation.Navigate(substation);
            }
            else if (teststring == "Substation_Builder.Model.Thevenin")
            {
                TheveninView theveninview = new TheveninView
                {
                    DataContext = treepart.SelectedItem
                };

                pagenavigation.Navigate(theveninview);
            }
            else if (teststring == "Substation_Builder.Model.Relay")
            {
                RelayView relayview = new RelayView
                {
                    DataContext = treepart.SelectedItem
                };
                pagenavigation.Navigate(relayview);
            }
            else if (teststring == "Substation_Builder.Model.Transformer")
            {
                TransformerView transformerview = new TransformerView
                {
                    DataContext = treepart.SelectedItem
                };
                pagenavigation.Navigate(transformerview);
            }
            else
            {
                pagenavigation.Source = new Uri("DefaultView.xaml", UriKind.Relative);
            }
        }

    }
}
