using PostSystemAPI.DAL.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.ViewModels
{
    public class DeliveryView
    {
        public string DeliveryName { get; set; }
        public string DeliveryDescription { get; set; }
        public int Price { get; set; }
        public DeliveryType DeliveryType { get; set; }
        public string PostOfficeId { get; set; }
        public string ReceiverId { get; set; }
        public string SenderId { get; set; }
    }

    public class AvaliableDriverDeliveries
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }
}
