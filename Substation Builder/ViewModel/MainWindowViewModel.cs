using Substation_Builder.Helpers;
using Substation_Builder.Model;
using Substation_Builder.Resources.Monster;
using Substation_Builder.View;

namespace Substation_Builder.ViewModel
{
    class MainWindowViewModel : ObservableObject
    {
        //Declare the Relay commands to call from the Main Window
        public RelayCommand OnelineViewOpenCommand { get; private set; }
        public RelayCommand FaultViewOpenCommand { get; private set; }
        public RelayCommand RelayViewOpenCommand { get; private set; }
        //Declare the view models
        OnelineViewModel onelineViewModel;
        RelayViewModel relayViewModel;
        //Declare a blank project to refrence between windows
        private Substation _project;
        public Substation Project
        {
            get { return _project; }
            set
            {
                _project = value;
                NotifyPropertyChanged("Project");
            }
        }
        //Make a new Project and Relay Commands 
        public MainWindowViewModel()
        {
            Project = new Substation
            {
                SubData = new SubstationData
                {
                    Name = "(Not Set)",
                    Monster = Monster.MonsterName()
                },
                OnelinePref = new OnelinePreferences()
            };
            OnelineViewOpenCommand = new RelayCommand(OnelineViewOpen, Can_Open_OnelineView);
            FaultViewOpenCommand = new RelayCommand(FaultViewOpen, Can_Open_FaultView);
            RelayViewOpenCommand = new RelayCommand(RelayViewOpen, Can_Open_RelayView);
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
        //Relay Module
        public void RelayViewOpen(object sender)
        {
            if (relayViewModel == null)
            {
                relayViewModel = new RelayViewModel (Project);
            }
            else
            {
                RelayView relayView = new RelayView
                {
                    DataContext = relayViewModel
                };
                relayView.Show();
            }
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
        public bool Can_Open_RelayView(object sender)
        {
            bool canexecute = false;
            if (!IsWindowOpen.WindowCheck<RelayView>())
            {
                canexecute = true;
            }
            return canexecute;
        }
    }
}
