using WebAtrioApp.Core.Entities;
using WebAtrioApp.Core.Interfaces;

namespace WebAtrioApp.Application.Services;

public class PersonService
{
    private readonly IPersonRepository _personRepository;

    public PersonService(IPersonRepository personRepository)
    {
        _personRepository = personRepository;
    }

    public async Task AddPersonAsync(string firstName, string lastName, DateTime birthDate)
    {
        if ((DateTime.Now.Year - birthDate.Year) > 150)
            throw new ArgumentException("Person age cannot exceed 150 years.");

        var person = new Person
        {
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = birthDate
        };

        await _personRepository.AddAsync(person);
    }

    public async Task AddEmploymentAsync(Guid personId, string companyName, string position, DateTime startDate, DateTime? endDate = null)
    {
        var person = await _personRepository.GetByIdAsync(personId);
        if (person == null)
            throw new KeyNotFoundException("Person not found.");

        var employment = new Employment
        {
            CompanyName = companyName,
            Position = position,
            StartDate = startDate,
            EndDate = endDate
        };

        person.AddEmployment(employment);
        await _personRepository.UpdateAsync(person);
    }

    public async Task<IEnumerable<Person>> GetAllPersonsAsync()
    {
        var persons = await _personRepository.GetAllAsync();
        return persons.OrderBy(p => p.LastName)
                      .ThenBy(p => p.FirstName);
    }

    public async Task<IEnumerable<Person>> GetPersonsByCompanyAsync(string companyName)
    {
        var persons = await _personRepository.GetAllAsync();
        return persons.Where(p => p.Employments.Any(e => e.CompanyName == companyName));
    }
}
