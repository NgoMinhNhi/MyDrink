using MyDrink.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MyDrink.Helpers
{
    public struct dataLogin
    {
        public string useName;
        public string password;
        public dataLogin(string usename, string pass)
        {
            useName = usename;
            password = pass;
        }
    }

    class APIRequestHelper
    {
        static HttpClient client = new HttpClient();

         public async Task<User> LoginUser(dataLogin login)
        {
            client.BaseAddress = new Uri("http://localhost:8001/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            User user = null;
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/user/login", login);
            if (response.IsSuccessStatusCode)
            {
                user = await response.Content.ReadAsAsync<User>();
            }

            // return URI of the created resource.
            return user;
        }
    }
}
