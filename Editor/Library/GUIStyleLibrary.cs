using UnityEngine;

namespace MFramework
{
    public static class GUIStyleLibrary
    {
        public static GUIStyle urlStyle { get; }


        static GUIStyleLibrary()
        {
            urlStyle = new GUIStyle(GUI.skin.label)
            {
                richText = true,
                normal = { textColor = new Color(0.2f, 0.6f, 1f, 1f) },
            };
        }

        public static GUIStyle middleBackground =>
            new GUIStyle
            {
                normal =
                {
                    background = MakeBackgroundTexture(4, 4, new Color(0.25f, 0.25f, 0.25f, 1f))
                }
            };

        public static GUIStyle bottomBackground =>
            new GUIStyle
            {
                normal =
                {
                    background = MakeBackgroundTexture(4, 4, new Color(0.15f, 0.15f, 0.15f, 1f))
                }
            };

        private static Texture2D MakeBackgroundTexture(int width, int height, Color color)
        {
            var pixels = new Color[width * height];
            for (var i = 0; i < pixels.Length; ++i)
            {
                pixels[i] = color;
            }

            var texture = new Texture2D(width, height);
            texture.SetPixels(pixels);
            texture.Apply();
            return texture;
        }

        public static GUIStyle MainTitleStyle { get; } = new()
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 25,
            fontStyle = FontStyle.Bold,
            normal = new GUIStyleState()
            {
                textColor = Color.white
            }
        };

        public static GUIStyle SubTitleStyle { get; } = new()
        {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 12,
            fontStyle = FontStyle.Normal,
            normal = new GUIStyleState()
            {
                textColor = new Color(0.5f, 0.5f, 0.5f)
            }
        };
    }
}