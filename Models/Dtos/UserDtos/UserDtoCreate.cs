namespace dotnet_api.Models.Dtos.UserDtos
{
    public class UserDtoCreate
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
    }
}
