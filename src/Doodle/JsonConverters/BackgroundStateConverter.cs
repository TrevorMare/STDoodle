using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Doodle.Abstractions.Interfaces;
using Doodle.State;

namespace Doodle.JsonConverters
{

    public class BackgroundStateConverter : JsonConverter<State.BackgroundState>
    {
        public override bool CanConvert(Type typeToConvert)
        {
            return typeToConvert.IsAssignableFrom(typeof(State.BackgroundState));
        }

        public override BackgroundState Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return JsonSerializer.Deserialize<State.BackgroundState>(ref reader, options);
        }

        public override void Write(Utf8JsonWriter writer, BackgroundState value, JsonSerializerOptions options)
        {
            JsonSerializer.Serialize(writer, value, options);
        }
    }


}