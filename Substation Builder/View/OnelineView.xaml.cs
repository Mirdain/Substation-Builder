using MahApps.Metro.Controls;
using Substation_Builder.Model;
using Substation_Builder.ViewModel;
using System.Collections.Generic;
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

        public List<Node> Nodes { get; set; }
        public List<Connector> Connectors { get; set; }


        public OnelineView()
        {
            InitializeComponent();
        }

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

        private void OnelineTreeview_MouseMove(object sender, MouseEventArgs e)
        {
            //Drag Source - Create a treeview and get the selected item
            TreeView treeview = sender as TreeView;
            if (treeview.SelectedItem != null && e.LeftButton == MouseButtonState.Pressed)
            {
                DataObject data = new DataObject(DataFormats.Serializable, treeview.SelectedItem);

                if (treeview.SelectedItem.GetType() == typeof(Relay))
                {
                    DragDrop.DoDragDrop(treeview, data, DragDropEffects.Link);
                }
                else if (treeview.SelectedItem.GetType() == typeof(Breaker))
                {
                    DragDrop.DoDragDrop(treeview, data, DragDropEffects.Link);
                }
                else if (treeview.SelectedItem.GetType() == typeof(Thevenin))
                {
                    DragDrop.DoDragDrop(treeview, data, DragDropEffects.Link);
                }
                else if (treeview.SelectedItem.GetType() == typeof(Transformer))
                {
                    DragDrop.DoDragDrop(treeview, data, DragDropEffects.Link);
                }
                else
                {

                }

            }
        }

        //show link when over the listbox
        private void ListBoxUI_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Serializable))
            {
                e.Effects = DragDropEffects.Link;
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
            if (!(sender is Thumb thumb))
                return;

            if (!(thumb.DataContext is Node node))
                return;

            node.X += e.HorizontalChange;
            node.Y += e.VerticalChange;
        }

        private void ListBox_PreviewMouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

            if (!(sender is ListBox listbox))
                return;

            if (!(listbox.DataContext is OnelineViewModel vm))
                return;

            if (vm.SelectedObject != null && vm.SelectedObject is Node && vm.SelectedObject.IsNew)
            {
                vm.SelectedObject.X = e.GetPosition(listbox).X;
                vm.SelectedObject.Y = e.GetPosition(listbox).Y;
            }
            else if (vm.SelectedObject != null && vm.SelectedObject is Connector && vm.SelectedObject.IsNew)
            {
                var node = GetNodeUnderMouse();
                if (node == null)
                    return;

                var connector = vm.SelectedObject as Connector;

                if (connector.Start != null && node != connector.Start)
                    connector.End = node;
            }
        }

        private void ListBox_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is OnelineViewModel vm)
            {
                //Connecting objects
                if (vm.CreatingNewConnector)
                {
                    var node = GetNodeUnderMouse();
                    var connector = vm.SelectedObject as Connector;

                    if (node != null && connector != null && connector.Start == null)
                    {
                        connector.Start = node;
                        node.IsHighlighted = true;
                        e.Handled = true;
                        return;
                    }
                }

                //Checks if new item - if not set to false
                if (vm.SelectedObject != null)
                {
                    vm.SelectedObject.IsNew = false;
                }

                //remove selected item if
                if (GetItemUnderMouse() == false)
                {
                    ListBoxUI.SelectedItem = null;
                }

                vm.CreatingNewNode = false;
                vm.CreatingNewConnector = false;
            }
        }

        //Retruns the Node that is selected
        private Node GetNodeUnderMouse()
        {
            if (!(Mouse.DirectlyOver is ContentPresenter item))
                return null;

            return item.DataContext as Node;
        }

        //Retruns the Node that is selected
        private bool GetItemUnderMouse()
        {
            if ((Mouse.DirectlyOver.GetType() == typeof(System.Windows.Shapes.Rectangle)))
                return true;

            return false;
        }

        private void OnelineTreeview_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            object TVSelectedItem = OnelineTreeview.SelectedItem;

            if (TVSelectedItem.GetType() != typeof(TreeViewItem))
            {
                if(TVSelectedItem.GetType() == typeof(Thevenin))
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

                else
                {
          
                }

            }
        }
    }
}
