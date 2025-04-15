using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controller
{

     [Route("api/[controller]")]
    [ApiController] 
    public class StockController:ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IStockRepository _stockRepository;
        public StockController(ApplicationDbContext context, IStockRepository stockRepository)
        {
            _context = context;
            _stockRepository = stockRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks= await _stockRepository.GettAllAsync();
            var stockDto=stocks.Select(s=>s.ToStockDto());
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStockById([FromRoute] int id)
        {
            var stock= await _context.Stocks.FindAsync(id);
            if(stock is null)
            {
                return NotFound();
            }
            return Ok(stock.ToStockDto());

        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateStockRequestDto stockDto)
        {
            var stockModel=stockDto.ToStockFromCreateDto();
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStockById),
                new{id=stockModel.Id},stockModel.ToStockDto());
        }
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id,
        [FromBody] UpdateStockRequestDto updateDto)
        {
            var stockModel=await _context.Stocks.FirstOrDefaultAsync(x=>x.Id==id);
            if(stockModel==null)
            {
                return NotFound();
            }
            stockModel.Symbol=updateDto.Symbol;
            stockModel.CompanyName=updateDto.CompanyName;
            stockModel.Purchase=updateDto.Purchase;
            stockModel.LastDiv=updateDto.LastDiv;
            stockModel.Industry=updateDto.Industry;
            stockModel.MarketCap=updateDto.MarketCap;

            await _context.SaveChangesAsync(); // SENDING TO DATABASE
            return Ok(stockModel.ToStockDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var stockModel=await _context.Stocks.FirstOrDefaultAsync(x=>x.Id==id);
            if(stockModel==null)
            {
                return NotFound();
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return NoContent(); // For delete method
        }

    }
}