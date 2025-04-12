using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConputerPerformMonitor.Models
{
    public class HardwareCpuSpec
    {
        public static Dictionary<string, List<string>> BasicSpec = new();
        public static Dictionary<string, List<string>> DetailSpec = new();
        public HardwareCpuSpec()
        {

            List<string> CpuSpec = new();
            List<string> GpuSpec = new();
            List<string> RamSpec = new();

            
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("select * from Win32_Processor"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    CpuSpec.Add($@"CPU 名称: {obj["Name"]} 

制造商: {obj["Manufacturer"]} 

物理核心数: {obj["NumberOfCores"]} 

逻辑核心数: {obj["NumberOfLogicalProcessors"]}

时钟频率: {obj["MaxClockSpeed"]} MHz

L2 缓存大小: {obj["L2CacheSize"]} KB

L3 缓存大小: {obj["L3CacheSize"]} KB");

                }
                
            }
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    GpuSpec.Add($"GPU 名称: {obj["Name"]}");
                    GpuSpec.Add($"显存大小: {Convert.ToInt64(obj["AdapterRAM"]) / 1024 / 1024} MB");
                    GpuSpec.Add($"驱动版本: {obj["DriverVersion"]}");
                    GpuSpec.Add($"当前分辨率: {obj["CurrentHorizontalResolution"]}x{obj["CurrentVerticalResolution"]}");
                    GpuSpec.Add($"刷新率: {obj["CurrentRefreshRate"]} Hz");
                    GpuSpec.Add($"驱动日期: {obj["DriverDate"]}");
                    GpuSpec.Add(new string('-', 40));
                }
            }
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory"))
            {
                static string GetMemoryType(int type)
                {
                    return type switch
                    {
                        20 => "DDR",
                        21 => "DDR2",
                        22 => "DDR2 FB-DIMM",
                        24 => "DDR3",
                        26 => "DDR4",
                        27 => "DDR5",
                        _ => "未知类型"
                    };
                }

                static string FormatBytes(ulong bytes)
                {
                    const ulong KB = 1024;
                    const ulong MB = KB * 1024;
                    const ulong GB = MB * 1024;
                    return bytes >= GB ? $"{bytes / GB} GB" : $"{bytes / MB} MB";
                }
                ManagementObjectCollection managementobj = searcher.Get();
                string ramInfo = null;
                foreach (ManagementObject obj in managementobj)
                {
                    ramInfo = $"=== RAM 详细信息 ===\n\n" +
                    $"制造商: {obj["Manufacturer"]}\n\n" +
                    $"型号: {obj["PartNumber"]}\n\n" +
                    $"序列号: {obj["SerialNumber"]}\n\n" +
                    $"容量: {FormatBytes(Convert.ToUInt64(obj["Capacity"]))}\n\n" +
                    $"速度: {obj["Speed"]} MHz\n\n" +
                    $"类型: {GetMemoryType(Convert.ToUInt16(obj["MemoryType"]))}\n\n" +
                    $"电压: {obj["ConfiguredVoltage"]} mV\n\n" +
                    $"===================================";
                }
                RamSpec.Add(ramInfo);

            }
            
            BasicSpec.Add("CPU", CpuSpec);
            BasicSpec.Add("GPU", GpuSpec);
            BasicSpec.Add("RAM", RamSpec);

            GetAllDeviceInfo();

        }

        public static string GetAllDeviceInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("========= 计算机设备信息 =========");

            // 获取 CPU 信息
            GetDeviceInfo(sb, "Win32_Processor", "=== CPU 信息 ===");
            sb.Clear();
            // 获取 GPU 信息
            GetDeviceInfo(sb, "Win32_VideoController", "=== GPU 信息 ===");
            sb.Clear();
            // 获取主板信息
            GetDeviceInfo(sb, "Win32_BaseBoard", "=== 主板信息 ===");
            sb.Clear();
            // 获取 BIOS 信息
            GetDeviceInfo(sb, "Win32_BIOS", "=== BIOS 信息 ===");
            sb.Clear();
            // 获取内存信息
            GetDeviceInfo(sb, "Win32_PhysicalMemory", "=== 内存信息 ===");
            sb.Clear();
            // 获取存储设备（硬盘、SSD）信息
            GetDeviceInfo(sb, "Win32_DiskDrive", "=== 存储设备信息 ===");
            sb.Clear();
            // 获取分区信息
            GetDeviceInfo(sb, "Win32_LogicalDisk", "=== 分区信息 ===");
            sb.Clear();
            // 获取网络适配器信息
            GetDeviceInfo(sb, "Win32_NetworkAdapter", "=== 网络适配器信息 ===");
            sb.Clear();
            // 获取声卡信息
            GetDeviceInfo(sb, "Win32_SoundDevice", "=== 声卡信息 ===");
            sb.Clear();
            // 获取 USB 设备信息
            GetDeviceInfo(sb, "Win32_USBController", "=== USB 设备信息 ===");
            sb.Clear();
            // 获取电池信息（如果是笔记本）
            GetDeviceInfo(sb, "Win32_Battery", "=== 电池信息 ===");
            sb.Clear();
            // 获取操作系统信息
            GetDeviceInfo(sb, "Win32_OperatingSystem", "=== 操作系统信息 ===");
            sb.Clear();
            return sb.ToString();
        }

        /// <summary>
        /// 获取 WMI 设备信息
        /// </summary>
        private static void GetDeviceInfo(StringBuilder sb, string wmiClass, string header)
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM {wmiClass}");
                
                foreach (ManagementObject obj in searcher.Get())
                {
                    sb.AppendLine(header);
                    foreach (PropertyData prop in obj.Properties)
                    {
                        sb.AppendLine($"{prop.Name}: {prop.Value}");
                    }
                    sb.AppendLine(new string('=', 40));
                }
                List<string> list = new List<string>();
                list.Clear();
                list.Add(sb.ToString());
                string hardwaretype = null;
                switch (wmiClass)
                {
                    case "Win32_Processor":
                        hardwaretype = "CPU";
                        break;
                    case "Win32_VideoController":
                        hardwaretype = "GPU";
                        break;
                    case "Win32_BaseBoard":
                        hardwaretype = "Motherboard";
                        break;
                    case "Win32_BIOS":
                        hardwaretype = "MotherBoard";
                        break;
                    case "Win32_PhysicalMemory":
                        hardwaretype = "RAM";
                        break;
                    case "Win32_DiskDrive":
                        hardwaretype = "Storage";
                        break;
                    case "Win32_LogicalDisk":
                        hardwaretype = "Storage";
                        break;
                    case "Win32_NetworkAdapter":
                        hardwaretype = "Network";
                        break;
                    case "Win32_SoundDevice":
                        hardwaretype = "Sound";
                        break;
                    case "Win32_USBController":
                        hardwaretype = "USB";
                        break;
                    case "Win32_Battery":
                        hardwaretype = "Battery";
                        break;
                    case "Win32_OperatingSystem":
                        hardwaretype = "OS";
                        break;
                    
                }
                DetailSpec.Add(hardwaretype, list);
            }
            catch (Exception ex)
            {
                sb.AppendLine($"获取 {wmiClass} 失败: {ex.Message}");
            }
        }
    }
}
