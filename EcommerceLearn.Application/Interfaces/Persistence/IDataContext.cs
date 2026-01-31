using EcommerceLearn.Domain.Entities.Addresses;
using EcommerceLearn.Domain.Entities.Auth;
using EcommerceLearn.Domain.Entities.Books;
using EcommerceLearn.Domain.Entities.Carts;
using EcommerceLearn.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore;
using EcommerceLearn.Domain.Entities.Users;

namespace EcommerceLearn.Application.Interfaces.Persistence;

public interface IDataContext
{
    DbSet<User> Users { get; }
    DbSet<UserAddress> UserAddresses { get; }
    DbSet<Book> Books { get; }
    DbSet<Cart> Carts { get; }
    DbSet<Order> Orders { get; }
    DbSet<CartItem> CartItems { get; }
    DbSet<OrderItem> OrderItems { get; }
    DbSet<EmailVerification> EmailVerifications { get; }
    DbSet<PasswordVerification> PasswordVerifications { get; }
    DbSet<RefreshToken> RefreshTokens { get; }


    Task<int> SaveChangesAsync(CancellationToken ct = default);
}