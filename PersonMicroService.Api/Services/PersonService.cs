using Microsoft.Extensions.Caching.Memory;
using PersonMicroService.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonMicroService.Api.Services
{
    public class PersonService : IPersonService
    {
        private readonly IMemoryCache _cache;
        public IMemoryCache Cache => _cache;

        public PersonService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            var people = _cache.Get<List<Person>>("peopleList") ?? new List<Person>();
            return people.FirstOrDefault(p => p.PersonId == id);
        }

        public async Task<List<Person>> GetAllPeopleAsync()
        {
            var people = _cache.Get<List<Person>>("peopleList") ?? new List<Person>();
            return people.ToList();
        }

        public async Task<Person> CreatePersonAsync(Person person)
        {
            var people = _cache.Get<List<Person>>("peopleList") ?? new List<Person>();
            person.PersonId = people.Count > 0 ? people.Max(p => p.PersonId) + 1 : 1;
            people.Add(person);
            _cache.Set("peopleList", people);
            return person;
        }

        public async Task UpdatePersonAsync(int id, Person person)
        {
            var people = _cache.Get<List<Person>>("peopleList") ?? new List<Person>();
            var existingPerson = people.FirstOrDefault(p => p.PersonId == id);
            if (existingPerson == null)
            {
                throw new ArgumentException("Person not found");
            }

            existingPerson.FirstName = person.FirstName;
            existingPerson.LastName = person.LastName;
            existingPerson.BirthDate = person.BirthDate;
            existingPerson.Email = person.Email;
            _cache.Set("peopleList", people);
        }

        public async Task DeletePersonAsync(int id)
        {
            var people = _cache.Get<List<Person>>("peopleList") ?? new List<Person>();
            var person = people.FirstOrDefault(p => p.PersonId == id);
            if (person == null)
            {
                throw new ArgumentException("Person not found");
            }
            people.Remove(person);
            _cache.Set("peopleList", people);
        }
    }
}
