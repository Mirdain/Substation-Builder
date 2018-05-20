﻿using System.Windows;
using System.IO;
using Microsoft.Win32;
using MahApps.Metro.Controls;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System;

namespace Substation_Builder
{
    /// <summary>
    /// Interaction logic for Oneline.xaml
    /// </summary>
    /// 

    public partial class Oneline : MetroWindow
    {

        DataModel.Substation Project = new DataModel.Substation();
        DataModel.Thevenin ActiveThevenin = new DataModel.Thevenin();

        public Oneline()
        {
            InitializeComponent();

        }

        //read a .xaml file and load into the Datamodel classes
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

        //Serialize the DataModel and save
        private void SaveFile(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveoneline = new SaveFileDialog();
            saveoneline.Filter = "XML File (*.xml)|*.xml|All Files (*.*)|*.*";
            if (saveoneline.ShowDialog() == true)
            {
                StringWriter sw = new StringWriter();
                XmlSerializer x = new XmlSerializer(Project.GetType());
                x.Serialize(sw, Project);
                string outputstring = sw.ToString();
                outputstring = outputstring.Replace("utf-16", "utf-8");
                File.WriteAllText(saveoneline.FileName, outputstring);
            }
        }

        //Example populate - delete at end
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

            DataModel.Thevenin BaseThevenin = new DataModel.Thevenin
            {
                Name = "Base Thevenin",
                R0 = .928,
                X0 = 1.28,
            };

            DataModel.Thevenin SecondThevenin = new DataModel.Thevenin
            {
                Name = "Second Thevenin",
                R0 = .128,
                X0 = 5.38,
            };

            DataModel.Relay FirstRelay = new DataModel.Relay
            {
                Name = "Xfmr Diff",
                Type = DataModel.RelayType.SEL_387,
            };
            DataModel.Relay SecondRelay = new DataModel.Relay
            {
                Name = "High Side",
                Type = DataModel.RelayType.SEL_351S_7,
            };

            Project = new DataModel.Substation
            {
                Name = "Squash Bend",
                Thevenin = new ObservableCollection<DataModel.Thevenin>()
                {
                    BaseThevenin,
                    SecondThevenin,
                },
                Transformers = new ObservableCollection<DataModel.Transformer>
                {
                    T1,
                    T2
                },
                Relays = new ObservableCollection<DataModel.Relay>
                {
                    FirstRelay,
                    SecondRelay,
                }

            };
            DataContext = Project;

        }

        //used to test the treeview databinding - modify to allow edits 
        private void Modify(object sender, RoutedEventArgs e)
        {
            DataModel.Relay ThirdRelay = new DataModel.Relay
            {
                Name = "Low Side",
                Type = DataModel.RelayType.SEL_351S_7,
            };

            Project.Relays.Add(ThirdRelay);

            Project.Name = "Updated Name";

        }

        //used to navigate to forms located in teh XAML Page folder
        private void LoadPage(object sender, RoutedEventArgs e)
        {

            TreeView treepart = (TreeView)sender;

            string teststring = treepart.SelectedItem.ToString();

            if (teststring == "Substation_Builder.DataModel.Thevenin")
            {
                Substation_Builder.Resources.XAML_Pages.Thevenin thevenin = new Resources.XAML_Pages.Thevenin
                {
                    DataContext = treepart.SelectedItem
                };

                pagenavigation.Navigate(thevenin);

            }
            else if (teststring == "Substation_Builder.DataModel.Relay")
            {
                Substation_Builder.Resources.XAML_Pages.Relay relay = new Resources.XAML_Pages.Relay
                {
                    DataContext = treepart.SelectedItem
                };

                pagenavigation.Navigate(relay);
                
            }
            else if (teststring == "Substation_Builder.DataModel.Transformer")
            {
                Substation_Builder.Resources.XAML_Pages.Transformer transformer = new Resources.XAML_Pages.Transformer
                {
                    DataContext = treepart.SelectedItem
                };

                pagenavigation.Navigate(transformer);
            }
            else
            {
                pagenavigation.Navigate("Resources/XAML Pages/Default.xaml", UriKind.Relative);

            }

            
        }
    }
}
