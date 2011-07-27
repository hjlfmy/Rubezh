﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FiresecClient.Models
{
    [DataContract]
    public class ResetItem
    {
        [DataMember]
        public string DeviceId { get; set; }

        [DataMember]
        public List<string> States { get; set; }
    }
}