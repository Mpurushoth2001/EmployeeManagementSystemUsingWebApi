using System.Net.Http;
using System;

namespace NewWebClient
{

    public class Program
    {
        public static async void Main(string[] args) 
        {
        
            Console.WriteLine("Press any Key ....");
            Console.ReadLine();
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync("https://localhost:7127/api/Employee");
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    string message = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(message);
                }
                else
                {
                    Console.WriteLine($"response Error Code: {response.StatusCode}");
                }
            }
            
        }
    }
}
