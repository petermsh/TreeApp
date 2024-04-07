using Microsoft.EntityFrameworkCore;
using TreeApp.Models;

namespace TreeApp.DAL;

public sealed class TreeDbContext : DbContext
{
    public DbSet<Node> Nodes { get; set; }

    public TreeDbContext(DbContextOptions<TreeDbContext> options)
        : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Node>()
            .HasMany(x => x.Childrens)
            .WithOne(x => x.ParentNode)
            .HasForeignKey(x => x.ParentNodeId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        base.OnModelCreating(modelBuilder);
    }
}