using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.Models
{
    public class PostOffice
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CityId { get; set; }
        public virtual City City { get; set; }
        public virtual ICollection<Delivery> Deliveries { get; set; }

        public PostOffice()
        {
            Deliveries = new HashSet<Delivery>();
        }

    }
}
