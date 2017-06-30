using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppClient
{
    using IdentityModel;
    using IdentityModel.Client;
    using System.Net.Http;

    class Program
    {
        static void Main(string[] args)
        {
            var response = GetClientToken();
            CallApi(response);
            Console.Read();


        }


        static TokenResponse GetClientToken()
        {
            var client = new TokenClient("http://localhost:5000/connect/token", "silicon", "F621F470-9731-4A25-80EF-67A6F7C5F4B8");
            return client.RequestClientCredentialsAsync("api1").Result;
        }

        static void CallApi(TokenResponse response)
        {
            var client = new HttpClient();
            client.SetBearerToken(response.AccessToken);

            Console.WriteLine(client.GetStringAsync("http://localhost:14869/test/get").Result);

        }
    }
}
