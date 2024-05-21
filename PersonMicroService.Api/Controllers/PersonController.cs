using Microsoft.AspNetCore.Mvc;
using PersonMicroService.Api.Models;
using PersonMicroService.Api.Services;
using System.Runtime.Intrinsics.X86;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly IPersonService _personService;    

    public PersonController(IPersonService personService)
    {
        _personService = personService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetPersonById(int id)
    {
        var person = await _personService.GetPersonByIdAsync(id);
        if (person == null)
        {
            return NotFound();
        }

        return Ok(person);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPeople()
    {
        var people = await _personService.GetAllPeopleAsync();
        return Ok(people);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePerson([FromBody] Person person)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdPerson = await _personService.CreatePersonAsync(person);        
        return Ok(createdPerson);        
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePerson(int id, [FromBody] Person person)
    {
        if (id != person.PersonId)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            await _personService.UpdatePersonAsync(id, person);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePerson(int id)
    {
        try
        {
            await _personService.DeletePersonAsync(id);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }

        return NoContent();
    }
}
