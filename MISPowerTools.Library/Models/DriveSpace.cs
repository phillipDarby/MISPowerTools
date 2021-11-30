using System;
using System.Collections.Generic;
using System.Text;

namespace MISPowerTools.Library.Models
{
    public class DriveSpace
    {
        public double TotalSpace { get; set; }
        public double FreeSpace { get; set; }
        public string Description { get; set; }
        private double _totalUsed;
        private double _percentUsed;
        private double _percentFree;

        public double TotalUsed { get => _totalUsed;  set => _totalUsed = TotalSpace - FreeSpace; } 
        public double PercentUsed { get => _percentUsed; set => _percentUsed = (TotalUsed * 100) / TotalSpace; }
        public double PercentFree { get => _percentFree; set => _percentFree = 100 - PercentUsed; }
    }
}
