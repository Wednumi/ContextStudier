using AutoMapper;
using ContextStudier.Core.Entitites;

namespace ContextStudier.Core.MapperProfiles
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<Folder, Folder>();
        }
    }
}
