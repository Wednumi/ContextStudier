using AutoMapper;
using ContextStudier.Api.Models.AccountModels;
using ContextStudier.Core.Entitites;

namespace ContextStudier.Api.MapperProfiles
{
    public class ModelsProfile : Profile
    {
        public ModelsProfile()
        {
            CreateMap<UserRegistrationModel, User>();
        }
    }
}
