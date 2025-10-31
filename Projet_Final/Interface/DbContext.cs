﻿
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
        
        
        modelBuilder.Entity<Car>()
            .HasOne(ca => ca.Client)
            .WithMany(cl => cl.Cars)
            .HasForeignKey(ca => ca.ClientId)
            .IsRequired(false);
    }

    // --- Configuration de la connexion ---
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            optionsBuilder.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        }
    }
}