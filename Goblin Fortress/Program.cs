using GF_API.GFWindow;
using Goblin_Fortress.Graphics;

namespace MainProgram
{
    public static class Program
    {
        internal static Render render;
        public static void Main(string[] args)
        {
            using (GameWindow game = new GameWindow())
            {
                game.Title = "Test Window";
                game.Width = 1280;
                game.Height = 720;
                game.Flags = SDL2.SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE;
             
                render = new Render(game);

                game.Create();
            }
        }
    }
}