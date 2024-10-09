using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goblin_Fortress.Components
{
    public abstract class GameObject
    {
        public string name { get; }
        public GameObject(string name) { this.name = name; }
        public abstract void Init();
        public abstract void Destroy();
    }
}
