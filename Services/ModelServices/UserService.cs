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
                }
                catch (Exception e)
                {

                }
            }
        }

        public void DeleteUser(User user)
        {
            try
            {
                _context.Users.Remove(user);
                _context.SaveChanges();

            }
            catch (Exception e)
            {


            }
        }

        public void UpdateUserGeneralInfo(User user)
        {
            var updatedUser = _context.Users.FirstOrDefault(u => u != null && u.UserId == user.UserId);
            if (updatedUser != null)
            {
                updatedUser.UserName = user.UserName;
                updatedUser.Email = user.Email;
                updatedUser.UsingProducts = user.UsingProducts;
                _context.SaveChanges();
            }
            else
            {
                throw new Exception("No User founded!");
            }
        }

        public void UpdateUserPassword(int userId, string userEncryptedPassword)
        {
            var user = _context.Users.FirstOrDefault(u => u != null && u.UserId == userId);
            if (user != null)
            {
                user.Password = userEncryptedPassword;
            }
            else
            {
                throw new Exception("No User Founded!");
            }
        }
    }
}
