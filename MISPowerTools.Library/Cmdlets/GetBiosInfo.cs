using MISPowerTools.Internals;
using MISPowerTools.Library.Internal;
using MISPowerTools.Library.Models;
using System;
using System.Collections.Generic;
using System.Management;
using System.Management.Automation;
using System.Text;

namespace MISPowerTools.Library.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "BiosInf")]
    public class GetBiosInfo : Cmdlet
    {

        protected override void ProcessRecord()
        {

            var bios = GetBios();
            WriteObject(bios);
        }
        private BiosModel GetBios()
        {
            BiosModel compInfo = new BiosModel();


            ManagementObjectSearcher mos = new ManagementObjectSearcher(new SelectQuery("SELECT Name,SerialNumber FROM Win32_BIOS"));

            foreach (ManagementObject mo in mos.Get())
            {
                compInfo.Name = mo.GetPropertyValue("Name").ToString();
                compInfo.SerialNumber = mo.GetPropertyValue("SerialNumber").ToString();

            }
            return compInfo;
        }
    }
}
