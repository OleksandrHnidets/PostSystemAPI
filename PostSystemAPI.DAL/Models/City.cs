﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.Models
{
    public class City
    {
        public int Id { get; set; }
        [Required] 
        [MaxLength(50)]
        public string Name { get; set; }
        public List<PostOffice> PostOffices { get; set; }
    }
}
