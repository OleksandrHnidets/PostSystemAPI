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
        Task<Delivery> GetDeliveryByIdAsync(int id);
        Task CreateDeliveryAsync(Delivery delivery);
        Task DeleteDeliveryAsync(int id);
        Task UpdateDeliveryAsync(int id, Delivery delivery);
    }
}
