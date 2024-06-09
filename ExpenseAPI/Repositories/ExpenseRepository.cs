using AutoMapper;
using ExpenseAPI.Models;
using ExpenseAPI.Models.Dto;
using ExpenseAPI.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace ExpenseAPI.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {

        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ExpenseRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ReadDailyExpense> CreateDailyExpense(CreateDailyExpense createDailyExpense)
        {
            try
            {
                var dailyExpense = _mapper.Map<DailyExpense>(createDailyExpense);
                await _context.DailyExpenses.AddAsync(dailyExpense);
                await _context.SaveChangesAsync();
                return _mapper.Map<ReadDailyExpense>(dailyExpense);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReadFuturePlannedExpense> CreateFuturePlannedExpense(CreateFuturePlannedExpense createFuturePlannedExpense)
        {
            try
            {
                var futurePlanedExpense = _mapper.Map<FuturePlannedExpense>(createFuturePlannedExpense);
                await _context.FuturePlannedExpenses.AddAsync(futurePlanedExpense);
                await _context.SaveChangesAsync();
                return _mapper.Map<ReadFuturePlannedExpense>(futurePlanedExpense);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteDailyExpense(int id)
        {
            try
            {
                var dailyExpense = await _context.DailyExpenses.FirstOrDefaultAsync(x => x.Id == id);
                if (dailyExpense == null)
                {
                    return false;
                }
                _context.DailyExpenses.Remove(dailyExpense);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteFuturePlannedExpense(int id)
        {
            try
            {
                var futurePlannedExpense = await _context.FuturePlannedExpenses.FirstOrDefaultAsync(x => x.Id == id);
                if (futurePlannedExpense == null)
                {
                    return false;
                }
                _context.FuturePlannedExpenses.Remove(futurePlannedExpense);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReadDailyExpense?> GetDailyExpenseById(int id)
        {
            try
            {
                var dailyExpense = await _context.DailyExpenses.FirstOrDefaultAsync(x => x.Id == id);
                if (dailyExpense == null)
                {
                    return null;
                }
                return _mapper.Map<ReadDailyExpense>(dailyExpense);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            }

        public async Task<List<ReadDailyExpense?>?> GetDailyExpenses(string userId)
        {
            try
            {
                var dailyExpenses = await _context.DailyExpenses.Where(x => x.UserId == userId).ToListAsync();
                if (dailyExpenses == null)
                {
                    return null;
                }
                return _mapper.Map<List<ReadDailyExpense?>>(dailyExpenses);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReadFuturePlannedExpense?> GetFuturePlannedExpenseById(int id)
        {
            try
            {
                var futurePlannedExpense = await _context.FuturePlannedExpenses.FirstOrDefaultAsync(x => x.Id == id);
                if (futurePlannedExpense == null)
                {
                    return null;
                }
                return _mapper.Map<ReadFuturePlannedExpense>(futurePlannedExpense);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ReadFuturePlannedExpense?>?> GetFuturePlannedExpenses(string userId)
        {
            try
            {
                var futurePlannedExpenses = await _context.FuturePlannedExpenses.Where(x => x.UserId == userId).ToListAsync();
                if (futurePlannedExpenses == null)
                {
                    return null;
                }
                return _mapper.Map<List<ReadFuturePlannedExpense?>>(futurePlannedExpenses);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReadDailyExpense> UpdateDailyExpense(int id, CreateDailyExpense createDailyExpense)
        {
            try
            {
                var dailyExpense = _mapper.Map<DailyExpense>(createDailyExpense);
                dailyExpense.Id = id;
                _context.DailyExpenses.Update(dailyExpense);
                await _context.SaveChangesAsync();
                return _mapper.Map<ReadDailyExpense>(dailyExpense);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReadFuturePlannedExpense> UpdateFuturePlannedExpense(int id, CreateFuturePlannedExpense createFuturePlannedExpense)
        {
            try
            {
                var futurePlanedExpense = _mapper.Map<FuturePlannedExpense>(createFuturePlannedExpense);
                futurePlanedExpense.Id = id;
                _context.FuturePlannedExpenses.Update(futurePlanedExpense);
                await _context.SaveChangesAsync();
                return _mapper.Map<ReadFuturePlannedExpense>(futurePlanedExpense);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }  
}

