using System.Net.Http.Json;
using QuickTools.Core;

var httpClient = ResilientClientFactory.Make(new Uri($"https://localhost:7096"), ResilientClientFactory.QuickLog);
var example = new ExampleClient(httpClient);

var i = await example.GetSecretAsync("example", "example", "fake"); //env/app/key
if (i is not null) { Console.WriteLine($"Found {i.Key}!"); }

Console.ReadKey();

// Helpers

internal sealed class ExampleClient(HttpClient client)
{
    public async Task<ConfigValue?> GetSecretAsync(string env, string app, string key)
    {
        return await client.GetFromJsonAsync<ConfigValue>($"/secrets/{env}/{app}/{key}");
    }
}

public record ConfigValue(string Env, string App, string Key, string Value, DateTimeOffset? ExpiresOn = null, bool IsValid = false, bool IsSecure = false);
