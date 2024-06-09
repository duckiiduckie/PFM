using BudgetAPI.Models.Dto;
using BudgetAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BudgetAPI.Controllers
{
    [Route("api/budget")]
    [ApiController]
    [Authorize]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetRepository _budgetRepository;
        private readonly ResponseDto _respone;
        public BudgetController( IBudgetRepository budgetRepository)
        {
            _budgetRepository = budgetRepository;
            _respone = new ResponseDto();
        }

        [HttpPost("create-budget")]
        public async Task<IActionResult> CreateBudget(CreateBudget createBudget)
        {
            try
            {
                var budget = await _budgetRepository.CreateBudget(createBudget);
                _respone.Result = budget;
                _respone.IsSuccess = true;
                return Ok(_respone);
            }
            catch (Exception ex)
            {
                _respone.Message = ex.Message;
                _respone.IsSuccess = false;
                return BadRequest(_respone);
            }
        }

        [HttpGet("get-budget/{id}")]
        public async Task<IActionResult> GetBudget(int id)
        {
            try
            {
                var budget = await _budgetRepository.GetBudgetById(id);
                _respone.Result = budget;
                _respone.IsSuccess = true;
                return Ok(_respone);
            }
            catch (Exception ex)
            {
                _respone.Message = ex.Message;
                _respone.IsSuccess = false;
                return BadRequest(_respone);
            }
        }

        [HttpGet("get-budgets/{userId}")]
        public async Task<IActionResult> GetBudgets(string userId)
        {
            try
            {
                var budgets = await _budgetRepository.GetBudgets(userId);
                _respone.Result = budgets;
                _respone.IsSuccess = true;
                return Ok(_respone);
            }
            catch (Exception ex)
            {
                _respone.Message = ex.Message;
                _respone.IsSuccess = false;
                return BadRequest(_respone);
            }
        }

        [HttpGet("get-budget-now/{userId}")]
        public async Task<IActionResult> GetBudgetNow(string userId)
        {
            try
            {
                var budget = await _budgetRepository.GetBudgetNow(userId);
                _respone.Result = budget;
                _respone.IsSuccess = true;
                return Ok(_respone);
            }
            catch (Exception ex)
            {
                _respone.Message = ex.Message;
                _respone.IsSuccess = false;
                return BadRequest(_respone);
            }
        }

        [HttpPut("update-budget/{id}")]
        public async Task<IActionResult> UpdateBudget(int id, CreateBudget createBudget)
        {
            try
            {
                var budget = await _budgetRepository.UpdateBudget(id, createBudget);
                _respone.Result = budget;
                _respone.IsSuccess = true;
                return Ok(_respone);
            }
            catch (Exception ex)
            {
                _respone.Message = ex.Message;
                _respone.IsSuccess = false;
                return BadRequest(_respone);
            }
        }

        [HttpDelete("delete-budget/{id}")]

        public async Task<IActionResult> DeleteBudget(int id)
        {
            try
            {
                var result = await _budgetRepository.DeleteBudget(id);
                _respone.Result = result;
                _respone.IsSuccess = true;
                return Ok(_respone);
            }
            catch (Exception ex)
            {
                _respone.Message = ex.Message;
                _respone.IsSuccess = false;
                return BadRequest(_respone);
            }
        }
    }
}
