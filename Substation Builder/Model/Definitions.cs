using Substation_Builder.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Substation_Builder.Model
{
    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Bus
    {
        [Description("Bus 1")] Bus_1,
        [Description("Bus 2")] Bus_2,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum SubType
    {
        [Description("Open Air")] OpenAir,
        [Description("Switchgear")] Switchgear,
        [Description("Recloser")] Recloser,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum XFMRCon
    {
        [Description("Delta")] Delta,
        [Description("Grounded Wye")] GrndWye,
        [Description("Ungrounded Wye")] UngrndWye,
        [Description("Impedance Grnd")] ImpGrnd,
        [Description("Zig-Zag")] ZigZag,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum BusType
    {
        [Description("Single Bus")] SingleBus,
        [Description("Double Bus")] DoubleBus,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum Polarity
    {
        [Description("On Polarity")] OnPolarity,
        [Description("Off Polarity")] OffPolarity,
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
    public enum RelayFunction
    {
        [Description("High Side Backup")] HBU_OC,
        [Description("Low Side Backup")] LBU_OC,
        [Description("Transformer Diff")] XFMR_Diff,
        [Description("Bus Diff")] Bus_Diff,
        [Description("Feeder Protection")] Feeder,
        [Description("Bus Tie Protection")] BT_OC,
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
        [Description("161 kV")] _161kV,
        [Description("115 kV")] _115kV,
        [Description("69 kV")] _69kV,
        [Description("34.5 kV")] _34_5kV,
        [Description("26.2 kV")] _26_2kV,
        [Description("13.8 kV")] _13_8kV,
        [Description("13.2 kV")] _13_2kV,
        [Description("12.47 kV")] _12_5kV,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum BreakerType
    {
        [Description("SF-6")] SF6,
        [Description("R-MAG")] RMAG,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum BrkManufacture
    {
        [Description("ABB")] ABB,
        [Description("Eaton")] EATON,
        [Description("Mitsubishi")] MITSU,
        [Description("Areva")] AREVA,
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
        [Description("1200:5 - (240:1)")] T240,
        [Description("900:5 - (180:1)")] T180,
        [Description("800:5 - (160:1)")] T160,
        [Description("600:5 - (120:1)")] T120,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum PTR
    {
        [Description("600:1")] T600,
        [Description("300:1")] T300,
        [Description("120:1")] T120,
        [Description("70:1")] T70,
        [Description("60:1")] T60,
        [Description("None")] TNone,
    }

    [TypeConverter(typeof(EnumDescriptionTypeConverter))]
    public enum ConnectionType
    {
        [Description("Delta")] Delta,
        [Description("Wye")] Wye,
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
        public string Description { get; set; }
        public RelayType Type { get; set; }
        public RelayFunction Function { get; set; }
        public Bus Bus { get; set; }
        public RelayPosition Position { get; set; }
        public CT ConnectedCT { get; set; }
    }

    public class Breaker : DiagramObject
    {
        public ObservableCollection<CT> CTs { get; set; } = new ObservableCollection<CT>();
        public string Name { get; set; }
        public string Description { get; set; }
        public BreakerSize Breaker_Size { get; set; }
        public Bus Bus { get; set; }
        public BreakerPosition Breaker_Position { get; set; }
        public Voltage Voltage { get; set; }
        public BreakerType Breaker_Type { get; set; }
        public BrkManufacture BrkMan { get; set; }
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
        public XFMRCon LowVoltageWndg { get; set; }
        public XFMRCon HighVoltageWndg { get; set; }
    }

    public class CT
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public CTRating Rating { get; set; }
        public CTTaps CTR { get; set; }
        public CTTaps Tap { get; set; }
        public Polarity Polarity { get; set; }
        public Relay Relay { get; set; }
    }

    public class SubstationData
    {
        public string Name { get; set; }
        public string Engineer { get; set; }
        public string[] Monster { get; set; }
        public SubType SubType { get; set; }
        public BusType BusType { get; set; }
        public PTR HighPTR { get; set; }
        public PTR LowPTR { get; set; }
        public ConnectionType HighPTCon { get; set; }
        public ConnectionType LowPTCon { get; set; }
        public double MVA { get; set; }
        public Voltage HighVoltage { get; set; }
        public Voltage LowVoltage { get; set; }
    }

}

