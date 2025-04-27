using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Attributes;
using Services.Abstractions;
using Shared;
using Shared.ErrorModels;
using Store.G03.API.ErrorModels;
using System;
using System.Collections;
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
       
        // Sorting: nameasc [default]
        //Sorting:  namedesc
        // Sorting: priceDesc
        //sorting: priceAsc
        //endpoint- public none-static methods 
        [HttpGet] //endpoint : Get: /api/products
        [ProducesResponseType(StatusCodes.Status200OK, Type= typeof(PaginationResponse<ProductResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [Cache(100)]
        public async Task<ActionResult<PaginationResponse<ProductResultDto>>> GetAllProducts([FromQuery]ProductSpecificationsParameters  productSpecParams) {
           var result= await serviceManger.ProductService.GetAllProductAsync(productSpecParams);
          
            return Ok(result); //200
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductResultDto))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorDetails))]
        public async Task<ActionResult<ProductResultDto>> GetProductById(int id) { 
      var result= await  serviceManger.ProductService.GetProductByIdAsync(id);
        if(result is null) { return BadRequest(); };
            return Ok(result);
        }
        // Get All Brands

        [HttpGet("brands")] // GET: /api/products/brands
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<BrandResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]

        public async Task<ActionResult<BrandResultDto>> GetAllBrands() {
           var result= await serviceManger.ProductService.GetAllBrandsAsync();
            if(result is null) return BadRequest();
            return Ok(result);  
        
        }
        [HttpGet("types")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationResponse<TypeResultDto>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ValidationErrorResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorDetails))]

        public async Task<ActionResult<TypeResultDto>> GetAllTypes() {
            var result = await serviceManger.ProductService.GetAllTypesAsync();
            if (result is null) return BadRequest();
            return Ok(result);




        }

    }
}
