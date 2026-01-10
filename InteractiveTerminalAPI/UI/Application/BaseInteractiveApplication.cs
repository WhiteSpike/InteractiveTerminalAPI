using InteractiveTerminalAPI.Input;
using InteractiveTerminalAPI.Misc.Util;
using InteractiveTerminalAPI.UI.Cursor;
using InteractiveTerminalAPI.UI.Screen;
using System;
using static UnityEngine.InputSystem.InputAction;

namespace InteractiveTerminalAPI.UI.Application
{
    public abstract class BaseInteractiveApplication<T> : TerminalApplication where T : CursorElement
    {
        public BaseCursorMenu<T> currentCursorMenu;
        public BaseCursorMenu<T> previousCursorMenu;
        public IScreen previousScreen;
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
            Keybinds.changeSortingAction.performed += OnApplicationChangeSorting;
            Keybinds.cursorUpAction.performed += OnApplicationCursorUp;
            Keybinds.cursorDownAction.performed += OnApplicationCursorDown;
            Keybinds.storeConfirmAction.performed += OnApplicationConfirm;
        }
        protected override void RemoveInputBindings()
        {
            base.RemoveInputBindings();
            Keybinds.changeSortingAction.performed -= OnApplicationChangeSorting;
            Keybinds.cursorUpAction.performed -= OnApplicationCursorUp;
            Keybinds.cursorDownAction.performed -= OnApplicationCursorDown;
            Keybinds.storeConfirmAction.performed -= OnApplicationConfirm;
        }
        internal void OnApplicationConfirm(CallbackContext context)
        {
            Submit();
        }
        internal void OnApplicationCursorUp(CallbackContext context)
        {
            MoveCursorUp();
        }
        internal void OnApplicationChangeSorting(CallbackContext context)
        {
            ChangeSorting();
        }
        internal void OnApplicationCursorDown(CallbackContext context)
        {
            MoveCursorDown();
        }
        protected virtual void ChangeSorting()
        {
            RoundManager.PlayRandomClip(terminal.terminalAudio, terminal.keyboardClips);
            currentCursorMenu.ChangeSorting();
        }
        public virtual void MoveCursorUp()
        {
            RoundManager.PlayRandomClip(terminal.terminalAudio, terminal.keyboardClips);
            currentCursorMenu.Backward();
        }
        public virtual void MoveCursorDown()
        {
            RoundManager.PlayRandomClip(terminal.terminalAudio, terminal.keyboardClips);
            currentCursorMenu.Forward();
        }
        public void Submit()
        {
            RoundManager.PlayRandomClip(terminal.terminalAudio, terminal.keyboardClips);
            currentCursorMenu.Execute();
        }
        protected virtual void SwitchScreen(IScreen screen, BaseCursorMenu<T> cursorMenu, bool previous)
        {
            previousScreen = screen;
            previousCursorMenu = currentCursorMenu;
            currentScreen = screen;
            currentCursorMenu = cursorMenu;
            if (!previous)
            {
                cursorMenu.cursorIndex = 0;
                if (!cursorMenu.IsCurrentElementSelectable())
                    cursorMenu.Forward();
            }
        }
    }
}
