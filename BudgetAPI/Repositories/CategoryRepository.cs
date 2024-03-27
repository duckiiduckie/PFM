using AutoMapper;
using BudgetAPI.DataAccess;
using BudgetAPI.Models;
using BudgetAPI.Models.Dto;
using BudgetAPI.Services;
using Microsoft.EntityFrameworkCore;

namespace BudgetAPI.Repositories
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IBudgetRepository _budgetRepository;
        private readonly IExpenseService _expenseService;
        private readonly IMapper _mapper;

        public CategoryRepository(AppDbContext context, IMapper mapper, IBudgetRepository budgetRepository, IExpenseService expense)
        {
            _context = context;
            _mapper = mapper;
            _budgetRepository = budgetRepository;
            _expenseService = expense;
        }

        public async Task<ReadCategoryDto?> CreateCategoryAsync(CreateCategoryDto createCategoryDto)
        {
            var category = _mapper.Map<Category>(createCategoryDto);
            category.UsedAmount = 0;
            var result = await _context.Categories.AddAsync(category);
            if (result == null)
            {
                return null;
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadCategoryDto>(category);
        }

        public async Task<ReadCategoryDto?> DeleteCategoryAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(b => b.Id == id);
            if (category == null)
            {
                return null;
            }
            _context.Remove(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadCategoryDto>(category);
        }

        public async Task<IEnumerable<ReadCategoryDto>> GetAllCategoryAsync()
        {
            var Categorys = await _context.Categories.ToListAsync();
            return _mapper.Map<IEnumerable<ReadCategoryDto>>(Categorys);
        }

        public async Task<ReadCategoryDto?> GetCategoryAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(b => b.Id == id);
            if (category == null)
            {
                return null;
            }
            return _mapper.Map<ReadCategoryDto>(category);
        }

        public async Task<ReadCategoryDto?> UpdateCategoryAsync(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(b => b.Id == id);
            var budget = await _budgetRepository.GetBudgetAsync(category.BudgetId);
            var startDate = budget.StartDate;
            var endDate = budget.EndDate;
            var expenses = await _expenseService.GetAllExpenses();
            var filter = expenses.Where(e => e.CategoryId == id && e.Date >= startDate && e.Date <= endDate);
            var total = filter.Sum(e => e.Amount);
            category.UsedAmount = total;
            _context.Update(category);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadCategoryDto>(category);
        }

    }
}
