using ExpenseTracker.Models;
using ExpenseTracker.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExpenseTracker.ViewModels;

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
        var model = new ExpenseCreateViewModel
        {
            Kategorien = (await _categoryRepository.GetAllAsync())
                .Select(k => new SelectListItem
                {
                    Value = k.KategorieID.ToString(),
                    Text = k.Name
                })
                .ToList(),

            Zahlungsarten = (await _paymentMethodRepository.GetAllAsync())
                .Select(z => new SelectListItem
                {
                    Value = z.ZahlungsartID.ToString(),
                    Text = z.Name
                })
                .ToList()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ExpenseCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Kategorien = (await _categoryRepository.GetAllAsync())
                .Select(k => new SelectListItem
                {
                    Value = k.KategorieID.ToString(),
                    Text = k.Name
                })
                .ToList();

            model.Zahlungsarten = (await _paymentMethodRepository.GetAllAsync())
                .Select(z => new SelectListItem
                {
                    Value = z.ZahlungsartID.ToString(),
                    Text = z.Name
                })
                .ToList();

            return View(model);
        }

        await _expenseRepository.AddAsync(model.Ausgabe);

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