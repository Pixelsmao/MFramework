using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MFramework
{
    public class HorizontalMenuOptionGroup : MenuOptionGroup
    {
        public int menuOptionHeight = 35;

        public int menuOptionXCount
        {
            set => xCount = value;
        }

        public HorizontalMenuOptionGroup(int xCount = 1)
        {
            base.xCount = xCount;
        }

        public override void DrawMenuGroup(Rect rect, int menuWidth, Vector4 padding)
        {
            var drawPos = new Vector2(rect.x + padding.x, rect.y + padding.y);
            var drawSize = new Vector2(rect.width - padding.x + padding.z, rect.height - padding.y - padding.w);
            var drawRect = new Rect(drawPos, drawSize);
            GUILayout.BeginArea(drawRect);
            //菜单
            var menuSize = new Vector2(menuWidth, drawRect.height);
            var menuRect = new Rect(drawPos, menuSize);
            GUILayout.BeginArea(menuRect, GUI.skin.box);
            EditorGUILayout.BeginVertical();
            var menuOptionStyle = new GUIStyle(GUI.skin.button) { alignment = menuOptionTextAnchor };
            menuOptionIndex = GUILayout.SelectionGrid(menuOptionIndex, menuOptionContents, xCount, menuOptionStyle,
                GUILayout.Height(menuOptionHeight * menuOptions.Count));
            EditorGUILayout.EndVertical();
            GUILayout.EndArea();
            //菜单内容
            var contentPos = new Vector2(drawPos.x + menuWidth, drawPos.y);
            var contentSize = new Vector2(drawRect.width - menuWidth, drawRect.height);
            var contentRect = new Rect(contentPos, contentSize);
            GUILayout.BeginArea(contentRect);
            menuOptions[menuOptionIndex].optionDrawing?.Invoke();
            GUILayout.EndArea();
            GUILayout.EndArea();
        }

        public override void DrawMenuGroup()
        {
            EditorGUILayout.BeginVertical();
            var menuOptionStyle = new GUIStyle() { alignment = menuOptionTextAnchor };
            menuOptionIndex = GUILayout.SelectionGrid(menuOptionIndex, menuOptionContents, xCount, menuOptionStyle,
                GUILayout.Height(menuOptionHeight * menuOptions.Count));
            EditorGUILayout.EndVertical();
        }
    }
}