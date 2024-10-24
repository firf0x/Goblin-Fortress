using GF_API.GFGraphics.Compoents;
using GF_API.GFGraphics.Graphics.RenderViewConsole;
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
        private const string ERROR_SIZE_EXCEEDS_ARRAY_LIMIT = "The size exceeds the allowed size of the array";

        private const string ERROR_NOT_FOUND_RENDER_INDEX = "Display not found for render index";

        // 255 TileMap ~ 110 Mb
        public Tile[,] Map { get; private set; }
        private byte _sizeArray;
        private byte _sizeTile;
        private int _renderIndex;
        private SDL2.SDL.SDL_Rect _rect;
        /// <summary>
        /// Constructor
        /// </summary>
        public TileMap(byte sizeArray, byte sizeTile) 
        {
            _renderIndex = DisplayList.NewList(DisplayList.GenList(), DisplayList.ListMode.Compile);
            
            _sizeArray = sizeArray;
            _sizeTile = sizeTile;
            
            Map = new Tile[_sizeArray, _sizeArray];
            
            _rect.h = sizeTile;
            _rect.w = sizeTile;
        }
        /// <summary>
        /// Set a tile on a TileMap.
        /// </summary>
        public void SetTile(Tile tile, Vector2Byte vector)
        {
            Map[(int)vector.x, (int)vector.y] = tile;
        }

        /// <summary>
        /// Set the tiles on the TileMap.
        /// </summary>
        public void SetTiles(Tile[,] tiles)
        {
            if (tiles.Length > _sizeArray * _sizeArray)
            {
                Debug.LogError($"Error TileMap : {ERROR_SIZE_EXCEEDS_ARRAY_LIMIT}");
                return;
            }

            else Map = tiles;
        }
        /// <summary>
        /// Render Maps.
        /// </summary>
        public void RenderMap()
        {
            if (DisplayList._displays.TryGetValue((uint)_renderIndex, out DisplayList display))
            {
                display.RenderBlocks.Add(Render);
            }
            else Debug.LogError($"Error TileMap : {ERROR_NOT_FOUND_RENDER_INDEX} => {_renderIndex}");
            DisplayList.EndList();
        }

        private void Render()
        {
            for (int x = 0; x < _sizeArray; x++)
            {
                _rect.x = x * _sizeTile;
                for (int y = 0; y < _sizeArray; y++)
                {
                    _rect.y = y * _sizeTile;
                    if (SDL2.SDL.SDL_SetTextureBlendMode(Map[x, y].gameObject.texture.Handle, SDL2.SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND) != 0)
                    {
                        Debug.LogWarn("Failed to set texture blend mode");
                    }

                    if (SDL2.SDL.SDL_SetRenderDrawColor(RVC.renderer, Map[x, y].gameObject.texture.color.r, Map[x, y].gameObject.texture.color.g, Map[x, y].gameObject.texture.color.b, Map[x, y].gameObject.texture.color.a) != 0)
                    {
                        Debug.LogWarn("Failed to set render draw color");
                    }

                    if (SDL2.SDL.SDL_SetTextureAlphaMod(Map[x, y].gameObject.texture.Handle, Map[x, y].gameObject.texture.color.a) != 0)
                    {
                        Debug.LogWarn("Failed to set render draw alpha mod");
                    }

                    if (SDL2.SDL.SDL_SetTextureColorMod(Map[x, y].gameObject.texture.Handle, Map[x, y].gameObject.texture.color.r, Map[x, y].gameObject.texture.color.g, Map[x, y].gameObject.texture.color.b) != 0)
                    {
                        Debug.LogWarn("Failed to set texture color mod");
                    }

                    if (SDL2.SDL.SDL_RenderCopyEx(RVC.renderer, Map[x, y].gameObject.texture.Handle, IntPtr.Zero, ref _rect, 0, IntPtr.Zero, SDL2.SDL.SDL_RendererFlip.SDL_FLIP_NONE) != 0)
                    {
                        Debug.LogWarn("Failed to render copy");
                    }
                }
            }
        }

        // ~344 Kb || 352800 byte
        public static int GetSize() => (8 * 210^2) + sizeof(byte);

        /// <summary>
        /// Get info.
        /// </summary>
        public override string ToString()
        {
            return $"Info TileMap\n" +
                   $"------------\n" +
                   $"Size map: {_sizeArray}\n" +
                   $"Size Tile: {_sizeTile}\n" +
                   $"Map: {Map}\n" +
                   $"------------\n";
        }
    }
}
