using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using WebShop.Data;
using WebShop.Data.Entites;
//using WebShop.Data.Entities;
//using WebShop.Helpers;
using WebShop.Models;
using WebShop.Models.Order;

namespace WebShop.Controllers
{


    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly AppEFContext _appEFContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public OrderController(AppEFContext appEFContext, IMapper mapper, IConfiguration configuration)
        {
            _appEFContext = appEFContext;
            _mapper = mapper;
            _configuration = configuration;
        }
        [HttpGet("list_order")]
        public async Task<IActionResult> List()
        {
            var result = await _appEFContext.Orders
                .Select(x => _mapper.Map<OrderViewModel>(x))
                .ToListAsync();
            return Ok(result);
        }
        [HttpPost("add_order")]

        public async Task<ActionResult> UploadOrder([FromForm] OrderCreateViewModel model)
        {
           
            var order = _mapper.Map<OrderEntity>(model);
           
            //order.id = model.id;
            order.phonenumber = model.number;
            //order.product = model.product;
            order.dateofarrivale = model.dateofarrivale;
            order.orderdate = model.orderdate;
            order.count = model.count;
            order.name = model.name;
            order.dateofarrivale = model.dateofarrivale.Value.ToUniversalTime();
            order.orderdate = model.orderdate.Value.ToUniversalTime();
            var entity = new OrderEntity();
            _appEFContext.Orders.Add(order);
            //await _appEFContext.AddAsync(order);
            await _appEFContext.SaveChangesAsync();
           return Ok(_mapper.Map<OrderEntity>(order));
  
        }
    }
}
