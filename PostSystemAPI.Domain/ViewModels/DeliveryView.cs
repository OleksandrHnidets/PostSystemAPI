using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.ViewModels
{
    public class DeliveryView
    {
        public int Id { get; set; }
        public string DeliveryName { get; set; }
        public string DeliveryDescription { get; set; } = "No description";
        public DateTime DeliveryDate { get; set; }
        public SenderView Sender { get; set; }
        public ReceiverView Receiver { get; set; }
        public PostOfficeView PostOffice { get; set; }
    }
}
