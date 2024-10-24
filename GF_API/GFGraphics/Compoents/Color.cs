using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GF_API.GFGraphics.Compoents
{
    public struct Color
    {
        public byte r, g, b, a;
        public Color()
        {
            r = 255;
            g = 255;
            b = 255;
            a = 255;
        }
        public Color(byte r, byte g, byte b, byte a)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            this.a = a;
        }
        
        public static int GetSize()
        {
            return Marshal.SizeOf(typeof(Color));
        }
    }
}
