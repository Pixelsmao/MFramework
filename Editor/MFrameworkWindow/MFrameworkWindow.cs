using System;
using System.Reflection;
using MFramework.EditorExtensions;
using UnityEditor;
using UnityEngine;

namespace MFramework.Internal
{
    internal partial class MFrameworkWindow : EditorWindowBase<MFrameworkWindow>
    {
        private HorizontalMenuOptionGroup mainMenu;

        private void OnEnable()
        {
            //主菜单
            InitMainMenu();
            //包视图
            InitPackageView();
        }

        private void InitMainMenu()
        {
            mainMenu = new HorizontalMenuOptionGroup();
            mainMenu.AddMenuOption("Package Manager", EditorGUIIcons.PackageInstalled.image,
                () => packageMenu.DrawMenuGroup());
            mainMenu.AddMenuOption("Editor Tools", EditorGUIIcons.UnityLoge.image, EditorToolsViewGUI);
            InitPackageViewMenu();
        }

        protected void OnGUI()
        {
            var titleRect = EditorGUIDrawer.DrawRectArea(Vector2.zero, new Vector2(this.position.width, 80),
                new Color(0.2f, 0.2f, 0.2f), new Color(0.13f, 0.13f, 0.13f), 1, () =>
                {
                    EditorGUILayout.Space(5);
                    GUILayout.Label("MFramework Manager", EditorGUIStyles.MainTitleStyle);
                    GUILayout.FlexibleSpace();
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Label("Wonderful things start from here.", EditorGUIStyles.SubTitleStyle);
                    GUILayout.Label("Wonderful things start from here.", EditorGUIStyles.SubTitleStyle);
                    GUILayout.Label("Wonderful things start from here.", EditorGUIStyles.SubTitleStyle);
                    EditorGUILayout.EndHorizontal();
                    EditorGUILayout.Space(10);
                });
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
        public static void ShowMainWindow()
        {
            ShowWindow();
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