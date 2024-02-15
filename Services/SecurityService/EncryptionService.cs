using System.Security.Cryptography;
using System.Text;
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

    public string GetHashSha256(string input)
    {
        byte[] originalStringByte = Encoding.UTF8.GetBytes(input);
        byte[] hashedResult = SHA256.HashData(originalStringByte);
        return Convert.ToBase64String(hashedResult);
    }
}
