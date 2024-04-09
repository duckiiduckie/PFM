using IncomeAPI.Models.Dto;
using IncomeAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IncomeAPI.Controllers
{
    [Route("api/income")]
    [ApiController]
    [Authorize]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeRepository _incomeRepository;
        private ResponeDto _response;
        public IncomeController(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
            _response = new ResponeDto();
        }
        [HttpGet("getincomes/{userId}")]
        public async Task<ActionResult<ResponeDto>> Get(string userId)
        {
            try
            {
                var incomes = await _incomeRepository.GetIncomesAsync(userId);
                _response.Result = incomes;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ResponeDto>> Get(int id)
        {
            try
            {
                var income = await _incomeRepository.GetIncomeAsync(id);
                _response.Result = income;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }

        [HttpGet]
        [Route("{userId}/filter/{type}/{number:int}")]
        public async Task<ActionResult<ResponeDto>> Get(string userId, string type, int number)
        {
            try
            {
                var incomes = await _incomeRepository.GetIncomesAsync(userId, type, number);
                _response.Result = incomes;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }

        [HttpPost]
        public async Task<ActionResult<ResponeDto>> Post([FromBody] CreateIncomeDto expense)
        {
            try
            {
                var newExpense = await _incomeRepository.CreateIncomeAsync(expense);
                _response.Result = newExpense;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<ResponeDto>> Put(int id, [FromBody] CreateIncomeDto expense)
        {
            try
            {
                var updatedIncome = await _incomeRepository.UpdateIncomeAsync(id,expense);
                _response.Result = updatedIncome;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<ResponeDto>> Delete( int id)
        {
            try
            {
                var deletedIncome = await _incomeRepository.DeleteIncomeAsync(id);
                _response.Result = deletedIncome;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
                return BadRequest(_response);
            }
        }
    }
}
