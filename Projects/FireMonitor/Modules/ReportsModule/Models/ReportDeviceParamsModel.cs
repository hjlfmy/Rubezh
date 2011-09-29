﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportsModule.Models
{
    public class ReportDeviceParamsModel
    {
        public string Type { get; set; }
        public string Address { get; set; }
        public string Zone { get; set; }
        public string Dustiness { get; set; }
        public string FailureType { get; set; }
    }
}
