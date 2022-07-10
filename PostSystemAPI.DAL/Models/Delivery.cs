using PostSystemAPI.DAL.Enums;
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
        public Guid Id { get; set; }
        public string DeliveryName { get; set; }
        public string DeliveryDescription { get; set; }
        public DateTime DeliveryDate { get; set; }
        public int Price { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public string SendedBy { get; set; }
        public string ReceivedBy { get; set; }
        public Guid PostOfficeId { get; set; }

        public virtual User SendedUser { get; set; }
        public virtual User ReceivedUser { get; set; }
        public virtual PostOffice PostOffice { get; set; }
        public TransactionHistory TransactionHistory { get; set; }

    }
}
