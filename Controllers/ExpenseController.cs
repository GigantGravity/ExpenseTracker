using ExpenseTracker.Models;
using ExpenseTracker.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseTracker.Controllers;

public class ExpenseController : Controller
{
    private readonly IExpenseRepository _expenseRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IPaymentMethodRepository _paymentMethodRepository;

    public ExpenseController(
        IExpenseRepository expenseRepository,
        ICategoryRepository categoryRepository,
        IPaymentMethodRepository paymentMethodRepository)
    {
        _expenseRepository = expenseRepository;
        _categoryRepository = categoryRepository;
        _paymentMethodRepository = paymentMethodRepository;
    }

    public async Task<IActionResult> Index()
    {
        var ausgaben = await _expenseRepository.GetAllAsync();
        return View(ausgaben);
    }

    public async Task<IActionResult> Create()
    {
        await LoadDropdownsAsync();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Ausgabe ausgabe)
    {
        if (!ModelState.IsValid)
        {
            await LoadDropdownsAsync();
            return View(ausgabe);
        }

        await _expenseRepository.AddAsync(ausgabe);

        return RedirectToAction(nameof(Index));
    }

    private async Task LoadDropdownsAsync()
    {
        var kategorien = await _categoryRepository.GetAllAsync();
        var zahlungsarten = await _paymentMethodRepository.GetAllAsync();

        ViewBag.Kategorien = new SelectList(kategorien, "KategorieID", "Name");
        ViewBag.Zahlungsarten = new SelectList(zahlungsarten, "ZahlungsartID", "Name");
    }
}