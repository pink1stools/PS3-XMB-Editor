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
            dropInfo.DragInfo.SourceCollection.ToString();
            MSP msp = (MSP)dropInfo.Data;
            ((IList)dropInfo.DragInfo.SourceCollection).Remove(msp);
        }
    }

    class PatchViewModel : IDropTarget
    {
        public ObservableCollection<MSP> PatchCollection { get; set; }

        public PatchViewModel()
        {
            //MainWindow w = new MainWindow();
            string xmlString = File.ReadAllText("Files/xml/Patches/patches.xml");
            xmlString = xmlString.Replace("&", "&amp;");

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);
            XmlNode root1 = doc.DocumentElement;
            if (root1.HasChildNodes)
            {
                PatchCollection = new ObservableCollection<MSP>();
                PatchCollection.Clear();
                for (int o = 0; o < root1.ChildNodes.Count; o++)
                {

                    if (root1.ChildNodes[o].Attributes != null && root1.ChildNodes[o].Attributes.Item(0).Value.ToString() == "root")
                    {
                        if (root1.ChildNodes[o].HasChildNodes)
                        {
                            //int tmp1 = root1.ChildNodes[o].ChildNodes.Count;
                            for (int tmp = 0; tmp < root1.ChildNodes[o].ChildNodes.Count; tmp++)
                            {
                                if (root1.ChildNodes[o].ChildNodes[tmp].HasChildNodes)
                                {
                                    //int tmp1 = root1.ChildNodes[o].ChildNodes.Count;
                                    for (int tmp2 = 0; tmp2 < root1.ChildNodes[o].ChildNodes[tmp].ChildNodes.Count; tmp2++)
                                    {
                                        if (root1.ChildNodes[o].ChildNodes[tmp].Name == "Items")
                                        {
                                            int t = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Count;
                                            //string EasyNames = get_known(root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(1).Value.ToString());

                                            if (t == 1)
                                            {
                                                PatchCollection.Add(new MSP()
                                                {
                                                    Id = tmp2 + 1,
                                                    Name = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(0).Value.ToString(),
                                                    EasyName1 = "test",
                                                    Cnode = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2],
                                                    visibility2 = Visibility.Collapsed,
                                                    visibility3 = Visibility.Collapsed,
                                                    visibility4 = Visibility.Collapsed

                                                });

                                            }
                                            if (t == 2)
                                            {
                                                PatchCollection.Add(new MSP()
                                                {
                                                    Id = tmp2 + 1,
                                                    Name = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(0).Value.ToString(),
                                                    Name2 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(1).Value.ToString(),
                                                    EasyName1 = "test",
                                                    Cnode = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2],
                                                    visibility2 = Visibility.Visible,
                                                    visibility3 = Visibility.Collapsed,
                                                    visibility4 = Visibility.Collapsed
,

                                                });
                                                //get_known(root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(1).Value.ToString());
                                            }
                                            if (t == 3)
                                            {
                                                PatchCollection.Add(new MSP()
                                                {
                                                    Id = tmp2 + 1,
                                                    Name = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(0).Value.ToString(),
                                                    Name2 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(1).Value.ToString(),
                                                    Name3 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(2).Value.ToString(),
                                                    EasyName1 = "test",
                                                    Cnode = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2],
                                                    visibility2 = Visibility.Visible,
                                                    visibility3 = Visibility.Visible,
                                                    visibility4 = Visibility.Collapsed
,

                                                });
                                                //get_known(root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(1).Value.ToString());
                                            }
                                            if (t == 4)
                                            {
                                                PatchCollection.Add(new MSP()
                                                {
                                                    Id = tmp2 + 1,
                                                    Name = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(0).Value.ToString(),
                                                    Name2 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(1).Value.ToString(),
                                                    Name3 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(2).Value.ToString(),
                                                    Name4 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(3).Value.ToString(),
                                                    EasyName1 = "test",
                                                    Cnode = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2],
                                                    visibility2 = Visibility.Visible,
                                                    visibility3 = Visibility.Visible,
                                                    visibility4 = Visibility.Visible
,

                                                });
                                                //get_known(root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(1).Value.ToString());
                                            }
                                        }


                                    }
                                }
                            }
                            /*
                            DataRow dr = dtpkg2.NewRow();
                            dr["id"] = root1.ChildNodes[o].Attributes.Item(0).Value.ToString();
                            mainViewModel.MSPCollection.Add(new MSP()
                            {
                                Id = o + 1,
                                Name = root1.ChildNodes[o].Attributes.Item(0).Value.ToString(),
                                Cnode = root1.ChildNodes[o]
                            });
                            */



                            //dtpkg2.Rows.Add(dr);
                            //DataContext = mainViewModel;

                        }
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
            dropInfo.DragInfo.SourceCollection.ToString();
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

