using SDL2;

namespace GFGraphics
{
    public class GameWindow : IDisposable
    {
        public string Title;

        public IntPtr WindowPtr;
        public int Width;
        public int Height;

        public event Action UpdateFrame;

        public event Action RenderFrame;

        public event Action OnLoad;
        public event Action OnUnload;

        private bool exitRequested = false;

        public void Create()
        {
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
            
            if(SDL.SDL_Init(SDL.SDL_INIT_VIDEO) != 0)
            {
                Debug.LogError($"SDL_Init failed: {SDL.SDL_GetError()}");
                return;
            }

            WindowPtr = SDL.SDL_CreateWindow(Title, SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, Width, Height, SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if(WindowPtr == IntPtr.Zero)
            {
                Debug.LogError($"SDL_CreateWindow failed: {SDL.SDL_GetError()}");
            }

            // Main loop
            bool run = true;

            OnLoad?.Invoke();
            while (run)
            {
                SDL.SDL_Event e;
                while (SDL.SDL_PollEvent(out e) != 0)
                {
                    if(e.type == SDL.SDL_EventType.SDL_QUIT)
                    {
                        exitRequested = true;
                        run = false;
                    }
                }

                if (exitRequested) break;

                // Update and render
                UpdateFrame?.Invoke();
                RenderFrame?.Invoke();

                SDL.SDL_Delay(16);
            }
            OnUnload?.Invoke();
            Dispose();
        }


        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }


        protected virtual void Dispose(bool disposing)
        {
            if (WindowPtr != IntPtr.Zero)
            {
                SDL.SDL_DestroyWindow(WindowPtr);
                WindowPtr = IntPtr.Zero;
            }
            SDL.SDL_Quit();

        }
    }
}
