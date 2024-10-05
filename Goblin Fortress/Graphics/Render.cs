using GFGraphics;
using GFGraphics.Compoents;
using GFGraphics.Graphics.RenderViewConsole;
using Goblin_Fortress.Logger;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Goblin_Fortress.Graphics
{
    internal class Render : IDisposable
    {
        private GameWindow _window;
        private int angle = 0;

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

            //Debug.Log("Update");
        }

        private void OnRenderFrame()
        {
            RVC.ClearColor(Color.Black);

            // Create List
            DisplayIndex = DisplayList.NewList(1, DisplayList.ListMode.Compile);


                // Begin
                RVC.Begin(PrimitiveType.LineLoop, DisplayIndex);
                
                RVC.VertexColor(Color.White, DisplayIndex);
                RVC.Vertex2(100, 100, DisplayIndex);
                RVC.Vertex2(600, 100, DisplayIndex);
                RVC.Vertex2(450, 300, DisplayIndex);
                // End
                RVC.End(DisplayIndex);

            // Render all render block 
            DisplayList.EndList();

            // Call all invoke 
            DisplayList.CallLists();

            RVC.SwapBuffers();
            
            angle += 1;

            if (angle > 360)
            {
                angle = 0;
            }
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
