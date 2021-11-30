using System;
using System.Collections.Generic;
using System.Management.Automation;
using System.Text;
using System.Management;
using MISPowerTools.Library.Models;
using System.Linq;
using WUApiLib;

namespace MISPowerTools.Library
{
    [Cmdlet(VerbsCommon.Get, "Greeting")]
    public class HelloWorld : Cmdlet
    {
        [Parameter(Position = 0, ValueFromPipeline = true)]
        public string Name { get; set; }

        protected override void ProcessRecord()
        {
            //            ManagementPath p =
            //            new ManagementPath(
            //                "\\\\ComputerName\\root" +
            //            "\\cimv2:Win32_OperatingSystemQFE");
            //SelectQuery query = new SelectQuery(" select * from Win32_OperatingSystemQFE");

            //ManagementObjectSearcher searcher =
            //new ManagementObjectSearcher(
            //query);
            //foreach (ManagementObject service in searcher.Get())
            //{
            //    string myString = service.GetPropertyValue("HotFixID").ToString();
            //    // show the class
            //    Console.WriteLine();
            //}



            // var session = WUApiLib.Extensions.IUpdateExtension.ToUpdateCollection(WUApiLib.IUpdate);

            PowerShell ps = PowerShell.Create().AddCommand("Get-Hotfix");
            List<UpdateModel> updates = new List<UpdateModel>();
            foreach (PSObject result in ps.Invoke())
            {
                var m = result.Members.ToList();
                var o = new UpdateModel();
                o.Description = (string)m[22].Value;
                o.Update = (string)m[19].Value;

                updates.Add(o);
            }

            var whoDat = string.IsNullOrEmpty(Name) ? "World" : Name;
            var greeting = "Hello " + whoDat;

           

            WriteObject(greeting);
        }
    }
}
