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
                    odgovor.Version = reader.GetString();
                }
                else if (i == 2)
                {
                    odgovor.Href = reader.GetString();
                }
                else
                {
                    while (reader.Read())
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
                    }
                }
            }
            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, Odgovor value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override bool CanConvert(Type typeToConvert)
        {
            if (!typeToConvert.IsGenericType)
            {
                return false;
            }
            if (typeToConvert.GetGenericTypeDefinition() != typeof(List<Recept>))
            {
                return false;
            }
            return base.CanConvert(typeToConvert);
        }
    }
}
