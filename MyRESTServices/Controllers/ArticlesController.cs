using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRESTServices.BLL.DTOs;
using MyRESTServices.BLL.Interfaces;

namespace MyRESTServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleBLL _articleBLL;
        public ArticlesController(IArticleBLL articleBLL)
        {
            _articleBLL = articleBLL;
        }
        [HttpGet]
        public async Task<IEnumerable<ArticleDTO>> Get()
        {
            var results = await _articleBLL.GetAll();
            return results;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _articleBLL.GetArticleById(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        [HttpGet("withcategories")]
        public async Task<IEnumerable<ArticleDTO>> GetWithCategories()
        {
            var articles = await _articleBLL.GetArticleWithCategory();
            return articles;
        }
        [HttpGet("withcategories/{id}")]
        public async Task<IEnumerable<ArticleDTO>> GetbyCategories(int id)
        {
            var articles = await _articleBLL.GetArticleByCategory(id);
            return articles;
        }
        [HttpPost]
        public async Task<IActionResult> Post(ArticleCreateDTO articleCreateDTO)
        {

            try
            {
                var result = await _articleBLL.Insert(articleCreateDTO);
                return Ok("Insert data success");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ArticleUpdateDTO articleUpdateDTO)
        {

            try
            {
                await _articleBLL.Update(id, articleUpdateDTO);
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
                var result = await _articleBLL.Delete(id);
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
