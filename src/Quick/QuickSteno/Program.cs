using QuickSteno;

const string defaultRoot = @"C:\notes"; //C:\data\sample\notes
var httpClient = ConfigClient.MakeClient(new Uri($"http://lithium:8410"));

var example = new ConfigClient(httpClient);
var key = await example.GetSecretAsync("dev", "steno", "SubscriptionKey");
var region = await example.GetSecretAsync("dev", "steno", "ServiceRegion");

var spoken = await SpeechService.CaptureSingleUtterance(key?.Value, region?.Value, CancellationToken.None);
if (spoken is not null) { await FileWriter.WriteResultToFileAsync(defaultRoot, spoken, CancellationToken.None); }