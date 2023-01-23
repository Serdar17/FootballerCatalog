namespace Web.Models.Repository.Interfaces;

public interface IPlayerManager
{
    public Task<BaseResponse> CreateAsync(Player player);

    public List<Player> GetPlayers();

    public Player GetPlayerById(int? id);

    public bool UpdatePlayer(int? id, Player player);

    public bool DeletePlayer(int? id);
}