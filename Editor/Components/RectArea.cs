using System;
using MFramework.EditorExtension;
using UnityEditor;
using UnityEngine;

public class RectArea
{
    public Color fillColor { get; set; } = Color.clear;
    public Color outlineColor { get; set; } = Color.clear;
    public int outlineWidth { get; set; } = 0;
    private Action drawMethod;

    public RectArea(Action drawMethod = null)
    {
        this.drawMethod = drawMethod;
    }

    public Rect DrawRectArea(Vector2 rectPosition, Vector2 rectSize)
    {
        var rect = new Rect(rectPosition, rectSize);
        EditorGUI.DrawRect(rect, fillColor);
        rect.DrawOutline(outlineColor, outlineWidth);
        GUILayout.BeginArea(rect);
        drawMethod?.Invoke();
        GUILayout.EndArea();
        return rect;
    }
}