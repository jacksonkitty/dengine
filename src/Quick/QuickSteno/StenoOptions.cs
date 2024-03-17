namespace QuickSteno;

using QuickTools.Core;

public interface INoteManagerOptions
{
    string NoteRootFolder { get; set; }
}

public interface IStenoOptions : INoteManagerOptions, IResilientConfigClientOptions { }

public class StenoOptions : IStenoOptions
{
    public string ConfigServiceUrl { get; set; }
    public string NoteRootFolder { get; set; }
    public string App { get; set; }
    public string Env { get; set; }
}
