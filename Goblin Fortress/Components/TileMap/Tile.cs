using GF_API.GFGraphics.Compoents;
using System.Runtime.InteropServices;
using System.Buffers;
using GF_API.GFLogic.Components;

namespace Goblin_Fortress.Components.TileMap
{
    // Size ~ 8 byte. 
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Tile
    {
        public GameObject gameObject { get; set; }

        /// <summary>
        /// Get the size(byte) of one tile.
        /// </summary>
        public readonly int GetSize()
        {
            return Marshal.SizeOf(typeof(Tile));
        }
    }
}