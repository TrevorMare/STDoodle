using System.Text.Json;

namespace Doodle.JsonConverters
{

    public class Serialization
    {

        public static JsonSerializerOptions GetJsonSerializerOptionsNoConverters()
        {
            var serializerOptions = new JsonSerializerOptions 
            {
                WriteIndented = true, 
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            return serializerOptions;
        }

        public static JsonSerializerOptions GetJsonSerializerOptions()
        {
            var serializerOptions = GetJsonSerializerOptionsNoConverters();
            serializerOptions.Converters.Add(new JsonConverters.ResizableElementConverter());
            return serializerOptions;
        } 

        public static T DeserializeNoConverter<T>(string jsonData)
        {
            var options = GetJsonSerializerOptionsNoConverters();
            return System.Text.Json.JsonSerializer.Deserialize<T>(jsonData, options);
        }

        public static T Deserialize<T>(string jsonData)
        {
            var options = GetJsonSerializerOptions();
            return System.Text.Json.JsonSerializer.Deserialize<T>(jsonData, options);
        }

        public static string Serialize(object item)
        {
            var options = GetJsonSerializerOptions();
            return System.Text.Json.JsonSerializer.Serialize(item, options);
        }
    }
}