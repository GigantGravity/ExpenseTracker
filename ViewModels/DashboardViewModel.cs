using ExpenseTracker.Models;

namespace ExpenseTracker.ViewModels;

public class DashboardViewModel
{
    public decimal Gesamtausgaben { get; set; }

    public decimal AusgabenDiesenMonat { get; set; }

    public int AnzahlAusgaben { get; set; }

    public decimal DurchschnittlicheAusgabe { get; set; }

    public List<Ausgabe> NeuesteAusgaben { get; set; } = new();
    
    public Ausgabe? GroessteAusgabe { get; set; }

    public Kategorie? HaeufigsteKategorie { get; set; }

    public int AnzahlAusgabenHaeufigsteKategorie { get; set; }
}