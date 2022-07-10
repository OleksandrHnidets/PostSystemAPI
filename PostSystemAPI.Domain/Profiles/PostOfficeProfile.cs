using AutoMapper;
using PostSystemAPI.DAL.Models;
using PostSystemAPI.Domain.DTO.ReadDTO;
using PostSystemAPI.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.Profiles
{
    public  class PostOfficeProfile: Profile
    {
        public PostOfficeProfile()
        {
            CreateMap<PostOffice, PostOfficeView>().ReverseMap();
            CreateMap<PostOffice, PostOfficeDTO>().ReverseMap();
            CreateMap<PostOfficeView, PostOfficeDTO>().ReverseMap();
            CreateMap<PostOffice, PostOfficeView>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId.ToString()));

            CreateMap<PostOffice, PostOfficeReadView>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForPath(dest => dest.City.Id, opt => opt.MapFrom(src => src.City.Id.ToString()))
                .ForPath(dest => dest.City.Name, opt => opt.MapFrom(src => src.City.Name));
        }
    }
}
