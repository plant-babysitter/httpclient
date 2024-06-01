using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Text.Json;




namespace TcpClientTest
{

    internal class Program
    {

        public class Plant
        {

            public double height { get; set; }
            public double sunshine { get; set; }
            public double temperature { get; set; }
            public double waterPerSec { get; set; }
            public double humdity { get; set; }

        }

        static async Task Main(string[] args)
        {

            while (true)
            {
                await PostJsonDataAsync();

                await Task.Delay(5000);
            }

        }

        public static async Task PostJsonDataAsync()
        {
            Random random = new Random();
            double baseValue = 50.0;
            double deviation = 20.0;

            double[] array = new double[5];

            for (int i = 0; i < 5; i++)
            {
                array[i] = baseValue + deviation * (2 * random.NextDouble() - 1);
            }

            using var client = new HttpClient();


            var plant = new Plant
            {
                height = array[0],
                sunshine = array[1],
                temperature = array[2],
                waterPerSec = array[3],
                humdity = array[4]
            };

            string json = JsonSerializer.Serialize(plant);

            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync("https://dkssudgktpdy2.free.beeceptor.com", content);

            string result = await response.Content.ReadAsStringAsync();
            Console.WriteLine(result);

        }

    }
}