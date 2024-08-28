namespace astro_backend.models;

public class UserDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public AccountDto Account { get; set; }
}

public class AccountDto
{
    public int AccountId { get; set; }
    public decimal Balance { get; set; }

    public bool Active { get; set; }
    public int Account_status_id { get; set; }
    public ICollection<Transaction>? TransactionsFrom { get; set; }
    public ICollection<Transaction>? TransactionsTo { get; set; }
    public List<AssetDto>? Assets { get; set; }
    public Status? Status { get; set; }
}

public class AssetDto
{
    public int AssetId { get; set; }
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
}
