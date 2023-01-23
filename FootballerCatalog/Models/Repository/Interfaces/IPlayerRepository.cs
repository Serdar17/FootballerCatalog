namespace Web.Models.Repository.Interfaces;

public interface IPlayerRepository
{
    public IEnumerable<Player> GetAll();

    public Player Create(Player player);

    public void Update(Player player);

    public void Delete(Player player);
}