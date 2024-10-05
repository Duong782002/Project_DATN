using NK.Core.Model.Entities;
using NK.Core.Model;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace NK.Core.DataAccess.Repository
{
    public class SoleRepository : ISoleRepository
    {
        private readonly AppDbContext _context;

        public SoleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Sole> GetByIdAsync(string id)
        {
            return await _context.Soles.FindAsync(id);
        }

        public async Task<IEnumerable<Sole>> GetAllAsync()
        {
            return await _context.Soles.ToListAsync();
        }

        public async Task<IEnumerable<Sole>> FindAsync(Expression<Func<Sole, bool>> predicate)
        {
            return await _context.Soles.Where(predicate).ToListAsync();
        }

        public async Task AddAsync(Sole sole)
        {
            await _context.Soles.AddAsync(sole);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Sole sole)
        {
            _context.Soles.Update(sole);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Sole sole)
        {
            _context.Soles.Remove(sole);
            await _context.SaveChangesAsync();
        }

        public async Task<Sole> GetSoleByNameAsync(string name)
        {
            return await _context.Soles.FirstOrDefaultAsync(s => s.Name == name);
        }
    }
}
