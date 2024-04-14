using ClosedXML.Excel;
using ExpenseAPI.Models.Dto;
using ExpenseAPI.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace ExpenseAPI.Controllers
{
    [Route("api/expense")]
    [ApiController]
    [Authorize]
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
        [Route("getexpenses/{userId}")]
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
        [Route("exportexcel/{userId}")]

        public async Task<ActionResult<ResponeDto>> ExportExcel(string userId)
        {
            try
            {
                var incomes = await _expenseRepository.GetExpensesAsync(userId);
                DataTable dt = new DataTable();
                dt.TableName = "Incomes";
                dt.Columns.Add("Id", typeof(int));
                dt.Columns.Add("Amount", typeof(decimal));
                dt.Columns.Add("Description", typeof(string));
                dt.Columns.Add("Date", typeof(DateTime));
                foreach (var item in incomes)
                {
                    dt.Rows.Add(item.Id, item.Amount, item.Description, item.Date);
                }
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var sheet1 = wb.Worksheets.Add(dt);
                    sheet1.Column(1).Style.Font.FontColor = XLColor.Red;

                    sheet1.Columns(2, 4).Style.Font.FontColor = XLColor.Blue;

                    sheet1.Row(1).CellsUsed().Style.Fill.BackgroundColor = XLColor.Black;
                    sheet1.Row(1).Style.Font.FontColor = XLColor.White;

                    sheet1.Row(1).Style.Font.Bold = true;
                    sheet1.Row(1).Style.Font.Shadow = true;
                    sheet1.Row(1).Style.Font.Underline = XLFontUnderlineValues.Single;
                    sheet1.Row(1).Style.Font.VerticalAlignment = XLFontVerticalTextAlignmentValues.Superscript;
                    sheet1.Row(1).Style.Font.Italic = true;

                    sheet1.Rows(2, 3).Style.Font.FontColor = XLColor.AshGrey;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        var content = stream.ToArray();
                        _response.Result = File(content, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Incomes.xlsx");
                        return Ok(_response);
                    }
                }
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
