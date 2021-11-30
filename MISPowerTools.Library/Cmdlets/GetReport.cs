using MISPowerTools.Library.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Text;

namespace MISPowerTools.Library.Cmdlets
{
    [Cmdlet(VerbsCommon.Get, "Report")]
    public class GetReport : Cmdlet
    {
        protected override void ProcessRecord()
        {
            var p = new ProgressRecord(1, "Building Report", "In Progress please be patient...");
            p.RecordType = ProgressRecordType.Processing;
            WriteProgress(p);
            var result = new StringBuilder();
            BuildReport(result, p);

            var computerName = Environment.GetEnvironmentVariable("computername");
            var month = DateTime.Now.Month;
            var year = DateTime.Now.Year;
            var reportName = $"{computerName}_{month}-{year}.log";
            string path = $@"c:\temp\{reportName}";
            StringBuilder sw = new StringBuilder();
            sw.Append(computerName + "\n");
            sw.Append("==================================================\n");
            sw.Append(result);
            File.WriteAllText(path, sw.ToString());
            p.RecordType = ProgressRecordType.Completed;
            WriteProgress(p);
            WriteObject(result.ToString());
            

        }
        private void BuildReport(StringBuilder result, ProgressRecord p)
        {
            p.StatusDescription = "Gathering Basic Info...";
            WriteProgress(p);
            GetBasicInfo(result);
            p.StatusDescription = "Gathering Disk Space Info...";
            WriteProgress(p);
            GetDiskSpace(result);
        }
        private void GetBasicInfo(StringBuilder result)
        {
            var GetComputerInfo = new GetComputerSystemInfo().Invoke().OfType<ComputerInfoModel>().First();
            var GetOsInfo = new GetOs().Invoke().OfType<OsModel>().First();
            var GetBiosInfo = new GetBiosInfo().Invoke().OfType<BiosModel>().First();
            result.Append("Domain: " + GetComputerInfo.Domain +" \n");
            result.Append(GetOsInfo.Caption + " \n");
            result.Append("Manufacturer: " + GetComputerInfo.Manufacturer + " \n");
            result.Append("Model: " + GetComputerInfo.Model + " \n");
            result.Append("Serial Number: " + GetBiosInfo.SerialNumber + " \n");
            int tickCountMs = Environment.TickCount;
            var uptime = TimeSpan.FromMilliseconds(tickCountMs);
            result.Append("System Uptime: " + uptime + " \n");
        }
        private void GetCPU(StringBuilder result)
        {
            var GetCPUInfo = new GetCpuInfo().Invoke().OfType<string>().First();
            result.Append("----------------------\n");
            result.Append("CPU INFO \n");
            result.Append("----------------------\n");
            result.Append(GetCPUInfo + "\n");
        }
        private void GetBios(StringBuilder result)
        {
            var GetBiosInfo = new GetBiosInfo().Invoke().OfType<BiosModel>().First();
            result.Append("----------------------\n");
            result.Append("BIOS INFO \n");
            result.Append("----------------------\n");
            result.Append("Name: " + GetBiosInfo.Name + "\n");
            result.Append("Serial Number: " + GetBiosInfo.SerialNumber + "\n");
        }
        private void GetDiskSpace(StringBuilder result)
        {
            var GetDrivenfo = new GetDriveInfo().Invoke().OfType<List<DriveSpace>>().First();
            result.Append("----------------------\n");
            result.Append("Disk Usage: \n");
            result.Append("----------------------\n");

            foreach(var drive in GetDrivenfo)
            {
                result.Append($"Drive {drive.Description} {drive.TotalSpace} GB Total, {drive.FreeSpace} ({drive.PercentFree} %) Free \n");
            }
            
        }
    }
}
