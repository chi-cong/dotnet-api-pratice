using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using dotnet_api.Models.Interfaces;

namespace dotnet_api.Services.SecurityService;

public class EncryptionService : IEncryptionService
{
    public string GenerateSalt()
    {
        string salt = string.Empty;
        byte[] saltBytes = new byte[16];
        using (RNGCryptoServiceProvider rngCsp = new())
        {
            rngCsp.GetBytes(saltBytes);
        }
        salt = Convert.ToBase64String(saltBytes);
        return salt;
    }
    public string GenerateSalt(int saltLength)
    {
        string salt = "";
        byte[] saltBytes = new byte[saltLength];
        using (RNGCryptoServiceProvider rngCsp = new())
        {
            rngCsp.GetBytes(saltBytes);
        }
        salt = Convert.ToBase64String(saltBytes);
        return salt;
    }

    public string HashPassword(string plaintext, string salt)
    {

        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: plaintext, salt: Convert.FromBase64String(salt), iterationCount: 9999, numBytesRequested : 256 / 8, prf: KeyDerivationPrf.HMACSHA256));
    }

    public bool VerifyHash(string hashedString, string plaintext, string salt)
    {
        return hashedString == HashPassword(plaintext, salt);
    }
}
