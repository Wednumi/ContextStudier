using AutoMapper;
using ContextStudier.Presentation.Core.EntitiesModels;

namespace ContextStudier.Api.MapperProfiles
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<FolderModel, FolderModel>();
        }
    }
}
