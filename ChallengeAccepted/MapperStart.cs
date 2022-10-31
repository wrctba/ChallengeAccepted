using AutoMapper;
using ChallengeAccepted.DTO;
using ChallengeAccepted.Models;

namespace ChallengeAccepted
{
    public static class MapperStart
    {
        public static IMapper StartAutoMapper()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CategoryPostDto, Category>()
                    .ForMember(x => x.Id, opt => opt.Ignore())
                    .ForMember(x => x.Parent, opt => opt.Ignore());
                cfg.CreateMap<CategoryPutDto, Category>()
                    .ForMember(x => x.Id, opt => opt.Ignore())
                    .ForMember(x => x.Parent, opt => opt.Ignore())
                    .ForMember(x => x.Visible, opt => opt.Ignore());
                cfg.CreateMap<CategoryPostDto, CategoryPutDto>();
                cfg.CreateMap<CategoryDto, CategoryTreeDto>()
                    .ForMember(x => x.Children, opt => opt.Ignore());
                cfg.CreateMap<Category, CategoryDto>();
            });

#if DEBUG
            configuration.AssertConfigurationIsValid();
#endif
            return configuration.CreateMapper();
        }
    }
}
