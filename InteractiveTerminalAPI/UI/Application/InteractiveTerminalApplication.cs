using InteractiveTerminalAPI.Misc.Util;
using InteractiveTerminalAPI.UI.Cursor;
using InteractiveTerminalAPI.UI.Screen;
using System;

namespace InteractiveTerminalAPI.UI.Application
{
    public abstract class InteractiveTerminalApplication<T> : BaseInteractiveApplication<T> where T : CursorElement
    {
        protected void Confirm(string title, string description, Action confirmAction, Action declineAction, string additionalMessage = "")
        {
            T[] cursorElements =
                [
                    (T)CursorElement.Create(name: APIConstants.CONFIRM_PROMPT, action: confirmAction),
                    (T)CursorElement.Create(name: APIConstants.CANCEL_PROMPT, action: declineAction),
                ];
            CursorMenu<T> cursorMenu = CursorMenu<T>.Create(elements: cursorElements);

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
            T[] cursorElements = [(T)CursorElement.Create(name: APIConstants.GO_BACK_PROMPT, action: backAction)];
            CursorMenu<T> cursorMenu = CursorMenu<T>.Create(startingCursorIndex: 0, elements: cursorElements);
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
            T[] cursorElements =
                [
                    (T)CursorElement.Create(name: APIConstants.GO_BACK_PROMPT, action: backAction)
                ];
			CursorMenu<T> cursorMenu = CursorMenu<T>.Create(startingCursorIndex: 0, elements: cursorElements);
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
