using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace astro_backend.models;

public class Account
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int account_id { get; set; }


    [Required]
    public decimal balance { get; set; }

    [Required]
    public bool active { get; set; }



    //!foreign key = user_id (from user table)
    //!foreign key = account_status_id (from Status table)

    //foreign key
    public int user_id { get; set; }
    public int account_status_id { get; set; }

    //navigation property
    //this user value is only one user - which is why we say User datatype
    public User? User { get; set; }
    public Status? Status { get; set; }

    public ICollection<Transaction>? TransactionsFrom { get; set; }
    public ICollection<Transaction>? TransactionsTo { get; set; }

    public ICollection<Asset> Assets { get; set; } = new List<Asset>();

    public decimal GetTotalBalance()
    {
        return Assets.Sum(asset => asset.price * asset.amount);
    }

}
