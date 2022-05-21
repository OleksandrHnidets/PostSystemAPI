using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.Models
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<PostOffice> PostOffices { get; set; }

        public City()
        {
            PostOffices = new HashSet<PostOffice>();
        }
    }
}
