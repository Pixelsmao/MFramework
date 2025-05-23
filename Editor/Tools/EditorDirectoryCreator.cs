using System.IO;
using UnityEditor;

namespace MFramework
{
    public static class EditorDirectoryCreator
    {
        private static string selectedObjectPath => AssetDatabase.GetAssetPath(Selection.activeObject);
        private const string createMenuDirectory = "Assets/Create Special Folder";
        private const bool isValidateFunction = false;
        private const int priority = -200;

        [MenuItem("Assets/Create/Directory/Editor", isValidateFunction, priority)]
        public static void CreateEditorFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "Editor");
        }

        [MenuItem("Assets/Create/Directory/Scenes", isValidateFunction, priority)]
        public static void CreateScenesFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "Scenes");
        }

        [MenuItem("Assets/Create/Directory/Prefabs", isValidateFunction, priority)]
        public static void CreatePrefabsFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "Prefabs");
        }

        [MenuItem("Assets/Create/Directory/Audio", isValidateFunction, priority)]
        public static void CreateAudioFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "Audio");
        }

        [MenuItem("Assets/Create/Directory/Materials", isValidateFunction, priority)]
        public static void CreateMaterialsFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "Materials");
        }

        [MenuItem("Assets/Create/Directory/ArtResources", isValidateFunction, priority)]
        public static void CreateArtResourcesFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "ArtResources");
        }

        [MenuItem("Assets/Create/Directory/UI", isValidateFunction, priority)]
        public static void CreateUIFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "UI");
        }

        [MenuItem("Assets/Create/Directory/Animations", isValidateFunction, priority)]
        public static void CreateAnimationsFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "Animations");
        }

        [MenuItem("Assets/Create/Directory/Models", isValidateFunction, priority)]
        public static void CreateModelsFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "Models");
        }

        [MenuItem("Assets/Create/Directory/Textures", isValidateFunction, priority)]
        public static void CreateTexturesFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "Textures");
        }

        [MenuItem("Assets/Create/Directory/Editor Default Resources", isValidateFunction, priority)]
        public static void CreateEditorDefaultResourcesFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "Editor Default Resources");
        }

        [MenuItem("Assets/Create/Directory/Scripts", isValidateFunction, priority)]
        public static void CreateScriptsFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "Scripts");
        }

        [MenuItem("Assets/Create/Directory/Plugins", isValidateFunction, priority)]
        public static void CreatePluginsFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "Plugins");
        }

        [MenuItem("Assets/Create/Directory/Resources", isValidateFunction, priority)]
        public static void CreateResourcesFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "Plugins");
        }

        [MenuItem("Assets/Create/Directory/StreamingAssets", isValidateFunction, priority)]
        public static void CreateStreamingAssetsFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "StreamingAssets");
        }

        [MenuItem("Assets/Create/Directory/Standard Assets", isValidateFunction, priority)]
        public static void CreateStandardAssetsFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "Standard Assets");
        }

        [MenuItem("Assets/Create/Directory/Gizmos", isValidateFunction, priority)]
        public static void CreateGizmosFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "Gizmos");
        }

        [MenuItem("Assets/Create/Directory/Shaders", isValidateFunction, priority)]
        public static void CreateShadersFolder()
        {
            CreateDirectory(GetSelectedPathOrFallback(), "Shaders");
        }

        public static void CreateDirectory(string parentDirectory, string directoryName)
        {
            AssetDatabase.CreateFolder(parentDirectory, directoryName);
        }

        private static string GetSelectedPathOrFallback()
        {
            var path = "Assets";
            foreach (var obj in Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.Assets))
            {
                path = AssetDatabase.GetAssetPath(obj);
                if (!string.IsNullOrEmpty(path) && File.Exists(path))
                {
                    path = System.IO.Path.GetDirectoryName(path);
                }

                break;
            }

            return path;
        }
    }
}