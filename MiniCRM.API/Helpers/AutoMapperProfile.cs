using AutoMapper;

namespace MiniCRM.API.Helpers
{
    public class AutoMapperProfile : Profile
    {
        private readonly IMapper _mapper;

        public AutoMapperProfile()
        {
            //CreateMap<source,distination>()
        }
    }
}