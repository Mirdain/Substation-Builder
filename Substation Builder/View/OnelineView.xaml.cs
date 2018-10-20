using MahApps.Metro.Controls;
using Substation_Builder.Model;
using Substation_Builder.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
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
        private void Oneline_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

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
                }
                else if (TVItem.GetType() == typeof(TreeViewItem))
                {

                }
            }
        }

        //Set Data Context properly for Expander from Treeview
        private void OnelineTreeview_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            UpdateExpander();
        }

        //Set datacontext of the expander
        private void OnelineTreeview_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UpdateExpander();
        }

        //Update expander with selected item from UI
        private void UpdateExpander()
        {
            object TVSelectedItem = OnelineTreeview.SelectedItem;

            if (TVSelectedItem != null)
            {
                if (TVSelectedItem.GetType() != typeof(TreeViewItem))
                {
                    if (TVSelectedItem.GetType() == typeof(Thevenin))
                    {
                        Thevenin thevenin = (Thevenin)TVSelectedItem;
                        TreeviewExpander.Content = TVSelectedItem;
                        TreeviewExpander.Header = "Thevenin Impedance";
                        TreeviewExpander.Tag = thevenin.Name;
                    }

                    else if (TVSelectedItem.GetType() == typeof(Breaker))
                    {
                        Breaker breaker = (Breaker)TVSelectedItem;
                        TreeviewExpander.Content = TVSelectedItem;
                        TreeviewExpander.Header = "Curcuit Breaker";
                        TreeviewExpander.Tag = breaker.Name;
                    }

                    else if (TVSelectedItem.GetType() == typeof(Transformer))
                    {
                        Transformer transformer = (Transformer)TVSelectedItem;
                        TreeviewExpander.Content = TVSelectedItem;
                        TreeviewExpander.Header = "Power Transformer";
                        TreeviewExpander.Tag = transformer.Name;
                    }

                    else if (TVSelectedItem.GetType() == typeof(Relay))
                    {
                        Relay relay = (Relay)TVSelectedItem;
                        TreeviewExpander.Content = TVSelectedItem;
                        TreeviewExpander.Header = "Protective Relay";
                        TreeviewExpander.Tag = relay.Name;
                    }

                    else if (TVSelectedItem.GetType() == typeof(CT))
                    {
                        CT cT = (CT)TVSelectedItem;
                        TreeviewExpander.Content = TVSelectedItem;
                        TreeviewExpander.Header = "Current Transformer";
                        TreeviewExpander.Tag = cT.CT_Position;
                    }
                }
                else if (TVSelectedItem.GetType() == typeof(TreeViewItem))
                {
                    TreeViewItem SelectedItem = (TreeViewItem)TVSelectedItem;

                    if (SelectedItem.Name == "SubData")
                    {
                        TreeviewExpander.Content = ((OnelineViewModel)SelectedItem.DataContext).Project.SubData;
                        TreeviewExpander.Header = "Substation Project";
                        TreeviewExpander.Tag = ((OnelineViewModel)SelectedItem.DataContext).Project.SubData.Name;
                    }
                    else
                    {
                        TreeviewExpander.Content = null;
                        TreeviewExpander.Header = "[Select an Element]";
                        TreeviewExpander.Tag = null;
                    }
                }
            }
        }

        //---------------- Drag / Drop Section --------------------------------

        //Drag Source
        private void OnelineTreeview_MouseMove(object sender, MouseEventArgs e)
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
                relay.Visible = "Visible";
            }
            else if (item.GetType() == typeof(Breaker))
            {
                Breaker breaker = (Breaker)item;
                var point = e.GetPosition(this.ListBoxUI);
                breaker.X = point.X - 20;
                breaker.Y = point.Y + 10;
                breaker.Visible = "Visible";
            }
            else if (item.GetType() == typeof(Thevenin))
            {
                Thevenin thevenin = (Thevenin)item;
                var point = e.GetPosition(this.ListBoxUI);
                thevenin.X = point.X - 20;
                thevenin.Y = point.Y + 10;
                thevenin.Visible = "Visible";
            }
            else if (item.GetType() == typeof(Transformer))
            {
                Transformer transformer = (Transformer)item;
                var point = e.GetPosition(this.ListBoxUI);
                transformer.X = point.X - 20;
                transformer.Y = point.Y + 10;
                transformer.Visible = "Visible";
            }
            else if (item.GetType() == typeof(Transformer))
            {
                CT ct = (CT)item;
                ct.Visible = "Visible";
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

        }

        //Remove item from UI (MenuItem is context menu screen)
        private void Remove_Click(object sender, RoutedEventArgs e)
        {
            object item = ((FrameworkElement)sender).DataContext;

            if (item.GetType() == typeof(Breaker))
            {
                Breaker breaker = (Breaker)item;
                breaker.Visible = "Hidden";
            }
            else if (item.GetType() == typeof(Thevenin))
            {
                Thevenin thevenin = (Thevenin)item;
                thevenin.Visible = "Hidden";
            }
            else if (item.GetType() == typeof(Transformer))
            {
                Transformer transformer = (Transformer)item;
                transformer.Visible = "Hidden";
            }
        }

        //Used to select an object when selected from UI
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
            else
            {

            }

        }


        private void ListBoxUI_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            object item = Mouse.DirectlyOver;


            ListBoxUI.SelectedItem = null;
        }

        private void Expander_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxUI.SelectedItem = null;
        }


    }
}
