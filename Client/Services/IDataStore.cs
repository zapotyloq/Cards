using Cards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Services
{
    interface IDataStore<T>
    {
        Task<IEnumerable<T>> GetAsync();
        Task<T> GetAsync(int id);
        Task<T> AddAsync(Card card);
        Task<T> DeleteAsync(int id);
        Task<T> UpdateAsync(Card card);
                
    }
}
