using InteractiveTerminalAPI.Input;
using InteractiveTerminalAPI.Misc.Util;
using InteractiveTerminalAPI.UI.Cursor;
using InteractiveTerminalAPI.UI.Page;
using InteractiveTerminalAPI.UI.Screen;
using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace InteractiveTerminalAPI.UI.Application
{
    public abstract class PageApplication<T> : InteractiveTerminalApplication<T> where T : CursorElement
	{
        protected PageCursorElement<T> initialPage;
        protected PageCursorElement<T> previousPage;
        protected PageCursorElement<T> currentPage;
        protected override string GetApplicationText()
        {
            return currentScreen == currentPage.GetCurrentScreen() ? currentPage.GetText(APIConstants.AVAILABLE_CHARACTERS_PER_LINE) : currentScreen.GetText(APIConstants.AVAILABLE_CHARACTERS_PER_LINE);
        }
        protected virtual int GetEntriesPerPage<K>(K[] entries)
        {
            return Mathf.CeilToInt(entries.Length / 2f);
        }
        protected override Action PreviousScreen()
        {
            return () =>
            {
                if (previousPage != null)
                    SwitchScreen(previousPage, previous: true);
                else ResetScreen(); 
            };
        }

        protected virtual int GetAmountPages<K>(K[] entries)
        {
            return Mathf.CeilToInt(entries.Length / (float)GetEntriesPerPage(entries));
        }
        protected override void ChangeSorting()
        {
            PlayKeyboardSounds();
            currentPage.ChangeSorting();
        }
        public override void MoveCursorUp()
        {
            int cursorIndex = currentCursorMenu.cursorIndex;
            base.MoveCursorUp();
            if (currentPage.GetCurrentCursorMenu() == currentCursorMenu && currentCursorMenu.cursorIndex > cursorIndex)
            {
                ChangeScreenBackward();
                base.MoveCursorUp();
            }
        }
        public override void MoveCursorDown()
        {
            int cursorIndex = currentCursorMenu.cursorIndex;
            base.MoveCursorDown();
            if (currentPage.GetCurrentCursorMenu() == currentCursorMenu && currentCursorMenu.cursorIndex < cursorIndex)
            {
                ChangeScreenForward();
                base.MoveCursorDown();
            }
        }
        protected void ResetScreen()
        {
            SwitchScreen(initialPage, true);
        }
        protected void SwitchScreen(PageCursorElement<T> pages, bool previous)
        {
            previousPage = currentPage;
            currentPage = pages;
            SwitchScreen(currentPage.GetCurrentScreen(), currentPage.GetCurrentCursorMenu(), previous);
        }
        protected override void SwitchScreen(IScreen screen, BaseCursorMenu<T> cursorMenu, bool previous)
        {
            base.SwitchScreen(screen, cursorMenu, previous);
            if (screen == currentPage.GetCurrentScreen())
            {
                Keybinds.pageUpAction.performed += OnApplicationPageUp;
                Keybinds.pageDownAction.performed += OnApplicationPageDown;
            }
            else
            {
                Keybinds.pageUpAction.performed -= OnApplicationPageUp;
                Keybinds.pageDownAction.performed -= OnApplicationPageDown;
            }
        }
        public void ChangeScreenForward()
		{
			PlayKeyboardSounds();
			currentPage.PageUp();
            SwitchScreen(currentPage.GetCurrentScreen(), currentPage.GetCurrentCursorMenu(), false);
        }
        public void ChangeScreenBackward()
		{
			PlayKeyboardSounds();
			currentPage.PageDown();
            SwitchScreen(currentPage.GetCurrentScreen(), currentPage.GetCurrentCursorMenu(), false);
        }
        protected override void AddInputBindings()
        {
            base.AddInputBindings();
            Keybinds.pageUpAction.performed += OnApplicationPageUp;
            Keybinds.pageDownAction.performed += OnApplicationPageDown;
        }
        protected override void RemoveInputBindings()
        {
            base.RemoveInputBindings();
            Keybinds.pageUpAction.performed -= OnApplicationPageUp;
            Keybinds.pageDownAction.performed -= OnApplicationPageDown;
        }

        protected (K[][], BaseCursorMenu<T>[], IScreen[]) GetPageEntries<K>(K[] entries)
        {
            int amountPages = GetAmountPages(entries);
            int lengthPerPage = GetEntriesPerPage(entries);

            K[][] pages = new K[amountPages][];
            for (int i = 0; i < amountPages; i++)
                pages[i] = new K[lengthPerPage];
            for (int i = 0; i < entries.Length; i++)
            {
                int row = i / lengthPerPage;
                int col = i % lengthPerPage;
                pages[row][col] = entries[i];
            }
			BaseCursorMenu<T>[] cursorMenus = new BaseCursorMenu<T>[pages.Length];
            IScreen[] screens = new IScreen[pages.Length];
            initialPage = PageCursorElement<T>.Create(startingPageIndex: 0, cursorMenus: cursorMenus, elements: screens);

            return (pages, cursorMenus, screens);
        }
        protected void OnApplicationPageUp(CallbackContext context)
        {
            ChangeScreenForward();
        }
        protected void OnApplicationPageDown(CallbackContext context)
        {
            ChangeScreenBackward();
        }

    }
}
