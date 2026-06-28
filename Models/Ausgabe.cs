using System.ComponentModel.DataAnnotations;

namespace ExpenseTracker.Models;

public class Ausgabe
{
    public int AusgabeID { get; set; }

    [Required(ErrorMessage = "Bitte eine Bezeichnung eingeben.")]
    [StringLength(100, ErrorMessage = "Die Bezeichnung darf maximal 100 Zeichen lang sein.")]
    public string Bezeichnung { get; set; } = string.Empty;

    [Range(0.01, 100000, ErrorMessage = "Der Betrag muss größer als 0 sein.")]
    public decimal Betrag { get; set; }

    [Required(ErrorMessage = "Bitte ein Datum auswählen.")]
    public DateTime Datum { get; set; }

    [StringLength(255, ErrorMessage = "Die Beschreibung darf maximal 255 Zeichen lang sein.")]
    public string? Beschreibung { get; set; }

    public DateTime ErstelltAm { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Bitte eine Kategorie auswählen.")]
    public int KategorieID { get; set; }

    public Kategorie? Kategorie { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Bitte eine Zahlungsart auswählen.")]
    public int ZahlungsartID { get; set; }

    public Zahlungsart? Zahlungsart { get; set; }
}