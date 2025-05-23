using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace MFramework.Internal
{
    internal partial class MFrameworkWindow : EditorWindowCreator<MFrameworkWindow>
    {
        private RectArea titleArea;
        private RectArea mainMenuArea;
        private HorizontalMenuOptionGroup mainMenu;

        private void OnEnable()
        {
            //标题
            InitTitle();
            //主菜单
            InitMainMenu();
            //包视图
            InitPackageView();
        }

        private void InitTitle()
        {
            titleArea = new RectArea(() =>
            {
                EditorGUILayout.Space(5);
                GUILayout.Label("MFramework Manager", GUIStyleLibrary.MainTitleStyle);
                GUILayout.FlexibleSpace();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Wonderful things start from here.", GUIStyleLibrary.SubTitleStyle);
                GUILayout.Label("Wonderful things start from here.", GUIStyleLibrary.SubTitleStyle);
                GUILayout.Label("Wonderful things start from here.", GUIStyleLibrary.SubTitleStyle);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.Space(10);
            })
            {
                fillColor = new Color(0.2f, 0.2f, 0.2f),
                outlineColor = new Color(0.13f, 0.13f, 0.13f),
                outlineWidth = 1
            };
        }

        private void InitMainMenu()
        {
            mainMenu = new HorizontalMenuOptionGroup();
            mainMenu.AddMenuOption("Package Manager", EditorTexturesLibrary.packageInstalled.image,
                () => packageMenu.DrawMenuGroup());
            mainMenu.AddMenuOption("Editor Tools", EditorTexturesLibrary.unityLogo.image, EditorToolsViewGUI);
            InitPackageViewMenu();
        }

        protected void OnGUI()
        {
            var titleRect = titleArea.DrawRectArea(Vector2.zero, new Vector2(this.position.width, 80));
            var mainMenuPos = new Vector2(0, titleRect.center.y);
            var mainMenuSize = new Vector2(position.width, position.height - titleRect.center.y);
            var mainMenuRect = new Rect(mainMenuPos, mainMenuSize);
            mainMenu.DrawMenuGroup(mainMenuRect, 200, Vector4.zero);
        }

        private void EditorToolsViewGUI()
        {
            //GetWindow(StringToType("UnityEditor.PackageManager.UI.PackageManagerWindow")).Show();
        }

        [MenuItem("MFramework/MFrameworkWindow"), MenuItem("Assets/MFramework", priority = 0)]
        public new static void ShowWindow()
        {
            EditorWindowCreator<MFrameworkWindow>.ShowWindow();
        }

        protected Type StringToType(string typeName)
        {
            Type type = null;
            Assembly[] assemblyArray = AppDomain.CurrentDomain.GetAssemblies();
            int assemblyArrayLength = assemblyArray.Length;
            for (int i = 0; i < assemblyArrayLength; ++i)
            {
                type = assemblyArray[i].GetType(typeName);
                if (type != null)
                {
                    return type;
                }
            }

            for (int i = 0; (i < assemblyArrayLength); ++i)
            {
                Type[] typeArray = assemblyArray[i].GetTypes();
                int typeArrayLength = typeArray.Length;
                for (int j = 0; j < typeArrayLength; ++j)
                {
                    if (typeArray[j].Name.Equals(typeName))
                    {
                        return typeArray[j];
                    }
                }
            }

            return null;
        }
    }
}