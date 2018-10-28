using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Net;

namespace OpenWeatherAPIProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("What is the ZipCode of the area?");
            int zipCodeResponse = int.Parse(Console.ReadLine());
            Console.Clear();

            WebClient webClient = new WebClient();
#if DEBUG
            string apiKey = File.ReadAllText("appsettings.development.txt");
#else
            string apiKey = File.ReadAllText("appsettings.release.txt");
#endif
            string api = $"Https://api.openweathermap.org/data/2.5/weather?zip={zipCodeResponse}" + apiKey;
            string apiResponse = webClient.DownloadString(api);
            JObject job = JObject.Parse(apiResponse);
            double tempMath = double.Parse(job.GetValue("main")["temp"].ToString());
            double fahrenheitTemp = tempMath * 9 / 5 - 459.67;
            fahrenheitTemp = Math.Round(fahrenheitTemp, 1);
            Console.WriteLine($"The temperature is currently {fahrenheitTemp} degrees fahrenheit.");
            Console.ReadLine();
        }
    }
}
