using System.ComponentModel;

namespace Substation_Builder.Helpers
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        // Property Change Logic  
        public event PropertyChangedEventHandler PropertyChanged;
        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
