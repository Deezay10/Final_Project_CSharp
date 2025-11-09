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

    public List<Purchase> GetPurchases()
    {
        return _dbContext.Purchases
            .Include(p => p.Car)
            .Include(p => p.Client)
            .OrderBy(p => p.purchase_date) // Tri ascendant ✅
            .ToList();
    }
}