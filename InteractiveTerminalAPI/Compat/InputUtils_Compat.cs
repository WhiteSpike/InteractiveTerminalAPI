using InteractiveTerminalAPI.Input;
using UnityEngine.InputSystem;

namespace InteractiveTerminalAPI.Compat
{
    public static class InputUtils_Compat
    {
        internal static InputActionAsset Asset => IngameKeybinds.GetAsset();
        public static InputAction CursorUpKey => IngameKeybinds.Instance.CursorUpKey;
        public static InputAction CursorDownKey => IngameKeybinds.Instance.CursorDownKey;
        public static InputAction CursorExitKey => IngameKeybinds.Instance.CursorExitKey;
        public static InputAction PageUpKey => IngameKeybinds.Instance.PageUpKey;
        public static InputAction PageDownKey => IngameKeybinds.Instance.PageDownKey;
        public static InputAction LguStoreConfirmKey => IngameKeybinds.Instance.LguStoreConfirmKey;
        public static InputAction ChangeApplicationSortingKey => IngameKeybinds.Instance.ApplicationChangeSorting;
        public static InputAction PageDownCounterOnlyKey => IngameKeybinds.Instance.PageDownCounterOnlyKey;
        public static InputAction PageUpCounterOnlyKey => IngameKeybinds.Instance.PageUpCounterOnlyKey;
        internal static void Init()
        {
            IngameKeybinds.Instance = new();
        }
    }
}
