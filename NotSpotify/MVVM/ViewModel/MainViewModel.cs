using Newtonsoft.Json;
using NotSpotify.MVVM.Model;
using RestSharp;
using RestSharp.Authenticators.OAuth2;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NotSpotify.MVVM.ViewModel
{
    
    internal class MainViewModel
    {
        public ObservableCollection<Item> Songs { get; set; }
        public MainViewModel()
        {
            Songs = new ObservableCollection<Item>();
            PopulateCollection();
        }

        void PopulateCollection()
        {
            var client = new RestClient();
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator("BQDWgYtG4AVs-QApxrgsLgHxy28_pZoVQPgOvfzmtaLFlO2hYcBxlefswJzdMaU8uDHWf7YHf6UP9OLGFDNovgklxFvldwZ3p5-oPGlsxU4QcK9dWihUi8mifjSHTTmfeNIUQWjyO2YH3w", "Bearer");

            var request = new RestRequest("https://api.spotify.com/v1/browse/new-releases", Method.Get);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Content-Type", "application/json");

            var response = client.GetAsync(request).GetAwaiter().GetResult();
            var data = JsonConvert.DeserializeObject<TrackModel>(response.Content);

            for (int i = 0; i<data.Albums.Limit; i++)
            {
                var track = data.Albums.Items[i];
                track.Duration = "3:52";
                Songs.Add(track); 
            }
        }
    }
}
