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
    public class UserInfoProfile: Profile
    {
        public UserInfoProfile()
        {
            CreateMap<User, UserInfoViewModel>().ReverseMap();
            CreateMap<User, ReceiverViewModel>().ReverseMap();
        }
    }
}
