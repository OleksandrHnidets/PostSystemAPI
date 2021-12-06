using PostSystemAPI.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostSystemAPI.Domain.Services.Interfaces
{
    public interface IReceiverService
    {
        Task<IEnumerable<Receiver>> GetAllReceiversAsync();
        Task<Receiver> GetReceiverByIdAsync(int id);
        Task CreateReceiverAsync(Receiver receiver);
        Task UpdateReceiverAsync(int id,Receiver receiver);
        Task DeleteReceiverAsync(int id);

    }
}
