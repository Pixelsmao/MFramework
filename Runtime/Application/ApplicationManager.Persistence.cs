using System;
using System.Globalization;
using System.IO;
using MFramework.Extensions.DataType;
using UnityEngine;

namespace MFramework.UnityApplication
{
    public partial class ApplicationManager
    {
        [Header("应用程序设置")] [Tooltip("全屏模式")] public FullScreenMode fullScreenMode = FullScreenMode.FullScreenWindow;
        [Tooltip("分辨率宽度(全屏模式为0时生效)")] public int resolutionWidth = 1920;
        [Tooltip("分辨率高度(全屏模式为0时生效)")] public int resolutionHeight = 1080;
        [Tooltip("限制FPS")] public bool limitFPS = true;

        [Tooltip("应用程序目标帧率(在limitFPS为True时生效)")]
        public int applicationFPS = 60;

        [Tooltip("启动时置顶应用程序")] public bool topmostOnAwake;
        [Tooltip("启动时显示光标")] public bool showCursorOnAwake = true;
        [Tooltip("收集运行时日志")] public bool collectRuntimeLogs = true;
        [Tooltip("创建桌面快捷方式")] public bool shortcutToDesktop = true;
        [Tooltip("创建启动快捷方式")] public bool shortcutToStartup = true;

        [Header("GUI")] [Tooltip("显示GUI信息")] public bool showGUI = false;
        [Tooltip("切换显示GUI信息键盘值")] public KeyCode showGUIKeyCode = KeyCode.F3;

        private void ApplyApplicationSettings()
        {
            if (fullScreenMode == FullScreenMode.ExclusiveFullScreen)
                Screen.SetResolution(resolutionWidth, resolutionHeight, true);
            Screen.fullScreenMode = fullScreenMode;
            if (limitFPS) Application.targetFrameRate = applicationFPS;
            Cursor.visible = showCursorOnAwake;
            if (topmostOnAwake) ApplicationWindow.Topmost();
            if (shortcutToDesktop) ApplicationUtility.CreateDesktopShortcut();
            else ApplicationUtility.DeleteDesktopShortcut();
            if (shortcutToStartup) ApplicationUtility.CreateStartupShortcut();
            else ApplicationUtility.DeleteStartupShortcut();
        }

        private void CollectPlayerLog()
        {
            if (!collectRuntimeLogs) return;
            Directory.CreateDirectory(ApplicationUtility.playerLogCollectDirectory);
            var runtimeLogPath = ApplicationUtility.playerLogPath;
            var collectRuntimeLogDirectory = ApplicationUtility.playerLogCollectDirectory;
            var destFileName = Path.Combine(collectRuntimeLogDirectory,
                $"【{DateTime.Now.ToString(CultureInfo.CurrentCulture).RemovePunctuation()}】Player.log");
            if (File.Exists(runtimeLogPath)) File.Copy(runtimeLogPath, destFileName, true);
        }
    }
}