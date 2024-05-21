using Xunit;
using Moq;
using Microsoft.Extensions.Caching.Memory;
using System.Threading.Tasks;
using InsuranceMicroService.Api.Services;
using PersonMicroService.Api.Services;
using PersonMicroService.Api.Models;
using InsuranceMicroService.Api.Models;
using InsuranceMicroserviceTests.Mocks;

namespace InsuranceMicroService.Api.Tests
{

    public class InsuranceServiceTests
    {
        private readonly Mock<IPersonService> _mockPersonService;
        private readonly MockMemoryCache _mockMemoryCache;
        private readonly InsuranceService _insuranceService;

        public InsuranceServiceTests()
        {
            _mockPersonService = new Mock<IPersonService>();
            _mockMemoryCache = new MockMemoryCache();
            _insuranceService = new InsuranceService(_mockPersonService.Object, _mockMemoryCache);
        }

        [Fact]
        public async Task GetInsurancePolicyByIdAsync_ReturnsPolicyWithPerson()
        {
            // Arrange
            var policyId = 1;
            var personId = 1;
            var policy = new Insurance
            {
                PolicyId = policyId,
                PersonId = personId
            };
            var policies = new List<Insurance> { policy };

            _mockMemoryCache.Set("policyList", policies);
            _mockPersonService.Setup(s => s.GetPersonByIdAsync(personId)).ReturnsAsync(new Person { PersonId = personId });

            // Act
            var result = await _insuranceService.GetInsurancePolicyByIdAsync(policyId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(policyId, result.PolicyId);
            Assert.NotNull(result.Person);
            Assert.Equal(personId, result.Person.PersonId);
        }

        [Fact]
        public async Task GetAllInsurancePoliciesAsync_ReturnsAllPoliciesWithPersons()
        {
            // Arrange
            var policies = new List<Insurance>
        {
            new Insurance { PolicyId = 1, PersonId = 1 },
            new Insurance { PolicyId = 2, PersonId = 2 }
        };
            _mockMemoryCache.Set("policyList", policies);
            _mockPersonService.Setup(s => s.GetPersonByIdAsync(It.IsAny<int>()))
                              .ReturnsAsync((int id) => new Person { PersonId = id });

            // Act
            var result = await _insuranceService.GetAllInsurancePoliciesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.All(result, policy => Assert.NotNull(policy.Person));
        }

        [Fact]
        public async Task CreateInsurancePolicyAsync_AddsPolicyToCache()
        {
            // Arrange
            var newPolicy = new Insurance
            {
                PersonId = 1,
                PolicyType = "Health",
                StartDate = DateTime.UtcNow,
                EndDate = DateTime.UtcNow.AddYears(1),
                PremiumAmount = 500m
            };

            // Initialize an empty list for the policyList key in the cache
            _mockMemoryCache.Set("policyList", new List<Insurance>());

            _mockPersonService.Setup(s => s.GetPersonByIdAsync(newPolicy.PersonId)).ReturnsAsync(new Person { PersonId = newPolicy.PersonId });

            // Act
            var result = await _insuranceService.CreateInsurancePolicyAsync(newPolicy);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.PolicyId);
            Assert.Contains(result, _mockMemoryCache.Get<List<Insurance>>("policyList"));
        }
    }
}



