using GF_API.GFGraphics.Compoents;
using GF_API.GFGraphics.Graphics.RenderViewConsole.RenderBeginEnd;
using GF_API.GFWindow;
using GF_API.Logger;
using SDL2;

namespace GF_API.GFGraphics.Graphics.RenderViewConsole
{
    /// <summary>
    /// Render View Console 
    /// </summary>
    public class RVC : IDisposable
    {
        private static GameWindow _window;
        public static IntPtr renderer { get; private set; }

        internal static bool _isReadCommand = false;

        private static int pointsCapacity = 1024;
        private static int pointIndex = 0;
        private static PrimitiveType Type = PrimitiveType.None;

        private static RBE rbe;

        private static Texture2D renderTexture;

        public RVC(GameWindow window)
        {
            _window = window;
            renderer = SDL.SDL_CreateRenderer(_window.WindowPtr, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_TARGETTEXTURE);
        }

        /// <summary>
        /// Creates a list of points that need to be drawn with a certain type.
        /// </summary>
        public static void Begin(PrimitiveType type)
        {
            if (Type != PrimitiveType.None) return;

            Type = type;

            rbe.points = new SDL.SDL_Point[pointsCapacity];
            pointIndex = 0;

        }

        /// <summary>
        /// Creates a list of points that need to be drawn with a certain type also supports DisplayList.
        /// </summary>
        public static void Begin(PrimitiveType type, int displayIndex)
        {
            if (Type != PrimitiveType.None) return;
            if (_isReadCommand == true)
            {
                if(DisplayList._displays.TryGetValue((uint)displayIndex, out DisplayList display))
                {
                    display.RenderBlocks.Add(() =>
                    {
                        Type = type;

                        rbe.points = new SDL.SDL_Point[pointsCapacity];
                        pointIndex = 0;
                    });
                }
                return;
            }

            Type = type;

            rbe.points = new SDL.SDL_Point[pointsCapacity];
            pointIndex = 0;

        }

        public static void PointSize()
        {
            // TODO: I need to think about how to implement this.
        }

        /// <summary>
        /// Point color
        /// </summary>
        public static void VertexColor(Color color)
        {
            SDL.SDL_SetRenderDrawColor(renderer, color.r, color.g, color.b, color.a);
        }
        /// <summary>
        /// Point color also supports DisplayList.
        /// </summary> 
        public static void VertexColor(Color color, int displayIndex)
        {
            if(_isReadCommand == true)
            {
                if (DisplayList._displays.TryGetValue((uint)displayIndex, out DisplayList display))
                {
                    display.RenderBlocks.Add(() =>
                    {
                        SDL.SDL_SetRenderDrawColor(renderer, color.r, color.g, color.b, color.a);
                    });
                }
                return;
            }
            SDL.SDL_SetRenderDrawColor(renderer, color.r, color.g, color.b, color.a);
        }

        /// <summary>
        /// 2D point
        /// </summary>
        public static void Vertex2(float x, float y)
        {
            if (pointIndex >= pointsCapacity)
            {
                // Let's allocate another array of points
                pointsCapacity *= 2;
                SDL.SDL_Point[] newPoints = new SDL.SDL_Point[pointsCapacity];
                Array.Copy(rbe.points, newPoints, rbe.points.Length);
                rbe.points = newPoints;
            }

            rbe.points[pointIndex].x = (int)x;
            rbe.points[pointIndex].y = (int)y;
            pointIndex++;
        }

        /// <summary>
        /// 2D point also supports DisplayList.
        /// </summary>
        public static void Vertex2(float x, float y, int displayIndex)
        {
            if (_isReadCommand == true)
            {
                if (DisplayList._displays.TryGetValue((uint)displayIndex, out DisplayList display))
                {
                    display.RenderBlocks.Add(() =>
                    {
                        if (pointIndex >= pointsCapacity)
                        {
                            // Let's allocate another array of points
                            pointsCapacity *= 2;
                            SDL.SDL_Point[] newPoints = new SDL.SDL_Point[pointsCapacity];
                            Array.Copy(rbe.points, newPoints, rbe.points.Length);
                            rbe.points = newPoints;
                        }

                        rbe.points[pointIndex].x = (int)x;
                        rbe.points[pointIndex].y = (int)y;
                        pointIndex++;
                    });
                }
                return;
            }
            if (pointIndex >= pointsCapacity)
            {
                // Let's allocate another array of points
                pointsCapacity *= 2;
                SDL.SDL_Point[] newPoints = new SDL.SDL_Point[pointsCapacity];
                Array.Copy(rbe.points, newPoints, rbe.points.Length);
                rbe.points = newPoints;
            }

            rbe.points[pointIndex].x = (int)x;
            rbe.points[pointIndex].y = (int)y;
            pointIndex++;
        }

        // TODO: Finish all types of rendering. End
        /// <summary>
        /// Start drawing all points. 
        /// </summary>
        public static void End()
        {

            switch (Type)
            {
                case PrimitiveType.Points:
                    SDL.SDL_RenderDrawPoints(renderer, rbe.points, rbe.points.Length);
                    break;

                case PrimitiveType.Lines:
                    if (pointIndex >= 3) pointIndex = 2;
                    SDL.SDL_RenderDrawLines(renderer, rbe.points, pointIndex);
                    break;

                case PrimitiveType.LineStrip:
                    SDL.SDL_RenderDrawLines(renderer, rbe.points, pointIndex);
                    break;

                case PrimitiveType.LineLoop:
                    rbe.points[pointIndex] = rbe.points[0];
                    pointIndex++;
                    SDL.SDL_RenderDrawLines(renderer, rbe.points, pointIndex);
                    break;
                    
                case PrimitiveType.Quads:
                    if (pointIndex < 4) return;
                    for (int i = 0; i < pointIndex; i += 4)
                    {
                        SDL.SDL_Rect rect = new SDL.SDL_Rect();

                        rect.x = rbe.points[i].x;
                        rect.y = rbe.points[i].y;
                        rect.w = rbe.points[i + 2].x - rbe.points[i].x;
                        rect.h = rbe.points[i + 2].y - rbe.points[i].y;
                        SDL.SDL_RenderFillRect(renderer, ref rect);
                    }
                    break;

                    //TODO: (?) QuadsTexture
                case PrimitiveType.QuadsTexture:
                    if (pointIndex < 4) return;

                    for(int i = 0; i < pointIndex; i += 4)
                    {
                        SDL.SDL_Rect rect = new SDL.SDL_Rect();
                        
                        rect.x = rbe.points[i].x;
                        rect.y = rbe.points[i].y;
                        rect.w = rbe.points[i + 2].x - rbe.points[i].x;
                        rect.h = rbe.points[i + 2].y - rbe.points[i].y;

                        RenderTexture(ref renderTexture, ref rect);
                    }

                    break;
                case PrimitiveType.QuadsStrip:

                    break;

                case PrimitiveType.Polygon:

                    break;

                default:

                    break;
            }
            Type = PrimitiveType.None;
        }
        
        /// <summary>
        /// Start drawing all points also supports DisplayList. 
        /// </summary>
        public static void End(int displayIndex)
        {
            if (_isReadCommand == true)
            {
                if (DisplayList._displays.TryGetValue((uint)displayIndex, out DisplayList display))
                {
                    display.RenderBlocks.Add(() =>
                    {
                        switch (Type)
                        {
                            case PrimitiveType.Points:
                                SDL.SDL_RenderDrawPoints(renderer, rbe.points, rbe.points.Length);
                                break;

                            case PrimitiveType.Lines:
                                if (pointIndex >= 3) pointIndex = 2;
                                SDL.SDL_RenderDrawLines(renderer, rbe.points, pointIndex);
                                break;

                            case PrimitiveType.LineStrip:
                                SDL.SDL_RenderDrawLines(renderer, rbe.points, pointIndex);
                                break;

                            case PrimitiveType.LineLoop:
                                rbe.points[pointIndex] = rbe.points[0];
                                pointIndex++;
                                SDL.SDL_RenderDrawLines(renderer, rbe.points, pointIndex);
                                break;

                            case PrimitiveType.Quads:
                                if (pointIndex < 4) return;
                                
                                for (int i = 0; i < pointIndex; i += 4)
                                {
                                    SDL.SDL_Rect rect = new SDL.SDL_Rect();

                                    rect.x = rbe.points[i].x;
                                    rect.y = rbe.points[i].y;
                                    rect.w = rbe.points[i + 2].x - rbe.points[i].x;
                                    rect.h = rbe.points[i + 2].y - rbe.points[i].y;
                                    SDL.SDL_RenderFillRect(renderer, ref rect);
                                }
                                break;

                            case PrimitiveType.QuadsStrip:

                                break;

                            case PrimitiveType.Polygon:

                                break;

                            default:

                                break;
                        }
                        Type = PrimitiveType.None;
                    });
                }
                return;
            }
        }
        
        public static void RenderTexture(ref Texture2D texture, ref SDL.SDL_Rect rect)
        {
            if (SDL.SDL_SetTextureBlendMode(texture.Handle, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND) != 0)
            {
                Debug.LogWarn("Failed to set texture blend mode");
            }

            //if (SDL.SDL_SetRenderDrawColor(renderer, texture.Color.r, texture.Color.g, texture.Color.b, texture.Color.a) != 0)
            //{
            //    Debug.LogWarn("Failed to set render draw color");
            //}

            if (SDL.SDL_RenderCopy(renderer, texture.Handle, ref rect, ref rect) != 0)
            {
                Debug.LogWarn("Failed to render copy");
            }
            renderTexture = texture;
        }

        public static void RenderTexture(Texture2D texture, SDL.SDL_Rect rect, int displayIndex)
        {
            if (_isReadCommand == true)
            {
                if (DisplayList._displays.TryGetValue((uint)displayIndex, out DisplayList display))
                {
                    display.RenderBlocks.Add(() =>
                    {
                        if (SDL.SDL_SetTextureBlendMode(texture.Handle, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND) != 0)
                        {
                            Debug.LogWarn("Failed to set texture blend mode");
                        }

                        /*if (SDL.SDL_SetRenderDrawColor(renderer, texture.Color.r, texture.Color.g, texture.Color.b, texture.Color.a) != 0)
                        {
                            Debug.LogWarn("Failed to set render draw color");
                        }
                        if(SDL.SDL_SetTextureColorMod(texture.Handle, texture.Color.r, texture.Color.g, texture.Color.b) != 0)
                        {
                            Debug.LogWarn("Failed to set texture color mod");
                        }*/

                        if (SDL.SDL_RenderCopyEx(renderer, texture.Handle, IntPtr.Zero, ref rect, 0, IntPtr.Zero, SDL.SDL_RendererFlip.SDL_FLIP_NONE) != 0)
                        {
                            Debug.LogWarn("Failed to render copy");
                        }
                        renderTexture = texture;
                    });
                }
            }
        }
        /// <summary>
        /// Paints the window in the color of your choice.
        /// </summary>
        /// <param name="color"></param>
        public static void ClearColor(Color color)
        { 
            SDL.SDL_SetRenderDrawColor(renderer, color.r, color.g, color.b, color.a);
            SDL.SDL_Rect rect = new SDL.SDL_Rect();
            rect.h = _window.Height;
            rect.w = _window.Width;
            SDL.SDL_RenderFillRect(renderer, ref rect);
        }

        /// <summary>
        /// Clear window(color).
        /// </summary>
        public static void SwapBuffers() => SDL.SDL_RenderPresent(renderer);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (renderer != IntPtr.Zero)
                {
                    SDL.SDL_DestroyRenderer(renderer);
                    renderer = IntPtr.Zero;
                }
                if (rbe.points != null)
                {
                    rbe.points = null;
                }
            }
        }

        ~RVC()
        {
            Dispose();
        }
    }
}
