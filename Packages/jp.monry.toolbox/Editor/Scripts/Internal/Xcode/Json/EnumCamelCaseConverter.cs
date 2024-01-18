using System;
using Monry.Toolbox.Extensions;
using Newtonsoft.Json;

namespace Monry.Toolbox.Editor.Internal.Xcode.Json
{
    public class EnumSnakeCaseConverter<TEnum> : JsonConverter<TEnum> where TEnum : Enum
    {
        public override void WriteJson(JsonWriter writer, TEnum? value, JsonSerializer serializer)
        {
            if (value != null)
            {
                writer.WriteValue(value.ToString().ToSnakeCase());
            }
        }

        public override TEnum? ReadJson(JsonReader reader, Type objectType, TEnum? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = reader.ReadAsString();
            if (value == null || !Enum.TryParse(objectType, value.ToPascalCase(), out var result))
            {
                return default;
            }
            return (TEnum)result;
        }
    }
}
