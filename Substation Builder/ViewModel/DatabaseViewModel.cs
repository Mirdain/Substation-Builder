using Substation_Builder.Helpers;
using Substation_Builder.Model;
using Substation_Builder.Services;
using Substation_Builder.View;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Windows;
using System.Reflection;

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
                NotifyPropertyChanged();
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

        public DatabaseViewModel(Substation refproject)
        {
            Project = refproject;
            LoadCommand = new RelayCommand(LoadFile);
            SaveCommand = new RelayCommand(SaveFile);
            TemplateCommand = new RelayCommand(LoadTemplate);
            AddItemCommand = new RelayCommandParam(AddItem);
            RemoveItemCommand = new RelayCommandParam(RemoveItem);
            AddCTCommand = new RelayCommandParam(AddCT);
            AddItemCommand = new RelayCommandParam(AddItem);

            DatabaseView databaseView = new DatabaseView
            {
                DataContext = this
            };

            databaseView.Show();

        }

        //read a .xaml file and load into the Datamodel classes
        private void LoadFile()
        {
            FileIO.FileOpen(Project);
        }       

        //Serialize the DataModel and save
        public void SaveFile()
        {
            FileIO.FileSave(Project);
        }

        //Load Template
        public void LoadTemplate()
        {
            FileIO.LoadTemplate(Project);
        }

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

        public static Substation DeepClone<Substation>(Substation obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (Substation)formatter.Deserialize(ms);
            }
        }

    }




}
