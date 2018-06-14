using Substation_Builder.Helpers;
using Substation_Builder.Model;
using Substation_Builder.View;
using System.ComponentModel;
using System.Windows;

namespace Substation_Builder.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand DatabaseViewOpenCommand { get; private set; }
        public RelayCommand FaultViewOpenCommand { get; private set; }

        DatabaseViewModel databaseViewModel;

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

        public MainWindowViewModel()
        {
            DatabaseViewOpenCommand = new RelayCommand(DatabaseViewOpen);
            FaultViewOpenCommand = new RelayCommand(FaultViewOpen);

            Project = new Substation
            {
                Name = "No Project Set"
            };
        }

        public void DatabaseViewOpen()
        {
            if (databaseViewModel == null)
            {
                databaseViewModel = new DatabaseViewModel(Project);
            }
            else
            {
       
            }
        }

        public void FaultViewOpen()
        {
          

        }
    }
}
