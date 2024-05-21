using PersonMicroService.Api.Models;

namespace InsuranceMicroService.Api.Models
{
    public class Insurance
    {
        public int PolicyId { get; set; }
        public int PersonId { get; set; }
        public string PolicyType { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal PremiumAmount { get; set; }

        public Person Person { get; set; } 
    }
}
