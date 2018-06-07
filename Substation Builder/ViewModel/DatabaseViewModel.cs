using Substation_Builder.Model;
using Substation_Builder.Services;
using Substation_Builder.Helpers;
using System.Windows;
using MahApps.Metro.Controls.Dialogs;

namespace Substation_Builder.ViewModel
{

    public partial class DatabaseViewModel : ViewModelBase
    {
        private Substation project;
        public Substation Project
        {
            get { return project; }
            set
            {
                project = value;
                NotifyPropertyChanged("Project");
            }
        }
        public object TVSelectedItem;
        private SystemIO FileIO = new SystemIO();
        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand LoadCommand { get; private set; }
        public RelayCommand TemplateCommand { get; private set; }
        public RelayCommandParam RemoveItemCommand { get; private set; }
        public RelayCommandParam AddCTCommand { get; private set; }
        public RelayCommandParam AddItemCommand { get; private set; }

        public DatabaseViewModel()
        {
            Project = new Substation();
            LoadCommand = new RelayCommand(LoadFile);
            SaveCommand = new RelayCommand(SaveFile);
            TemplateCommand = new RelayCommand(LoadTemplate);
            AddItemCommand = new RelayCommandParam(AddItem);
            RemoveItemCommand = new RelayCommandParam(RemoveItem);
            AddCTCommand = new RelayCommandParam(AddCT);
            AddItemCommand = new RelayCommandParam(AddItem);
        }

        //read a .xaml file and load into the Datamodel classes
        private void LoadFile()
        {
            Project = FileIO.FileOpen(Project);
        }

        //Serialize the DataModel and save
        public void SaveFile()
        {
            FileIO.FileSave(Project);
        }

        //Load Template
        public void LoadTemplate()
        {
            Project = FileIO.LoadTemplate();
        }

        public void AddItem(object sender)
        {
            if (sender.ToString() == "Thevenin")
            {
                Thevenin thevenin = new Thevenin { Name = "New Thevenin " + (project.Thevenins.Count + 1).ToString() };
                Project.Thevenins.Add(thevenin);
            }

            if (sender.ToString() == "Project")
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Save Curent Project?", "Create New Project", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);

                if ( messageBoxResult == MessageBoxResult.No)
                {
                    Project = new Substation
                    {
                        Name = "New Project"
                    };
                }
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    SaveFile();

                    Project = new Substation
                    {
                        Name = "New Project"
                    };
                }
            }

            if (sender.ToString() == "Breaker")
            {
                Breaker breaker = new Breaker { Name = "New Breaker " + (project.Breakers.Count + 1).ToString() };
                Project.Breakers.Add(breaker);
            }

            if (sender.ToString() == "Relay")
            {
                Relay relay = new Relay { Name = "New Relay " + (project.Relays.Count + 1).ToString() };
                Project.Relays.Add(relay);
            }

            if (sender.ToString() == "Transformer")
            {
                Transformer transformer = new Transformer { Name = "New Transformer " + (project.Transformers.Count + 1).ToString() };
                Project.Transformers.Add(transformer);
            }
        }

        //Add a CT
        public void AddCT(object sender)
        {
            if (sender.GetType() == typeof(Breaker))
            {
                int index = project.Breakers.IndexOf(sender as Breaker);
                CT ct = new CT { Name = "CT" + (project.Breakers[index].CTs.Count + 1).ToString() };
                project.Breakers[index].CTs.Add(ct);
            }
            else if (sender.GetType() == typeof(Transformer))
            {
                int index = project.Transformers.IndexOf(sender as Transformer);
                CT ct = new CT { Name = "CT" + (project.Transformers[index].CTs.Count + 1).ToString() };
                project.Transformers[index].CTs.Add(ct);
            }
        }

        //Select Item to remove
        public void RemoveItem(object sender)
        {
            if (sender.GetType() == typeof(Thevenin))
            {
                project.Thevenins.Remove(sender as Thevenin);
            }
            else if (sender.GetType() == typeof(Breaker))
            {
                project.Breakers.Remove(sender as Breaker);
            }
            else if (sender.GetType() == typeof(Relay))
            {
                project.Relays.Remove(sender as Relay);
            }
            else if (sender.GetType() == typeof(Transformer))
            {
                project.Transformers.Remove(sender as Transformer);
            }
            else if (sender.GetType() == typeof(CT))
            {
                for (int i = 0; i < project.Transformers.Count; i++)
                {
                    int index = project.Transformers[i].CTs.IndexOf((CT)sender);
                    if (index >= 0)
                        project.Transformers[i].CTs.RemoveAt(index);
                }

                for (int i = 0; i < project.Breakers.Count; i++)
                {
                    int index = project.Breakers[i].CTs.IndexOf((CT)sender);
                    if (index >= 0)
                        project.Breakers[i].CTs.RemoveAt(index);
                }
            }
        }

    }

}
