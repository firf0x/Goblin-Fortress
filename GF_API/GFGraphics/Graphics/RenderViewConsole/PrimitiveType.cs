using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF_API.GFGraphics.Graphics.RenderViewConsole
{
    /// <summary>
    /// Type of figure.
    /// </summary>
    public enum PrimitiveType
    {
        // Test
        None,

        // Point
        Points,

        // Lines
        Lines,
        LineStrip,
        LineLoop,

        // Quads
        Quads,
        QuadsTexture,
        QuadsStrip,

        // Polygon
        Polygon
    }
}
