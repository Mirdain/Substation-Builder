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
            XmlDocument xmldoc = new XmlDocument();

            OpenFileDialog openoneline = new OpenFileDialog();

            openoneline.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";

            if (openoneline.ShowDialog() == true)
                xmldoc.Load(openoneline.FileName);

            Project.Name = "squash bend";

            DataModel.Transformer t1 = new DataModel.Transformer
            {
                Size1 = 23.2
            };

            DataModel.Transformer t2 = new DataModel.Transformer
            {
                Size1 = 12.2
            };

            Project.Transformers = new List<DataModel.Transformer>
            {
                t1,
                t2
            };

            var xml = "";

            StringWriter sw = new StringWriter();
            XmlSerializer x = new XmlSerializer(Project.GetType());

            XmlWriter writer = XmlWriter.Create(sw);

            x.Serialize(writer, Project);

            xml = sw.ToString();
        }

    }
}
