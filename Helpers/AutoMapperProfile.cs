using AutoMapper;
using practices.Data;
using practices.Models;

namespace practices.Helpers {

    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Blog, BlogModel>().ReverseMap(); //reverse so the both direction
        }
    }
}
