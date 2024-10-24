using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Goblin_Fortress.Components
{
    public abstract class Component
    {
        public bool enabled;
        protected GameObject parent;
        public abstract void Init();
        public abstract void Update();
        public abstract void Destroy();
    }
}
