using PostSystemAPI.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.ViewModels
{
    public class ReadDeliveryView
    {
        public string Id { get; set; }
        public string DeliveryName { get; set; }
        public string DeliveryDescription { get;set; }
        public string DeliveryDate { get; set; }
        public int Price { get; set; }
        public DeliveryStatus DeliveryStatus { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public string SendedBy { get; set; }
        public string ReceivedBy { get; set; }
        public string PostOfficeId { get; set; }
    }
}
