using System;
using Isopoh.Cryptography.Argon2;

namespace astro_backend.Services;

public class Argon2Hash
{
    private readonly string _password;

    public Argon2Hash(string password)
    {
        _password = password;
    }

    public string Hash()
    {
        return Argon2.Hash(_password);
    }
}
