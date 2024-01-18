using System.Collections.Generic;
using JetBrains.Annotations;
using Monry.Toolbox.Editor.Internal.Xcode.Json;
using Newtonsoft.Json;

namespace Monry.Toolbox.Editor.Internal.Xcode
{
    public partial class XCStringsData
    {
        [PublicAPI]
        public partial class StringData
        {
            public string? Comment { get; set; }
            [JsonConverter(typeof(EnumSnakeCaseConverter<ExtractionState>))]
            public ExtractionState ExtractionState { get; set; } = ExtractionState.ExtractedWithValue;
            public Dictionary<string, LocalizationData> Localizations { get; } = new();
        }
    }
}
