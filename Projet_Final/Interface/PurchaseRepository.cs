using Projet_Final.Interface.InterfaceRepository;
using Projet_Final.Model;
using Microsoft.EntityFrameworkCore;

namespace Projet_Final.Interface;
public class PurchaseRepository : IPurchaseRepository
{
    private readonly CarDbContext _dbContext;

    public PurchaseRepository(CarDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    //Fonction getpurchase pour obtenir la liste des achats du plus récent au plus ancien
    public List<Purchase> GetPurchases()
    {
        return _dbContext.Purchases
            .Include(p => p.Car)
            .Include(p => p.Client)
            .OrderByDescending(p => p.purchase_date)
            .ToList();
    }
}