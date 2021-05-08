using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace VotifyTest
{
    static public class Controller
    {
        static readonly HttpClient httpClient = new HttpClient();

        // do kontaktu z api
        /* logowanie:
                METHOD: POST
                URL: /api/authenticate
                BODY: login*, password*

            eventy:
                HEADER: Auth; z tokenem JWT dla poniższych:
                pobieranie z przedziału:
                    METHOD: GET
                    URL: /api/auth/events?from=<int>&to=<int>
    
                pobieranie z dzisiaj:
                    METHOD: GET
                    URL: /api/auth/events
        */

        public static async Task<string> PostAsync()
        {
            var values = new Dictionary<string, string>
            {
                { "login", "login" },
                { "password", "password1" }
            };

            var content = new FormUrlEncodedContent(values);

            var response = httpClient.PostAsync("http://127.0.0.1/api/authenticate", content).Result;

            var responseString = response.Content.ReadAsStringAsync();

            return await responseString;
        }

        private static async Task<string> SendLoginRequest(string login, string password)
        {
            var body = new Dictionary<string, string>
            {
                { "login", login },
                { "password", password }
            };

            var content = new FormUrlEncodedContent(body);

            var response = httpClient.PostAsync("http://127.0.0.1/api/authenticate", content).Result;

            var responseString = response.Content.ReadAsStringAsync();

            return await responseString;
        }


        public static string Login(string login, string password)
        {
            var s = SendLoginRequest(login, password);

            JObject parsedJSON = JObject.Parse(s.Result);

            return (string)parsedJSON["token"];
        }

    }
}
