namespace ExpenseTracker.Models;

public class AusgabeLog
{
    public int LogID { get; set; }

    public int? AusgabeID { get; set; }

    public string? Bezeichnung { get; set; }

    public decimal? Betrag { get; set; }

    public DateTime? Datum { get; set; }

    public int? KategorieID { get; set; }

    public int? ZahlungsartID { get; set; }

    public string Aktion { get; set; } = string.Empty;

    public DateTime Zeitpunkt { get; set; }

    public string? Hostname { get; set; }

    public string? Benutzername { get; set; }
}