using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAtrioApp.Application.DTOs;
using WebAtrioApp.Application.Services;

namespace WebAtrioApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly PersonService _personService;

        public PersonsController(PersonService personService)
        {
            _personService = personService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPerson([FromBody] CreatePersonDto personDto)
        {
            try
            {
                await _personService.AddPersonAsync(personDto.FirstName, personDto.LastName, personDto.BirthDate);
                return Ok("Person added successfully.");
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("{id}/employment")]
        public async Task<IActionResult> AddEmployment(Guid id, [FromBody] CreateEmploymentDto employmentDto)
        {
            try
            {
                await _personService.AddEmploymentAsync(id, employmentDto.CompanyName, employmentDto.Position, employmentDto.StartDate, employmentDto.EndDate);
                return Ok("Employment added successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPersons()
        {
            var persons = await _personService.GetAllPersonsAsync();
            return Ok(persons);
        }

        [HttpGet("by-company")]
        public async Task<IActionResult> GetPersonsByCompany([FromQuery] string companyName)
        {
            var persons = await _personService.GetPersonsByCompanyAsync(companyName);
            return Ok(persons);
        }
    }
}
