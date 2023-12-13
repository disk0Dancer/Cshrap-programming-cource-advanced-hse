using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Transforms
{
    [PluginLib.VersionAttribute(1, 1)]
    public class ContrastTransform : PluginLib.IPlugin
    {
        public string Name
        {
            get { return "Повысить контрастность"; }
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
            BitmapData sourceData = bitmap.LockBits(new Rectangle(0, 0,
                                bitmap.Width, bitmap.Height),
                                ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            bitmap.UnlockBits(sourceData);
            double b = 0, g = 0, r = 0;

            for (int k = 0; k + 4 < pixelBuffer.Length; k += 4)
            {
                b = ((((pixelBuffer[k] / 255.0) - 0.5) * 1.6) + 0.5) * 255.0;
                g = ((((pixelBuffer[k + 1] / 255.0) - 0.5) * 1.6) + 0.5) * 255.0;
                r = ((((pixelBuffer[k + 2] / 255.0) - 0.5) * 1.6) + 0.5) * 255.0;
                // I = (I • 100 — 128 • N) / (100 — N)


                if (b > 255)
                { b = 255; }
                else if (b < 0)
                { b = 0; }

                if (g > 255)
                { g = 255; }
                else if (g < 0)
                { g = 0; }

                if (r > 255)
                { r = 255; }
                else if (r < 0)
                { r = 0; }

                pixelBuffer[k] = (byte)b;
                pixelBuffer[k + 1] = (byte)g;
                pixelBuffer[k + 2] = (byte)r;
            }

            BitmapData resultData = bitmap.LockBits(
                new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
            Marshal.Copy(pixelBuffer, 0, resultData.Scan0, pixelBuffer.Length);
            bitmap.UnlockBits(resultData);
        }
    }
}
