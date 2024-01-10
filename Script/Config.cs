using System.Text.Json.Serialization;

namespace Script;

public class Config
{

    [JsonPropertyName("generatedFileName")]
    public required string GeneratedFileName { get; set; }

    [JsonPropertyName("files")]
    public required List<ConfigFileInfo> Files { get; set; }
}


public class ConfigFileInfo
{
    [JsonPropertyName("index")]
    public required int Index { get; set; }

    [JsonPropertyName("name")]
    public required string FileName { get; set; }
}