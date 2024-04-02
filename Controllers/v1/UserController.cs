using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using dotnet_api.Models.Interfaces;
using dotnet_api.Services.ModelServices;
using dotnet_api.Services.SecurityService;
using dotnet_api.Models.Dtos.UserDtos;

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

        //[Authorize]
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
        public IActionResult CreateUser([FromBody]UserDtoCreate createdUser)
        {
            User user = new User() {UserName = createdUser.UserName ,Email = createdUser.Email};
            if (_userService.GetUserByEmail(user.Email) == null)
            {
                user.Salt = _encryptionService.GenerateSalt();
                user.Password = _encryptionService.HashPassword(createdUser.Password, user.Salt);
                _userService.CreateUser(user);
                return CreatedAtAction(nameof(CreateUser), user);
            }
            return Unauthorized();
        }

        [HttpPost("login")]
        public IActionResult CheckUserPassword([FromBody]UserDtoLogin attemptingUser)
        {
            User? checkedUser = _userService.GetUserByEmail(attemptingUser.UserNameEmail);
            if (checkedUser == null)
            {
                return Unauthorized(new { message = "Provided email or password is incorrect" });
            }
            bool isValidPassword = _encryptionService.VerifyHash(checkedUser.Password, attemptingUser.Password, checkedUser.Salt);
            if (!isValidPassword)
            {
                return Unauthorized(new { message = "Provided email or password is incorrect" });
            }
            return Ok(new { message = "Succesful Authentication" });
        }

        [HttpDelete("deleteUser{id}")]
        public IActionResult DeleteUser(int id)
        {
            User? user = _userService.GetUserById(id);
            if (user is null)
            {
                return NotFound();
            }
            _userService.DeleteUser(user);
            return Ok("User deleted");
        }

        [HttpPost("updateUserGeneralInfo")]
        public IActionResult UpdateUser([FromBody]User user)
        {
            try
            {
                _userService.UpdateUserGeneralInfo(user);
            } catch (Exception e)
            {
                return NotFound("No specific user founded");
            }
            return Ok("Success updating user");
        }
    }
}
