using AutoMapper;
using Web.Dto;
using Web.Models;

namespace Web.MappingConfiguration;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<Player, PlayerDto>();
        CreateMap<PlayerDto, Player>();
    }
}