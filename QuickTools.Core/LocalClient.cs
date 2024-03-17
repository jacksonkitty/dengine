namespace QuickTools.Core;

using System.Diagnostics;

[DebuggerDisplay("[{Env}:{App}:{Key}]::V:{IsValid}::S:{IsSecure}::{ExpiresOn}")]
public record ConfigValue(string Env, string App, string Key, string Value, DateTimeOffset? ExpiresOn = null, bool IsValid = false, bool IsSecure = false);

public sealed class LocalClient : ConfigClient<ConfigValue>
{
    public LocalClient(IResilientConfigClientOptions options) : base(options) { }
}