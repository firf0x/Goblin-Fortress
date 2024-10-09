using GF_API.GFGraphics.Compoents;
using System.Runtime.InteropServices;

namespace Goblin_Fortress.Components.TileMap
{
    public struct Tile
    {
        public Texture2D texture { get; set; }
        public Transform transform { get; set; }
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
