using ExpenseTracker.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Controllers;

public class CategoryController : Controller
{
    private readonly ExpenseTrackerContext _context;

    public CategoryController(ExpenseTrackerContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var kategorien = await _context.Kategorien
            .OrderBy(k => k.Name)
            .ToListAsync();

        return View(kategorien);
    }
}