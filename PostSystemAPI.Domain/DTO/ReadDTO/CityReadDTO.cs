﻿using PostSystemAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.DTO.ReadDTO
{
    public class CityReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PostOffice> PostOffices { get; set; }
    }
}
