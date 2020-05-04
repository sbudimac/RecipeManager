using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace RecipeManager
{
    public class Odgovor
    {
        public string Title { get; set; }
        public string Version { get; set; }
        public string Href { get; set; }
        public List<Recept> Results { get; set; }

        public static Odgovor GetOdgovor(string json)
        {
            return JsonSerializer.Deserialize<Odgovor>(json);
        }

        public List<Recept> Rezultati
        {
            get { return this.Results; }
        }
    }
}
