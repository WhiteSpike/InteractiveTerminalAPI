﻿using InteractiveTerminalAPI.Input;
using InteractiveTerminalAPI.UI.Screen;
using InteractiveTerminalAPI.Util;
using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

namespace InteractiveTerminalAPI.UI.Application
{
    public abstract class TerminalApplication
    {
        protected IScreen currentScreen;
        protected readonly Terminal terminal = Tools.GetTerminal();

        public abstract void Initialization();
        protected abstract string GetApplicationText();
        protected abstract Action PreviousScreen();
        public void UpdateText()
        {
            string text = GetApplicationText();
            terminal.screenText.text = text;
            terminal.currentText = text;
        }
        internal void UpdateInputBindings(bool enable = false)
        {
            if (enable) AddInputBindings();
            else RemoveInputBindings();
        }
        protected virtual void AddInputBindings()
        {
            Keybinds.cursorExitAction.performed += OnUpgradeStoreCursorExit;
            terminal.playerActions.Movement.OpenMenu.performed -= terminal.PressESC;
        }

        protected virtual void RemoveInputBindings()
        {
            Keybinds.cursorExitAction.performed -= OnUpgradeStoreCursorExit;
            terminal.playerActions.Movement.OpenMenu.performed += terminal.PressESC;
        }
        internal void OnUpgradeStoreCursorExit(CallbackContext context)
        {
            UnityEngine.Object.Destroy(InteractiveTerminalManager.Instance);
        }
    }
}
