using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Doodle.Abstractions.Interfaces;
using Doodle.State;

namespace Doodle.JsonConverters
{

    public class ResizableElementConverter : JsonConverter<IResizableContent>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(Abstractions.Models.ResizableImage)) ||
                   typeToConvert.IsAssignableFrom(typeof(Abstractions.Models.ResizableText));
        }

        public override IResizableContent Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Utf8JsonReader readerClone = reader;

            Abstractions.Common.ResizableContentType? contentType = GetContentType(readerClone);

            if (contentType == Abstractions.Common.ResizableContentType.Text)
            {
                return JsonSerializer.Deserialize<Abstractions.Models.ResizableText>(ref reader, Serialization.GetJsonSerializerOptionsNoConverters());
            }
            else if (contentType == Abstractions.Common.ResizableContentType.Image)
            {
                return JsonSerializer.Deserialize<Abstractions.Models.ResizableImage>(ref reader, Serialization.GetJsonSerializerOptionsNoConverters());
            }
            else
            {
                throw new Exception("Unable to locate the Resizable Element Type");
            }
        }

        public override void Write(Utf8JsonWriter writer, IResizableContent value, JsonSerializerOptions options)
        {
            var elementType = value.GetType();
            JsonSerializer.Serialize(writer, value, elementType, Serialization.GetJsonSerializerOptionsNoConverters());
        }

        private Abstractions.Common.ResizableContentType? GetContentType(Utf8JsonReader reader)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    if (propertyName.ToLower() == "resizablecontenttype")
                    {
                        reader.Read();

                        Abstractions.Common.ResizableContentType result = (Abstractions.Common.ResizableContentType)reader.GetInt32();
                        return result;
                    }
                }

            }
            return null;
        }
    }
}