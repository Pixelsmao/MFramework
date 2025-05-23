using UnityEditor;
using UnityEngine;

namespace MFramework
{
    public static class EditorTexturesLibrary
    {
        public static GUIContent packageInstalled { get; }
        public static GUIContent unityLogo { get; }

        public static Texture MFramework => EditorGUIUtility.FindTexture("Icons/MFramework");

        static EditorTexturesLibrary()
        {
            packageInstalled = EditorGUIUtility.IconContent("package_installed");
            unityLogo = EditorGUIUtility.IconContent("UnityLogo");
        }
    }
}