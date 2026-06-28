namespace ExpenseTracker.Models;

public class Zahlungsart
{
    public int ZahlungsartID { get; set; }

    public string Name { get; set; } = string.Empty;

    public List<Ausgabe> Ausgaben { get; set; } = new();
}