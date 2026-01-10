using HarmonyLib;
using InteractiveTerminalAPI.UI.Cursor;
using InteractiveTerminalAPI.UI.Screen;
using System;
using System.Linq;

namespace InteractiveTerminalAPI.UI.Page
{
    public class PageCursorElement<T> : PageElement where T : CursorElement
    {
        public BaseCursorMenu<T>[] cursorMenus;
        public BaseCursorMenu<T> GetCurrentCursorMenu()
        {
            return cursorMenus[pageIndex];
        }

        public void ChangeSorting()
        {
            cursorMenus.Do((x) => x.ChangeSorting());
            T[] allElements = cursorMenus.SelectMany(menu => menu.elements).ToArray();

            Func<T, T, int> sortFunction = cursorMenus[0].GetCurrentSorting();

            Array.Sort(allElements, (x,y) => sortFunction(x,y));

            int currentIndex = 0;

            foreach (var cursorMenu in cursorMenus)
            {
                int length = cursorMenu.elements.Length;

                Array.Copy(allElements, currentIndex, cursorMenu.elements, 0, length);

                currentIndex += length;
            }
        }
        public static PageCursorElement<T> Create(int startingPageIndex = 0, IScreen[] elements = default, BaseCursorMenu<T>[] cursorMenus = default)
        {
            return new PageCursorElement<T>()
            {
                pageIndex = startingPageIndex,
                elements = elements,
                cursorMenus = cursorMenus
            };
        }
    }
}
