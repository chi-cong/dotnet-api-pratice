namespace dotnet_api.Models.Interfaces;
public interface IEncryptionService
{
    public string GenerateSalt();
    public string GetHashSha256(string input);
}

