
using System;
using System.Net.Http;
using System.Threading.Tasks;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        if (args.Length != 1 || string.IsNullOrWhiteSpace(args[0]))
        {
            Console.WriteLine("This program expects exactly one paramter with url to check");
            Console.WriteLine();
            Console.WriteLine("Example usage:");
            Console.WriteLine("Docker.HealthCheck http://10.80.31.15/healthz/ready");
            return 2;
        }

        if (!Uri.TryCreate(args[0], UriKind.Absolute, out var uri))
        {
            Console.WriteLine(args[0] + " is not valid uri");
            return 2;
        }

        using (HttpClient client = new HttpClient())
        {
            client.Timeout = TimeSpan.FromSeconds(5);
            client.DefaultRequestHeaders.ConnectionClose = true;

            var result = await client.GetAsync(uri);

            if (!result.IsSuccessStatusCode)
            {
                return 1;
            }

            return 0;
        }
    }
}