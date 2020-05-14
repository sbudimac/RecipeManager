using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RecipeManager
{
    public class OdgovorJson : JsonConverter<Odgovor>
    {
        private readonly JsonConverter<Recept> receptConverter;

        public OdgovorJson(JsonSerializerOptions options)
        {
            receptConverter = (JsonConverter<Recept>)options.GetConverter(typeof(Recept));
        }
        public override Odgovor Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }
            Odgovor odgovor = new Odgovor();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    throw new JsonException();
                }
                else if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString();
                    reader.Read();
                    switch (propertyName.ToLower())
                    {
                        case "title":
                            odgovor.Title = reader.GetString();
                            break;
                        case "version":
                            odgovor.Version = reader.GetDecimal();
                            break;
                        case "href":
                            odgovor.Href = reader.GetString();
                            break;
                        case "results":
                            if (reader.TokenType != JsonTokenType.StartArray)
                            {
                                throw new JsonException();
                            }
                            odgovor.Results = new List<Recept>();
                            break;
                        default:
                            throw new JsonException();
                    }
                }
                else
                {
                    if (reader.TokenType == JsonTokenType.EndArray)
                    {
                        reader.Read();
                        if (reader.TokenType == JsonTokenType.EndObject)
                        {
                            return odgovor;
                        }
                    }
                    Recept r;
                    r = receptConverter.Read(ref reader, typeof(Recept), options);
                    odgovor.Results.Add(r);
                }
            }
            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Odgovor value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("title", value.Title);
            writer.WriteNumber("version", value.Version);
            writer.WriteString("href", value.Href);
            writer.WritePropertyName("results");
            writer.WriteStartArray();
            foreach (Recept recept in value.Results)
            {
                receptConverter.Write(writer, recept, options);
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
    }
}