using Moq;
using WebAtrioApp.Application.Services;
using WebAtrioApp.Core.Entities;
using WebAtrioApp.Core.Interfaces;

namespace WebAtrioApp.Tests.Application.Services;

public class PersonServiceTests
{
    private readonly Mock<IPersonRepository> _mockPersonRepository;
    private readonly PersonService _personService;

    public PersonServiceTests()
    {
        _mockPersonRepository = new Mock<IPersonRepository>();
        _personService = new PersonService(_mockPersonRepository.Object);
    }

    [Fact]
    public async Task AddPerson_ShouldReturnPerson_WhenValid()
    {
        // Arrange
        var mockPersonRepository = new Mock<IPersonRepository>();
        var newPerson = new Person
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1)
        };

        // Simuler le comportement de AddAsync pour renvoyer le GUID de la personne
        mockPersonRepository.Setup(repo => repo.AddAsync(It.IsAny<Person>()))
            .ReturnsAsync(newPerson.Id); // Correct ici : renvoie un GUID

        // Act
        var result = await _personService.AddPersonAsync(newPerson.FirstName, newPerson.LastName, newPerson.DateOfBirth);


        var retrievedPerson = await _personService.GetPersonByIdAsync(result);

        // Assert
        Assert.NotNull(retrievedPerson); // Vérifier que la personne existe
        Assert.Equal(newPerson.FirstName, retrievedPerson.FirstName); // Vérifier le prénom
        Assert.Equal(newPerson.LastName, retrievedPerson.LastName); // Vérifier le nom
        Assert.Equal(newPerson.DateOfBirth, retrievedPerson.DateOfBirth); // Vérifier la date de naissance
        Assert.Equal(newPerson.Id, retrievedPerson.Id); // Vérifier l'ID

    }
}
