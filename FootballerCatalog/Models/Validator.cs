using Web.Models.Repository.Interfaces;

namespace Web.Models;

public class Validator : IValidator
{
    private readonly IPlayerRepository _repository;

    public Validator(IPlayerRepository repository)
    {
        _repository = repository;
    }
    
    /// <summary>
    /// Валидация игрока по название команды, то есть в одной команде не могут быть 2 игрока с одинаковыми инициалами
    /// </summary>
    /// <param name="player">Игрок, для которого происходит валидация</param>
    /// <returns>true - если валадицая прошла успешно, false - если найдена команда с таким же игроком</returns>
    public bool IsValidByTeamName(Player player)
    {
        var count = _repository
            .GetAll()
            .Count(item => item.TeamName == player.TeamName && item.Equals(player));
        return !(count > 0);
    }
}