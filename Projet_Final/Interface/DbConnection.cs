using Projet_Final.Interface;
using Projet_Final.Model;

namespace Projet_Final.InterfaceRepository;
using Npgsql;

//Déclare une classe DbConnection accessible dans d'autres parties du code
public class DbConnection
{
    //Création d'une variable en lecture seule
    private readonly CarDbContext _DbContext;
    
    //Constructeur de la classe DbConnection
    public DbConnection(CarDbContext dbContext)
    {
        _DbContext = dbContext;
    }
    
    public void SaveFullClasse(Car myCar)
    {
        _DbContext.Add(myCar);
        
        _DbContext.SaveChanges();
    }

}