using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.Web.Http;
using System.Threading.Tasks;

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
            string url = BuildUrl();
            string odgovor = await SendReceiveHTTPRequest(url);
            Odgovor odgovorD = Odgovor.GetOdgovor(odgovor);
            IspisUListu(odgovorD);
        }

        private static async Task<string> SendReceiveHTTPRequest(string url)
        {
            HttpClient httpKlijent = new HttpClient();
            Uri izvor = new Uri(url);
            string odgovor;
            try
            {
                HttpResponseMessage httpOdgovor = await httpKlijent.GetAsync(izvor);
                httpOdgovor.EnsureSuccessStatusCode();
                odgovor = await httpOdgovor.Content.ReadAsStringAsync();
            }
            catch (Exception e1)
            {
                odgovor = "Error: " + e1.HResult.ToString("X") + " Message:" + e1.Message;
            }

            return odgovor;
        }

        private string BuildUrl()
        {
            string url = "http://www.recipepuppy.com/api/?i=";
            List<string> sastojci = new List<string>();
            char[] separatori = { ',', ' ' };
            string unos = tbUnos.Text;
            sastojci.AddRange(unos.Split(separatori));
            for (int i = 0; i < sastojci.Count - 1; i++)
            {
                url += sastojci[i] + ",";
            }
            url += sastojci[sastojci.Count - 1];
            return url;
        }

        private void IspisUListu(Odgovor odgovorD)
        {
            lista.Items.Clear();
            if (odgovorD != null)
            {
                foreach (Recept r in odgovorD.Results)
                {
                    lista.Items.Add(r);
                }
            }
            else
            {
                lista.Items.Add("x");
            }
            return;
        }
    }
}
