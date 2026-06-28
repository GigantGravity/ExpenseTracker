namespace ExpenseTracker.Models;

public class Kategorie
{
    public int KategorieID { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Icon { get; set; }

    public List<Ausgabe> Ausgaben { get; set; } = new();
}