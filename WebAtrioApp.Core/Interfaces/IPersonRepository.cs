using WebAtrioApp.Core.Entities;

namespace WebAtrioApp.Core.Interfaces;

public interface IPersonRepository
{
    Task<Guid> AddAsync(Person person);
    Task<Guid> UpdateAsync(Person person);
    Task<Person?> GetByIdAsync(Guid id);
    Task<IEnumerable<Person>> GetAllAsync();
    Task<IEnumerable<Person>> GetByCompanyNameAsync(string companyName);
    Task<Person?> GetPersonByIdAsync(Guid id);
    Task<IEnumerable<Employment>> GetEmploymentsByDateRangeAsync(Guid personId, DateTime startDate, DateTime endDate);

}
