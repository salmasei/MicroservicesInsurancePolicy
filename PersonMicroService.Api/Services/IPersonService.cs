using PersonMicroService.Api.Models;

namespace PersonMicroService.Api.Services
{
    public interface IPersonService
    {
        Task<Person> GetPersonByIdAsync(int id);
        Task<List<Person>> GetAllPeopleAsync();
        Task<Person> CreatePersonAsync(Person person);
        Task UpdatePersonAsync(int id, Person person);
        Task DeletePersonAsync(int id);
    }
}
