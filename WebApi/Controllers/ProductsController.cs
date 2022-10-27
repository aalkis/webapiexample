using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Interfaces;

namespace WebApi.Controllers
{
    //Ok(200) NotFound(404) NoContent(204) Created(201) BadRequest(400)
    [EnableCors]
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result =  await _productRepository.GetAllAsync();     
            return Ok(result);
        }
        [Authorize(Roles ="Member")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var data = await _productRepository.GetAsync(id);
            if(data == null)
            {
            return NotFound(id);
            }
            return Ok(data);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(Product product)
        {
            var addedProduct = await _productRepository.CreateAsync(product);
            return Created(String.Empty, addedProduct);
        }
        [HttpPut]
        public async Task<IActionResult> Update(Product product)
        {
            var checkProduct = await _productRepository.GetAsync(product.Id);
            if(checkProduct == null)
            {
                return NotFound(product.Id);
            }
            await _productRepository.UpdateAsync(product);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Remove(int id)
        {
            var checkProduct = await _productRepository.GetAsync(id);
            if(checkProduct == null)
            {
                return NotFound(id);
            }
            await _productRepository.RemoveAsync(id);
            return NoContent();
        }
        [HttpPost("upload")]
        public async Task<IActionResult> Upload(IFormFile formFile)
        {
            var newName = Guid.NewGuid() + "." + Path.GetExtension(formFile.FileName);
            var path = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot", newName );
            var stream = new FileStream(path, FileMode.Create);
            await formFile.CopyToAsync(stream);
            return Created(string.Empty, formFile);
        }
        [HttpGet("[action]")]
        public IActionResult Test([FromServices] IDummyRepo dummyRepo)
        {
            //request => Response
            //[FromForm] string name, [FromHeader] string auth, 

            //var authentication = HttpContext.Request.Headers["auth"];
            //var name2 = HttpContext.Request.Form["name"];
            return Ok(dummyRepo.GetName());
        }
    }
}
