using AutoMapper;
using IncomeAPI.Models;
using IncomeAPI.Models.Dto;
using IncomeAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace IncomeAPI.Repositories
{
    public class IncomeRepository : IIncomeRepository
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;
        public IncomeRepository(AppDbContext context, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _context = context;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        public async Task<ReadIncomeDto?> CreateIncomeAsync(CreateIncomeDto income)
        {

            var cate = await _categoryRepository.GetCategory(income.CategoryName , income.UserId);
            if (cate == null)
            {
                var category = new CreateCategoryDto
                {
                    Name = income.CategoryName,
                    UserId = income.UserId
                };
                await _categoryRepository.AddCategory(category);
            }

            var obj = _mapper.Map<Income>(income);
            obj.Category = _context.Categories.FirstOrDefault(c => c.Name == income.CategoryName);
            _context.Incomes.Add(obj);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadIncomeDto>(obj);
        }

        public async Task<ReadIncomeDto?> DeleteIncomeAsync(int id)
        {
            var obj = _context.Incomes.FirstOrDefault(o => o.Id == id);
            if(obj != null)
            {
                _context.Incomes.Remove(obj);
            }
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadIncomeDto>(obj);
        }

        public async Task<ReadIncomeDto?> GetIncomeAsync(int id)
        {
            var obj = _context.Incomes.FirstOrDefault(o => o.Id == id);
            await _context.SaveChangesAsync();
            var res = _mapper.Map<ReadIncomeDto>(obj);
            res.CategoryName = _context.Categories.FirstOrDefault(o => o.Id == obj.CategoryId).Name;
            return res;
        }

        public async Task<IEnumerable<ReadIncomeDto?>?> GetIncomesAsync(string userId)
        {
            var incomes = await _context.Incomes.Where(o => o.UserId == userId).ToListAsync();
            
            var res = _mapper.Map<List<ReadIncomeDto>>(incomes);
            foreach (var item in res)
            {
                var tmp = _mapper.Map<Income>(item);
                item.CategoryName = _context.Categories.FirstOrDefault(o => o.Id == tmp.CategoryId).Name;
            }
            return res;
        }

        public async Task<IEnumerable<ReadIncomeDto?>?> GetIncomesAsync(string userId,string type, int number)
        {
            type = type.ToLower();
            switch (type)
            {
                case "day":
                    var dayIncomes = await _context.Incomes.Where(e => e.Date.Day == number && e.UserId == userId).ToListAsync();
                    return _mapper.Map<List<ReadIncomeDto>>(dayIncomes);
                case "month":
                    var monthIncomes = await _context.Incomes.Where(e => e.Date.Month == number && e.UserId == userId).ToListAsync();
                    return _mapper.Map<List<ReadIncomeDto>>(monthIncomes);
                case "year":
                    var yearIncomes = await _context.Incomes.Where(e => e.Date.Year == number && e.UserId == userId).ToListAsync();
                    return _mapper.Map<List<ReadIncomeDto>>(yearIncomes);
                default:
                    return null;
            }
        }

        public async Task<ReadIncomeDto?> UpdateIncomeAsync(int id, CreateIncomeDto income)
        {

            var cate = await _categoryRepository.GetCategory(income.CategoryName, income.UserId);
            if (cate == null)
            {
                var category = new CreateCategoryDto
                {
                    Name = income.CategoryName,
                    UserId = income.UserId
                };
                await _categoryRepository.AddCategory(category);
            }

            var obj = _mapper.Map<Income>(income);
            obj.Id = id;
            obj.Category = _context.Categories.FirstOrDefault(c => c.Name == income.CategoryName);
            _context.Incomes.Update(obj);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadIncomeDto>(obj);
        }
    }
}
