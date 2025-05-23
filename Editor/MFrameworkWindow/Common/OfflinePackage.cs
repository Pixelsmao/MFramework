using System;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace MFramework.Internal
{
    internal class OfflinePackage
    {
        internal enum PackageType
        {
            Unknown,
            MFramework,
            Favorite,
            Offline,
            Solution
        }

        public bool isInstalled = false;
        public bool isSelected = false;
        public PackageType type { get; }
        public string packageName { get; }
        public string url { get; }
        public string author { get; }
        public string description { get; }
        public string version { get; }
        public string[] dependencies { get; }
        private Texture defaultIcon;

        public Texture icon => GetIcon();

        public OfflinePackage(string packageName, string url, PackageType type = PackageType.Unknown,
            string author = "unknown",
            string description = "No description",
            string version = "unknown",
            string[] dependencies = null, Texture icon = null)
        {
            this.packageName = packageName;
            this.author = author;
            this.description = description;
            this.version = version;
            this.url = url;
            this.dependencies = dependencies;
            this.type = type;
            defaultIcon = icon;
        }
        
        public void Locate()
        {
            if (File.Exists(url))
            {
                Process.Start("explorer.exe", "/select," + url);
            }
        }

        private Texture GetIcon()
        {
            if (defaultIcon != null) return defaultIcon;
            switch (type)
            {
                case PackageType.Unknown:
                    return EditorTexturesLibrary.unityLogo.image;
                case PackageType.MFramework:
                    return EditorTexturesLibrary.MFramework;
                case PackageType.Favorite:
                    return EditorTexturesLibrary.unityLogo.image;
                case PackageType.Offline:
                    return EditorTexturesLibrary.unityLogo.image;
                case PackageType.Solution:
                    return EditorTexturesLibrary.unityLogo.image;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}