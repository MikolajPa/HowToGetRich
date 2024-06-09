using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HackatonAdminPanel.Models;
public class UserDto
{
    
    [Key]
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("isDistributor")]
    public bool IsDistributor { get; set; }

    [JsonPropertyName("walletId")]
    public string WalletId { get; set; }


    public string accountId { get; set; }
}


public class UserRequest
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public bool IsDistributor { get; set; }
    public string WalletId { get; set; }
    public string AccountId { get; set; }
}
