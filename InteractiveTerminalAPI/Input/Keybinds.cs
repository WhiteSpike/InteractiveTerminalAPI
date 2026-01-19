using GameNetcodeStuff;
using HarmonyLib;
using InteractiveTerminalAPI.Compat;
using UnityEngine.InputSystem;

namespace InteractiveTerminalAPI.Input
{
    [HarmonyPatch]
    internal static class Keybinds
    {
        public static InputActionAsset Asset;

        public static InputActionMap ActionMap;

        public static InputAction cursorUpAction;

        public static InputAction cursorDownAction;

        public static InputAction cursorExitAction;

        public static InputAction pageUpAction;

        public static InputAction pageDownAction;

        public static InputAction storeConfirmAction;

        public static InputAction changeSortingAction;

        public static InputAction pageUpCounterOnlyAction;
        public static InputAction pageDownCounterOnlyAction;

        public static PlayerControllerB localPlayerController => StartOfRound.Instance?.localPlayerController;

        [HarmonyPatch(typeof(PreInitSceneScript), "Awake")]
        [HarmonyPrefix]
        public static void AddToKeybindMenu()
        {
            Asset = InputUtils_Compat.Asset;
            ActionMap = Asset.actionMaps[0];
            cursorUpAction = InputUtils_Compat.CursorUpKey;
            cursorDownAction = InputUtils_Compat.CursorDownKey;
            cursorExitAction = InputUtils_Compat.CursorExitKey;
            pageUpAction = InputUtils_Compat.PageUpKey;
            pageDownAction = InputUtils_Compat.PageDownKey;
            storeConfirmAction = InputUtils_Compat.LguStoreConfirmKey;
            changeSortingAction = InputUtils_Compat.ChangeApplicationSortingKey;
            pageUpCounterOnlyAction = InputUtils_Compat.PageUpCounterOnlyKey;
            pageDownCounterOnlyAction = InputUtils_Compat.PageDownCounterOnlyKey;
        }
    }
}
