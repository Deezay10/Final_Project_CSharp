using Projet_Final.Model;
using System.Collections.Generic;
namespace Projet_Final.Interface.InterfaceRepository;

public interface ICarRepository
{
    List<Car> GetCar();
}