using Projet_Final.Model;

namespace Projet_Final.Interface;
using Npgsql;


//code du formateur à adapter à nos besoins

//déclare une classe DbConnection accessible dans d'autres parties du code
public class DbConnection
{
    //création d'une variable en lecture seule
    private readonly CarDbContext _DbContext;
    
    // constructeur de la classe DbConnection
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