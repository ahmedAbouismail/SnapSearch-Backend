using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Snapsearch.Services
{
    public partial class CbirResult
    {
        [JsonProperty("score")]
        public string Score { get; set; }

        [JsonProperty("link")]
        public Uri Link { get; set; }
    }

    public partial class CbirResult
    {
        public static Dictionary<string, CbirResult> FromJson(string json) => JsonConvert.DeserializeObject<Dictionary<string, CbirResult>>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this Dictionary<string, CbirResult> self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
