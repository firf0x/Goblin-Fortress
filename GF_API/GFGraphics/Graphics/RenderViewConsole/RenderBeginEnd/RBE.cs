using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GF_API.GFGraphics.Compoents;
using System.Runtime.InteropServices;

namespace GF_API.GFGraphics.Graphics.RenderViewConsole.RenderBeginEnd
{
    /// <summary>
    /// Render Begin, End
    /// </summary>
    
    // Size ~ 8 byte.
    internal struct RBE
    {
        public SDL.SDL_Point[] points;

        public int GetSize() => Marshal.SizeOf(typeof(RBE));
    }
}
