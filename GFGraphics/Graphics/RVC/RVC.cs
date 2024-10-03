using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GFGraphics.Graphics.RVC;
using GFGraphics.Graphics.RVC.RenderBeginEnd;
using SDL2;

namespace GFGraphics.Graphics.RenderViewConsole
{
    /// <summary>
    /// Render View Console 
    /// </summary>
    public class RVC : IDisposable
    {
        private static GameWindow _window;
        private static IntPtr renderer;

        private static RBE rbe;
        private static int pointsCapacity = 1024;
        private static int pointIndex = 0;
        private static PrimitiveType Type = PrimitiveType.None;

        public RVC(GameWindow window)
        {
            _window = window;
            renderer = SDL.SDL_CreateRenderer(_window.WindowPtr, -1, SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
            rbe = new RBE();
        }


        public static void Begin(PrimitiveType type)
        {
            if (Type != PrimitiveType.None) return;

            Type = type;
            
            switch (type)
            {
                case PrimitiveType.Points:
                    rbe.points = new SDL.SDL_Point[pointsCapacity];
                    pointIndex = 0;
                    break;
                case PrimitiveType.Lines:
                    rbe.points = new SDL.SDL_Point[pointsCapacity];
                    break;
                case PrimitiveType.LineStrip:
                    break;
                case PrimitiveType.LineLoop:
                    rbe.points = new SDL.SDL_Point[pointsCapacity];
                    pointIndex = 0;
                    break;
                case PrimitiveType.Triangles:
                    break;
                case PrimitiveType.TrianglesStrip:
                    break;
                case PrimitiveType.TrianglesFan:
                    break;
                case PrimitiveType.Quads:
                    break;
                case PrimitiveType.QuadsStrip:
                    break;
                case PrimitiveType.Polygon:
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Point color
        /// </summary>
        public static void VertexColor(Color color)
        {
            SDL.SDL_SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// 2D point
        /// </summary>
        public static void Vertex2(int x, int y)
        {
            if (pointIndex >= pointsCapacity)
            {
                // Let's allocate another array of points
                pointsCapacity *= 2;
                SDL.SDL_Point[] newPoints = new SDL.SDL_Point[pointsCapacity];
                Array.Copy(rbe.points, newPoints, rbe.points.Length);
                rbe.points = newPoints;
            }

            rbe.points[pointIndex].x = x;
            rbe.points[pointIndex].y = y;
            pointIndex++;
        }

        // TODO: Complete three-dimensional points.
        /// <summary>
        /// Don't work ヾ(⌐■_■)ノ 
        /// </summary>
        public static void Vertex3(int x, int y, int z)
        {
            var point = new SDL.SDL_Point();
            point.x = x;
            point.y = y;
            //points.Add(point);
        }

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

                    break;

                case PrimitiveType.LineStrip:

                    break;

                case PrimitiveType.LineLoop:
                    rbe.points[pointIndex] = rbe.points[0];
                    pointIndex++;
                    SDL.SDL_RenderDrawLines(renderer, rbe.points, pointIndex);
                    break;

                case PrimitiveType.Triangles:
                    
                    break;

                case PrimitiveType.TrianglesStrip:

                    break;

                case PrimitiveType.TrianglesFan:

                    break;

                case PrimitiveType.Quads:

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
        /// Paints the window in the color of your choice.
        /// </summary>
        /// <param name="color"></param>
        public static void ClearColor(Color color)
        { 
            SDL.SDL_SetRenderDrawColor(renderer, color.R, color.G, color.B, color.A);
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
            }
        }

        ~RVC()
        {
            Dispose();
        }
    }
}
