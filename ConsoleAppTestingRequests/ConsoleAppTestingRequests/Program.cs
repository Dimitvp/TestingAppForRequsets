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
            var urlPost = "http://localhost:1236/api/TestRequests/TestingPostRequests";
            var urlGet = "http://localhost:1236/api/TestRequests/TestingGetRequests";

            var postHeader = new PostModel()
            {
                Post = "I am posting."
            };

            for (int i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    postHeader.NumberOfRequest = i;
                    var requestJsonString = JsonConvert.SerializeObject(postHeader);
                    var stringContent = new StringContent( requestJsonString, Encoding.UTF8, "application/json");

                    var post = httpClient.PostAsync(urlPost, stringContent).Result;

                    Console.WriteLine("POST:");
                    Console.WriteLine(post);
                }
                else
                {
                    var get = httpClient.GetAsync(urlGet).Result;

                    Console.WriteLine("GET:");
                    Console.WriteLine(get);
                }
            }
        }
    }
}
