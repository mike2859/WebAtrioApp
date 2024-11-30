namespace WebAtrioApp.Core.Entities;

public class Person
{

    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<Employment> Employments { get; set; } = new List<Employment>();  // Liste des emplois

    // Méthode pour ajouter un emploi
    public void AddEmployment(Employment employment)
    {
        if (employment != null)
        {
            Employments.Add(employment);
        }
    }

    // Optionnel : Méthode pour supprimer un emploi si nécessaire
    public void RemoveEmployment(Employment employment)
    {
        if (employment != null)
        {
            Employments.Remove(employment);
        }
    }
}
