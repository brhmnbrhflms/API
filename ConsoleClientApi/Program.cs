using System.Text;

namespace ConsoleClientApi
{
    internal class Program
    {

        static HttpClient? httpClient;
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            httpClient = new HttpClient();
            var responce = await httpClient.GetAsync("https://localhost:7231/WeatherForecast");
            Console.WriteLine(responce);
            Console.WriteLine(await responce.Content.ReadAsStringAsync());

            //добавление данных POST
            string newRecord = "{\r\n \"id\": 5, \r\n \"date\": \"01.01.2002\", \r\n \"degree\": -30, \r\n \"location\": \"Самара\"\r\n}";
            var stringContent = new StringContent(newRecord, Encoding.UTF8, "application/json");
            responce = await httpClient.PostAsync("https://localhost:7231/WeatherForecast", stringContent);
            Console.WriteLine(responce);

            //повторное получение данных POST
            responce = await httpClient.GetAsync("https://localhost:7231/WeatherForecast");
            Console.WriteLine(responce);
            Console.WriteLine(await responce.Content.ReadAsStringAsync());

            //обновление данных PUT-метод
            string updateRecord = "{\r\n \"id\": 5, \r\n \"date\": \"01.01.2002\", \r\n \"degree\": -30, \r\n \"location\": \"Самара\"\r\n}";
            stringContent = new StringContent(updateRecord, Encoding.UTF8, "application/json");
            responce = await httpClient.PutAsync("https://localhost:7231/WeatherForecast", stringContent);
            Console.WriteLine(responce);

            //повторное получение данных GET-метод
            responce = await httpClient.GetAsync("https://localhost:7231/WeatherForecast");
            Console.WriteLine(responce);
            Console.WriteLine(await responce.Content.ReadAsStringAsync());

            //удаление данных DELETE-метод
            responce = await httpClient.DeleteAsync("https://localhost:7231/WeatherForecast?id=6");
            Console.WriteLine(responce);
            Console.WriteLine(await responce.Content.ReadAsStringAsync());

            //Получение записи по Id GET-метод
            var id = Console.ReadLine();
            responce = await httpClient.GetAsync("https://localhost:7231/WeatherForecast" + id);
            Console.WriteLine(await responce.Content.ReadAsStringAsync());
        }
    }
}