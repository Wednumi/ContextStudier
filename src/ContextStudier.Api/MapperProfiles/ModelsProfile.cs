using AutoMapper;
using ContextStudier.Api.Models.AccountModels;
using ContextStudier.Core.Entitites;
using ContextStudier.Presentation.Core.EntitiesModels;

namespace ContextStudier.Api.MapperProfiles
{
    public class ModelsProfile : Profile
    {
        public ModelsProfile()
        {
            CreateMap<UserRegistrationModel, User>();
            CreateMap<FolderModel, Folder>();
            CreateMap<Folder, FolderInfo>();
            CreateMap<Folder, FolderModel>()
                .ForMember(dest => dest.CardsCount, opt => opt.MapFrom(src => src.Cards.Count));
        }
    }
}
