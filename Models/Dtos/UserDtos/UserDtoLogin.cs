namespace dotnet_api.Models.Dtos.UserDtos
{
    public class UserDtoLogin
    {
        public required string UserNameEmail { get; set; }
        public required string Password { get; set; }
    }
}
