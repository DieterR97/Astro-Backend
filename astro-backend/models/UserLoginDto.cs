using System;

namespace astro_backend.models;

public class UserLoginDto
{
    public string Email { get; set; }
    public string Password { get; set; }
}
