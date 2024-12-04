using Microsoft.EntityFrameworkCore;

namespace ComputerApiHetfo.Models;

public partial class ComputerContext : DbContext
{
    public ComputerContext()
    {
    }

    public ComputerContext(DbContextOptions<ComputerContext> options)
        : base(options)
    {
        Database.EnsureCreated();
    }
    public virtual DbSet<Comp> Comps { get; set; }
    public virtual DbSet<Osystem> Osystems { get; set; }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
