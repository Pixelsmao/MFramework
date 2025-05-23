using UnityEngine;

namespace MFramework.Internal
{
    public static class MFrameworkIcons
    {
        public static Texture Github { get; }
        public static Texture Gitee { get; }

        static MFrameworkIcons()
        {
            Github = Resources.Load<Texture>("Icons/icons-github-128");
            Gitee = Resources.Load<Texture>("Icons/icons-gitee-64");
        }
    }
}