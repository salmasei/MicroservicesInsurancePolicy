using InsuranceMicroService.Api.Models;

namespace InsuranceMicroService.Api.Services
{
    public interface IInsuranceService
    {
        Task<Insurance> GetInsurancePolicyByIdAsync(int id);
        Task<List<Insurance>> GetAllInsurancePoliciesAsync();
        Task<Insurance> CreateInsurancePolicyAsync(Insurance policy);
    }
}
