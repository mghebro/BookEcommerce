using EcommerceLearn.Application.Interfaces.Persistence;
using EcommerceLearn.Domain.Entities.Auth;
using EcommerceLearn.Domain.Entities.Books;
using EcommerceLearn.Domain.Entities.Carts;
using EcommerceLearn.Domain.Entities.Orders;
using EcommerceLearn.Domain.Entities.Reviews;
using Microsoft.EntityFrameworkCore;
using EcommerceLearn.Domain.Entities.Users;

namespace EcommerceLearn.Infrastructure.Persistence;

public sealed class DataContext : DbContext, IDataContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<Review> Reviews => Set<Review>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();
    public DbSet<EmailVerification> EmailVerifications => Set<EmailVerification>();
    public DbSet<PasswordVerification> PasswordVerifications => Set<PasswordVerification>();
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }
}