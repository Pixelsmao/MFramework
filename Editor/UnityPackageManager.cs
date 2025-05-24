using System;
using System.Collections.Generic;
using System.IO;
using MFramework.UnityApplication;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace MFramework.Internal
{
    public static class UnityPackageManager
    {
        private static string manifestPath =>
            Path.Combine(ApplicationUtility.applicationDirectory, "Packages", "manifest.json");

        private static UnityManifest manifest;

        static UnityPackageManager()
        {
            manifest = JsonConvert.DeserializeObject<UnityManifest>(File.ReadAllText(manifestPath));
        }

        public static bool IsInstalled(string manifestKey, string manifestValue)
        {
            return manifest != null && manifest.IsInstalled(manifestKey, manifestValue);
        }

        public static void Install(string manifestKey, string manifestValue)
        {
            if (IsInstalled(manifestKey, manifestValue))
            {
                Debug.Log($"{manifestKey}:{manifestValue} 已经安装!");
                return;
            }

            var manifestContent = File.ReadAllText(manifestPath);
            var newManifest = JsonConvert.DeserializeObject<UnityManifest>(manifestContent);
            // ReSharper disable once PossibleNullReferenceException
            foreach (var keyValuePair in newManifest.dependencies)
            {
                // ReSharper disable once PossibleNullReferenceException
                if (!manifest.IsInstalled(manifestKey, manifestValue))
                {
                    manifest?.AddManifest(manifestKey, manifestValue);
                }
            }

            File.WriteAllText(manifestPath, JsonConvert.SerializeObject(manifest, Formatting.Indented));
            UnityEditor.PackageManager.Client.Resolve();
            AssetDatabase.Refresh();
        }

        public static void Uninstall(string manifestKey, string manifestValue)
        {
            if (!IsInstalled(manifestKey, manifestValue))
            {
                Debug.Log($"{manifestKey}:{manifestValue} 没有安装!");
                return;
            }

            var manifestContent = File.ReadAllText(manifestPath);
            var newManifest = JsonConvert.DeserializeObject<UnityManifest>(manifestContent);
            // ReSharper disable once PossibleNullReferenceException
            foreach (var keyValuePair in newManifest.dependencies)
            {
                // ReSharper disable once PossibleNullReferenceException
                if (manifest.IsInstalled(manifestKey, manifestValue))
                {
                    manifest?.RemoveManifest(manifestKey, manifestValue);
                }
            }

            File.WriteAllText(manifestPath, JsonConvert.SerializeObject(manifest, Formatting.Indented));
            UnityEditor.PackageManager.Client.Resolve();
            AssetDatabase.Refresh();
        }

        /// <summary>
        /// 从计算机中导入*.unitypackage文件
        /// </summary>
        /// <param name="packagePath">文件路径</param>
        /// <param name="enableImportDialog">导入时是否显示导入对话框</param>
        public static void Import(string packagePath, bool enableImportDialog)
        {
            if (File.Exists(packagePath))
            {
                AssetDatabase.ImportPackage(packagePath, enableImportDialog);
            }

            Debug.LogError($"指定的文件不存在: {packagePath}");
        }
    }
}