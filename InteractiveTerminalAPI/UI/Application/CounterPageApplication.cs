using InteractiveTerminalAPI.Input;
using InteractiveTerminalAPI.UI.Cursor;
using InteractiveTerminalAPI.UI.Screen;
using static UnityEngine.InputSystem.InputAction;

namespace InteractiveTerminalAPI.UI.Application
{
	public abstract class CounterPageApplication<T> : PageApplication<T> where T : CursorCounterElement
	{
		protected override void AddInputBindings()
		{
			base.AddInputBindings();
			Keybinds.pageDownAction.performed -= OnApplicationPageDown;
			Keybinds.pageUpAction.performed -= OnApplicationPageUp;

			Keybinds.pageUpCounterOnlyAction.performed += OnApplicationPageUp;
			Keybinds.pageDownCounterOnlyAction.performed += OnApplicationPageDown;
			Keybinds.pageUpAction.performed += OnApplicationCountUp;
			Keybinds.pageDownAction.performed += OnApplicationCountDown;
		}
		protected override void RemoveInputBindings()
		{
			base.RemoveInputBindings();
			Keybinds.pageUpAction.performed -= OnApplicationCountUp;
			Keybinds.pageDownAction.performed -= OnApplicationCountDown;
			Keybinds.pageUpCounterOnlyAction.performed -= OnApplicationPageUp;
			Keybinds.pageDownCounterOnlyAction.performed -= OnApplicationPageDown;
		}
		void IncrementSelectedCounter()
		{
			RoundManager.PlayRandomClip(terminal.terminalAudio, terminal.keyboardClips);
			currentCursorMenu.GetSelectedElement().IncrementCounter();
		}
		protected override void SwitchScreen(IScreen screen, BaseCursorMenu<T> cursorMenu, bool previous)
		{
			base.SwitchScreen(screen, cursorMenu, previous);
			Keybinds.pageDownAction.performed -= OnApplicationPageDown;
			Keybinds.pageUpAction.performed -= OnApplicationPageUp;
			if (screen == currentPage.GetCurrentScreen())
			{
				Keybinds.pageUpCounterOnlyAction.performed += OnApplicationPageUp;
				Keybinds.pageDownCounterOnlyAction.performed += OnApplicationPageDown;
			}
			else
			{
				Keybinds.pageUpCounterOnlyAction.performed -= OnApplicationPageUp;
				Keybinds.pageDownCounterOnlyAction.performed -= OnApplicationPageDown;
			}
		}
		void DecrementSelectedCounter()
		{
			RoundManager.PlayRandomClip(terminal.terminalAudio, terminal.keyboardClips);
			currentCursorMenu.GetSelectedElement().DecrementCounter();
		}
		internal void OnApplicationCountUp(CallbackContext context)
		{
			IncrementSelectedCounter();
		}
		internal void OnApplicationCountDown(CallbackContext context)
		{
			DecrementSelectedCounter();
		}
	}
}
