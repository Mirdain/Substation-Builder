using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Substation_Builder.Model
{
    [Serializable]
    public class Substation : INotifyPropertyChanged
    {

        private SubstationData subData;
        public SubstationData SubData
        {
            get { return subData; }
            set
            {
                subData = value;
                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Thevenin> Thevenins { get; set; } = new ObservableCollection<Thevenin>();
        public ObservableCollection<Relay> Relays { get; set; } = new ObservableCollection<Relay>();
        public ObservableCollection<Breaker> Breakers { get; set; } = new ObservableCollection<Breaker>();
        public ObservableCollection<Transformer> Transformers { get; set; } = new ObservableCollection<Transformer>();


        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Replace(Substation Append)
        {
            SubData = Append.SubData;

            Thevenins.Clear();
            for (int i=0; i<Append.Thevenins.Count;i++)
            {
                Thevenins.Add(Append.Thevenins[i]);
            }

            Relays.Clear();
            for (int i = 0; i < Append.Relays.Count; i++)
            {
                Relays.Add(Append.Relays[i]);
            }

            Breakers.Clear();
            for (int i = 0; i < Append.Breakers.Count; i++)
            {
                Breakers.Add(Append.Breakers[i]);
            }

            Transformers.Clear();
            for (int i = 0; i < Append.Transformers.Count; i++)
            {
                Transformers.Add(Append.Transformers[i]);
            }
        }

        public void Clear()
        {
            Thevenins.Clear();
            Breakers.Clear();
            Transformers.Clear();
            Relays.Clear();
        }
    }
}
