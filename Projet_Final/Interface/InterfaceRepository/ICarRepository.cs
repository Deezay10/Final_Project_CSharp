using Projet_Final.Model;

namespace Projet_Final.Interface;

public abstract class ICarRepository
{
    public abstract List<Car> GetCar();
}