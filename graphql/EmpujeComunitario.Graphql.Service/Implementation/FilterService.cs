using AutoMapper;
using EmpujeComunitario.Graphql.Common;
using EmpujeComunitario.Graphql.Common.Model;
using EmpujeComunitario.Graphql.DataAccess.Entities;
using EmpujeComunitario.Graphql.DataAccess.Interface;
using EmpujeComunitario.Graphql.Service.Interface;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Graphql.Service.Implementation
{
    public class FilterService : IFilterService
    {
        private readonly IMapper _mapper;
        private readonly IUserSavedFilterRepository _filterRepository;
        public FilterService(IMapper mapper, IUserSavedFilterRepository filterRepository)
        {
            _mapper = mapper;
            _filterRepository = filterRepository;
        }

        public async Task<BaseObjectResponse<bool>> SaveUserFilter(QueryFilter filter, string userId)
        {
            var response = new BaseObjectResponse<bool>();
            try
            {
                var filterDb = _mapper.Map<UserSavedFilter>(filter);
                filterDb.UserId = Guid.Parse(userId);
                var result = await _filterRepository.SaveFilter(filterDb);
                return response.OkWithData(result);
            }
            catch (Exception ex)
            {
                return response.ExceptionWithData(ex.Message);
            }
        }
        public async Task<BaseObjectResponse<List<QueryFilter>>> GetAllFilterAsync(string userId)
        {
            var response = new BaseObjectResponse<List<QueryFilter>>();
            try
            {
                var filters = await _filterRepository.GetFiltersAsync(Guid.Parse(userId));
                var result = _mapper.Map<List<QueryFilter>>(filters);
                return response.OkWithData(result);
            }
            catch (Exception ex)
            {
                return response.ExceptionWithData(ex.Message);
            }
        }
        public async Task<BaseObjectResponse<bool>> DeleteFilterAsync(string name, string userId)
        {
            var response = new BaseObjectResponse<bool>();
            try
            {
                var filters = await _filterRepository.DeleteFilterAsync(name,Guid.Parse(userId));
                return response.OkWithData(filters);
            }
            catch (Exception ex)
            {
                return response.ExceptionWithData(ex.Message);
            }
        }


    }
}
