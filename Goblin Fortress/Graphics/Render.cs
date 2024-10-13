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
        internal Render(GameWindow window) 
        {
            _window = window;
            window.RenderFrame += OnRenderFrame;
            window.UpdateFrame += OnUpdate;

            window.OnLoad += OnLoad;
            window.OnUnload += OnUnload;
        }

        private SDL2.SDL.SDL_Rect rect;
        private void OnLoad()
        {
            DisplayIndex = DisplayList.NewList(1, DisplayList.ListMode.Compile);

            _texture = new Texture2D("Texture\\TileMap.bmp", RVC.renderer);
            rect = new SDL2.SDL.SDL_Rect();

            TileMap[] tileMaps = new TileMap[255];
            
            Tile tile = new Tile();
            
            GameObject gameObject = new Block();
            gameObject.Init("Test Block", new GF_API.GFLogic.Components.Vector2Byte(0,0), _texture);

            tile.gameObject = gameObject;

            for (int i = 0; i < 255; i++)
            {
                tileMaps[i] = new TileMap(210);
                for (byte y = 0; y < 210; y++)
                {
                    for (byte x = 0; x < 210; x++)
                    {
                        tileMaps[i].SetTile(tile, new GF_API.GFLogic.Components.Vector2Byte(x, y));
                    }
                }
            }
            Debug.LogWarn("Ready!");
            long memory = GC.GetTotalMemory(true);
            Debug.LogWarn($"{memory}");
        }

        float x;
        float y;
        float w; 
        float h;

        private void OnUnload()
        {

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
        }

        
        private void OnRenderFrame()
        {
            RVC.ClearColor(new Color(255,255,255,255));

            // Create List

            // Begin
            RVC.Begin(PrimitiveType.Points, DisplayIndex);

            
            RVC.VertexColor(new Color(0,0,0,255), DisplayIndex);

            //float x = (float)Math.Sin(angle) * 10;
            //float y = (float)Math.Cos(angle) * angle;
            //RVC.Vertex2(_window.Width / 2 + x, _window.Height / 2 + y);

            //_texture.Color.r = 255;
            //_texture.Color.g = 255;
            //_texture.Color.b = 0;
            //_texture.Color.a = 255;

            rect.x = (int)x;
            rect.y = (int)y;
            rect.h = (int)h;
            rect.w = (int)w;
            RVC.RenderTexture(_texture, rect, DisplayIndex);
            // End
            RVC.End(DisplayIndex);

            // Render all render block 
            DisplayList.EndList();

            // Call all invoke 
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
