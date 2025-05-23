using UnityEngine;

namespace MFramework.UnityApplication
{
    public abstract class GUIWindow : MonoBehaviour
    {
        private void OnGUI()
        {
            GUILayout.Label($"<b>{windowTitle}</b>");
            DoGUI();
        }

        protected abstract string windowTitle { get; }

        public abstract void DoGUI();
    }
}