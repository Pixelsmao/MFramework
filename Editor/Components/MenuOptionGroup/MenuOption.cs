using System;
using UnityEngine;

namespace MFramework
{
    public class MenuOption
    {
        public string optionName { get; }
        public Texture optionIcon { get; }
        public Action optionDrawing { get; }

        public GUIContent option =>
            optionIcon == null ? new GUIContent(optionName) : new GUIContent(optionName, optionIcon);

        public MenuOption(string optionName, Action optionDrawing)
        {
            this.optionName = optionName;
            this.optionDrawing = optionDrawing;
        }

        public MenuOption(string optionName, Texture optionIcon, Action optionDrawing)
        {
            this.optionName = optionName;
            this.optionIcon = optionIcon;
            this.optionDrawing = optionDrawing;
        }
    }
}