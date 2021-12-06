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
    public class ReceiverService : IReceiverService
    {
        private readonly IReceiverRepo _repo;
        public ReceiverService(IReceiverRepo repo)
        {
            _repo = repo;
        }
        public async Task CreateReceiverAsync(Receiver receiver)
        {
            await _repo.AddAsync(receiver);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteReceiverAsync(int id)
        {
            var receiver = await GetReceiverByIdAsync(id);
            _repo.Delete(receiver);
            await _repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<Receiver>> GetAllReceiversAsync()
        {
            return await _repo.GetAllAsync(include: source => source
            .Include(d => d.Deliveries));
        }

        public async Task<Receiver> GetReceiverByIdAsync(int id)
        {
            var receiver = await _repo.GetFirstAsync(x => x.Id == id);
            return receiver;
        }

        public async Task UpdateReceiverAsync(int id, Receiver receiver)
        {
            receiver.Id = id;
            _repo.Update(receiver);
            await _repo.SaveChangesAsync();
        }
    }
}
