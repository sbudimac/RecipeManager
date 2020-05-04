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
        public string title;
        string version;
        string href;
        List<Recept> results;

        public static Odgovor GetOdgovor(string json)
        {
            return JsonSerializer.Deserialize<Odgovor>(json);
        }

        public List<Recept> Rezultati
        {
            get { return this.results; }
        }
    }
}
