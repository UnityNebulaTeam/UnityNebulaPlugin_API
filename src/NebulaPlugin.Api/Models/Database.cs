using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace NebulaPlugin.Api.Models;

public class Database
{
    [Key]
    public Guid Id { get; set; }
    public string ConnectionString { get; set; } = null!;
    public string KeyIdentifier { get; set; } = null!;

    [ForeignKey("User")]
    public string UserId { get; set; }
    [JsonIgnore]
    public User User { get; set; }
}
