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
        private readonly IProduceMessage _produceMessage;
        private readonly IMapper _mapper;
        

        public BudgetRepository(AppDbContext context, IMapper mapper,IExpenseService expenseService, IProduceMessage produceMessage)
        {
            _context = context;
            _expenseService = expenseService;
            _mapper = mapper;
            _produceMessage = produceMessage;
        }

        public async Task<ReadBudget> CreateBudget(CreateBudget createBudgetDto)
        {
            try
            {
                var budget = _mapper.Map<Budget>(createBudgetDto);
                await _context.Budgets.AddAsync(budget);
                await _context.SaveChangesAsync();
                return _mapper.Map<ReadBudget>(budget);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteBudget(int id)
        {
            try
            {
                var budget = await _context.Budgets.FirstOrDefaultAsync(b => b.Id == id);
                if (budget == null)
                {
                    return false;
                }
                _context.Budgets.Remove(budget);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ReadBudget>> GetAllBudgets()
        {
            try
            {
                var result = await _context.Budgets.ToListAsync();
                return _mapper.Map<List<ReadBudget>>(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReadBudget> GetBudgetById(int id)
        {
            try
            {
                var budget = await _context.Budgets.FirstOrDefaultAsync(b => b.Id == id);
                if (budget == null)
                {
                    return null;
                }
                var readBudget = _mapper.Map<ReadBudget>(budget);
                return await UpdateBudgetAmount(readBudget);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReadBudget> GetBudgetNow(string userId)
        {
            try
            {
                var budget = _context.Budgets.Where(b => b.UserId == userId).OrderByDescending(b => b.Time).FirstOrDefault();
                return await UpdateBudgetAmount(_mapper.Map<ReadBudget>(budget));
            }
            catch (Exception ex)
            {
                /*throw new Exception(ex.Message);*/
                return null;
            }
        }

        public async Task<List<ReadBudget>> GetBudgets(string userId)
        {
            try
            {
                var budgets = await _context.Budgets.Where(b => b.UserId == userId).ToListAsync();
                var result = new List<ReadBudget>();
                foreach (var budget in budgets)
                {
                    var readBudget = _mapper.Map<ReadBudget>(budget);
                    result.Add(await UpdateBudgetAmount(readBudget));
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<ReadBudget> UpdateBudget(int id, CreateBudget createBudgetDto)
        {
            try
            {
                var budget = _mapper.Map<Budget>(createBudgetDto);
                budget.Id = id;
                _context.Budgets.Update(budget);
                await _context.SaveChangesAsync();
                return _mapper.Map<ReadBudget>(budget);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

     
        private async Task<ReadBudget> UpdateBudgetAmount(ReadBudget budget)
        {
            budget.UsedEssential = 0;
            budget.UsedSavingAndInvestment = 0;
            budget.UsedWant = 0;
            var result = await _expenseService.GetReadDailyExpensesAsync(budget.UserId);
            foreach(var item in result)
            {
                if(item.Type == "Essential")
                {
                    budget.UsedEssential += item.Amount;
                }
                else if(item.Type == "Want")
                {
                    budget.UsedWant += item.Amount;
                }
                else if(item.Type == "Saving and Investment")
                {
                    budget.UsedSavingAndInvestment += item.Amount;
                }
            }
            budget.UsedAmount = budget.UsedEssential + budget.UsedSavingAndInvestment + budget.UsedWant;
            if(budget.UsedAmount > budget.Amount && budget.IsMailSent == false)
            {
                var budgetObj = await _context.Budgets.FirstOrDefaultAsync(b => b.Id == budget.Id);
                budgetObj.IsMailSent = true;
                await _context.SaveChangesAsync();
                await _produceMessage.ProduceMessageAsync(budget.UserEmail);
            }
            return budget;
        }
    }
}
