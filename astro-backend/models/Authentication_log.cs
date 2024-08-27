using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace astro_backend.models;

public class Authentication_log
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int log_id { get; set; }

    public DateTime? login_time { get; set; }

    public DateTime? logout_time { get; set; }

    [Required]
    public string ip_address { get; set; }

    [Required]
    public string device_info { get; set; }


    //!foreign key = user_id (from user table)

    //foreign key
    public int user_id { get; set; }

    //navigation property
    //this user value is only one user - which is why we say User datatype
    public User? User { get; set; }


}
