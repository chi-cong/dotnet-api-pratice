using Microsoft.EntityFrameworkCore;
using System;

public interface IDataContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Product> Products { get; set; }
}
