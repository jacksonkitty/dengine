namespace QuickTools.Core;

using Microsoft.Extensions.Http;
using Polly;
using Polly.Extensions.Http;

public static class ResilientClientFactory
{
    public static void QuickLog(DelegateResult<HttpResponseMessage> result, TimeSpan span) => Console.WriteLine("Retry");

    public static HttpClient Make(IResilientClientOptions options) => Make(new Uri(options.ConfigServiceUrl), QuickLog);

    public static HttpClient Make(Uri target) => Make(target, QuickLog);

    public static HttpClient Make(Uri target, Action<DelegateResult<HttpResponseMessage>, TimeSpan> logger)
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

        var retryPolicy = HttpPolicyExtensions
            .HandleTransientHttpError()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), logger);

        var socketHandler = new SocketsHttpHandler { PooledConnectionLifetime = TimeSpan.FromMinutes(15) };
        var pollyHandler = new PolicyHttpMessageHandler(retryPolicy) { InnerHandler = socketHandler };
        var httpClient = new HttpClient(pollyHandler);
#endif

        httpClient.BaseAddress = target;
        return httpClient;
    }

}
