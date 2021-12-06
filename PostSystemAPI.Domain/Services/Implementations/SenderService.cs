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
    public class SenderService : ISenderService
    {
        private readonly ISenderRepo _repo;
        public SenderService(ISenderRepo repo)
        {
            _repo = repo;
        }
        public async Task CreateSenderAsync(Sender sender)
        {
            await _repo.AddAsync(sender);
            await _repo.SaveChangesAsync();
        }

        public async Task DeleteSenderAsync(int id)
        {
            var Sender = await GetSenderByIdAsync(id);
            _repo.Delete(Sender);
            await _repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<Sender>> GetAllSendersAsync()
        {
            return await _repo.GetAllAsync(include: source => source
            .Include(d => d.Deliveries));
        }

        public async Task<Sender> GetSenderByIdAsync(int id)
        {
            var sender = await _repo.GetFirstAsync(x=> x.Id == id);
            return sender;
        }

        public async Task UpdateSenderAsync(int id, Sender sender)
        {
            sender.Id = id;
            _repo.Update(sender);
            await _repo.SaveChangesAsync();
        }
    }
}
