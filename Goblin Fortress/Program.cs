using GFGraphics;
using Goblin_Fortress.Graphics;
using System;


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
                game.Width = 640;
                game.Height = 640;
             
                render = new Render(game);

                game.Create();
            }
        }
    
    }

}