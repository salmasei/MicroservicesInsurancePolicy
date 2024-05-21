using Microsoft.Extensions.Caching.Memory;
using InsuranceMicroService.Api.Models;
using PersonMicroService.Api.Services;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceMicroService.Api.Services
{
    public class InsuranceService : IInsuranceService
    {
        private readonly IMemoryCache _cache;
        private readonly IPersonService _personService;

        public IMemoryCache Cache => _cache;

        public InsuranceService(IPersonService personService, IMemoryCache cache)
        {
            _personService = personService;
            _cache = cache;
        }

        public async Task<Insurance> GetInsurancePolicyByIdAsync(int id)
        {
            var policies = _cache.Get<List<Insurance>>("policyList") ?? new List<Insurance>();

            var policy = policies.FirstOrDefault(p => p.PolicyId == id);
            if (policy != null)
            {
                policy.Person = await _personService.GetPersonByIdAsync(policy.PersonId);
            }

            return policy;
        }

        public async Task<List<Insurance>> GetAllInsurancePoliciesAsync()
        {
            var policies = _cache.Get<List<Insurance>>("policyList") ?? new List<Insurance>();

            foreach (var policy in policies)
            {
                policy.Person = await _personService.GetPersonByIdAsync(policy.PersonId);
            }

            return policies;
        }

        public async Task<Insurance> CreateInsurancePolicyAsync(Insurance policy)
        {
            if (await _personService.GetPersonByIdAsync(policy.PersonId) == null)
            {
                throw new ArgumentException("Person not found");
            }

            var policies = _cache.Get<List<Insurance>>("policyList") ?? new List<Insurance>();

            policy.PolicyId = policies.Count + 1;
            policies.Add(policy);
            _cache.Set("policyList", policies);

            return policy;
        }
    }
}
