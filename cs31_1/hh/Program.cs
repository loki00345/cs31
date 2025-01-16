using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace cs31_1
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Введіть посилання на фото:");
            string url = Console.ReadLine();

            Console.WriteLine("Введіть шлях для збереження :");
            string path = Console.ReadLine();

            Console.WriteLine("Введіть назву файлу :");
            string fileName = Console.ReadLine();

            string fullPath = Path.Combine(path, fileName);

            try
            {
                Console.WriteLine("Завантаження фото...");
                await DownloadImage(url, fullPath);
                Console.WriteLine($"Фото успішно збережено за адресою: {fullPath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка: {ex.Message}");
            }
        }

        static async Task DownloadImage(string url, string path)
        {
            using (HttpClient client = new HttpClient())
            {
                byte[] imageBytes = await client.GetByteArrayAsync(url);
                await File.WriteAllBytesAsync(path, imageBytes);
            }
        }
    }
}
