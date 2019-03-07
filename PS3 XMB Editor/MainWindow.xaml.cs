using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using GongSolutions.Wpf.DragDrop;
using MahApps.Metro;
using MahApps.Metro.Controls;
using MahApps.Metro.SimpleChildWindow;
using Newtonsoft.Json;
using Syncfusion.Windows.Edit;
using Syncfusion.Windows.Shared;

namespace PS3_XMB_Editor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        TestObject t = new TestObject();
        Cursor Cursor2;

        MainViewModel mainViewModel = new MainViewModel("category_user.xml");
        PatchViewModel patchViewModel = new PatchViewModel();

        public DataTable COLORS = new DataTable();
        public DataTable KnownItems = new DataTable();
        public string color;
        public string ctheme = "BaseDark";
        public DataTable dtpkg2 = new DataTable();
        string appPath = System.Reflection.Assembly.GetExecutingAssembly().Location;//System.Windows.Shapes.Path.GetDirectoryName(Application.ExecutablePath.);
        public MainWindow()
        {
            InitializeComponent();
            ChangeAppStyle();
            set_colors();
            set_known();

            DataContext = mainViewModel;//new MainViewModel();
            //SkinStorage.SetMetroBrush(this, Brushes.Green);
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            dtpkg2.Columns.Add("id");
            
            mainViewModel = new MainViewModel("category_user.xml");
            patchViewModel = new PatchViewModel();

            Open_xml("category_user.xml");
            Open_patch_xml();
            //ReadXDocumentWithInvalidCharacters("category_user.xml");
        }


        private void CursorSelector2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBox source = e.Source as ComboBox;

            if (source != null)
            {
                ComboBoxItem selectedCursor = source.SelectedItem as ComboBoxItem;

                // Changing the cursor of the Border control 
                // by setting the Cursor property
                switch (selectedCursor.Content.ToString())
                {
                    case "AppStarting":
                        t.Cursor1 = Cursors.AppStarting;
                        break;
                    case "ArrowCD":
                        t.Cursor1 = Cursors.ArrowCD;
                        break;
                    case "Arrow":
                        t.Cursor1 = Cursors.Arrow;
                        break;
                    case "Cross":
                        t.Cursor1 = Cursors.Cross;
                        break;
                    case "HandCursor":
                        t.Cursor1 = Cursors.Hand;
                        break;
                    case "Help":
                        t.Cursor1 = Cursors.Help;
                        break;
                    case "IBeam":
                        t.Cursor1 = Cursors.IBeam;
                        break;
                    case "No":
                        t.Cursor1 = Cursors.No;
                        break;
                    case "None":
                        t.Cursor1 = Cursors.None;
                        break;
                    case "Pen":
                        t.Cursor1 = Cursors.Pen;
                        break;
                    case "ScrollSE":
                        t.Cursor1 = Cursors.ScrollSE;
                        break;
                    case "ScrollWE":
                        t.Cursor1 = Cursors.ScrollWE;
                        break;
                    case "SizeAll":
                        t.Cursor1 = Cursors.SizeAll;
                        break;
                    case "SizeNESW":
                        t.Cursor1 = Cursors.SizeNESW;
                        break;
                    case "SizeNS":
                        t.Cursor1 = Cursors.SizeNS;
                        break;
                    case "SizeNWSE":
                        t.Cursor1 = Cursors.SizeNWSE;
                        break;
                    case "SizeWE":
                        t.Cursor1 = Cursors.SizeWE;
                        break;
                    case "UpArrow":
                        t.Cursor1 = Cursors.UpArrow;
                        break;
                    case "WaitCursor":
                        t.Cursor1 = Cursors.Wait;
                        break;
                    case "Middle Finger":
                        t.Cursor1 = Cursor2;
                        break;
                    default:
                        break;
                }


            }
        }

        private void MouseEnter2(object sender, MouseEventArgs e)
        {

            Tile t = sender as Tile;
            string n = t.Title;
            string x = "name = " + '\'' + n + '\'';

            if (e.RoutedEvent.Name == "MouseEnter")
            {
                COLORS.Select(x)[0]["bg"] = t.BorderBrush;
            }
            else
                COLORS.Select(x)[0]["bg"] = System.Windows.Media.Brushes.Transparent;

        }


        #region<<style>>


        private void button7_Click(object sender, RoutedEventArgs e)
        {
            if (ctheme == "BaseDark")
            {
                button7.Content = "Dark Theme";
                ctheme = "BaseLight";
            }
            else
            {
                button7.Content = "Light Theme";
                ctheme = "BaseDark";
            }
            ChangeAppStyle();
        }

        private void ListBoxItem_Selected(object sender, RoutedEventArgs e)
        {

            color = ((ListBoxItem)listBox1.SelectedValue).Content.ToString();
            ChangeAppStyle();
        }
        public void set_known()
        {
            string known = File.ReadAllText("Known_Items.json");
            KnownItems = (DataTable)JsonConvert.DeserializeObject(known, (typeof(DataTable)));
            
        }

        public string get_known(string n)
        {

            string x = "name = " + '\'' + n + '\'';
            try
            { 
            string r = KnownItems.Select(x)[0]["EasyName"].ToString();
            return (r);
            }
            catch
            {
                return(n);
            }

        }

        public void set_colors()
        {
            string colors = "[{\"name\":\"Red\",\"fill\":\"Red\",\"text\":\"Red\",\"bg\":\"Transparent\",\"count\":\"0\"}, {\"name\":\"Green\",\"fill\":\"Green\",\"text\":\"Green\",\"bg\":\"Transparent\",\"count\":\"1\"}, {\"name\":\"Blue\",\"fill\":\"Blue\",\"text\":\"Blue\",\"bg\":\"Transparent\",\"count\":\"2\"}, {\"name\":\"Purple\",\"fill\":\"Purple\",\"text\":\"Purple\",\"bg\":\"Transparent\",\"count\":\"3\"}, {\"name\":\"Orange\",\"fill\":\"Orange\",\"text\":\"Orange\",\"bg\":\"Transparent\",\"count\":\"4\"}, {\"name\":\"Lime\",\"fill\":\"Lime\",\"text\":\"Lime\",\"bg\":\"Transparent\",\"count\":\"5\"}, {\"name\":\"Emerald\",\"fill\":\"#FF077517\",\"text\":\"Emerald\",\"bg\":\"Transparent\",\"count\":\"6\"}, {\"name\":\"Teal\",\"fill\":\"Teal\",\"text\":\"Teal\",\"bg\":\"Transparent\",\"count\":\"7\"}, {\"name\":\"Cyan\",\"fill\":\"Cyan\",\"text\":\"Cyan\",\"bg\":\"Transparent\",\"count\":\"8\"}, {\"name\":\"Cobalt\",\"fill\":\"#FF0747C6\",\"text\":\"Cobalt\",\"bg\":\"Transparent\",\"count\":\"9\"}, {\"name\":\"Indigo\",\"fill\":\"Indigo\",\"text\":\"Indigo\",\"bg\":\"Transparent\",\"count\":\"10\"}, {\"name\":\"Violet\",\"fill\":\"Violet\",\"text\":\"Violet\",\"bg\":\"Transparent\",\"count\":\"11\"}, {\"name\":\"Pink\",\"fill\":\"Pink\",\"text\":\"Pink\",\"bg\":\"Transparent\",\"count\":\"12\"}, {\"name\":\"Magenta\",\"fill\":\"Magenta\",\"text\":\"Magenta\",\"bg\":\"Transparent\",\"count\":\"13\"}, {\"name\":\"Crimson\",\"fill\":\"Crimson\",\"text\":\"Crimson\",\"bg\":\"Transparent\",\"count\":\"14\"}, {\"name\":\"Amber\",\"fill\":\"#FFC7890F\",\"text\":\"Amber\",\"bg\":\"Transparent\",\"count\":\"15\"}, {\"name\":\"Yellow\",\"fill\":\"Yellow\",\"text\":\"Yellow\",\"bg\":\"Transparent\",\"count\":\"16\"}, {\"name\":\"Brown\",\"fill\":\"Brown\",\"text\":\"Brown\",\"bg\":\"Transparent\",\"count\":\"17\"}, {\"name\":\"Olive\",\"fill\":\"Olive\",\"text\":\"Olive\",\"bg\":\"Transparent\",\"count\":\"18\"}, {\"name\":\"Steel\",\"fill\":\"#FF576573\",\"text\":\"Steel\",\"bg\":\"Transparent\",\"count\":\"19\"}, {\"name\":\"Mauve\",\"fill\":\"#FF655475\",\"text\":\"Mauve\",\"bg\":\"Transparent\",\"count\":\"20\"}, {\"name\":\"Taupe\",\"fill\":\"#FF736845\",\"text\":\"Taupe\",\"bg\":\"Transparent\",\"count\":\"21\"}, {\"name\":\"Sienna\",\"fill\":\"Sienna\",\"text\":\"Sienna\",\"bg\":\"Transparent\",\"count\":\"22\"}]";
            COLORS = (DataTable)JsonConvert.DeserializeObject(colors, (typeof(DataTable)));
            Color_Grid.DataContext = null;
            Color_Grid.DataContext = COLORS.DefaultView;
        }

        private void TILE1_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Tile t = sender as Tile;
            string n = t.Title;
            color = n;
            ChangeAppStyle();
            string x = "name = " + '\'' + n + '\'';
            COLORS.Select(x)[0]["bg"] = t.BorderBrush;
        }


        public void ChangeAppStyle()
        {

            if (ctheme == "BaseDark")
            {

                color2.Background = System.Windows.Media.Brushes.White;
                
            }
            else if (ctheme == "BaseLight")
            {
                color2.Background = new SolidColorBrush(System.Windows.Media.Color.FromArgb(0xFF, 0x25, 0x25, 0x25));
                
            }
            if (ctheme == "")
            {
                ctheme = "BaseDark";
                color2.Background = System.Windows.Media.Brushes.White;
                
            }

            var theme = ThemeManager.DetectAppStyle(System.Windows.Application.Current);

            if (color == null || color == "")
            {
                color = "Steel";
            }
            ThemeManager.ChangeAppStyle(System.Windows.Application.Current,
                                        ThemeManager.GetAccent(color),

                                        ThemeManager.GetAppTheme(ctheme));
            

        }

        
        #endregion<<style>>


        public void Open_xml(string xml)
        {
            try
            {
                // lbtest.Items.Clear();
                dtpkg2.Clear();
                
                mainViewModel = new MainViewModel(xml);

                string xmlString = File.ReadAllText("Files/xml/" + xml);
                xmlString = xmlString.Replace("&", "&amp;");
               // xmlString = xmlString.Replace("\t", "&#x9;").Replace("\r", "&#xd;").Replace("\n", "&#xa;");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);
                XmlNode root1 = doc.DocumentElement;
                if (root1.HasChildNodes)
                {
                   mainViewModel.MSPCollection = new ObservableCollection<MSP>();
                    mainViewModel.MSPCollection.Clear();
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
                                            if(root1.ChildNodes[o].ChildNodes[tmp].Name == "Items")
                                            {
                                            int t = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Count;
                                                string EasyNames = get_known(root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(1).Value.ToString());
                                                
                                            if (t == 1)
                                            {
                                                    mainViewModel.MSPCollection.Add(new MSP()
                                                    {
                                                        Id = tmp2 + 1,
                                                        Name = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(0).Value.ToString(),
                                                        EasyName1 = EasyNames,
                                                        Cnode = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2],
                                                        visibility2 = Visibility.Collapsed,
                                                        visibility3 = Visibility.Collapsed,
                                                        visibility4 = Visibility.Collapsed

                                                    });

                                               }
                                            if (t == 2)
                                            {
                                                mainViewModel.MSPCollection.Add(new MSP()
                                                {
                                                    Id = tmp2 + 1,
                                                    Name = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(0).Value.ToString(),
                                                    Name2 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(1).Value.ToString(),
                                                    EasyName1 = EasyNames,
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
                                                mainViewModel.MSPCollection.Add(new MSP()
                                                {
                                                    Id = tmp2 + 1,
                                                    Name = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(0).Value.ToString(),
                                                    Name2 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(1).Value.ToString(),
                                                    Name3 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(2).Value.ToString(),
                                                    EasyName1 = EasyNames,
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
                                                    mainViewModel.MSPCollection.Add(new MSP()
                                                    {
                                                        Id = tmp2 + 1,
                                                        Name = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(0).Value.ToString(),
                                                        Name2 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(1).Value.ToString(),
                                                        Name3 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(2).Value.ToString(),
                                                        Name4 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(3).Value.ToString(),
                                                        EasyName1 = EasyNames,
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
                                DataContext = mainViewModel;
                                
                            }
                        }

                    }
                }
                        System.Windows.Application.Current.Dispatcher.Invoke(
                DispatcherPriority.Normal,
    (ThreadStart)delegate
    {
       /* lbtest.DataContext = null;
        lb2.DataContext = null;
        lb3.DataContext = null;
        lb4.DataContext = null;
        lb5.DataContext = null;
        lb6.DataContext = null;
        lb7.DataContext = null;
        lb8.DataContext = null;*/
    });


                System.Windows.Application.Current.Dispatcher.Invoke(
                               DispatcherPriority.Normal,
                               (ThreadStart)delegate
                               {
                                   try
                                   {
                                       /*
                                       lbtest.DataContext = null;
                                       lb2.DataContext = null;
                                       lb3.DataContext = null;
                                       lb4.DataContext = null;
                                       lb5.DataContext = null;
                                       lb6.DataContext = null;
                                       lb7.DataContext = null;
                                       lb8.DataContext = null;

                                       lbtest.DataContext = dtpkg2.DefaultView;
                                       lb2.DataContext = dtpkg2.DefaultView;
                                       lb3.DataContext = dtpkg2.DefaultView;
                                       lb4.DataContext = dtpkg2.DefaultView;
                                       lb5.DataContext = dtpkg2.DefaultView;
                                       lb6.DataContext = dtpkg2.DefaultView;
                                       lb7.DataContext = dtpkg2.DefaultView;
                                       lb8.DataContext = dtpkg2.DefaultView;

                                       

                                       lbtest.Items.Refresh();
                                       lb2.Items.Refresh();
                                       lb3.Items.Refresh();
                                       lb4.Items.Refresh();
                                       lb5.Items.Refresh();
                                       lb6.Items.Refresh();
                                       lb7.Items.Refresh();
                                       lb8.Items.Refresh();*/
                                   }
                                   catch (Exception ex)
                                   {

                                   }
                               });



            }
            catch (Exception ex)
            {
                /*added a try catch for this enitire method as well as something is causing it to fall over on some of the pkg's i tested */
            }


        }

        public void Open_patch_xml()
        {
            try
            {
                // lbtest.Items.Clear();
                //dtpkg2.Clear();

                patchViewModel = new PatchViewModel();

                string xmlString = File.ReadAllText("Files/xml/Patches/patches.xml");
                xmlString = xmlString.Replace("&", "&amp;");
                // xmlString = xmlString.Replace("\t", "&#x9;").Replace("\r", "&#xd;").Replace("\n", "&#xa;");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlString);
                XmlNode root1 = doc.DocumentElement;
                if (root1.HasChildNodes)
                {
                    patchViewModel.PatchCollection = new ObservableCollection<MSP>();
                    patchViewModel.PatchCollection.Clear();
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
                                                string EasyNames = get_known(root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(1).Value.ToString());

                                                if (t == 1)
                                                {
                                                    patchViewModel.PatchCollection.Add(new MSP()
                                                    {
                                                        Id = tmp2 + 1,
                                                        Name = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(0).Value.ToString(),
                                                        EasyName1 = EasyNames,
                                                        Cnode = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2],
                                                        visibility2 = Visibility.Collapsed,
                                                        visibility3 = Visibility.Collapsed,
                                                        visibility4 = Visibility.Collapsed

                                                    });

                                                }
                                                if (t == 2)
                                                {
                                                    patchViewModel.PatchCollection.Add(new MSP()
                                                    {
                                                        Id = tmp2 + 1,
                                                        Name = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(0).Value.ToString(),
                                                        Name2 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(1).Value.ToString(),
                                                        EasyName1 = EasyNames,
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
                                                    patchViewModel.PatchCollection.Add(new MSP()
                                                    {
                                                        Id = tmp2 + 1,
                                                        Name = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(0).Value.ToString(),
                                                        Name2 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(1).Value.ToString(),
                                                        Name3 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(2).Value.ToString(),
                                                        EasyName1 = EasyNames,
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
                                                    patchViewModel.PatchCollection.Add(new MSP()
                                                    {
                                                        Id = tmp2 + 1,
                                                        Name = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(0).Value.ToString(),
                                                        Name2 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(1).Value.ToString(),
                                                        Name3 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(2).Value.ToString(),
                                                        Name4 = root1.ChildNodes[o].ChildNodes[tmp].ChildNodes[tmp2].Attributes.Item(3).Value.ToString(),
                                                        EasyName1 = EasyNames,
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
                                userpatchList.DataContext = patchViewModel;

                                gamepatchList.DataContext = patchViewModel;
                                musicpatchList.DataContext = patchViewModel;
                                tvpatchList.DataContext = patchViewModel;
                                photopatchList.DataContext = patchViewModel;
                                psnpatchList.DataContext = patchViewModel;
                                networkpatchList.DataContext = patchViewModel;
                                videopatchList.DataContext = patchViewModel;
                            }
                        }

                    }
                }
                System.Windows.Application.Current.Dispatcher.Invoke(
        DispatcherPriority.Normal,
(ThreadStart)delegate
{
        /* lbtest.DataContext = null;
         lb2.DataContext = null;
         lb3.DataContext = null;
         lb4.DataContext = null;
         lb5.DataContext = null;
         lb6.DataContext = null;
         lb7.DataContext = null;
         lb8.DataContext = null;*/
});


                System.Windows.Application.Current.Dispatcher.Invoke(
                               DispatcherPriority.Normal,
                               (ThreadStart)delegate
                               {
                                   try
                                   {
                                       /*
                                       lbtest.DataContext = null;
                                       lb2.DataContext = null;
                                       lb3.DataContext = null;
                                       lb4.DataContext = null;
                                       lb5.DataContext = null;
                                       lb6.DataContext = null;
                                       lb7.DataContext = null;
                                       lb8.DataContext = null;

                                       lbtest.DataContext = dtpkg2.DefaultView;
                                       lb2.DataContext = dtpkg2.DefaultView;
                                       lb3.DataContext = dtpkg2.DefaultView;
                                       lb4.DataContext = dtpkg2.DefaultView;
                                       lb5.DataContext = dtpkg2.DefaultView;
                                       lb6.DataContext = dtpkg2.DefaultView;
                                       lb7.DataContext = dtpkg2.DefaultView;
                                       lb8.DataContext = dtpkg2.DefaultView;

                                       

                                       lbtest.Items.Refresh();
                                       lb2.Items.Refresh();
                                       lb3.Items.Refresh();
                                       lb4.Items.Refresh();
                                       lb5.Items.Refresh();
                                       lb6.Items.Refresh();
                                       lb7.Items.Refresh();
                                       lb8.Items.Refresh();*/
                                   }
                                   catch (Exception ex)
                                   {

                                   }
                               });



            }
            catch (Exception ex)
            {
                /*added a try catch for this enitire method as well as something is causing it to fall over on some of the pkg's i tested */
            }


        }

        private void lbtest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_1_Click(object sender, RoutedEventArgs e)
        {

        }
        
        private void FlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lbxmb_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void metroAnimatedTabControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ClickableLabel_Click(object sender, RoutedEventArgs e)
        {
            ClickableLabel Q = sender as ClickableLabel;
            string clbname = Q.Name;

            if(clbname == "clb1")
            {
                
                XMBfv.SelectedIndex = 0;
                reset_xmblabels();
                Open_xml("category_user.xml");
                clb1l.Foreground = (Brush)Application.Current.MainWindow.FindResource("BlackBrush");
                clb1.Background = (Brush)Application.Current.MainWindow.FindResource("AccentColorBrush");
            }
            else if (clbname == "clb2")
            {
                XMBfv.SelectedIndex = 1;
                reset_xmblabels();
                Open_xml("category_photo.xml");
                clb2l.Foreground = (Brush)Application.Current.MainWindow.FindResource("BlackBrush");
                clb2.Background = (Brush)Application.Current.MainWindow.FindResource("AccentColorBrush");

            }
            else if (clbname == "clb3")
            {
                XMBfv.SelectedIndex = 2;
                reset_xmblabels();
                Open_xml("category_music.xml");
                clb3l.Foreground = (Brush)Application.Current.MainWindow.FindResource("BlackBrush");
                clb3.Background = (Brush)Application.Current.MainWindow.FindResource("AccentColorBrush");

            }
            else if (clbname == "clb4")
            {
                XMBfv.SelectedIndex = 3;
                reset_xmblabels();
                Open_xml("category_video.xml");
                clb4l.Foreground = (Brush)Application.Current.MainWindow.FindResource("BlackBrush");
                clb4.Background = (Brush)Application.Current.MainWindow.FindResource("AccentColorBrush");

            }
            else if (clbname == "clb5")
            {
                XMBfv.SelectedIndex = 4;
                reset_xmblabels();
                Open_xml("category_tv.xml");
                clb5l.Foreground = (Brush)Application.Current.MainWindow.FindResource("BlackBrush");
                clb5.Background = (Brush)Application.Current.MainWindow.FindResource("AccentColorBrush");

            }
            else if (clbname == "clb6")
            {
                XMBfv.SelectedIndex = 5;
                reset_xmblabels();
                Open_xml("category_game.xml");
                clb6l.Foreground = (Brush)Application.Current.MainWindow.FindResource("BlackBrush");
                clb6.Background = (Brush)Application.Current.MainWindow.FindResource("AccentColorBrush");

            }
            else if (clbname == "clb7")
            {
                XMBfv.SelectedIndex = 6;
                reset_xmblabels();
                Open_xml("category_network.xml");
                clb7l.Foreground = (Brush)Application.Current.MainWindow.FindResource("BlackBrush");
                clb7.Background = (Brush)Application.Current.MainWindow.FindResource("AccentColorBrush");

            }
            else if (clbname == "clb8")
            {
                XMBfv.SelectedIndex = 7;
                reset_xmblabels();
                Open_xml("category_psn.xml");
                clb8l.Foreground = (Brush)Application.Current.MainWindow.FindResource("BlackBrush");
                clb8.Background = (Brush)Application.Current.MainWindow.FindResource("AccentColorBrush");

            }


        }

        private void reset_xmblabels()
        { 


                clb1.Background = new SolidColorBrush(Colors.Transparent);

                clb2.Background = new SolidColorBrush(Colors.Transparent);

                clb3.Background = new SolidColorBrush(Colors.Transparent);

                clb4.Background = new SolidColorBrush(Colors.Transparent);

                clb5.Background = new SolidColorBrush(Colors.Transparent);

                clb6.Background = new SolidColorBrush(Colors.Transparent);

                clb7.Background = new SolidColorBrush(Colors.Transparent);

                clb8.Background = new SolidColorBrush(Colors.Transparent);

               // clb9.Background = new SolidColorBrush(Colors.Transparent);

            clb1l.Foreground = (Brush)Application.Current.MainWindow.FindResource("GrayBrush7");
            clb2l.Foreground = (Brush)Application.Current.MainWindow.FindResource("GrayBrush7");
            clb3l.Foreground = (Brush)Application.Current.MainWindow.FindResource("GrayBrush7");
            clb4l.Foreground = (Brush)Application.Current.MainWindow.FindResource("GrayBrush7");
            clb5l.Foreground = (Brush)Application.Current.MainWindow.FindResource("GrayBrush7");
            clb6l.Foreground = (Brush)Application.Current.MainWindow.FindResource("GrayBrush7");
            clb7l.Foreground = (Brush)Application.Current.MainWindow.FindResource("GrayBrush7");
            clb8l.Foreground = (Brush)Application.Current.MainWindow.FindResource("GrayBrush7");
            //clb9l.Foreground = (Brush)Application.Current.MainWindow.FindResource("GrayBrush7");

        }

        private void clb1_MouseEnter(object sender, MouseEventArgs e)
        {
            if (sender != null)
            {
                ClickableLabel Q = sender as ClickableLabel;
                if (sender != null)
                {
                    string clbname = Q.Name;
                    int Qnum = Convert.ToInt32(clbname.Replace("clb", "")) - 1;
                    if (XMBfv.SelectedIndex != Qnum)
                    {
                        Q.Background = (Brush)Application.Current.MainWindow.FindResource("AccentColorBrush3");

                    }
                }
            }
        }

        private void clb1_MouseLeave(object sender, MouseEventArgs e)
        {
            if (sender != null)
            {
                ClickableLabel Q = sender as ClickableLabel;
                string clbname = Q.Name;
                int Qnum = Convert.ToInt32(clbname.Replace("clb", "")) -1;
                if (XMBfv.SelectedIndex != Qnum)
                {
                    Q.Background = new SolidColorBrush(Colors.Transparent);
                }
                else
                {
                    //clb1l.Foreground = (Brush)Application.Current.MainWindow.FindResource("BlackBrush");
                    Q.Background = (Brush)Application.Current.MainWindow.FindResource("AccentColorBrush");

                }
            }
        }

        private void ClickableLabel_Click_1(object sender, RoutedEventArgs e)
        {

        }

        private void button_5_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            ListBox item1 = sender as ListBox;
            if (item1.SelectedItem != null)
            {
                MSP item2 = item1.SelectedItem as MSP;
                
            }

        }

        private void ClickableLabel_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            
        }

        private void ClickableLabel_Click_2(object sender, RoutedEventArgs e)
        {

        }
        
        private async void OpenAChildWindow_OnButtonClick(object sender, RoutedEventArgs e)
        {
           // button.IsEnabled = false;
            metroAnimatedTabControl.IsEnabled = false;
            
            await this.ShowChildWindowAsync(new Settings() { IsModal = false, CloseOnOverlay = true, IsWindowHostActive = false });
           // button.IsEnabled = true;
            metroAnimatedTabControl.IsEnabled = true;
        }

        private async void OpenEditorWindow_OnButtonClick(object sender, RoutedEventArgs e)
        {
            string xml = "category_user.xml";

            //button.IsEnabled = false;
            
            int xmlnum = XMBfv.SelectedIndex;
            switch (xmlnum)
            {
                case 0:
                    xml = "category_user.xml";
                    break;
                case 1:
                    xml = "category_photo.xml";
                    break;
                case 2:
                    xml = "category_music.xml";
                    break;
                case 3:
                    xml = "category_photo.xml";
                    break;
                case 4:
                    xml = "category_video.xml";
                    break;
                case 5:
                    xml = "category_tv.xml";
                    break;
                case 6:
                    xml = "category_psn.xml";
                    break;
                case 7:
                    xml = "category_network.xml";
                    break;
            }
            metroAnimatedTabControl.IsEnabled = false;

            await this.ShowChildWindowAsync(new Editor(xml) { IsModal = false, CloseOnOverlay = true, IsWindowHostActive = false});
          //  button.IsEnabled = true;
            metroAnimatedTabControl.IsEnabled = true;
        }

        private void cfw_han_lable_Click(object sender, RoutedEventArgs e)
        {
            if(cfw_han_lable.Content == "CFW Mode")
            {
                cfw_han_lable.Content = "HAN Mode";
            }
            else
                cfw_han_lable.Content = "CFW Mode";
        }

        private void button_4_Click(object sender, RoutedEventArgs e)
        {
            ChangeAppStyle();
        }


        private void Image_DragEnter(object sender, DragEventArgs e)
        {
            var s = sender as Image;
            //DragEventArgs dropInfo = e as DragEventArgs;
            
            if (s.DataContext == mainViewModel)
            {
                
            }
        }
        
        private void Image_Drop(object sender, DragEventArgs e)
        {

            var s = sender as Image;
            DragEventArgs dropInfo = e as DragEventArgs;

            if (s.DataContext == mainViewModel)
            {
                MSP t = userList.Items.CurrentItem as MSP;
                mainViewModel.MSPCollection.Remove(t);
            }

        }

        #region<<ContextMenu>>
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var s = sender as MenuItem;
            MSP t = s.DataContext as MSP;
            mainViewModel.MSPCollection.Remove(t);
        }

        private void ContextMenu_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void ContextMenu_MouseLeave(object sender, MouseEventArgs e)
        {

        }
        #endregion<<ContextMenu>>

        #region<<UnselectAll>>
        private void userList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            userList.UnselectAll();
        }

        private void userpatchList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            userpatchList.UnselectAll();
        }

        private void photoList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            photoList.UnselectAll();
        }

        private void photopatchList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            photopatchList.UnselectAll();
        }

        private void musicList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            musicList.UnselectAll();
        }

        private void musicpatchList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            musicpatchList.UnselectAll();
        }

        private void videoList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            videoList.UnselectAll();
        }

        private void videopatchList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            videopatchList.UnselectAll();
        }

        private void tvList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tvList.UnselectAll();
        }

        private void tvpatchList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tvpatchList.UnselectAll();
        }

        private void gameList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            gameList.UnselectAll();
        }

        private void gamepatchList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            gamepatchList.UnselectAll();
        }

        private void networkList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            networkList.UnselectAll();
        }

        private void networkpatchList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            networkpatchList.UnselectAll();
        }

        private void psnList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            psnList.UnselectAll();
        }

        private void psnpatchList_MouseDown(object sender, MouseButtonEventArgs e)
        {
            psnpatchList.UnselectAll();
        }
        #endregion<<UnselectAll>>


    }


    class TestObject : INotifyPropertyChanged
    {
        private Cursor _cursor;
        public Cursor Cursor1
        {
            get
            {
                return _cursor;
            }

            set
            {
                if (_cursor == value) return;

                _cursor = value;
                OnPropertyChanged("Cursor1");
            }
        }
        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }


    public class Mysource
    {
        object source { get; set; }
        FrameworkElement control { get; set; }
    }



}
