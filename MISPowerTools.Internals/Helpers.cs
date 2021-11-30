using Hardware.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MISPowerTools.Internals
{
    public class Helpers
    {
        private static readonly IHardwareInfo HardwareInfo;
        private static readonly string batteryString;
        private static readonly string cpuString;
        private static readonly string biosString;
        private static readonly string driveString;
        public static string BatteryString => batteryString;
        public static string CPUString => cpuString;
        public static string BiosString => biosString;
        public static string DriveString => driveString;
        

        static Helpers()
        {
            HardwareInfo = new HardwareInfo();
            cpuString = GetCpuString();
            batteryString = GetBatteryString();
            biosString = GetBiosString();
            driveString = GetDriveList().ToString();
        }

        public static List<Drive> GetDriveList()
        {
            HardwareInfo.RefreshDriveList();
            var drive = HardwareInfo.DriveList;
            return drive;
        }


        private static string GetBatteryString()
        {
            HardwareInfo.RefreshBatteryList();
            var battery = HardwareInfo.BatteryList.First().ToString();
            return battery;
        }
        private static string GetBiosString()
        {
            HardwareInfo.RefreshBIOSList();
            var bios = HardwareInfo.BiosList.First().ToString();
            return bios;
        }

        private static string GetCpuString()
        {
            HardwareInfo.RefreshCPUList();
            var cpu = HardwareInfo.CpuList.First().ToString();
            return cpu;
        }
    }
}
