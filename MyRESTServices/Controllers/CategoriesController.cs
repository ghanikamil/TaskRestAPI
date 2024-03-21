using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using MyRESTServices.BLL.DTOs;
using MyRESTServices.BLL.Interfaces;

namespace MyRESTServices.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryBLL _categoryBLL;
        private readonly IValidator<CategoryCreateDTO> _validatorCategoryCreate;
        public CategoriesController(ICategoryBLL categoryBLL, IValidator<CategoryCreateDTO> validatorCategoryCreate)
        {
            _categoryBLL = categoryBLL;
            _validatorCategoryCreate = validatorCategoryCreate;
        }

        [HttpGet]
        public async Task<IEnumerable<CategoryDTO>> Get()
        {
            var results = await _categoryBLL.GetAll();
            return results;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _categoryBLL.GetById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(int id,CategoryCreateDTO categoryCreateDTO)
        {

            try
            {
                var validatorResult = await _validatorCategoryCreate.ValidateAsync(categoryCreateDTO);
                if (!validatorResult.IsValid)
                {
                    var eror = validatorResult.Errors.Select(e => e.ErrorMessage).ToList();
                    return BadRequest(eror);
                }
                var result = await _categoryBLL.Insert(categoryCreateDTO);
                return CreatedAtAction("Get",new { id = id }, result);
                //return Ok("Insert data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CategoryUpdateDTO categoryUpdateDTO)
        {

            try
            {
                await _categoryBLL.Update(id,categoryUpdateDTO);
                return Ok("Update data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var result = await _categoryBLL.Delete(id);
                if (!result)
                {
                    return NotFound();
                }
                return Ok("delete data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
