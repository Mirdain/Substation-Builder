using MahApps.Metro.Controls;
using Substation_Builder.Model;
using Substation_Builder.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace Substation_Builder.View
{
    /// <summary>
    /// Interaction logic for OnelineView.xaml
    /// </summary>
    public partial class OnelineView : MetroWindow
    {
        public OnelineView()
        {
            InitializeComponent();
        }
        //------------------  Treeview Section ----------------------------
        //searches the treeview
        static TreeViewItem VisualUpwardSearch(DependencyObject source)
        {
            while (source != null && !(source is TreeViewItem))
                source = VisualTreeHelper.GetParent(source);

            return source as TreeViewItem;
        }
        //used to jump to correct treenode and then open contextmenu screen
        private void UpdateContextMenu (object sender, MouseButtonEventArgs e)
        {
            /*
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);
            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }
            object TVItem = OnelineTreeview.SelectedItem;
            if (TVItem != null)
            {
                if (TVItem.GetType() == typeof(Thevenin))
                {
                    treeViewItem.ContextMenu = (ContextMenu)OnelineTreeview.TryFindResource("TheveninItemContext");
                }
                else if (TVItem.GetType() == typeof(Transformer))
                {
                    treeViewItem.ContextMenu = (ContextMenu)OnelineTreeview.TryFindResource("XFMRContext");
                }
                else if (TVItem.GetType() == typeof(Breaker))
                {
                    treeViewItem.ContextMenu = (ContextMenu)OnelineTreeview.TryFindResource("BreakerContext");
                }
                else if (TVItem.GetType() == typeof(Relay))
                {
                    treeViewItem.ContextMenu = (ContextMenu)OnelineTreeview.TryFindResource("RelayItemContext");
                }
                else if (TVItem.GetType() == typeof(CT))
                {
                    treeViewItem.ContextMenu = (ContextMenu)OnelineTreeview.TryFindResource("CTItemContext");
                    //Update the Context for the correct CT object
                    treeViewItem.ContextMenu.DataContext = TVItem;
                }
                else if (TVItem.GetType() == typeof(TreeViewItem))
                {

                }
            }
            */
        }
        private void UpdateProjectExpander(object sender, MouseButtonEventArgs e)
        {
            Border item = (Border)sender;
            OnelineViewModel substation = (OnelineViewModel)item.DataContext;
            TreeviewExpander.Content = substation.Project.SubData;
            TreeviewExpander.Header = "Substation Data";
            TreeviewExpander.Tag = substation.Project.SubData.Name;
        }
        //Update expander with selected item the treeview
        private void UpdateExpander(object sender, MouseButtonEventArgs e)
        {
            TextBlock item = (TextBlock)sender;
            object selecteditem = item.DataContext;
            if (selecteditem != null)
            {
                if (selecteditem.GetType() == typeof(Thevenin))
                {
                    Thevenin thevenin = (Thevenin)selecteditem;
                    TreeviewExpander.Content = selecteditem;
                    TreeviewExpander.Header = "Thevenin Impedance";
                    TreeviewExpander.Tag = thevenin.Name;
                    TreeviewExpander.IsExpanded = true;
                }
                if (selecteditem.GetType() == typeof(Transformer))
                {
                    Transformer transformer = (Transformer)selecteditem;
                    TreeviewExpander.Content = selecteditem;
                    TreeviewExpander.Header = "Power Transformer";
                    TreeviewExpander.Tag = transformer.Name;
                    TreeviewExpander.IsExpanded = true;
                }
                if (selecteditem.GetType() == typeof(Breaker))
                {
                    Breaker breaker = (Breaker)selecteditem;
                    TreeviewExpander.Content = selecteditem;
                    TreeviewExpander.Header = "Curcuit Breaker";
                    TreeviewExpander.Tag = breaker.Name;
                    TreeviewExpander.IsExpanded = true;
                }
                else if (selecteditem.GetType() == typeof(Relay))
                {
                    Relay relay = (Relay)selecteditem;
                    TreeviewExpander.Content = selecteditem;
                    TreeviewExpander.Header = "Protective Relay";
                    TreeviewExpander.Tag = relay.Name;
                    TreeviewExpander.IsExpanded = true;
                }
                else if (selecteditem.GetType() == typeof(CT))
                {
                    CT cT = (CT)selecteditem;
                    TreeviewExpander.Content = selecteditem;
                    TreeviewExpander.Header = "Current Transformer";
                    TreeviewExpander.Tag = cT.CT_Position;
                    TreeviewExpander.IsExpanded = true;
                }
            }
        }
        //---------------- Drag / Drop Section --------------------------------
        //Drag Source
        private void Treeview_MouseMove(object sender, MouseEventArgs e)
        {
            //Drag Source - Create a treeview and get the selected item
            TreeView treeview = sender as TreeView;

            //If correct data type serialize it and put it in the dragdrop item
            if (treeview.SelectedItem != null && e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject(DataFormats.Serializable, treeview.SelectedItem);

                if (treeview.SelectedItem.GetType() == typeof(Relay))
                {
                    DragDrop.DoDragDrop(treeview, data, DragDropEffects.Copy);
                }
                else if (treeview.SelectedItem.GetType() == typeof(Breaker))
                {
                    DragDrop.DoDragDrop(treeview, data, DragDropEffects.Copy);
                }
                else if (treeview.SelectedItem.GetType() == typeof(Thevenin))
                {
                    DragDrop.DoDragDrop(treeview, data, DragDropEffects.Copy);
                }
                else if (treeview.SelectedItem.GetType() == typeof(Transformer))
                {
                    DragDrop.DoDragDrop(treeview, data, DragDropEffects.Copy);
                }
                else if (treeview.SelectedItem.GetType() == typeof(CT))
                {
                    DragDrop.DoDragDrop(treeview, data, DragDropEffects.Copy);
                }
                else
                {

                }
            }
        }
        //show copy cursor when over the listbox
        private void ListBoxUI_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Serializable))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }
            e.Handled = true;
        }
        //Block out void space on boarder
        private void Border_DragOver(object sender, DragEventArgs e)
        {
            e.Effects = DragDropEffects.None;
            e.Handled = true;
        }
        //Drop action
        private void ListBoxUI_Drop(object sender, DragEventArgs e)
        {
            object item = e.Data.GetData(DataFormats.Serializable);

            if (item.GetType() == typeof(Relay))
            {
                Relay relay = (Relay)item;
                var point = e.GetPosition(this.ListBoxUI);
                relay.X = point.X - 20;
                relay.Y = point.Y + 10;
                relay.Visibility = "Visible";
            }
            else if (item.GetType() == typeof(Breaker))
            {
                Breaker breaker = (Breaker)item;
                var point = e.GetPosition(this.ListBoxUI);
                breaker.X = point.X - 20;
                breaker.Y = point.Y + 10;
                breaker.Visibility = "Visible";
            }
            else if (item.GetType() == typeof(Thevenin))
            {
                Thevenin thevenin = (Thevenin)item;
                var point = e.GetPosition(this.ListBoxUI);
                thevenin.X = point.X - 20;
                thevenin.Y = point.Y + 10;
                thevenin.Visibility = "Visible";
            }
            else if (item.GetType() == typeof(Transformer))
            {
                Transformer transformer = (Transformer)item;
                var point = e.GetPosition(this.ListBoxUI);
                transformer.X = point.X - 20;
                transformer.Y = point.Y + 10;
                transformer.Visibility = "Visible";
            }
            else if (item.GetType() == typeof(CT))
            {
                CT ct = (CT)item;
                ct.Visibility = "Visible";
            }
        }
        //Move the item
        private void Thumb_Drag(object sender, DragDeltaEventArgs e)
        {
            if (!(sender is Thumb thumb))
                return;
            if (thumb.DataContext is Breaker breaker)
            {
                breaker.X += e.HorizontalChange;
                breaker.Y += e.VerticalChange;
            }
            else if (thumb.DataContext is Thevenin thevenin)
            {
                thevenin.X += e.HorizontalChange;
                thevenin.Y += e.VerticalChange;
            }
            else if (thumb.DataContext is Transformer transformer)
            {
                transformer.X += e.HorizontalChange;
                transformer.Y += e.VerticalChange;
            }
            else if (thumb.DataContext is Relay relay)
            {
                relay.X += e.HorizontalChange;
                relay.Y += e.VerticalChange;
            }

        }
        //Remove item from UI (MenuItem is context menu screen)
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            object item = TreeviewExpander.Content;  
            if (item.GetType() == typeof(Breaker))
            {
                Breaker breaker = (Breaker)item;
                breaker.Visibility = "Collapsed";
            }
            else if (item.GetType() == typeof(Thevenin))
            {
                Thevenin thevenin = (Thevenin)item;
                thevenin.Visibility = "Collapsed";
            }
            else if (item.GetType() == typeof(Transformer))
            {
                Transformer transformer = (Transformer)item;
                transformer.Visibility = "Collapsed";
            }
            else if (item.GetType() == typeof(CT))
            {
                CT cT = (CT)item;
                cT.Visibility = "Collapsed";
            }
            else if (item.GetType() == typeof(Relay))
            {
                Relay relay = (Relay)item;
                relay.Visibility = "Collapsed";
            }
        }
        //Show all CTsR
        private void Show_All_CTs(object sender, RoutedEventArgs e)
        {
            object item = ((FrameworkElement)sender).DataContext;

            if (item.GetType() == typeof(Transformer))
            {
                Transformer transformer = (Transformer)item;
                if (transformer.CTs.Count > 0)
                {
                    for (int i = 0; i < transformer.CTs.Count; i++)
                    {
                        transformer.CTs[i].Visibility = "Visible";
                    }
                }
            }
            else if (item.GetType() == typeof(Breaker))
            {
                Breaker breaker = (Breaker)item;
                if (breaker.CTs.Count > 0)
                {
                    for (int i = 0; i < breaker.CTs.Count; i++)
                    {
                        breaker.CTs[i].Visibility = "Visible";
                    }
                }
            }

        }
        //Hide all CTs
        private void Hide_All_CTs(object sender, RoutedEventArgs e)
        {
            object item = ((FrameworkElement)sender).DataContext;

            if (item.GetType() == typeof(Transformer))
            {
                Transformer transformer = (Transformer)item;
                if (transformer.CTs.Count > 0)
                {
                    for (int i = 0; i < transformer.CTs.Count; i++)
                    {
                        transformer.CTs[i].Visibility = "Collapsed";
                    }
                }
            }
            else if (item.GetType() == typeof(Breaker))
            {
                Breaker breaker = (Breaker)item;
                if (breaker.CTs.Count > 0)
                {
                    for (int i = 0; i < breaker.CTs.Count; i++)
                    {
                        breaker.CTs[i].Visibility = "Collapsed";
                    }
                }
            }
        }
        //Used to set the expander based on an object from the UI (Listbox)
        private void Thumb_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            object item = e.OriginalSource;
            if (((FrameworkElement)item).DataContext.GetType() == typeof(Breaker))
            {
                Breaker breaker = (Breaker)((FrameworkElement)item).DataContext;
                ListBoxUI.SelectedItem = breaker;
                TreeviewExpander.Content = breaker;
                TreeviewExpander.Header = "Curcuit Breaker";
                TreeviewExpander.Tag = breaker.Name;
            }
            else if (((FrameworkElement)item).DataContext.GetType() == typeof(Thevenin))
            {
                Thevenin thevenin = (Thevenin)((FrameworkElement)item).DataContext;
                ListBoxUI.SelectedItem = thevenin;
                TreeviewExpander.Content = thevenin;
                TreeviewExpander.Header = "Thevenin Impedance";
                TreeviewExpander.Tag = thevenin.Name;
            }
            else if (((FrameworkElement)item).DataContext.GetType() == typeof(Transformer))
            {
                Transformer transformer = (Transformer)((FrameworkElement)item).DataContext;
                ListBoxUI.SelectedItem = transformer;
                TreeviewExpander.Content = transformer;
                TreeviewExpander.Header = "Power Transformer";
                TreeviewExpander.Tag = transformer.Name;
            }
            else if (((FrameworkElement)item).DataContext.GetType() == typeof(CT))
            {
                CT ct = (CT)((FrameworkElement)item).DataContext;
                ListBoxUI.SelectedItem = ct;
                TreeviewExpander.Content = ct;
                TreeviewExpander.Header = "Current Transformer";
                TreeviewExpander.Tag = ct.CT_Position;
            }
            else if (((FrameworkElement)item).DataContext.GetType() == typeof(Relay))
            {
                Relay relay = (Relay)((FrameworkElement)item).DataContext;
                ListBoxUI.SelectedItem = relay;
                TreeviewExpander.Content = relay;
                TreeviewExpander.Header = "Relay";
                TreeviewExpander.Tag = relay.Position;
            }
            else
            {

            }

        }
        private void ListBoxUI_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBox listBox = (ListBox)sender;

            object item = listBox.SelectedItem;

            if (item != null)
            {

                if (item.GetType() == typeof(Relay))
                {
                    Relay relay = (Relay)item;

                }
                else if (item.GetType() == typeof(Breaker))
                {
                    Breaker breaker = (Breaker)item;

                }
                else if (item.GetType() == typeof(Thevenin))
                {
                    Thevenin thevenin = (Thevenin)item;

                }
                else if (item.GetType() == typeof(Transformer))
                {
                    Transformer transformer = (Transformer)item;

                }
                else if (item.GetType() == typeof(CT))
                {

                }
            }
            //  selectedItemObject is not a TreeViewItem, but an item from the collection that 
            //  populated the TreeView.
        }
        private void Expander_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxUI.SelectedItem = null;
        }
        private void Border_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            Thumb_PreviewMouseLeftButtonDown(sender, e);
        }
    }
}
