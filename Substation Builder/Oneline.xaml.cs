using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using MahApps.Metro.Controls;
using System.Xml.Serialization;

namespace Substation_Builder
{
    /// <summary>
    /// Interaction logic for Oneline.xaml
    /// </summary>
    /// 

    public partial class Oneline : MetroWindow
    {

        DataModel.Substation Project = new DataModel.Substation();

        public Oneline()
        {
            InitializeComponent();
        }

        private void LoadFile(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openoneline = new OpenFileDialog();
            openoneline.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
            if (openoneline.ShowDialog() == true)
            {
                XmlSerializer serializer = new XmlSerializer(typeof(DataModel.Substation));
                StreamReader reader = new StreamReader(openoneline.FileName);
                Project = (DataModel.Substation)serializer.Deserialize(reader);
                reader.Close();

                DataContext = Project;
            }
        }

        private void SaveFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveoneline = new SaveFileDialog();
            saveoneline.Filter = "XML File (*.xml)|*.xml|All Files (*.*)|*.*";
            if (saveoneline.ShowDialog() == true)
            {
                StringWriter sw = new StringWriter();
                XmlSerializer x = new XmlSerializer(Project.GetType());
                x.Serialize(sw, Project);
                File.WriteAllText(saveoneline.FileName, sw.ToString());
            }
        }

        private void NewFile(object sender, RoutedEventArgs e)
        {

            DataModel.Transformer T1 = new DataModel.Transformer
            {
                Bus = DataModel.Bus.Bus_1,
                Name = "T1",
                Size1 = 20,
                Size2 = 25,
                Size3 = 33,
                Z1 = 9.17,
                Z0 = 9.06,
                LowVoltage = 25,
                LowVoltageWndg = DataModel.Winding.Solidly_Grounded_Wye,
                HighVoltage = 69,
                HighVoltageWndg = DataModel.Winding.Delta
            };

            DataModel.Transformer T2 = new DataModel.Transformer
            {
                Bus = DataModel.Bus.Bus_1,
                Name = "T2",
                Size1 = 18,
                Size2 = 23,
                Size3 = 30,
                Z1 = 9.4,
                Z0 = 9.26,
                LowVoltage = 13.2,
                LowVoltageWndg = DataModel.Winding.Solidly_Grounded_Wye,
                HighVoltage = 69,
                HighVoltageWndg = DataModel.Winding.Delta
            };


            Project = new DataModel.Substation
            {
                Name = "Squash Bend",
                Thevenin = new DataModel.Thevenin
                {
                    R1_PU = 12,
                    X1_PU = 3,
                    R2_PU = 6,
                    X2_PU = 9,
                    R0_PU = 23,
                    X0_PU = 55
                }

            };

            Project.Transformers = new List<DataModel.Transformer>
            {
                T1,
                T2
            };

            DataContext = Project;
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            Project.Name = "updated Name";
        }

        private void Thevenin_Page(object sender, RoutedEventArgs e)
        {
            DatabasePanel.Navigate(new Uri("/Resources/XAML Pages/Thevenin.xaml", UriKind.Relative));
        }

        private void Transformer_Page(object sender, RoutedEventArgs e)
        {
            DatabasePanel.Navigate(new Uri("/Resources/XAML Pages/Transformer.xaml", UriKind.Relative));
        }
    }
}
