using AutoMapper;
using IncomeAPI.DataAccess;
using IncomeAPI.Models;
using IncomeAPI.Models.Dto;
using Microsoft.EntityFrameworkCore;

namespace IncomeAPI.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public CategoryRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReadCategoryDto?>?> GetCategories(string userId)
        {
            var categories = _mapper.Map<IEnumerable<ReadCategoryDto>>(
                await _context.Categories.Where(o => o.UserId == userId).ToListAsync());
            foreach (var category in categories)
            {
                var incomes = await _context.Incomes.Where(e => e.CategoryId == category.Id).ToListAsync();
                category.Incomes = _mapper.Map<IEnumerable<ReadIncomeDto>>(incomes);
            }
            return categories;
        }
        
        public async Task<ReadCategoryDto?> GetCategory(int id)
        {
            var category = _mapper.Map<ReadCategoryDto>(await _context.Categories.FindAsync(id));
            if (category == null)
            {
                return null;
            }
            var incomes = await _context.Incomes.Where(e => e.CategoryId == category.Id).ToListAsync();
            category.Incomes = _mapper.Map<IEnumerable<ReadIncomeDto>>(incomes);
            return category;
        }

        public async Task<ReadCategoryDto?> GetCategoryByName(string name)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == name);
            if(category == null)
            {
                return null;
            }
            return _mapper.Map<ReadCategoryDto>(category);
        }

        public async Task<ReadCategoryDto?> AddCategory(CreateCategoryDto category)
        {
            var newCategory = _mapper.Map<Category>(category);
            if (await _context.Categories.AnyAsync(c => c.Name == newCategory.Name))
            {
                return null;
            }
            _context.Categories.Add(newCategory);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadCategoryDto>(newCategory);
        }

        public async Task<ReadCategoryDto?> UpdateCategory(int id, CreateCategoryDto category)
        {
            var obj = _mapper.Map<Category>(category);
            obj.Id = id;
            _context.Categories.Update(obj);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadCategoryDto>(obj);
        }

        public async Task<ReadCategoryDto?> DeleteCategory(int id)
        {
            var categoryToDelete = await _context.Categories.FirstOrDefaultAsync(o => o.Id == id);
            if (categoryToDelete == null)
            {
                return null;
            }
            _context.Categories.Remove(categoryToDelete);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReadCategoryDto>(categoryToDelete);
        }
    }
}
