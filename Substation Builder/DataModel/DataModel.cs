﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Substation_Builder.DataModel
{
    public enum Winding
    {
        [Description("Delta")] Delta,
        [Description("Ungrounded Wye")] Ungrounded_Wye,
        [Description("Solidly Grounded Wye")] Solidly_Grounded_Wye,
        [Description("Impedance Grounded Wye")] Impedance_Grounded_Wye
    }

    public enum Substation_Type
    {
        [Description("Open Air")] Open_Air,
        [Description("Recloser")] Recloser,
        [Description("Switchgear")] Switchgear,
    }

    public enum Bus
    {
        [Description("Bus 1")] Bus_1,
        [Description("Bus 2")] Bus_2,
    }

    public enum RelayType
    {
        [Description("SEL 351S-6")] SEL_351S_6,
        [Description("SEL 351S-7")] SEL_351S_7,
        [Description("SEL 387")] SEL_387,
        [Description("SEL 587Z")] SEL_587,
    }

    public enum RelayPosition
    {
        [Description("351-BH1")] BH1,
        [Description("351-BH2")] BH2,
        [Description("351-BL1")] BL1,
        [Description("351-BL2")] BL2,
        [Description("351-BT1")] BT1,
        [Description("351-BT2")] BT2,
        [Description("387-XF1")] XF1,
        [Description("387-XF2")] XF2,
        [Description("587-BS1")] BS1,
        [Description("587-BS2")] BS2,
        [Description("351-BB11")] BB11,
        [Description("351-BB12")] BB12,
        [Description("351-BB13")] BB13,
        [Description("351-BB14")] BB14,
        [Description("351-BB21")] BB21,
        [Description("351-BB22")] BB22,
        [Description("351-BB23")] BB23,
        [Description("351-BB24")] BB24,
    }

    public enum Voltage
    {
        [Description("12.47 kV")] _12_5kV,
        [Description("13.2 kV")] _13_2kV,
        [Description("13.8 kV")] _13_8kV,
        [Description("26.2 kV")] _26_2kV,
        [Description("34.5 kV")] _34_5kV,
        [Description("69 kV")] _69kV,
        [Description("115 kV")] _115kV,
        [Description("161 kV")] _161kV,
    }

    public enum BreakerType
    {
        [Description("SF-6")] SF6,
        [Description("R-MAG")] RMAG,
    }

    public enum BreakerSize
    {
        [Description("2000 A")] A2000,
        [Description("1200 A")] A1200,
    }

    public enum BreakerPosition
    {
        [Description("BH1")] BH1,
        [Description("BH2")] BH2,
        [Description("BL1")] BL1,
        [Description("BL2")] BL2,
        [Description("BT1")] BT1,
        [Description("BT2")] BT2,
        [Description("BB11")] BB11,
        [Description("BB12")] BB12,
        [Description("BB13")] BB13,
        [Description("BB14")] BB14,
        [Description("BB21")] BB21,
        [Description("BB22")] BB22,
        [Description("BB23")] BB23,
        [Description("BB24")] BB24,
    }

    public enum CTRating
    {
        [Description("C200")] C200,
        [Description("C400")] C400,
        [Description("C800")] C800,
    }

    public enum CTTap
    {
        [Description("120")] T120,
        [Description("240")] T240,
        [Description("400")] T400,
    }

    [Serializable]
    public class Substation : INotifyPropertyChanged
    {

        public string Name
        {
            get => name;
            set
            {
                name = value;
                NotifyPropertyChanged("Name");
            }
        }
        
        public Thevenin Thevenin
        {
            get => thevenin;
            set
            {
                thevenin = value;
                NotifyPropertyChanged("Thevenin");
            }
        }


        public Substation_Type Type { get => type; set => type = value; }
        public System System { get => system; set => system = value; }
        public List<Relay> Relays { get => relays; set => relays = value; }
        public List<Breaker> Breakers { get => breakers; set => breakers = value; }
        public List<Transformer> Transformers { get => transformers; set => transformers = value; }

        private string name;
        private Substation_Type type;
        private Thevenin thevenin;
        private System system;
        private List<Relay> relays;
        private List<Breaker> breakers;
        private List<Transformer> transformers;

        // Property Change Logic  
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }   
    }

    public class Thevenin
    {
        public double R0_PU { get => r0_pu; set => r0_pu = value; }
        public double R1_PU { get => r1_pu; set => r1_pu = value; }
        public double R2_PU { get => r2_pu; set => r2_pu = value; }
        public double X0_PU { get => x0_pu; set => x0_pu = value; }
        public double X1_PU { get => x1_pu; set => x1_pu = value; }
        public double X2_PU { get => x2_pu; set => x2_pu = value; }

        private double r0_pu;
        private double r1_pu;
        private double r2_pu;
        private double x0_pu;
        private double x1_pu;
        private double x2_pu;

        public double R0_OHM { get => r0_ohm; set => r0_ohm = value; }
        public double R1_OHM { get => r1_ohm; set => r1_ohm = value; }
        public double R2_OHM { get => r2_ohm; set => r2_ohm = value; }
        public double X0_OHM { get => x0_ohm; set => x0_ohm = value; }
        public double X1_OHM { get => x1_ohm; set => x1_ohm = value; }
        public double X2_OHM { get => x2_ohm; set => x2_ohm = value; }

        private double r0_ohm;
        private double r1_ohm;
        private double r2_ohm;
        private double x0_ohm;
        private double x1_ohm;
        private double x2_ohm;
    }

    public class System
    {
        public double MVA { get => mva; set => mva = value; }
        public double HighVoltage { get => highVoltage; set => highVoltage = value; }
        public double LowVoltage { get => lowVoltage; set => lowVoltage = value; }

        private double mva;
        private double highVoltage;
        private double lowVoltage;
    }



    public class Relay
    {
        public string Name { get => name; set => name = value; }
        public Bus Bus { get => bus; set => bus = value; }
        public RelayPosition Position { get => position; set => position = value; }
        public RelayType Type { get => type; set => type = value; }

        private string name;
        private Bus bus;
        private RelayPosition position;
        private RelayType type;
    }

    public class Breaker
    {
        public CT[] CTs { get => cTs; set => cTs = value; }
        public string Name { get => name; set => name = value; }
        public Bus Bus { get => bus; set => bus = value; }
        public BreakerPosition Position { get => position; set => position = value; }
        public Voltage Voltage { get => voltage; set => voltage = value; }
        public BreakerType Type { get => type; set => type = value; }
        public BreakerSize Size { get => size; set => size = value; }

        private Voltage voltage;
        private CT[] cTs;
        private string name;
        private Bus bus;
        private BreakerPosition position;
        private BreakerType type;
        private BreakerSize size;
    }

    public class Transformer
    {
        public CT[] CTs { get => cTs; set => cTs = value; }
        public string Name { get => name; set => name = value; }
        public Bus Bus { get => bus; set => bus = value; }
        public double Size1 { get => size1; set => size1 = value; }
        public double Size2 { get => size2; set => size2 = value; }
        public double Size3 { get => size3; set => size3 = value; }
        public double Z1 { get => z1; set => z1 = value; }
        public double Z0 { get => z0; set => z0 = value; }
        public double LowVoltage { get => lowVoltage; set => lowVoltage = value; }
        public double HighVoltage { get => highVoltage; set => highVoltage = value; }
        public Winding LowVoltageWndg { get => lowVoltageWndg; set => lowVoltageWndg = value; }
        public Winding HighVoltageWndg { get => highVoltageWndg; set => highVoltageWndg = value; }

        private CT[] cTs;
        private Bus bus;
        private string name;
        private double size1;
        private double size2;
        private double size3;
        private double z1;
        private double z0;
        private double lowVoltage;
        private double highVoltage;
        private Winding lowVoltageWndg;
        private Winding highVoltageWndg;
    }

    public class CT
    {
        public string Name { get => name; set => name = value; }
        public CTRating Rating { get => rating; set => rating = value; }
        public CTTap CTR { get => cTR; set => cTR = value; }
        public CTTap Tap { get => tap; set => tap = value; }
        public bool OnPolarity { get => onPolarity; set => onPolarity = value; }

        private string name;
        private CTRating rating;
        private CTTap cTR;
        private CTTap tap;
        private bool onPolarity;
    }
}

