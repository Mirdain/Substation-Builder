using System;
namespace Substation_Builder.Model
{
    public class Node: DiagramObject
    {
        private double _x;
        public override double X
        {
            get { return _x; }
            set
            {
                //"Grid Snapping"
                //this actually "rounds" the value so that it will always be a multiple of 25.
                _x = (Math.Round(value / 25.0)) * 25;
                OnPropertyChanged("X");
            }
        }

        private double _y;       
        public override double Y
        {
            get { return _y; }
            set
            {
                //"Grid Snapping"
                //this actually "rounds" the value so that it will always be a multiple of 25.
                _y = (Math.Round(value / 25.0)) * 25;
                OnPropertyChanged("Y");
            }
        }

        private bool _isHighlighted { get; set; }
        public bool IsHighlighted
        {
            get
            {
                return _isHighlighted;
            }
            set
            {
                _isHighlighted = value;
                OnPropertyChanged("IsHighlighted");
            }
        }
        
    }
}