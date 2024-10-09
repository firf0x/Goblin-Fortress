using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF_API.GFLogic.Components
{
    public struct Vector2
    {
        public int x;
        public int y;

        public static Vector2 Zero = new Vector2(0, 0);

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2 WithX(int x)
        {
            this = new Vector2(x, y);
            return this;
        }
        public Vector2 WithY(int y)
        {
            this = new Vector2(x, y);
            return this;
        }
    }
}
