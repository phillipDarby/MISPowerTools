using MISPowerTools.Internals;
using MISPowerTools.Library.Models;
using System;
using System.Collections.Generic;
using System.Management;
using System.Management.Automation;
using System.Text;

namespace MISPowerTools.Library.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "CompInfo")]
    public class GetComputerSystemInfo : Cmdlet
    {
        protected override void ProcessRecord()
        {
            var bios = CompInfo();
            WriteObject(bios);
        }

        private ComputerInfoModel CompInfo()
        {
            ComputerInfoModel compInfo = new ComputerInfoModel();


            ManagementObjectSearcher mos = new ManagementObjectSearcher(new SelectQuery("SELECT Model,Manufacturer,SystemType,Domain FROM Win32_ComputerSystem"));

            foreach (ManagementObject mo in mos.Get())
            {
                compInfo.Model = mo.GetPropertyValue("Model").ToString();
                compInfo.Manufacturer = mo.GetPropertyValue("Manufacturer").ToString();
                compInfo.SystemType = mo.GetPropertyValue("SystemType").ToString();
                compInfo.Domain = mo.GetPropertyValue("Domain").ToString();

            }
            return compInfo;
        }
    }
    
}
