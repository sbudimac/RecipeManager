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
        string title;
        string href;
        string ingredients;
        string thumbnail;

        public List<string> Sastojci
        {
            get
            {
                List<string> sastojciL = new List<string>();
                sastojciL.AddRange(this.ingredients.Split(','));
                return sastojciL;
            }
        }

        public override string ToString()
        {
            string recept = this.title + ": " + this.ingredients;
            return recept;
        }
    }
}
