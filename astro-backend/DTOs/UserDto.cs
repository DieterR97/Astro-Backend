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
    public List<AssetDto> Assets { get; set; }
}

public class AssetDto
{
    public int AssetId { get; set; }
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    public decimal Price { get; set; }
    public decimal Amount { get; set; }
}
