using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Diagnostics;

namespace RecipeManager
{
    public class Odgovor
    {
        public string Title { get; set; }
        public decimal Version { get; set; }
        public string Href { get; set; }
        public List<Recept> Results { get; set; }

        public static Odgovor GetOdgovor(string json)
        {
            JsonSerializerOptions serializeOptions = new JsonSerializerOptions();
            serializeOptions.Converters.Add(new ReceptJson());
            serializeOptions.Converters.Add(new OdgovorJson(serializeOptions));
            Odgovor odgovorD = null;
            try
            {
                odgovorD = JsonSerializer.Deserialize<Odgovor>(json, serializeOptions);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error: " + e.HResult.ToString("X") + " Message:" + e.Message);
                Debug.WriteLine("Web site does not support your ingredients.");
            }
            return odgovorD;
        }

        public List<Recept> Rezultati
        {
            get { return this.Results; }
        }
    }
}
