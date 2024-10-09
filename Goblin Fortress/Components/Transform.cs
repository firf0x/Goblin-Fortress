using GF_API.GFLogic.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goblin_Fortress.Components
{
    public class Transform
    {
        public Vector2 position { get; set; }
        public Vector2 Scale { get; set; }

        public Transform(Vector2 newPos)
        {
            this.position = newPos;
        }
    }
}
