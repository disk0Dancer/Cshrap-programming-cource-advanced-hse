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
    public partial class CanvasSizeForm : Form
    {
        private MainForm mainForm;
        public CanvasSizeForm(MainForm mainForm)
        {
            InitializeComponent();
            this.mainForm = mainForm;
            //widthNumericUpDown.Value = ((DocumentForm1)mainForm.ActiveMdiChild).Width;
            //heightNumericUpDown.Value = ((DocumentForm1)mainForm.ActiveMdiChild).Height;
        }

        private void CanvasSizeForm_Load(object sender, EventArgs e)
        {

        }

        private void Btnok_Click(object sender, EventArgs e)
        {
            int w,h;
            if (int.TryParse(textBox1.Text, out w))
            {
                if (int.TryParse(textBox2.Text, out h))
                {
                    MainForm.Height = h;
                    Close();
                }
                else
                {
                    //invalid
                    MessageBox.Show("Please enter a valid number");
                    Close();
                }
                MainForm.Width = w;
                Close();
            }
            else
            {
                //invalid
                MessageBox.Show("Please enter a valid number");
                this.Close();
            }
        }

        private void Btncancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
