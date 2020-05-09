using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Buffers.Text;

namespace RecipeManager
{
    class ReceptJson : JsonConverter<Recept>
    {
        string[] vrednosti = { "title", "href", "ingredients", "thumbnail" };
        public override Recept Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }
            Recept recept=new Recept();
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
                    recept.Title = reader.GetString();
                }else if (i == 1)
                {
                    recept.Href = reader.GetString();
                }else if (i == 2)
                {
                    recept.Ingredients = reader.GetString();
                }
                else
                {
                    recept.Thumbnail = reader.GetString();
                }
            }
            return recept;
        }

        public override void Write(Utf8JsonWriter writer, Recept value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("title", value.Title);
            writer.WriteString("href", value.Href);
            writer.WriteString("ingredients", value.Ingredients);
            writer.WriteString("thumbnail", value.Thumbnail);
            writer.WriteEndObject();
        }
    }
}
