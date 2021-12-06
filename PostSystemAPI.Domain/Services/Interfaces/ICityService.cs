using PostSystemAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.Services.Interfaces
{
    public interface ICityService
    {
        Task<IEnumerable<City>> GetAllCitiesAsync();
        Task<City> GetCityById(int id);
        Task CreateCityAsync(City city);
        Task UpdateCityAsync(int id, City city);
        Task DeleteCityAsync(int id);
    }
}
