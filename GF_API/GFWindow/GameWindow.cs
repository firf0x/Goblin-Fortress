using SDL2;
using GF_API.GFGraphics.Graphics.RenderViewConsole;
using GF_API.Logger;
using System.ComponentModel;

namespace GF_API.GFWindow
{
    public class GameWindow : IDisposable
    {
        public string Title;

        public IntPtr WindowPtr;
        public int Width;
        public int Height;

        public SDL.SDL_WindowFlags Flags;

        public event Action UpdateFrame;

        public event Action RenderFrame;

        public event Action OnLoad;
        public event Action OnUnload;

        private bool exitRequested = false;
        private bool isUpdate = true;
        private bool isRender = true;

        private BackgroundWorker _renderWorker;
        
        public void Create()
        {
            _renderWorker = new BackgroundWorker();
            _renderWorker.DoWork += RenderLoop;

            // Title != Empty (null)
            if (string.IsNullOrEmpty(Title))
            {
                Debug.LogError("Title is not set");
                return;
            }

            // Width&Height <= 0
            if (Width <= 0 || Height <= 0)
            {
                Debug.LogError("Width and Height must be greater than 0");
                return;
            }

            // Init
            if (SDL.SDL_Init(SDL.SDL_INIT_VIDEO) != 0)
            {
                Debug.LogError($"SDL_Init failed: {SDL.SDL_GetError()}");
                return;
            }

            WindowPtr = SDL.SDL_CreateWindow(Title, SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, Width, Height, Flags);

            if (WindowPtr == IntPtr.Zero)
            {
                Debug.LogError($"SDL_CreateWindow failed: {SDL.SDL_GetError()}");
            }

            // Create RVC
            RVC rvc = new RVC(this);

            // Main loop
            bool run = true;

            OnLoad?.Invoke();

            _renderWorker.RunWorkerAsync();

            while (run)
            {
                SDL.SDL_Event e;
                while (SDL.SDL_PollEvent(out e) != 0)
                {
                    if (e.type == SDL.SDL_EventType.SDL_QUIT)
                    {
                        exitRequested = true;
                        run = false;
                    }
                }

                if (exitRequested) break;

                // Logic while
                while (isUpdate)
                {
                    UpdateFrame?.Invoke();
                    SDL.SDL_Delay(16);
                    isUpdate = false;
                }

                isUpdate = !isUpdate;
            }
            
            isUpdate = false;
            isRender = false;
            
            Dispose();
            rvc.Dispose();

            OnUnload?.Invoke();
        }

        private void RenderLoop(object sender, DoWorkEventArgs e)
        {
            while (isRender)
            {
                // framerate:
                // 16 = 60.0fps
                // 33 ~ 30.0fps
                RenderFrame?.Invoke();
                SDL.SDL_Delay(16);
            }
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (WindowPtr != IntPtr.Zero & _renderWorker != null)
            {
                SDL.SDL_DestroyWindow(WindowPtr);
                WindowPtr = IntPtr.Zero;
                _renderWorker.Dispose();
            }

            SDL.SDL_Quit();
            GC.Collect();
        }
    }
}
