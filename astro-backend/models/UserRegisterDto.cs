using System;

namespace astro_backend.models;

public class UserRegisterDto
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}
