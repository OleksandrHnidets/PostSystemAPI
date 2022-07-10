using PostSystemAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.DTO.ReadDTO
{
    public class PostOfficeDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public CityDTO City { get; set; }
        public List<DeliveryDTO> Deliveries { get; set; }
    }
}
