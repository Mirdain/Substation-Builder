using Substation_Builder.Helpers;
using System;
using System.Collections.ObjectModel;

namespace Substation_Builder.Model
{
    [Serializable]
    public class Substation : ObservableObject
    {
        private SubstationData _subData;
        public SubstationData SubData
        {
            get { return _subData; }
            set
            {
                _subData = value;
                NotifyPropertyChanged("SubData");
            }
        }
        private OnelinePreferences _onelinePref;
        public OnelinePreferences OnelinePref
        {
            get { return _onelinePref; }
            set
            {
                _onelinePref = value;
                NotifyPropertyChanged("OnelinePref");
            }
        }
        public ObservableCollection<int> IDs { get; set; } = new ObservableCollection<int>();
        public ObservableCollection<Thevenin> Thevenins { get; set; } = new ObservableCollection<Thevenin>();
        public ObservableCollection<Relay> Relays { get; set; } = new ObservableCollection<Relay>();
        public ObservableCollection<Breaker> Breakers { get; set; } = new ObservableCollection<Breaker>();
        public ObservableCollection<Transformer> Transformers { get; set; } = new ObservableCollection<Transformer>();
        public void Replace(Substation Append)
        {
            SubData = Append.SubData;
            OnelinePref = Append.OnelinePref;
            IDs.Clear();
            for (int i = 0; i < Append.IDs.Count; i++)
            {
                IDs.Add(Append.IDs[i]);
            }
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
            IDs.Clear();
            Thevenins.Clear();
            Breakers.Clear();
            Transformers.Clear();
            Relays.Clear();
        }
    }
}
