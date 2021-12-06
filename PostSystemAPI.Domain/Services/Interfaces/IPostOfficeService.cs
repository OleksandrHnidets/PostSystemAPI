using PostSystemAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.Services.Interfaces
{
    public interface IPostOfficeService
    {
        Task<IEnumerable<PostOffice>> GetAllPostOffices();
        Task<PostOffice> GetPostOfficeById(int id);
        Task CreatePostOfficeAsync(PostOffice postOffice);
        Task DeletePostOfficeAsync(int id);
    }
}
