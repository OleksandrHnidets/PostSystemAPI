using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.DAL.Models
{
    public class TransactionHistory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid? DeliveryId { get; set; }
        public Delivery Delivery { get; set; }

    }
}
