using Substation_Builder.Helpers;
using Substation_Builder.Model;
using Substation_Builder.View;
using System.Collections.ObjectModel;
using System.Linq;

namespace Substation_Builder.ViewModel
{
    class OnelineViewModel : ViewModelBase
    {
        private Substation project;
        public Substation Project
        {
            get { return project; }
            set
            {
                project = value;
                NotifyPropertyChanged();
            }
        }

        OnelineView OLView = new OnelineView();

        public RelayCommand ResetZoom { get; private set; }

        private double _zoomLevel = 1;
        public double ZoomLevel
        {
            get { return _zoomLevel; }
            set
            {
                _zoomLevel = value;
                ZoomLevelPercent = (value * 100).ToString() + "%";
                NotifyPropertyChanged("ZoomLevel");
            }
        }

        private string _zoomLevelPercent = "100%";
        public string ZoomLevelPercent
        {
            get { return _zoomLevelPercent; }
            set{
                _zoomLevelPercent = value;
                NotifyPropertyChanged("ZoomLevelPercent");
            }
        }

        public OnelineViewModel(Substation refproject)
          {
            Project = refproject;

            //Copied section - this generates the random nodes to display
            _nodes = new ObservableCollection<Node>();
            _connectors = new ObservableCollection<Connector>();

            ResetZoom = new RelayCommand(ResetZ);

            ShowAllCoordinates = true;
            ShowNames = true;

            OLView.DataContext = this;
            OLView.Show();
        }

        public void ResetZ(object sender)
        {
            ZoomLevel = 1;
        }

        #region Collections

        private ObservableCollection<Node> _nodes;
        public ObservableCollection<Node> Nodes
        {
            get { return _nodes ?? (_nodes = new ObservableCollection<Node>()); }
        }

        private ObservableCollection<Connector> _connectors;
        public ObservableCollection<Connector> Connectors
        {
            get { return _connectors ?? (_connectors = new ObservableCollection<Connector>()); }
        }

        private DiagramObject _selectedObject;
        public DiagramObject SelectedObject
        {
            get
            {
                return _selectedObject;
            }
            set
            {
                Nodes.ToList().ForEach(x => x.IsHighlighted = false);

                _selectedObject = value;
                NotifyPropertyChanged("SelectedObject");

                if (value is Connector connector)
                {
                    if (connector.Start != null)
                        connector.Start.IsHighlighted = true;

                    if (connector.End != null)
                        connector.End.IsHighlighted = true;
                }
            }
        }

        #endregion

        #region Bool (Visibility) Options

        private bool _showNames;
        public bool ShowNames
        {
            get { return _showNames; }
            set
            {
                _showNames = value;
                NotifyPropertyChanged("ShowNames");
            }
        }

        private bool _showCurrentCoordinates;
        public bool ShowCurrentCoordinates
        {
            get { return _showCurrentCoordinates; }
            set
            {
                _showCurrentCoordinates = value;
                NotifyPropertyChanged("ShowCurrentCoordinates");
            }
        }

        private bool _showAllCoordinates;
        public bool ShowAllCoordinates
        {
            get { return _showAllCoordinates; }
            set
            {
                _showAllCoordinates = value;
                NotifyPropertyChanged("ShowAllCoordinates");
            }
        }

        #endregion

        #region Creating New Objects

        private bool _creatingNewNode;
        public bool CreatingNewNode
        {
            get { return _creatingNewNode; }
            set
            {
                _creatingNewNode = value;
                NotifyPropertyChanged("CreatingNewNode");

                if (value)
                    CreateNewNode();
                else
                    RemoveNewObjects();
            }
        }

        public void CreateNewNode()
        {
            var newnode = new Node()
            {
                Name = "Node" + (Nodes.Count + 1),
                IsNew = true
            };

            Nodes.Add(newnode);
            SelectedObject = newnode;
        }

        public void RemoveNewObjects()
        {
            Nodes.Where(x => x.IsNew).ToList().ForEach(x => Nodes.Remove(x));
            Connectors.Where(x => x.IsNew).ToList().ForEach(x => Connectors.Remove(x));
        }

        private bool _creatingNewConnector;
        public bool CreatingNewConnector
        {
            get { return _creatingNewConnector; }
            set
            {
                _creatingNewConnector = value;
                NotifyPropertyChanged("CreatingNewConnector");

                if (value)
                    CreateNewConnector();
                else
                    RemoveNewObjects();
            }
        }

        public void CreateNewConnector()
        {
            var connector = new Connector()
            {
                Name = "Connector" + (Connectors.Count + 1),
                IsNew = true,
            };

            Connectors.Add(connector);
            SelectedObject = connector;
        }

        #endregion

        #region Delete Command

        private RelayCommand _deleteCommand;
        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand = new RelayCommand(Delete,Can_Delete)); }
        }

        private void Delete(object sender)
        {
            if (SelectedObject is Connector)
                Connectors.Remove(SelectedObject as Connector);

            if (SelectedObject is Node)
            {
                var node = SelectedObject as Node;
                Connectors.Where(x => x.Start == node || x.End == node).ToList().ForEach(x => Connectors.Remove(x));
                Nodes.Remove(node);
            }
        }

        private bool Can_Delete(object sender)
        {
            if (SelectedObject is Connector)
                return true;

            if (SelectedObject is Node)
                return true;

            return false;
        }

        #endregion

        #region Scrolling support

        private double _areaHeight = 600;
        public double AreaHeight
        {
            get { return _areaHeight; }
            set
            {
                _areaHeight = value;
                NotifyPropertyChanged("AreaHeight");
            }
        }

        private double _areaWidth = 600;
        public double AreaWidth
        {
            get { return _areaWidth; }
            set
            {
                _areaWidth = value;
                NotifyPropertyChanged("AreaWidth");
            }
        }

        #endregion
    }

}
