namespace ExpenseTracker.Models;

public class Ausgabe
{
    public int AusgabeID { get; set; }

    public string Bezeichnung { get; set; } = string.Empty;

    public decimal Betrag { get; set; }

    public DateTime Datum { get; set; }

    public string? Beschreibung { get; set; }

    public DateTime ErstelltAm { get; set; }

    public int KategorieID { get; set; }

    public Kategorie? Kategorie { get; set; }

    public int ZahlungsartID { get; set; }

    public Zahlungsart? Zahlungsart { get; set; }
}