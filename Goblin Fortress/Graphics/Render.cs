using GF_API.GFGraphics.Compoents;
using GF_API.GFGraphics.Graphics.RenderViewConsole;
using GF_API.GFInput;
using GF_API.GFWindow;
using GF_API.Logger;
using Goblin_Fortress.Assets.Scripts;
using Goblin_Fortress.Components;
using Goblin_Fortress.Components.TileMap;

namespace Goblin_Fortress.Graphics
{
    internal class Render : IDisposable
    {
        private GameWindow _window;
        private int angle = 0;

        private int DisplayIndex;
        private Texture2D _texture;

        private TileMap[] tileMaps;
        
        Tile tile;
        internal Render(GameWindow window) 
        {
            _window = window;
            window.OnLoad += OnLoad;

            window.RenderFrame += OnRenderFrame;
            window.UpdateFrame += OnUpdate;

            window.OnUnload += OnUnload;
        }

        private SDL2.SDL.SDL_Rect rect;
        private void OnLoad()
        {
            DisplayIndex = DisplayList.NewList(DisplayList.GenList(), DisplayList.ListMode.Compile);

            _texture = new Texture2D("Texture\\TileMap.bmp", RVC.renderer);
            _texture.color = new Color(255, 255, 255, 125);
            rect = new SDL2.SDL.SDL_Rect();

            tileMaps = new TileMap[255];
            
            tile = new Tile();

            GameObject gameObject = new Block();
            gameObject.Init("Test Block", new GF_API.GFLogic.Components.Vector2Byte(0, 0), _texture);
            var a = new GF_API.GFLogic.Components.Vector2Byte();
            tile.gameObject = gameObject;

            for (int i = 0; i < 255; i++)
            {
                tileMaps[i] = new TileMap(15, 10);
                for (byte y = 0; y < 15; y++)
                {
                    for (byte x = 0; x < 15; x++)
                    {
                        tileMaps[0].SetTile(tile, new GF_API.GFLogic.Components.Vector2Byte(x, y));
                    }
                }
            }
        }

        float x;
        float y;
        float w; 
        float h;

        private void OnUnload()
        {
            Dispose();
        }

        private void OnUpdate()
        {
            if (Input.GetKey(Input.KeyCode.W))
            {
                y -= 1;
            }
            if (Input.GetKey(Input.KeyCode.S))
            {
                y += 1;
            }
            if (Input.GetKey(Input.KeyCode.A))
            {
                x -= 1;
            }
            if (Input.GetKey(Input.KeyCode.D))
            {
                x += 1;
            }

            if (Input.GetKey(Input.KeyCode.Up))
            {
                h -= 1;
                //Debug.LogWarn(Input.KeyCode.Up.ToString());
            }
            if (Input.GetKey(Input.KeyCode.Down))
            {
                h += 1;
                //Debug.LogWarn(Input.KeyCode.Down.ToString());
            }

            if (Input.GetKey(Input.KeyCode.Right))
            {
                w += 1;
                //Debug.LogWarn(Input.KeyCode.Right.ToString());

            }
            if (Input.GetKey(Input.KeyCode.Left))
            {
                w -= 1;
                //Debug.LogWarn(Input.KeyCode.Left.ToString());
            }
            //Debug.Log((double)(SDL2.SDL.SDL_GetPerformanceCounter() / SDL2.SDL.SDL_GetPerformanceFrequency()));
            //Debug.Log($"\n{SDL2.SDL.SDL_GetCPUCount()} : ");
        }

        
        private void OnRenderFrame()
        {
            RVC.ClearColor(new Color(0,0,0,255));

            tileMaps[0].RenderMap();

            // Call aall invoke 
            DisplayList.CallLists();

            RVC.SwapBuffers();
        }

        public void Dispose()
        {
            _window.RenderFrame -= OnRenderFrame;
            _window.UpdateFrame -= OnUpdate;

            _window.OnLoad -= OnLoad;
            _window.OnUnload -= OnUnload;
            _texture.Dispose();
        }
    }
}
