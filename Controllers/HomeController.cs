using System.Diagnostics;
using ExpenseTracker.Models;
using ExpenseTracker.Repositories.Interfaces;
using ExpenseTracker.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers;

public class HomeController : Controller
{
    private readonly IExpenseRepository _expenseRepository;

    public HomeController(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<IActionResult> Index()
    {
        var haeufigsteKategorie = await _expenseRepository.GetMostUsedCategoryAsync();

        var model = new DashboardViewModel
        {
            Gesamtausgaben = await _expenseRepository.GetTotalAmountAsync(),
            AusgabenDiesenMonat = await _expenseRepository.GetTotalAmountForCurrentMonthAsync(),
            AnzahlAusgaben = await _expenseRepository.GetCountAsync(),
            DurchschnittlicheAusgabe = await _expenseRepository.GetAverageAmountAsync(),

            GroessteAusgabe = await _expenseRepository.GetLargestExpenseAsync(),

            HaeufigsteKategorie = haeufigsteKategorie.Kategorie,
            AnzahlAusgabenHaeufigsteKategorie = haeufigsteKategorie.Anzahl,

            NeuesteAusgaben = await _expenseRepository.GetLatestAsync(5)
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
        });
    }
}