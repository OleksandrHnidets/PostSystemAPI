using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.ViewModels
{
    public class PostOfficeView
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CityView City { get; set; }
        public List<DeliveryView> Deliveries { get; set; }
    }
}
