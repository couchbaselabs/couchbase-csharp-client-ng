﻿using Newtonsoft.Json;

namespace Couchbase.Configuration.Server.Serialization
{
    internal sealed class SetAutoCompaction
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("validateURI")]
        public string ValidateURI { get; set; }
    }
}