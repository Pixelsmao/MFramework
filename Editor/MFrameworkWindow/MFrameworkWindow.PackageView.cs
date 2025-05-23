using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using MFramework.UnityApplication;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

#pragma warning disable CS4014

namespace MFramework.Internal
{
    internal partial class MFrameworkWindow
    {
        #region MFramework Fields

        private GiteeUser giteeUser;
        private GithubUser githubUser;

        #endregion

        private VerticalMenuOptionGroup packageMenu;
        private EditorWindow packageManagerWindow;
        private OfflinePackage[] offlinePackages;
        private GUIStyle packageNameStyle;

        private static string manifestFilePath =>
            Path.Combine(ApplicationUtility.applicationDirectory, "Packages", "manifest.json");

        private readonly GUIContent packageInstallButton =
            new("Install", "使用导入对话框导入离线包\n Show import dialog when install offline package.");

        private readonly GUIContent repositoryInstallButton =
            new("Install", "从远程仓库安装代码\n install the code from the remote repository.");

        private readonly GUIContent repositoryRemoveButton =
            new("Remove", "移除远程仓库代码\n remove the remote repository code.");

        private readonly GUIContent repositoryUpdateButton =
            new("Update", "更新远程仓库代码\n update the remote repository code.");

        private readonly GUIContent packageSilentInstallButton =
            new("Silent Install", "静默导入离线包\n Silently import offline packages.");

        private Vector2 scrollViewPosition;
        private SearchField offlinePackagesSearchField;
        private string searchText = string.Empty;

        private void InitPackageView()
        {
            InitPackageViewStyles();
            InitPackageViewMenu();
        }

        private void InitPackageViewStyles()
        {
            packageNameStyle = new GUIStyle
            {
                alignment = TextAnchor.MiddleLeft, fontStyle = FontStyle.Bold,
                fontSize = 16,
                normal =
                {
                    textColor = new Color(0.7f, 0.7f, 0.7f)
                }
            };
            offlinePackages = SearchOfflinePackages();
            offlinePackagesSearchField = new SearchField();
        }

        private void InitPackageViewMenu()
        {
            giteeUser = new GiteeUser(LoadOrCreateGiteeConfig());
            githubUser = new GithubUser(LoadOrCreateGithubConfig());
            packageMenu = new VerticalMenuOptionGroup(4)
            {
                menuOptionTextAnchor = TextAnchor.MiddleCenter
            };
            giteeUser.TryRequestRepositories();
            foreach (var repository in giteeUser.repositories)
            {
                repository.IsInstalled =
                    UnityPackageManager.IsInstalled(repository.ManifestKey, repository.ManifestValue);
            }

            //TODO 完善github请求信息
            githubUser.TryRequestRepositories();
            packageMenu.AddMenuOption("Github", PackageView_GithubGUI);
            packageMenu.AddMenuOption("Gitee", PackageView_GiteeGUI);
            packageMenu.AddMenuOption("Offline", PackageView_OfflinePackageViewGUI);
            packageMenu.AddMenuOption("Favorite", () => GUILayout.Label("Favorite", EditorStyles.boldLabel));
        }

        private void PackageView_GiteeGUI()
        {
            //搜索框
            DrawSearchBox();
            //清单修复提示
            //DrawManifestFixHint();
            //包列表
            DrawPackagesScrollView(giteeUser, MFrameworkIcons.Gitee);
        }

        private void PackageView_GithubGUI()
        {
            //搜索框
            DrawSearchBox();
            //清单修复提示
            //DrawManifestFixHint();
            //包列表
            DrawPackagesScrollView(githubUser, MFrameworkIcons.Github);
        }

        private void PackageView_OfflinePackageViewGUI()
        {
            //搜索框
            DrawSearchBox();
            //包列表
            scrollViewPosition = GUILayout.BeginScrollView(scrollViewPosition, false, true,
                GUI.skin.horizontalScrollbar,
                GUI.skin.verticalScrollbar);
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.EndHorizontal();
            foreach (var package in offlinePackages)
            {
                if (!package.packageName.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    continue;
                EditorGUIDrawer.DrawHorizontalLine(new Color(0.1f, 0.1f, 0.1f));
                EditorGUILayout.BeginHorizontal(GUILayout.Height(85));
                GUILayout.Space(10);
                //包信息
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal(GUILayout.Height(30));

                GUILayout.Label(new GUIContent(package.packageName, package.icon), packageNameStyle,
                    GUILayout.Height(35));
                EditorGUILayout.EndHorizontal();
                GUI.enabled = false;
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(40);
                EditorGUILayout.LabelField($"Version: {package.version}");
                EditorGUILayout.LabelField($"Author: {package.author}");
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(40);
                EditorGUILayout.LabelField($"Description: {package.description}");
                EditorGUILayout.EndHorizontal();
                GUI.enabled = true;
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(40);
                EditorGUIDrawer.DrawUrl(string.Empty, package.url, () => package.Locate());
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();

                //按钮
                EditorGUILayout.BeginVertical(GUILayout.Width(60));
                GUILayout.FlexibleSpace();
                if (GUILayout.Button(packageInstallButton))
                {
                    UnityPackageManager.Import(package.url, true);
                }

                if (GUILayout.Button(packageSilentInstallButton))
                {
                    UnityPackageManager.Import(package.url, false);
                }

                GUILayout.FlexibleSpace();
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
        }

        private void DrawSearchBox()
        {
            EditorGUIDrawer.DrawHorizontalLine(new Color(0.1f, 0.1f, 0.1f), padding: 4);
            GUILayout.BeginHorizontal();
            searchText = offlinePackagesSearchField.OnToolbarGUI(searchText);
            if (GUILayout.Button("Clear", GUI.skin.button, GUILayout.Width(80)))
            {
                searchText = string.Empty;
                GUIUtility.keyboardControl = 0; // 清除焦点
                Repaint(); // 强制刷新界面
            }

            GUILayout.Space(10);
            GUILayout.EndHorizontal();
        }

        private void DrawManifestFixHint()
        {
            EditorGUILayout.BeginHorizontal(GUILayout.Height(35));
            EditorGUILayout.BeginVertical(GUILayout.Width(350));
            EditorGUILayout.HelpBox("如果在安装代码后显示包清单有误，请点击按钮编辑manifest.json清单文件，手动删除或修复其中有错误的依赖项，然后刷新编辑器。",
                MessageType.Warning);
            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            if (GUILayout.Button(new GUIContent("Edit Manifest", "编辑清单文件"), GUI.skin.button,
                    GUILayout.Width(100)))
            {
                Process.Start(manifestFilePath);
            }

            if (GUILayout.Button(new GUIContent("Refresh Editor", "刷新编辑器"), GUI.skin.button, GUILayout.Width(100)))
            {
                UnityEditor.PackageManager.Client.Resolve();
                AssetDatabase.Refresh();
            }

            EditorGUILayout.EndVertical();
            EditorGUILayout.BeginVertical();
            EditorGUILayout.HelpBox("国内环境请禁用Unity系统代理并从Gitee进行安装;" +
                                    "\n国际环境请启用Unity系统代理并从Github进行安装。", MessageType.Info);
            EditorGUILayout.EndVertical();
            //EditorGUILayout.BeginVertical();
            // if (GUILayout.Button(new GUIContent("Enable Proxy", "启用Unity系统代理，选择是否重启编辑器。"), GUI.skin.button,
            //         GUILayout.Width(100)))
            // {
            //     if (EditorUtility.DisplayDialog("Unity系统代理变更提醒", "更改Unity系统代理需要重启编辑器才能生效，请确认是否有未保存的工作，然后立即重启编辑器？",
            //             "立即重启", "稍后手动重启"))
            //     {
            //         ChangeProxy(true, true);
            //     }
            //     else
            //     {
            //         ChangeProxy(true, false);
            //     }
            // }
            //
            // if (GUILayout.Button(new GUIContent("Disable Proxy", "禁用Unity系统代理，选择是否重启编辑器。"), GUI.skin.button,
            //         GUILayout.Width(100)))
            // {
            //     if (EditorUtility.DisplayDialog("Unity系统代理变更提醒", "更改Unity系统代理需要重启编辑器才能生效，请确认是否有未保存的工作，然后立即重启编辑器？",
            //             "立即重启", "稍后手动重启"))
            //     {
            //         ChangeProxy(false, true);
            //     }
            //     else
            //     {
            //         ChangeProxy(false, false);
            //     }
            // }

            //EditorGUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            EditorGUILayout.EndHorizontal();
            EditorGUILayout.Space(10);
        }

        private bool onlyUnityPackage;

        private void DrawPackagesScrollView(User user, Texture icon)
        {
            GUILayout.Space(10);
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            EditorGUILayout.LabelField("Only Show Unity Package Repository", GUILayout.Width(220));
            onlyUnityPackage = EditorGUILayout.Toggle(onlyUnityPackage, GUILayout.Width(20));
            GUILayout.Space(10);
            EditorGUILayout.EndHorizontal();
            scrollViewPosition = GUILayout.BeginScrollView(scrollViewPosition, false, true,
                GUI.skin.horizontalScrollbar, GUI.skin.verticalScrollbar);
            if (user.repositories.Count == 0)
            {
                EditorGUILayout.HelpBox("没有可用的远程仓库代码，请依次检查以下项目：" +
                                        "\n1. MFramework目录中的远程账号配置文件是否填写正确；" +
                                        "\n2. 如果是国内环境，请检查是否清除了Unity系统代理全局变量，同时计算机关闭网络全局代理；" +
                                        "\n3. 如果是国际环境，请检查是否配置了了Unity系统代理全局变量，同时计算机开启网络全局代理；", MessageType.Warning);
                EditorGUIDrawer.DrawUrl("有关Unity代理设置，请参考：",
                    "https://docs.unity.cn/cn/2020.3/Manual/upm-config-network.html");
            }

            foreach (var repository in user.repositories)
            {
                if (onlyUnityPackage && !repository.IsUnityPackage) continue;
                if (!repository.Name.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                    continue;
                EditorGUIDrawer.DrawHorizontalLine(new Color(0.1f, 0.1f, 0.1f));
                EditorGUILayout.BeginHorizontal(GUILayout.Height(85));
                GUILayout.Space(10);
                //包信息
                EditorGUILayout.BeginVertical();
                EditorGUILayout.BeginHorizontal(GUILayout.Height(30));

                GUILayout.Label(new GUIContent($" {repository.Name}", icon), packageNameStyle,
                    GUILayout.Height(35));
                EditorGUILayout.EndHorizontal();
                GUI.enabled = false;
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(40);
                EditorGUILayout.LabelField($"Manifest Name: {repository.ManifestKey}");
                EditorGUILayout.LabelField($"Author: {repository.Author}");
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(40);
                EditorGUILayout.LabelField($"Description: {repository.Description}");
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(40);
                EditorGUILayout.EndHorizontal();
                GUI.enabled = true;
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(40);
                EditorGUIDrawer.DrawUrl("", repository.HtmlUrl);
                EditorGUILayout.EndHorizontal();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(40);
                if (!repository.IsUnityPackage)
                {
                    EditorGUILayout.LabelField(new GUIContent("Is Not Unity Package Repository",
                        EditorGUIUtility.IconContent("d_console.warnicon.sml").image));
                }

                EditorGUILayout.EndHorizontal();
                EditorGUILayout.EndVertical();

                //按钮
                EditorGUILayout.BeginVertical(GUILayout.Width(60));
                GUILayout.FlexibleSpace();
                GUI.enabled = repository.IsUnityPackage &&
                              !UnityPackageManager.IsInstalled(repository.ManifestKey, repository.ManifestValue);
                if (GUILayout.Button(repositoryInstallButton, GUILayout.Width(100))) repository.Install();
                GUI.enabled = repository.IsUnityPackage &&
                              UnityPackageManager.IsInstalled(repository.ManifestKey, repository.ManifestValue);
                if (GUILayout.Button(repositoryUpdateButton, GUILayout.Width(100))) repository.Update();
                if (GUILayout.Button(repositoryRemoveButton, GUILayout.Width(100))) repository.Uninstall();
                GUI.enabled = true;
                GUILayout.FlexibleSpace();
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
            }

            GUILayout.EndScrollView();
        }

        private static string enableProxyBat =>
            Path.Combine(Application.streamingAssetsPath, "EnableUnitySystemProxy.bat");

        private static string disableProxyBat =>
            Path.Combine(Application.streamingAssetsPath, "DisableUnitySystemProxy.bat");


        private static OfflinePackage[] SearchOfflinePackages()
        {
            var offLineDirInfo =
                new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "Unity"));
            return offLineDirInfo.GetFiles("*.unitypackage", SearchOption.AllDirectories)
                .Select(file =>
                    new OfflinePackage(file.Name, url: file.FullName, type: OfflinePackage.PackageType.Offline))
                .ToArray();
        }
    }
}