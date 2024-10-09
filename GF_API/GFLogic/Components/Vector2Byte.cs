using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF_API.GFLogic.Components
{
    public struct Vector2Byte
    {
        public byte x; 
        public byte y;

        public static Vector2Byte Zero = new Vector2Byte(0, 0);

        public Vector2Byte(byte x, byte y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2Byte WithX(byte x)
        {
            this = new Vector2Byte(x, y);
            return this;
        }
        public Vector2Byte WithY(byte y)
        {
            this = new Vector2Byte(x, y);
            return this;
        }
    }
}
