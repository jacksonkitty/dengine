namespace QuickSteno;

using System.Net.Http.Json;
using QuickTools.Core;

public record ConfigValue(string Env, string App, string Key, string Value, DateTimeOffset? ExpiresOn = null, bool IsValid = false, bool IsSecure = false);

public abstract class ConfigClient<T>
{
    private IResilientConfigClientOptions options;
    private HttpClient client;

    public ConfigClient(IResilientConfigClientOptions options)
    {
        this.options = options;
        this.client = ResilientClientFactory.Make(options);
    }

    public async Task<T?> GetSecretAsync(string key)
    {
        var path = $"/secrets/{options.Env}/{options.App}/{key}";
       // var test = await client.GetAsync(path);
        return await client.GetFromJsonAsync<T>(path, CancellationToken.None);
    }
}


public sealed class ExampleClient: ConfigClient<ConfigValue>
{
    public ExampleClient(IResilientConfigClientOptions options):base(options) { }
}
