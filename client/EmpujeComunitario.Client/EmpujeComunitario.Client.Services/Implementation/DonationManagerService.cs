using AutoMapper;
using EmpujeComunitario.Client.Common.Model.DonationDtos;
using EmpujeComunitario.Client.Services.Interface;
using EmpujeComunitario.MessageFlow.Common.Model;
using Grpc;
using static Grpc.DonationInventoryService;

namespace EmpujeComunitario.Client.Services.Implementation
{
    public class DonationManagerService : IDonationManagerService
    {
        private readonly DonationInventoryServiceClient _client;
        private readonly IMapper _mapper;
        public DonationManagerService(DonationInventoryServiceClient donationClient, IMapper mapper)
        {
            _client = donationClient;
            _mapper = mapper;
        }
        public async Task<BaseObjectResponse<GenericResponse>> CreateDonationInventoryAsync(DonationInventoryCreateDto createDonation, string auth)
        {
            BaseObjectResponse<GenericResponse> response = new BaseObjectResponse<GenericResponse>();
            try
            {
                CreateDonationInventoryRequest createDonationInventoryRequest = _mapper.Map<CreateDonationInventoryRequest>(createDonation);
                createDonationInventoryRequest.Token = auth;
                var result = await _client.CreateDonationInventoryAsync(createDonationInventoryRequest);
                return result.Success
                    ? response.OkWithData(result)
                    : response.BadRequestWithoutData(result.Message);
            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la creación: {ex.Message} {ex?.InnerException?.Message}");
            }
        }

        public async Task<BaseObjectResponse<UpdateDonationInventoryResponse>> UpdateDonationInventoryAsync(DonationInventoryUpdateDto update, string auth)
        {
            BaseObjectResponse<UpdateDonationInventoryResponse> response = new BaseObjectResponse<UpdateDonationInventoryResponse>();
            try
            {
                UpdateDonationInventoryRequest updateRequest = _mapper.Map<UpdateDonationInventoryRequest>(update);
                updateRequest.Token = auth;

                var result = await _client.UpdateDonationInventoryAsync(updateRequest);
                return result.Success
                    ? response.OkWithData(result)
                    : response.BadRequestWithoutData(result.Message);
            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la creación: {ex.Message} {ex?.InnerException?.Message}");
            }
        }

        public async Task<BaseObjectResponse<GenericResponse>> DeleteDonationInventoryAsync(DonationInventoryDeleteDto deleteDonation, string auth)
        {
            BaseObjectResponse<GenericResponse> response = new BaseObjectResponse<GenericResponse>();
            try
            {
                DeleteDonationInventoryRequest deleteDonationInventoryRequest = _mapper.Map<DeleteDonationInventoryRequest>(deleteDonation);
                deleteDonationInventoryRequest.Token = auth;

                var result = await _client.DeleteDonationInventoryAsync(deleteDonationInventoryRequest);
                return result.Success
                    ? response.OkWithData(result)
                    : response.BadRequestWithoutData(result.Message);
            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la creación: {ex.Message} {ex?.InnerException?.Message}");
            }
        }
        public async Task<BaseObjectResponse<ListDonationInventoryResponse>> ListDonationInventoryAsync(string auth)
        {
            BaseObjectResponse<ListDonationInventoryResponse> response = new BaseObjectResponse<ListDonationInventoryResponse>();
            try
            {
                ListDonationInventoryRequest listRequest = new ListDonationInventoryRequest();
                listRequest.Token = auth;
                var result = await _client.ListDonationInventoryAsync(listRequest);
                return result.Success
                    ? response.OkWithData(result)
                    : response.BadRequestWithoutData(result.Message);
            }
            catch (Exception ex)
            {
                return response.ExceptionWithData($"Ocurrio un error durante la creación: {ex.Message} {ex?.InnerException?.Message}");
            }
        }
    }
}
