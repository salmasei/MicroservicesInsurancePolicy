using InsuranceMicroService.Api.Models;
using InsuranceMicroService.Api.Services;
using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("[controller]")]
public class InsuranceController : ControllerBase
{
    private readonly IInsuranceService _insuranceService;

    public InsuranceController(IInsuranceService insuranceService)
    {
        _insuranceService = insuranceService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetInsurancePolicyById(int id)
    {
        var policy = await _insuranceService.GetInsurancePolicyByIdAsync(id);
        if (policy == null)
        {
            return NotFound();
        }

        return Ok(policy);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllInsurancePolicies()
    {
        var policies = await _insuranceService.GetAllInsurancePoliciesAsync();
        return Ok(policies);
    }

    [HttpPost]
    public async Task<IActionResult> CreateInsurancePolicy([FromBody] Insurance policy)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var createdPolicy = await _insuranceService.CreateInsurancePolicyAsync(policy);
            return Ok(createdPolicy);           
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}