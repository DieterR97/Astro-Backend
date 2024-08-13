using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace astro_backend.models;

public class Transaction
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int transaction_id { get; set; }


    [Required]
    public string transaction_type { get; set; }

    [Required]
    public decimal amount { get; set; }

    [Required]
    public DateTime timestamp { get; set; }


    //!foreign key = from_account_id (from user table)
    //!foreign key = to_account_id (from user table)

    // Foreign keys
    public int from_account_id { get; set; }
    public int to_account_id { get; set; }

    // Navigation properties
    [ForeignKey("from_account_id")]
    public Account? FromAccount { get; set; }

    [ForeignKey("to_account_id")]
    public Account? ToAccount { get; set; }

}
