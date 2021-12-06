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
    public class ReceiverProfile: Profile
    {
        public ReceiverProfile()
        {
            CreateMap<Receiver, ReceiverView>().ReverseMap();
            CreateMap<Receiver, ReceiverDTO>().ReverseMap();
            CreateMap<ReceiverView, ReceiverDTO>().ReverseMap();
        }
    }
}
