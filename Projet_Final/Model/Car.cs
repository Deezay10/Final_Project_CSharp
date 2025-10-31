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
    
    public int PriceExlTax { get; set; }
    
    public int PriceInclTax { get; set; }
    
    [ForeignKey("Client")]
    public Guid ClientId { get; set; }
    
    public Client Client { get; set; }

}