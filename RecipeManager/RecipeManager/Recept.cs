using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RecipeManager
{
    //[JsonConverter(typeof(ReceptJson))]
    public class Recept
    {
        public string Title { get; set; }
        public string Href { get; set; }
        public string Ingredients { get; set; }
        public string Thumbnail { get; set; }

        public Recept(string title, string href, string ingredients, string thumbnail)
        {
            this.Title = title;
            this.Href = href;
            this.Ingredients = ingredients;
            this.Thumbnail = thumbnail;
        }

        public Recept() { }

        public static Recept getRecept(string json)
        {
            var deserializeOptions = new JsonSerializerOptions();
            deserializeOptions.Converters.Add(new ReceptJson());
            return JsonSerializer.Deserialize<Recept>(json);
        }

        public List<string> Sastojci
        {
            get
            {
                List<string> sastojciL = new List<string>();
                sastojciL.AddRange(this.Ingredients.Split(','));
                return sastojciL;
            }
        }

        public override string ToString()
        {
            string recept = this.Title + ": " + this.Ingredients;
            return recept;
        }
    }
}
