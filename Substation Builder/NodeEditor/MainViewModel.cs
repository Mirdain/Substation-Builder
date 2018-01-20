using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace Substation_Builder.NodesEditor
{
    public class MainViewModel: INotifyPropertyChanged
    {
        #region Collections

        private ObservableCollection<Node> _nodes;
        public ObservableCollection<Node> Nodes
        {
            get { return _nodes ?? (_nodes = new ObservableCollection<Node>()); }
        }

        private ObservableCollection<Node> _xfmrs;
        public ObservableCollection<Node> Xfmrs
        {
            get { return _xfmrs ?? (_xfmrs = new ObservableCollection<Node>()); }
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
                OnPropertyChanged("SelectedObject");

                DeleteCommand.IsEnabled = value != null;

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
                OnPropertyChanged("ShowNames");
            }
        }

        private bool _showCurrentCoordinates;
        public bool ShowCurrentCoordinates
        {
            get { return _showCurrentCoordinates; }
            set
            {
                _showCurrentCoordinates = value;
                OnPropertyChanged("ShowCurrentCoordinates");
            }
        }

        private bool _showAllCoordinates;
        public bool ShowAllCoordinates
        {
            get { return _showAllCoordinates; }
            set
            {
                _showAllCoordinates = value;
                OnPropertyChanged("ShowAllCoordinates");
            }
        }

        #endregion

        #region Constructor

        public MainViewModel()
        {

            ShowAllCoordinates = true;
            ShowNames = true;
        }

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
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
                OnPropertyChanged("CreatingNewNode");

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
                                  Name = "Breaker " + (Nodes.Count + 1),
                                  IsNew = true
                              };

            Nodes.Add(newnode);
            SelectedObject = newnode;
        }


        //Create new Xfmr

        private bool _creatingNewXfmr;
        public bool CreatingNewXfmr
        {
            get { return _creatingNewXfmr; }
            set
            {
                _creatingNewXfmr = value;
                OnPropertyChanged("CreatingNewXfmr");

                if (value)
                    CreateNewXfmr();
                else
                    RemoveNewObjects();
            }
        }

        public void CreateNewXfmr()
        {
            var newxfmr = new Node()
            {
                Name = "Transformer " + (Xfmrs.Count + 1),
                IsNew = true
            };

            Xfmrs.Add(newxfmr);
            SelectedObject = newxfmr;
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
                OnPropertyChanged("CreatingNewConnector");

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

        private Command _deleteCommand;
        public Command DeleteCommand
        {
            get { return _deleteCommand ?? (_deleteCommand =new Command(Delete)); }
        }

        private void Delete()
        {
            if (SelectedObject is Connector)
                Connectors.Remove(SelectedObject as Connector);

            if (SelectedObject is Node)
            {
                var node = SelectedObject as Node;
                Connectors.Where(x => x.Start == node || x.End == node).ToList().ForEach(x => Connectors.Remove(x));
                Nodes.Remove(node);
            }

            if (SelectedObject is Xfmr)
            {
                var xfmr = SelectedObject as Node;
                Connectors.Where(x => x.Start == xfmr || x.End == xfmr).ToList().ForEach(x => Connectors.Remove(x));
                Nodes.Remove(xfmr);
            }

        }

        #endregion

        #region Scrolling support

        private double _areaHeight = 500;
        public double AreaHeight
        {
            get { return _areaHeight; }
            set
            {
                _areaHeight = value;
                OnPropertyChanged("AreaHeight");
            }
        }

        private double _areaWidth = 500;
        public double AreaWidth
        {
            get { return _areaWidth; }
            set
            {
                _areaWidth = value;
                OnPropertyChanged("AreaWidth");
            }
        }

        #endregion
    }
}
