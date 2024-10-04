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
            DisplayList.NewList();

            // Begin
            RVC.Begin(PrimitiveType.LineLoop);



            // End
            RVC.End();

            // Render all render block
            DisplayList.EndList();
            
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
