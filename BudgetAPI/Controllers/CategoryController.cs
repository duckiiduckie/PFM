using BudgetAPI.Models.Dto;
using BudgetAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BudgetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ResponeDto _respone;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
            _respone = new ResponeDto();
        }

        [HttpGet]
        public async Task<ActionResult<ResponeDto>> Get() 
        {
            var result = await _categoryRepository.GetAllCategoryAsync();
            _respone.Result = result;
            return Ok(_respone);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ResponeDto>> Get(int id)
        {
            await _categoryRepository.UpdateCategoryAsync(id);
            var result = await _categoryRepository.GetCategoryAsync(id);
            if (result == null)
            {
                _respone.Result = null;
                _respone.Message = "Not Found";
            }
            _respone.Result = result;
            return Ok(_respone);
        }

        [HttpPost]
        public async Task<ActionResult<ResponeDto>> Post([FromBody] CreateCategoryDto model)
        {
            var result = await _categoryRepository.CreateCategoryAsync(model);
            if(result == null)
            {
                _respone.Result = null;
                _respone.Message = "Can't create";
                _respone.IsSuccess = false;
                return BadRequest(_respone);
            }
            _respone.Result = result;
            return Ok(_respone);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<ResponeDto>> Delete(int id)
        {
            var result = await _categoryRepository.DeleteCategoryAsync(id);
            if (result == null)
            {
                _respone.Result = null;
                _respone.Message = "Can't delete";
                _respone.IsSuccess = false;
                return BadRequest(_respone);
            }
            _respone.Result = result;
            return Ok(_respone);
        }
    }
}
