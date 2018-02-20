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

            Project.Name = "Squash Bend";

            DataModel.TransformerClass T1 = new DataModel.TransformerClass
            {
                Size1 = 23.2
            };

            Project.Transformer = new List<DataModel.TransformerClass>
            {
                T1
            };



        }

    }
}
