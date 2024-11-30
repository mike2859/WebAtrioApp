namespace WebAtrioApp.Application.DTOs;

public record CreatePersonDto(string FirstName, string LastName, DateTime BirthDate);
public record CreateEmploymentDto(string CompanyName, string Position, DateTime StartDate, DateTime? EndDate);
