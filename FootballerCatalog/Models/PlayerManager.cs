using Web.Models.Repository.Interfaces;

namespace Web.Models;

public class PlayerManager : IPlayerManager
{
    private readonly IPlayerRepository _repository;
    private readonly IValidator _validator;
    
    public PlayerManager(IPlayerRepository repository, IValidator validator)
    {
        _repository = repository;
        _validator = validator;
    }
    
    public async Task<BaseResponse> CreateAsync(Player player)
    {
        if (!_validator.IsValidByTeamName(player))
        {
            return new BaseResponse
            {
                StatusCode = 400,
                Description = $"В команде {player.TeamName} уже существует игрок с таким именем и фамилией"
            };
        }

        var playerFromdb = _repository.Create(player);
        return new BaseResponse
        {
            Description = "Игрок добавлен успешно",
            Player = playerFromdb
        };
    }

    public List<Player> GetPlayers()
    {
        return _repository.GetAll().ToList();
    }

    public Player GetPlayerById(int? id)
    {
        return _repository.GetAll().Where(item => item.Id == id).FirstOrDefault();
    }

    public bool UpdatePlayer(int? id, Player player)
    {
        var item = GetPlayerById(id);
        if (item is not null)
        {
            player.Id = item.Id;
            _repository.Update(player);
            return true;
        }

        return false;
    }

    public bool DeletePlayer(int? id)
    {
        var item = GetPlayerById(id);
        if (item is not null)
        {
            _repository.Delete(item);
            return true;
        }

        return false;
    }
}