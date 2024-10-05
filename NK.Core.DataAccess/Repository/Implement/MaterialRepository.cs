using NK.Core.Model.Entities;
using NK.Core.Model;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace NK.Core.DataAccess.Repository
{
    public class MaterialRepository : IMaterialRepository
    {
        private readonly AppDbContext _context;
        public MaterialRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Material> GetByIdAsync(string id)
        {
            return await _context.Materials.FindAsync(id);
        }

        public async Task<IEnumerable<Material>> GetAllAsync()
        {
            return await _context.Materials.ToListAsync();
        }

        public async Task<IEnumerable<Material>> FindAsync(Expression<Func<Material, bool>> predicate)
        {
            return await _context.Materials.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(Material material)
        {

            await _context.Materials.AddAsync(material);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Material material)
        {
            _context.Materials.Update(material);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Material material)
        {
            _context.Materials.Remove(material);
            await _context.SaveChangesAsync();
        }

        public async Task<Material> GetByNameMaterialAsync(string name)
        {
            return await _context.Materials.FirstOrDefaultAsync(p => p.Name == name);
        }
    }
}
