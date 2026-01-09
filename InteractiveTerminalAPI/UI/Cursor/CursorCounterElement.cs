using InteractiveTerminalAPI.Misc.Util;
using System;
using System.Text;

namespace InteractiveTerminalAPI.UI.Cursor
{
    public class CursorCounterElement : CursorElement
    {
        public int Counter { get; set; }
        public bool ShowCounter { get; set; }

        public void ToggleShow(bool showCounter)
        {
            ShowCounter = showCounter;
        }
        public void IncrementCounter()
        {
            Counter++;
        }

        public void DecrementCounter()
        {
            if (Counter > 0) Counter--;
        }

        public override string GetText(int availableLength)
        {
            StringBuilder sb = new StringBuilder();
            if (!Active(this)) sb.Append(string.Format(APIConstants.COLOR_INITIAL_FORMAT, APIConstants.HEXADECIMAL_GREY));
            if (ShowCounter) { sb.Append(APIConstants.LEFT_ARROW).Append(Counter).Append(APIConstants.RIGHT_ARROW).Append(APIConstants.WHITE_SPACE); }
            sb.Append(base.GetText(availableLength));
            if (!Active(this)) sb.Append(APIConstants.COLOR_FINAL_FORMAT);
            return sb.ToString();
        }
        public static CursorCounterElement Create(string name = "", string description = "", Action action = default, int counter = 0, Func<CursorElement, bool> active = null, bool selectInactive = true, bool showCounter = true)
        {
            return new CursorCounterElement()
            {
                Name = name,
                Description = description,
                Action = action,
                Counter = counter,
                Active = active == null ? (_ => true) : active,
                SelectInactive = selectInactive,
                ShowCounter = showCounter
            };
        }
    }
}
