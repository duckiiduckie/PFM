using BudgetAPI.Models.Dto;
using BudgetAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BudgetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ResponeDto _respone;
        public BudgetController( IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
            _respone = new ResponeDto();
        }

        [HttpGet]
        public async Task<ActionResult<ResponeDto>> Get()
        {
            var result = await _budgetRepository.GetAllBudgetAsync();
            _respone.Result = result;
            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ResponeDto>> Get(int id)
        {
            var result = await _budgetRepository.GetBudgetAsync(id);
            _respone.Result = result;
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<ResponeDto>> Post([FromBody] CreateBudgetDto model)
        {
            var result = await _budgetRepository.CreateBudgetAsync(model);
            if(result == null) 
            {
                _respone.Result = null;
                _respone.Message = "Can't create budget";
                _respone.IsSuccess = false;
                return BadRequest(result);
            }
            _respone.Result = result;
            return Ok(result);
        }


        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<ResponeDto>> Delete(int id)
        {
            var result = await _budgetRepository.DeleteBudgetAsync(id);
            if (result == null)
            {
                _respone.Result = null;
                _respone.Message = "Can't delete budget";
                _respone.IsSuccess = false;
                return BadRequest(result);
            }
            _respone.Result = result;
            return Ok(result);
        }

    }
}
