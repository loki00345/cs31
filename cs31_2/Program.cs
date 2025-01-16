using Newtonsoft.Json;

namespace cs31_2
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Оберіть категорію для отримання даних:");
            Console.WriteLine("1. People");
            Console.WriteLine("2. Films");
            Console.WriteLine("3. Starships");
            Console.WriteLine("4. Vehicles");
            Console.WriteLine("5. Species");
            Console.WriteLine("6. Planets");

            string[] categories = { "people", "films", "starships", "vehicles", "species", "planets" };
            int choice = int.Parse(Console.ReadLine());

            if (choice < 1 || choice > 6)
            {
                Console.WriteLine("Неправильний вибір.");
                return;
            }

            string category = categories[choice - 1];
            string apiUrl = $"https://swapi.py4e.com/api/{category}/";

            try
            {
                Console.WriteLine($"Отримання даних з категорії: {category}...");
                string data = await FetchData(apiUrl);
                Console.WriteLine($"Результати:\n{data}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        static async Task<string> FetchData(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                dynamic parsedJson = JsonConvert.DeserializeObject(jsonResponse);
                return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
            }
        }
    }
}
