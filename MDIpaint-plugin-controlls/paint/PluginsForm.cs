using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace paint
{
    public partial class PluginsForm : Form
    {
        int i = 20;

        public PluginsForm(Dictionary<string, PluginLib.IPlugin> plugins)
        {
            
            InitializeComponent();

            var label = new Label();
            var t = "";
            foreach (var p in plugins.Values)
            {

                var vers = p.GetType()
                        .GetCustomAttributes(true)
                        .Select(v => v.ToString()).First();

                t += "+ " + p.ToString() + ", " + vers + "\n";
            }

            label.Text = t;
            label.Visible = true;
            label.AutoSize = true;
            label.Location = new Point(100 + i, 100);
            Controls.Add(label);

        }

    }
}
