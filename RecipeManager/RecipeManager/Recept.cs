using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Media.Imaging;

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

        public BitmapImage Image
        {
            get
            {
                Uri thumbnailUrl = new Uri(Thumbnail);
                BitmapImage thumbnailImage = new BitmapImage(thumbnailUrl);
                return thumbnailImage;
            }
        }
    }
}
