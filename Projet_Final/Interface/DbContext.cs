using Projet_Final.Model;

namespace Projet_Final.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class DbContext : DBContext
{
    // --- Tables principales ---
    public DbSet<Car> Cars { get; set; }
    public DbSet<Client> Clients { get; set; }

    // --- Constructeur ---
    public DbContext(DbContextOptions<DbContext> options)
        : base(options)
    {
    }
    
    // Constructeur vide pour EF CLI
    public DbContext() { }

    // --- Configuration des relations ---
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Relation Classe -> Person (1..n)
        modelBuilder.Entity<Person>()
            .HasOne(p => p.Classe)
            .WithMany(c => c.Persons)
            .HasForeignKey(p => p.IdClasse);
    }

    // --- Configuration de la connexion ---
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Charger la configuration manuellement
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(@"C:\\Users\\julie\\RiderProjects\\CoursSupDeVinci\\CoursSupDeVinci\\appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}