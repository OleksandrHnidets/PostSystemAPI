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
    public class DeliverProfile: Profile
    {
        public DeliverProfile()
        {
            CreateMap<DeliveryView, Delivery>()
                .ForMember(dest => dest.ReceivedBy, opt => opt.MapFrom(src => Guid.Parse(src.ReceiverId)))
                .ForMember(dest => dest.SendedBy, opt => opt.MapFrom(src => Guid.Parse(src.SenderId)))
                .ForMember(dest => dest.PostOfficeId, opt => opt.MapFrom(src => Guid.Parse(src.PostOfficeId)));
            CreateMap<Delivery, ReadDeliveryView>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(opt => opt.Id.ToString()))
                .ForMember(dest => dest.PostOfficeId, opt => opt.MapFrom(opt => opt.PostOfficeId.ToString()));
            CreateMap<Delivery, DeliveryDTO>().ReverseMap();
            CreateMap<DeliveryView, DeliveryDTO>().ReverseMap();
        }
    }
}
