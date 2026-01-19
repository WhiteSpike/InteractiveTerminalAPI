using InteractiveTerminalAPI.Input;
using InteractiveTerminalAPI.Misc.Util;
using InteractiveTerminalAPI.UI.Cursor;
using InteractiveTerminalAPI.UI.Screen;
using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace InteractiveTerminalAPI.UI.Application
{
    public abstract class BaseInteractiveApplication<T> : TerminalApplication where T : CursorElement
    {
        public BaseCursorMenu<T> currentCursorMenu;
        public BaseCursorMenu<T> previousCursorMenu;
        public IScreen previousScreen;
        Coroutine scrollingRoutine;
        protected float scrollRate = 0.1f;
        protected override string GetApplicationText()
        {
            return currentScreen.GetText(APIConstants.AVAILABLE_CHARACTERS_PER_LINE);
        }
        protected override Action PreviousScreen()
        {
            return () => SwitchScreen(previousScreen != null ? previousScreen : currentScreen, previousCursorMenu != null ? previousCursorMenu : currentCursorMenu, previous: true);
        }

        protected override void AddInputBindings()
        {
            base.AddInputBindings();
            Keybinds.changeSortingAction.started += OnApplicationChangeSorting;
            Keybinds.cursorUpAction.started += OnApplicationCursorUp;
            Keybinds.cursorDownAction.started += OnApplicationCursorDown;
            Keybinds.storeConfirmAction.started += OnApplicationConfirm;
            Keybinds.cursorUpAction.canceled += OnApplicationCursorScrollCancel;
			Keybinds.cursorDownAction.canceled += OnApplicationCursorScrollCancel;
		}
        protected override void RemoveInputBindings()
        {
            base.RemoveInputBindings();
            Keybinds.changeSortingAction.started -= OnApplicationChangeSorting;
            Keybinds.cursorUpAction.started -= OnApplicationCursorUp;
            Keybinds.cursorDownAction.started -= OnApplicationCursorDown;
            Keybinds.storeConfirmAction.started -= OnApplicationConfirm;
			Keybinds.cursorUpAction.canceled -= OnApplicationCursorScrollCancel;
			Keybinds.cursorDownAction.canceled -= OnApplicationCursorScrollCancel;
		}
        internal void OnApplicationConfirm(CallbackContext context)
        {
            Submit();
        }
        internal void OnApplicationCursorUp(CallbackContext context)
        {
            scrollingRoutine = InteractiveTerminalManager.Instance.StartCoroutine(RepeatScrolling(scrollUp: true));
        }
        internal void OnApplicationCursorScrollCancel(CallbackContext context)
		{
			InteractiveTerminalManager.Instance.StopCoroutine(scrollingRoutine);
		}
        internal void OnApplicationChangeSorting(CallbackContext context)
        {
            ChangeSorting();
        }
        internal void OnApplicationCursorDown(CallbackContext context)
        {
			scrollingRoutine = InteractiveTerminalManager.Instance.StartCoroutine(RepeatScrolling(scrollUp: false));
		}
        protected virtual void ChangeSorting()
        {
            PlayKeyboardSounds();
			currentCursorMenu.ChangeSorting();
        }
        public virtual void MoveCursorUp()
		{
			PlayKeyboardSounds();
			currentCursorMenu.Backward();
        }
        public virtual void MoveCursorDown()
		{
			PlayKeyboardSounds();
			currentCursorMenu.Forward();
        }
        public void Submit()
		{
			PlayKeyboardSounds();
			currentCursorMenu.Execute();
        }
        protected virtual void SwitchScreen(IScreen screen, BaseCursorMenu<T> cursorMenu, bool previous)
        {
            previousScreen = currentScreen;
            previousCursorMenu = currentCursorMenu;
            currentScreen = screen;
            currentCursorMenu = cursorMenu;
            if (!previous)
            {
                cursorMenu.ResetCursor();
            }
        }

        private IEnumerator RepeatScrolling(bool scrollUp)
        {
            while (true)
            {
                if (scrollUp) MoveCursorUp();
                else MoveCursorDown();
                yield return new WaitForEndOfFrame();
                yield return new WaitForSeconds(scrollRate);
            }
        }
    }
}
