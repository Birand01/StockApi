using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Stock;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly ApplicationDbContext _context;
        public StockRepository(ApplicationDbContext applicationDbContext)
        {
            _context=applicationDbContext;
        }

        public async Task<Stock> CreateAsync(Stock stockModel)
        {
            await _context.Stocks.AddAsync(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> DeleteAsync(int id)
        {
            var stockModel=await _context.Stocks.FirstOrDefaultAsync(x=>x.Id==id);
            if(stockModel==null)
            {
                return null;
            }
            _context.Stocks.Remove(stockModel);
            await _context.SaveChangesAsync();
            return stockModel;
        }

        public async Task<Stock?> GetByIdAsync(int id)
        {
            return await _context.Stocks
            .Include(c=>c.Comments).FirstOrDefaultAsync(i=>i.Id==id);

        }

        public async Task<List<Stock>> GettAllAsync()
        {
            return await _context.Stocks.Include(c=>c.Comments).ToListAsync();
        }

        public async Task<Stock> UpdateAsync(int id, UpdateStockRequestDto updateDto)
        {
             var stockModel=await _context.Stocks.FirstOrDefaultAsync(x=>x.Id==id);
            if(stockModel==null)
            {
                return null;
            }
            stockModel.Symbol=updateDto.Symbol;
            stockModel.CompanyName=updateDto.CompanyName;
            stockModel.Purchase=updateDto.Purchase;
            stockModel.LastDiv=updateDto.LastDiv;
            stockModel.Industry=updateDto.Industry;
            stockModel.MarketCap=updateDto.MarketCap;

            await _context.SaveChangesAsync(); // SENDING TO DATABASE
            return stockModel;
        }
    }
}