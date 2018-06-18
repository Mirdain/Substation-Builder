using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Substation_Builder.Model
{
    [Serializable]
    public class Substation : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                NotifyPropertyChanged();
            }
        }

        private SubType subType;
        public SubType SubType
        {
            get { return subType; }
            set
            {
                subType = value;
                NotifyPropertyChanged();
            }
        }

        private BusType busType;
        public BusType BusType
        {
            get { return busType; }
            set
            {
                busType = value;
                NotifyPropertyChanged();
            }
        }

        private PTR highPT;
        public PTR HighPT
        {
            get { return highPT; }
            set
            {
                highPT = value;
                NotifyPropertyChanged();
            }
        }

        private PTR lowPT;
        public PTR LowPT
        {
            get { return lowPT; }
            set
            {
                lowPT = value;
                NotifyPropertyChanged();
            }
        }

        private ConnectionType highPTCon;
        public ConnectionType HighPTCon
        {
            get { return highPTCon; }
            set
            {
                highPTCon = value;
                NotifyPropertyChanged();
            }
        }

        private ConnectionType lowPTCon;
        public ConnectionType LowPTCon
        {
            get { return lowPTCon; }
            set
            {
                lowPTCon = value;
                NotifyPropertyChanged();
            }
        }

        private double mVA;
        public double MVA
        {
            get { return mVA; }
            set
            {
                mVA = value;
                NotifyPropertyChanged();
            }
        }

        private Voltage highVoltage;
        public Voltage HighVoltage
        {
            get { return highVoltage; }
            set
            {
                highVoltage = value;
                NotifyPropertyChanged();
            }
        }

        private Voltage lowVoltage;
        public Voltage LowVoltage
        {
            get { return lowVoltage; }
            set
            {
                lowVoltage = value;
                NotifyPropertyChanged();
            }
        }
        
        public ObservableCollection<Thevenin> Thevenins { get; set; } = new ObservableCollection<Thevenin>();
        public ObservableCollection<Relay> Relays { get; set; } = new ObservableCollection<Relay>();
        public ObservableCollection<Breaker> Breakers { get; set; } = new ObservableCollection<Breaker>();
        public ObservableCollection<Transformer> Transformers { get; set; } = new ObservableCollection<Transformer>();

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Replace(Substation Append)
        {
            Name = Append.Name;
            SubType = Append.SubType;
            BusType = Append.BusType;
            HighPT = Append.HighPT;
            LowPT = Append.LowPT;
            HighPTCon = Append.HighPTCon;
            LowPTCon = Append.LowPTCon;
            MVA = Append.MVA;
            HighVoltage = Append.HighVoltage;
            LowVoltage = Append.LowVoltage;

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
