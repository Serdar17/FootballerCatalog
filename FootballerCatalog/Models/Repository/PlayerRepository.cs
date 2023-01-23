using Microsoft.EntityFrameworkCore;
using Web.Context;
using Web.Models.Repository.Interfaces;

namespace Web.Models.Repository;

public class PlayerRepository : IPlayerRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public PlayerRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public IEnumerable<Player> GetAll()
    {
        return _dbContext.Players;
    }

    public Player Create(Player player)
    {
        var entity = _dbContext.AddAsync(player).Result;
        _dbContext.SaveChangesAsync();
        return entity.Entity;
    }

    public void Update(Player player)
    {
        _dbContext.ChangeTracker.Clear();
        _dbContext.Entry(player).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public void Delete(Player player)
    {
        _dbContext.ChangeTracker.Clear();
        _dbContext.Players.Remove(player);
        _dbContext.SaveChanges();
    }
}