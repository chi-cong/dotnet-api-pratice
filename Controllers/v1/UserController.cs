using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using dotnet_api.Models.Interfaces; 
using dotnet_api.Services.ModelServices;
using dotnet_api.Services.SecurityService;

namespace dotnet_api.Controllers.v1
{
    [ApiController]
    [Route("/api/v1/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IEncryptionService _encryptionService;

        public UserController(IUserService userService, IEncryptionService encryptionService)
        {
            _userService = userService;
            _encryptionService = encryptionService;
        }

        [HttpGet("{id}")]
        public ActionResult<User?> GetUserById(int id)
        {
            User? user = _userService.GetUserById(id);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);
        }


        [HttpPost("createUser")]
        public IActionResult CreateUser(User user)
        {
            if (_userService.GetUserByEmail(user.Email) == null)
            {
                user.Salt = _encryptionService.GenerateSalt();
                user.Password = _encryptionService.GetHashSha256(user.Password + user.Salt);
                _userService.CreateUser(user);
                return CreatedAtAction(nameof(CreateUser), user);
            }
            return Unauthorized();
        }

        [HttpPost("checkUserPassWord")]
        public IActionResult CheckUserPassword(User user)
        {
            User? checkedUser = _userService.GetUserByEmail(user.Email);
            if (checkedUser == null)
            {
                return Unauthorized(new { message = "Provided email or password is incorrect" });
            }
            string EncryptedPassword = (_encryptionService.GetHashSha256(user.Password + checkedUser.Salt)); 
            if (EncryptedPassword != checkedUser.Password)
            {
                return Unauthorized(new {message = "Provided email or password is incorrect"});
            }
            return Ok(new { message = "Succesful Authentication" });
        }
    }
}
