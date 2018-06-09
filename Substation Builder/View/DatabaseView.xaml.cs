using MahApps.Metro.Controls;
using System.Windows;
using Substation_Builder.Model;
using System.Windows.Controls;
using System;
using System.Windows.Media;

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
        private CTView ctview = new CTView();
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
            else if (teststring == "Substation_Builder.Model.CT")
            {
                ctview.DataContext = treepart.SelectedItem;
                pagenavigation.Navigate(ctview);
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

        //used to jump to correct treenode when contextmenu opened
        private void OnPreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.Source as DependencyObject);
            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
        }

        //searches the treeview
        static TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);
            return source as TreeViewItem;
        }
    }
}
