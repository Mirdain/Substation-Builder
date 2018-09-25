using Substation_Builder.Helpers;
using System;

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

        private string _visible { get; set; }
        public string Visible
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
                //"Grid Snapping"
                //this actually "rounds" the value so that it will always be a multiple of 25.
                _x = (Math.Round(value / 25.0)) * 25;
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
                //"Grid Snapping"
                //this actually "rounds" the value so that it will always be a multiple of 25.
                _y = (Math.Round(value / 25.0)) * 25;
                NotifyPropertyChanged("Y");
            }
        }
    }
}
