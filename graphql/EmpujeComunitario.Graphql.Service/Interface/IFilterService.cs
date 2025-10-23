using EmpujeComunitario.Graphql.Common;
using EmpujeComunitario.Graphql.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.Service.Interface
{
    public interface IFilterService
    {
        Task<BaseObjectResponse<bool>> SaveUserFilter(QueryFilter filter, string userId);
        Task<BaseObjectResponse<List<QueryFilter>>> GetAllFilterAsync(string userId);
        Task<BaseObjectResponse<bool>> DeleteFilterAsync(string name, string userId);
    }
}
