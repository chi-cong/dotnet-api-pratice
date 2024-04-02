namespace dotnet_api.Models.Dtos.UserDtos
{
    public class UserDtoData
    {
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public ICollection<Product> UsingProducts { get; set; } = new List<Product>();
    }
}
