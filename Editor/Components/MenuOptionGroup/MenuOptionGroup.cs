using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MFramework
{
    public abstract class MenuOptionGroup
    {
        public TextAnchor menuOptionTextAnchor { get; set; } = TextAnchor.MiddleLeft;
        protected readonly List<MenuOption> menuOptions = new List<MenuOption>();
        protected readonly List<MenuOption> menuButtons = new List<MenuOption>();
        protected int menuOptionIndex = 0;
        protected int xCount = 1;
        protected GUIContent[] menuOptionContents => menuOptions.Select(option => option.option).ToArray();

        public abstract void DrawMenuGroup(Rect rect, int menuWidth, Vector4 padding);
        public abstract void DrawMenuGroup();

        public void AddMenuOption(string menuOptionName, Action menuOptionDrawing) =>
            menuOptions.Add(new MenuOption(menuOptionName, menuOptionDrawing));

        public void AddMenuOption(string menuOptionName, Texture menuOptionIcon, Action menuOptionDrawing) =>
            menuOptions.Add(new MenuOption(menuOptionName, menuOptionIcon, menuOptionDrawing));

        public void AddMenuButton(string menuOptionName, Action menuOptionDrawing) =>
            menuButtons.Add(new MenuOption(menuOptionName, menuOptionDrawing));

        public void AddMenuButton(string menuOptionName, Texture menuOptionIcon, Action menuOptionDrawing) =>
            menuButtons.Add(new MenuOption(menuOptionName, menuOptionIcon, menuOptionDrawing));
    }
}