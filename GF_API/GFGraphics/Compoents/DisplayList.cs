using GF_API.GFGraphics.Graphics.RenderViewConsole;
using GF_API.Logger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GF_API.GFGraphics.Compoents
{
    /// <summary>
    /// The display list collects all render requests and outputs them in one call.
    /// </summary>
    public class DisplayList
    {
        public static Dictionary<uint, DisplayList> _displays { get; private set; } = new Dictionary<uint, DisplayList>();
        public List<Action> RenderBlocks { get; private set; } = new List<Action>();

        private static bool _activate;

        private static List<uint> _diplaysIndex = new List<uint>();

        public static uint GenList()
        {
            if (_diplaysIndex.Count == 0)
            {
                _diplaysIndex.Add(1);
                return 1;
            }

            for (int i = 0; i < _diplaysIndex.Count; i++)
            {
                if (_diplaysIndex.Contains(_diplaysIndex[i])) return _diplaysIndex.Last()+1;
            }
            return 0;
        }

        public static int NewList(uint listId, ListMode mode)
        {
            if (mode == ListMode.Compile)
            {
                if (!_displays.ContainsKey(listId))
                {
                    _displays[listId] = new DisplayList();
                    _activate = true;
                }
            }
            else if (mode == ListMode.RunTime)
            {
                _activate = false;
            }
            RVC._isReadCommand = _activate;
            return (int)listId;
        }

        public static void EndList()
        {
            RVC._isReadCommand = _activate;
        }

        public static void CallLists()
        {
            foreach (var display in _displays)
            {
                for (int i = 0; i < display.Value.RenderBlocks.Count; i++)
                {
                    display.Value.RenderBlocks[i]?.Invoke();
                }
                display.Value.RenderBlocks.Clear();
            }
        }
        public enum ListMode
        {
            Compile,
            RunTime
        }
    }
}
