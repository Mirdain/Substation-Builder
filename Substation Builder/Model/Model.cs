﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Substation_Builder.Helpers;

namespace Substation_Builder.Model
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Bus
    {
        [Description("Bus 1")] Bus_1,
        [Description("Bus 2")] Bus_2,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum RelayType
    {
        [Description("SEL 351S-6")] SEL_351S_6,
        [Description("SEL 351S-7")] SEL_351S_7,
        [Description("SEL 387")] SEL_387,
        [Description("SEL 587Z")] SEL_587,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
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

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
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

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum BreakerType
    {
        [Description("SF-6")] SF6,
        [Description("R-MAG")] RMAG,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum BreakerSize
    {
        [Description("2000 A")] A2000,
        [Description("1200 A")] A1200,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
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

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum CTRating
    {
        [Description("C200")] C200,
        [Description("C400")] C400,
        [Description("C800")] C800,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum CTTaps
    {
        [Description("2000:5 - (400:1)")] T400,
        [Description("1500:5 - (300:1)")] T300,
        [Description("1200:5 - (240:1")] T240,
        [Description("900:5 - (180:1")] T180,
        [Description("800:5 - (160:1")] T160,
        [Description("600:5 - (120:1")] T120,

    }

    [Serializable]
    public class Substation
    {
        public string Name { get; set; }
        public string SubType { get; set; }
        public string BusType { get; set; }
        public double MVA { get; set; }
        public double HighVoltage { get; set; }
        public double LowVoltage { get; set; }
        public ObservableCollection<Thevenin> Thevenins { get; set; } = new ObservableCollection<Thevenin>();
        public ObservableCollection<Relay> Relays { get; set; } = new ObservableCollection<Relay>();
        public ObservableCollection<Breaker> Breakers { get; set; } = new ObservableCollection<Breaker>();
        public ObservableCollection<Transformer> Transformers { get; set; } = new ObservableCollection<Transformer>();
    }

    public class Thevenin
    {
        public string Name { get; set; }
        public double R0_Z { get; set; }
        public double R1_Z { get; set; }
        public double R2_Z { get; set; }
        public double X0_Z { get; set; }
        public double X1_Z { get; set; }
        public double X2_Z { get; set; }

        public double R0_Ohms { get; set; }
        public double R1_Ohms { get; set; }
        public double R2_Ohms { get; set; }
        public double X0_Ohms { get; set; }
        public double X1_Ohms { get; set; }
        public double X2_Ohms { get; set; }
    }

    public class Relay
    {
        public string Name { get; set; }
        public Bus Bus { get; set; }
        public RelayPosition Position { get; set; }
        public RelayType Type { get; set; }
    }

    public class Breaker
    {
        public ObservableCollection<CT> CTs { get; set; } = new ObservableCollection<CT>();
        public string Name { get; set; }
        public Bus Bus { get; set; }
        public BreakerPosition Breaker_Position { get; set; }
        public Voltage Voltage { get; set; }
        public BreakerType Breaker_Type { get; set; }
        public BreakerSize Breaker_Size { get; set; }
    }

    public class Transformer
    {
        public ObservableCollection<CT> CTs { get; set; } = new ObservableCollection<CT>();
        public string Name { get; set; }
        public double Size1 { get; set; }
        public double Size2 { get; set; }
        public double Z1 { get; set; }
        public double Z0 { get; set; }
        public double Losses { get; set; }
        public double R1 { get; set; }
        public double X1 { get; set; }
        public double R0 { get; set; }
        public double X0 { get; set; }
        public double LowVoltage { get; set; }
        public double HighVoltage { get; set; }
        public string LowVoltageWndg { get; set; }
        public string HighVoltageWndg { get; set; }
    }

    public class CT
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CTRating Rating { get; set; }
        public CTTaps CTR { get; set; }
        public CTTaps Tap { get; set; }
        public bool OnPolarity { get; set; }
        public Relay Relay { get; set; }
    }

}

