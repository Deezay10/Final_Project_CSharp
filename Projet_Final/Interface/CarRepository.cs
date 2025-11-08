using Projet_Final.Interface.InterfaceRepository;
using Projet_Final.InterfaceRepository;
using Projet_Final.Model;

namespace Projet_Final.Interface;

public class CarRepository : ICarRepository
{
    private readonly CarDbContext _dbContext;
    
    public CarRepository(CarDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //fonction getcar pour obtenir la liste des voitures
    public List<Car> GetCar()
    {
        return _dbContext.Cars.ToList();
    }
}