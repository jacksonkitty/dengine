using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using QuickTools.Core;

var environmentName = "Development";
var configuration = new ConfigurationBuilder()
  .AddJsonFile($"appsettings.json", true, true)
  .AddJsonFile($"appsettings.{environmentName}.json", true, true)
  .Build();//  .AddEnvironmentVariables();

var options = configuration.GetSection("BotOptions").Get<BotOptions>();
var example = new LocalClient(options);

var token = await example.GetSecretAsync("discordToken");

//https://discordnet.dev/guides/getting_started/first-bot.html
var client = new DiscordSocketClient();
client.Log += Log;

await client.LoginAsync(TokenType.Bot, token.Value);
await client.StartAsync();
await Task.Delay(TimeSpan.FromMinutes(5)); //Limited time only!

// Helpers
// https://discordnet.dev/guides/getting_started/samples/first-bot/structure.cs , but improved!
static Task Log(LogMessage message)
{
    var color = message.Severity switch {
        LogSeverity.Critical => ConsoleColor.Red,
        LogSeverity.Error => ConsoleColor.Red,
        LogSeverity.Warning => ConsoleColor.Yellow,
        LogSeverity.Info => ConsoleColor.White,
        LogSeverity.Verbose => ConsoleColor.DarkGray,
        LogSeverity.Debug => ConsoleColor.DarkGray,
        _ => ConsoleColor.DarkBlue
    };
            
    Console.ForegroundColor = color;    
    Console.WriteLine($"{DateTime.Now,-19} [{message.Severity,8}] {message.Source}: {message.Message} {message.Exception}");
    Console.ResetColor();

    return Task.CompletedTask;
}