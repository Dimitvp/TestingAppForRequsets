using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;

namespace ConsoleAppTestingRequests
{
    public class Program
    {
        static void Main(string[] args)
        {
            var httpClient = new HttpClient();
            var urlGet = "http://localhost:1236/api/TestRequests/TestingGetRequests";

            for (int i = 0; i < 5; i++)
            {
                var get = httpClient.GetAsync(urlGet).Result;

                Console.WriteLine("GET:");
                Console.WriteLine(get);
            }
        }
    }
}
