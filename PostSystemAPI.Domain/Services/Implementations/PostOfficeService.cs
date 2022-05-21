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
    public class PostOfficeService// : IPostOfficeService
    {
        /*private readonly IPostOfficeRepo _repo;

        public PostOfficeService(IPostOfficeRepo repo)
        {
            _repo = repo;
        }
        public async Task CreatePostOfficeAsync(PostOffice postOffice)
        {
            await _repo.AddAsync(postOffice);
            await _repo.SaveChangesAsync();
        }

        public async Task DeletePostOfficeAsync(string id)
        {
            var postOffice = await GetPostOfficeById(id);
            _repo.Delete(postOffice);
            await _repo.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostOffice>> GetAllPostOffices()
        {
            return await _repo.GetAllAsync(include: source => source
            .Include(c=>c.City));
        }

        public async Task<PostOffice> GetPostOfficeById(string id)
        {
           var postOffice = await _repo.GetFirstAsync(x => Guid.Parse(id) == x.Id);
            return postOffice; 
        }
        */
    }
}
