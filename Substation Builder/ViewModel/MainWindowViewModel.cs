using Substation_Builder.Helpers;
using Substation_Builder.Model;
using Substation_Builder.Resources.Monster;
using Substation_Builder.View;

namespace Substation_Builder.ViewModel
{
    class MainWindowViewModel : ObservableObject
    {
        public RelayCommand FaultViewOpenCommand { get; private set; }
        public RelayCommand OnelineViewOpenCommand { get; private set; }

        OnelineViewModel onelineViewModel;

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

            OnelineViewOpenCommand = new RelayCommand(OnelineViewOpen, Can_Open_OnelineView);
            FaultViewOpenCommand = new RelayCommand(FaultViewOpen, Can_Open_FaultView);
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
