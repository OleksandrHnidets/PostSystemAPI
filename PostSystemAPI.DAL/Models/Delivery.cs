using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.Models
{
    public class Delivery
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string DeliveryName { get; set; }
        public string DeliveryDescription { get; set; } = "No description";
        [Required]
        public DateTime DeliveryDate { get; set; }
        public Sender Sender { get; set; }
        public Receiver Receiver { get; set; }
        public PostOffice PostOffice { get; set; }

    }
}
