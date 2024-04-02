namespace dotnet_api.Models.Interfaces;
public interface IUserService
{
    public User? GetUserById(int id);
    public User? GetUserByEmail(string email);
    public void CreateUser(User user);
    public void DeleteUser(User user);
    public void UpdateUserGeneralInfo(User user);
    public void UpdateUserPassword(int userId, string encryptedPassword);

}

