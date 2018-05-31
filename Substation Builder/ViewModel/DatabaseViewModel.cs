using Substation_Builder.Model;
using Substation_Builder.Services;
using Substation_Builder.Helpers;

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
        public RelayCommand AddTheveninCommand { get; private set; }
        public RelayCommand AddBreakerCommand { get; private set; }
        public RelayCommand AddRelayCommand { get; private set; }
        public RelayCommand AddTransformerCommand { get; private set; }
        public RelayCommand NewProjectCommand { get; private set; }

        public RelayCommandParam RemoveItemCommand { get; private set; }

        public DatabaseViewModel()
        {
            Project = new Substation();
            LoadCommand = new RelayCommand(LoadFile);
            SaveCommand = new RelayCommand(SaveFile);
            TemplateCommand = new RelayCommand(LoadTemplate);

            AddTheveninCommand = new RelayCommand(AddThevenin);
            AddBreakerCommand = new RelayCommand(AddBreaker);
            AddRelayCommand = new RelayCommand(AddRelay);
            AddTransformerCommand = new RelayCommand(AddTransformer);
            NewProjectCommand = new RelayCommand(NewProject);

            RemoveItemCommand = new RelayCommandParam(RemoveItem);

        }

        //read a .xaml file and load into the Datamodel classes
        private void LoadFile()
        {
            Project = FileIO.FileOpen();
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

        //New Project
        public void NewProject()
        {
            Project = new Substation
            {
                Name = "New Project"
            };
        }

        //Add a Thevenin
        public void AddThevenin()
        {
            Thevenin thevenin = new Thevenin { Name = "New Thevenin " + (project.Thevenins.Count + 1).ToString() };
            Project.Thevenins.Add(thevenin);
        }

        //Add a Breaker
        public void AddBreaker()
        {

            Breaker breaker = new Breaker { Name = "New Breaker " + (project.Thevenins.Count + 1).ToString() };
            Project.Breakers.Add(breaker);
        }

        //Add a Relay
        public void AddRelay()
        {
            Relay relay = new Relay { Name = "New Relay " + (project.Thevenins.Count + 1).ToString() };
            Project.Relays.Add(relay);
        }


        //Add a Transformer
        public void AddTransformer()
        {
            Transformer transformer = new Transformer { Name = "New Transformer " + (project.Thevenins.Count + 1).ToString() };
            Project.Transformers.Add(transformer);
        }

        //Select Item to remove
        public void RemoveItem(object sender)
        {
            if(sender.GetType() == typeof(Thevenin))
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
        }

    }

}
