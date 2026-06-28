using ExpenseTracker.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseTracker.ViewModels;

public class ExpenseCreateViewModel
{
    public Ausgabe Ausgabe { get; set; } = new()
    {
        Datum = DateTime.Today
    };

    public List<SelectListItem> Kategorien { get; set; } = new();

    public List<SelectListItem> Zahlungsarten { get; set; } = new();
}