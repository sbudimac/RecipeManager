using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace RecipeManager
{
    public class ReceptJson : JsonConverter<Recept>
    {
        public override Recept Read(ref Utf8JsonReader reader, Type type, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException();
            }
            Recept recept = new Recept();
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
                            recept.Title = Filtriraj(recept.Title);
                            break;
                        case "href":
                            recept.Href = reader.GetString();
                            recept.Href = Filtriraj(recept.Href);
                            break;
                        case "ingredients":
                            recept.Ingredients = reader.GetString();
                            recept.Ingredients = Filtriraj(recept.Ingredients);
                            break;
                        case "thumbnail":
                            recept.Thumbnail = reader.GetString();
                            recept.Thumbnail = Filtriraj(recept.Thumbnail);
                            break;
                        default:
                            throw new JsonException();
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

        private string Filtriraj(string stavka)
        {
            string filter = Regex.Replace(stavka, @"\t|\n|\r", "");
            return filter;
        }
    }
}
