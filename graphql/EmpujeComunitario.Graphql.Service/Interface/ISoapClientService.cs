using EmpujeComunitario.Graphql.Common;
using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.Service.Interface
{
    public interface ISoapClientService
    {
        Task<BaseObjectResponse<list_associationsResponse1>> GetAllOrganization(List<string> id);
        Task<BaseObjectResponse<list_presidentsResponse1>> GetAllPresident(List<string> id);
    }
}
