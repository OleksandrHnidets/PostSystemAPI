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
    public class CityService// : ICityService
    {
        private readonly ICityRepo _repo;
        public CityService(ICityRepo repo)
        {
            _repo = repo;
        }

        public async Task CreateCityAsync(City city)
        {
            await _repo.AddAsync(city);
            await _repo.SaveChangesAsync();
        }

       /* public async Task DeleteCityAsync(string id)
        {
            var city = await GetCityById(id);
            _repo.Delete(city);
            await _repo.SaveChangesAsync();
        }
       */

        public async Task<IEnumerable<City>> GetAllCitiesAsync()
        {
            return await _repo.GetAllAsync(include: source => source
                .Include(x => x.PostOffices));
        }

        /*public async Task<City> GetCityById(string id)
        {
            var city = await _repo.GetFirstAsync(predicate: c => c.Id == Guid.Parse(id));
            return city;
        } */  

       /* public async Task UpdateCityAsync(string id, City city)
        {
            city.Id = Guid.Parse(id);
            _repo.Update(city);
            await _repo.SaveChangesAsync();
        }
       */
    }
}
