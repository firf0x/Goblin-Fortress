using GF_API.GFLogic.Components;
using GF_API.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Goblin_Fortress.Components.TileMap
{
    public class TileMap
    {
        // 255 TileMap = 428.4 Mb
        public Tile[,] Map { get; private set; }
        private byte _size;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public TileMap(byte sizeArray) 
        {
            _size = sizeArray;
            Map = new Tile[_size, _size];
        }
        /// <summary>
        /// Set a tile on a TileMap.
        /// </summary>
        public void SetTile(Tile tile, Vector2Byte vector)
        {
            Map[vector.x, vector.y] = tile;
        }

        /// <summary>
        /// Set the tiles on the TileMap.
        /// </summary>
        public void SetTiles(Tile[,] tiles)
        {
            Map = tiles;
        }

        // ~1.68 Mb || 1764001 byte
        public static int GetSize() => (40 * 210^2) + sizeof(byte);

        /// <summary>
        /// Get info.
        /// </summary>
        public override string ToString()
        {
            return $"Info TileMap" +
                   $"------------" +
                   $"Size map: {(int)_size}" +
                   $"Map: {Map}" +
                   $"------------";
        }
    }
}
