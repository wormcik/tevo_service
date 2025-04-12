using Microsoft.EntityFrameworkCore;

namespace tevo_service.Entities
{
    public class AppDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public AppDbContext(DbContextOptions options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        public DbSet<Client> Client { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<AddressInfo> AddressInfo { get; set; }
        public DbSet<ContactInfo> ContactInfo { get; set; }
        public DbSet<Demand> Demand { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var defaultSchema = configuration.GetConnectionString("DefaultSchema");
            if (!string.IsNullOrEmpty(defaultSchema) && defaultSchema != "public")
            {
                modelBuilder.HasDefaultSchema(defaultSchema);
            }

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<Demand>()
                .HasOne(d => d.DelivererUser)
                .WithMany(u => u.DeliveredDemands)
                .HasForeignKey(d => d.DelivererUserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Demand>()
                .HasOne(d => d.RecipientUser)
                .WithMany(u => u.ReceivedDemands)
                .HasForeignKey(d => d.RecipientUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);
            _ = configurationBuilder.Properties<DateTime>().HaveColumnType("timestamp");
        }
    }
}
