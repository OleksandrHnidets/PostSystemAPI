using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Balance { get; set; }

        public virtual ICollection<Position> Positions { get; set; }
        public virtual ICollection<Delivery> SendedDeliveries { get; set; }
        public virtual ICollection<Delivery> ReceivedDeliveries { get; set; }
        public virtual ICollection<Delivery> AssignedDeliveries { get; set; }

        public User ()
        {
            AssignedDeliveries = new HashSet<Delivery>();
            SendedDeliveries = new HashSet<Delivery> ();
            ReceivedDeliveries = new HashSet<Delivery> ();
        }
    }
}
