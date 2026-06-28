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
    
    private async Task<ExpenseCreateViewModel> CreateExpenseViewModelAsync(Ausgabe ausgabe)
    {
        var kategorien = await _categoryRepository.GetAllAsync();
        var zahlungsarten = await _paymentMethodRepository.GetAllAsync();

        return new ExpenseCreateViewModel
        {
            Ausgabe = ausgabe,
            Kategorien = kategorien
                .Select(k => new SelectListItem
                {
                    Value = k.KategorieID.ToString(),
                    Text = k.Name
                })
                .ToList(),
            Zahlungsarten = zahlungsarten
                .Select(z => new SelectListItem
                {
                    Value = z.ZahlungsartID.ToString(),
                    Text = z.Name
                })
                .ToList()
        };
    }
    
    public async Task<IActionResult> Edit(int id)
    {
        var ausgabe = await _expenseRepository.GetByIdAsync(id);

        if (ausgabe == null)
        {
            return NotFound();
        }

        var model = await CreateExpenseViewModelAsync(ausgabe);

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ExpenseCreateViewModel model)
    {
        if (id != model.Ausgabe.AusgabeID)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            model = await CreateExpenseViewModelAsync(model.Ausgabe);
            return View(model);
        }

        await _expenseRepository.UpdateAsync(model.Ausgabe);

        return RedirectToAction(nameof(Index));
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