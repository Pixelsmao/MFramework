using System;
using UnityEngine;

namespace MFramework.Internal
{
    internal static class NativeMethods
    {
        static void DeleteEnvironmentVariable(EnvironmentVariableTarget target, string variableName)
        {
            try
            {
                Environment.SetEnvironmentVariable(variableName, null, target);
                Console.WriteLine($"已删除 {target} 级别的环境变量: {variableName}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"删除 {target} 级别的 {variableName} 失败: {ex.Message}");
            }
        }

        static void RefreshEnvironmentVariables()
        {
            try
            {
                // 设置并立即删除一个临时变量来触发环境变量刷新
                Environment.SetEnvironmentVariable("DUMMY_ENV", Guid.NewGuid().ToString(),
                    EnvironmentVariableTarget.User);
                Environment.SetEnvironmentVariable("DUMMY_ENV", null, EnvironmentVariableTarget.User);

                // 广播 WM_SETTINGCHANGE 消息以刷新环境变量
                IntPtr hwndBroadcast = (IntPtr)0xFFFF;
                IntPtr wParam = IntPtr.Zero;
                NativeMethods.SendMessageTimeout(hwndBroadcast, 0x1A, wParam, "Environment", 0x0002, 5000, out _);

                Debug.Log("系统环境已刷新");
            }
            catch (Exception ex)
            {
                Debug.Log($"刷新环境变量失败: {ex.Message}");
            }
        }

        static bool IsRunAsAdmin()
        {
            var identity = System.Security.Principal.WindowsIdentity.GetCurrent();
            var principal = new System.Security.Principal.WindowsPrincipal(identity);
            return principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
        }

        static void ElevatePrivileges()
        {
            var exeName = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
            var startInfo = new System.Diagnostics.ProcessStartInfo
            {
                FileName = exeName,
                Verb = "runas",
                UseShellExecute = true
            };

            try
            {
                System.Diagnostics.Process.Start(startInfo);
            }
            catch (System.ComponentModel.Win32Exception)
            {
                Debug.Log("用户拒绝了管理员权限请求");
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true,
            CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern IntPtr SendMessageTimeout(
            IntPtr hWnd,
            uint Msg,
            IntPtr wParam,
            string lParam,
            uint fuFlags,
            uint uTimeout,
            out IntPtr lpdwResult);
    }
}