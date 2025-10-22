using EmpujeComunitario.Graphql.DataAccess.Context;
using EmpujeComunitario.Graphql.DataAccess.Entities;
using EmpujeComunitario.Graphql.DataAccess.Interface;
using Microsoft.EntityFrameworkCore;


namespace EmpujeComunitario.Graphql.DataAccess.Implementation
{
    public class UserSavedFilterRepository : IUserSavedFilterRepository
    {
        private readonly MessageFlowDbContext _context;
        public UserSavedFilterRepository(MessageFlowDbContext context)
        {
            _context = context;
        }
        public async Task<bool> SaveFilter(UserSavedFilter filter)
        {
            var existing = await _context.UserSavedFilters
                 .FirstOrDefaultAsync(f => f.UserId == filter.UserId && f.Name == filter.Name);

            if (existing != null)
            {
                // Actualizar los campos necesarios
                existing.Filter = filter.Filter;
                existing.Name = filter.Name;
                
                _context.UserSavedFilters.Update(existing);
            }
            else
            {
                // Crear nuevo
                _context.UserSavedFilters.Add(filter);
            }

            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<List<UserSavedFilter>> GetFiltersAsync(Guid userId)
        {
            var result = _context.UserSavedFilters.Where(x=> x.UserId == userId ).ToList();
            return result;
        }

        public async Task<bool> DeleteFilterAsync(string name ,Guid userId)
        {
            var filter = await _context.UserSavedFilters.FirstOrDefaultAsync(x => x.Name == name && x.UserId == userId);
            _context.UserSavedFilters.Remove(filter);
            return true;
        }
    }
}
