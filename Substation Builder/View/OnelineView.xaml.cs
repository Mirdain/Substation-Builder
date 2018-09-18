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
        private void Oneline_PreviewMouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            TreeViewItem treeViewItem = VisualUpwardSearch(e.OriginalSource as DependencyObject);

            if (treeViewItem != null)
            {
                treeViewItem.Focus();
                e.Handled = true;
            }

            object TVItem = OnelineTreeview.SelectedItem;

            if (TVItem.GetType() == typeof(Thevenin))
            {
                treeViewItem.ContextMenu = (ContextMenu) OnelineTreeview.TryFindResource("TheveninItemContext");
            }
            else if (TVItem.GetType() == typeof(Transformer))
            {
                treeViewItem.ContextMenu = (ContextMenu) OnelineTreeview.TryFindResource("XFMRContext");
            }
            else if (TVItem.GetType() == typeof(Breaker))
            {
                treeViewItem.ContextMenu = (ContextMenu) OnelineTreeview.TryFindResource("BreakerContext");
            }
            else if (TVItem.GetType() == typeof(Relay))
            {
                treeViewItem.ContextMenu = (ContextMenu) OnelineTreeview.TryFindResource("RelayItemContext");
            }
            else if (TVItem.GetType() == typeof(CT))
            {
                treeViewItem.ContextMenu = (ContextMenu) OnelineTreeview.TryFindResource("CTItemContext");
            }
            else if (TVItem.GetType() == typeof(TreeViewItem))
            {


            }
        }

        //Set Data Context properly for Expander from Treeview
        private void OnelineTreeview_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            object TVSelectedItem = OnelineTreeview.SelectedItem;

            if (TVSelectedItem.GetType() != typeof(TreeViewItem))
            {
                if (TVSelectedItem.GetType() == typeof(Thevenin))
                {
                    Thevenin thevenin = (Thevenin)TVSelectedItem;
                    TreeviewExpander.Content = TVSelectedItem;
                    TreeviewExpander.Header = thevenin.Name;
                }

                else if (TVSelectedItem.GetType() == typeof(Breaker))
                {
                    Breaker breaker = (Breaker)TVSelectedItem;
                    TreeviewExpander.Content = TVSelectedItem;
                    TreeviewExpander.Header = breaker.Name;
                }

                else if (TVSelectedItem.GetType() == typeof(Transformer))
                {
                    Transformer transformer = (Transformer)TVSelectedItem;
                    TreeviewExpander.Content = TVSelectedItem;
                    TreeviewExpander.Header = transformer.Name;
                }

                else if (TVSelectedItem.GetType() == typeof(Relay))
                {
                    Relay relay = (Relay)TVSelectedItem;
                    TreeviewExpander.Content = TVSelectedItem;
                    TreeviewExpander.Header = relay.Name;
                }

                else if (TVSelectedItem.GetType() == typeof(CT))
                {
                    CT cT = (CT)TVSelectedItem;
                    TreeviewExpander.Content = TVSelectedItem;
                    TreeviewExpander.Header = cT.Name;
                }
            }
            else if (TVSelectedItem.GetType() == typeof(TreeViewItem))
            {
                TreeViewItem SelectedItem = (TreeViewItem)TVSelectedItem;

                if (SelectedItem.Name == "Subdata")
                {
                    TreeviewExpander.Content = ((OnelineViewModel)SelectedItem.DataContext).Project.SubData;
                    TreeviewExpander.Header = ((OnelineViewModel)SelectedItem.DataContext).Project.SubData.Name;
                }
                else
                {
                    TreeviewExpander.Content = null;
                    TreeviewExpander.Header = "[Select an Element]";
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

        //show link when over the listbox and render a preview
        private void ListBoxUI_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Serializable))
            {
                e.Effects = DragDropEffects.Copy;

                var dataobject = e.Data as DataObject;

                if (dataobject.GetData(DataFormats.Serializable).GetType() == typeof (Breaker))
                {
                    //Get the Breaker data
                    Breaker breaker = (Breaker)dataobject.GetData(DataFormats.Serializable);

                    //Make the breaker visible
                    //((OnelineViewModel)DataContext).AddBreakerItem(breaker);
                }

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
            else
            {

            }

        }





        //Begin the Node code

        private void Thumb_Drag(object sender, DragDeltaEventArgs e)
        {
            object item = ((FrameworkElement)sender).DataContext;

            if(item.GetType() == typeof(Breaker))
            {
                Breaker breaker = (Breaker)item;
                breaker.X += e.HorizontalChange;
                breaker.Y += e.VerticalChange;
                
            }



            e.Handled = true;
        }

        private void ListBox_PreviewMouseMove(object sender, MouseEventArgs e)
        {


        }

        private void ListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {

        }

    }
}
