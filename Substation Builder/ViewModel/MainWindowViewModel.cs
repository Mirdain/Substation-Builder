using Substation_Builder.Helpers;
using Substation_Builder.Model;
using Substation_Builder.Resources.Monster;
using Substation_Builder.View;

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
            Monster monster = new Monster();

            Project = new Substation
            {
                SubData = new SubstationData
                {
                    Name = "(Not Set)",
                    Monster = monster.MonsterName()
                }
            };

            DatabaseViewOpenCommand = new RelayCommand(DatabaseViewOpen, Can_Open_DatabaseView);
            OnelineViewOpenCommand = new RelayCommand(OnelineViewOpen, Can_Open_OnelineView);
            FaultViewOpenCommand = new RelayCommand(FaultViewOpen, Can_Open_DatabaseView);
        }

        //Database Module
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

        //One Line Module
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

        //Fault Module
        public void FaultViewOpen(object sender)
        {
          

        }


        //Checks if a windows are open
        public bool Can_Open_DatabaseView(object sender)
        {
            bool canexecute = false;

            if (!IsWindowOpen.WindowCheck<DatabaseView>())
            {
                canexecute = true;
            }
            return canexecute;
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

        public bool Can_Open_FaultView(object sender)
        {
            bool canexecute = false;

            if (!IsWindowOpen.WindowCheck<FaultView>())
            {
                canexecute = true;
            }
            return canexecute;
        }

    }
}
