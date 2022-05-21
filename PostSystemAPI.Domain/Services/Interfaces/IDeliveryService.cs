using PostSystemAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.Services.Interfaces
{
    public interface IDeliveryService
    {
        Task<IEnumerable<Delivery>> GetAllDeliveries();
        Task<Delivery> GetDeliveryByIdAsync(string id);
        Task CreateDeliveryAsync(Delivery delivery);
        Task DeleteDeliveryAsync(string id);
        Task UpdateDeliveryAsync(string id, Delivery delivery);
    }
}
