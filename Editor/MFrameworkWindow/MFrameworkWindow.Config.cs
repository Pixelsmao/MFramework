using System.IO;
using MFramework.UnityApplication;
using UnityEditor;
using UnityEditor.Compilation;
using UnityEngine;

namespace MFramework.Internal
{
    internal partial class MFrameworkWindow
    {
        private static readonly string githubConfigPath = "Assets\\MFramework\\GithubUserConfig.asset";

        private static readonly string giteeConfigPath = "Assets\\MFramework\\GiteeUserConfig.asset";

        [InitializeOnLoadMethod]
        private static void Initialize()
        {
            CompilationPipeline.compilationFinished += OnCompilationFinished;
        }

        private static UserConfig LoadOrCreateGithubConfig()
        {
            return !File.Exists(githubConfigPath)
                ? CreateConfig(githubConfigPath)
                : AssetDatabase.LoadAssetAtPath<UserConfig>(githubConfigPath);
        }

        private static UserConfig LoadOrCreateGiteeConfig()
        {
            return !File.Exists(giteeConfigPath)
                ? CreateConfig(giteeConfigPath)
                : AssetDatabase.LoadAssetAtPath<UserConfig>(giteeConfigPath);
        }

        private static void OnCompilationFinished(object obj)
        {
            if (!AssetDatabase.AssetPathExists(giteeConfigPath)) CreateConfig(giteeConfigPath);
            if (!AssetDatabase.AssetPathExists(githubConfigPath)) CreateConfig(githubConfigPath);
        }

        private static UserConfig CreateConfig(string configPath)
        {
            Debug.Log($"创建用户配置文件{configPath}");
            var userConfig = CreateInstance<UserConfig>();
            AssetDatabase.CreateAsset(userConfig, configPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return userConfig;
        }
    }
}