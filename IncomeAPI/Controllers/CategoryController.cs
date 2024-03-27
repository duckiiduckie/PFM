using IncomeAPI.Models.Dto;
using IncomeAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace IncomeAPI.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private ResponeDto _respone;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _respone = new ResponeDto();
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<ResponeDto>> Get(string userId)
        {
            try
            {
                var categories = await _categoryRepository.GetCategories(userId);
                _respone.Result = categories;
                return Ok(_respone);
            }
            catch (Exception ex)
            {
                _respone.IsSuccess = false;
                _respone.Message = ex.Message;
                return BadRequest(_respone);
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ResponeDto>> Get(int id)
        {
            try
            {
                var category = await _categoryRepository.GetCategory(id);
                _respone.Result = category;
                return Ok(_respone);
            }
            catch (Exception ex)
            {
                _respone.IsSuccess = false;
                _respone.Message = ex.Message;
                return BadRequest(_respone);
            }
        }

        [HttpPost]  
        public async Task<ActionResult<ResponeDto>> Post(string userId, [FromBody] CreateCategoryDto category)
        {
            try
            {
                var newCategory = await _categoryRepository.AddCategory(category);
                _respone.Result = newCategory;
                return Ok(_respone);
            }
            catch (Exception ex)
            {
                _respone.IsSuccess = false;
                _respone.Message = ex.Message;
                return BadRequest(_respone);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<ResponeDto>> Put(int id, [FromBody] CreateCategoryDto category)
        {
            try
            {
                var updatedCategory = await _categoryRepository.UpdateCategory(id, category);
                _respone.Result = updatedCategory;
                return Ok(_respone);
            }
            catch (Exception ex)
            {
                _respone.IsSuccess = false;
                _respone.Message = ex.Message;
                return BadRequest(_respone);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<ResponeDto>> Delete(int id)
        {
            try
            {
                var deletedCategory = await _categoryRepository.DeleteCategory(id);
                _respone.Result = deletedCategory;
                return Ok(_respone);
            }
            catch (Exception ex)
            {
                _respone.IsSuccess = false;
                _respone.Message = ex.Message;
                return BadRequest(_respone);
            }
        }
    }
}
