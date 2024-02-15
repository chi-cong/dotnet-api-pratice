using System;

public class User
{
	public int UserId { get; set; }
	public string UserName { get; set; } = string.Empty;
	public string Password { get; set; } = string.Empty;
	public string Salt { get; set; } = string.Empty;
	public string Email { get; set; } = string.Empty;
	public ICollection<Product> UsingProducts { get; set; } = new List<Product>();
}
