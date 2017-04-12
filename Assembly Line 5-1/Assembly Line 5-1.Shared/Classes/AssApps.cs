﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assembly_Line_5_1
{
   public class AssApps
    {
        public string Id { get; set; }

        [JsonProperty(PropertyName = "appname")]
        public string appname { get; set; }

        [JsonProperty(PropertyName = "applink")]
        public string applink { get; set; }

        [JsonProperty(PropertyName = "appsection")]
        public string appsection { get; set; }

        [JsonProperty(PropertyName = "apptype")]
        public string apptype { get; set; }

        [JsonProperty(PropertyName = "topapps")]
        public int topapps { get; set; }

        [JsonProperty(PropertyName = "applogo")]
        public string applogo { get; set; }

        [JsonProperty(PropertyName = "developername")]
        public string developername { get; set; }

        [JsonProperty(PropertyName = "appdescription")]
        public string appdescription { get; set; }

        [JsonProperty(PropertyName = "complete")]
        public bool Complete { get; set; }
    }          
}
