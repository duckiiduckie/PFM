using ExpenseAPI.Models.Dto;
using ExpenseAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseAPI.Controllers
{
    [Route("api/expense")]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseRepository _expenseRepository;
        private ResponeDto _response;
        public ExpenseController(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
            _response = new ResponeDto();
        }
        [HttpGet]
        [Route("{userId}")]
        public async Task<ActionResult<ResponeDto>> Get(string userId)
        {
            try
            {
                var expenses = await _expenseRepository.GetExpensesAsync(userId);
                _response.Result = expenses;
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
                var expense = await _expenseRepository.GetExpenseAsync(id);
                _response.Result = expense;
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
        public async Task<ActionResult<ResponeDto>> Get(string userId,string type, int number)
        {
            try
            {
                var expenses = await _expenseRepository.GetExpensesAsync(userId, type, number);
                _response.Result = expenses;
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
        public async Task<ActionResult<ResponeDto>> Post([FromBody] CreateExpenseDto expense)
        {
            try
            {
                var newExpense = await _expenseRepository.CreateExpenseAsync(expense);
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
        public async Task<ActionResult<ResponeDto>> Put(int id, [FromBody] CreateExpenseDto expense)
        {
            try
            {
                var updatedExpense = await _expenseRepository.UpdateExpenseAsync(id,expense);
                _response.Result = updatedExpense;
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
        public async Task<ActionResult<ResponeDto>> Delete(int id)
        {
            try
            {
                var deletedExpense = await _expenseRepository.DeleteExpenseAsync(id);
                _response.Result = deletedExpense;
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
