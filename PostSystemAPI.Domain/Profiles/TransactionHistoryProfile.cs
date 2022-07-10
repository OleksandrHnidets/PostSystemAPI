using AutoMapper;
using PostSystemAPI.DAL.Models;
using PostSystemAPI.Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.Profiles
{
    public class TransactionHistoryProfile: Profile
    {
        public TransactionHistoryProfile()
        {
            CreateMap<TransactionHistory, TransactionHistoryViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.DeliveryId, opt => opt.MapFrom(src => src.DeliveryId.ToString()))
                .ForPath(dest => dest.Delivery.ReceiverId, opt => opt.MapFrom(src => src.Delivery.ReceivedBy))
                .ForPath(dest => dest.Delivery.SenderId, opt => opt.MapFrom(src => src.Delivery.SendedBy))
                .ForPath(dest => dest.Delivery.DeliveryName, opt => opt.MapFrom(src => src.Delivery.DeliveryName))
                .ForPath(dest => dest.Delivery.DeliveryDescription, opt => opt.MapFrom(src => src.Delivery.DeliveryDescription))
                .ForPath(dest => dest.Delivery.PostOfficeId, opt => opt.MapFrom(src => src.Delivery.PostOfficeId.ToString()))
                .ForPath(dest => dest.Delivery.DeliveryType, opt => opt.MapFrom(src => src.Delivery.DeliveryType))
                .ForPath(dest => dest.Delivery.Price, opt => opt.MapFrom(src => src.Delivery.Price));
        }
    }
}
