using System.Diagnostics;
using System.Globalization;
using MFramework.DataPersistence;
using Unity.Collections;
using UnityEngine;
using UnityEngine.Profiling;

// ReSharper disable UnusedMember.Local

namespace MFramework.UnityApplication
{
    public partial class ApplicationManager
    {

        #region Fields

        private float fps;
        private string screenCountText { get; set; }
        private string screenFPSText { get; set; }
        private string currentScreenResolutionText { get; set; }
        private string currentScreenDPIText { get; set; }

        #endregion

        private int fpsFrames;
        private float lastInterval;
        //private static PerformanceCounter cpuCounter;
        private const int KbDiv = 1024;
        private const int MbDiv = 1024 * 1024;
        private const int GbDIV = 1024 * 1024 * 1024;
        

        private void InitHardwareInfos()
        {
            lastInterval = Time.realtimeSinceStartup;
            fpsFrames = 0;
            fps = 0.0f;
            //cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            currentScreenResolutionText = $"当前屏幕分辨率：{Screen.currentResolution}";
            currentScreenDPIText = $"当前屏幕DPI：{Screen.dpi}";
            screenCountText = $"屏幕数量：{Display.displays.Length}";
        }

        private void GUIUpdate()
        {
            if (!showGUI) return;
            HardwareInfosOnGUI();
        }

        private void HardwareInfosOnGUI()
        {
            // var backgroundStyle = new GUIStyle(GUI.skin.box)
            // {
            //     fontSize = labelFontSize
            // };
            // GUI.Box(new Rect(10, 10, Screen.width / 4, Screen.height - 20), "设备信息(Device Infos)", backgroundStyle);
            // {
            //     GUILayout.BeginArea(new Rect(20, 40, 480, Screen.height - 40));
            //     GUILayout.BeginVertical();
            //     
            //     GUILayout.EndVertical();
            //     GUILayout.FlexibleSpace();
            //     GUILayout.BeginHorizontal();
            //     {
            //         if (GUILayout.Button("-", GUILayout.Width(100))) labelFontSize -= 1;
            //         if (GUILayout.Button("+", GUILayout.Width(100))) labelFontSize += 1;
            //         labelStyle.fontSize = labelFontSize;
            //     }
            //     GUILayout.EndHorizontal();
            //
            //     GUILayout.EndArea();
            // }
            // GUI.Box(new Rect(520, 10, Screen.width / 4, Screen.height - 20),
            //     "CPU和内存信息(Processor And Memory Infos)", backgroundStyle);
            // {
            //     GUILayout.BeginArea(new Rect(540, 40, 480, Screen.height - 40));
            //     GUILayout.BeginVertical();
            //     GUILayout.Label(processorTypeText, labelStyle);
            //     GUILayout.Label(processorCountText, labelStyle);
            //     GUILayout.Label(processorFrequencyText, labelStyle);
            //     GUILayout.Label(cpuUsageText, labelStyle);
            //     GUILayout.Label(systemMemorySizeText, labelStyle);
            //     GUILayout.Label(totalReservedMemoryText, labelStyle);
            //     GUILayout.Label(totalAllocatedMemoryText, labelStyle);
            //     GUILayout.Label(totalUnusedReservedMemoryText, labelStyle);
            //     GUILayout.Label(usedHeapMemoryText, labelStyle);
            //     GUILayout.Label(monoHeapSizeText, labelStyle);
            //     GUILayout.Label(tempAllocatorSizeText, labelStyle);
            //     GUILayout.Label(heapMemoryFragmentationText, labelStyle);
            //     GUILayout.EndVertical();
            //     GUILayout.FlexibleSpace();
            //     GUILayout.EndArea();
            // }
            // GUI.Box(new Rect(1020, 10, Screen.width / 4, Screen.height - 20), "屏幕信息(Screen Infos)", backgroundStyle);
            // {
            //     GUILayout.BeginArea(new Rect(1040, 40, 480, Screen.height - 40));
            //     GUILayout.BeginVertical();
            //     GUILayout.Label(screenFPSText, labelStyle);
            //     GUILayout.Label(screenCountText, labelStyle);
            //     GUILayout.Label(currentScreenResolutionText, labelStyle);
            //     GUILayout.Label(currentScreenDPIText, labelStyle);
            //     GUILayout.EndVertical();
            //     GUILayout.FlexibleSpace();
            //     GUILayout.EndArea();
            // }
        }

        private void HardwareInfosUpdate()
        {
            // totalReservedMemoryText = $"总预留内存：{totalReservedMemory} MB";
            // totalAllocatedMemoryText = $"已分配内存：{totalAllocatedMemory} MB";
            // totalUnusedReservedMemoryText = $"未使用预留内存：{totalUnusedReservedMemory} MB";
            // usedHeapMemoryText = $"已使用堆内存：{usedHeapMemory} MB";
            // monoHeapSizeText = $"保留的托管内存：{monoHeapSize} MB";
            // tempAllocatorSizeText = $"临时分配器大小：{tempAllocatorSize} MB";
            // heapMemoryFragmentationText = $"堆内存碎片：{heapMemoryFragmentation} MB";
            // processorFrequencyText = $"CPU频率：{SystemInfo.processorFrequency} MHz";
            // systemMemorySizeText = $"系统内存大小：{SystemInfo.systemMemorySize} MB";
        }

        private void FPSUpdate()
        {
            ++fpsFrames;
            var timeNow = Time.realtimeSinceStartup;
            if (!(timeNow > lastInterval + 1)) return;
            fps = fpsFrames / (timeNow - lastInterval);
            fpsFrames = 0;
            lastInterval = timeNow;
            screenFPSText = $"刷新率：{fps.ToString(CultureInfo.CurrentUICulture)}";
        }

        /// <summary>
        /// 总预留内存(MB)
        /// </summary>
        [NonPersistence]
        public long totalReservedMemory => Profiler.GetTotalReservedMemoryLong() / MbDiv;

        /// <summary>
        /// 已分配内存(MB)
        /// </summary>
        [NonPersistence]
        public long totalAllocatedMemory => Profiler.GetTotalAllocatedMemoryLong() / MbDiv;

        /// <summary>
        /// 未使用预留内存(MB)
        /// </summary>
        [NonPersistence]
        public long totalUnusedReservedMemory => Profiler.GetTotalUnusedReservedMemoryLong() / MbDiv;

        /// <summary>
        /// 已使用堆内存(MB)
        /// </summary>
        [NonPersistence]
        public long usedHeapMemory => Profiler.usedHeapSizeLong / MbDiv;

        /// <summary>
        /// 保留的托管内存大小
        /// </summary>
        [NonPersistence]
        public long monoHeapSize => Profiler.GetMonoHeapSizeLong() / MbDiv;

        /// <summary>
        /// 临时分配器大小
        /// </summary>
        [NonPersistence]
        public long tempAllocatorSize => Profiler.GetTempAllocatorSize() / MbDiv;


        /// <summary>
        /// 堆内存碎片
        /// </summary>
        [NonPersistence]
        public long heapMemoryFragmentation =>
            Profiler.GetTotalFragmentationInfo(new NativeArray<int>(24, Allocator.Temp)) / MbDiv;

        /// <summary>
        /// CPU使用率(Windows)
        /// </summary>
        //[NonPersistence]
        //public float cpuUsage => cpuCounter.NextValue();
    }
}