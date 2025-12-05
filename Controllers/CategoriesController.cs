using Microsoft.AspNetCore.Mvc;
using TestApi.DTOs.Category;
using TestApi.Services.Interfaces;


namespace TestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {


            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page = 1, int take = 3)
        {
            //var categories = await _context.Categories.Skip((page - 1) * take).Take(take).ToListAsync();
            //var categories = await _repository.GetAll(includes: "Products").ToListAsync();
            //int skipValues = (page - 1) * take;
            //var categories = await _repository.GetAll(c => c.Name.Contains("test"), c => c.Name, skipValues, take, true, true, "Products").ToListAsync();

            //return Ok(categories);
            return Ok(await _service.GetAllAsync(page, take));

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id < 1) return BadRequest();
            var categoryDTO = await _service.GetByIdAsync(id);
            if (categoryDTO is null) return NotFound();
            return (StatusCode(StatusCodes.Status200OK, categoryDTO));

        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateCategoryDTO categorydto)
        {
            if (await _service.CreateAsync(categorydto)) return BadRequest();


            return (StatusCode(StatusCodes.Status201Created));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id < 1) return BadRequest();
            await _service.DeleteAsync(id);
            return NoContent();
        }
        [HttpPut]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateCategoryDTO categoryDTO)
        {
            if (id < 1) return BadRequest();
            await _service.UpdateAsync(id, categoryDTO);

            return NoContent();
        }
    }
}
