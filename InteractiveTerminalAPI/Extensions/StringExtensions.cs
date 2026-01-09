using System;
using System.Collections.Generic;
using System.Text;

namespace InteractiveTerminalAPI.Extensions
{
	public static class StringExtensions
	{
		public static int NonHTMLLength(this string characters)
		{
			int length = 0;
			bool HTMLTag = false;
			for(int i = 0; i < characters.Length; i++) 
			{
				char c = characters[i];
				if (c == '<' && !HTMLTag)
				{
					HTMLTag = true;
					continue;
				}
				if (c == '>' && HTMLTag)
				{
					HTMLTag = false;
					continue;
				}
				if (!HTMLTag) length++;
			}
			return length;
		}
	}
}
