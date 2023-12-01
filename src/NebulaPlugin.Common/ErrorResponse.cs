using System.Text.Json;

namespace src.NebulaPlugin.Common;

public class ErrorResponse
{
    public string? Message { get; set; }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }



}
