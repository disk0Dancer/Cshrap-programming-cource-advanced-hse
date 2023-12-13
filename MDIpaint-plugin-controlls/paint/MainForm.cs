using PluginLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.XPath;
using System.Xml.Linq;

namespace paint
{
    public partial class MainForm : Form
    {
        public static Color Color { set; get; }
        public static int Thickness { get; set; }
        public static Tools Tool { get; set; }
        public static Tools Prev_Tool { get; set; }
        public new static int Width { get; set; } 
        public new static int Height { get; set; }
        static DocumentForm1 CurrentForm { get; set; }

        Dictionary<string, PluginLib.IPlugin> plugins = new Dictionary<string, IPlugin>();

        public MainForm()
        {
            InitializeComponent();
            Color = Color.Black;
            Thickness = 6;
            Tool = Tools.Pen;
            Prev_Tool = Tools.Pen;
            Width = 600;
            Height = 400;
            FindPlugins();
            CreatePluginsMenu();

        }

        private void toolStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {

        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {

        }
        # region forms
        private void ClickAbout(object sender, EventArgs e)
        {
            var frmAbout = new AboutForm();
            frmAbout.ShowDialog();
        }

        private void ClickExit(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void NewClick(object sender, EventArgs e)
        {
            var frm = new DocumentForm1();
            frm.MdiParent = this;
            frm.Show();
        }

        private void canvasSizeFormStripLabel1_Click(object sender, EventArgs e)
        {
            var frm = new CanvasSizeForm(this);
            frm.MdiParent = this;
            frm.Show();
        }

        private void ThicknessStripLabel6_Click(object sender, EventArgs e)
        {
            var frm = new ThicknessSizeForm();
            frm.MdiParent = this;
            frm.Show();
        } 

        private void PictureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CanvasSizeToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
        }

        private void FileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
            saveAsToolStripMenuItem.Enabled = !(ActiveMdiChild == null);
        }

        #endregion
        #region color n formstile
        private void RedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color = Color.Red;
        }

        private void BlueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color = Color.Blue;
        }

        private void GreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Color = Color.Green;
        }

        private void AnotherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
                Color = cd.Color;
        }
        
        //
        private void cascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void leftrightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void updownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void orderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }
        #endregion
        #region tools
        private void penStripButton_Click(object sender, EventArgs e)
        {
            
           
            Prev_Tool = Tool;
            
            Tool = Tools.Pen;
        }

        private void circleStripButton_Click(object sender, EventArgs e)
        {
            Prev_Tool = Tool;
            Tool = Tools.Ellipse;
        }

        private void starStripButton_Click(object sender, EventArgs e)
        {
            Prev_Tool = Tool;
            Tool = Tools.Star;
        }

        private void eraserStripButton_Click(object sender, EventArgs e)
        {
            Prev_Tool = Tool;
            Tool = Tools.Eraser;
        }

        private void lineStripButton_Click(object sender, EventArgs e)
        {
            Prev_Tool = Tool;
            Tool = Tools.Line;
        }
        #endregion
        #region save open
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dlg = new OpenFileDialog();
            dlg.Filter = "Файлы JPEG (*.jpeg, *.jpg)|*.jpeg;*.jpg|Windows Bitmap (*.bmp)|*.bmp|Все файлы ()*.*|*.*";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                var newBitmap = new Bitmap(Bitmap.FromFile(dlg.FileName));
                string newPath = dlg.FileName;
                var newForm = new DocumentForm1(newBitmap, newPath);
                newForm.saveDlg.FileName = dlg.FileName;
                newForm.WasOpened = true;
                newForm.FilePath = newPath;
                newForm.Text = dlg.FileName.Split('\\').Last();
                newForm.MdiParent = this;
                newForm.Show();
            }
        }

        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = ActiveMdiChild as DocumentForm1;

            Bitmap currentBitmap = d.bitmap;
            if (d != null && !(d.WasOpened))
            {
                var dlg = new SaveFileDialog();
                dlg.AddExtension = true;
                dlg.Filter = " Файлы JPEG (*.jpg)|*.jpg|Windows Bitmap (*.bmp)|*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    currentBitmap.Save(dlg.FileName);
                    d.WasOpened = true;
                    d.FilePath = dlg.FileName;
                    d.wasChanged = false;
                    d.Text = dlg.FileName.Split('\\').Last();
                    d.saveDlg = dlg;
                }
            }
            else if (d != null && d.WasOpened)
            {
                d.bitmap.Save(d.saveDlg.FileName);
                d.WasOpened = true;
                d.wasChanged = false;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var d = ActiveMdiChild as DocumentForm1;

            if (d != null)
            {
                var dlg = new SaveFileDialog();
                dlg.AddExtension = true;
                dlg.Filter = " Файлы JPEG (*.jpg)|*.jpg|Windows Bitmap (*.bmp)|*.bmp";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    d.bitmap.Save(dlg.FileName);
                    d.WasOpened = true;
                    d.FilePath = dlg.FileName;
                    d.Text = dlg.FileName.Split('\\').Last();
                    d.saveDlg = dlg;
                    d.wasChanged = false;
                }

            }
        }
        

        private void Btnminim_Click(object sender, EventArgs e)
        {

            //minimize
        }

        private void Btnmaxim_Click(object sender, EventArgs e)
        {
            //maximize
            //if (this.CurrentForm.DScale != 0)
            //{

            //    //MainForm.Prev_Tool = MainForm.Tool;
            //    MainForm.Tool = Tools.DScale;
            //    bitmapTemp = (Bitmap)bitmap.Clone();
            //    pictureBox1.Image = ZoomPicture(bitmapTemp, new Size(trackBar1.Value, trackBar1.Value));

            //}
        }

        #endregion
        // REFLECTION

        void FindPlugins()
        {
            // папка с плагинами
            string folder = AppDomain.CurrentDomain.BaseDirectory;

            var plugs = XDocument.Load("plugins.xml").Root.Elements().ToList();

            // dll-файлы в этой папке
            string[] files = Directory.GetFiles(folder, "*.dll");

            foreach (string file in files)
                try
                {
                    Assembly assembly = Assembly.LoadFile(file);

                    foreach (Type type in assembly.GetTypes())
                    {
                        Type iface = type.GetInterface("PluginLib.IPlugin");

                        if (iface != null)
                        {
                            IPlugin plugin = (IPlugin)Activator.CreateInstance(type);
                            if (plugs != null)
                            {
                                foreach(var p in plugs)
                                {
                                    if (p.Value == plugin.Name)
                                        plugins.Add(plugin.Name, plugin);
                                }
                            }
                            else
                            {
                                plugins.Add(plugin.Name, plugin);
                            }
                            
                            
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка загрузки плагина\n" + ex.Message);
                }
        }
        private void OnPluginClick(object sender, EventArgs args)
        {
            IPlugin plugin = plugins[((ToolStripMenuItem)sender).Text];
            if (ActiveMdiChild != null)
                (ActiveMdiChild as DocumentForm1).OnPluginClick(sender, args, plugin);

            

        }

        private void CreatePluginsMenu()
        {
            foreach (var p in plugins)
            {
                var item = filtersDropDown.DropDownItems.Add(p.Value.Name);
                item.Click += OnPluginClick;
            }
        }

        private void FiltersDropDown_Click(object sender, EventArgs e)
        {
            
        }

        private void AllPluginsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmAbout = new PluginsForm(plugins);
            frmAbout.Show();
        }
    }

}

    


 