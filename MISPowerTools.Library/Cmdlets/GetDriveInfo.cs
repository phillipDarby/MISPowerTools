using Microsoft.Management.Infrastructure;
using MISPowerTools.Internals;
using MISPowerTools.Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Management.Automation;
using System.Text;

namespace MISPowerTools.Library.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "DriveInf")]
    public class GetDriveInfo : Cmdlet
    {
        protected override void ProcessRecord()
        {
            var cpu = GetDriveSpace();
            WriteObject(cpu);
        }

        private List<DriveSpace> GetDriveSpace()
        {
            DriveSpace driveInfo = new DriveSpace();
            List<DriveSpace> drives = new List<DriveSpace>();
            string cimNamespace = @"root\cimv2";
            string cimClassName = "Win32_LogicalDisk";

            try
            {
                CimSession cimSession = CimSession.Create("localhost");
                IEnumerable<CimInstance> queryInstances = cimSession.QueryInstances(@"root\cimv2",
                             "WQL",
                             $@"select * from {cimClassName}");
                foreach (CimInstance cimInstance in queryInstances)
                {
                    //Use the current instance. This example prints the instance. 
                    //(cimInstance);
                    var drivetype = cimInstance.CimInstanceProperties["DriveType"].Value.ToString();
                    if (drivetype == "3")
                    {
                        var freeSpace = (UInt64)cimInstance.CimInstanceProperties["FreeSpace"].Value / Math.Pow(1024, 3);
                        var totalSpace = (UInt64)cimInstance.CimInstanceProperties["Size"].Value / Math.Pow(1024, 3);
                        var description = cimInstance.CimInstanceProperties["DeviceId"].Value.ToString();
                        var totalUsed = totalSpace - freeSpace;
                        var percentUsed = (totalUsed * 100) / totalSpace;
                        var percentFree = 100 - percentUsed;

                        drives.Add(new DriveSpace
                        {
                            FreeSpace = freeSpace,
                            TotalSpace = totalSpace,
                            Description = description,
                            TotalUsed = totalUsed,
                            PercentUsed = percentUsed,
                            PercentFree = percentFree
                        });
                    }
                    
                }
            }
            catch (CimException ex)
            {
                // Handle the exception as appropriate.
                // This example prints the message.
                Console.WriteLine(ex.Message);
            }

            return drives;
        }
        static Dictionary<string, object> GetKeyPropertiesAndValues(CimSession cimSession, string cimNamespace, string cimClassName)
        {
            Dictionary<string, object> propertyValues = new Dictionary<string, object>();

            CimClass cimClass = cimSession.GetClass(cimNamespace, cimClassName);
            var keyProperties = from p in cimClass.CimClassProperties
                                where ((p.Flags & CimFlags.Key) == CimFlags.Key)
                                select p;

            foreach (CimPropertyDeclaration keyProperty in keyProperties)
            {
                Console.Write(String.Format("Please type Key value for Property '{0}' of Type:({1}): ",
                                            keyProperty.Name, keyProperty.CimType));
                propertyValues.Add(keyProperty.Name, Console.ReadLine());
            }

            return propertyValues;
        }
        static CimInstance CreateSearchInstance(CimSession cimSession, string cimNamespace, string cimClassName)
        {
            CimInstance searchInstance = new CimInstance(cimClassName);

            Dictionary<string, object> propertyValues = GetKeyPropertiesAndValues(cimSession, cimNamespace, cimClassName);
            if (propertyValues != null && propertyValues.Count > 0)
        {
                foreach (var property in propertyValues)
                {
                    searchInstance.CimInstanceProperties.Add(CimProperty.Create(property.Key,
                                                                                property.Value,
                                                                                CimFlags.Key));
                }
            }

            return searchInstance;
        }
    }

}
