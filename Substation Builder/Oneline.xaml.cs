using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.IO;
using System.Data;
using System.Xml;
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
            Project = new DataModel.Substation();
            DataContext = Project;
        }

        private void Tile_Click(object sender, RoutedEventArgs e)
        {
            Project.Name = "updated Name";
        }
    }
}
