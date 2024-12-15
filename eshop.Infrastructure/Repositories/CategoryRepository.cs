using eshop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eshop.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository<Category>
    {
        public readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Category entity)
        {
            await _context.Categories.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<IReadOnlyCollection<Category>> GetAllAsync(CancellationToken cancellationToken = default)
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            var category = await _context.Categories.FindAsync(id);
            return category is null ? throw new ArgumentException($"Category with id {id} not found") : category;
        }

        public async Task<Category> GetCategoryWithProductsAsync(int categoryId, CancellationToken cancellationToken = default)
        {
            var categories = await _context.Categories
                         .Include(c => c.Products)
                         .FirstOrDefaultAsync(c => c.Id == categoryId);
            if (categories is null)
                throw new Exception("Category is null");

            return categories;

        }

        public Task UpdateAsync(Category entity)
        {
            throw new NotImplementedException();
        }
    }
}
