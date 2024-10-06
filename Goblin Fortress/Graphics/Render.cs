using GF_API.GFGraphics;
using GF_API.GFGraphics.Compoents;
using GF_API.GFGraphics.Graphics.RenderViewConsole;
using GF_API.GFInput;
using GF_API.Logger;
using GF_API.GFWindow;
using System.Drawing;

namespace Goblin_Fortress.Graphics
{
    internal class Render : IDisposable
    {
        private GameWindow _window;
        private int angle = 0;

        private Point position;

        private int DisplayIndex;
        internal Render(GameWindow window) 
        {
            _window = window;
            window.RenderFrame += OnRenderFrame;
            window.UpdateFrame += OnUpdate;

            window.OnLoad += OnLoad;
            window.OnUnload += OnUnload;
        }


        private void OnLoad()
        {
            
        }

        private void OnUnload()
        {

        }

        private void OnUpdate()
        {
            if (Input.GetKeyDown(Input.KeyCode.W))
            {
                Debug.Log("W", Debug.LogLevel.Warning);
            }
            if (Input.GetKeyUp(Input.KeyCode.Tab))
            {
                Debug.Log("Tab", Debug.LogLevel.Warning);
            }
            if (Input.GetKey(Input.KeyCode.Q))
            {
                Debug.Log("Q", Debug.LogLevel.Warning);
            }
            
        }

        private void OnRenderFrame()
        {
            RVC.ClearColor(Color.Black);

            // Create List
            DisplayIndex = DisplayList.NewList(1, DisplayList.ListMode.Compile);

            // Begin
            RVC.Begin(PrimitiveType.Points);

            RVC.VertexColor(Color.White);

            float x = (float)Math.Sin(360) * 10;
            float y = (float)Math.Cos(360) * 10;
            
            RVC.Vertex2(_window.Width / 2 + x, _window.Height / 2 + y);

            // End
            RVC.End();

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
        }

    }
}
