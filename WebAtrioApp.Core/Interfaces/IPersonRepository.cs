using WebAtrioApp.Core.Entities;

namespace WebAtrioApp.Core.Interfaces;

public interface IPersonRepository
{
    Task AddAsync(Person person);
    Task UpdateAsync(Person person);
    Task<Person?> GetByIdAsync(Guid id);
    Task<IEnumerable<Person>> GetAllAsync();
    Task<IEnumerable<Person>> GetByCompanyNameAsync(string companyName);
    Task<IEnumerable<Employment>> GetEmploymentsByDateRangeAsync(Guid personId, DateTime startDate, DateTime endDate);
}
