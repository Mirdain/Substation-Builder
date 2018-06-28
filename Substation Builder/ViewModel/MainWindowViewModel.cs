using Substation_Builder.Helpers;
using Substation_Builder.Model;
using Substation_Builder.View;
using Substation_Builder.Resources.Monster;

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

            Monster monster = new Monster();

            Project = new Substation
            {
                Name = "No Project Set",
                Engineer = monster.MonsterName()
            };
        }

        public void DatabaseViewOpen(object sender)
        {
            if (databaseViewModel == null)
            {
                databaseViewModel = new DatabaseViewModel(Project);
            }
            else
            {
                DatabaseView databaseView = new DatabaseView
                {
                    DataContext = databaseViewModel
                };
                databaseView.Show();
            }
        }

        public void FaultViewOpen(object sender)
        {
          

        }
    }
}
