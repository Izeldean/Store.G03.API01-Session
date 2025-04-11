using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Presentation
{
    // Api Controller
    [ApiController]
    [Route("api/[controller]")]
   public  class ProductsController(IServiceManger serviceManger):ControllerBase
    {
        //endpoint- public none-static methods 
        [HttpGet] //endpoint : Get: /api/products
        public async Task<IActionResult> GetAllProducts() {
           var result= await serviceManger.ProductService.GetAllProductAsync();
            if (result is null) { return BadRequest(); } //400
            return Ok(result); //200
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id) { 
      var result= await  serviceManger.ProductService.GetProductByIdAsync(id);
        if(result is null) { return BadRequest(); };
            return Ok(result);
        }
    }
}
