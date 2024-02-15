using dotnet_api.Models.Interfaces;

namespace dotnet_api.Services.ModelServices
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public User? GetUserById(int id)
        {
            User? user;
            try
            {
                user = _context.Users.Single(u => u.UserId == id);
            }
            catch (Exception e)
            {
                user = null;
            }
            return user;
        }
        public User? GetUserByEmail(string email)
        {
            User? user;
            try
            {
                user = _context.Users.Single(u => u.Email == email);
            }
            catch (Exception e)
            {
                user = null;
            }
            return user;
        }

        public void CreateUser(User user)
        {
            User? existedUser = GetUserByEmail(user.Email);
            if (existedUser == null)
            {
                try
                {
                    _context.Users.Add(user);
                    _context.SaveChanges();
                } catch (Exception e)
                {

                }
            }
        }
    }
}
