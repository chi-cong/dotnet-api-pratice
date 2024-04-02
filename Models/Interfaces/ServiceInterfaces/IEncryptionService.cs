namespace dotnet_api.Models.Interfaces;
public interface IEncryptionService
{
    public string GenerateSalt();
    public string HashPassword(string plaintext, string salt);
    public bool VerifyHash(string hashedString ,string plaintext, string salt);
}

