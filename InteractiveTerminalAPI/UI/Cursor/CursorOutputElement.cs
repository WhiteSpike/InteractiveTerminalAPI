using InteractiveTerminalAPI.Misc.Util;
using System;
using System.Text;

namespace InteractiveTerminalAPI.UI.Cursor
{
    public class CursorOutputElement<T> : CursorCounterElement
    {
        public Func<int, T> Func { get; set; }

        public T ApplyFunction()
        {
            if (Func == null) return default;
            return Func(Counter);
        }
        public override string GetText(int availableLength)
        {
            StringBuilder sb = new StringBuilder();
            if (!Active(this)) sb.Append(string.Format(APIConstants.COLOR_INITIAL_FORMAT, APIConstants.HEXADECIMAL_GREY));
            sb.Append(base.GetText(availableLength));
			if (ShowCounter)
            {
				sb.Append(new string(APIConstants.WHITE_SPACE, 10));
				sb.Append(ApplyFunction());
			}
            if (!Active(this)) sb.Append(APIConstants.COLOR_FINAL_FORMAT);
            return sb.ToString();
        }
        public static CursorOutputElement<T> Create(string name = "", string description = "", Action action = default, int counter = 0, Func<int, T> func = default, Func<CursorElement, bool> active = null, bool selectInactive = true, bool showCounter = true)
        {
            return new CursorOutputElement<T>()
            {
                Name = name,
                Description = description,
                Action = action,
                Counter = counter,
                Func = func,
                Active = active == null ? (_ => true) : active,
                SelectInactive = selectInactive,
                ShowCounter = showCounter
            };
        }

    }
}
