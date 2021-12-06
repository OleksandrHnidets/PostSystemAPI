using PostSystemAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.DTO.ReadDTO
{
    public class CityDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<PostOfficeDTO> PostOffices { get; set; }
    }
}
