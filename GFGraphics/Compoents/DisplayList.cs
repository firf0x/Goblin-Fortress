using GFGraphics.Graphics.RenderViewConsole;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GFGraphics.Compoents
{
    public class DisplayList
    {
        internal static List<DisplayList> displays = new List<DisplayList>();
        internal List<Action> RenderBlocks = new List<Action>();

        private static int displayIndex = -1;

        public static void NewList(int listId, ListMode mode)
        {
            displayIndex = listId;
            displays[displayIndex] = new DisplayList();
            RVC._isReadCommand = true;
        }

        public static void EndList()
        {
            var list = displays.ElementAt(displayIndex);
            foreach (var block in list.RenderBlocks)
            {
                block?.Invoke();
            }
            RVC._isReadCommand = false;
            list.RenderBlocks.Clear();
        }

        public enum ListMode
        {
            Compile
        }
    }
}
