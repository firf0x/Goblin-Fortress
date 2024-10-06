using GF_API.GFGraphics.Graphics.RenderViewConsole;
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
        internal static Dictionary<uint, DisplayList> _displays = new Dictionary<uint, DisplayList>();
        internal List<Action> RenderBlocks = new List<Action>();

        private static bool _activate;

        private static List<uint> _diplaysIndex = new List<uint>();

        public static int NewList(uint listId, ListMode mode)
        {
            if (mode == ListMode.Compile)
            {
                if (!_displays.ContainsKey(listId))
                {
                    _displays[listId] = new DisplayList();
                }
                _activate = true;
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
                foreach(var item in display.Value.RenderBlocks)
                {
                    item.Invoke();
                }
            }
        }
        public enum ListMode
        {
            Compile,
            RunTime
        }
    }
}
