using System.Collections.Generic;
using JetBrains.Annotations;
using Monry.Toolbox.Editor.Internal.Xcode.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Monry.Toolbox.Editor.Internal.Xcode
{
    [PublicAPI]
    public partial class XCStringsData
    {
        public static JsonSerializerSettings DefaultSerializerSettings { get; } = new()
        {
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
        };

        public string SourceLanguage { get; set; } = string.Empty;
        [JsonConverter(typeof(StringDictionaryConverter))]
        public Dictionary<string, StringData> Strings { get; } = new();
        public string Version { get; set; } = "1.0";
    }
}
