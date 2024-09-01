namespace astro_backend.models;

public class UserDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public AccountDto Account { get; set; }
}

public class AccountDto
{
    public int AccountId { get; set; }
    public decimal Balance { get; set; }
    public bool Active { get; set; }
    public int Account_status_id { get; set; }
    public List<TransactionDto> TransactionsTo { get; set; } = new List<TransactionDto>();
    public List<TransactionDto> TransactionsFrom { get; set; } = new List<TransactionDto>();
    public List<AssetDto>? Assets { get; set; }
    public StatusDto? Status { get; set; }
    public AstroDto? Astro { get; set; }
    public int TotalTransactions { get; set; }
}

public class AssetDto
{
    public int AssetId { get; set; }
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    public decimal Price { get; set; }
    public decimal Astro_price { get; set; }
    public decimal Tokens { get; set; }
}

public class AstroDto
{
    public int AstroId { get; set; }
    public string Name { get; set; }
    public string Abbreviation { get; set; }
    public decimal Price { get; set; }
    public decimal Tokens { get; set; }

}

public class StatusDto
{
    public int StatusId { get; set; }

    public string StatusName { get; set; }

    public decimal TotalAmountCriteria { get; set; }

    public int TransactionsCriteria { get; set; }

    public decimal AnnualInterestRate { get; set; }

    public decimal TransactionFee { get; set; }
}

public class TransactionDto
{
    public int TransactionId { get; set; }
    public string TransactionType { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; }

    // Optionally include Account IDs if needed for client-side logic
    public int FromAccountId { get; set; }
    public int ToAccountId { get; set; }
}




