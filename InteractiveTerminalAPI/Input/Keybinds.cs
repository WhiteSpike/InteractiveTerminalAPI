using GameNetcodeStuff;
using HarmonyLib;
using InteractiveTerminalAPI.Compat;
using UnityEngine.InputSystem;

namespace InteractiveTerminalAPI.Input
{
    /// <summary>
    /// Class used to initialize the keybinds on game boot
    /// </summary>
    [HarmonyPatch]
    internal static class Keybinds
    {
        /// <summary>
        /// Asset used to store all the input bindings defined for our controls
        /// </summary>
        public static InputActionAsset Asset;

        public static InputActionMap ActionMap;

        /// <summary>
        /// Input binding used to trigger the move cursor up in the LGU interface action
        /// </summary>
        public static InputAction cursorUpAction;

        /// <summary>
        /// Input binding used to trigger the move cursor down in the LGU interface action
        /// </summary>
        public static InputAction cursorDownAction;

        /// <summary>
        /// Input binding used to trigger the exit LGU interface action
        /// </summary>
        public static InputAction cursorExitAction;

        /// <summary>
        /// Input binding used to change to next page in the LGU interface action
        /// </summary>
        public static InputAction pageUpAction;

        /// <summary>
        /// Input binding used to change to previous page in the LGU interface action
        /// </summary>
        public static InputAction pageDownAction;

        /// <summary>
        /// Input binding used to submit current prompt in the LGU interface action
        /// </summary>
        public static InputAction storeConfirmAction;

        /// <summary>
        /// Input binding used to change the current cursor menu's sorting
        /// </summary>
        public static InputAction changeSortingAction;

        public static InputAction pageUpCounterOnlyAction;
        public static InputAction pageDownCounterOnlyAction;

        public static PlayerControllerB localPlayerController => StartOfRound.Instance?.localPlayerController;

        /// <summary>
        /// Initializes the related assets with the control bindings
        /// </summary>
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
