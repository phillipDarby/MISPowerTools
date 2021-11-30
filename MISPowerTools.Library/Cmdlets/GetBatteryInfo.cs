using MISPowerTools.Internals;
using MISPowerTools.Library.Internal;
using MISPowerTools.Library.Internal.Components;
using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;

namespace MISPowerTools.Library.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "BatInf")]
    public class GetBatteryInfo : Cmdlet
    {

        protected override void ProcessRecord()
        {
            var bat = Helpers.BatteryString;
            WriteObject(bat);
        }
    }
}
