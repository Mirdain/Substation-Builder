using Substation_Builder.Helpers;

namespace Substation_Builder.Model
{
    public class DiagramObject : ObservableObject
    {
        private bool _isSelected { get; set; }
        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                NotifyPropertyChanged("IsSelected");
            }
        }

        private bool _visible { get; set; }
        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
                NotifyPropertyChanged("Visible");
            }
        }

        private double _x { get; set; }
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                NotifyPropertyChanged("X");
            }
        }

        private double _y { get; set; }
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                NotifyPropertyChanged("Y");
            }
        }
    }
}
