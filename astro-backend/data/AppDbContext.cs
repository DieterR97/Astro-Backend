using System;
using Microsoft.EntityFrameworkCore;

namespace astro_backend.data;

public class AppDbContext : DbContext
{

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // TODO: All my Endpoint Controllers


    // public DbSet<User> Users { get; set; }

    // public DbSet<TodoItem> TodoItems { get; set; }

    // //build relationships between our tables/entities
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     //base.OnModelCreating(modelBuilder);

    //     modelBuilder.Entity<User>()
    //     .HasMany(u => u.TodoItems)
    //     .WithOne(t => t.User)
    //     .HasForeignKey(t => t.UserId);

    // }

}
