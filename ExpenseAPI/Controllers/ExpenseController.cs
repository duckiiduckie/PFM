using ExpenseAPI.Models.Dto;
using ExpenseAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseAPI.Controllers
{
    [Route("api/expense")]
    [ApiController]
    [Authorize]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseRepository _expenseRepository;
        private ResponseDto _response;
        public ExpenseController(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
            _response = new ResponseDto();
        }

        [HttpPost("create-daily-expense")]
        public async Task<IActionResult> CreateDailyExpense([FromBody] CreateDailyExpense createDailyExpense)
        {
            try
            {
                var result = await _expenseRepository.CreateDailyExpense(createDailyExpense);
                _response.Result = result;
                _response.Message = "Daily expense created successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpPost("create-future-planned-expense")]

        public async Task<IActionResult> CreateFuturePlannedExpense([FromBody] CreateFuturePlannedExpense createFuturePlannedExpense)
        {
            try
            {
                var result = await _expenseRepository.CreateFuturePlannedExpense(createFuturePlannedExpense);
                _response.Result = result;
                _response.Message = "Future planned expense created successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpGet("read-daily-expenses/{userId}")]
        public async Task<IActionResult> ReadDailyExpenses(string userId)
        {
            try
            {
                var result = await _expenseRepository.GetDailyExpenses(userId);
                _response.Result = result;
                _response.Message = "Daily expenses retrieved successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpGet("read-future-planned-expenses/{userId}")]
        public async Task<IActionResult> ReadFuturePlannedExpenses(string userId)
        {
            try
            {
                var result = await _expenseRepository.GetFuturePlannedExpenses(userId);
                _response.Result = result;
                _response.Message = "Future planned expenses retrieved successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpGet("read-daily-expense/{id}")]

        public async Task<IActionResult> ReadDailyExpense(int id)
        {
            try
            {
                var result = await _expenseRepository.GetDailyExpenseById(id);
                _response.Result = result;
                _response.Message = "Daily expense retrieved successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpGet("read-future-planned-expense/{id}")]

        public async Task<IActionResult> ReadFuturePlannedExpense(int id)
        {
            try
            {
                var result = await _expenseRepository.GetFuturePlannedExpenseById(id);
                _response.Result = result;
                _response.Message = "Future planned expense retrieved successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpPut("update-daily-expense/{id}")]

        public async Task<IActionResult> UpdateDailyExpense(int id, [FromBody] CreateDailyExpense createDailyExpense)
        {
            try
            {
                var result = await _expenseRepository.UpdateDailyExpense(id, createDailyExpense);
                _response.Result = result;
                _response.Message = "Daily expense updated successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpPut("update-future-planned-expense/{id}")]

        public async Task<IActionResult> UpdateFuturePlannedExpense(int id, [FromBody] CreateFuturePlannedExpense createFuturePlannedExpense)
        {
            try
            {
                var result = await _expenseRepository.UpdateFuturePlannedExpense(id, createFuturePlannedExpense);
                _response.Result = result;
                _response.Message = "Future planned expense updated successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpDelete("delete-daily-expense/{id}")]

        public async Task<IActionResult> DeleteDailyExpense(int id)
        {
            try
            {
                var result = await _expenseRepository.DeleteDailyExpense(id);
                _response.Result = result;
                _response.Message = "Daily expense deleted successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpDelete("delete-future-planned-expense/{id}")]

        public async Task<IActionResult> DeleteFuturePlannedExpense(int id)
        {
            try
            {
                var result = await _expenseRepository.DeleteFuturePlannedExpense(id);
                _response.Result = result;
                _response.Message = "Future planned expense deleted successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);


        }
    }
}
