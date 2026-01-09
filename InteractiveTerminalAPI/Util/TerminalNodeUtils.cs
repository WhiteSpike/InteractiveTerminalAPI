using System.Collections.Generic;
using UnityEngine;

namespace InteractiveTerminalAPI.Util
{
    /// <summary>
    /// Handler for Terminal nodes stored in the terminal for customization when required
    /// Later on, this can also be used for when we decide to store our own TerminalNodes into the terminal
    /// instead of generating new terminal nodes left and right through CommandParser
    /// </summary>
    internal static class TerminalNodeUtils
    {
        /// <summary>
        /// Dictionary used as cache to optimize unnecessary searches when already found the reference.
        /// </summary>
        static readonly Dictionary<string, TerminalNode> retrievedTerminalNodes = new Dictionary<string, TerminalNode>();

        const string HELP_NOUN = "help";
#if DEBUG
        const string INFO_VERB = "info";
        const string SHOVEL_NOUN = "shovel";
#endif
        /// <summary>
        /// Finds the associated TerminalKeyword stored in the terminal which has the requested word
        /// </summary>
        /// <param name="word">Word used to prompt a terminal node</param>
        /// <returns>Associated keyword if it's stored in the terminal, null if it doesn't exist</returns>
        internal static TerminalKeyword FindTerminalKeyword(string word)
        {
            TerminalKeyword terminalKeyword = Tools.GetTerminal().CheckForExactSentences(word);
            if (terminalKeyword == null) Plugin.mls.LogError($"Couldn't find terminal node for the word \"{word}\"");
            return terminalKeyword;
        }
#if DEBUG
        /// <summary>
        /// Retrieves the TerminalNode used to display info of the requested item designated through the given word
        /// </summary>
        /// <param name="word">Name of the item to gather the info terminal node</param>
        /// <returns>The info TerminalNode associated to the item if it's stored in the terminal, null if it doesn't exist</returns>
        internal static TerminalNode GetItemInfoTerminalNode(string word)
        {
            TerminalNode result = retrievedTerminalNodes.GetValueOrDefault(word, null);
            if (result != null) return result;
            TerminalKeyword infoKeyword = Tools.GetTerminal().ParseWord(INFO_VERB);
            for (int i = 0; i < infoKeyword.compatibleNouns.Length && result == null; i++)
            {
                if (infoKeyword.compatibleNouns[i].noun.word != word) continue;
                result = infoKeyword.compatibleNouns[i].result;
            }
            if (result == null) Plugin.mls.LogError($"Couldn't find info terminal node for item \"{word}\"");
            if (!retrievedTerminalNodes.ContainsKey(word)) retrievedTerminalNodes[word] = result;
            return result;
        }
#endif
        /// <summary>
        /// Retrieves the TerminalNode associated with provided special keyword
        /// </summary>
        /// <param name="word">Word used to prompt the special TerminalNode</param>
        /// <returns>Associated TerminalNode if it's stored in the terminal, null if it doesn't exist</returns>
        internal static TerminalNode GetSpecialTerminalNodeByWord(string word)
        {
            TerminalNode result = retrievedTerminalNodes.GetValueOrDefault(word, null);
            if (result != null) return result;
            TerminalKeyword terminalKeyword = FindTerminalKeyword(word);
            if (terminalKeyword == null) return null;
            result = FindTerminalKeyword(word).specialKeywordResult;
            if (!retrievedTerminalNodes.ContainsKey(word)) retrievedTerminalNodes[word] = result;
            return result;
        }
#if DEBUG
        /// <summary>
        /// Adds or remove additional text from the given terminal node if a given feature is enabled or not
        /// </summary>
        /// <param name="node">Terminal node which we want to change the text of</param>
        /// <param name="insertText">The additional text we wish to add to the node</param>
        /// <param name="enabled">Feature associated with the additional text is enabled or not</param>
        internal static void UpdateTextToNode(ref TerminalNode node, string insertText, bool enabled = true)
        {
            string text = node.displayText;
            int index = text.IndexOf(insertText);
            if (index != -1 && !enabled)
            {
                node.displayText = text.Remove(index, insertText.Length);
                return;
            }
            if (index == -1 && enabled)
            {
                text += insertText;
                node.displayText = text;
            }
        }
#endif
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Retrieves the TerminalNode associated with the keyword "help"</returns>
        internal static TerminalNode GetHelpTerminalNode()
        {
            return GetSpecialTerminalNodeByWord(HELP_NOUN);
        }
#if DEBUG
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Retrieves the TerminalNode associated with the keywords "info shovel"</returns>
        internal static TerminalNode GetInfoShovelTerminalNode()
        {
            return GetItemInfoTerminalNode(SHOVEL_NOUN);
        }
#endif
    }
}
