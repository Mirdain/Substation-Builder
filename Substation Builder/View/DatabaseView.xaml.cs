using MahApps.Metro.Controls;
using System.Windows;
using Substation_Builder.Model;
using System.Windows.Controls;
using System;
using System.Windows.Media;
using Substation_Builder.ViewModel;
using Substation_Builder.Pages.DatabaseView;

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

        public DatabaseView()
        {
            InitializeComponent();
        }

        //used to navigate to forms located in the xaml folder and bind to classes
        private void LoadPage(object sender, RoutedEventArgs e)
        {

           

            TreeView TV = (TreeView)sender;
            object TVI = TV.SelectedItem;

            if (Subdata.IsSelected)
            {
                substationview.DataContext = TV.DataContext;
                pagenavigation.Navigate(substationview);
            }
            else if (TVI.GetType() == typeof(Thevenin))
            {
                theveninview.DataContext = TV.SelectedItem;
                pagenavigation.Navigate(theveninview);
            }
            else if (TVI.GetType() == typeof(Transformer))
            {
                transformerview.DataContext = TV.SelectedItem;
                pagenavigation.Navigate(transformerview);
            }
            else if (TVI.GetType() == typeof(Breaker))
            {
                breakerView.DataContext = TV.SelectedItem;
                pagenavigation.Navigate(breakerView);
            }
            else if (TVI.GetType() == typeof(Relay))
            {
                relayview.DataContext = TV.SelectedItem;
                pagenavigation.Navigate(relayview);
            }
            else if (TVI.GetType() == typeof(CT))
            {
                ctview.DataContext = TV.SelectedItem;
                pagenavigation.Navigate(ctview);
            }
            else
            {

            }
        }

        //searches the treeview
        static TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);

            return source as TreeViewItem;
        }

        //used to jump to correct treenode and then open contextmenu screen
        private void DataBaseTreeview_PreviewMouseRightButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }

            object TVItem = DataBaseTreeview.SelectedItem;

            if (TVItem.GetType() == typeof(Thevenin))
            {
                treeViewItem.ContextMenu = (ContextMenu) DataBaseTreeview.TryFindResource("TheveninItemContext");
            }
            if (TVItem.GetType() == typeof(Transformer))
            {
                treeViewItem.ContextMenu = (ContextMenu)DataBaseTreeview.TryFindResource("XFMRContext");
            }
            if (TVItem.GetType() == typeof(Breaker))
            {
                treeViewItem.ContextMenu = (ContextMenu)DataBaseTreeview.TryFindResource("BreakerContext");
            }
            if (TVItem.GetType() == typeof(Relay))
            {
                treeViewItem.ContextMenu = (ContextMenu)DataBaseTreeview.TryFindResource("RelayItemContext");
            }
            if (TVItem.GetType() == typeof(CT))
            {
                treeViewItem.ContextMenu = (ContextMenu)DataBaseTreeview.TryFindResource("CTItemContext");
            }
        }
    }
}
