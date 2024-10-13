using GF_API.GFGraphics.Compoents;
using GF_API.GFLogic.Components;
using Goblin_Fortress.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goblin_Fortress.Assets.Scripts
{
    public class Block : GameObject
    {
        public override void Init(string Name, Vector2Byte Position, Texture2D Texture)
        {
            name = Name;
            position = Position;
            texture = Texture;
        }

        public void TickUpdate()
        {
            
        }

        public override void Destroy()
        {

        }
    }
}
