using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace astro_backend.models;

public class User_security
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int security_id { get; set; }

    [Required]
    public string password_hash { get; set; }

    [Required]
    public string latest_otp_secret { get; set; }

    [Required]  
    public DateTime updated_at { get; set; }




    //!foreign key = user_id (from user table)

    //foreign key
    public int user_id { get; set; }

    //navigation property
    //this user value is only one user - which is why we say User datatype
    public User? User { get; set; }

}
