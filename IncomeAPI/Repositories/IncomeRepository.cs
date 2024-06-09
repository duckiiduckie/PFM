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
        public IncomeRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CreateAdditionalIncome> CreateAdditionalIncome(CreateAdditionalIncome createAdditionalIncome)
        {
            try
            {
                var additionalIncome = _mapper.Map<AdditionalIncome>(createAdditionalIncome);
                await _context.AdditionalIncomes.AddAsync(additionalIncome);
                await _context.SaveChangesAsync();
                return createAdditionalIncome;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CreateMainIncome> CreateMainIncome(CreateMainIncome createMainIncome)
        {
            try
            {
                var mainIncome = _mapper.Map<MainIncome>(createMainIncome);
                await _context.MainIncomes.AddAsync(mainIncome);
                await _context.SaveChangesAsync();
                return createMainIncome;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteAdditionalIncome(int id)
        {
            try 
            { 
                var additionalIncome = await _context.AdditionalIncomes.FirstOrDefaultAsync(x => x.Id == id);
                if (additionalIncome == null)
                {
                    return false;
                }
                _context.AdditionalIncomes.Remove(additionalIncome);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteMainIncome(int id)
        {
            try
            {
                var mainIncome = await _context.MainIncomes.FirstOrDefaultAsync(x => x.Id == id);
                if (mainIncome == null)
                {
                    return false;
                }
                _context.MainIncomes.Remove(mainIncome);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ReadMainIncome>> GetAllMainIncome()
        {
            try
            {
                var mainIncomes = await _context.MainIncomes.ToListAsync();
                return _mapper.Map<List<ReadMainIncome>>(mainIncomes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReadAdditionalIncome?> ReadAdditionalIncome(int id)
        {
            try
            {
                var additionalIncome = await _context.AdditionalIncomes.FirstOrDefaultAsync(x => x.Id == id);
                if (additionalIncome == null)
                {
                    return null;
                }
                return _mapper.Map<ReadAdditionalIncome>(additionalIncome);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ReadAdditionalIncome?>?> ReadAdditionalIncomes(string userId)
        {
            try
            {
                var additionalIncomes = await _context.AdditionalIncomes.Where(x => x.UserId == userId).ToListAsync();
                if (additionalIncomes == null)
                {
                    return null;
                }
                return _mapper.Map<List<ReadAdditionalIncome?>>(additionalIncomes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReadMainIncome?> ReadMainIncome(int id)
        {
            try
            {
                var mainIncome = await _context.MainIncomes.FirstOrDefaultAsync(x => x.Id == id);
                if (mainIncome == null)
                {
                    return null;
                }
                return _mapper.Map<ReadMainIncome>(mainIncome);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ReadMainIncome?>?> ReadMainIncomes(string userId)
        {
            try
            {
                var mainIncomes = await _context.MainIncomes.Where(x => x.UserId == userId).ToListAsync();
                if (mainIncomes == null)
                {
                    return null;
                }
                return _mapper.Map<List<ReadMainIncome?>>(mainIncomes);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReadAdditionalIncome> UpdateAdditionalIncome(int id, CreateAdditionalIncome createAdditionalIncome)
        {
            try
            {
                var additionalIncome = _mapper.Map<AdditionalIncome>(createAdditionalIncome);
                additionalIncome.Id = id;
                _context.AdditionalIncomes.Update(additionalIncome);
                await _context.SaveChangesAsync();
                return _mapper.Map<ReadAdditionalIncome>(additionalIncome);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ReadMainIncome> UpdateMainIncome(int id, CreateMainIncome createMainIncome)
        {
            try
            {
                var mainIncome = _mapper.Map<MainIncome>(createMainIncome);
                mainIncome.Id = id;
                _context.MainIncomes.Update(mainIncome);
                await _context.SaveChangesAsync();
                return _mapper.Map<ReadMainIncome>(mainIncome);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
