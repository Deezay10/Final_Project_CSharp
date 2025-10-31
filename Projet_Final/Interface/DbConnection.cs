namespace Projet_Final.Interface;
using Npgsql;


//code du formateur à adapter à nos besoins

//déclare une classe DbConnection accessible dans d'autres parties du code
public class DbConnection
{
    //création d'une variable en lecture seule
    private readonly SchoolDbContext _schoolDbContext;
    
    // constructeur de la classe DbConnection
    public DbConnection(SchoolDbContext schoolDbContext)
    {
        _schoolDbContext = schoolDbContext;
    }

    //ajoute l'objet maClasse dans la base de donnée
    public void SaveFullClasse(Classe maClasse)
    {
        _schoolDbContext.Add(maClasse);
        
        _schoolDbContext.SaveChanges();
    }

}