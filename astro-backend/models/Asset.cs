using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace astro_backend.models;


public class Asset
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int asset_id { get; set; }

    [Required]
    public string name { get; set; }

    [Required]
    public string abbreviation { get; set; }

    [Required]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal price { get; set; }

    [Required]
    public decimal amount { get; set; } // The quantity of the asset owned

    // Foreign key for Account
    public int account_id { get; set; }

    // Navigation property back to Account
    public Account Account { get; set; }
}
