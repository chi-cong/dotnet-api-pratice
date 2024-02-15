
public class Product
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductDescription { get; set; } = string.Empty;
    public ICollection<User> SubscribedUsers { get; set; } = new List<User>();
}

