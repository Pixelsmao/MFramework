using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;

namespace MFramework.Internal
{
    public abstract class RemoteRepository
    {
        public abstract string HtmlUrl { get; set; }
        public abstract string Name { get; set; }
        public abstract string Description { get; set; }
        public abstract string Author { get; }
        public abstract bool IsUnityPackage { get; set; }
        public string ManifestKeyValuePair => $"\"{ManifestKey}\":\"{ManifestValue}\"";
        public abstract string ManifestKey { get; }
        public string ManifestValue => HtmlUrl;

        /// <summary>
        /// 是否已经安装，检查属性值之前需要调用Refresh()刷新属性状态
        /// </summary>
        public bool IsInstalled { get; set; }

        public void Install() => UnityPackageManager.Install(ManifestKey, ManifestValue);

        public void Update()
        {
            if (Client.Add(HtmlUrl).Error != null)
            {
                Debug.LogError($"{ManifestKeyValuePair}更新失败");
            }
            else
            {
                Debug.Log($"{ManifestKeyValuePair}更新成功!");
            }

            UnityEditor.PackageManager.Client.Resolve();
            AssetDatabase.Refresh();
        }

        public void Uninstall() => UnityPackageManager.Uninstall(ManifestKey, ManifestValue);
    }
}