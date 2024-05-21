using Microsoft.Extensions.Caching.Memory;
using Moq;
using PersonMicroService.Api.Models;
using PersonMicroService.Api.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace PersonMicroService.Api.Tests
{
    public class PersonServiceTests
    {
        private readonly Mock<IMemoryCache> _mockCache;
        private readonly PersonService _personService;
        private List<Person> _peopleList;

        public PersonServiceTests()
        {
            _mockCache = new Mock<IMemoryCache>();
            _peopleList = new List<Person>
            {
                new Person { PersonId = 1, FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1980, 1, 1), Email = "john.doe@example.com" },
                new Person { PersonId = 2, FirstName = "Jane", LastName = "Doe", BirthDate = new DateTime(1990, 2, 2), Email = "jane.doe@example.com" }
            };

            SetupMockCache();

            _personService = new PersonService(_mockCache.Object);
        }

        private void SetupMockCache()
        {
            object cacheEntry;
            _mockCache.Setup(m => m.TryGetValue("peopleList", out cacheEntry))
                .Returns((string key, out object value) =>
                {
                    value = _peopleList;
                    return true;
                });

            _mockCache.Setup(m => m.CreateEntry(It.IsAny<object>()))
                .Returns(Mock.Of<ICacheEntry>);
        }

        [Fact]
        public async Task GetPersonByIdAsync_ShouldReturnPerson_WhenPersonExists()
        {
            // Act
            var person = await _personService.GetPersonByIdAsync(1);

            // Assert
            Assert.NotNull(person);
            Assert.Equal(1, person.PersonId);
        }

        [Fact]
        public async Task GetPersonByIdAsync_ShouldReturnNull_WhenPersonDoesNotExist()
        {
            // Act
            var person = await _personService.GetPersonByIdAsync(3);

            // Assert
            Assert.Null(person);
        }

        [Fact]
        public async Task GetAllPeopleAsync_ShouldReturnAllPeople()
        {
            // Act
            var people = await _personService.GetAllPeopleAsync();

            // Assert
            Assert.NotNull(people);
            Assert.Equal(2, people.Count);
        }

        [Fact]
        public async Task CreatePersonAsync_ShouldAddPersonToList()
        {
            // Arrange
            var newPerson = new Person { FirstName = "Mark", LastName = "Smith", BirthDate = new DateTime(2000, 3, 3), Email = "mark.smith@example.com" };

            // Act
            var createdPerson = await _personService.CreatePersonAsync(newPerson);
            var people = await _personService.GetAllPeopleAsync();

            // Assert
            Assert.NotNull(createdPerson);
            Assert.Equal(3, createdPerson.PersonId);
            Assert.Equal(3, people.Count);
        }

        [Fact]
        public async Task UpdatePersonAsync_ShouldUpdateExistingPerson()
        {
            // Arrange
            var updatedPerson = new Person { FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1980, 1, 1), Email = "john.doe@newdomain.com" };

            // Act
            await _personService.UpdatePersonAsync(1, updatedPerson);
            var person = await _personService.GetPersonByIdAsync(1);

            // Assert
            Assert.NotNull(person);
            Assert.Equal("john.doe@newdomain.com", person.Email);
        }

        [Fact]
        public async Task DeletePersonAsync_ShouldRemovePersonFromList()
        {
            // Act
            await _personService.DeletePersonAsync(1);
            var people = await _personService.GetAllPeopleAsync();

            // Assert
            Assert.Single(people);
            Assert.Null(people.FirstOrDefault(p => p.PersonId == 1));
        }
    }
}
