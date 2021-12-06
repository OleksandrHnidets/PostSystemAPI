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
    public class SenderProfile: Profile
    {
        public SenderProfile()
        {
            CreateMap<Sender, SenderView>().ReverseMap();
            CreateMap<Sender, SenderDTO>().ReverseMap();
            CreateMap<SenderView, SenderDTO>().ReverseMap();
        }
    }
}
