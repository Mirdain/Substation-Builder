using System.Windows;
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
        private SystemIO FileIO = new SystemIO();

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand LoadCommand { get; private set; }
        public RelayCommand NewCommand { get; private set; }
        public RelayCommand TemplateCommand { get; private set; }

        public DatabaseViewModel()
        {
            Project = new Substation();
            LoadCommand = new RelayCommand(LoadFile, CanOpen);
            SaveCommand = new RelayCommand(SaveFile, CanSave);
            NewCommand = new RelayCommand(NewFile, CanOpen);
            TemplateCommand = new RelayCommand(LoadTemplate, CanOpen);
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

        //Serialize the DataModel and save
        public void NewFile()
        {
            Project = new Substation();
        }

        //Load Template
        public void LoadTemplate()
        {
            Project = FileIO.LoadTemplate();
        }

        public bool CanOpen()
        {
            return true;
        }

        public bool CanSave()
        {
            return true;
        }

    }


}
