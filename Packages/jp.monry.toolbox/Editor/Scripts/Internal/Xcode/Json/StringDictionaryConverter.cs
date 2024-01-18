using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Monry.Toolbox.Editor.Internal.Xcode.Json
{
    public class StringDictionaryConverter : JsonConverter<Dictionary<string, XCStringsData.StringData>>
    {
        public override void WriteJson(JsonWriter writer, Dictionary<string, XCStringsData.StringData>? value, JsonSerializer serializer)
        {
            writer.WriteStartObject();
            foreach (var (key, data) in value ?? new Dictionary<string, XCStringsData.StringData>())
            {
                writer.WritePropertyName(key);
                serializer.Serialize(writer, data);
            }
            writer.WriteEndObject();
        }

        public override Dictionary<string, XCStringsData.StringData> ReadJson(JsonReader reader, Type objectType, Dictionary<string, XCStringsData.StringData>? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var result = new Dictionary<string, XCStringsData.StringData>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonToken.EndObject)
                {
                    return result;
                }

                if (reader.TokenType != JsonToken.PropertyName)
                {
                    throw new JsonSerializationException($"Unexpected token type: {reader.TokenType}");
                }

                var key = (string)reader.Value!;
                var data = serializer.Deserialize<XCStringsData.StringData>(reader)!;
                result.Add(key, data);
            }
            return result;
        }
    }
}
