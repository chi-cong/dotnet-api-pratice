namespace dotnet_api.Models.Interfaces;
public interface IUserService
{
    public User? GetUserById(int id);
    public User? GetUserByEmail(string email);
    public void CreateUser(User user);

}

