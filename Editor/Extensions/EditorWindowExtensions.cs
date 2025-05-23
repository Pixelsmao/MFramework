using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MFramework
{
    public static class EditorWindowExtensions
    {
        public static bool IsShowing(this EditorWindow window)
        {
            var assembly = typeof(EditorWindow).Assembly;
            return assembly.GetTypes().FirstOrDefault(type => type.BaseType == window.GetType()) != null;
        }
    }
}