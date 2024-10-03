using GFGraphics;
using Goblin_Fortress.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goblin_Fortress.Graphics
{
    internal class Render : IDisposable
    {
        private GameWindow _window;
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
            Debug.Log("Update");
        }

        private void OnRenderFrame()
        {
            //Debug.Log("Render Update");
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
