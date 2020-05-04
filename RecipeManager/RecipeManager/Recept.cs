using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace RecipeManager
{
    public class Recept
    {
        public string Title { get; set; }
        public string Href { get; set; }
        public string Ingredients { get; set; }
        public string Thumbnail { get; set; }

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
