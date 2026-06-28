using ExpenseTracker.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Controllers;

public class ExpenseController : Controller
{
    private readonly IExpenseRepository _expenseRepository;

    public ExpenseController(IExpenseRepository expenseRepository)
    {
        _expenseRepository = expenseRepository;
    }

    public async Task<IActionResult> Index()
    {
        var ausgaben = await _expenseRepository.GetAllAsync();

        return View(ausgaben);
    }
}