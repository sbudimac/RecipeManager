using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Buffers.Text;
using System.Text.RegularExpressions;

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
                            recept.Title = filtriraj(recept.Title);
                            break;
                        case "href":
                            recept.Href = reader.GetString();
                            recept.Href = filtriraj(recept.Href);
                            break;
                        case "ingredients":
                            recept.Ingredients = reader.GetString();
                            recept.Ingredients = filtriraj(recept.Ingredients);
                            break;
                        case "thumbnail":
                            recept.Thumbnail = reader.GetString();
                            recept.Thumbnail = filtriraj(recept.Thumbnail);
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

        private string filtriraj(string stavka)
        {
            string filter = Regex.Replace(stavka, @"\t|\n|\r", "");
            return filter;
        }
    }
}
