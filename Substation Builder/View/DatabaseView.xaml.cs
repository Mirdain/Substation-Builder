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
        private TheveninPage theveninview = new TheveninPage();
        private SubstationPage substationview = new SubstationPage();
        private RelayPage relayview = new RelayPage();
        private BreakerPage breakerView = new BreakerPage();
        private CTPage ctview = new CTPage();
        private TransformerPage transformerview = new TransformerPage();
        private DefaultPage defaultPage = new DefaultPage();

        public DatabaseView()
        {
            InitializeComponent();
            pagenavigation.IsEnabled = false;
            pagenavigation.Navigate(substationview);
        }

        //used to navigate to forms located in the xaml folder and bind to classes
        private void LoadPage(object sender, RoutedEventArgs e)
        {
            TreeView treepart = (TreeView)sender;

            string teststring = treepart.SelectedItem.ToString();

            if (Subdata.IsSelected)
            {
                substationview.DataContext = Subdata.DataContext;
                pagenavigation.IsEnabled = true;
                pagenavigation.Navigate(substationview);
            }
            else if (teststring.Contains("Thevenins"))
            {
                theveninview.DataContext = null;
                pagenavigation.IsEnabled = false;
                pagenavigation.Navigate(theveninview);
            }
            else if (teststring.Contains("Transformers"))
            {
                transformerview.DataContext = null;
                pagenavigation.IsEnabled = false;
                pagenavigation.Navigate(transformerview);
            }
            else if (teststring.Contains("Breakers"))
            {
                breakerView.DataContext = null;
                pagenavigation.IsEnabled = false;
                pagenavigation.Navigate(breakerView);
            }
            else if (teststring.Contains("Relays"))
            {
                relayview.DataContext = null;
                pagenavigation.IsEnabled = false;
                pagenavigation.Navigate(relayview);
            }
            else if (teststring.Contains("Thevenin"))
            {
                theveninview.DataContext = treepart.SelectedItem;
                pagenavigation.IsEnabled = true;
                pagenavigation.Navigate(theveninview);
            }
            else if (teststring.Contains("Transformer"))
            {
                transformerview.DataContext = treepart.SelectedItem;
                pagenavigation.IsEnabled = true;
                pagenavigation.Navigate(transformerview);
            }
            else if (teststring.Contains("Breaker"))
            {
                breakerView.DataContext = treepart.SelectedItem;
                pagenavigation.IsEnabled = true;
                pagenavigation.Navigate(breakerView);
            }
            else if (teststring.Contains("Relay"))
            {
                relayview.DataContext = treepart.SelectedItem;
                pagenavigation.IsEnabled = true;
                pagenavigation.Navigate(relayview);
            }
            else if (teststring.Contains("CT"))
            {
                ctview.DataContext = treepart.SelectedItem;
                pagenavigation.IsEnabled = true;
                pagenavigation.Navigate(ctview);
            }
            else
            {
                pagenavigation.Navigate(defaultPage);
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
