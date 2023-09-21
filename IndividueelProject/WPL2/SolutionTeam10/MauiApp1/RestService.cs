using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MauiApp1
{
    internal class RestService
    {
        private string REST_URL { get; set; }

        private bool IsAndroid() =>
        DeviceInfo.Current.Platform == DevicePlatform.Android;

        string factions;
        public RestService()
        {
            REST_URL = IsAndroid() ? "http://10.0.2.2:7155/api/Faction/GetFactions" : "http://localhost:7155/api/Faction/GetFactions";

        }
        public HttpClient httpClient = new HttpClient();

        public async Task<string> getFactions()
        {
                try
                {
                    HttpResponseMessage Response = await httpClient.GetAsync(REST_URL);
                    if (Response.IsSuccessStatusCode)
                    {
                        factions = await Response.Content.ReadAsStringAsync();
                    }

                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error:", ex.Message);
                }
                return factions;
            }

    }
}
