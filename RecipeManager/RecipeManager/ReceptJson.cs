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
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    return recept;
                }
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string property = reader.GetString();
                    reader.Read();
                    switch (property.ToLower())
                    {
                        case "title":
                            recept.Title = reader.GetString();
                            break;
                        case "href":
                            recept.Href = reader.GetString();
                            break;
                        case "ingredients":
                            recept.Ingredients = reader.GetString();
                            break;
                        case "thumbnail":
                            recept.Thumbnail = reader.GetString();
                            break;
                    }
                }
            }
            throw new JsonException();
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
