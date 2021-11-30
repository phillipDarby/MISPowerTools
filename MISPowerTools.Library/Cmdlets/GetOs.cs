using MISPowerTools.Library.Models;
using System;
using System.Collections.Generic;
using System.Management;
using System.Management.Automation;
using System.Text;

namespace MISPowerTools.Library.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "OSInf")]
    public class GetOs : Cmdlet
    {
        protected override void ProcessRecord()
        {
            var os = GetOS();
            WriteObject(os);
        }

        private OsModel GetOS()
        {
            OsModel osInfo = new OsModel();


            ManagementObjectSearcher mos = new ManagementObjectSearcher(new SelectQuery("SELECT Caption FROM WIN32_OperatingSystem"));

            foreach (ManagementObject mo in mos.Get())
            {
                osInfo.Caption = mo.GetPropertyValue("Caption").ToString();
                osInfo.DateTime = DateTime.Now;

            }
            return osInfo;
        }
    }
}
