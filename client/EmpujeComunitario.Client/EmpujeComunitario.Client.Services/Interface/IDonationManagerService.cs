using EmpujeComunitario.Client.Common.Model;
using EmpujeComunitario.Client.Common.Model.DonationDtos;
using Grpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.Client.Services.Interface
{
    public interface IDonationManagerService
    {
        Task<BaseObjectResponse<GenericResponse>> CreateDonationInventoryAsync(DonationInventoryCreateDto createDonation, string auth);
        Task<BaseObjectResponse<UpdateDonationInventoryResponse>> UpdateDonationInventoryAsync(DonationInventoryUpdateDto update, string auth);
        Task<BaseObjectResponse<GenericResponse>> DeleteDonationInventoryAsync(DonationInventoryDeleteDto deleteDonation, string auth);
        Task<BaseObjectResponse<ListDonationInventoryResponse>> ListDonationInventoryAsync(string auth);
    }
}
