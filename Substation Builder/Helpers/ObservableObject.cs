using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Substation_Builder.Helpers
{
    public abstract class ObservableObject : INotifyPropertyChanged
    {
        // Property Change Logic  
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
