using MISPowerTools.Library.Internal.Components;
using System;
using System.Collections.Generic;
using System.Management;
using System.Text;

namespace MISPowerTools.Library.Internal
{
    internal class HardwareInfoRetreival
    {
        public static T GetPropertyValue<T>(object obj) where T : struct
        {
            return (obj == null) ? default(T) : (T)obj;
        }

        public static T[] GetPropertyArray<T>(object obj)
        {
            return (obj is T[] array) ? array : Array.Empty<T>();
        }

        public static string GetPropertyString(object obj)
        {
            return (obj is string str) ? str : string.Empty;
        }

        public static List<Battery> GetBatteryList()
        {
            List<Battery> batteryList = new List<Battery>();

            
            ManagementObjectSearcher mos = new ManagementObjectSearcher(new SelectQuery("SELECT * FROM Win32_Battery"));

            foreach (ManagementObject mo in mos.Get())
            {
                Battery battery = new Battery
                {
                    FullChargeCapacity = GetPropertyValue<uint>(mo["FullChargeCapacity"]),
                    DesignCapacity = GetPropertyValue<uint>(mo["DesignCapacity"]),
                    BatteryStatus = GetPropertyValue<ushort>(mo["BatteryStatus"]),
                    EstimatedChargeRemaining = GetPropertyValue<ushort>(mo["EstimatedChargeRemaining"]),
                    EstimatedRunTime = GetPropertyValue<uint>(mo["EstimatedRunTime"]),
                    ExpectedLife = GetPropertyValue<uint>(mo["ExpectedLife"]),
                    MaxRechargeTime = GetPropertyValue<uint>(mo["MaxRechargeTime"]),
                    TimeOnBattery = GetPropertyValue<uint>(mo["TimeOnBattery"]),
                    TimeToFullCharge = GetPropertyValue<uint>(mo["TimeToFullCharge"])
                };

                batteryList.Add(battery);
            }

            return batteryList;
        }
        public static List<BIOS> GetBiosList()
        {
            List<BIOS> biosList = new List<BIOS>();

            ManagementObjectSearcher mos = new ManagementObjectSearcher(new SelectQuery("SELECT * FROM Win32_BIOS"));

            foreach (ManagementObject mo in mos.Get())
            {
                BIOS bios = new BIOS
                {
                    Caption = GetPropertyString(mo["Caption"]),
                    Description = GetPropertyString(mo["Description"]),
                    Manufacturer = GetPropertyString(mo["Manufacturer"]),
                    Name = GetPropertyString(mo["Name"]),
                    ReleaseDate = GetPropertyString(mo["ReleaseDate"]),
                    SerialNumber = GetPropertyString(mo["SerialNumber"]),
                    SoftwareElementID = GetPropertyString(mo["SoftwareElementID"]),
                    Version = GetPropertyString(mo["Version"])
                };

                biosList.Add(bios);
            }

            return biosList;
        }
        public static List<CPU> GetCpuList(bool includePercentProcessorTime = true)
        {
            List<CPU> cpuList = new List<CPU>();

            List<CpuCore> cpuCoreList = new List<CpuCore>();

            ulong percentProcessorTime = 0ul;
            if (includePercentProcessorTime)
            {
                
                ManagementObjectSearcher percentProcessorTimeMOS = new ManagementObjectSearcher(new SelectQuery("SELECT * FROM Win32_PerfFormattedData_PerfOS_Processor WHERE Name != '_Total'"));

                foreach (ManagementObject mo in percentProcessorTimeMOS.Get())
                {
                    CpuCore core = new CpuCore
                    {
                        Name = GetPropertyString(mo["Name"]),
                        PercentProcessorTime = GetPropertyValue<ulong>(mo["PercentProcessorTime"])
                    };

                    cpuCoreList.Add(core);
                }

                
                ManagementObjectSearcher managementObjectSearcher = new ManagementObjectSearcher(new SelectQuery("SELECT * FROM Win32_PerfFormattedData_PerfOS_Processor WHERE Name = '_Total'"));

                foreach (ManagementObject mo in managementObjectSearcher.Get())
                {
                    percentProcessorTime = GetPropertyValue<ulong>(mo["PercentProcessorTime"]);
                }
            }

            bool isAtLeastWin8 = true;

            
            ManagementObjectSearcher mos = new ManagementObjectSearcher(new SelectQuery("SELECT * FROM Win32_Processor"));

            foreach (ManagementObject mo in mos.Get())
            {
                CPU cpu = new CPU
                {
                    Caption = GetPropertyString(mo["Caption"]),
                    CurrentClockSpeed = GetPropertyValue<uint>(mo["CurrentClockSpeed"]),
                    Description = GetPropertyString(mo["Description"]),
                    L2CacheSize = GetPropertyValue<uint>(mo["L2CacheSize"]),
                    L3CacheSize = GetPropertyValue<uint>(mo["L3CacheSize"]),
                    Manufacturer = GetPropertyString(mo["Manufacturer"]),
                    MaxClockSpeed = GetPropertyValue<uint>(mo["MaxClockSpeed"]),
                    Name = GetPropertyString(mo["Name"]),
                    NumberOfCores = GetPropertyValue<uint>(mo["NumberOfCores"]),
                    NumberOfLogicalProcessors = GetPropertyValue<uint>(mo["NumberOfLogicalProcessors"]),
                    ProcessorId = GetPropertyString(mo["ProcessorId"]),
                    SocketDesignation = GetPropertyString(mo["SocketDesignation"]),
                    PercentProcessorTime = percentProcessorTime,
                    CpuCoreList = cpuCoreList
                };

                if (isAtLeastWin8)
                {
                    cpu.SecondLevelAddressTranslationExtensions = GetPropertyValue<bool>(mo["SecondLevelAddressTranslationExtensions"]);
                    cpu.VirtualizationFirmwareEnabled = GetPropertyValue<bool>(mo["VirtualizationFirmwareEnabled"]);
                    cpu.VMMonitorModeExtensions = GetPropertyValue<bool>(mo["VMMonitorModeExtensions"]);
                }

                cpuList.Add(cpu);
            }

            return cpuList;
        }
    }
}
