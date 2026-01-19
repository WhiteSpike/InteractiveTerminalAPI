using InteractiveTerminalAPI.Input;
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
        protected AudioSource terminalAudio;
        protected AudioClip[] terminalKeyboardAudioClips;

        public abstract void Initialization();
        protected abstract string GetApplicationText();
        protected abstract Action PreviousScreen();
        public virtual void SetDefaultKeyboardAudio()
        {
            terminalAudio = terminal.terminalAudio;
            terminalKeyboardAudioClips = terminal.keyboardClips;
        }
        public void UpdateText()
        {
            string text = GetApplicationText();
            terminal.screenText.text = text;
            terminal.currentText = text;
		}
		public void OnScreenExit(CallbackContext context)
		{
			PlayKeyboardSounds();
			ScreenExit();
		}
		internal void UpdateInputBindings(bool enable = false)
        {
            if (enable) AddInputBindings();
            else RemoveInputBindings();
        }
        protected virtual void AddInputBindings()
        {
            Keybinds.cursorExitAction.performed += OnScreenExit;
            terminal.playerActions.Movement.OpenMenu.performed -= terminal.PressESC;
        }

        protected virtual void RemoveInputBindings()
        {
            Keybinds.cursorExitAction.performed -= OnScreenExit;
            terminal.playerActions.Movement.OpenMenu.performed += terminal.PressESC;
        }

        protected virtual void PlayKeyboardSounds()
        {
            RoundManager.PlayRandomClip(terminalAudio, terminalKeyboardAudioClips);
        }

        protected virtual void ScreenExit()
        {
			UnityEngine.Object.Destroy(InteractiveTerminalManager.Instance.gameObject);
		}
    }
}
