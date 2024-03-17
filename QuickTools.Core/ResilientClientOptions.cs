namespace QuickTools.Core;

public interface IResilientClientOptions
{
    string ConfigServiceUrl { get; set; }
}

public interface IResilientConfigClientOptions : IResilientClientOptions
{
    string App { get; set; }
    string Env { get; set; }
}

public class ResilientClientOptions : IResilientClientOptions
{
    public string ConfigServiceUrl { get; set; }
}
