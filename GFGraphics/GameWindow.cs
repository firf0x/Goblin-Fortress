using GFGraphics.Graphics.RenderViewConsole;
using SDL2;

namespace GFGraphics
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

            WindowPtr = SDL.SDL_CreateWindow(Title, SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED, Width, Height, Flags);

            if(WindowPtr == IntPtr.Zero)
            {
                Debug.LogError($"SDL_CreateWindow failed: {SDL.SDL_GetError()}");
            }
            
            // Create RVC
            RVC rvc = new RVC(this);

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

                // Logic while
                while (isUpdate)
                {
                    UpdateFrame?.Invoke();
                    SDL.SDL_Delay(1);
                    isUpdate = false;
                }

                while (isRender)
                {
                    // framerate:
                    // 16 = 60.0fps
                    // 33 ~ 30.0fps
                    RenderFrame?.Invoke();
                    SDL.SDL_Delay(16);
                    isRender = false;
                }

                isUpdate = !isUpdate;
                isRender = !isRender;
            }
            
            Dispose();
            rvc.Dispose();

            OnUnload?.Invoke();
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
