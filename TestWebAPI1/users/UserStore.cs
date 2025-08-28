using System.Text.Json.Serialization;

namespace TestWebAPI1.users;

public class User
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }
}

public class Admin : User
{
    [JsonPropertyName("email")]
    public required string Email { get; set; }
}

public class UserStore
{
    [JsonPropertyName("admins")]
    public required Admin[]  Admins { get; set; }

    [JsonPropertyName("users")]
    public required User[]  Users { get; set; }
}