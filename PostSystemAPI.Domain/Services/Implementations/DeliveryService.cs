using Microsoft.EntityFrameworkCore;
using PostSystemAPI.DAL.Models;
using PostSystemAPI.DAL.Repository;
using PostSystemAPI.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.Services.Implementations
{
    public class DeliveryService// : IDeliveryService
    {
       /* private readonly IDeliveryRepo _repo;
        public DeliveryService(IDeliveryRepo repo)
        {
            _repo = repo;
        }
        public async Task CreateDeliveryAsync(Delivery delivery)
        {
            await _repo.AddAsync(delivery);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteDeliveryAsync(string id)
        {
            var delivery = await GetDeliveryByIdAsync(id);
            _repo.Delete(delivery);
            await _repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<Delivery>> GetAllDeliveries()
        {
            return await _repo.GetAllAsync(include: source =>source
            .Include(p => p.PostOffice)
                .ThenInclude(c => c.City)
            .Include(s => s.Sender)
            .Include(r => r.Receiver));
        }

        public async Task<Delivery> GetDeliveryByIdAsync(string id)
        {
            var delivery = await _repo.GetFirstAsync(predicate: d => d.Id == Guid.Parse(id));
            return delivery;
        }

        public async Task UpdateDeliveryAsync(string id, Delivery delivery)
        {
            delivery.Id = Guid.Parse(id);
            _repo.Update(delivery);
            await _repo.SaveChangesAsync();
        }
       */
    }
}
