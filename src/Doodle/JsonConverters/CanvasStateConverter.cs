using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Doodle.Abstractions.Interfaces;
using Doodle.State;

namespace Doodle.JsonConverters
{

    public class CanvasStateConverter : JsonConverter<State.CanvasState>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(State.CanvasState));
        }

        public override CanvasState Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<State.CanvasState>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, CanvasState value, JsonSerializerOptions options)
        {
           JsonSerializer.Serialize(writer, value, options);
        }
    }


}