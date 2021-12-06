using PostSystemAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.Services.Interfaces
{
    public interface ISenderService
    {
        Task<IEnumerable<Sender>> GetAllSendersAsync();
        Task<Sender> GetSenderByIdAsync(int id);
        Task CreateSenderAsync(Sender sender);
        Task DeleteSenderAsync(int id);
        Task UpdateSenderAsync(int id,Sender sender);
    }
}
