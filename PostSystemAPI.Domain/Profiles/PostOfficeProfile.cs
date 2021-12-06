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
        }
    }
}
