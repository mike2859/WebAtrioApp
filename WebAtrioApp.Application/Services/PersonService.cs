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

    public async Task<Guid> AddPersonAsync(string firstName, string lastName, DateTime birthDate)
    {
        if ((DateTime.Now.Year - birthDate.Year) > 150)
            throw new ArgumentException("Person age cannot exceed 150 years.");

        var person = new Person
        {
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = birthDate
        };

        var personId = await _personRepository.AddAsync(person);

        if (personId == Guid.Empty)
        {
            throw new InvalidOperationException("Failed to add person.");
        }
        return person.Id;
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

    public async Task<Person?> GetPersonByIdAsync(Guid id)
    {
        return await _personRepository.GetByIdAsync(id);
    }
    public async Task<IEnumerable<Person>> GetPersonsByCompanyAsync(string companyName)
    {
        var persons = await _personRepository.GetAllAsync();

        return persons.Where(p => p.Employments.Any(e => string.Equals(e.CompanyName, companyName, StringComparison.OrdinalIgnoreCase)));

    }


    public async Task<IEnumerable<Employment>> GetEmploymentsByDateRangeAsync(Guid personId, DateTime startDate, DateTime endDate)
    {

        // Récupérer la personne par son Id
        var person = await _personRepository.GetByIdAsync(personId);

        // Vérifier si la personne existe
        if (person == null)
        {
            throw new KeyNotFoundException("Person not found.");
        }

        // Filtrer les emplois de la personne entre les deux dates
        var employmentsInRange = person.Employments
            .Where(e => e.StartDate >= startDate && (e.EndDate <= endDate || e.EndDate == null))
            .ToList();

        return employmentsInRange;

    }
}
