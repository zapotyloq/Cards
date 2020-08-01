using Microsoft.AspNetCore.Mvc;
using Cards.Models;
using Server.Services;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CardsController
    {
        private readonly ICardsService service;

        public CardsController(ICardsService service)
            => this.service = service;

        [HttpGet("get")]
        public async Task<IEnumerable<Card>> Get()
        {
            return await service.GetAsync();
        }
        [HttpGet("get/{id}")]
        public async Task<Card> Get(int id)
        {
            return await service.GetAsync(id);
        }
        [HttpDelete("delete/{id}")]
        public async Task<Card> Delete(int id)
        {
            return await service.DeleteAsync(id);
        }

        [HttpPost("add")]
        public async Task<Card> Add(Card card)
        {
            return await service.AddAsync(card);
        }

        [HttpPut("update")]
        public async Task<Card> Update(Card card)
        {
            return await service.UpdateAsync(card);
        }
    }
}
