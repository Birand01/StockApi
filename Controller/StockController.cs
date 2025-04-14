using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using Microsoft.AspNetCore.Mvc;

namespace api.Controller
{

     [Route("api/[controller]")]
    [ApiController] 
    public class StockController:ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public StockController(ApplicationDbContext context)
        {
            _context=context;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var stocks=_context.Stocks.ToList();
            return Ok(stocks);
        }

        [HttpGet("{id}")]
        public IActionResult GetStockById([FromRoute] int id)
        {
            var stock=_context.Stocks.Find(id);
            if(stock is null)
            {
                return NotFound();
            }
            return Ok(stock);

        }
    }
}