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
        [Route("exportexcel/{userId}")]

        public async Task<ActionResult<ResponeDto>> ExportExcel(string userId)
        {
            try
            {
                var incomes = await _incomeRepository.GetIncomesAsync(userId);
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
        public async Task<ActionResult<ResponeDto>> Post([FromBody] CreateIncomeDto income)
        {
            try
            {
                var newIncome = await _incomeRepository.CreateIncomeAsync(income);
                _response.Result = newIncome;
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
        public async Task<ActionResult<ResponeDto>> Put(int id, [FromBody] CreateIncomeDto income)
        {
            try
            {
                var updatedIncome = await _incomeRepository.UpdateIncomeAsync(id,income);
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
