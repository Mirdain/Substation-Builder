using Substation_Builder.Model;
using Substation_Builder.Services;
using Substation_Builder.Helpers;
using System;
using System.Windows;
using System.Windows.Input;

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
        public RelayCommand NewCommdand { get; private set; }
        public RelayCommand TemplateCommand { get; private set; }
        public RelayCommand AddTheveninCommand { get; private set; }

        public RelayCommandParam RemoveItemCommand { get; set; }
 
        public DatabaseViewModel()
        {
            Project = new Substation();
            LoadCommand = new RelayCommand(LoadFile);
            SaveCommand = new RelayCommand(SaveFile);
            NewCommdand = new RelayCommand(New);
            TemplateCommand = new RelayCommand(LoadTemplate);
            AddTheveninCommand = new RelayCommand(AddThevenin);
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

        //Intialize the project clean
        public void New()
        {
            Project = new Substation();
        }

        //Load Template
        public void LoadTemplate()
        {
            Project = FileIO.LoadTemplate();
        }

        //Add a Thevenin
        public void AddThevenin()
        {
            Thevenin thevenin = new Thevenin { Name = "New " + project.Thevenins.Count.ToString() };
            Project.Thevenins.Add(thevenin);
        }

        //Remove an Item
        public void RemoveItem(object sender)
        {
            MessageBox.Show("Fired with " + sender.ToString());
        }

    }

}
