using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Doodle.Abstractions.Interfaces;
using Doodle.State;

namespace Doodle.JsonConverters
{

    public class ResizableStateConverter : JsonConverter<State.ResizableState>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(State.ResizableState));
        }

        public override ResizableState Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<State.ResizableState>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, ResizableState value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }


}