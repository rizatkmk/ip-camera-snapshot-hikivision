using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        string cameraIp = "192.0.2.10"; // Örnek IP (temsilidir)
        string username = "admin";
        string password = "12345";
        string imageUrl = $"http://{cameraIp}/ISAPI/Streaming/channels/101/picture";
        string savePath = "hikvision_snapshot.jpg";

        var handler = new HttpClientHandler
        {
            Credentials = new NetworkCredential(username, password)
        };

        using (HttpClient client = new HttpClient(handler))
        {
            try
            {
                Console.WriteLine("Hikvision görüntü alınıyor...");
                var response = await client.GetAsync(imageUrl);
                response.EnsureSuccessStatusCode();
                var image = await response.Content.ReadAsByteArrayAsync();
                await File.WriteAllBytesAsync(savePath, image);
                Console.WriteLine("Görüntü kaydedildi: " + savePath);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Hata: " + ex.Message);
            }
        }
    }
}
