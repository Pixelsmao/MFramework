using System;
using MFramework;
using UnityEditor;
using UnityEngine;

public static class EditorGUIDrawer
{
    public static void DrawHorizontalLine(Color color, int thickness = 1, int padding = 10)
    {
        var lineRect = EditorGUILayout.GetControlRect(false, thickness + padding);
        lineRect.height = thickness;
        lineRect.y += padding * 0.5f;
        EditorGUI.DrawRect(lineRect, color);
    }

    public static void DrawVerticalLine(Color color, int thickness = 1, int padding = 10)
    {
        var lineRect = EditorGUILayout.GetControlRect(false, thickness + padding);
        lineRect.width = thickness;
        lineRect.x += padding * 0.5f;
        EditorGUI.DrawRect(lineRect, color);
    }

    public static void DrawUrl(string label, string url, Action onClick = null)
    {
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField(label);
        EditorGUILayout.EndVertical();
        EditorGUILayout.BeginVertical();
        EditorGUILayout.LabelField(url, GUIStyleLibrary.urlStyle);
        Rect linkRect = GUILayoutUtility.GetLastRect();
        EditorGUIUtility.AddCursorRect(linkRect, MouseCursor.Link);
        // 处理点击事件
        if (Event.current.type == EventType.MouseDown && linkRect.Contains(Event.current.mousePosition))
        {
            if (onClick != null) onClick.Invoke();
            else Application.OpenURL(url);
            Event.current.Use();
        }


        EditorGUILayout.EndVertical();
        EditorGUILayout.EndHorizontal();
    }
}