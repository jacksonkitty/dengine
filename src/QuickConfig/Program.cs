using System.Text.Json.Serialization;

// Setup
var explainerText = "This example secret lives in the example env, and is part of the Example app";
var eoly = new DateTimeOffset(DateTime.UtcNow.Year, 1, 1, 0, 0, 0, TimeSpan.Zero).AddMicroseconds(-1); // End of last year. 
var secrets = new Dictionary<string, ConfigValue>
{
    { "fake", new ConfigValue("example", "example", "fake", explainerText, null, true, false) },
    { "invalid", new ConfigValue("example", "example", "invalid", explainerText, null, false, false) },
    { "lastyear", new ConfigValue("example", "example", "lastyear", explainerText, eoly, true, false) },
    { "secret", new ConfigValue("example", "example", "secret", explainerText, null, true, true) },
    { "bad", new ConfigValue("example", "example", "bad", explainerText, null, true, true) },
    { "expired", new ConfigValue("example", "example", "expired", explainerText, eoly, true, true) }
};

var builder = WebApplication.CreateBuilder(args);
builder.Services.ConfigureHttpJsonOptions(options => { options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);});

// App
var app = builder.Build();

var secretsApi = app.MapGroup("/secrets");
secretsApi.MapGet("/{env}/{app}/{key}", (string env, string app, string key) => HandleGet(env, app, key));
secretsApi.MapPost("/", (ConfigValue secret) => HandlePost(secret));

app.UseHttpsRedirection();
app.Run();

// Helpers
IResult HandleGet(string env, string app, string key)
{
    if (secrets.ContainsKey(key))
    {
        var secret = secrets.FirstOrDefault(a => a.Key == key);
     
        if (secret.Value.Env == env && secret.Value.App == app)
        {
            return Results.Ok(secret.Value);
        }
    }

    return Results.NotFound();
}

IResult HandlePost(ConfigValue secret)
{
    try
    {
        app.Logger.LogInformation("Saving {key}", secret.Key);
        secrets[secret.Key] = secret;
        return Results.Accepted(secret.Value);
    }
    catch (Exception ex) { }
    return Results.BadRequest();
}

public record ConfigValue(string Env, string App, string Key, string Value, DateTimeOffset? ExpiresOn = null, bool IsValid = false, bool IsSecure = false);

[JsonSerializable(typeof(ConfigValue[]))]
internal partial class AppJsonSerializerContext : JsonSerializerContext { }