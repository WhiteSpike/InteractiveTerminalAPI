using UnityEngine;

namespace InteractiveTerminalAPI.Misc.Util
{
    internal static class APIConstants
    {
        #region General

        #region Characters
        internal const char WHITE_SPACE = ' ';
        #endregion

        #region Hex Colors

        internal const string HEXADECIMAL_RED = "#FF0000";
        internal const string HEXADECIMAL_WHITE = "#FFFFFF";
        internal const string HEXADECIMAL_GREEN = "#00FF00";
        internal const string HEXADECIMAL_GREY = "#666666";

        #endregion

        #endregion

        #region Keybinds

        internal const string MOVE_CURSOR_UP_KEYBIND_NAME = "Move Cursor Up in the current application";
        internal const string MOVE_CURSOR_UP_DEFAULT_KEYBIND = "<Keyboard>/w";

        internal const string MOVE_CURSOR_DOWN_KEYBIND_NAME = "Move Cursor Down in the current application";
        internal const string MOVE_CURSOR_DOWN_DEFAULT_KEYBIND = "<Keyboard>/s";

        internal const string EXIT_STORE_KEYBIND_NAME = "Exit application";
        internal const string EXIT_STORE_DEFAULT_KEYBIND = "<Keyboard>/escape";

        internal const string NEXT_PAGE_KEYBIND_NAME = "Next Page in the current application";
        internal const string NEXT_PAGE_DEFAULT_KEYBIND = "<Keyboard>/d";

        internal const string PREVIOUS_PAGE_KEYBIND_NAME = "Previous Page in the current application";
        internal const string PREVIOUS_PAGE_DEFAULT_KEYBIND = "<Keyboard>/a";

        internal const string SUBMIT_PROMPT_KEYBIND_NAME = "Submit Prompt in the current application";
        internal const string SUBMIT_PROMPT_DEFAULT_KEYBIND = "<Keyboard>/enter";

        internal const string CHANGE_SORTING_KEYBIND_NAME = "Change sorting in the current application";
        internal const string CHANGE_SORTING_DEFAULT_KEYBIND = "<Keyboard>/f";

        internal const string NEXT_PAGE_COUNTER_ONLY_KEYBIND_NAME = "Next Page in the current application (only applied for applications with counters)";
        internal const string NEXT_PAGE_COUNTER_ONLY_KEYBIND = "<Keyboard>/e";

        internal const string PREVIOUS_PAGE_COUNTER_ONLY_KEYBIND_NAME = "Previous Page in the current Application (only applied for applications with counters)";
        internal const string PREVIOUS_PAGE_COUNTER_ONLY_KEYBIND = "<Keyboard>/q";

        #endregion

        #region LGU Store Interactive UI

        #region Hierarchy Display

        internal const char INTERSECTION = '├';
        internal const char LAST_INTERSECTION = '└';
        internal const char SPACING = '─';
        internal const int SPACING_AMOUNT = 2;
        internal const char VERTICAL_SPACING = '│';
        internal const int VERTICAL_SPACING_AMOUNT = 1;

        #endregion

        #region Cursor Display
        internal const char CURSOR = '>';

        internal const string SELECTED_CURSOR_ELEMENT_FORMAT = "<mark={0}><color={1}>{2}</color></mark>";
        internal const string COLOR_INITIAL_FORMAT = "<color={0}>";
        internal const string COLOR_FINAL_FORMAT = "</color>";
        internal const string DEFAULT_BACKGROUND_SELECTED_COLOR = HEXADECIMAL_GREEN + "66";
        internal const string INACTIVE_BACKGROUND_SELECTED_COLOR = HEXADECIMAL_GREY + "33";
        internal const string DEFAULT_TEXT_SELECTED_COLOR = HEXADECIMAL_WHITE + "FF";
        internal const string DEFAULT_DEACTIVATED_TEXT_COLOUR = HEXADECIMAL_GREY + "55";

        internal const string GO_BACK_PROMPT = "Back";

        internal const string CONFIRM_PROMPT = "Confirm";
        internal const string CANCEL_PROMPT = "Abort";
        #endregion

        #region Screen Display

        internal const int AVAILABLE_CHARACTERS_PER_LINE = 51;
        internal const char TOP_LEFT_CORNER = '╭';
        internal const char TOP_RIGHT_CORNER = '╮';
        internal const char BOTTOM_LEFT_CORNER = '╰';
        internal const char BOTTOM_RIGHT_CORNER = '╯';
        internal const char VERTICAL_LINE = '│';
        internal const char HORIZONTAL_LINE = '─';

        internal const char CONNECTING_TITLE_LEFT = '╢';
        internal const char CONNECTING_TITLE_RIGHT = '╟';
        internal const char TOP_RIGHT_TITLE_CORNER = '╗';
        internal const char TOP_LEFT_TITLE_CORNER = '╔';
        internal const char BOTTOM_RIGHT_TITLE_CORNER = '╝';
        internal const char BOTTOM_LEFT_TITLE_CORNER = '╚';
        internal const char VERTICAL_TITLE_LINE = '║';
        internal const char HORIZONTAL_TITLE_LINE = '═';

        internal const char LEFT_ARROW = '←';
        internal const char RIGHT_ARROW = '→';

        #region Page Display
        internal const int START_PAGE_COUNTER = 30;
        #endregion

        #region Upgrades Display
        internal const int NAME_LENGTH = 17;
        internal const int LEVEL_LENGTH = 7;

        internal const char EMPTY_LEVEL = '○';
        internal const char FILLED_LEVEL = '●';
        #endregion

        #endregion

        #region Colours
        internal static readonly Color Invisible = new Color(0, 0, 0, 0);
        #endregion

        #endregion
    }
}
