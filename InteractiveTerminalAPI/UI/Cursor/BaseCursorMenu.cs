using System;

namespace InteractiveTerminalAPI.UI.Cursor
{
    public abstract class BaseCursorMenu<T> : ITextElement where T : CursorElement
    {
        public char cursorCharacter = '>';
        public int cursorIndex;
        public int sortingIndex;
        public T[] elements;
        public Func<T,T, int>[] SortingFunctions { get; set; }

        public T GetSelectedElement()
        {
            return elements[cursorIndex];
        }
        public void Execute()
        {
			GetSelectedElement().ExecuteAction();
        }
        public Func<T, T, int> GetCurrentSorting()
        {
            return SortingFunctions[sortingIndex];
        }
        public void ChangeSorting()
        {
            sortingIndex = (sortingIndex + 1) % SortingFunctions.Length;
            Array.Sort(elements, (x,y) => SortingFunctions[sortingIndex](x,y));
        }

        public void Forward()
        {
            cursorIndex = (cursorIndex + 1) % elements.Length;
            while (!IsCurrentElementSelectable())
            {
                cursorIndex = (cursorIndex + 1) % elements.Length;
            }
        }

        public void Backward()
        {
            cursorIndex--;
            if (cursorIndex < 0) cursorIndex = elements.Length - 1;
            while (!IsCurrentElementSelectable())
            {
                cursorIndex--;
                if (cursorIndex < 0) cursorIndex = elements.Length - 1;
            }
        }

        public bool IsCurrentElementSelectable()
        {
            T selectedElement = GetSelectedElement();
			return selectedElement != null && (selectedElement.Active(selectedElement) || selectedElement.SelectInactive);
        }

        public void ResetCursor()
        {
            cursorIndex = 0;
			if (!IsCurrentElementSelectable())
				Forward();
		}

        public abstract string GetText(int availableLength);
    }
}
