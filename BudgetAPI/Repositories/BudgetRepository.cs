using AutoMapper;
using BudgetAPI.DataAccess;
using BudgetAPI.Models;
using BudgetAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace BudgetAPI.Repositories
{
    public class BudgetRepository : IBudgetRepository
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public BudgetRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReadBudgetDto?> CreateBudgetAsync(CreateBudgetDto createBudgetDto)
        {
            var budget = _mapper.Map<Budget>(createBudgetDto);
            budget.UsedAmount = 0;
            var result = await _context.Budgets.AddAsync(budget);
            if (result == null)
            {
                return null;
            }
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
            _context.Remove(budget);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadBudgetDto>(budget);
        }

        public async Task<IEnumerable<ReadBudgetDto>> GetAllBudgetAsync()
        {
            var budgets = _mapper.Map<IEnumerable<ReadBudgetDto>>(await _context.Budgets.ToListAsync());
            foreach(var budget in budgets)
            {
                budget.Categories = _mapper.Map<IEnumerable<ReadCategoryDto>>(
                    await _context.Categories.Where(c => c.BudgetId == budget.Id).ToListAsync());
            }
            return budgets;
        }

        public async Task<ReadBudgetDto?> GetBudgetAsync(int id)
        {
            var obj = await _context.Budgets.FirstOrDefaultAsync(b => b.Id == id);
            if (obj == null)
            {
                return null;
            }
            var budget = _mapper.Map<ReadBudgetDto>(obj);
            budget.Categories = _mapper.Map<IEnumerable<ReadCategoryDto>>(
                await _context.Categories.Where(c => c.BudgetId == budget.Id).ToListAsync());
            return budget;
        }

        public async Task<ReadBudgetDto?> UpdateBudgetAsyn(int id)
        {
            var budget = await _context.Budgets.FirstOrDefaultAsync(b => b.Id == id);
            if (budget == null)
            {
                return null;
            }
            budget.UsedAmount = 0;
            var cate = await _context.Categories.ToListAsync();
            foreach(var obj in cate)
            {
                if(obj.BudgetId == id)
                {
                    budget.UsedAmount += obj.UsedAmount;
                }
            }
            _context.Update(budget);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadBudgetDto>(budget);
        }
    }
}
