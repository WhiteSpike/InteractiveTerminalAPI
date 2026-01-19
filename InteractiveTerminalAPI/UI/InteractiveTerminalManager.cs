using InteractiveTerminalAPI.Misc.Util;
using InteractiveTerminalAPI.UI.Application;
using InteractiveTerminalAPI.Util;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace InteractiveTerminalAPI.UI
{
    public class InteractiveTerminalManager : MonoBehaviour
    {
        internal static Dictionary<string, Func<TerminalApplication>> registeredApplications = new Dictionary<string, Func<TerminalApplication>>();
        internal static Dictionary<string, bool> commandSensitive = new Dictionary<string, bool>();
        public static InteractiveTerminalManager Instance;
        internal TerminalApplication mainApplication;
        Terminal terminalReference;
        TerminalNode lastTerminalNode;
        Color previousCaretColor;

        void Start()
        {
            Instance = this;
            terminalReference = Tools.GetTerminal();
            lastTerminalNode = terminalReference.currentNode;
            UpdateInput(false);
        }
        internal void Initialize(string command)
        {
            Func<TerminalApplication> function = registeredApplications.GetValueOrDefault(command, null);
            if (commandSensitive.ContainsKey(command.ToLower()))
            {
                foreach (string commandPrompt in registeredApplications.Keys)
                    if (string.Equals(commandPrompt, command, StringComparison.OrdinalIgnoreCase))
                    {
                        function = registeredApplications[commandPrompt];
                        break;
                    }
            }
            if (function == null)
            {
                Plugin.mls.LogError("An application was not selected to change the terminal's text.");
                return;
            }
            mainApplication = function.Invoke();
            if (mainApplication == null)
            {
                Plugin.mls.LogError("The selected application doesn't have a valid constructor.");
                return;
            }

            mainApplication.Initialization();
            mainApplication.SetDefaultKeyboardAudio();
			mainApplication.UpdateInputBindings(enable: true);
        }
        void Update()
        {
            if (terminalReference == null) return;
            if (mainApplication == null) return;
            mainApplication.UpdateText();
        }

        void OnDestroy()
        {
            mainApplication.UpdateInputBindings(enable: false);
            terminalReference.LoadNewNode(lastTerminalNode);
            UpdateInput(true);
            Instance = null;
        }
        internal void UpdateInput(bool enable)
        {
            if (enable)
            {
                terminalReference.screenText.interactable = true;
                terminalReference.screenText.ActivateInputField();
                terminalReference.screenText.Select();
                terminalReference.screenText.caretColor = previousCaretColor;
            }
            else
            {
                terminalReference.screenText.DeactivateInputField();
                terminalReference.screenText.interactable = false;
                previousCaretColor = terminalReference.screenText.caretColor;
                terminalReference.screenText.caretColor = APIConstants.Invisible;
            }
        }
        public static bool InteractiveTerminalBeingUsed()
        {
            return Instance != null;
        }
        public static bool ContainsApplication(string command)
        {
            if (!commandSensitive.ContainsKey(command.ToLower()))
                return registeredApplications.ContainsKey(command);
            else
            {
                foreach (string commandPrompt in registeredApplications.Keys)
                {
                    if (string.Equals(commandPrompt, command, StringComparison.OrdinalIgnoreCase)) return true;
                }
                return false;
            }
        }
        public static void RegisterApplication<T>(string command) where T : TerminalApplication, new()
        {
            if (registeredApplications.ContainsKey(command))
            {
                Plugin.mls.LogError($"An application has already been registered under the command \"{command}\"");
                return;
            }
            registeredApplications.Add(command, () => new T());
        }
        public static void RegisterApplication<T>(string command, bool caseSensitive) where T : TerminalApplication, new()
        {
            RegisterApplication<T>(command);
            commandSensitive.Add(command.ToLower(), caseSensitive);
        }
        public static void RegisterApplication<T>(string[] commands) where T : TerminalApplication, new()
        {
            foreach (string command in commands)
                RegisterApplication<T>(command);
        }
        public static void RegisterApplication<T>(string[] commands, bool caseSensitive) where T : TerminalApplication, new()
        {
            foreach (string command in commands)
                RegisterApplication<T>(command, caseSensitive);
        }
    }
}
