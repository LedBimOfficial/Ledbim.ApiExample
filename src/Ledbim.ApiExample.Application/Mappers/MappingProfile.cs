using AutoMapper;
using Ledbim.ApiExample.Application.Commands.Users;
using Ledbim.ApiExample.Domain.Entities;
using Ledbim.ApiExample.Models.Users.ResponseModels;

namespace Ledbim.ApiExample.Application.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponse>().ReverseMap();

            CreateMap<User, UserCreateRequest>().ReverseMap();

            CreateMap<User, UserUpdateRequest>().ReverseMap();
        }
    }
}