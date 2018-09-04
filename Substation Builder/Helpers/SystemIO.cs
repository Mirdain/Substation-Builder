using Substation_Builder.Model;
using System.IO;
using Microsoft.Win32;
using System.Xml.Serialization;
using Substation_Builder.Resources.Monster;
using System.Collections.ObjectModel;
using System;

namespace Substation_Builder.Services
{
    public partial class SystemIO
    {
        //Load a  Project File
        public void FileOpen(Substation RefProject)
        {
            Substation Project = new Substation();

            OpenFileDialog openfile = new OpenFileDialog
            {
                Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*"
            };
            if (openfile.ShowDialog() == true)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(Substation));
                StreamReader reader = new StreamReader(openfile.FileName);
                Project = (Substation)serializer.Deserialize(reader);
                reader.Close();
                RefProject.Replace(Project);
            }

        }

        //Save a Project File
        public void FileSave(Substation Project)
        {
            SaveFileDialog savefile = new SaveFileDialog
            {
                Filter = "XML File (*.xml)|*.xml|All Files (*.*)|*.*"
            };
            if (savefile.ShowDialog() == true)
            {
                StringWriter sw = new StringWriter();
                XmlSerializer x = new XmlSerializer(Project.GetType());
                x.Serialize(sw, Project);
                string outputstring = sw.ToString();
                outputstring = outputstring.Replace("utf-16", "utf-8");
                File.WriteAllText(savefile.FileName, outputstring);
            }
        }

        //Loads a Template File
        public void LoadTemplate(Substation RefProject)
        {
            Monster monster = new Monster();

            Substation Project = new Substation();
            SubstationData Data = new SubstationData
            {
                Name = "Squash Bend Template",
                Monster = monster.MonsterName()
            };

            Project.SubData = Data;

            CT XFMR1CT = new CT
            {
                CTR = CTTaps.T400,
                Rating = CTRating.C400,
                Name = "CT1",
                Polarity = Polarity.OnPolarity,
                Tap = CTTaps.T180
            };
            CT XFMR2CT = new CT
            {
                CTR = CTTaps.T400,
                Rating = CTRating.C400,
                Name = "CT2",
                Polarity = Polarity.OnPolarity,
                Tap = CTTaps.T400
            };
            CT XFMR3CT = new CT
            {
                CTR = CTTaps.T400,
                Rating = CTRating.C400,
                Name = "CT3",
                Polarity = Polarity.OnPolarity,
                Tap = CTTaps.T180
            };

            CT BRK1CT = new CT
            {
                CTR = CTTaps.T400,
                Rating = CTRating.C400,
                Name = "CT1B",
                Polarity = Polarity.OnPolarity,
                Tap = CTTaps.T120
            };
            CT BRK2CT = new CT
            {
                CTR = CTTaps.T400,
                Rating = CTRating.C400,
                Name = "CT2B",
                Polarity = Polarity.OnPolarity,
                Tap = CTTaps.T160
            };
            CT BRK3CT = new CT
            {
                CTR = CTTaps.T240,
                Rating = CTRating.C400,
                Name = "CT3B",
                Polarity = Polarity.OnPolarity,
                Tap = CTTaps.T120
            };

            Transformer T1 = new Transformer
            {
                Name = "T1",
                Size1 = 20,
                Size2 = 33,
                Z1 = 9.17,
                Z0 = 9.06,
                LowVoltage = 25,
                LowVoltageWndg = XFMRCon.GrndWye,
                HighVoltage = 69,
                HighVoltageWndg = XFMRCon.Delta,
                Losses = 60
            };

            T1.CTs.Add(XFMR1CT);
            T1.CTs.Add(XFMR2CT);
            T1.CTs.Add(XFMR3CT);

            Transformer T2 = new Transformer
            {
                Name = "T2",
                Size1 = 18,
                Size2 = 30,
                Z1 = 9.4,
                Z0 = 9.26,
                LowVoltage = 13.2,
                LowVoltageWndg = XFMRCon.GrndWye,
                HighVoltage = 69,
                HighVoltageWndg = XFMRCon.Delta,
            };

            Thevenin BaseThevenin = new Thevenin
            {
                Name = "Base Thevenin",
                R0_Z = .928,
                X0_Z = 1.28,
            };

            Thevenin SecondThevenin = new Thevenin
            {
                Name = "Second Thevenin",
                R0_Z = .128,
                X0_Z = 5.38,
            };

            Breaker HighSide = new Breaker
            {
                Name = "High Side Name",
                Bus = Bus.Bus_1,
                Breaker_Position = BreakerPosition.BH1,
                Voltage = Voltage._69kV,
                Breaker_Type = BreakerType.SF6,
                Breaker_Size = BreakerSize.A2000,
            };

            HighSide.CTs.Add(BRK3CT);
            HighSide.CTs.Add(BRK1CT);
            HighSide.CTs.Add(BRK1CT);

            CT CT1 = new CT
            {
                Name = "BH1 CT1",
                Rating = CTRating.C400,
                CTR = CTTaps.T240,
                Tap = CTTaps.T120,
                Polarity = Polarity.OnPolarity,
            };

            Relay FirstRelay = new Relay
            {
                Name = "Xfmr Diff",
                Type = RelayType.SEL_387,
            };

            Relay SecondRelay = new Relay
            {
                Name = "High Side",
                Type = RelayType.SEL_351S_7,
            };

            Project.Thevenins.Add(BaseThevenin);
            Project.Thevenins.Add(SecondThevenin);
            Project.Transformers.Add(T1);
            Project.Transformers.Add(T2);
            Project.Relays.Add(FirstRelay);
            Project.Relays.Add(SecondRelay);
            Project.Breakers.Add(HighSide);
            RefProject.Replace(Project);

        }

    }
}
