using InteractiveTerminalAPI.Misc.Util;
using LethalCompanyInputUtils.Api;
using UnityEngine.InputSystem;

namespace InteractiveTerminalAPI.Input
{
    internal class IngameKeybinds : LcInputActions
    {
        public static IngameKeybinds Instance;
        internal static InputActionAsset GetAsset()
        {
            return Instance.Asset;
        }

        [InputAction(APIConstants.MOVE_CURSOR_UP_DEFAULT_KEYBIND, Name = APIConstants.MOVE_CURSOR_UP_KEYBIND_NAME)]
        public InputAction CursorUpKey { get; set; }

        [InputAction(APIConstants.MOVE_CURSOR_DOWN_DEFAULT_KEYBIND, Name = APIConstants.MOVE_CURSOR_DOWN_KEYBIND_NAME)]
        public InputAction CursorDownKey { get; set; }

        [InputAction(APIConstants.EXIT_STORE_DEFAULT_KEYBIND, Name = APIConstants.EXIT_STORE_KEYBIND_NAME)]
        public InputAction CursorExitKey { get; set; }

        [InputAction(APIConstants.NEXT_PAGE_DEFAULT_KEYBIND, Name = APIConstants.NEXT_PAGE_KEYBIND_NAME)]
        public InputAction PageUpKey { get; set; }

        [InputAction(APIConstants.PREVIOUS_PAGE_DEFAULT_KEYBIND, Name = APIConstants.PREVIOUS_PAGE_KEYBIND_NAME)]
        public InputAction PageDownKey { get; set; }

        [InputAction(APIConstants.SUBMIT_PROMPT_DEFAULT_KEYBIND, Name = APIConstants.SUBMIT_PROMPT_KEYBIND_NAME)]
        public InputAction LguStoreConfirmKey { get; set; }

        [InputAction(APIConstants.CHANGE_SORTING_DEFAULT_KEYBIND, Name = APIConstants.CHANGE_SORTING_KEYBIND_NAME)]
        public InputAction ApplicationChangeSorting { get; set; }

		[InputAction(APIConstants.NEXT_PAGE_COUNTER_ONLY_KEYBIND, Name = APIConstants.NEXT_PAGE_COUNTER_ONLY_KEYBIND_NAME)]
		public InputAction PageUpCounterOnlyKey { get; set; }

		[InputAction(APIConstants.PREVIOUS_PAGE_COUNTER_ONLY_KEYBIND, Name = APIConstants.PREVIOUS_PAGE_COUNTER_ONLY_KEYBIND_NAME)]
		public InputAction PageDownCounterOnlyKey { get; set; }

	}
}
