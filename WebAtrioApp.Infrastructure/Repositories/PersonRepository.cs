using Microsoft.EntityFrameworkCore;
using WebAtrioApp.Core.Entities;
using WebAtrioApp.Core.Interfaces;
using WebAtrioApp.Infrastructure.Data;

namespace WebAtrioApp.Infrastructure.Repositories;

public class PersonRepository : IPersonRepository
{

    private readonly WebAtrioDbContext _dbContext;

    public PersonRepository(WebAtrioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Person person)
    {
        await _dbContext.Persons.AddAsync(person);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(Person person)
    {
        _dbContext.Persons.Update(person);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Person?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Persons
            .Include(p => p.Employments) // Inclure les emplois liés
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _dbContext.Persons
            .Include(p => p.Employments) // Inclure les emplois liés
            .OrderBy(p => p.LastName)
            .ThenBy(p => p.FirstName)
            .ToListAsync();
    }

    public async Task<IEnumerable<Person>> GetByCompanyNameAsync(string companyName)
    {
        return await _dbContext.Persons
            .Include(p => p.Employments)
            .Where(p => p.Employments.Any(e => e.CompanyName == companyName))
            .ToListAsync();
    }

    public async Task<IEnumerable<Employment>> GetEmploymentsByDateRangeAsync(Guid personId, DateTime startDate, DateTime endDate)
    {
        var person = await _dbContext.Persons
            .Include(p => p.Employments)
            .FirstOrDefaultAsync(p => p.Id == personId);

        if (person == null)
            return Enumerable.Empty<Employment>();

        return person.Employments
            .Where(e => e.StartDate <= endDate && (e.EndDate == null || e.EndDate >= startDate))
            .ToList();
    }
}
