using EmpujeComunitario.Graphql.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.DataAccess.Interface
{
    public interface IUserSavedFilterRepository
    {
        Task<bool> SaveFilter(UserSavedFilter filter);
        Task<List<UserSavedFilter>> GetFiltersAsync(Guid userId);
        Task<bool> DeleteFilterAsync(string name, Guid userId);
    }
}
