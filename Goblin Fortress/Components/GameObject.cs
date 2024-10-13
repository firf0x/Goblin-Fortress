using GF_API.GFGraphics.Compoents;
using GF_API.GFLogic.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Goblin_Fortress.Components
{
    // Size ~ 32 byte.
    public abstract class GameObject
    {
        public string name { get; protected set; }
        public Vector2Byte position { get; protected set; }
        public Texture2D texture { get; protected set; }

        public abstract void Init(string Name, Vector2Byte Position, Texture2D Texture);
        public abstract void Destroy();
    }
}
