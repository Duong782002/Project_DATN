using Microsoft.EntityFrameworkCore;
using NK.Core.Model;
using NK.Core.Model.Entities;

namespace NK.Core.DataAccess.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;

        public CategoryRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Category category)
        {
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    await _dbContext.Categories.AddAsync(category);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task DeleteAsync(string id)
        {
            var categoryToDelete = await _dbContext.Categories.FindAsync(id);

            if (categoryToDelete != null)
            {
                using (var transaction = await _dbContext.Database.BeginTransactionAsync())
                {
                    try
                    {
                        _dbContext.Categories.Remove(categoryToDelete);
                        await _dbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception)
                    {
                        await transaction.RollbackAsync();
                        throw;
                    }
                }
            }
            else
            {
                // Throw an exception or handle the case when the category is not found
                throw new Exception("Category not found.");
            }
        }

        public async Task<IEnumerable<object>> GetAllAsync()
        {
            var categories = await _dbContext.Categories.Select(c => new { c.Id, c.Name, c.Description }).ToListAsync();

            return categories;
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            return await _dbContext.Categories.FindAsync(id);
        }

        public async Task<Category> GetCategoryByNameAsync(string name)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(s => s.Name == name);
        }

        public async Task UpdateAsync(string id, Category category)
        {
            var searchCategory = await _dbContext.Categories.FindAsync(id);
            if (searchCategory != null)
            {
                searchCategory.Name = category.Name;
                searchCategory.Description = category.Description;
                searchCategory.ModifiedDate = DateTime.Now;
            }
            using (var transaction = await _dbContext.Database.BeginTransactionAsync())
            {
                try
                {
                    _dbContext.Categories.Update(searchCategory);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
