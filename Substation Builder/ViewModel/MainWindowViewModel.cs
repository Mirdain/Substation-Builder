using Substation_Builder.Helpers;
using Substation_Builder.Model;
using Substation_Builder.View;
using Substation_Builder.Resources.Monster;
using System.Windows;

namespace Substation_Builder.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public RelayCommand DatabaseViewOpenCommand { get; private set; }
        public RelayCommand FaultViewOpenCommand { get; private set; }
        public RelayCommand OnelineViewOpenCommand { get; private set; }

        DatabaseViewModel databaseViewModel;
        OnelineViewModel onelineViewModel;

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
            DatabaseViewOpenCommand = new RelayCommand(DatabaseViewOpen, Can_Open_DatabaseView);
            OnelineViewOpenCommand = new RelayCommand(OnelineViewOpen, Can_Open_OnelineView);
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


        public bool Can_Open_DatabaseView(object sender)
        {
            bool canexecute = false;

            if(!IsWindowOpen.WindowCheck<DatabaseView>())
            {
                canexecute = true;
            }
            return canexecute;
        }

        public void OnelineViewOpen(object sender)
        {
            if (onelineViewModel == null)
            {
                onelineViewModel = new  OnelineViewModel(Project);
            }
            else
            {
                OnelineView onelineView = new OnelineView
                {

                    DataContext = onelineViewModel
                };
                onelineView.Show();
            }
        }

        public bool Can_Open_OnelineView(object sender)
        {
            bool canexecute = false;

            if (!IsWindowOpen.WindowCheck<OnelineView>())
            {
                canexecute = true;
            }
            return canexecute;
        }


        public void FaultViewOpen(object sender)
        {
          

        }
    }
}
