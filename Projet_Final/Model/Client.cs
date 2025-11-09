using System;
using Projet_Final;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Projet_Final.Model;

public class Client
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    
    public string Lastname { get; set; }
    
    public string Firstname { get; set; }
    
    public DateTime Birthdate { get; set; }
    
    public string PhoneNumber { get; set; }
    
    public string Email { get; set; }
    
    // Relation 1..n : un client peut avoir plusieurs voitures
    public ICollection<Car> Cars { get; set; } = new List<Car>();
    
    public List<Purchase> Purchases { get; set; } = new();
}