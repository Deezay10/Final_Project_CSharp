using System;
using Projet_Final;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Final.Model;

public class Purchase
{
    [Key] 
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public DateTime purchase_date { get; set; }
    
    // Clé étrangère pour relier la table Purchase à la table Client
    [ForeignKey("Client")]
    public Guid? ClientId { get; set; }
    
    public Client? Client { get; set; }
    
    // Clé étrangère pour relier la table Purchase à la table Car
    [ForeignKey("Car")]
    public Guid? CarId { get; set; }
    
    public Car? Car { get; set; }
}