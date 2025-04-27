using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    [ApiController]
    [Route("api/[controller]")]
    public class BasketController(IServiceManger serviceManger):ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBasketById(string id) {

            var result=serviceManger.BasketService.GetBasketAsync(id);
            return Ok(result);
        }
        [HttpPost] //Post: /api/basket
        public async Task<IActionResult> UpdateBasket(BasketDto basketDto) { 

            var result = await serviceManger.BasketService.UpdateBasketAsync(basketDto);
            return Ok(result);  
        }
        [HttpDelete] // Delete : /api/basket?id
        public async Task<IActionResult> DeleteBasket(string id) { 
       await serviceManger.BasketService.DeleteBasketAsync(id);
            return NoContent();
        }
       
    }
}
