using InteractiveTerminalAPI.Input;
using InteractiveTerminalAPI.Misc.Util;
using InteractiveTerminalAPI.UI.Application;
using InteractiveTerminalAPI.UI.Cursor;
using InteractiveTerminalAPI.Util;
using System.Text;
using static UnityEngine.InputSystem.InputAction;

namespace InteractiveTerminalAPI.UI.Hierarchy
{
    public class BaseCursorHierarchyElement : CursorElement
    {
        public char IntersectionCharacter { get; set; } = APIConstants.INTERSECTION;
        public char LastIntersectionCharacter { get; set; } = APIConstants.LAST_INTERSECTION;
        public char SpacingCharacter { get; set; } = APIConstants.SPACING;
        public int Spacing { get; set; } = APIConstants.SPACING_AMOUNT;
        public char VerticalSpacingCharacter { get; set; } = APIConstants.VERTICAL_SPACING;
        public int VerticalSpacing { get; set; } = APIConstants.VERTICAL_SPACING_AMOUNT;
        public string Title { get; set; }
        public BaseCursorMenu<CursorElement> innerCursorMenu {  get; set; }
        public bool Selected { get; set; }

        BaseCursorMenu<CursorElement> previousCursorMenu;
        public override void ExecuteAction()
        {
            base.ExecuteAction();
            Selected = true;
            if (InteractiveTerminalManager.Instance.mainApplication is InteractiveTerminalApplication<CursorElement> application)
            {
                Keybinds.cursorExitAction.performed -= InteractiveTerminalManager.Instance.mainApplication.OnScreenExit;
                Keybinds.cursorExitAction.performed += OnHierarchyExit;
                previousCursorMenu = application.currentCursorMenu;
                application.currentCursorMenu = innerCursorMenu;
            }
        }
        public void OnHierarchyExit(CallbackContext context)
        {
            Keybinds.cursorExitAction.performed += InteractiveTerminalManager.Instance.mainApplication.OnScreenExit;
            Keybinds.cursorExitAction.performed -= OnHierarchyExit;
            if (InteractiveTerminalManager.Instance.mainApplication is InteractiveTerminalApplication<CursorElement> application)
            {
                application.currentCursorMenu = previousCursorMenu;
                previousCursorMenu = null;
            }
            Selected = false;
        }
        public override string GetText(int availableLength)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Title);
            if (!Selected) return sb.ToString();
            sb.Append("\n");
            int verticalSpacing = 0;
            CursorElement[] textElements = innerCursorMenu.elements;
            for (int i = 0; i < textElements.Length;)
            {
                CursorElement element = textElements[i];
                if (verticalSpacing != VerticalSpacing)
                {
                    sb.Append(VerticalSpacingCharacter + "\n");
                    verticalSpacing++;
                    continue;
                }
                verticalSpacing = 0;
                bool last = i == textElements.Length - 1;
                if (last)
                {
                    sb.Append(LastIntersectionCharacter);
                }
                else
                {
                    sb.Append(IntersectionCharacter);
                }
                sb.Append(new string(SpacingCharacter, Spacing));
                sb.Append(APIConstants.WHITE_SPACE);
                string text = element.GetText(availableLength - 5 - Spacing);
                string backgroundColor = element.Active(element) ? APIConstants.DEFAULT_BACKGROUND_SELECTED_COLOR : APIConstants.INACTIVE_BACKGROUND_SELECTED_COLOR;
                text = i == innerCursorMenu.cursorIndex ? string.Format(APIConstants.SELECTED_CURSOR_ELEMENT_FORMAT, backgroundColor, APIConstants.DEFAULT_TEXT_SELECTED_COLOR, text) : text;
                if (element is BaseCursorHierarchyElement)
                {
                    sb.Append(Tools.WrapText(text, availableLength - 5 - Spacing, leftPadding: (!last ? new string(VerticalSpacingCharacter, 1) : "") + new string(APIConstants.WHITE_SPACE, Spacing + 1), padLeftFirst: false));
                }
                else
                {
                    if (last)
                    {
                        sb.Append(Tools.WrapText(text, availableLength - 5 - Spacing));
                    }
                    else
                    {
                        sb.Append(Tools.WrapText(text, availableLength - 5 - Spacing, leftPadding: new string(VerticalSpacingCharacter, 1), padLeftFirst: false));
                    }
                }
                i++;
            }
            return sb.ToString();
        }

        
    }
}
