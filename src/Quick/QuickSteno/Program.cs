using Microsoft.Extensions.Configuration;
using QuickSteno;
using QuickTools.Core;

var environmentName = "Development";
var configuration = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json", true, true)
  .AddJsonFile($"appsettings.{environmentName}.json", true, true)
  .Build();//  .AddEnvironmentVariables();

var options = configuration.GetSection("StenoOptions").Get<StenoOptions>();
const string defaultRoot = @"C:\notes"; //C:\data\sample\notes

var example = new LocalClient(options);
var key = await example.GetSecretAsync("SubscriptionKey");
var region = await example.GetSecretAsync("ServiceRegion");

var spoken = await SpeechService.CaptureSingleUtterance(key?.Value, region?.Value, CancellationToken.None);
if (spoken is not null) { await FileWriter.WriteResultToFileAsync(defaultRoot, spoken, CancellationToken.None); }