using P3.Driver.OpenWeather.Api;

namespace P3.Driver.OpenWeather.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Hello World!");

            var client = new WeatherClient("c0c38cdc2ee9822e272fa2c9a25015ef");

            var x = client.GetOneCall(50, 50, []);
           
            System.Console.ReadLine();
        }
    }
}
