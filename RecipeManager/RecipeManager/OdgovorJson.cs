using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RecipeManager
{
    class OdgovorJson : JsonConverter<Odgovor>
    {
        private readonly JsonConverter<Recept> receptConverter;
        string[] vrednosti = { "title", "version", "href"};

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
            for (int i = 0; i < vrednosti.Length; i++)
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    throw new JsonException();
                }
                reader.Read();
                string property = reader.GetString();
                if (property != vrednosti[i])
                {
                    throw new JsonException();
                }
                reader.Read();
                if (i == 0)
                {
                    odgovor.Title = reader.GetString();
                }
                else if (i == 1)
                {
                    odgovor.Version = reader.GetDecimal();
                }
                else if (i == 2)
                {
                    odgovor.Href = reader.GetString();
                }
                else
                {
                    do
                    {
                        if (reader.TokenType == JsonTokenType.EndObject)
                        {
                            return odgovor;
                        }
                        if (reader.TokenType != JsonTokenType.PropertyName)
                        {
                            throw new JsonException();
                        }
                        Recept r;
                        reader.Read();
                        r = receptConverter.Read(ref reader, typeof(Recept), options);
                        odgovor.Results.Add(r);
                    } while (reader.Read());
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

        /*public bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType)
            {
                return false;
            }
            if (typeToConvert.GetGenericTypeDefinition() != typeof(List<Recept>))
            {
                return false;
            }
            if (typeToConvert == typeof(Odgovor))
            {
                return base.CanConvert(typeToConvert);
            }
        }*/
    }
}
