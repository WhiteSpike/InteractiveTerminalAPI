using InteractiveTerminalAPI.Extensions;
using InteractiveTerminalAPI.Misc.Util;
using InteractiveTerminalAPI.Util;
using System;
using System.Text;

namespace InteractiveTerminalAPI.UI.Screen
{
    public class BoxedOutputScreen<K,V> : BoxedScreen
    {
        public Func<K> Input { get; set; }
        public Func<K,V> Output { get; set; }

        public V GetOutput()
        {
            if (Output == null || Input == null) return default;
            return Output(Input());
        }

        public override string GetText(int availableLength)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine().AppendLine();
            sb.Append(new string(APIConstants.WHITE_SPACE, 1))
                .Append(APIConstants.TOP_LEFT_TITLE_CORNER)
                .Append(new string(APIConstants.HORIZONTAL_TITLE_LINE, Title.Length + 2))
                .Append(APIConstants.TOP_RIGHT_TITLE_CORNER)
                .AppendLine();
            sb.Append(APIConstants.TOP_LEFT_CORNER)
                .Append(APIConstants.CONNECTING_TITLE_LEFT)
                .Append(APIConstants.WHITE_SPACE)
                .Append(Title)
                .Append(APIConstants.WHITE_SPACE)
                .Append(APIConstants.CONNECTING_TITLE_RIGHT)
                .Append(new string(APIConstants.HORIZONTAL_LINE, availableLength - 6 - Title.Length))
                .Append(APIConstants.TOP_RIGHT_CORNER)
                .AppendLine();
            sb.Append(APIConstants.VERTICAL_LINE)
                .Append(APIConstants.BOTTOM_LEFT_TITLE_CORNER)
                .Append(new string(APIConstants.HORIZONTAL_TITLE_LINE, Title.Length + 2))
                .Append(APIConstants.BOTTOM_RIGHT_TITLE_CORNER)
                .Append(new string(APIConstants.WHITE_SPACE, availableLength - 6 - Title.Length))
                .Append(APIConstants.VERTICAL_LINE)
                .AppendLine();
            for (int i = 0; i < elements.Length; i++)
            {
                sb.Append(Tools.WrapText(elements[i].GetText(availableLength - 4), availableLength, leftPadding: "│ ", rightPadding: " │"));
            }
            string outputString = GetOutput().ToString();
            int rightPos = availableLength - outputString.NonHTMLLength() - 6;
            sb.Append(APIConstants.VERTICAL_LINE)
                .Append(new string(APIConstants.WHITE_SPACE, rightPos - 3))
                .Append(APIConstants.TOP_LEFT_TITLE_CORNER);
                sb.Append(new string(APIConstants.HORIZONTAL_TITLE_LINE, outputString.NonHTMLLength() + 2))
                .Append(APIConstants.TOP_RIGHT_TITLE_CORNER)
                .Append(new string(APIConstants.WHITE_SPACE, 3))
                .Append(APIConstants.VERTICAL_LINE)
                .Append('\n');
            sb.Append(APIConstants.BOTTOM_LEFT_CORNER)
                .Append(new string(APIConstants.HORIZONTAL_LINE, rightPos - 3))
                .Append(APIConstants.CONNECTING_TITLE_LEFT)
                .Append(APIConstants.WHITE_SPACE)
                .Append(outputString)
                .Append(APIConstants.WHITE_SPACE)
                .Append(APIConstants.CONNECTING_TITLE_RIGHT)
                .Append(new string(APIConstants.HORIZONTAL_LINE, 3))
                .Append(APIConstants.BOTTOM_RIGHT_CORNER)
                .Append('\n');
            sb.Append(new string(APIConstants.WHITE_SPACE, rightPos - 2))
                .Append(APIConstants.BOTTOM_LEFT_TITLE_CORNER)
                .Append(new string(APIConstants.HORIZONTAL_TITLE_LINE, outputString.NonHTMLLength() + 2))
                .Append(APIConstants.BOTTOM_RIGHT_TITLE_CORNER)
                .Append('\n');


            return sb.ToString();
        }

        public static BoxedOutputScreen<K,V> Create(string title = "", ITextElement[] elements = default, Func<K> input = default, Func<K,V> output = default)
        {
            return new BoxedOutputScreen<K,V>()
            {
                Title = title,
                elements = elements,
                Input = input,
                Output = output
            };
        }
    }
}
