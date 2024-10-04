using SDL2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFGraphics.Compoents
{
    public struct Texture2D : IDisposable
    {
        public string Name { get; set; }
        public string FileName { get; }
        private IntPtr _texture;

        public Texture2D(string filePath, IntPtr renderer)
        {
            LoadTextureFromFile(filePath, renderer);
            Name = "Texture2D";
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
