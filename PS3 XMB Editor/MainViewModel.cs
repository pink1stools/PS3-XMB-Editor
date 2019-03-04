using GongSolutions.Wpf.DragDrop;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;

namespace PS3_XMB_Editor
{
    class MainViewModel : IDropTarget
    {
        public ObservableCollection<MSP> MSPCollection { get; set; }

        public MainViewModel(string xml)
        {
            //MainWindow w = new MainWindow();
                string xmlString = File.ReadAllText("Files/xml/" + xml);
                xmlString = xmlString.Replace("&", "&amp;");

                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);
                XmlNode root1 = doc.DocumentElement;
                if (root1.HasChildNodes)
                {
                    MSPCollection = new ObservableCollection<MSP>();
                    MSPCollection.Clear();
                    for (int o = 0; o < root1.ChildNodes.Count; o++)
                    {

                        if (root1.ChildNodes[o].Attributes != null && root1.ChildNodes[o].Attributes.Item(0).Value.ToString() != "root")
                        {
                        MSPCollection.Add(new MSP()
                        {
                            Id = o + 1,
                            Name = root1.ChildNodes[o].Attributes.Item(0).Value.ToString(),
                            Cnode = root1.ChildNodes[o],
                            //EasyName1 = w.get_known(root1.ChildNodes[o].ChildNodes[o].Attributes.Item(1).Value.ToString())

                        });
                           
                        }

                    }
                }
            
        }


        void IDropTarget.DragOver(DropInfo dropInfo)
        {
            if (dropInfo.Data is MSP)
            {
                dropInfo.DropTargetAdorner = DropTargetAdorners.Highlight;
                dropInfo.Effects = DragDropEffects.Move;
            }
        }

        void IDropTarget.Drop(DropInfo dropInfo)
        {

            MSP msp = (MSP)dropInfo.Data;
            ((IList)dropInfo.DragInfo.SourceCollection).Remove(msp);
        }
    }

    class MSP
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Name4 { get; set; }
        public string EasyName1 { get; set; }
        public Visibility visibility2 { get; set; }
        public Visibility visibility3 { get; set; }
        public Visibility visibility4 { get; set; }

        

        public XmlNode Cnode { get; set; }

    }
}

