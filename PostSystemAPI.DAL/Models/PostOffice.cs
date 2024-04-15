using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.Models
{
    public sealed class PostOffice
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int PostOfficeBalance { get; set; }
        public Guid CityId { get; set; }
        public City City { get; set; }
        public ICollection<Delivery> SentDeliveries { get; set; }
        public ICollection<Delivery> ReceivedDeliveries { get; set; }

        public PostOffice()
        {
            SentDeliveries = new HashSet<Delivery>();
            ReceivedDeliveries = new HashSet<Delivery>();
        }

    }
}
