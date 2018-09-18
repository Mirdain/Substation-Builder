using Substation_Builder.Helpers;
using Substation_Builder.Model;
using Substation_Builder.View;
using Substation_Builder.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Linq;
using System.Windows.Controls;
using Substation_Builder.Resources.Monster;
using System.Windows.Controls.Primitives;

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

        private SystemIO FileIO = new SystemIO();
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand LoadCommand { get; private set; }
        public RelayCommand TemplateCommand { get; private set; }
        public RelayCommand NewCommand { get; private set; }
        public RelayCommand RemoveItemCommand { get; private set; }
        public RelayCommand AddCTCommand { get; private set; }
        public RelayCommand AddItemCommand { get; private set; }
        public RelayCommand ResetZoom { get; private set; }

        OnelineView OLView = new OnelineView();

        public OnelineViewModel(Substation refproject)
          {
            Project = refproject;

            LoadCommand = new RelayCommand(LoadFile);
            SaveCommand = new RelayCommand(SaveFile);
            TemplateCommand = new RelayCommand(LoadTemplate);
            NewCommand = new RelayCommand(NewProject);
            AddItemCommand = new RelayCommand(AddItem, Can_Add);
            RemoveItemCommand = new RelayCommand(RemoveItem, Can_Remove);
            AddCTCommand = new RelayCommand(AddCT);
            ResetZoom = new RelayCommand(ResetZ);

            ShowAllCoordinates = true;
            ShowNames = true;

            OLView.DataContext = this;
            OLView.Show();
        }

        #region Context Menu Section

        //read a .xaml file and load into the Datamodel classes
        private void LoadFile(object sender)
        {
            FileIO.FileOpen(Project);
        }

        //Create Blank Project
        private void NewProject(object sender)
        {
            Monster monster = new Monster();

            Substation substation = new Substation
            {
                SubData = new SubstationData
                {
                    Name = "New Project",
                    Monster = monster.MonsterName()
                }
            };

            Project.Replace(substation);
        }

        //Serialize the DataModel and save
        public void SaveFile(object sender)
        {
            FileIO.FileSave(Project);
        }

        //Load Template
        public void LoadTemplate(object sender)
        {
            FileIO.LoadTemplate(Project);
        }

        //Add Item in Treeview
        public void AddItem(object sender)
        {

            if (sender.ToString() == "Thevenin")
            {
                Thevenin thevenin = new Thevenin { Name = "New Thevenin " + (Project.Thevenins.Count + 1).ToString() };
                Project.Thevenins.Add(thevenin);
            }

            if (sender.ToString() == "Project")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Save Curent Project?", "Create New Project", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                if (messageBoxResult == MessageBoxResult.No)
                {
                    Monster monster = new Monster();

                    Project = new Substation
                    {
                        SubData = new SubstationData
                        {
                            Name = "New Project",
                            Monster = monster.MonsterName()
                        }
    
                    };

                }
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    SaveFile(sender);

                    Monster monster = new Monster();

                    Project = new Substation
                    {
                        SubData = new SubstationData
                        {
                            Name = "New Project",
                            Monster = monster.MonsterName()
                        }

                    };
                }
            }

            if (sender.ToString() == "Breaker")
            {
                Breaker breaker = new Breaker { Name = "New Breaker " + (Project.Breakers.Count + 1).ToString() };
                Project.Breakers.Add(breaker);
            }

            if (sender.ToString() == "Relay")
            {
                Relay relay = new Relay { Name = "New Relay " + (Project.Relays.Count + 1).ToString() };
                Project.Relays.Add(relay);
            }

            if (sender.ToString() == "Transformer")
            {
                Transformer transformer = new Transformer { Name = "New Transformer " + (Project.Transformers.Count + 1).ToString() };
                Project.Transformers.Add(transformer);
            }
        }

        //Add a CT
        public void AddCT(object sender)
        {
            if (sender != null)
            {
                if (sender.GetType() == typeof(Breaker))
                {
                    int index = Project.Breakers.IndexOf(sender as Breaker);
                    CT ct = new CT { Name = "CT" + (Project.Breakers[index].CTs.Count + 1).ToString() };
                    Project.Breakers[index].CTs.Add(ct);
                }
                else if (sender.GetType() == typeof(Transformer))
                {
                    int index = Project.Transformers.IndexOf(sender as Transformer);
                    CT ct = new CT { Name = "CT" + (Project.Transformers[index].CTs.Count + 1).ToString() };
                    Project.Transformers[index].CTs.Add(ct);
                }
            }
        }

        //Select Item to remove
        public void RemoveItem(object sender)
        {
            if (sender.GetType() == typeof(Thevenin))
            {
                Project.Thevenins.Remove(sender as Thevenin);
            }
            else if (sender.GetType() == typeof(Breaker))
            {
                Project.Breakers.Remove(sender as Breaker);
            }
            else if (sender.GetType() == typeof(Relay))
            {
                Project.Relays.Remove(sender as Relay);
            }
            else if (sender.GetType() == typeof(Transformer))
            {
                Project.Transformers.Remove(sender as Transformer);
            }
            else if (sender.GetType() == typeof(CT))
            {
                for (int i = 0; i < Project.Transformers.Count; i++)
                {
                    int index = Project.Transformers[i].CTs.IndexOf((CT)sender);
                    if (index >= 0)
                        Project.Transformers[i].CTs.RemoveAt(index);
                }

                for (int i = 0; i < Project.Breakers.Count; i++)
                {
                    int index = Project.Breakers[i].CTs.IndexOf((CT)sender);
                    if (index >= 0)
                        Project.Breakers[i].CTs.RemoveAt(index);
                }
            }
        }

        //Controls abiliity to add/remove items
        public bool Can_Add(object sender)
        {
            bool canexecute = false;

            if (OLView.OnelineTreeview.SelectedItem != null)
            {
                if (OLView.OnelineTreeview.SelectedItem.GetType() == typeof(TreeViewItem))
                {
                    TreeViewItem TVI = (TreeViewItem) OLView.OnelineTreeview.SelectedItem;

                    if (TVI.Header.ToString() == "Thevenins")
                    {
                        canexecute = true;
                    }
                    else if (TVI.Header.ToString() == "Transformers")
                    {
                        canexecute = true;
                    }
                    else if (TVI.Header.ToString() == "Breakers")
                    {
                        canexecute = true;
                    }
                    else if (TVI.Header.ToString() == "Relays")
                    {
                        canexecute = true;
                    }
                }
            }
            return canexecute;
        }

        //Controls abiliity to add/remove items
        public bool Can_Remove(object sender)
        {
            bool canexecute = false;

            if (OLView.OnelineTreeview.SelectedItem != null)
            {
                if (OLView.OnelineTreeview.SelectedValue.GetType() == typeof(Thevenin))
                {
                    canexecute = true;
                }
                else if (OLView.OnelineTreeview.SelectedValue.GetType() == typeof(Transformer))
                {
                    canexecute = true;
                }
                else if (OLView.OnelineTreeview.SelectedValue.GetType() == typeof(Breaker))
                {
                    canexecute = true;
                }
                else if (OLView.OnelineTreeview.SelectedValue.GetType() == typeof(Relay))
                {
                    canexecute = true;
                }
                else if (OLView.OnelineTreeview.SelectedValue.GetType() == typeof(CT))
                {
                    canexecute = true;
                }

            }
            return canexecute;
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

        #region Scrolling support

        public void ResetZ(object sender)
        {
            ZoomLevel = 1;
        }

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
            set
            {
                _zoomLevelPercent = value;
                NotifyPropertyChanged("ZoomLevelPercent");
            }
        }

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
