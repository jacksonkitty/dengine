namespace QuickSteno;

using System.Net.Http.Json;
using Microsoft.Extensions.Http;
using Polly;
using Polly.Extensions.Http;

public record ConfigValue(string Env, string App, string Key, string Value, DateTimeOffset? ExpiresOn = null, bool IsValid = false, bool IsSecure = false);

public sealed class ConfigClient(HttpClient client)
{
    public static HttpClient MakeClient(Uri target)
    {
        //https://learn.microsoft.com/en-us/dotnet/fundamentals/networking/http/httpclient-guidelines
#if RESILIENCE_HANDLER_IS_NO_LONGER_EVAL
var retryPipeline = new ResiliencePipelineBuilder<HttpResponseMessage>()
    .AddRetry(new HttpRetryStrategyOptions
    {
        BackoffType = DelayBackoffType.Exponential,
        MaxRetryAttempts = 3
    })
    .Build();

var socketHandler = new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(15) };
var resilienceHandler = new ResilienceHandler(retryPolicy) { InnerHandler = socketHandler };
var httpClient = new HttpClient(resilienceHandler);
#else

        var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), QuickLog);

        var socketHandler = new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(15) };
        var pollyHandler = new PolicyHttpMessageHandler(retryPolicy) { InnerHandler = socketHandler };
        var httpClient = new HttpClient(pollyHandler);
#endif

        httpClient.BaseAddress = target;
        return httpClient;
    }

    public async Task<ConfigValue?> GetSecretAsync(string env, string app, string key)
    {
        var path= $"/secrets/{env}/{app}/{key}";
        var test = await client.GetAsync(path);
        return await client.GetFromJsonAsync<ConfigValue>(path, CancellationToken.None);
    }

    static void QuickLog(DelegateResult<HttpResponseMessage> result, TimeSpan span) => Console.WriteLine("Retry");
}
