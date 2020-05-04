using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Web.Http;
using System.Text.Json;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RecipeManager
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string url = "http://www.recipepuppy.com/api/?i=";
            List<string> sastojci = new List<string>();
            char[] separatori = { ',', ' ' };
            string unos = tbUnos.Text;
            sastojci.AddRange(unos.Split(separatori));
            for (int i = 0; i < sastojci.Count - 1; i++)
            {
                url += sastojci[i] + "&";
            }
            url += sastojci[sastojci.Count - 1];
            HttpClient httpKlijent = new HttpClient();
            var headers = httpKlijent.DefaultRequestHeaders;
            Uri izvor = new Uri(url);
            HttpResponseMessage httpOdgovor = new HttpResponseMessage();
            string odgovor = "";
            try
            {
                httpOdgovor = await httpKlijent.GetAsync(izvor);
                httpOdgovor.EnsureSuccessStatusCode();
                odgovor = await httpOdgovor.Content.ReadAsStringAsync();
            }
            catch(Exception e1)
            {
                odgovor = "Error: " + e1.HResult.ToString("X") + " Message:" + e1.Message;
            }
            Odgovor odgovorD = Odgovor.GetOdgovor(odgovor);
        }
    }
}
