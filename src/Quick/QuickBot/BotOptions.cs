using QuickTools.Core;

public interface IBotOptions : IResilientConfigClientOptions { }

public class BotOptions : IBotOptions
{
    public string ConfigServiceUrl { get; set; }
    public string App { get; set; }
    public string Env { get; set; }
}
