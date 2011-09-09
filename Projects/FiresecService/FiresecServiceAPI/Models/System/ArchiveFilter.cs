﻿using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FiresecAPI.Models
{
    public enum SubsystemType
    {
        Fire,
        Guard,
        Other
    };

    [DataContract]
    public class ArchiveFilter
    {
        [DataMember]
        public List<string> Descriptions { get; set; }

        [DataMember]
        public List<SubsystemType> Subsystems { get; set; }

        [DataMember]
        public bool UseSystemDate { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }
    }
}