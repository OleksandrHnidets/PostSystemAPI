using AutoMapper;
using PostSystemAPI.DAL.Models;
using PostSystemAPI.Domain.DTO.ReadDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.Profiles
{
    public class CitiesProfile: Profile
    {
        public CitiesProfile()
        {
            CreateMap<City, CityReadDTO>();
        }
    }
}
