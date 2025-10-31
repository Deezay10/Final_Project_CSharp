using Projet_Final.Model;

namespace Projet_Final.Interface;
using Microsoft.EntityFrameworkCore;

public class CarRepository : ICarRepository
{
    private readonly CarDbContext _DbContext;
    
    public CarRepository(CarDbContext dbContext)
    {
        _DbContext = dbContext;
    }

    //fonction getcar pour obtenir la liste des voitures
    public override List<Car> GetCar()
    {
        return _DbContext.Cars.ToList();
    }
}