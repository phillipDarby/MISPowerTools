
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using MISPowerTools.Internals;
using System.Text;

namespace MISPowerTools.Library.Cmdlets
{
    [Cmdlet(VerbsCommon.Select, "CpuInf")]
    public class GetCpuInfo : Cmdlet
    {
        
        
        protected override void ProcessRecord()
        {
            var cpu = Helpers.CPUString;
            WriteObject(cpu);
        }

      
    }
}
