using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HackatonAdminPanel.Models;
public class AccountNft
{
    [JsonPropertyName("flags")]
    public int flags { get; set; }

    [JsonPropertyName("issuer")]
    public string issuer { get; set; }

    [JsonPropertyName("nfTokenID")]
    public string nfTokenID { get; set; }

    [JsonPropertyName("nfTokenTaxon")]
    public int nfTokenTaxon { get; set; }

    [JsonPropertyName("transferFee")]
    public int transferFee { get; set; }

    [JsonPropertyName("uri")]
    public string uri { get; set; }

    [JsonPropertyName("nft_serial")]
    public int nft_serial { get; set; }
}

public class Root
{
    [JsonPropertyName("account")]
    public string account { get; set; }

    [JsonPropertyName("account_nfts")]
    public List<AccountNft> account_nfts { get; set; }

    [JsonPropertyName("ledger_current_index")]
    public int ledger_current_index { get; set; }

    [JsonPropertyName("validated")]
    public bool validated { get; set; }
}
