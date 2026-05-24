using Microsoft.EntityFrameworkCore;

namespace PcBuilderAPI.Models
{
    public class PcBuilderContext : DbContext
    {
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Manufacturer> Manufacturers { get; set; }
        public virtual DbSet<Component> Components { get; set; }
        public virtual DbSet<Build> Builds { get; set; }
        public virtual DbSet<BuildComponent> BuildComponents { get; set; }

        public PcBuilderContext(DbContextOptions<PcBuilderContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }
    }
}