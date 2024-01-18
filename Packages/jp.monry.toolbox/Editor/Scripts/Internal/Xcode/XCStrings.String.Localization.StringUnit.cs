using JetBrains.Annotations;
using Monry.Toolbox.Editor.Internal.Xcode.Json;
using Newtonsoft.Json;

namespace Monry.Toolbox.Editor.Internal.Xcode
{
    public partial class XCStringsData
    {
        public partial class StringData
        {
            public partial class LocalizationData
            {
                [PublicAPI]
                public class StringUnitData
                {
                    [JsonConverter(typeof(EnumSnakeCaseConverter<LocalizationState>))]
                    public LocalizationState State { get; set; } = LocalizationState.New;
                    public string Value { get; set; } = string.Empty;
                }
            }
        }
    }
}
