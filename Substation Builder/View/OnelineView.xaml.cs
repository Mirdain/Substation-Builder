using MahApps.Metro.Controls;
using Substation_Builder.Model;
using Substation_Builder.ViewModel;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

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

        private void OnelineTreeview_MouseMove(object sender, MouseEventArgs e)
        {
            //Drag Source - Create a treeview and get the selected item
            TreeView treeview = sender as TreeView;
            if (treeview.SelectedItem != null && e.LeftButton == MouseButtonState.Pressed)
            {;

                if (treeview.SelectedItem.GetType() == typeof(Relay))
                {
                    DragDrop.DoDragDrop(treeview, treeview.SelectedItem, DragDropEffects.Link);
                }
                else if (treeview.SelectedItem.GetType() == typeof(Breaker))
                {
                    DragDrop.DoDragDrop(treeview, treeview.SelectedItem, DragDropEffects.Link);
                }
                else if (treeview.SelectedItem.GetType() == typeof(Thevenin))
                {
                    DragDrop.DoDragDrop(treeview, treeview.SelectedItem, DragDropEffects.Link);
                }
                else if (treeview.SelectedItem.GetType() == typeof(Transformer))
                {
                    DragDrop.DoDragDrop(treeview, treeview.SelectedItem, DragDropEffects.Link);
                }
                else
                {

                }

            }
        }

        //Drop destination trigger
        private void Border_DragEnter(object sender, DragEventArgs e)
        {

        }

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
                if(GetItemUnderMouse() == false)
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

    }
}
