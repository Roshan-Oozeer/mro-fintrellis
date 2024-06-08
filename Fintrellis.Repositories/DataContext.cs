using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Fintrellis.Models;
using Fintrellis.Common.Extensions;

namespace Fintrellis.Repositories;

public class DataContext : DbContext
{
    private readonly ILogger<DataContext> _logger;

    public DataContext()
     : this(new DbContextOptions<DataContext>(), null) { }

    public DataContext(DbContextOptions<DataContext> options,
        ILogger<DataContext> logger) : base(options)
    {
        _logger = logger;
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Blog> Blogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Configure the foreign key relationship between Blog and User
        modelBuilder.Entity<Blog>()
            .HasOne(b => b.User)
            .WithMany(u => u.Blogs)
            .HasForeignKey(b => b.UserId);

        base.OnModelCreating(modelBuilder);
    }

    public override int SaveChanges()
    {
        try
        {
            return base.SaveChanges();
        }
        catch (Exception ex)
        {
            ex.LogError(_logger, "An error occurred while saving changes to the database.");
            throw;
        }
    }
}

