using System.Text.Json;

namespace src.NebulaPlugin.Common;

public class ErrorResponse
{
    public bool Success { get; set; }
    public string? Message { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }



}
