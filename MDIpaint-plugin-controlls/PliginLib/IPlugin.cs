using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace PluginLib
{
    
    public interface IPlugin
    {
        string Name { get; }
        string Author { get; }
        string ToString();
        void Transform(System.Drawing.Bitmap app);

    }
}
