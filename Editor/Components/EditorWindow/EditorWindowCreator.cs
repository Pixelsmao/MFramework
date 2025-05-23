using UnityEditor;
using UnityEngine;

namespace MFramework
{
    public abstract class EditorWindowCreator<T> : EditorWindow where T : EditorWindowCreator<T>
    {
        private static Resolution resolution => Screen.currentResolution;
        protected static Vector2 defaultWindowPos => new Vector2(resolution.width / 6f, resolution.height / 5f);
        protected static Vector2 defaultWindowSize => new Vector2(resolution.width / 2f, resolution.height / 2f);
        protected Vector2 WindowMinSize { get; set; } = new Vector2(300, 300);

        // 窗口默认标题（子类可重写）
        public virtual string WindowTitle => typeof(T).Name;

        // 窗口最小尺寸（子类可重写）
        protected virtual Vector2 MinSize => new Vector2(200, 100);

        // 窗口最大尺寸（子类可重写）
        protected virtual Vector2 MaxSize => new Vector2(400, 400);

        public static void ShowWindow()
        {
            var window = GetWindow<T>();
            window.titleContent = new GUIContent(window.WindowTitle);
            window.position = new Rect(defaultWindowPos, defaultWindowSize);
            window.minSize = window.MinSize;
            window.Show();
        }
    }
}