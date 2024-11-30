namespace WebAtrioApp.Core.Entities;

public class Employment
{
    public Guid Id { get; set; }
    public string CompanyName { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }  // La fin de l'emploi peut être nulle pour les emplois en cours
}
