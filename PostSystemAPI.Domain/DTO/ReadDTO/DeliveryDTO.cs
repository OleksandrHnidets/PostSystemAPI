using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.DTO.ReadDTO
{
    public class DeliveryDTO
    {
        public int Id { get; set; }
        public string DeliveryName { get; set; }
        public string DeliveryDescription { get; set; } = "No description";
        public DateTime DeliveryDate { get; set; }
        public SenderDTO Sender { get; set; }
        public ReceiverDTO Receiver { get; set; }
        public PostOfficeDTO PostOffice { get; set; }
    }
}
