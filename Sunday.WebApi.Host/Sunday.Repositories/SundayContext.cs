using Microsoft.EntityFrameworkCore;
using Sunday.Repositories.Entities;

namespace Sunday.Repositories
{
    public class SundayContext: DbContext
    {
        public SundayContext()
        {
        }

        public SundayContext(DbContextOptions<SundayContext> options) : base(options)
        {
        }

        public DbSet<Municipality> Municipalities { get; set; }
        public DbSet<Tax> Taxes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=EFProviders.InMemory;Trusted_Connection=True;ConnectRetryCount=0");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Municipality>().HasKey(x => x.Uuid);
            modelBuilder.Entity<Municipality>()
                .HasMany(t => t.Taxes)
                .WithOne(t => t.Municipality)
                .HasForeignKey(d => d.MunicipalityUuid)
                .IsRequired();
            modelBuilder.Entity<Municipality>().HasIndex(x => x.Name).IsUnique(); // Indexes do not work when working with an InMemoryDataBase.
            modelBuilder.Entity<Municipality>().Property(x => x.Uuid).ValueGeneratedOnAdd();

            modelBuilder.Entity<Tax>().HasKey(x => x.Uuid);
        }
    }
}
