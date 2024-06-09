using ClosedXML.Excel;
using IncomeAPI.Models.Dto;
using IncomeAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace IncomeAPI.Controllers
{
    [Route("api/income")]
    [ApiController]
    [Authorize]
    public class IncomeController : ControllerBase
    {
        private readonly IIncomeRepository _incomeRepository;
        private ResponseDto _response;
        public IncomeController(IIncomeRepository incomeRepository)
        {
            _incomeRepository = incomeRepository;
            _response = new ResponseDto();
        }

        [HttpPost("create-main-income")]
        public async Task<IActionResult> CreateMainIncome([FromBody] CreateMainIncome createMainIncome)
        {
            try
            {
                var result = await _incomeRepository.CreateMainIncome(createMainIncome);
                _response.Result = result;
                _response.Message = "Main income created successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpPost("create-additional-income")]
        public async Task<IActionResult> CreateAdditionalIncome([FromBody] CreateAdditionalIncome createAdditionalIncome)
        {
            try
            {
                var result = await _incomeRepository.CreateAdditionalIncome(createAdditionalIncome);
                _response.Result = result;
                _response.Message = "Additional income created successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpGet("read-main-income/{id}")]
        public async Task<IActionResult> ReadMainIncome(int id)
        {
            try
            {
                var result = await _incomeRepository.ReadMainIncome(id);
                _response.Result = result;
                _response.Message = "Main income read successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpGet("read-additional-income/{id}")]
        public async Task<IActionResult> ReadAdditionalIncome(int id)
        {
            try
            {
                var result = await _incomeRepository.ReadAdditionalIncome(id);
                _response.Result = result;
                _response.Message = "Additional income read successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpGet("read-main-incomes/{userId}")]
        public async Task<IActionResult> ReadMainIncomes(string userId)
        {
            try
            {
                var result = await _incomeRepository.ReadMainIncomes(userId);
                _response.Result = result;
                _response.Message = "Main incomes read successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpGet("read-additional-incomes/{userId}")]
        public async Task<IActionResult> ReadAdditionalIncomes(string userId)
        {
            try
            {
                var result = await _incomeRepository.ReadAdditionalIncomes(userId);
                _response.Result = result;
                _response.Message = "Additional incomes read successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpPut("update-main-income/{id}")]
        public async Task<IActionResult> UpdateMainIncome(int id, [FromBody] CreateMainIncome createMainIncome)
        {
            try
            {
                var result = await _incomeRepository.UpdateMainIncome(id, createMainIncome);
                _response.Result = result;
                _response.Message = "Main income updated successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpPut("update-additional-income/{id}")]
        public async Task<IActionResult> UpdateAdditionalIncome(int id, [FromBody] CreateAdditionalIncome createAdditionalIncome)
        {
            try
            {
                var result = await _incomeRepository.UpdateAdditionalIncome(id, createAdditionalIncome);
                _response.Result = result;
                _response.Message = "Additional income updated successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpDelete("delete-main-income/{id}")]
        public async Task<IActionResult> DeleteMainIncome(int id)
        {
            try
            {
                var result = await _incomeRepository.DeleteMainIncome(id);
                _response.Result = result;
                _response.Message = "Main income deleted successfully";
            }
            catch (Exception ex)
            {
                _response.Message = ex.Message;
                _response.IsSuccess = false;
            }
            return Ok(_response);
        }

        [HttpDelete("delete-additional-income/{id}")]
        public async Task<IActionResult> DeleteAdditionalIncome(int id)
        {
            try
            {
                var result = await _incomeRepository.DeleteAdditionalIncome(id);
                _response.Result = result;
                _response.Message = "Additional income deleted successfully";
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
