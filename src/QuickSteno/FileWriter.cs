namespace QuickSteno;

using System;
using System.Threading.Tasks;

internal class FileWriter
{

    const string DefaultHeader = @"categories:
- Raw
description: TODO
tags:
- s2t";

    internal static async Task WriteResultToFileAsync(string root, string resultText, CancellationToken token)
    {
        if (token.IsCancellationRequested)
        {
            return;
        }

        var now = DateTime.UtcNow;
        var timestamp = $"{DateTime.UtcNow.Ticks}.md";
        var path = Path.Combine(root, "raw", timestamp);

        using (var fs = File.CreateText(path))
        {
            Console.WriteLine("Created.");
            await fs.WriteLineAsync("---");
            foreach (var line in (DefaultHeader.Split(Environment.NewLine)))
            {
                await fs.WriteLineAsync(line);
            }

            // https://learn.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings
            await fs.WriteLineAsync($"recorded: {now.ToString("u")}");
            await fs.WriteLineAsync("---");

            // Write Text as "markdown"
            await fs.WriteLineAsync(resultText);
        }
    }
}
