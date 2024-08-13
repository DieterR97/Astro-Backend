using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace astro_backend.models;

public class Status
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int status_id { get; set; }

    [Required]
    public string status_name { get; set; }

    [Required]
    public decimal total_amount_criteria { get; set; }

    [Required]
    public int transactions_criteria { get; set; }

    [Required]
    public decimal annual_interest_rate { get; set; }

    [Required]
    public decimal transaction_fee { get; set; }


    public ICollection<Account>? Accounts { get; set; }


}
