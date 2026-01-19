using InteractiveTerminalAPI.Input;
using InteractiveTerminalAPI.UI.Cursor;
using static UnityEngine.InputSystem.InputAction;

namespace InteractiveTerminalAPI.UI.Application
{
    public abstract class InteractiveCounterApplication<T> : BaseInteractiveApplication<T> where T : CursorCounterElement
    {
        protected override void AddInputBindings()
        {
            base.AddInputBindings();
            Keybinds.pageUpAction.performed += OnApplicationCountUp;
            Keybinds.pageDownAction.performed += OnApplicationCountDown;
        }
        protected override void RemoveInputBindings()
        {
            base.RemoveInputBindings();
            Keybinds.pageUpAction.performed -= OnApplicationCountUp;
            Keybinds.pageDownAction.performed -= OnApplicationCountDown;
        }
        void IncrementSelectedCounter()
        {
            PlayKeyboardSounds();
			T selectedElement = currentCursorMenu.GetSelectedElement();
			if (selectedElement is not CursorCounterElement) return;
			selectedElement.IncrementCounter();
        }
        void DecrementSelectedCounter()
		{
			PlayKeyboardSounds();
			T selectedElement = currentCursorMenu.GetSelectedElement();
            if (selectedElement is not CursorCounterElement) return;
			selectedElement.DecrementCounter();
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
