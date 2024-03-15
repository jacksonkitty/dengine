namespace Core.Data;

public class Note(string filename)
{
    public string FileName { get; set; } = filename;
    public long Timestamp { get; set; }
}
