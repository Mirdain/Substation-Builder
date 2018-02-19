using MahApps.Metro.Controls;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Input;

namespace Substation_Builder.ViewModel
{
    public partial class AboutLink : MetroWindow
    {

        public AboutLink()
        {
            MessageBox.Show("Flyout Class");
        }

    }

    public class Substation
    {
        public SubdataClass Subdata { get => subdata; set => subdata = value; }
        public TheveninClass Thevenin { get => thevenin; set => thevenin = value; }
        public SystemClass System { get => system; set => system = value; }
        public RelayClass [] Relay { get => relay; set => relay = value; }
        public BreakerClass [] Breaker { get => breaker; set => breaker = value; }
        public TransformerClass[] Transformer { get => transformer; set => transformer = value; }

        private SubdataClass subdata;
        private TheveninClass thevenin;
        private SystemClass system;
        private RelayClass [] relay;
        private BreakerClass [] breaker;
        private TransformerClass [] transformer;
    }

    public class SubdataClass
    {
        public string Name { get => name; set => name = value; }
        public IEnumerable<string> Subtype { get => subtype; set => subtype = value; }

        private string name;
        private IEnumerable<string> subtype = new List<string>() { "Open Air", "Switch Gear" };
    }

    public class TheveninClass
    {
        public int R0 { get => r0; set => r0 = value; }
        public int R1 { get => r1; set => r1 = value; }
        public int R2 { get => r2; set => r2 = value; }
        public int X0 { get => x0; set => x0 = value; }
        public int X1 { get => x1; set => x1 = value; }
        public int X2 { get => x2; set => x2 = value; }

        private int r0;
        private int r1;
        private int r2;
        private int x0;
        private int x1;
        private int x2;
    }

    public class SystemClass
    {
        public int MVA { get => mva; set => mva = value; }
        public int HighVoltage { get => highVoltage; set => highVoltage = value; }
        public int LowVoltage { get => lowVoltage; set => lowVoltage = value; }

        private int mva;
        private int highVoltage;
        private int lowVoltage;
    }

    public class RelayClass
    {
        public string Name { get => name; set => name = value; }
        public IEnumerable<string> Bus { get => bus; set => bus = value; }
        public IEnumerable<string> RelayId { get => bus; set => bus = value; }
        public IEnumerable<string> Type { get => relayId; set => relayId = value; }
        public IEnumerable<string> Position { get => position; set => position = value; }

        private string name;
        private IEnumerable<string> bus = new List<string>() { "Bus 1", "Bus 2" };
        private IEnumerable<string> relayId = new List<string>() { "351-BH1", "351-BH2","351-BL1","351-BL2","351-BT1","351-BT2",
            "387-XF1","387-XF1","587-BS1","587-BS2","351-BB11", "351-BB12", "351-BB13", "351-BB14", "351-BB21", "351-BB22", "351-BB23", "351-BB24"};
        private IEnumerable<string> position = new List<string>() { "High Side", "Low Side", "XFMR Diff", "Bus Diff", "Bus Tie" };
        private IEnumerable<string> type = new List<string>() { "SEL 351S", "SEL 387", "SEL 2414" };
    }

    public class BreakerClass
    {
        public CTClass[] CTs { get => cTs; set => cTs = value; }
        public string Name { get => name; set => name = value; }
        public IEnumerable<string> Bus { get => bus; set => bus = value; }
        public IEnumerable<string> Position { get => position; set => position = value; }
        public IEnumerable<int> Rating { get => rating; set => rating = value; }
        public IEnumerable<int> Size { get => size; set => size = value; }
        public IEnumerable<int> Voltage { get => voltage; set => voltage = value; }
        public IEnumerable<string> Type { get => type; set => type = value; }

        private IEnumerable<int> voltage = new List<int>() { 161, 69, 26, 15 };
        private CTClass[] cTs;
        private string name;
        private IEnumerable<string> bus = new List<string>() { "Bus 1", "Bus 2" };
        private IEnumerable<string> position = new List<string>() { "High Side", "Main", "Feeder", "Tie" };
        private IEnumerable<int> rating = new List<int>() { 40, 30 };
        private IEnumerable<int> size = new List<int>() { 2000, 1200 };
        private IEnumerable<string> type = new List<string>() { "SF6", "RMAG" };
    }

    public class TransformerClass
    {
        public CTClass[] CTs { get => cTs; set => cTs = value; }
        public string Name { get => name; set => name = value; }
        public IEnumerable<string> Bus { get => bus; set => bus = value; }
        public int Size1 { get => size1; set => size1 = value; }
        public int Size2 { get => size2; set => size2 = value; }
        public int Size3 { get => size3; set => size3 = value; }
        public int Z1 { get => z1; set => z1 = value; }
        public int Z0 { get => z0; set => z0 = value; }
        public int LowVoltage { get => lowVoltage; set => lowVoltage = value; }
        public int HighVoltage { get => highVoltage; set => highVoltage = value; }
        public IEnumerable<string> LowSideWndg { get => lowSideWndg; set => lowSideWndg = value; }
        public IEnumerable<string> HighSideWndg { get => highSideWndg; set => highSideWndg = value; }

        private CTClass[] cTs;
        private IEnumerable<string> bus = new List<string>() { "Bus 1", "Bus 2" };
        private string name;
        private int size1;
        private int size2;
        private int size3;
        private int z1;
        private int z0;
        private int lowVoltage;
        private int highVoltage;
        private IEnumerable<string> lowSideWndg = new List<string>() { "Delta", "Ungrounded Wye", "Solidly Grounded Wye", "Impedance Grounded Wye" };
        private IEnumerable<string> highSideWndg = new List<string>() { "Delta", "Ungrounded Wye", "Solidly Grounded Wye", "Impedance Grounded Wye" };
    }

    public class CTClass
    {
        public string Name { get => name; set => name = value; }
        public IEnumerable<string> CTclass { get => ctClass; set => ctClass = value; }
        public IEnumerable<int> CTR { get => ctr; set => ctr = value; }
        public IEnumerable<int> Tap { get => tap; set => tap = value; }
        public IEnumerable<string> Polarity { get => polarity; set => polarity = value; }

        private string name;
        private IEnumerable<string> ctClass = new List<string>() { "C200", "C400", "C800" };
        private IEnumerable<int> ctr = new List<int>() { 120, 240, 400 };
        private IEnumerable<int> tap = new List<int>() { 120, 240, 400 };
        private IEnumerable<string> polarity = new List<string>() { "On  Polarity", "Off Polarity" };
    }
    
}
