using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace RecipeManager
{
    //[JsonConverter(typeof(OdgovorJson))]
    public class Odgovor
    {
        public string Title { get; set; }
        public decimal Version { get; set; }
        public string Href { get; set; }
        public List<Recept> Results { get; set; }

        public Odgovor() { }
  
        public Odgovor(string title, decimal version, string href, List<Recept> results)
        {
            this.Title = title;
            this.Version = version;
            this.Href = href;
            this.Results = results;
        }

        public static Odgovor GetOdgovor(string json)
        {
            var serializeOptions = new JsonSerializerOptions();
            serializeOptions.Converters.Add(new ReceptJson());
            serializeOptions.Converters.Add(new OdgovorJson(serializeOptions));
            Odgovor odgovorD = JsonSerializer.Deserialize<Odgovor>(json, serializeOptions);
            return odgovorD;
        }

        public List<Recept> Rezultati
        {
            get { return this.Results; }
        }
    }
}
