using System;

namespace Transforms
{
    [PluginLib.VersionAttribute(1, 0)]
    public class ReverseTransform : PluginLib.IPlugin
    {
        public string Name
        {
            get{ return "Переворот изображения";}
        }

        public string Author
        {
            get{return "Me";}
        }

        public void Transform(System.Drawing.Bitmap bitmap)
        {
            for (int i = 0; i < bitmap.Width; ++i)
                for (int j = 0; j < bitmap.Height / 2; ++j)
                {
                    System.Drawing.Color color = bitmap.GetPixel(i, j);
                    bitmap.SetPixel(i, j, bitmap.GetPixel(i, bitmap.Height - j - 1));
                    bitmap.SetPixel(i, bitmap.Height - j - 1, color);
                }
        }

        public string ToString()
        {
            return Name + ", " + Author;
        }

    }
}
