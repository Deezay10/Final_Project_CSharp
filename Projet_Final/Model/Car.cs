using System;
using Projet_Final;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Final.Model;

public class Car
{
    [Key]
    public Guid Id { get; set; } =  Guid.NewGuid();
    
    public string Brand { get; set; }
    
    public string Model { get; set; }
    
    public int Year { get; set; }
    
    public Boolean Status { get; set; }
    
    public string Color { get; set; }
    
    public float PriceExlTax { get; set; }
    
    public float PriceInclTax { get; set; }
    
    // Clé étrangère pour relier la table Purchase à la table Client
    
    [ForeignKey("Client")]
    public Guid? ClientId { get; set; }
    
    public Client? Client { get; set; }
    
    // Relation 1..1 : une voiture se vend qu'une seule fois
    public Purchase Purchase { get; set; }

}