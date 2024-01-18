using JetBrains.Annotations;

namespace Monry.Toolbox.Editor.Internal.Xcode
{
    public partial class XCStringsData
    {
        public partial class StringData
        {
            [PublicAPI]
            public partial class LocalizationData
            {
                public StringUnitData StringUnit { get; set; } = new();
            }
        }
    }
}
