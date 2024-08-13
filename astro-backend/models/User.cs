using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace astro_backend.models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int user_id { get; set; }

    [Required]
    public string username { get; set; }

    [Required]
    [EmailAddress] // Helps ensure valid email format
    public string email { get; set; }

    [Required]
    public string role { get; set; }

    public DateTime created_at { get; set; }



    public ICollection<Authentication_log>? Authentication_logs { get; set; }
    public User_security? User_security { get; set; }
    public Account? Account { get; set; }


}
