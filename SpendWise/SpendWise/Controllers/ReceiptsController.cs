using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SpendWise.Business.Exceptions;
using SpendWise.Business.Interfaces;
using SpendWise_DataAccess.Dtos;
using SpendWise_DataAccess.Entities;

namespace SpendWise.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceiptsController : ControllerBase
    {
        private IReceiptsService _receiptsService;

        public ReceiptsController(IReceiptsService receiptsService)
        {
            _receiptsService = receiptsService;
        }

        [HttpPost("ScanReceipt")]
        public async Task<ActionResult<string>> ScanReceipt([FromForm] string categories, [FromForm] IFormFile image)
        {
            List<Category> categoriesList = JsonConvert.DeserializeObject<List<Category>>(categories) ?? new List<Category>();

            if(categoriesList.IsNullOrEmpty() || image.Length<=0)
            {
                return BadRequest("Categories list is empty");
            }

            var categorisedProductsDtos = await _receiptsService.ScanReceipt(categoriesList, image);

            return Ok(JsonConvert.SerializeObject(categorisedProductsDtos));
           
        }

        [HttpPost("SaveCart")]
        public async Task<ActionResult> SaveCart([FromBody] CartCreateDto cart)
        {
            try
            {
                if (cart.CategoryProducts.IsNullOrEmpty())
                {
                    return BadRequest("Cart is empty");
                }

                var createdCart = await _receiptsService.SaveCart(cart);

                return Ok();
            }
            catch(NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
