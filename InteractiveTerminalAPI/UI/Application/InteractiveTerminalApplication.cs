﻿using InteractiveTerminalAPI.Input;
using InteractiveTerminalAPI.Misc.Util;
using InteractiveTerminalAPI.UI.Cursor;
using InteractiveTerminalAPI.UI.Screen;
using System;
using static UnityEngine.InputSystem.InputAction;

namespace InteractiveTerminalAPI.UI.Application
{
    public abstract class InteractiveTerminalApplication : TerminalApplication
    {
        protected CursorMenu currentCursorMenu;
        protected override string GetApplicationText()
        {
            return currentScreen.GetText(APIConstants.AVAILABLE_CHARACTERS_PER_LINE);
        }
        protected override void AddInputBindings()
        {
            base.AddInputBindings();
            Keybinds.cursorUpAction.performed += OnUpgradeStoreCursorUp;
            Keybinds.cursorDownAction.performed += OnUpgradeStoreCursorDown;
            Keybinds.storeConfirmAction.performed += OnUpgradeStoreConfirm;
        }
        protected override void RemoveInputBindings()
        {
            base.RemoveInputBindings();
            Keybinds.cursorUpAction.performed -= OnUpgradeStoreCursorUp;
            Keybinds.cursorDownAction.performed -= OnUpgradeStoreCursorDown;
            Keybinds.storeConfirmAction.performed -= OnUpgradeStoreConfirm;
        }
        protected override Action PreviousScreen()
        {
            return () => SwitchScreen(currentScreen, currentCursorMenu, previous: true);
        }
        internal virtual void MoveCursorUp()
        {
            currentCursorMenu.Backward();
        }
        internal virtual void MoveCursorDown()
        {
            currentCursorMenu.Forward();
        }
        public virtual void Submit()
        {
            currentCursorMenu.Execute();
        }
        internal void OnUpgradeStoreConfirm(CallbackContext context)
        {
            Submit();
        }
        internal void OnUpgradeStoreCursorUp(CallbackContext context)
        {
            MoveCursorUp();
        }
        internal void OnUpgradeStoreCursorDown(CallbackContext context)
        {
            MoveCursorDown();
        }

        protected virtual void SwitchScreen(IScreen screen, CursorMenu cursorMenu, bool previous)
        {
            currentScreen = screen;
            currentCursorMenu = cursorMenu;
            if (!previous) cursorMenu.cursorIndex = 0;
        }

        protected void Confirm(string title, string description, Action confirmAction, Action declineAction, string additionalMessage = "")
        {
            CursorElement[] cursorElements =
                [
                    CursorElement.Create(name: APIConstants.CONFIRM_PROMPT, action: confirmAction),
                    CursorElement.Create(name: APIConstants.CANCEL_PROMPT, action: declineAction),
                ];
            CursorMenu cursorMenu = CursorMenu.Create(elements: cursorElements);

            ITextElement[] elements =
                [
                    TextElement.Create(description),
                    TextElement.Create(" "),
                    TextElement.Create(additionalMessage),
                    cursorMenu
                ];
            IScreen screen = BoxedScreen.Create(title: title, elements: elements);

            SwitchScreen(screen, cursorMenu, false);
        }
        protected void ErrorMessage(string title, Action backAction, string error)
        {
            CursorElement[] cursorElements = [CursorElement.Create(name: APIConstants.GO_BACK_PROMPT, action: backAction)];
            CursorMenu cursorMenu = CursorMenu.Create(startingCursorIndex: 0, elements: cursorElements);
            ITextElement[] elements =
                [
                    TextElement.Create(text: error),
                    TextElement.Create(" "),
                    cursorMenu
                ];
            IScreen screen = BoxedScreen.Create(title: title, elements: elements);
            SwitchScreen(screen, cursorMenu, false);
        }
        protected void ErrorMessage(string title, string description, Action backAction, string error)
        {
            CursorElement[] cursorElements =
                [
                    CursorElement.Create(name: APIConstants.GO_BACK_PROMPT, action: backAction)
                ];
            CursorMenu cursorMenu = CursorMenu.Create(startingCursorIndex: 0, elements: cursorElements);
            ITextElement[] elements =
                [
                    TextElement.Create(text: description),
                    TextElement.Create(" "),
                    TextElement.Create(text: error),
                    TextElement.Create(" "),
                    cursorMenu
                ];
            IScreen screen = BoxedScreen.Create(title: title, elements: elements);
            SwitchScreen(screen, cursorMenu, false);
        }
    }
}
