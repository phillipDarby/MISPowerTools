using Hardware.Info;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MISPowerTools.Library.Internal
{
    public class GetCPU
    {
        
        public static string GetCpuString()
        {
            IHardwareInfo hardwareInfo = new HardwareInfo();
            hardwareInfo.RefreshCPUList();
            var cpu = hardwareInfo.CpuList.First().ToString();
            return cpu;
        }
    }
}
