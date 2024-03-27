using AutoMapper;
using ExpenseAPI.Models;
using ExpenseAPI.Models.Dto;
using ExpenseAPI.DataAccess;

namespace ExpenseAPI.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public ExpenseRepository(AppDbContext context, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _context = context;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<ReadExpenseDto?> CreateExpenseAsync(CreateExpenseDto expense)
        {

            var cate = await _categoryRepository.GetCategoryByName(expense.CategoryName);
            if (cate == null)
            {
                var category = new CreateCategoryDto
                {
                    Name = expense.CategoryName
                };
                await _categoryRepository.AddCategory(category);
            }

            var obj = _mapper.Map<Expense>(expense);
            obj.Category = _context.Categories.FirstOrDefault(c => c.Name == expense.CategoryName);
            _context.Expenses.Add(obj);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadExpenseDto>(obj);
        }

        public async Task<ReadExpenseDto?> DeleteExpenseAsync(int id)
        {
            var obj = _context.Expenses.FirstOrDefault(o => o.Id == id);
            if(obj != null)
            {
                _context.Expenses.Remove(obj);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadExpenseDto>(obj);
        }

        public async Task<ReadExpenseDto?> GetExpenseAsync(int id)
        {
            var obj = _context.Expenses.FirstOrDefault(o => o.Id == id);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadExpenseDto>(obj);
        }

        public async Task<IEnumerable<ReadExpenseDto?>?> GetExpensesAsync(string userId)
        {
            var expenses = _context.Expenses.Where(o => o.UserId == userId).ToList();
            await _context.SaveChangesAsync();
            return _mapper.Map<List<ReadExpenseDto>>(expenses);
        }

        public async Task<IEnumerable<ReadExpenseDto?>?> GetExpensesAsync(string userId,string type, int number)
        {
            type = type.ToLower();
            switch (type)
            {
                case "day":
                    var dayExpenses = _context.Expenses.Where(e => e.Date.Day == number && e.UserId == userId).ToList();
                    await _context.SaveChangesAsync();
                    return _mapper.Map<List<ReadExpenseDto>>(dayExpenses);
                case "month":
                    var monthExpenses = _context.Expenses.Where(e => e.Date.Month == number && e.UserId == userId).ToList();
                    await _context.SaveChangesAsync();
                    return _mapper.Map<List<ReadExpenseDto>>(monthExpenses);
                case "year":
                    var yearExpenses = _context.Expenses.Where(e => e.Date.Year == number && e.UserId == userId).ToList();
                    await _context.SaveChangesAsync();
                    return _mapper.Map<List<ReadExpenseDto>>(yearExpenses);
                default:
                    return null;
            }
        }

        public async Task<ReadExpenseDto?> UpdateExpenseAsync(int id, CreateExpenseDto expense)
        {

            var cate = await _categoryRepository.GetCategoryByName(expense.CategoryName);
            if (cate == null)
            {
                var category = new CreateCategoryDto
                {
                    Name = expense.CategoryName
                };
                await _categoryRepository.AddCategory(category);
            }

            var obj = _mapper.Map<Expense>(expense);
            obj.Id = id;
            obj.Category = _context.Categories.FirstOrDefault(c => c.Name == expense.CategoryName);
            _context.Expenses.Update(obj);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadExpenseDto>(obj);
        }
    }
}
