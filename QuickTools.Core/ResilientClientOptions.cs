namespace QuickTools.Core;

public interface IResilientClientOptions
{
    string ConfigServiceUrl { get; set; }
}

public class ResilientClientOptions : IResilientClientOptions
{
    public string ConfigServiceUrl { get; set; }
}
