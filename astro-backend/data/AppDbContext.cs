using System;
using astro_backend.models;
using Microsoft.EntityFrameworkCore;

namespace astro_backend;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Account> Accounts { get; set; }
    public DbSet<Authentication_log> Authentication_logs { get; set; }
    public DbSet<Status> Statuss { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<User_security> User_securitys { get; set; }
    public DbSet<Asset> Assets { get; set; }

    //build relationships between our tables/entities
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // User to Authentication_log: One-to-Many
        modelBuilder.Entity<User>()
            .HasMany(user => user.Authentication_logs)
            .WithOne(auth => auth.User)
            .HasForeignKey(auth => auth.user_id);

        // User to User_security: One-to-One
        modelBuilder.Entity<User>()
            .HasOne(user => user.User_security)
            .WithOne(security => security.User)
            .HasForeignKey<User_security>(security => security.user_id);

        // User to Account: One-to-One
        modelBuilder.Entity<User>()
            .HasOne(user => user.Account)
            .WithOne(account => account.User)
            .HasForeignKey<Account>(account => account.user_id);

        // Status to Account: One-to-Many
        modelBuilder.Entity<Status>()
            .HasMany(status => status.Accounts)
            .WithOne(account => account.Status)
            .HasForeignKey(account => account.account_status_id);

        // Account to Transaction: One-to-Many
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.FromAccount)
            .WithMany(a => a.TransactionsFrom)
            .HasForeignKey(t => t.from_account_id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.ToAccount)
            .WithMany(a => a.TransactionsTo)
            .HasForeignKey(t => t.to_account_id)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Account>()
            .HasMany(a => a.Assets)
            .WithOne(a => a.Account)
            .HasForeignKey(a => a.account_id);
    }

}
