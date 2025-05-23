
using System.Runtime.InteropServices;
using UnityEngine;

namespace MFramework.UnityApplication
{
    public class DeviceInfoGUIWindow : GUIWindow
    {
        protected override string windowTitle { get; } = "设备信息(DeviceInfo)";
        private string processorTypeText { get; set; } //CPU类型
        private string processorCountText { get; set; } //CPU核心数
        private string processorFrequencyText { get; set; } //CPU频率
        private string cpuUsageText { get; set; } //CPU使用率
        private string systemMemorySizeText { get; set; } //系统内存大小
        private string totalReservedMemoryText { get; set; } //总预留内存
        private string totalAllocatedMemoryText { get; set; } //已分配内存
        private string totalUnusedReservedMemoryText { get; set; } //未使用预留内存
        private string usedHeapMemoryText { get; set; } //已使用堆内存
        private string monoHeapSizeText { get; set; } //保留的托管内存
        private string tempAllocatorSizeText { get; set; } //临时分配器大小
        private string heapMemoryFragmentationText { get; set; } //堆内存碎片
        private string deviceNameText { get; set; } //设备名称
        private string operatingSystemText { get; set; } //系统版本
        private string operatingSystemDescriptionText { get; set; } //系统版本
        private string osArchitectureText { get; set; } //操作系统架构
        private string dotNetFrameworkDescriptionText { get; set; } //操作.Net Framework描述
        private string deviceTypeText { get; set; } //设备类型
        private string deviceModelText { get; set; } //设备型号
        private string deviceUniqueIdentifierText { get; set; } //设备唯一标识符
        private string graphicsDeviceNameText { get; set; } //显卡名称
        private string graphicsDeviceTypeText { get; set; } //显卡类型
        private string graphicsDeviceIDText { get; set; } //显卡唯一标识符
        private string graphicsDeviceVendorText { get; set; } //显卡厂商
        private string graphicsDeviceVendorIDText { get; set; } //显卡厂商ID
        private string graphicsDeviceVersionText { get; set; } //显卡支持版本
        private string graphicsMemorySizeText { get; set; } //显卡显存大小
        private string graphicsShaderLevelText { get; set; } //显卡着色器级别
        private string graphicsMultiThreadedText { get; set; } //是否支持多线程
        private GUIStyle labelStyle;
        private int labelFontSize = 14;

        private void Start()
        {
            deviceNameText = $"设备名称：{SystemInfo.deviceName}";
            deviceTypeText = $"设备类型：{SystemInfo.deviceType}";
            deviceModelText = $"设备型号：{SystemInfo.deviceModel}";
            deviceUniqueIdentifierText = $"设备标识符：{SystemInfo.deviceUniqueIdentifier}";
            graphicsDeviceNameText = $"显卡名称：{SystemInfo.graphicsDeviceName}";
            graphicsDeviceTypeText = $"显卡类型：{SystemInfo.graphicsDeviceType}";
            graphicsDeviceIDText = $"显卡标识符：{SystemInfo.graphicsDeviceID}";
            graphicsDeviceVendorText = $"显卡厂商：{SystemInfo.graphicsDeviceVendor}";
            graphicsDeviceVendorIDText = $"显卡厂商标识符：{SystemInfo.graphicsDeviceVendorID}";
            graphicsDeviceVersionText = $"显卡支持版本：{SystemInfo.graphicsDeviceVersion}";
            graphicsMemorySizeText = $"显存大小：{SystemInfo.graphicsMemorySize} MB";
            graphicsShaderLevelText = $"显卡着色器级别：{SystemInfo.graphicsShaderLevel}";
            graphicsMultiThreadedText = $"显卡是否支持多线程：{SystemInfo.graphicsMultiThreaded}";
            processorTypeText = $"处理器类型：{SystemInfo.processorType}";
            processorCountText = $"CPU核心数：{SystemInfo.processorCount}";
            operatingSystemText = $"操作系统版本：{SystemInfo.operatingSystem}";
            operatingSystemDescriptionText = $"操作描述架构：{RuntimeInformation.OSDescription}";
            osArchitectureText = $"操作系统架构：{RuntimeInformation.OSArchitecture}";
            dotNetFrameworkDescriptionText = $"运行时平台：{RuntimeInformation.FrameworkDescription}";
            labelStyle = new GUIStyle()
            {
                alignment = TextAnchor.LowerLeft,
                normal = new GUIStyleState()
                {
                    textColor = Color.white * 0.9f,
                },
                fontSize = labelFontSize,
                margin = new RectOffset(0, 0, 0, 5)
            };
        }

        public override void DoGUI()
        {
            GUILayout.Label(deviceNameText, labelStyle);
            GUILayout.Label(operatingSystemText, labelStyle);
            GUILayout.Label(operatingSystemDescriptionText, labelStyle);
            GUILayout.Label(osArchitectureText, labelStyle);
            GUILayout.Label(dotNetFrameworkDescriptionText, labelStyle);
            GUILayout.Label(deviceTypeText, labelStyle);
            GUILayout.Label(deviceModelText, labelStyle);
            GUILayout.Label(deviceUniqueIdentifierText, labelStyle);
            GUILayout.Label(graphicsDeviceNameText, labelStyle);
            GUILayout.Label(graphicsDeviceTypeText, labelStyle);
            GUILayout.Label(graphicsDeviceIDText, labelStyle);
            GUILayout.Label(graphicsDeviceVendorText, labelStyle);
            GUILayout.Label(graphicsDeviceVendorIDText, labelStyle);
            GUILayout.Label(graphicsDeviceVersionText, labelStyle);
            GUILayout.Label(graphicsShaderLevelText, labelStyle);
            GUILayout.Label(graphicsMemorySizeText, labelStyle);
            GUILayout.Label(graphicsMultiThreadedText, labelStyle);
        }
    }
}