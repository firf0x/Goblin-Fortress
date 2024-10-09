using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GF_API.Logger;
using GF_API.GFLogic.Components;
using System.Drawing;
using System.Runtime.InteropServices;

namespace GF_API.GFGraphics.Compoents
{
    // Size ~ 24 byte.
    public struct Texture2D : IDisposable
    {
        public readonly string Name;
        public readonly string FileName;
        public Color Color;
        private IntPtr _texture;
        private SDL.SDL_Rect _rect;

        public Texture2D(string filePath, IntPtr renderer)
        {
            LoadTextureFromFile(filePath, renderer);
            Name = "Texture2D";
            FileName = filePath;
        }
        public Texture2D(string name, string filePath, IntPtr renderer)
        {
            LoadTextureFromFile(filePath, renderer);
            Name = name;
            FileName = filePath;
        }

        private void LoadTextureFromFile(string path, IntPtr renderer)
        {
            LoadBMPTexture(path, renderer);
        }

        private void LoadBMPTexture(string path, IntPtr renderer)
        {
            IntPtr _surface = SDL.SDL_LoadBMP(path);
            if (_surface == IntPtr.Zero) Debug.LogWarn($"Failed to load bitmap: {SDL.SDL_GetError()}");

            _texture = SDL.SDL_CreateTextureFromSurface(renderer, _surface);
            if (_texture == IntPtr.Zero) Debug.LogWarn($"Failed to create texture: {SDL.SDL_GetError()}");

            SDL.SDL_FreeSurface(_surface);
        }
        public IntPtr Handle
        {
            get { return _texture; }
        }

        public void Dispose()
        {
            SDL.SDL_DestroyTexture(_texture);
        }
    }
}
