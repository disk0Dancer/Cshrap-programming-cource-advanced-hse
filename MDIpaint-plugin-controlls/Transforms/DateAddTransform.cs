using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Transforms
{
    [PluginLib.VersionAttribute(1, 12)]
    class DateAddTransform : PluginLib.IPlugin
    {
        public string Name
        {
            get { return "Добавить дату"; }
        }

        public string Author
        {
            get { return "Me"; }
        }

         
        public override string ToString()
        {
            return Name + ", " + Author;
        }

        public void Transform(Bitmap bitmap)
        {
            string stamp;

            stamp = DateTime.Now.ToString("dd/mm/yyyy");

            Graphics g = Graphics.FromImage(bitmap);
            g.FillRectangle(Brushes.Black, bitmap.Width-102, bitmap.Height-20, bitmap.Width, bitmap.Height);

            g.DrawString(stamp, new Font("Arial", 14f), Brushes.White, bitmap.Width - 102, bitmap.Height - 20);
        }
    }
}
