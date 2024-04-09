using AutoMapper;
using BudgetAPI.DataAccess;
using BudgetAPI.Models;
using BudgetAPI.Models.Dto;
using BudgetAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace BudgetAPI.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {

        private readonly AppDbContext _context;
        private readonly IExpenseService _expenseService;
        private readonly IMapper _mapper;

        public BudgetRepository(AppDbContext context, IMapper mapper,IExpenseService expenseService)
        {
            _context = context;
            _expenseService = expenseService;
            _mapper = mapper;
        }

        public async Task<ReadBudgetDto?> CreateBudgetAsync(CreateBudgetDto createBudgetDto)
        {
            var budget = _mapper.Map<Budget>(createBudgetDto);
            _context.Budgets.Add(budget);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadBudgetDto>(budget);
        }

        public async Task<ReadBudgetDto?> DeleteBudgetAsync(int id)
        {
            var budget = await _context.Budgets.FirstOrDefaultAsync(b => b.Id == id);
            if(budget == null)
            {
                return null;
            }
            _context.Budgets.Remove(budget);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadBudgetDto>(budget);
        }

        public async Task<IEnumerable<ReadBudgetDto>> GetAllBudgetAsync(string userId)
        {
            var objs = await _context.Budgets.Where(o => o.UserId == userId).ToListAsync();
            var budgets = _mapper.Map<IEnumerable<ReadBudgetDto>>(objs);
            var result = new List<ReadBudgetDto>();
            foreach(var budget in budgets)
            {
                var obj = await UpdateBudget(budget);
                result.Add(obj);
            }
            return result;
        }

        public async Task<ReadBudgetDto?> GetBudgetAsync(int id)
        {
            var obj = await _context.Budgets.FirstOrDefaultAsync(b => b.Id == id);
            var budget = _mapper.Map<ReadBudgetDto>(obj);
            return await UpdateBudget(budget);
        }


        private async Task<ReadBudgetDto> UpdateBudget(ReadBudgetDto budget)
        {
            budget.UsedAmount = 0;
            budget.Categories = new List<CategoryDto>();
            var categories = await _expenseService.GetCategories(budget.UserId);
            foreach (var cate in categories)
            {
                var category = new CategoryDto
                {
                    Id = cate.Id,
                    UserId = cate.UserId,
                    Name = cate.Name,
                    UsedAmount = 0,
                    Expenses = new List<ExpenseDto>()
                };
                foreach (var expense in cate.Expenses)
                {
                    if (DateTime.Compare(expense.Date, budget.StartDate) >= 0  && DateTime.Compare(expense.Date, budget.EndDate) <= 0)
                    {
                        category.UsedAmount += expense.Amount;
                        category.Expenses.Add(expense);
                    }
                }
                budget.Categories.Add(category);
                budget.UsedAmount += category.UsedAmount;
            }
            return budget;
        }
    }
}
