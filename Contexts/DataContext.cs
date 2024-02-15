using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext, IDataContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }
}

