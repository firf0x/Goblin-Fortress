using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF_API.GFLogic.Components
{
    internal struct Vector3Byte
    {
        public byte x;
        public byte y;
        public byte z;

        public static Vector2Byte Zero = new Vector2Byte(0, 0);

        public Vector3Byte(byte x, byte y, byte z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public Vector3Byte WithX(byte x)
        {
            this = new Vector3Byte(x, y, z);
            return this;
        }
        public Vector3Byte WithY(byte y)
        {
            this = new Vector3Byte(x, y, z);
            return this;
        }
        public Vector3Byte WithZ(byte z)
        {
            this = new Vector3Byte(x, y, z);
            return this;
        }
    }
}
