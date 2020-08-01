using Cards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Services
{
    public interface ICardsService
    {
        Task<IEnumerable<Card>> GetAsync();
        Task<Card> GetAsync(int id);
        Task<Card> AddAsync(Card card);
        Task<Card> DeleteAsync(int id);
        Task<Card> UpdateAsync(Card card);
    }
}
