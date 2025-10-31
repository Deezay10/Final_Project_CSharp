using Projet_Final.Model;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class CarDbContext :  DbContext
{
    // --- Tables principales ---
    public DbSet<Car> Cars { get; set; }
    public DbSet<Client> Clients { get; set; }

    // --- Constructeur ---
    public CarDbContext(DbContextOptions<CarDbContext> options)
        : base(options)
    {
    }
    
    // Constructeur vide pour EF CLI
    public CarDbContext() { }

    // --- Configuration des relations ---
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relation Classe -> Person (1..n)
        modelBuilder.Entity<Car>()
            .HasOne(ca => ca.Client)
            .WithMany(cl => cl.Cars)
            .HasForeignKey(ca => ca.ClientId);
    }

    // --- Configuration de la connexion ---
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Charger la configuration manuellement
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"Projet_Final/appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}
