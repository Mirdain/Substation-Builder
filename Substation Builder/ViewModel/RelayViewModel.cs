using Substation_Builder.Helpers;
using Substation_Builder.Model;
using Substation_Builder.View;
using Substation_Builder.Services;
using System.Windows;
using System.Windows.Controls;
using Substation_Builder.Resources.Monster;
using System;

namespace Substation_Builder.ViewModel
{
    class RelayViewModel : ObservableObject
    {
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

        public RelayView SettingsView = new RelayView();

        public RelayViewModel(Substation refproject)
        {
            Project = refproject;

            SettingsView.DataContext = this;
            SettingsView.Show();
        }

    }
}
